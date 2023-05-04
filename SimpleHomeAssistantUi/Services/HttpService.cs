using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantUi.Services;

public class HttpService
{
    private HttpClient _client;
    private AuthCredentials _actualLoggedAccount;

    public HttpService()
    {
        _client = new HttpClient();
        _actualLoggedAccount = new AuthCredentials("---", "---");
        _client.DefaultRequestHeaders.Add("User", _actualLoggedAccount.User);
        _client.DefaultRequestHeaders.Add("Password", _actualLoggedAccount.Password);
    }

    public void SetCredentials(AuthCredentials credentials)
    {
        _actualLoggedAccount = credentials;
        _client.DefaultRequestHeaders.Remove("User");
        _client.DefaultRequestHeaders.Remove("Password");
        _client.DefaultRequestHeaders.Add("User", _actualLoggedAccount.User);
        _client.DefaultRequestHeaders.Add("Password", _actualLoggedAccount.Password);
    }

    public AuthCredentials GetCredentials()
    {
        return _actualLoggedAccount;
    }

    public string DownloadString(string url)
    {
        try
        {
            var res = _client.GetStringAsync(url).Result;
            return res;
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
            return String.Empty;
        }
    }

    private const string UNAUTHORIZED_MSG = "Cannot communicate because of bad loging credentials";

    public T? DownloadJsonObject<T>(string url)where T : class
    {
        try
        {
            var response = _client.GetAsync(url).Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                MessageBox.Show(UNAUTHORIZED_MSG);
            return JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        return null;

    }

    public bool SendMessage(string url, string msg="")
    {
        var data = new StringContent(msg, Encoding.UTF8, "application/json");

        try
        {
            var response = _client.PostAsync(url, data).Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                MessageBox.Show(UNAUTHORIZED_MSG);
            if (response.IsSuccessStatusCode) return true;
            
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        return false;
    }

    public string SendMessageAndReturnResponseContent(string url, string msg)
    {
        var data = new StringContent(msg, Encoding.UTF8, "application/json");
        var res = string.Empty;
        try
        {
            var response = _client.PostAsync(url, data).Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                MessageBox.Show(UNAUTHORIZED_MSG);
            res = response.Content.ReadAsStringAsync().Result;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        return res;
    }
}