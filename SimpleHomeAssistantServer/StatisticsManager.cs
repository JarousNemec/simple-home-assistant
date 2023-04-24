using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Text.Json;
using System.Timers;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantServer.Workers;
using Timer = System.Timers.Timer;

namespace SimpleHomeAssistantServer;

public class StatisticsManager
{
    private List<Device> _devicesRegister;
    private Dictionary<string, List<DevicePowerStateRecord>> _todayDeviceStatistics = null!;
    private NameValueCollection _config;
    private Timer _autoSave;
    private Timer _statisticsLogger;

    public StatisticsManager(List<Device> devicesRegister)
    {
        _config = ConfigurationManager.AppSettings;
        _devicesRegister = devicesRegister;
        Load();

        _autoSave = new Timer();
        _autoSave.AutoReset = true;
        _autoSave.Interval = 60000;
        _autoSave.Elapsed += AutoSaveOnElapsed;

        _statisticsLogger = new Timer();
        _statisticsLogger.AutoReset = true;
        _statisticsLogger.Interval = 20000;
        _statisticsLogger.Elapsed += StatisticsLoggerOnElapsed;
    }

    public void Run()
    {
        _autoSave.Start();
        _statisticsLogger.Start();
    }

    private void StatisticsLoggerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        DownloadStatistics();
    }

    public void DownloadStatistics()
    {
        var topics = _devicesRegister.Select(device => device.Topic).ToArray();
        var topicsPaths = new List<string>();
        foreach (var topic in topics)
        {
            topicsPaths.Add($"stat/{topic}/RESULT");
        }

        var statisticsWorker =
            new Thread(new MqttDevicesPowerStatesWorker(topics, topicsPaths.ToArray(), _todayDeviceStatistics).Run);
        statisticsWorker.Start();
    }

    public Dictionary<string, List<DevicePowerStateRecord>> GetTodayRecords()
    {
        if (_todayDeviceStatistics.Count == 0)
            return null;
        return _todayDeviceStatistics;
    }
    
    public Dictionary<string, DevicePowerStateRecord> GetTodayLastRecords()
    {
        if (_todayDeviceStatistics.Count == 0)
            return null;
        
        var records = new Dictionary<string, DevicePowerStateRecord>();
        foreach (var todayDeviceStatistic in _todayDeviceStatistics)
        {
            records.Add(todayDeviceStatistic.Key, todayDeviceStatistic.Value[todayDeviceStatistic.Value.Count-1]);
        }
        return records;
    }

    private void AutoSaveOnElapsed(object? sender, ElapsedEventArgs e)
    {
        Save();
    }

    private void Load()
    {
        var todayStatisticsFileName =
            $"{_config.Get("StatisticsPath")}{new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks}.rec";
        if (File.Exists(todayStatisticsFileName))
        {
            var data = File.ReadAllText(todayStatisticsFileName);
            _todayDeviceStatistics =
                JsonSerializer.Deserialize<Dictionary<string, List<DevicePowerStateRecord>>>(data) ??
                new Dictionary<string, List<DevicePowerStateRecord>>();
        }
        else
        {
            _todayDeviceStatistics = new Dictionary<string, List<DevicePowerStateRecord>>();
        }
    }

    public void Save()
    {
        var data = JsonSerializer.Serialize(_todayDeviceStatistics);
        var todayStatisticsFileName =
            $"{_config.Get("StatisticsPath")}{new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks}.rec";
        File.WriteAllText(todayStatisticsFileName, data);
    }

    public string GetSpecificTodayStatistic(string topic)
    {
        return _todayDeviceStatistics.ContainsKey(topic) ? JsonSerializer.Serialize(_todayDeviceStatistics[topic][^1]) : string.Empty;
    }

    public string GetSpecificStatistic(string topic)
    {
        List<DevicePowerStateRecord> records = new List<DevicePowerStateRecord>();
        var recordFiles = Directory.GetFiles(_config.Get("StatisticsPath"));
        for (int i = 0; i < recordFiles.Length; i++)
        {
            var data = File.ReadAllText(recordFiles[i]);
            var DeviceStatistics = JsonSerializer.Deserialize<Dictionary<string, List<DevicePowerStateRecord>>>(data) ??
                               new Dictionary<string, List<DevicePowerStateRecord>>();
            if (DeviceStatistics.ContainsKey(topic))
            {
                for (int j = 0; j < DeviceStatistics[topic].Count; j++)
                {
                    records.Add(DeviceStatistics[topic][j]);
                }
            }
            
        }

        return JsonSerializer.Serialize(records);
    }
}