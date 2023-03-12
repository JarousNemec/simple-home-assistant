using System.Globalization;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi;

public partial class MainOverview : Form
{
    private HttpService _httpService;
    private const string GETALLDEVICESURL = "http://127.0.0.1:10002/alldevices";
    public MainOverview()
    {
        InitializeComponent();
        _httpService = new HttpService();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        _updater_Tick(sender, e);
        var devices = _httpService.DownloadJsonObject(GETALLDEVICESURL);
        _deviceCardsPanel.Load(devices);
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
}