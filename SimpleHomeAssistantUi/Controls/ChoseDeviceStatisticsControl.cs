using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantUi.Controls;

public partial class ChoseDeviceStatisticsControl : UserControl
{
    private List<Device> _loadedDevices;
    public ChoseDeviceStatisticsControl()
    {
        InitializeComponent();
    }
    
    public void SetDevices(List<Device> loadedDevices)
    {
        _loadedDevices = loadedDevices;
        DisplayDevices();
    }
    
    private const int BUTTONS_MARGIN = 7;
    private const int BUTTONS_HEIGHT = 30;
    private void DisplayDevices()
    {
        for (int i = 0; i < _loadedDevices.Count; i++)
        {
            Button deviceButton = new Button();
            deviceButton.Size = new Size(_pnlDevices.Width - BUTTONS_MARGIN * 2, BUTTONS_HEIGHT);
            deviceButton.Location = new Point(BUTTONS_MARGIN, BUTTONS_MARGIN + i * (BUTTONS_MARGIN + BUTTONS_HEIGHT));
            deviceButton.Text = _loadedDevices[i].FriendlyName;
            deviceButton.Name = $"_btn{_loadedDevices[i].DeviceName}";
            deviceButton.Click += DeviceButtonOnClick;
            _pnlDevices.Controls.Add(deviceButton);
        }
    }

    public delegate void ChoseDevice(string deviceName);

    public event ChoseDevice OnDeviceChosed;

    private void DeviceButtonOnClick(object? sender, EventArgs e)
    {
        var button = sender as Button;
        OnDeviceChosed?.Invoke(button.Name[4..]);
    }
}