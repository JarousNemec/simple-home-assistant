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

    public MqttManager()
    {
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
        var statisticsWorker = new Thread(new MqttDevicesDiscoveryWorker(DevicesRegister).Run);
        statisticsWorker.Start();
    }

    public string GetAllDiscoveredDevices()
    {
        var records = _statisticsManager.GetLastDevicePowerStateRecords();
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
        var isTopicKnown = DevicesRegister.Any(x => x.Topic == topic);
        if (isTopicKnown)
        {
            var res = PublishMqttMessage("cmnd", topic, "Power", "Toggle");
            return res;
        }

        return false;
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