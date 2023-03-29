using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace SimpleHomeAssistantUi.Services;

public class HttpService
{
    private HttpClient _client;

    public HttpService()
    {
        _client = new HttpClient();
    }

    public JsonNode DownloadJsonObject(string url)
    {
        try
        {
            var res = _client.GetStringAsync(url).Result;
            var data = JsonNode.Parse(res);
            return data ?? new JsonObject();
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
            return new JsonObject();
        }
    }

    public bool SendSwitchPowerStateCommand(string url, string topic)
    {
        var data = new StringContent(topic, Encoding.UTF8, "application/json");

        using var client = new HttpClient();

        var response = client.PostAsync(url,data).Result;
        if (response.IsSuccessStatusCode) return true;
        return false;
    }
}