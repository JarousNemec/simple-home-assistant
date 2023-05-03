namespace SimpleHomeAssistantServer.Models;

public class AuthCredentials
{
    public string User { get; set; }
    public string Password { get; set; }

    public AuthCredentials(string user, string password)
    {
        User = user;
        Password = password;
    }
}