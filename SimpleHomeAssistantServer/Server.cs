using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Internal;
using SimpleHttp;

namespace SimpleHomeAssistantServer;

public class Server
{
    private IMqttClient client;
    private MqttClientOptions options;
    private MqttManager _manager;

    public Server()
    {
        _manager = new MqttManager();
        var mqttFactory = new MqttFactory();
        client = mqttFactory.CreateMqttClient();
        options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer("192.168.1.237", 1883) // todo: parametrize
            .WithCredentials("havilland", "havillandHA123*") // todo: parametrize
            .WithCleanSession()
            .Build();
        InitMqttClientMethods();
        InitHttpServer();
    }

    private void InitHttpServer()
    {
        Route.Add("/alldevices",
            (req, res, props) => { res.AsText(_manager.GetAllDiscoveredDevicesJson(), "application/json"); });
        Route.Add("/operation",
            (req, res, props) =>
            {
                // if()//todo add operations
                ActualizeDevices();
                res.AsText("{OperationState: done}", "application/json");
            });
        Route.Add("/switchPowerState",
            (req, res, props) =>
            {
                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = ToggleDeviceState(reader.ReadToEnd()).Result;
                }
                res.AsText("done", "text/html");
                if (result)
                {
                    res.StatusCode = 200;
                }
                else
                {
                    res.StatusCode = 501;
                }
            }, "POST");
    }

    private async Task<bool> ToggleDeviceState(string topic)
    {
        var isTopicKnown = _manager._enableTopics.Contains(topic);
        if (isTopicKnown)
        {
            await PublishMqttMessage("cmnd", topic, "Power", "Toggle");
            return true;
        }

        return false;
    }

    private async void ActualizeDevices()
    {
        _manager._deviceInfos.Clear();
        _manager._devicesRegister.Clear();
        await PublishMqttMessage("cmnd", "tasmotas", "Status0");
    }


    private void InitMqttClientMethods()
    {
        client.ConnectedAsync += async e =>
        {
            Console.WriteLine("Connected");
            var topicFilter = new MqttTopicFilterBuilder().WithTopic("#").Build();
            await client.SubscribeAsync(topicFilter);
        };

        client.DisconnectedAsync += e =>
        {
            Console.WriteLine("Disconnected");
            return Task.CompletedTask;
        };

        client.ApplicationMessageReceivedAsync += e =>
        {
            if (e.ApplicationMessage.Payload == null)
                return Task.CompletedTask;
            string msg = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            MqttMessageReceived(msg);
            return Task.CompletedTask;
        };
    }

    private void MqttMessageReceived(string msg)
    {
        Console.WriteLine($"Message: {msg}");
        try
        {
            var data = JsonNode.Parse(msg) as JsonObject;
            if (data != null) ProcessIncomingJsonObject(data);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
        }
    }

    private void ProcessIncomingJsonObject(JsonObject jsonObject)
    {
        if (jsonObject.ContainsKey("StatusNET"))
        {
            var mac = jsonObject["StatusNET"]?["Mac"]?.ToString();
            var topic = jsonObject["Status"]?["Topic"]?.ToString();
            if (mac != null && !_manager._devicesRegister.Contains(mac))
            {
                _manager._deviceInfos.Add(jsonObject);
                _manager._devicesRegister.Add(mac);
                if (!_manager._enableTopics.Contains(topic))
                    _manager._enableTopics.Add(topic);
            }
        }
    }

    private async Task PublishMqttMessage(string type, string topic, string command, string msg = "")
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic($"{type}/{topic}/{command}")
            .WithPayload(msg)
            .Build();
        if (client.IsConnected)
        {
            await client.PublishAsync(message);
        }
    }


    private bool running = true;

    public async Task Run()
    {
        await client.ConnectAsync(options);

        Console.WriteLine("Running ...");

        await PublishMqttMessage("cmnd", "tasmotas", "Status0");

        Console.WriteLine("Discover sent ...");
        HttpServer.ListenAsync(10002, CancellationToken.None, Route.OnHttpRequestAsync).RunInBackground();

        while (running)
        {
            Thread.Sleep(1);
        }

        await client.DisconnectAsync();
    }
}