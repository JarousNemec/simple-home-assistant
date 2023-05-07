using System.Collections.Specialized;
using System.Configuration;
using System.Text.Json;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Forms;

public partial class SwitchSettings : Form
{
    private Device _device;
    private HttpService _service;
    private NameValueCollection _config;

    public SwitchSettings()
    {
        _config = ConfigurationManager.AppSettings;
        InitializeComponent();
    }

    public void Setup(Device device, HttpService service)
    {
        _service = service;
        _device = device;
        _lblName.Text = device.FriendlyName;
        _txtFrienlyName.Text = device.FriendlyName;
        _txtDeviceName.Text = device.DeviceName;
        _txtTopic.Text = device.Topic;

        if (device.Profile.PowerConsummationChangeDateWithSetting.Count == 0)
        {
            _numPowerConsumption.Value = 0;
            return;
        }
        var recordValue = device.Profile.PowerConsummationChangeDateWithSetting.Last().Value;
        _numPowerConsumption.Value = recordValue;
    }

    private void _btnSetTimers_Click(object sender, EventArgs e)
    {
        var timerSettings = new TimerSettingsDialog();
        timerSettings.Setup(_device.Timers);
        timerSettings.OnSave += TimerSettingsOnSave;
        timerSettings.Show();
    }

    private void TimerSettingsOnSave(TimerSettings settings)
    {
        var msg = JsonSerializer.Serialize(settings);
        var res = _service.SendMessage(_service.GetMainEndpoint() + _config.Get("SetTimer"), msg);
        if (!res) return;
        var i = _device.Timers.FindIndex(x => x.TimerName == settings.TimerName);
        _device.Timers[i] = settings;
    }

    private void _btnPowerConsumptionUpdate_Click(object sender, EventArgs e)
    {
        if (_numPowerConsumption.Value < 1) return;
        _device.Profile.PowerConsummationChangeDateWithSetting.Add(DateTime.Now, (int)_numPowerConsumption.Value);
        var msg = JsonSerializer.Serialize(_device.Profile);
        _service.SendMessage(_service.GetMainEndpoint() + _config.Get("EditProfile"), msg);
    }

    private void _btnTopicUpdate_Click(object sender, EventArgs e)
    {
        var msg = JsonSerializer.Serialize(new BasicMessage() { Topic = _device.Topic, Payload = _txtTopic.Text });
        var res =_service.SendMessage(_service.GetMainEndpoint() + _config.Get("SetDeviceTopic"), msg);
        if (res)
        {
            _device.Topic = _txtTopic.Text;
        }
    }

    private void _btnDeviceNameUpdate_Click(object sender, EventArgs e)
    {
        var msg = JsonSerializer.Serialize(new BasicMessage() { Topic = _device.Topic, Payload = _txtDeviceName.Text });
        var res =_service.SendMessage(_service.GetMainEndpoint() + _config.Get("SetDeviceName"), msg);
        if (res)
        {
            _device.DeviceName = _txtDeviceName.Text;
        }
    }

    private void _btnFriendlyNameUpdate_Click(object sender, EventArgs e)
    {
        var msg = JsonSerializer.Serialize(new BasicMessage() { Topic = _device.Topic, Payload = _txtFrienlyName.Text });
        var res = _service.SendMessage(_service.GetMainEndpoint() + _config.Get("SetFriendlyName"), msg);
        if (res)
        {
            _device.FriendlyName = _txtFrienlyName.Text;
        }
    }
}