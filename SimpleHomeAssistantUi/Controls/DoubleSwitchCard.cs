using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantUi.Interfaces;

namespace SimpleHomeAssistantUi.Controls;

public partial class DoubleSwitchCard : UserControl, DeviceCard
{
    public DoubleSwitchCard()
    {
        InitializeComponent();
    }

    public void Load(JsonObject data)
    {
        
    }
}