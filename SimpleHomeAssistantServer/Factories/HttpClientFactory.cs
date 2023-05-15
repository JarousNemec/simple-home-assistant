using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantServer.Factories;

public static class HttpClientFactory
{
    public static AuthCredentials Credentials;
    static HttpClientFactory()
    {
        Credentials = new AuthCredentials("---", "---");
    }
    
    public static HttpClient GetClient()
    {
        var client = new HttpClient();
        client.Timeout = new TimeSpan(0, 0, 15);
        client.DefaultRequestHeaders.Add("User", Credentials.User);
        client.DefaultRequestHeaders.Add("Password", Credentials.Password);
        return client;
    }

    public static void SetCredentials(string user, string pass)
    {
        Credentials.Password = pass;
        Credentials.User = user;
    }
}