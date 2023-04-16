using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Interfaces;

namespace SimpleHomeAssistantUi.Controls;

public partial class SensorCard : UserControl, IDeviceCard
{
    public SensorCard()
    {
        InitializeComponent();
    }
    public Device Info { get; set; }
    public void LoadInfo(Device data)
    {
        Info = data;
    }
}