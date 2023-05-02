namespace SimpleHomeAssistantServer.Models;

public class DeviceProfile
{
    public string Mac { get; set; }
    public Dictionary<DateTime, int> PowerConsummationChangeDateWithSetting { get; set; }

    public DeviceProfile(string mac, Dictionary<DateTime, int> powerConsummationChangeDateWithSettings)
    {
        Mac = mac;
        PowerConsummationChangeDateWithSetting = powerConsummationChangeDateWithSettings;
    }
    
    public DeviceProfile(string mac)
    {
        Mac = mac;
        PowerConsummationChangeDateWithSetting = new Dictionary<DateTime, int>();
    }
    public DeviceProfile()
    {
        
    }
}