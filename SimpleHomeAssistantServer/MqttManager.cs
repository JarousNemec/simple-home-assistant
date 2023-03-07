using System.Text.Json;
using System.Text.Json.Nodes;

namespace SimpleHomeAssistantServer;

public class MqttManager
{
    public List<JsonObject> _deviceInfos;
    public List<string> _devicesRegister;

    public MqttManager()
    {
        _deviceInfos = new List<JsonObject>();
        _devicesRegister = new List<string>();
    }

    public string GetAllDiscoveredDevicesJson()
    {
        return JsonSerializer.Serialize(_deviceInfos);
    }
}