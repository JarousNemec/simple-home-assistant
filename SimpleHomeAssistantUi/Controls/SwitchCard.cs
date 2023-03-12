using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantUi.Interfaces;

namespace SimpleHomeAssistantUi.Controls;

public partial class SwitchCard : UserControl, DeviceCard
{
    public SwitchCard()
    {
        InitializeComponent();
    }

    public void Load(JsonObject data)
    {
        
    }
}