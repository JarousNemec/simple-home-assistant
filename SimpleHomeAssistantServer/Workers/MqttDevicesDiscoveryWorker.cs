using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;
using System.Timers;
using MQTTnet;
using MQTTnet.Client;
using SimpleHomeAssistantServer.Models;
using Timer = System.Timers.Timer;

namespace SimpleHomeAssistantServer.Workers;

public class MqttDevicesDiscoveryWorker
{
    private readonly IMqttClient _client;
    private readonly MqttClientOptions _options;
    private readonly Dictionary<string, int> _statuses;
    private readonly List<Device> _sharedStorage;
    private readonly Timer _workersLiveTimer;

    public MqttDevicesDiscoveryWorker(List<Device> sharedStorage,
        int ttl = 10000)
    {
        _sharedStorage = sharedStorage;
        _statuses = new Dictionary<string, int>();
        var mqttFactory = new MqttFactory();
        _client = mqttFactory.CreateMqttClient();
        var config = ConfigurationManager.AppSettings;

        _options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer(config.Get("BrokerIp"), int.Parse(config.Get("BrokerPort") ?? "1883"))
            .WithCredentials(config.Get("Username"), config.Get("Password"))
            .WithCleanSession()
            .Build();
        InitMqttClientMethods();

        _workersLiveTimer = new Timer();
        _workersLiveTimer.Interval = ttl;
        _workersLiveTimer.AutoReset = false;
        _workersLiveTimer.Elapsed += WorkersLiveTimerOnElapsed;
    }

    private void WorkersLiveTimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        Stop();
    }

    private bool _running = true;

    public void Run()
    {
        ConnectToMqttBroker();
        _workersLiveTimer.Start();
        PublishMqttMessage("cmnd", "tasmotas", "Status0");
        Console.WriteLine("Discover sent ...");
        while (_running)
        {
            Thread.Sleep(100);
        }
    }

    private void Stop()
    {
        DisconnectFromMqttBroker();
        _client.Dispose();
        _workersLiveTimer.Stop();
        _workersLiveTimer.Dispose();
        _running = false;
    }

    private void InitMqttClientMethods()
    {
        _client.ConnectedAsync += async e =>
        {
            Console.WriteLine("Connected");
            var topicFilter = new MqttTopicFilterBuilder().WithTopic("#").Build();
            await _client.SubscribeAsync(topicFilter);
        };

        _client.DisconnectedAsync += e =>
        {
            Console.WriteLine("Disconnected");
            return Task.CompletedTask;
        };

        _client.ApplicationMessageReceivedAsync += e =>
        {
            var data = ParseMessageToJsonObject(e.ApplicationMessage);
            if (data != null) ProcessDiscoverMessage(e.ApplicationMessage, data);
            return Task.CompletedTask;
        };
    }

    private void ProcessDiscoverMessage(MqttApplicationMessage msg, JsonObject jsonObject)
    {
        if (IsValidDiscoverMsgResponse(jsonObject))
        {
            var mac = jsonObject["StatusNET"]?["Mac"]?.ToString();
            var topic = jsonObject["Status"]?["Topic"]?.ToString();
            if (mac != null && _sharedStorage.All(x => x.Mac != mac) && topic != null)
            {
                _sharedStorage.Add(new Device(jsonObject));
            }
        }
    }

    private bool IsValidDiscoverMsgResponse(JsonObject jsonObject)
    {
        return jsonObject.ContainsKey("Status") && jsonObject.ContainsKey("StatusPRM") &&
               jsonObject.ContainsKey("StatusFWR") && jsonObject.ContainsKey("StatusLOG") &&
               jsonObject.ContainsKey("StatusNET");
    }

    private bool PublishMqttMessage(string type, string topic, string command, string msg = "")
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic($"{type}/{topic}/{command}")
            .WithPayload(msg)
            .Build();
        if (!_client.IsConnected) return false;
        var mqttClientPublishResult = _client.PublishAsync(message).Result;
        return mqttClientPublishResult.IsSuccess;
    }

    private JsonObject? ParseMessageToJsonObject(MqttApplicationMessage msg)
    {
        if (msg.Payload == null)
            return null;
        var payload = Encoding.UTF8.GetString(msg.Payload);
        Console.WriteLine($"Topic: {msg.Topic} , Message: {payload}");
        try
        {
            var data = JsonNode.Parse(payload) as JsonObject;
            return data;
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
        }

        return null;
    }

    private void ConnectToMqttBroker()
    {
        retry:
        var _ = _client.ConnectAsync(_options).Result;
        if (!_client.IsConnected)
            goto retry;
    }

    private void DisconnectFromMqttBroker()
    {
        _client.DisconnectAsync();
    }
}