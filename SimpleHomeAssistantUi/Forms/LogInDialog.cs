using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Forms;

public partial class LogInDialog : Form
{
    private HttpService _service;

    public LogInDialog()
    {
        InitializeComponent();
    }

    public void SetService(HttpService service)
    {
        _service = service;
    }

    private void _btnLogIn_Click(object sender, EventArgs e)
    {
        _service.SetCredentials(new AuthCredentials(_txtUser.Text, _txtPassword.Text));
        Close();
    }
}