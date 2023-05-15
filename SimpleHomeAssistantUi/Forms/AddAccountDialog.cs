using System.Configuration;
using System.Windows.Forms;
using Newtonsoft.Json;
using SimpleHomeAssistantServer.Factories;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Managers;
using SimpleHomeAssistantUi.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SimpleHomeAssistantUi.Forms;

public partial class AddAccountDialog : Form
{
    private HttpService _service;

    public AddAccountDialog()
    {
        InitializeComponent();
    }

    public void SetService(HttpService service)
    {
        _service = service;
    }

    private async void _btnAdd_Click(object sender, EventArgs e)
    {
        using var client = HttpClientFactory.GetClient();
        var credence = new AuthCredentials(_txtUser.Text, _txtPassword.Text);
        var res = await _service.SendMessage(_service.GetMainEndpoint() + UserConfigurationManager.Get("AddAccount"),
            client,
            JsonSerializer.Serialize(credence));
        if (!res) MessageBox.Show("Cannot add account");
        else
            HttpClientFactory.SetCredentials(credence.User, credence.Password);
        Close();
    }
}