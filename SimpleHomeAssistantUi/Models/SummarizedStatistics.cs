namespace SimpleHomeAssistantUi.Models;

public class SummarizedStatistics
{
    private Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, double>>>>> _statistics;
    
    public SummarizedStatistics()
    {
        _statistics =
            new Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, double>>>>>();
        
        
    }

    private Dictionary<int, double> DefaultResponse(int count)
    {
        var defaultResponse = new Dictionary<int, double>();
        for (int i = 0; i < count; i++)
        {
            defaultResponse.Add(i+1,0);
        }

        return defaultResponse;
    }

    public void CheckOrAddDevice(string topic)
    {
        if (_statistics.ContainsKey(topic)) return;
        _statistics.Add(topic, new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, double>>>>());
    }

    public void CheckOrAddYear(string topic, DateTime time)
    {
        CheckOrAddDevice(topic);
        if (_statistics[topic].ContainsKey(time.Year)) return;
        _statistics[topic].Add(time.Year, new Dictionary<int, Dictionary<int, Dictionary<int, double>>>());
    }

    public void CheckOrAddMonth(string topic, DateTime time)
    {
        CheckOrAddYear(topic, time);
        if (_statistics[topic][time.Year].ContainsKey(time.Month)) return;
        _statistics[topic][time.Year].Add(time.Month, new Dictionary<int, Dictionary<int, double>>());
    }

    public void CheckOrAddDay(string topic, DateTime time)
    {
        CheckOrAddMonth(topic, time);
        if (_statistics[topic][time.Year][time.Month].ContainsKey(time.Day)) return;
        _statistics[topic][time.Year][time.Month].Add(time.Day, new Dictionary<int, double>());
    }

    public void AddHourConsumption(string topic, DateTime time, double value)
    {
        CheckOrAddDay(topic, time);
        if (_statistics[topic][time.Year][time.Month][time.Day].ContainsKey(time.Hour)) return;
        _statistics[topic][time.Year][time.Month][time.Day].Add(time.Hour, value);
    }

    public Dictionary<int, double> GetStatisticsForOneDay(string topic, DateTime time)
    {
        if (!_statistics.ContainsKey(topic)) return DefaultResponse(24);
        if (!_statistics[topic].ContainsKey(time.Year)) return DefaultResponse(24);
        if (!_statistics[topic][time.Year].ContainsKey(time.Month)) return DefaultResponse(24);
        if (!_statistics[topic][time.Year][time.Month].ContainsKey(time.Day)) return DefaultResponse(24);
        var output = new Dictionary<int, double>();
        for (int i = 0; i < 24; i++)
        {
            if (_statistics[topic][time.Year][time.Month][time.Day].ContainsKey(i + 1))
                output.Add(i + 1, _statistics[topic][time.Year][time.Month][time.Day][i + 1]);
            else
                output.Add(i + 1, 0);
        }

        return output;
    }

    public Dictionary<int, double> GetStatisticsForOneMonth(string topic, DateTime time)
    {
        if (!_statistics.ContainsKey(topic)) return DefaultResponse(DateTime.DaysInMonth(time.Year, time.Month));
        if (!_statistics[topic].ContainsKey(time.Year)) return DefaultResponse(DateTime.DaysInMonth(time.Year, time.Month));
        if (!_statistics[topic][time.Year].ContainsKey(time.Month)) return DefaultResponse(DateTime.DaysInMonth(time.Year, time.Month));
        var output = new Dictionary<int, double>();
        for (int i = 0; i < DateTime.DaysInMonth(time.Year, time.Month); i++)
        {
            output.Add(i + 1, MakeSumFromOneDay(topic, new DateTime(time.Year, time.Month, i + 1)));
        }

        return output;
    }

    public Dictionary<int, double> GetStatisticsForOneYear(string topic, DateTime time)
    {
        if (!_statistics.ContainsKey(topic)) return DefaultResponse(12);
        if (!_statistics[topic].ContainsKey(time.Year)) return DefaultResponse(12);
        var output = new Dictionary<int, double>();
        for (int i = 0; i < 12; i++)
        {
            output.Add(i + 1, MakeSumFromOneMonth(topic, new DateTime(time.Year, i + 1, 1)));
        }

        return output;
    }

    private double MakeSumFromOneDay(string topic, DateTime time)
    {
        double output = 0;
        if (!_statistics[topic][time.Year][time.Month].ContainsKey(time.Day)) return output;
        foreach (var hour in _statistics[topic][time.Year][time.Month][time.Day])
        {
            output += hour.Value;
        }

        return output;
    }

    private double MakeSumFromOneMonth(string topic, DateTime time)
    {
        double output = 0;
        if (!_statistics[topic][time.Year].ContainsKey(time.Month)) return output;
        foreach (var day in _statistics[topic][time.Year][time.Month])
        {
            output += MakeSumFromOneDay(topic, new DateTime(time.Year, time.Month, day.Key));
        }

        return output;
    }

    public bool IsDeviceSummarized(string topic)
    {
        return _statistics.ContainsKey(topic);
    }
}