using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using MQTTnet;
using MQTTnet.Client;
using System.Configuration;
using SimpleHomeAssistantServer.Enums;
using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantServer;

public class MqttManager
{
    private IMqttClient _client;
    private MqttClientOptions _options;
    public List<Device> DevicesRegister;
    private MqttClientStates _clientState = MqttClientStates.Free;

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
            _clientState = MqttClientStates.IndividualCommunication;
            var res = PublishMqttMessage("cmnd", topic, "Power", "Toggle");
            return res;
        }

        return false;
    }
    private void ActualizeDevices()
    {
        DevicesRegister.Clear();
        DiscoverAvailableDevices();
    }


    private void InitMqttClientMethods()
    {
        _client.ConnectedAsync += async e =>
        {
            Console.WriteLine("Connected");
            var topics = new[] { "$SYS/broker/clients/#", "#" };
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

        _client.ApplicationMessageReceivedAsync += e =>
        {
            ValidateMqttMessageReceived(e.ApplicationMessage);
            return Task.CompletedTask;
        };
    }

    private void ValidateMqttMessageReceived(MqttApplicationMessage msg)
    {
        if (msg.Payload == null)
            return;
        string payload = Encoding.UTF8.GetString(msg.Payload);
        Console.WriteLine($"Topic: {msg.Topic} , Message: {payload}");
        try
        {
            var data = JsonNode.Parse(payload) as JsonObject;
            if (data != null) ProcessIncomingMessage(msg, data);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
        }
    }

    private void ProcessIncomingMessage(MqttApplicationMessage msg, JsonObject jsonObject)
    {
        switch (_clientState)
        {
            case MqttClientStates.Discover:
                ProcessDiscoverMessage(msg, jsonObject);
                break;
            case MqttClientStates.IndividualCommunication:
                ProcessIndividualCommunicationMessage(msg, jsonObject);
                break;
            case MqttClientStates.Free:
                break;
            default:
                break;
        }
    }

    private void ProcessDiscoverMessage(MqttApplicationMessage msg, JsonObject jsonObject)
    {
        if (IsValidDiscoverMsgResponse(jsonObject))
        {
            var mac = jsonObject["StatusNET"]?["Mac"]?.ToString();
            var topic = jsonObject["Status"]?["Topic"]?.ToString();
            if (mac != null && DevicesRegister.All(x => x.Mac != mac) && topic != null)
            {
                DevicesRegister.Add(new Device(jsonObject));
            }

            _clientState = MqttClientStates.Free;
        }
    }

    private void ProcessIndividualCommunicationMessage(MqttApplicationMessage msg, JsonObject jsonObject)
    {
        _clientState = MqttClientStates.Free;
        ActualizeDevices();
    }

    private bool IsValidDiscoverMsgResponse(JsonObject jsonObject)
    {
        return jsonObject.ContainsKey("Status") && jsonObject.ContainsKey("StatusPRM") &&
               jsonObject.ContainsKey("StatusFWR") && jsonObject.ContainsKey("StatusLOG") &&
               jsonObject.ContainsKey("StatusNET");
    }

    public void DiscoverAvailableDevices()
    {
        _clientState = MqttClientStates.Discover;
        PublishMqttMessage("cmnd", "tasmotas", "Status0");
        Console.WriteLine("Discover sent ...");
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