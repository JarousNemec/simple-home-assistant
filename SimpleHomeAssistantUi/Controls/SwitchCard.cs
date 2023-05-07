using System.Configuration;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Forms;
using SimpleHomeAssistantUi.Interfaces;
using SimpleHomeAssistantUi.Managers;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Controls;

public partial class SwitchCard : UserControl, IDeviceCard
{
    public HttpService Service { get; set; }
    public Device Info { get; set; }
    public bool Power { get; set; }

    public SwitchCard()
    {
        InitializeComponent();
    }


    public void LoadInfo(Device data)
    {
        Info = data;

        _lblFriendlyName.Text = Info.FriendlyName;
        _lblIpAddress.Text = Info.Ip;

        switch (Info.Module)
        {
            case 1:
            case 9:
            {
                _picDeviceIcon.Image = Image.FromFile("./assets/images/smartSwitch.jpg");
                break;
            }
            case 8:
                _picDeviceIcon.Image = Image.FromFile("./assets/images/smartSocket.jpg");
                break;
        }

        if (!Info.Power)
        {
            _btnStateSwitch.Text = "Zapnout";
            Power = false;
        }
        else
        {
            _btnStateSwitch.Text = "Vypnout";
            Power = true;
        }
    }

    private void _btnStateSwitch_Click(object sender, EventArgs e)
    {
        var result = Service.SendMessage(Service.GetMainEndpoint() + UserConfigurationManager.Get("SwitchPowerState"),
            Info.Topic);
        if (result)
        {
            if (Power)
            {
                _btnStateSwitch.Text = "Zapnout";
                Power = false;
            }
            else
            {
                _btnStateSwitch.Text = "Vypnout";
                Power = true;
            }

            Info.Power = Power;
        }
    }

    private void _btnSettings_Click(object sender, EventArgs e)
    {
        var settings = new SwitchSettings();
        settings.Setup(Info, Service);
        settings.Show();
    }
}