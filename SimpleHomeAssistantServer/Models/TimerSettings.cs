namespace SimpleHomeAssistantServer.Models;

public class TimerSettings
{
    public string TimerName { get; set; }
    public string Topic { get; set; }
    public bool IsEnabled { get; set; }
    public int Enable
    {
        get => IsEnabled ? 1 : 0;
        set {if (value == 1)
            {
                IsEnabled = true;
            }
            else if (value == 0)
            {
                IsEnabled = false;
            }
        }
    }

    public DateTime ActionTime { get; set; }    
    public string Time
    {
        get => $"{ActionTime.Hour}:{ActionTime.Minute}";
        set
        {
            var temp = value.Split(':');
            ActionTime = new DateTime(0, 0, 0, Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1]), 0);
        } }
    public int Window { get; set; }
    public string Days { get; set; }
    public bool IsRepeating { get; set; }
    public int Repeat {get => IsRepeating ? 1 : 0;
        set {if (value == 1)
            {
                IsRepeating = true;
            }
            else if (value == 0)
            {
                IsRepeating = false;
            }
        } }
    public int Output { get; set; }
    public TimerActions TimerAction { get; set; }

    public int Action
    {
        get => (int)TimerAction;
        set => TimerAction = (TimerActions)value;
    }
}