using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Nodes;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Internal;
using SimpleHttp;

namespace SimpleHomeAssistantServer;

public class Server
{
    private MqttManager _mqttManager;
    private StatisticsManager _statisticsManager;

        public Server()
    {
        _mqttManager = new MqttManager();
        _statisticsManager = new StatisticsManager(_mqttManager);
        InitHttpServer();
    }

    private void InitHttpServer()
    {
        Route.Add("/alldevices",
            (req, res, props) => { res.AsText(_mqttManager.GetAllDiscoveredDevicesJson(), "application/json"); });
        Route.Add("/switchPowerState",
            (req, res, props) =>
            {
                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.ToggleDeviceState(reader.ReadToEnd());
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

    public void Run()
    {
        _mqttManager.ConnectToMqttBroker();
        Console.WriteLine("Running ...");
        _mqttManager.DiscoverAvailableDevices();
        
        HttpServer.ListenAsync(10002, CancellationToken.None, Route.OnHttpRequestAsync).RunInBackground();

        while (true)
        {
            Thread.Sleep(1);
        }

        _mqttManager.DisconnectFromMqttBroker();
        _statisticsManager.Save();
    }
}