using System.Configuration;
using System.Net;
using System.Text.Json;
using MQTTnet.Internal;
using SimpleHomeAssistantServer.Models;
using SimpleHttp;

namespace SimpleHomeAssistantServer;

public class Server
{
    private MqttManager _mqttManager;
    private StatisticsManager _statisticsManager;
    private DeviceProfilesManager _deviceProfilesManager;
    private AuthenticationManager _authenticationManager;

    public Server()
    {
        CheckSystemRequiredDirectories();
        _mqttManager = new MqttManager();
        _statisticsManager = new StatisticsManager(_mqttManager.DevicesRegister);
        _deviceProfilesManager = new DeviceProfilesManager();
        _authenticationManager = new AuthenticationManager();
        _mqttManager.SetStatisticManager(_statisticsManager);
        InitHttpServerEndpoints();
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

    private void InitHttpServerEndpoints()
    {
        Route.Add("/addAccount",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                bool result;
                using (var reader = new StreamReader(req.InputStream))
                {
                    result = _authenticationManager.AddAccountFromJson(reader.ReadToEnd());
                }

                res.StatusCode = result ? 200 : 400;
                res.AsText(result ? "done" : "cannot add");
            }, "POST");

        Route.Add("/deleteAccount",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                var credentials = new AuthCredentials(req.Headers.Get("User") ?? "",
                    req.Headers.Get("Password") ?? "");
                var result = _authenticationManager.DeleteAccount(credentials);

                res.StatusCode = result ? 200 : 400;
                res.AsText(result ? "done" : "cannot delete");
            });

        Route.Add("/allDevices",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                res.AsText(_mqttManager.GetAllDiscoveredDevices(), "application/json");
            });
        Route.Add("/refresh",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                _mqttManager.DiscoverAvailableDevices();
                res.AsText("discovering", "application/json");
            });

        Route.Add("/switchPowerState",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.ToggleDeviceState(reader.ReadToEnd());
                }

                res.StatusCode = result ? 200 : 501;
                res.AsText("done");
            }, "POST");

        Route.Add("/setFriendlyName",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.SetFriendlyName(reader.ReadToEnd());
                }

                res.StatusCode = result ? 200 : 501;
                res.AsText("done");
            }, "POST");
        Route.Add("/setDeviceName",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.SetDeviceName(reader.ReadToEnd());
                }

                res.StatusCode = result ? 200 : 501;
                res.AsText("done");
            }, "POST");

        Route.Add("/setDeviceTopic",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.SetDeviceTopic(reader.ReadToEnd());
                }

                res.StatusCode = result ? 200 : 501;
                res.AsText("done");
            }, "POST");

        Route.Add("/setTimer",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                bool result;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _mqttManager.SetTimer(reader.ReadToEnd());
                }

                res.StatusCode = result ? 200 : 501;
                res.AsText("done");
            }, "POST");

        Route.Add("/deviceProfiles",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                _deviceProfilesManager.CheckForNewProfiles(_mqttManager.DevicesRegister);
                res.AsText(JsonSerializer.Serialize(_deviceProfilesManager.GetProfiles()),
                    "application/json");
            });
        Route.Add("/editProfile",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                var result = true;
                using (var reader = new StreamReader(req.InputStream))
                {
                    result = _deviceProfilesManager.EditProfile(reader.ReadToEnd());
                }

                res.StatusCode = result ? 200 : 501;
                res.AsText("done");
            }, "POST");

        Route.Add("/lastSpecificTodayStatistic",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                string result = string.Empty;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _statisticsManager.GetLastSpecificTodayStatistic(reader.ReadToEnd());
                }

                res.AsText(result, "application/json");
            }, "POST");
        Route.Add("/statisticToday",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                res.AsText(JsonSerializer.Serialize(_statisticsManager.GetTodayRecords()),
                    "application/json");
            });
        Route.Add("/specificStatistic",
            (req, res, props) =>
            {
                if (!Authorized(req, res)) return;

                string result = string.Empty;
                using (StreamReader reader = new StreamReader(req.InputStream))
                {
                    result = _statisticsManager.GetSpecificStatistic(reader.ReadToEnd());
                }

                res.AsText(result, "application/json");
            }, "POST");
    }

    private bool Authorized(HttpListenerRequest req, HttpListenerResponse res)
    {
        if (!_authenticationManager.Authorize(new AuthCredentials(req.Headers.Get("User") ?? "",
                req.Headers.Get("Password") ?? "")))
        {
            res.StatusCode = 401;
            res.AsText("unauthorized");
            return false;
        }

        return true;
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