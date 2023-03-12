using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace SimpleHomeAssistantUi.Controls;

public partial class DeviceCardsPanel : UserControl
{
    public DeviceCardsPanel()
    {
        InitializeComponent();
    }

    public void Load(JsonObject devices)
    {
        foreach (var deviceInfo in devices.AsArray())
        {
            // var card = new SwitchCard();
            // _pnlCards.Controls.Add(card);
        }
    }
}