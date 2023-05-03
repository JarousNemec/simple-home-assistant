using System.Configuration;
using System.Text.Json;
using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantServer;

public class AuthenticationManager
{
    private List<AuthCredentials> _registeredCredentials;

    public AuthenticationManager()
    {
        Load();
    }

    private void Load()
    {
        var config = ConfigurationManager.AppSettings;
        var filename = config.Get("AccountsPath");
        if (File.Exists(filename))
        {
            var data = File.ReadAllText(filename);
            _registeredCredentials =
                JsonSerializer.Deserialize<List<AuthCredentials>>(data) ??
                new List<AuthCredentials>();
        }
        else
        {
            _registeredCredentials = new List<AuthCredentials>();
        }

        if (_registeredCredentials.Count == 0)
            AddAccount(new AuthCredentials("admin", "pass"));
    }

    private void Save()
    {
        var data = JsonSerializer.Serialize(_registeredCredentials);
        var filename = ConfigurationManager.AppSettings.Get("AccountsPath");
        File.WriteAllText(filename, data);
    }

    public bool Authorize(AuthCredentials credentials)
    {
        return _registeredCredentials.Any(x => x.User == credentials.User && x.Password == credentials.Password);
    }

    public bool AddAccountFromJson(string json)
    {
        AuthCredentials credentials = JsonSerializer.Deserialize<AuthCredentials>(json);
        if (credentials == null) return false;
        return AddAccount(credentials);
    }
    public bool AddAccount(AuthCredentials credentials)
    {
        if (_registeredCredentials.Any(x => x.User == credentials.User)) return false;
        _registeredCredentials.Add(credentials);
        Save();
        return true;
    }

    public bool DeleteAccount(AuthCredentials credentials)
    {
        if (_registeredCredentials.Count < 2) return false;
        var account =
            _registeredCredentials.FirstOrDefault(x =>
                x.User == credentials.User && x.Password == credentials.Password);
        if (account == null) return false;

        _registeredCredentials.Remove(account);
        Save();
        return true;
    }
}