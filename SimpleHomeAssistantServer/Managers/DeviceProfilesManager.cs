using System.Collections.Specialized;
using System.Configuration;
using System.Text.Json;
using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantServer;

public class DeviceProfilesManager
{
    private List<DeviceProfile> _profiles;
    private readonly NameValueCollection? _config;

    public DeviceProfilesManager()
    {
        _config = ConfigurationManager.AppSettings;
        Load();
    }

    public void Save()
    {
        if (_profiles == null) return;
        var dataToSave = JsonSerializer.Serialize(_profiles);
        var path = _config.Get("ProfilesPath");
        File.WriteAllText(path, dataToSave);
    }

    private void Load()
    {
        var path = _config.Get("ProfilesPath");
        if (File.Exists(path))
        {
            var data = File.ReadAllText(path);
            _profiles =
                JsonSerializer.Deserialize<List<DeviceProfile>>(data) ??
                new List<DeviceProfile>();
        }
        else
        {
            _profiles = new List<DeviceProfile>();
        }
    }

    public List<DeviceProfile> GetProfiles()
    {
        return _profiles;
    }

    public void CheckForNewProfiles(List<Device> actualDevices)
    {
        var change = false;
        foreach (var device in actualDevices)
        {
            if (_profiles.All(x => x.Mac != device.Mac))
            {
                change = true;
                _profiles.Add(new DeviceProfile(device.Mac));
            }
        }

        if (change) Save();
    }

    public bool EditProfile(string newProfile, List<Device> actualDevices)
    {
        var profile = JsonSerializer.Deserialize<DeviceProfile>(newProfile);
        if (profile == null) return false;
        for (var i = 0; i < _profiles.Count; i++)
        {
            if (_profiles[i].Mac != profile.Mac) continue;
            _profiles[i] = profile;

            var device = actualDevices.FirstOrDefault(x => x.Mac == profile.Mac);
            if (device == null) continue;
            device.Profile = profile;

            Save();
            break;
        }

        return true;
    }
}