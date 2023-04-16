using System.Text.Json.Nodes;
using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantUi.Interfaces;

public interface IDeviceCard
{
    public void LoadInfo(Device data);
}