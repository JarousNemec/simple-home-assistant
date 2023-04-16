using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Interfaces;

namespace SimpleHomeAssistantUi.Controls;

public partial class DoubleSwitchCard : UserControl, IDeviceCard
{
    public DoubleSwitchCard()
    {
        InitializeComponent();
    }
    public Device Info { get; set; }
    public void LoadInfo(Device data)
    {
        Info = data;
    }

    private void _btnStateSwitch1_Click(object sender, EventArgs e)
    {

    }

    private void _btnStateSwitch2_Click(object sender, EventArgs e)
    {

    }
}