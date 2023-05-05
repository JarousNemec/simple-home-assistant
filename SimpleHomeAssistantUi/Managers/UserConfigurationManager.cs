using System.Configuration;
using System.Text.Json;

namespace SimpleHomeAssistantUi.Managers;

public static class UserConfigurationManager
{
    private const string UserConfigurationfile = "./UserApp.config";
    private static Dictionary<string, string> _configuration = null!;

    static UserConfigurationManager()
    {
        Load();
    }

    private static void Load()
    {
        if (File.Exists(UserConfigurationfile))
        {
            var data = File.ReadAllText(UserConfigurationfile);
            _configuration =
                JsonSerializer.Deserialize<Dictionary<string, string>>(data) ??
                new Dictionary<string, string>();
        }
        else
        {
            _configuration = new Dictionary<string, string>();
        }
        CheckForEditableProperties();
    }

    private static void CheckForEditableProperties()
    {
        bool needSave = false;
        var props = ConfigurationManager.AppSettings.Get("Editable")?.Split(";");
        if (props == null) return;
        foreach (var prop in props)
        {
            if (_configuration.ContainsKey(prop)) continue;
            var value = ConfigurationManager.AppSettings.Get(prop);
            if (value == null) return;
            _configuration.Add(prop, value);
            needSave = true;
        }

        if (needSave)
            Save();
    }

    public static string? Get(string name)
    {
        var result = _configuration.ContainsKey(name) ? _configuration[name] : null;
        if (result == null)
            result = ConfigurationManager.AppSettings.Get(name);
        return result;
    }

    public static Dictionary<string, string> GetAllProps()
    {
        return _configuration;
    }

    public static void Set(string name, string value)
    {
        if (_configuration.ContainsKey(name))
            _configuration[name] = value;
        else
            _configuration.Add(name, value);
        Save();
    }

    private static void Save()
    {
        var data = JsonSerializer.Serialize(_configuration);
        File.WriteAllText(UserConfigurationfile, data);
    }
}