using System.Text.Json;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Managers;
using SimpleHomeAssistantUi.Models;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Forms;

public partial class StatisticsExplorer : Form
{
    private List<Device> _loadedDevices;
    private HttpService _service;
    private Dictionary<string, Dictionary<long, double>> _sumarizedStatistics;

    public StatisticsExplorer()
    {
        InitializeComponent();
        _sumarizedStatistics = new Dictionary<string, Dictionary<long, double>>();
        choseDeviceControl.OnDeviceChosed += ChoseDeviceControlOnDeviceChossed;
    }

    private void ChoseDeviceControlOnDeviceChossed(string deviceName)
    {
        var device = _loadedDevices.FirstOrDefault(x => x.DeviceName == deviceName);
        if (device?.Topic != null && !_sumarizedStatistics.ContainsKey(device.Topic)
                                  && device.Profile.PowerConsummationChangeDateWithSetting.Count > 0)
        {
            var response = _service.SendMessageAndReturnResponseContent(
                _service.GetMainEndpoint() + UserConfigurationManager.Get("SpecificStatistic"), device.Topic);
            var data = JsonSerializer.Deserialize<List<DevicePowerStateRecord>>(response);
            if (!_sumarizedStatistics.ContainsKey(device.Topic))
                _sumarizedStatistics.Add(device.Topic, new Dictionary<long, double>());
            if (data != null) SummarizeDataByHours(data, device);
        }
    }

    private void SummarizeDataByHours(List<DevicePowerStateRecord> data, Device device)
    {
        var ordered = data.OrderBy(x => x.Date);
        var temp = new List<DevicePowerStateRecord>();
        double tempEnergy = 0;
        foreach (var record in ordered)
        {
            if (temp.Count == 0)
            {
                temp.Add(record);
                continue;
            }

            if (record.Date.Hour == temp[^1].Date.Hour && record.Date.Day == temp[^1].Date.Day && record.Date.Month == temp[^1].Date.Month && record.Date.Year == temp[^1].Date.Year)
            {
                temp.Add(record);
                continue;
            }

            var consumption =
                FindCurrentProfileRecord(device.Profile.PowerConsummationChangeDateWithSetting, temp[0].Date);
            var perTick = (double)consumption / TimeSpan.TicksPerHour;
            for (int i = 0; i < temp.Count - 1; i++)
            {
                if (!temp[i + 1].State)
                    continue;
                var interval = temp[i + 1].Date.Ticks - temp[i].Date.Ticks;
                tempEnergy += interval * perTick;
            }

            _sumarizedStatistics[device.Topic]
                .Add(
                    new DateTime(record.Date.Year, record.Date.Month, record.Date.Day, record.Date.Hour, 0, 0)
                        .Ticks, tempEnergy);
            temp = new List<DevicePowerStateRecord>();
        }
    }

    private int FindCurrentProfileRecord(Dictionary<DateTime, int> profileRecords, DateTime record)
    {
        var profileRecordsList = profileRecords.OrderBy(x => x.Key).ToList();
        DateTime output = profileRecordsList[0].Key;
        foreach (var rec in profileRecordsList)
        {
            if (rec.Key < record.Date)
            {
                output = rec.Key;
            }
        }

        return profileRecords[output];
    }

    public void SetDevices(List<Device> loadedDevices, HttpService service)
    {
        _service = service;
        _loadedDevices = loadedDevices;
        choseDeviceControl.SetDevices(_loadedDevices);
        _deviceStatisticsChart.SetData("[E]", "[t]", ChartViewModes.Day);
    }

    private void _radioDayView_CheckedChanged(object sender, EventArgs e)
    {
        _deviceStatisticsChart.SetData("[E]", "[t]", ChartViewModes.Day);
    }

    private void _radioWeekView_CheckedChanged(object sender, EventArgs e)
    {
        _deviceStatisticsChart.SetData("[E]", "[t]", ChartViewModes.Week);
    }

    private void _radioMonthView_CheckedChanged(object sender, EventArgs e)
    {
        _deviceStatisticsChart.SetData("[E]", "[t]", ChartViewModes.Month);
    }

    private void _radioYearView_CheckedChanged(object sender, EventArgs e)
    {
        _deviceStatisticsChart.SetData("[E]", "[t]", ChartViewModes.Year);
    }

    private void _btnExportChart_Click(object sender, EventArgs e)
    {
        _deviceStatisticsChart.Invalidate();
    }

    private void _btnViewDataLogTable_Click(object sender, EventArgs e)
    {
    }

    private void _deviceStatisticsChart_Paint(object sender, PaintEventArgs e)
    {
    }
}