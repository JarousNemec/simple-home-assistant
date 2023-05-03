using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using MQTTnet;
using MQTTnet.Client;
using System.Configuration;
using System.Timers;
using SimpleHomeAssistantServer.Enums;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantServer.Workers;
using Timer = System.Timers.Timer;

namespace SimpleHomeAssistantServer;

public class MqttManager
{
    private IMqttClient _client;
    private MqttClientOptions _options;
    public List<Device> DevicesRegister;
    private Timer _discoveryTimer;
    private StatisticsManager _statisticsManager;
    private DiscoveryStatus _discoveryStatus;

    public MqttManager()
    {
        _discoveryStatus = new DiscoveryStatus();
        DevicesRegister = new List<Device>();
        var mqttFactory = new MqttFactory();
        _client = mqttFactory.CreateMqttClient();
        var config = ConfigurationManager.AppSettings;

        _options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer(config.Get("BrokerIp"), int.Parse(config.Get("BrokerPort") ?? "1883")) // todo: parametrize
            .WithCredentials(config.Get("Username"), config.Get("Password")) // todo: parametrize
            .WithCleanSession()
            .Build();
        InitMqttClientMethods();

        _discoveryTimer = new Timer();
        _discoveryTimer.Interval = 60000;
        _discoveryTimer.AutoReset = false;
        _discoveryTimer.Elapsed += DiscoveryTimerElapsed;
        _discoveryTimer.Start();
    }

    private void DiscoveryTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        DiscoverAvailableDevices();
    }

    public void SetStatisticManager(StatisticsManager manager)
    {
        _statisticsManager = manager;
    }

    public void DiscoverAvailableDevices()
    {
        if (_discoveryStatus.State) return;
        var statisticsWorker = new Thread(new MqttDevicesDiscoveryWorker(DevicesRegister, _discoveryStatus).Run);
        statisticsWorker.Start();
    }

    public string GetAllDiscoveredDevices()
    {
        var records = _statisticsManager.GetTodayLastRecords();
        if (records != null)
        {
            foreach (var device in DevicesRegister)
            {
                device.Power = records[device.Topic].State;
            }
        }

        return JsonSerializer.Serialize(DevicesRegister.ToArray());
    }

    public bool ToggleDeviceState(string topic)
    {
        return PublishMqttMessage("cmnd", topic, "Power", "Toggle");
    }

    public bool SetTimer(string json)
    {
        var settings = JsonSerializer.Deserialize<TimerSettings>(json);
        return settings != null && PublishMqttMessage("cmnd", settings.Topic, settings.TimerName, json);
    }
    
    public bool SetDeviceTopic(string json)
    {
        var data = JsonSerializer.Deserialize<BasicMessage>(json);
        if (data == null) return false;
        
        var isTopicKnown = DevicesRegister.Any(x => x.Topic == data.Payload);
        return !isTopicKnown && SendBasicMessage(json, "Topic", true);
    }

    public bool SetDeviceName(string json)
    {
        return SendBasicMessage(json, "DeviceName", true);
    }

    public bool SetFriendlyName(string json)
    {
        return SendBasicMessage(json, "FriendlyName1", true);
    }

    private bool SendBasicMessage(string json, string command, bool needRestart = false)
    {
        var data = JsonSerializer.Deserialize<BasicMessage>(json);
        if (data == null) return false;
        var result = PublishMqttMessage("cmnd", data.Topic, command, data.Payload);
        if (needRestart)
        {
            PublishMqttMessage("cmnd", data.Topic, "Restart", "1");
        }
            
        return result;
    }

    private void InitMqttClientMethods()
    {
        _client.ConnectedAsync += async e =>
        {
            Console.WriteLine("Connected");
            var topics = new string[] { };
            foreach (var topic in topics)
            {
                var topicFilter = new MqttTopicFilterBuilder().WithTopic(topic).Build();
                await _client.SubscribeAsync(topicFilter);
            }
        };

        _client.DisconnectedAsync += e =>
        {
            Console.WriteLine("Disconnected");
            return Task.CompletedTask;
        };

        _client.ApplicationMessageReceivedAsync += e => Task.CompletedTask;
    }

    public bool PublishMqttMessage(string type, string topic, string command, string msg = "")
    {
        var isTopicKnown = DevicesRegister.Any(x => x.Topic == topic);
        if (!isTopicKnown)
            return false;
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

    public async void ConnectToMqttBroker()
    {
        retry:
        var _ = await _client.ConnectAsync(_options);
        if (!_client.IsConnected)
            goto retry;
    }

    public void DisconnectFromMqttBroker()
    {
        _client.DisconnectAsync();
    }
}