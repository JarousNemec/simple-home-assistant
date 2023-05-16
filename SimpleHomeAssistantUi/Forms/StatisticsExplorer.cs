using System.Text.Json;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Factories;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Managers;
using SimpleHomeAssistantUi.Models;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Forms;

public partial class StatisticsExplorer : Form
{
    private List<Device> _loadedDevices;
    private HttpService _service;
    private SummarizedStatistics _statistics;
    private DateTime _cursor;
    private Device _actualDevice;

    public StatisticsExplorer()
    {
        InitializeComponent();
        choseDeviceControl.OnDeviceChosed += ChoseDeviceControlOnDeviceChossed;
        _deviceStatisticsChart.SetInit("[E]", "[t]");
        _statistics = new SummarizedStatistics();
    }

    private void ChoseDeviceControlOnDeviceChossed(string deviceName)
    {
        var device = _loadedDevices.FirstOrDefault(x => x.DeviceName == deviceName);
        _actualDevice = device;
        SetDataToChart();
    }

    private async void SetDataToChart()
    {
        if (_actualDevice?.Topic != null && !_statistics.IsDeviceSummarized(_actualDevice.Topic)
                                         && _actualDevice.Profile.PowerConsummationChangeDateWithSetting.Count >
                                         0)
        {
            using var client = HttpClientFactory.GetClient();
            var response = await _service.SendMessageAndReturnResponseContent(
                _service.GetMainEndpoint() + UserConfigurationManager.Get("SpecificStatistic"), client,
                _actualDevice.Topic);
            var data = JsonSerializer.Deserialize<List<DevicePowerStateRecord>>(response);

            if (data != null) SummarizeDataByHours(data, _actualDevice);
        }

        switch (_deviceStatisticsChart.Mode)
        {
            case ChartViewModes.Day:
                _deviceStatisticsChart.SetData(_statistics.GetStatisticsForOneDay(_actualDevice.Topic, _cursor));
                break;
            case ChartViewModes.Month:
                _deviceStatisticsChart.SetData(_statistics.GetStatisticsForOneMonth(_actualDevice.Topic, _cursor));
                break;
            case ChartViewModes.Year:
                _deviceStatisticsChart.SetData(_statistics.GetStatisticsForOneYear(_actualDevice.Topic, _cursor));
                break;
            default:
                break;
        }

        if (_actualDevice.Profile.PowerConsummationChangeDateWithSetting.Count >
            0)
            _lblPowerConsumtion.Text = _actualDevice.Profile.PowerConsummationChangeDateWithSetting.ToList()[^1].Value
                .ToString();
        else _lblPowerConsumtion.Text = "unset";
        _lblRecordInterval.Text = $"{_cursor.Day}.{_cursor.Month}.{_cursor.Year}";
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

            if (record.Date.Hour == temp[^1].Date.Hour && record.Date.Day == temp[^1].Date.Day &&
                record.Date.Month == temp[^1].Date.Month && record.Date.Year == temp[^1].Date.Year)
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

            _statistics.AddHourConsumption(device.Topic,
                new DateTime(record.Date.Year, record.Date.Month, record.Date.Day, record.Date.Hour, 0, 0), tempEnergy);
            tempEnergy = 0;
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
        if (loadedDevices.Count == 0) return;
        _cursor = _loadedDevices[0].Profile.PowerConsummationChangeDateWithSetting.Count == 0
            ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            : new Func<DateTime>(() =>
            {
                var date = _loadedDevices[0].Profile.PowerConsummationChangeDateWithSetting.ToArray()[0].Key;
                return new DateTime(date.Year, date.Month, date.Day);
            }).Invoke();
        _actualDevice = _loadedDevices[0];
        _deviceStatisticsChart.Mode = ChartViewModes.Month;
        SetDataToChart();
    }

    private void _radioDayView_CheckedChanged(object sender, EventArgs e)
    {
        _deviceStatisticsChart.Mode = ChartViewModes.Day;
        SetDataToChart();
    }

    private void _radioMonthView_CheckedChanged(object sender, EventArgs e)
    {
        _deviceStatisticsChart.Mode = ChartViewModes.Month;
        SetDataToChart();
    }

    private void _radioYearView_CheckedChanged(object sender, EventArgs e)
    {
        _deviceStatisticsChart.Mode = ChartViewModes.Year;
        SetDataToChart();
    }

    private void _btnExportChart_Click(object sender, EventArgs e)
    {
        SaveFileDialog dialog = new SaveFileDialog();
        var res = dialog.ShowDialog();
        if (res == DialogResult.OK)
        {
            Image bmp = new Bitmap(Width, Height);
            _deviceStatisticsChart.GetChartPanel().DrawToBitmap((Bitmap)bmp,
                new Rectangle(0, 0, _deviceStatisticsChart.Width, _deviceStatisticsChart.Height));
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawString(
                    $"Name: {_actualDevice.FriendlyName}, Cursor: {_cursor.Day}.{_cursor.Month}.{_cursor.Year}, Set consumption[Wh]: {_actualDevice.Profile.PowerConsummationChangeDateWithSetting.ToList()[^1].Value.ToString()}",
                    new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, 40, 10);
            }

            bmp.Save(dialog.FileName + ".bmp");
        }
    }

    private void _deviceStatisticsChart_Paint(object sender, PaintEventArgs e)
    {
    }

    private void _btnMoveBack_Click(object sender, EventArgs e)
    {
        switch (_deviceStatisticsChart.Mode)
        {
            case ChartViewModes.Day:
                _cursor = _cursor.Day > 1 ? new DateTime(_cursor.Year, _cursor.Month, _cursor.Day - 1) : _cursor;
                break;
            case ChartViewModes.Month:
                _cursor = _cursor.Month > 1 ? new DateTime(_cursor.Year, _cursor.Month - 1, _cursor.Day) : _cursor;
                break;
            case ChartViewModes.Year:
                _cursor = _cursor.Year > 2023 ? new DateTime(_cursor.Year - 1, _cursor.Month, _cursor.Day) : _cursor;
                break;
            default:
                break;
        }

        SetDataToChart();
    }

    private void _btnMoveForward_Click(object sender, EventArgs e)
    {
        switch (_deviceStatisticsChart.Mode)
        {
            case ChartViewModes.Day:
                _cursor = _cursor.Day < DateTime.DaysInMonth(_cursor.Year, _cursor.Month)
                    ? new DateTime(_cursor.Year, _cursor.Month, _cursor.Day + 1)
                    : _cursor;
                break;
            case ChartViewModes.Month:
                _cursor = _cursor.Month < 12 ? new DateTime(_cursor.Year, _cursor.Month + 1, _cursor.Day) : _cursor;
                break;
            case ChartViewModes.Year:
                _cursor = _cursor.Year < 6969 ? new DateTime(_cursor.Year + 1, _cursor.Month, _cursor.Day) : _cursor;
                break;
            default:
                break;
        }

        SetDataToChart();
    }
}