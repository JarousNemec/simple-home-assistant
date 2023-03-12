using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantUi.Interfaces;

namespace SimpleHomeAssistantUi.Controls;

public partial class SensorCard : UserControl, DeviceCard
{
    public SensorCard()
    {
        InitializeComponent();
    }

    public void Load(JsonObject data)
    {
        
    }
}