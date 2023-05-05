using System.Configuration;
using System.Windows.Forms;
using Newtonsoft.Json;
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

    private void _btnAdd_Click(object sender, EventArgs e)
    {
        var credence = new AuthCredentials(_txtUser.Text, _txtPassword.Text);
        var res = _service.SendMessage(_service.GetMainEndpoint() + UserConfigurationManager.Get("AddAccount"),
            JsonSerializer.Serialize(credence));
        if (!res) MessageBox.Show("Cannot add account");
        else
            _service.SetCredentials(credence);
        Close();
    }
}