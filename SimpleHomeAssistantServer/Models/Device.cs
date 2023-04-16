using System.Text.Json.Nodes;

namespace SimpleHomeAssistantServer.Models;

public class Device
{
    public string Mac { get; set; }
    public string Ip { get; set; }
    public string FriendlyName { get; set; }
    public string DeviceName { get; set; }
    public string Topic { get; set; }
    public int Module { get; set; }
    public bool Power { get; set; }
    

    public Device(JsonObject allInfo)
    {
        Ip = allInfo["StatusNET"]?["IPAddress"].ToString()!;
        FriendlyName = allInfo["Status"]?[nameof(FriendlyName)]?[0].ToString()!;
        DeviceName = allInfo["Status"]?[nameof(DeviceName)].ToString()!;
        Mac = allInfo["StatusNET"]?[nameof(Mac)].ToString()!;
        Topic = allInfo["Status"]?[nameof(Topic)].ToString()!;
        Module = int.Parse(allInfo["Status"]?[nameof(Module)].ToJsonString());
        Power = allInfo["StatusSTS"]?["POWER"]?.ToString()! == "ON";
    }
    
    public Device(string ip, string friendlyName, string deviceName, string mac, string topic, int module, bool power)
    {
        Ip = ip;
        FriendlyName = friendlyName;
        DeviceName = deviceName;
        Mac = mac;
        Topic = topic;
        Module = module;
        Power = power;
    }
    
    public Device()
    {
        Ip = string.Empty;
        FriendlyName = string.Empty;
        DeviceName = string.Empty;
        Mac = string.Empty;
        Topic = string.Empty;
        Module = 0;
        Power = false;
    }
    
}