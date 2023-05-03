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

public class MqttDevicesDiscoveryWorker : MqttWorker
{
    private readonly List<Device> _devicesStorage;
    private List<string> _unknownTopics;
    private List<string> _knownTopics;
    private DiscoveryStatus _discovering;
    private string[] _supportedModules;

    public MqttDevicesDiscoveryWorker(List<Device> devicesStorage, DiscoveryStatus discovering) : base(new[] { "#" },
        ttl: 15000)
    {
        _discovering = discovering;
        _discovering.State = true;
        _unknownTopics = new List<string>();
        _knownTopics = new List<string>();
        _devicesStorage = devicesStorage;
        _supportedModules = ConfigurationManager.AppSettings.Get("SupportedModules").Split(';');
    }

    protected override void SetupRun()
    {
        PublishMqttMessage("cmnd", "tasmotas", "OtaUrl");
        Console.WriteLine("Discover sent ...");
    }

    protected override void Stop()
    {
        if (_knownTopics.Count < _devicesStorage.Count)
        {
            for (var i = _devicesStorage.Count - 1; i > 0; i--)
            {
                if (_knownTopics.All(x => x != _devicesStorage[i].Topic))
                {
                    _devicesStorage.RemoveAt(i);
                }
            }
        }


        if (_unknownTopics.Count > 0)
        {
            _workersLiveTimer.Stop();
            _workersLiveTimer.Start();
            foreach (var unknownTopic in _unknownTopics)
            {
                _knownTopics.Add(unknownTopic);
            }

            PublishMqttMessage("cmnd", "tasmotas", "Status0");
            _unknownTopics.Clear();
            return;
        }

        _knownTopics.Clear();
        _unknownTopics.Clear();
        _discovering.State = false;
        base.Stop();
    }

    private bool IsDiscoverMsgResponse(JsonObject jsonObject)
    {
        return jsonObject.ContainsKey("OtaUrl") && jsonObject.Count == 1;
    }

    private bool IsDiscoverGetInfoMsgResponse(JsonObject jsonObject)
    {
        return jsonObject.ContainsKey("Status") && jsonObject.ContainsKey("StatusPRM") &&
               jsonObject.ContainsKey("StatusFWR") && jsonObject.ContainsKey("StatusLOG") &&
               jsonObject.ContainsKey("StatusNET");
    }

    private bool IsTimersGetInfoMsgResponse(JsonObject jsonObject)
    {
        return jsonObject.ContainsKey("Timers") && jsonObject.ContainsKey("Timer1") &&
               jsonObject.ContainsKey("Timer2");
    }

    private bool IsSupportedModule(JsonObject jsonObject)
    {
        return _supportedModules.Contains(jsonObject["Status"]["Module"].ToString());
    }

    protected override void ProcessMessage(MqttApplicationMessage msg, JsonObject jsonObject)
    {
        if (IsDiscoverMsgResponse(jsonObject))
        {
            var topic = msg.Topic.Split('/')[1];
            if (_devicesStorage.All(x => x.Topic != topic))
            {
                _unknownTopics.Add(topic);
            }
            else
            {
                _knownTopics.Add(topic);
            }
        }
        else if (IsDiscoverGetInfoMsgResponse(jsonObject))
        {
            if (!IsSupportedModule(jsonObject)) return;
            var mac = jsonObject["StatusNET"]?["Mac"]?.ToString();
            var topic = jsonObject["Status"]?["Topic"]?.ToString();
            if (mac != null && _devicesStorage.All(x => x.Mac != mac) && topic != null)
            {
                var actualTime = Math.Round(DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalSeconds, 0);
                PublishMqttMessage("cmnd", topic, "Time", actualTime.ToString());
                PublishMqttMessage("cmnd", topic, "Timezone", "0");
                PublishMqttMessage("cmnd", topic, "Timers");
                _devicesStorage.Add(new Device(jsonObject));
            }
        }
        else if (IsTimersGetInfoMsgResponse(jsonObject))
        {
            var topic = msg.Topic.Split('/')[1];
            var device = _devicesStorage.FirstOrDefault(x => x.Topic == topic);
            if (device != null) device.Timers = jsonObject;
        }
    }
}