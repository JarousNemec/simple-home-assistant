using Timer = System.Timers.Timer;

namespace SimpleHomeAssistantServer.Models;

public class TimerSettings
{
    public string TimerName { get; set; }
    public string Topic { get; set; }
    public bool IsEnabled { get; set; }

    public int Enable
    {
        get => IsEnabled ? 1 : 0;
        set
        {
            if (value == 1)
            {
                IsEnabled = true;
            }
            else if (value == 0)
            {
                IsEnabled = false;
            }
        }
    }

    public int Mode { get; set; }
    public TimeSpan ActionTime { get; set; }

    public string Time
    {
        get => $"{ActionTime.Hours}:{ActionTime.Minutes}";
        set
        {
            var temp = value.Split(':');
            ActionTime = new TimeSpan(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1]), 0);
        }
    }

    public int Window { get; set; }
    public string Days { get; set; }
    public bool IsRepeating { get; set; }

    public int Repeat
    {
        get => IsRepeating ? 1 : 0;
        set
        {
            if (value == 1)
            {
                IsRepeating = true;
            }
            else if (value == 0)
            {
                IsRepeating = false;
            }
        }
    }

    public int Output { get; set; }
    public TimerActions TimerAction { get; set; }

    public int Action
    {
        get => (int)TimerAction;
        set => TimerAction = (TimerActions)value;
    }

    public TimerSettings(string timerName, string topic, string days, bool isEnabled, int enable, TimeSpan actionTime,
        string time, int window, bool isRepeating, int repeat, int output, TimerActions timerAction, int action, int mode)
    {
        TimerName = timerName;
        Topic = topic;
        Days = days;
        IsEnabled = isEnabled;
        Enable = enable;
        ActionTime = actionTime;
        Time = time;
        Window = window;
        IsRepeating = isRepeating;
        Repeat = repeat;
        Output = output;
        TimerAction = timerAction;
        Action = action;
        Mode = mode;
    }

    public TimerSettings(string timerName, string topic,int enable, int mode,string time,int window,string days,int repeat, int output, int action )
    {
        TimerName = timerName;
        Topic = topic;
        Days = days;
        Enable = enable;
        Mode = mode;
        Time = time;
        Window = window;
        Repeat = repeat;
        Output = output;
        Action = action;
    }
    
    public TimerSettings(string timerName, string topic, bool isEnabled, int mode, TimeSpan actionTime, int window, string days, bool isRepeating, int output, TimerActions timerAction)
    {
        TimerName = timerName;
        Topic = topic;
        IsEnabled = isEnabled;
        Mode = mode;
        ActionTime = actionTime;
        Window = window;
        Days = days;
        IsRepeating = isRepeating;
        Output = output;
        TimerAction = timerAction;
    }

    public TimerSettings()
    {
        
    }
}