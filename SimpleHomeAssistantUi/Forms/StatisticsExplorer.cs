using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantUi.Forms;

public partial class StatisticsExplorer : Form
{
    private List<Device> _loadedDevices;
    public StatisticsExplorer()
    {
        InitializeComponent();
        choseDeviceControl.OnDeviceChosed += ChoseDeviceControlOnDeviceChosed;
    }

    private void ChoseDeviceControlOnDeviceChosed(string deviceName)
    {

    }

    public void SetDevices(List<Device> loadedDevices)
    {
        _loadedDevices = loadedDevices;
        choseDeviceControl.SetDevices(_loadedDevices);
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {

    }
}