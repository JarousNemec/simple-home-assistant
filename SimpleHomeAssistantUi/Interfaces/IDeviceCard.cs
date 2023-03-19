using System.Text.Json.Nodes;

namespace SimpleHomeAssistantUi.Interfaces;

public interface IDeviceCard
{
    public void LoadInfo(JsonNode data);
}