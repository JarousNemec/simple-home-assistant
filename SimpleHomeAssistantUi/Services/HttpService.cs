using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Managers;

namespace SimpleHomeAssistantUi.Services;

public class HttpService
{

    

    public string DownloadString(string url, HttpClient client)
    {
        try
        {
            var res = client.GetStringAsync(url).Result;
            return res;
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);
            return String.Empty;
        }
    }

    private const string UNAUTHORIZED_MSG = "Cannot communicate because of bad loging credentials";

    public async Task<T?> DownloadJsonObject<T>(string url,HttpClient client) where T : class
    {
        Debug.WriteLine("Request to:" + url);
        try
        {
            var response = await client.GetAsync(url);
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

    public async Task<bool> SendMessage(string url, HttpClient client, string msg = "")
    {
        var data = new StringContent(msg, Encoding.UTF8, "application/json");
        Debug.WriteLine("Request to:" + url);
        try
        {
            var response = await client.PostAsync(url, data);
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

    public async Task<string> SendMessageAndReturnResponseContent(string url, HttpClient client, string msg)
    {
        var data = new StringContent(msg, Encoding.UTF8, "application/json");
        var res = string.Empty;
        try
        {
            var response = await client.PostAsync(url, data);
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

    public string GetMainEndpoint()
    {
        return
            $"{UserConfigurationManager.Get("MainEndpointProtocol")}{UserConfigurationManager.Get("MainEndpointHost")}{UserConfigurationManager.Get("MainEndpointPort")}";
    }
}