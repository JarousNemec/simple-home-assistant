using System.Diagnostics;
using System.Net;
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

    public HttpService()
    {
        _client = new HttpClient();
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

    public T? DownloadJsonObject<T>(string url)
    {
        try
        {
            var data = _client.GetStringAsync(url).Result;
            return JsonSerializer.Deserialize<T>(data);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
            return default;
        }
    }

    public bool SendStringMessage(string url, string msg)
    {
        var data = new StringContent(msg, Encoding.UTF8, "application/json");

        using var client = new HttpClient();

        var response = client.PostAsync(url,data).Result;
        if (response.IsSuccessStatusCode) return true;
        return false;
    }

    public bool SendPostRequest(string url)
    {
        var data = new StringContent("refresh", Encoding.UTF8, "text/html");

        using var client = new HttpClient();

        var response = client.PostAsync(url,data).Result;
        if (response.IsSuccessStatusCode) return true;
        return false;
    }
}