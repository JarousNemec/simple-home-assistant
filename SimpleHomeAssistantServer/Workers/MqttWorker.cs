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

public abstract class MqttWorker
{
    private readonly IMqttClient _client;
    private readonly MqttClientOptions _options;
    private protected readonly System.Timers.Timer _workersLiveTimer;

    protected MqttWorker(string[] workingTopicsPaths,int ttl = 10000)
    {
        var mqttFactory = new MqttFactory();
        _client = mqttFactory.CreateMqttClient();
        var config = ConfigurationManager.AppSettings;

        _options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer(config.Get("BrokerIp"), int.Parse(config.Get("BrokerPort") ?? "1883"))
            .WithCredentials(config.Get("Username"), config.Get("Password"))
            .WithCleanSession()
            .Build();
        InitMqttClientMethods(workingTopicsPaths);

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
        SetupRun();
        _workersLiveTimer.Start();
        while (_running)
        {
            Thread.Sleep(100);
        }
    }

    protected abstract void SetupRun();

    protected virtual void Stop()
    {
        DisconnectFromMqttBroker();
        _client.Dispose();
        _workersLiveTimer.Stop();
        _workersLiveTimer.Dispose();
        _running = false;
    }

    private void InitMqttClientMethods(string[] workingTopics)
    {
        _client.ConnectedAsync += async e =>
        {
            Logger.Success("Mqttworker Connected");
            foreach (var topic in workingTopics)
            {
                var topicFilter = new MqttTopicFilterBuilder().WithTopic(topic).Build();
                await _client.SubscribeAsync(topicFilter);
            }
        };

        _client.DisconnectedAsync += e =>
        {
            Logger.Success("Mqttworker Disconnected");
            _client.ReconnectAsync();
            return Task.CompletedTask;
        };

        _client.ApplicationMessageReceivedAsync += e =>
        {
            var data = ParseMessageToJsonObject(e.ApplicationMessage);
            if (data != null) ProcessMessage(e.ApplicationMessage, data);
            return Task.CompletedTask;
        };
    }

    protected abstract void ProcessMessage(MqttApplicationMessage msg, JsonObject jsonObject);

    public bool PublishMqttMessage(string type, string topic, string command, string msg = "")
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic($"{type}/{topic}/{command}")
            .WithPayload(msg)
            .Build();
        if (_client.IsConnected)
        {
            Logger.Info($"Mqttworker pushing: topic = {message.Topic}, payload = {message.Payload}");
            var mqttClientPublishResult = _client.PublishAsync(message).Result;
            Logger.Warning($"Mqttworker pushing result of (topic = {message.Topic}, payload = {msg}) = {mqttClientPublishResult.IsSuccess}");
            return mqttClientPublishResult.IsSuccess;
        }
        Logger.Error("Mqttworker cant publish the msg because client isnt connected!!!");
        return false;
    }

    private JsonObject? ParseMessageToJsonObject(MqttApplicationMessage msg)
    {
        if (msg.Payload == null)
            return null;
        var payload = Encoding.UTF8.GetString(msg.Payload);
        Logger.Info($"Topic: {msg.Topic} , Message: {payload}");
        try
        {
            var data = JsonNode.Parse(payload) as JsonObject;
            return data;
        }
        catch (Exception exception)
        {
            Logger.Error("Mqttworker error with parsing to json:"+exception.Message);
        }

        return null;
    }

    private void ConnectToMqttBroker()
    {
        retry:
        try
        {
            var _ = _client.ConnectAsync(_options).Result;
        }
        catch (Exception e)
        {
            Logger.Error("Mqttworker error:"+e.Message);
            Thread.Sleep(500);
            goto retry;
        }

        if (!_client.IsConnected)
        {
            Logger.Warning("Mqttworker retry connect");
            goto retry;
        }
            
    }

    private void DisconnectFromMqttBroker()
    {
        _client.DisconnectAsync();
    }
}