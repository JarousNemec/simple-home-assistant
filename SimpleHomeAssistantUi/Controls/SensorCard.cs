using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Interfaces;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Controls;

public partial class SensorCard : UserControl, IDeviceCard
{
    public HttpService Service { get; set; }
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