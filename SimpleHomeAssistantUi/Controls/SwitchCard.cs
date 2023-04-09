using System.Configuration;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantUi.Interfaces;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Controls;

public partial class SwitchCard : UserControl, IDeviceCard
{
    public SwitchCard()
    {
        InitializeComponent();
    }

    public JsonNode Info { get; set; }
    public bool Power { get; set; }

    public void LoadInfo(JsonNode data)
    {
        Info = data;

        _lblFriendlyName.Text = data["Status"]?["FriendlyName"]?[0]?.ToString();
        _lblIpAddress.Text = data["StatusNET"]?["IPAddress"]?.ToString();

        switch (data["Status"]?["Module"]?.ToString())
        {
            case "1":
            case "9":
                {
                    _picDeviceIcon.Image = Image.FromFile("./assets/images/smartSwitch.jpg");
                    break;
                }
            case "8":
                _picDeviceIcon.Image = Image.FromFile("./assets/images/smartSocket.jpg");
                break;
        }

        if (data["Status"]?["Power"]?.ToString() == "0")
        {
            _btnStateSwitch.Text = "Turn On";
            Power = false;
        }
        else
        {
            _btnStateSwitch.Text = "Turn Off";
            Power = true;
        }
    }

    private void _btnStateSwitch_Click(object sender, EventArgs e)
    {
        var http = new HttpService();
        var config = ConfigurationManager.AppSettings;
        var result = http.SendSwitchPowerStateCommand(config.Get("MainEndpoint")+config.Get("SwitchPowerState"),Info["Status"]?["Topic"]?.ToString()!);
        if (result)
        {
            if (Power)
            {
                _btnStateSwitch.Text = "Turn On";
                Power = false;
                Info["Status"]["Power"] = "0";
            }
            else
            {
                _btnStateSwitch.Text = "Turn Off";
                Power = true;
                Info["Status"]["Power"] = "1";
            }
        }
    }
}