using System.Collections.Specialized;
using System.Configuration;
using System.Text.Json;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Factories;
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

    private async void TimerSettingsOnSave(TimerSettings settings)
    {
        using var client = HttpClientFactory.GetClient();
        var msg = JsonSerializer.Serialize(settings);
        var res = await _service.SendMessage(_service.GetMainEndpoint() + _config.Get("SetTimer"),client, msg);
        if (!res) return;
        var i = _device.Timers.FindIndex(x => x.TimerName == settings.TimerName);
        _device.Timers[i] = settings;
    }

    private async void _btnPowerConsumptionUpdate_Click(object sender, EventArgs e)
    {
        using var client = HttpClientFactory.GetClient();
        if (_numPowerConsumption.Value < 1) return;
        _device.Profile.PowerConsummationChangeDateWithSetting.Add(DateTime.Now, (int)_numPowerConsumption.Value);
        var msg = JsonSerializer.Serialize(_device.Profile);
        var res = await _service.SendMessage(_service.GetMainEndpoint() + _config.Get("EditProfile"),client, msg);
        if (res)
        {
            _device.Topic = _txtTopic.Text;
        }
    }

    private async void _btnTopicUpdate_Click(object sender, EventArgs e)
    {
        using var client = HttpClientFactory.GetClient();
        var msg = JsonSerializer.Serialize(new BasicMessage() { Topic = _device.Topic, Payload = _txtTopic.Text });
        var res = await _service.SendMessage(_service.GetMainEndpoint() + _config.Get("SetDeviceTopic"),client, msg);
        if (res)
        {
            _device.Topic = _txtTopic.Text;
        }
    }

    private async void _btnDeviceNameUpdate_Click(object sender, EventArgs e)
    {
        using var client = HttpClientFactory.GetClient();
        var msg = JsonSerializer.Serialize(new BasicMessage() { Topic = _device.Topic, Payload = _txtDeviceName.Text });
        var res = await _service.SendMessage(_service.GetMainEndpoint() + _config.Get("SetDeviceName"),client, msg);
        if (res)
        {
            _device.DeviceName = _txtDeviceName.Text;
        }
    }

    private async void _btnFriendlyNameUpdate_Click(object sender, EventArgs e)
    {
        using var client = HttpClientFactory.GetClient();
        var msg = JsonSerializer.Serialize(new BasicMessage()
            { Topic = _device.Topic, Payload = _txtFrienlyName.Text });
        var res = await _service.SendMessage(_service.GetMainEndpoint() + _config.Get("SetFriendlyName"),client, msg);
        if (res)
        {
            _device.FriendlyName = _txtFrienlyName.Text;
        }
    }
}