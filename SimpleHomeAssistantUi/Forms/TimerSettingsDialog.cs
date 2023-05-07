using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;

namespace SimpleHomeAssistantUi.Forms;

public partial class TimerSettingsDialog : Form
{
    private List<TimerSettings> _timers;

    public TimerSettingsDialog()
    {
        InitializeComponent();
    }

    public void Setup(List<TimerSettings> timers)
    {
        _timers = timers;
        foreach (var timer in timers)
        {
            _comboTimers.Items.Add(timer.TimerName);
        }

        _comboTimers.SelectedIndex = 0;
        DisplayTimersData();
    }

    public delegate void Save(TimerSettings settings);

    public event Save OnSave;

    private void _btnSave_Click(object sender, EventArgs e)
    {
        var timer = _timers[_comboTimers.SelectedIndex];
        var days = string.Empty;
        for (int i = 0; i < _chckListDays.Items.Count; i++)
        {
            if (_chckListDays.GetItemChecked(i))
            {
                days += "1";
                continue;
            }

            days += "0";
        }

        var settings = new TimerSettings(timer.TimerName, timer.Topic, _chckEnable.Checked, _comboMode.SelectedIndex,
            new TimeSpan((int)_numTimeHours.Value, (int)_numTimeMinutes.Value, 0), (int)_numWindow.Value, days,
            _chckRepeat.Checked, _comboOutput.SelectedIndex, (TimerActions)_comboAction.SelectedIndex);
        OnSave.Invoke(settings);
        Close();
    }

    private void _comboTimers_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayTimersData();
    }

    private void DisplayTimersData()
    {
        var timer = _timers[_comboTimers.SelectedIndex];
        _chckEnable.Checked = timer.IsEnabled;
        _comboMode.SelectedIndex = timer.Mode;
        _numTimeHours.Value = timer.ActionTime.Hours;
        _numTimeMinutes.Value = timer.ActionTime.Minutes;
        _numWindow.Value = timer.Window;
        for (var i = 0; i < timer.Days.Length; i++)
        {
            if (timer.Days[i] == '0' || timer.Days[i] == '-')
            {
                _chckListDays.SetItemChecked(i, false);
                continue;
            }

            _chckListDays.SetItemChecked(i, true);
        }

        _chckRepeat.Checked = timer.IsRepeating;
        _comboOutput.SelectedIndex = timer.Output-1;
        _comboAction.SelectedIndex = timer.Action;
    }
}