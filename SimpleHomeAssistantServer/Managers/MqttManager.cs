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
    private DeviceProfilesManager _profilesManager;

    public MqttManager(DeviceProfilesManager profilesManager)
    {
        _profilesManager = profilesManager;
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
        _discoveryTimer.Interval = 600000;
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
        var statisticsWorker =
            new Thread(new MqttDevicesDiscoveryWorker(DevicesRegister, _discoveryStatus, _profilesManager).Run);
        statisticsWorker.Start();
    }

    public string GetAllDiscoveredDevices()
    {
        var records = _statisticsManager.GetTodayLastRecords();
        if (records != null)
        {
            foreach (var device in DevicesRegister)
            {
                if (records.ContainsKey(device.Topic))
                    device.Power = records[device.Topic].State;
            }
        }

        return JsonSerializer.Serialize(DevicesRegister.ToArray());
    }

    public bool ToggleDeviceState(string topic)
    {
        var res = PublishMqttMessage("cmnd", topic, "Power", "Toggle");
        if (!res) return res;
        var device = DevicesRegister.FirstOrDefault(x => x.Topic == topic);
        if (device != null) device.Power = !device.Power;
        return res;
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
        var res = SendBasicMessage(json, "Topic", true);
        if (res)
        {
            var device = DevicesRegister.FirstOrDefault(x => x.Topic == data.Topic);
            if (device == null) return !isTopicKnown && res;
            device.Topic = data.Payload;
        }

        return !isTopicKnown && res;
    }

    public bool SetDeviceName(string json)
    {
        var res = SendBasicMessage(json, "DeviceName", true);
        if (res)
        {
            var data = JsonSerializer.Deserialize<BasicMessage>(json);
            if (data == null) return res;
            var device = DevicesRegister.FirstOrDefault(x => x.Topic == data.Topic);
            if (device == null) return res;
            device.DeviceName = data.Payload;
        }

        return res;
    }

    public bool SetFriendlyName(string json)
    {
        var res = SendBasicMessage(json, "FriendlyName1", true);
        if (res)
        {
            var data = JsonSerializer.Deserialize<BasicMessage>(json);
            if (data == null) return res;
            var device = DevicesRegister.FirstOrDefault(x => x.Topic == data.Topic);
            if (device == null) return res;
            device.FriendlyName = data.Payload;
        }

        return res;
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

    public void ConnectToMqttBroker()
    {
        retry:
        try
        {
            var _ = _client.ConnectAsync(_options).Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Thread.Sleep(500);
            goto retry;
        }

        if (!_client.IsConnected)
            goto retry;
    }

    public void DisconnectFromMqttBroker()
    {
        _client.DisconnectAsync();
    }
}