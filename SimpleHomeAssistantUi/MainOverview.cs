using System.Configuration;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Forms;
using SimpleHomeAssistantUi.Managers;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi;

public partial class MainOverview : Form
{
    private HttpService _httpService;
    private List<Device> _loadedDevices;

    public MainOverview()
    {
        _loadedDevices = new List<Device>();
        InitializeComponent();

        _httpService = new HttpService();
        _deviceCardsPanel.Service = _httpService;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        _updater_Tick(sender, e);
        LoadDevices();
        var statisticsExplorer = new StatisticsExplorer();
        statisticsExplorer.SetDevices(_loadedDevices, _httpService);
        statisticsExplorer.Show();
    }

    private void _updater_Tick(object sender, EventArgs e)
    {
        var time = DateTime.Now;
        _lblDateTime.Text =
            $@"{time.Hour}:{CorrectFormat(time.Minute)}:{CorrectFormat(time.Second)}  {time.Day}.{CorrectFormat(time.Month)}.{time.Year}";
    }

    private static string CorrectFormat(int number)
    {
        return number.ToString().Length == 1 ? $"0{number.ToString()}" : number.ToString();
    }

    private void LoadDevices()
    {
        var devices =
            _httpService.DownloadJsonObject<Device[]>(_httpService.GetMainEndpoint() +
                                                      UserConfigurationManager.Get("AllDevices"));
        if (devices == null) return;
        _loadedDevices = devices.ToList();
        _deviceCardsPanel.LoadDevices(_loadedDevices);
    }

    private void _btnRefresh_Click(object sender, EventArgs e)
    {
        _httpService.SendMessage(_httpService.GetMainEndpoint() + UserConfigurationManager.Get("Refresh"));
        LoadDevices();
    }

    private void _btnStatics_Click(object sender, EventArgs e)
    {
        var statisticsExplorer = new StatisticsExplorer();
        statisticsExplorer.SetDevices(_loadedDevices, _httpService);
        statisticsExplorer.Show();
    }

    private void _btnAccount_Click(object sender, EventArgs e)
    {
        var accountsDialog = new AccountOptionsDialog();
        accountsDialog.SetService(_httpService);
        accountsDialog.Show();
    }

    private void _btnConfiguration_Click(object sender, EventArgs e)
    {
        var configurationDialog = new ConfigurationDialog();
        configurationDialog.Show();
    }
}