using SimpleHomeAssistantServer.Enums;

namespace SimpleHomeAssistantServer;

public class Logger
{
    public static void Info(string msg, HttpRequestType req = HttpRequestType.NONE)
    {
        Console.Write($"[{DateTime.Now}{ReqName(req)}] - ");
        Console.WriteLine(msg);
    }
    public static void Error(string msg, HttpRequestType req = HttpRequestType.NONE)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"[{DateTime.Now}{ReqName(req)}] - ");
        Console.WriteLine(msg);
        Console.ResetColor();
    }
    public static void Warning(string msg, HttpRequestType req = HttpRequestType.NONE)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"[{DateTime.Now}{ReqName(req)}] - ");
        Console.WriteLine(msg);
        Console.ResetColor();
    }
    
    public static void Success(string msg, HttpRequestType req = HttpRequestType.NONE)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"[{DateTime.Now}{ReqName(req)}] - ");
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    private static string ReqName(HttpRequestType req)
    {
        switch (req)
        {
            case HttpRequestType.GET:
                return ", GET";
            case HttpRequestType.POST:
                return ", POST";
            case HttpRequestType.PUT:
                return ", PUT";
            case HttpRequestType.DELETE:
                return ", DELETE";
            case HttpRequestType.NONE:
                return "";
            default:
                return "";
        }
    }
    
}