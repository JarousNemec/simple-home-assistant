using System.Configuration;
using System.Text.Json;
using MQTTnet.Internal;
using SimpleHttp;

namespace SimpleHomeAssistantServer;

public class Server
{
    private MqttManager _mqttManager;
    private StatisticsManager _statisticsManager;
    private DeviceProfilesManager _deviceProfilesManager;

    public Server()
    {
        _mqttManager = new MqttManager();
        _statisticsManager = new StatisticsManager(_mqttManager.DevicesRegister);
        _deviceProfilesManager = new DeviceProfilesManager();
        _mqttManager.SetStatisticManager(_statisticsManager);
        CheckSystemRequiredDirectories();
        InitHttpServer();
    }

    private void CheckSystemRequiredDirectories()
    {
        var required = ConfigurationManager.AppSettings.Get("RequiredDirectories")?.Split(';');
        var availableDirs = Directory.GetDirectories("./");
        if (required != null)
            foreach (var dir in required)
            {
                if (availableDirs.All(x => x != dir))
                {
                    Directory.CreateDirectory($"./{dir}");
                }
            }
    }

    private void InitHttpServer()
    {
        //todo: get info about devices timers
        //todo: add simple authentication
        Route.Add("/allDevices",
            (req, res, props) => { res.AsText(_mqttManager.GetAllDiscoveredDevices(), "application/json"); });
        Route.Add("/refresh",
            (req, res, props) =>
            {
                _mqttManager.DiscoverAvailableDevices();
                res.AsText("discovering", "application/json");
            }, "POST");
        
        Route.Add("/switchPowerState",
            (req, res, props) =>
            {
                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.ToggleDeviceState(reader.ReadToEnd());
                }

                res.AsText("done");
                res.StatusCode = result ? 200 : 501;
            }, "POST");
        
        Route.Add("/setFriendlyName",
            (req, res, props) =>
            {
                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.SetFriendlyName(reader.ReadToEnd());
                }

                res.AsText("done");
                res.StatusCode = result ? 200 : 501;
            }, "POST");
        Route.Add("/setDeviceName",
            (req, res, props) =>
            {
                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.SetDeviceName(reader.ReadToEnd());
                }

                res.AsText("done");
                res.StatusCode = result ? 200 : 501;
            }, "POST");
        
        Route.Add("/setDeviceTopic",
            (req, res, props) =>
            {
                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.SetDeviceTopic(reader.ReadToEnd());
                }

                res.AsText("done");
                res.StatusCode = result ? 200 : 501;
            }, "POST");
        
        Route.Add("/setTimer",
            (req, res, props) =>
            {
                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.SetTimer(reader.ReadToEnd());
                }

                res.AsText("done");
                res.StatusCode = result ? 200 : 501;
            }, "POST");

        Route.Add("/deviceProfiles",
            (req, res, props) =>
            {
                _deviceProfilesManager.CheckForNewProfiles(_mqttManager.DevicesRegister);
                res.AsText(JsonSerializer.Serialize(_deviceProfilesManager.GetProfiles()),
                    "application/json");
            });
        Route.Add("/editProfile",
            (req, res, props) =>
            {
                var result = true;
                using (var reader = new StreamReader(req.InputStream))
                {
                    result = _deviceProfilesManager.EditProfile(reader.ReadToEnd());
                }

                res.AsText("done");
                res.StatusCode = result ? 200 : 501;
            }, "POST");
        
        Route.Add("/specificTodayStatistic",
            (req, res, props) =>
            {
                string result = string.Empty;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _statisticsManager.GetSpecificTodayStatistic(reader.ReadToEnd());
                }

                res.AsText(result, "application/json");
            }, "POST");
        Route.Add("/statisticToday",
            (req, res, props) =>
            {
                res.AsText(JsonSerializer.Serialize(_statisticsManager.GetTodayRecords()),
                    "application/json");
            });
        Route.Add("/specificStatistic",
            (req, res, props) =>
            {
                string result = string.Empty;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _statisticsManager.GetSpecificStatistic(reader.ReadToEnd());
                }

                res.AsText(result, "application/json");
            }, "POST");
    }

    public void Run()
    {
        _mqttManager.ConnectToMqttBroker();
        Console.WriteLine("Running ...");
        _mqttManager.DiscoverAvailableDevices();
        Thread.Sleep(20000);
        _statisticsManager.Run();

        HttpServer.ListenAsync(10002, CancellationToken.None, Route.OnHttpRequestAsync).RunInBackground();

        while (true)
        {
            Thread.Sleep(1);
        }

        _mqttManager.DisconnectFromMqttBroker();
        _statisticsManager.Save();
    }
}