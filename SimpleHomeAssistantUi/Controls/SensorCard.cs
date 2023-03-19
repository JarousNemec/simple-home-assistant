using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantUi.Interfaces;

namespace SimpleHomeAssistantUi.Controls;

public partial class SensorCard : UserControl, IDeviceCard
{
    public SensorCard()
    {
        InitializeComponent();
    }
    public JsonNode Info { get; set; }
    public void LoadInfo(JsonNode data)
    {
        Info = data;
    }
}