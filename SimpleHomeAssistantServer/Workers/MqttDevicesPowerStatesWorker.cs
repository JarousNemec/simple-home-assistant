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

public class MqttDevicesPowerStatesWorker : MqttWorker
{
    private string[] Topics { get; set; }
    private readonly Dictionary<string, bool> _statuses;
    private readonly Dictionary<string, List<DevicePowerStateRecord>> _sharedStorage;

    public MqttDevicesPowerStatesWorker(string[] topics,string[] topicsPaths, Dictionary<string, List<DevicePowerStateRecord>> sharedStorage)
        : base(topicsPaths)
    {
        Topics = topics;
        _sharedStorage = sharedStorage;
        _statuses = new Dictionary<string, bool>();
    }

    protected override void SetupRun()
    {
        foreach (var topic in Topics)
        {
            PublishMqttMessage("cmnd", topic, "Power");
        }
    }

    protected override void ProcessMessage(MqttApplicationMessage msg, JsonObject jsonObject)
    {
        if (!jsonObject.ContainsKey("POWER")) return;
        var topic = msg.Topic.Split('/')[1];
        if (_statuses.ContainsKey(topic)) return;
        var value = jsonObject["POWER"].GetValue<string>() == "ON";
        _statuses.Add(topic,value);
    }

    protected override void Stop()
    {
        InsertDataToStorage();
        base.Stop();
    }

    private void InsertDataToStorage()
    {
        foreach (var device in _statuses)
        {
            if (_sharedStorage.ContainsKey(device.Key))
            {
                _sharedStorage[device.Key].Add(new DevicePowerStateRecord()
                    { Date = DateTime.Now, State = device.Value });
            }
            else
            {
                _sharedStorage.Add(device.Key, new List<DevicePowerStateRecord>());
                _sharedStorage[device.Key].Add(new DevicePowerStateRecord()
                    { Date = DateTime.Now, State = device.Value });
            }
        }
    }
}