using System.Configuration;
using System.Text.Json;
using System.Windows.Forms;
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
        var credentials = _service.GetCredentials();
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

    private void _btnDelete_Click(object sender, EventArgs e)
    {
        var res = _service.SendMessage(_service.GetMainEndpoint() + UserConfigurationManager.Get("DeleteAccount"));
        if (!res) MessageBox.Show("Cannot delete account");
        else
            _service.SetCredentials(new AuthCredentials("---", "---"));
        Close();
    }
}