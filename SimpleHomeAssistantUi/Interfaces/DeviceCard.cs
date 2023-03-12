using System.Text.Json.Nodes;

namespace SimpleHomeAssistantUi.Interfaces;

public interface DeviceCard
{
    public void Load(JsonObject data);
}