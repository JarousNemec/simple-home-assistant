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

    private void _radioDayView_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void _radioWeekView_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void _radioMonthView_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void _radioYearView_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void _btnExportChart_Click(object sender, EventArgs e)
    {

    }

    private void _btnViewDataLogTable_Click(object sender, EventArgs e)
    {

    }
}