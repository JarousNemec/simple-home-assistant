using System.Configuration;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi;

public partial class MainOverview : Form
{
    private HttpService _httpService;
    public MainOverview()
    {
        InitializeComponent();
        _httpService = new HttpService();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        _updater_Tick(sender, e);
        LoadDevices();
    }

    private void _updater_Tick(object sender, EventArgs e)
    {
        var time = DateTime.Now;
        _lblDateTime.Text = $@"{time.Hour}:{CorrectFormat(time.Minute)}:{CorrectFormat(time.Second)}  {time.Day}.{CorrectFormat(time.Month)}.{time.Year}";
    }

    private static string CorrectFormat(int number)
    {
        return number.ToString().Length == 1 ? $"0{number.ToString()}" : number.ToString();
    }

    private void LoadDevices()
    {
        var config = ConfigurationManager.AppSettings;
        var devices = _httpService.DownloadJsonObject<Device[]>(config.Get("MainEndpoint")+config.Get("AllDevices"));
        if (devices != null) _deviceCardsPanel.LoadDevices(devices.ToList());
    }
    private void _btnRefresh_Click(object sender, EventArgs e)
    {
        LoadDevices();
    }
}