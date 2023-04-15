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

public class MqttDevicesPowerStatesWorker
{
    private readonly IMqttClient _client;
    private readonly MqttClientOptions _options;
    private string[] Topics { get; set; }
    private readonly Dictionary<string, int> _statuses;
    private readonly Dictionary<string, List<DevicePowerStateRecord>> _sharedStorage;
    private readonly Timer _workersLiveTimer;

    public MqttDevicesPowerStatesWorker(string[] topics, Dictionary<string, List<DevicePowerStateRecord>> sharedStorage,
        int ttl = 10000)
    {
        Topics = topics;
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
        foreach (var topic in Topics)
        {
            PublishMqttMessage("cmnd", topic, "Status1");
        }
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
            foreach (var topic in Topics)
            {
                var topicFilter = new MqttTopicFilterBuilder().WithTopic($"stat/{topic}/STATUS").Build();
                await _client.SubscribeAsync(topicFilter);
            }
        };

        _client.DisconnectedAsync += e =>
        {
            Console.WriteLine("Disconnected");
            return Task.CompletedTask;
        };

        _client.ApplicationMessageReceivedAsync += e =>
        {
            var data = ParseMessageToJsonObject(e.ApplicationMessage);
            if (data != null)
                ProcessMessage(data, e.ApplicationMessage);
            return Task.CompletedTask;
        };
    }
    
    public bool PublishMqttMessage(string type, string topic, string command, string msg = "")
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic($"{type}/{topic}/{command}")
            .WithPayload(msg)
            .Build();
        if (_client.IsConnected)
        {
            var mqttClientPublishResult = _client.PublishAsync(message).Result;
            return mqttClientPublishResult.IsSuccess;
        }

        return false;
    }

    private void ProcessMessage(JsonObject data, MqttApplicationMessage msg)
    {
        if (_statuses.ContainsKey(msg.Topic)) return;
        _statuses.Add(msg.Topic,
            int.Parse(data["Status"]["Power"].ToString()));
        if (_statuses.Count < Topics.Length) return;
        InsertDataToStorage();
        Stop();
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

    private void InsertDataToStorage()
    {
        foreach (var device in _statuses)
        {
            if (_sharedStorage.ContainsKey(device.Key))
            {
                _sharedStorage[device.Key].Add(new DevicePowerStateRecord()
                    { Date = DateTime.Now, State = device.Value });
            }
            else
            {
                _sharedStorage.Add(device.Key, new List<DevicePowerStateRecord>());
                _sharedStorage[device.Key].Add(new DevicePowerStateRecord()
                    { Date = DateTime.Now, State = device.Value });
            }
        }
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