using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantUi.Interfaces;

namespace SimpleHomeAssistantUi.Controls;

public partial class DoubleSwitchCard : UserControl, IDeviceCard
{
    public DoubleSwitchCard()
    {
        InitializeComponent();
    }
    public JsonNode Info { get; set; }
    public void LoadInfo(JsonNode data)
    {
        Info = data;
    }
}