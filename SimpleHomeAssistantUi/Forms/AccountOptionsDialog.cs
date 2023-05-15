using System.Configuration;
using System.Text.Json;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Factories;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Managers;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Forms;

public partial class AccountOptionsDialog : Form
{
    private HttpService _service;

    public AccountOptionsDialog()
    {
        InitializeComponent();
    }

    public void SetService(HttpService service)
    {
        _service = service;
        var credentials = HttpClientFactory.Credentials;
        _lblUser.Text = credentials.User;
        _lblPassword.Text = credentials.Password;
    }

    private void _btnLogin_Click(object sender, EventArgs e)
    {
        var loginDialog = new LogInDialog();
        loginDialog.SetService(_service);
        loginDialog.Show();
        Close();
    }

    private void _btnAdd_Click(object sender, EventArgs e)
    {
        var addDialog = new AddAccountDialog();
        addDialog.SetService(_service);
        addDialog.Show();
        Close();
    }

    private async void _btnDelete_Click(object sender, EventArgs e)
    {using var client = HttpClientFactory.GetClient();
        var res = await _service.SendMessage(_service.GetMainEndpoint() + UserConfigurationManager.Get("DeleteAccount"), client);
        if (!res) MessageBox.Show("Cannot delete account");
        else
            HttpClientFactory.SetCredentials("---","---");
        Close();
    }
}