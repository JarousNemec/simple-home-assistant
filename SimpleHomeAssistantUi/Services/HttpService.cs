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

    public JsonObject DownloadJsonObject(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        try
        {
            var response = _client.Send(request, HttpCompletionOption.ResponseContentRead);
            if (!response.IsSuccessStatusCode)
            {
                return new JsonObject();
            }

            var data = JsonNode.Parse(response.Content.ReadAsStream()) as JsonObject;
            return data ?? new JsonObject();
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
            return new JsonObject();
        }
    }
}