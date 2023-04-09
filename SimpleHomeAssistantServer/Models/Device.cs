using System.Text.Json.Nodes;

namespace SimpleHomeAssistantServer.Models;

public class Device
{
    public JsonObject AllInfo { get; set; }
    public string Mac { get; set; }
    public string Ip { get; set; }
    public string FriendlyName { get; set; }
    public string DeviceName { get; set; }
    public string Topic { get; set; }

    public Device(JsonObject allInfo)
    {
        AllInfo = allInfo;
        Ip = allInfo["StatusNET"]?["IPAddress"]?.ToString()!;
        FriendlyName = allInfo["Status"]?[nameof(FriendlyName)]?[0]?.ToString()!;
        DeviceName = allInfo["Status"]?[nameof(DeviceName)]?.ToString()!;
        Mac = allInfo["StatusNET"]?[nameof(Mac)]?.ToString()!;
        Topic = allInfo["Status"]?[nameof(Topic)]?.ToString()!;
    }
    
}