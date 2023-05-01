namespace SimpleHomeAssistantServer.Models;

public class DeviceProfile
{
    public string Topic { get; set; }
    public Dictionary<DateTime, int> PowerConsummationChangeDateWithSetting { get; set; }

    public DeviceProfile(string topic, Dictionary<DateTime, int> powerConsummationChangeDateWithSettings)
    {
        Topic = topic;
        PowerConsummationChangeDateWithSetting = powerConsummationChangeDateWithSettings;
    }
    
    public DeviceProfile(string topic)
    {
        Topic = topic;
        PowerConsummationChangeDateWithSetting = new Dictionary<DateTime, int>();
    }
    public DeviceProfile()
    {
        
    }
}