using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
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
        // var request = new HttpRequestMessage(HttpMethod.Get, url);

        try
        {
            
            // var response = _client.SendAsync(request, HttpCompletionOption.ResponseContentRead).Result;
            // if (!response.IsSuccessStatusCode)
            // {
            //     return new JsonObject();
            // }
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
}