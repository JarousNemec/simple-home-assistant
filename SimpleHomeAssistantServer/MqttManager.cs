using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using MQTTnet;
using MQTTnet.Client;
using System.Configuration;
using SimpleHomeAssistantServer.Enums;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantServer.Workers;

namespace SimpleHomeAssistantServer;

public class MqttManager
{
    private IMqttClient _client;
    private MqttClientOptions _options;
    public List<Device> DevicesRegister;

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
    }

    public string GetAllDiscoveredDevicesJson()
    {
        var Infos = new List<JsonObject>();
        foreach (var device in DevicesRegister)
        {
            Infos.Add(device.AllInfo);
        }

        return JsonSerializer.Serialize(Infos);
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
            var topics = new string[]{};
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
    public void DiscoverAvailableDevices()
    {
        var statisticsWorker = new Thread(new MqttDevicesDiscoveryWorker(DevicesRegister).Run);
        statisticsWorker.Start();
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

    public void ConnectToMqttBroker()
    {
        retry:
        var _ = _client.ConnectAsync(_options).Result;
        if (!_client.IsConnected)
            goto retry;
    }

    public void DisconnectFromMqttBroker()
    {
        _client.DisconnectAsync();
    }
}