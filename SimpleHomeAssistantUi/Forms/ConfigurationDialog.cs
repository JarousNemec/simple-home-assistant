using System.Windows.Forms;
using SimpleHomeAssistantUi.Controls;
using SimpleHomeAssistantUi.Managers;

namespace SimpleHomeAssistantUi.Forms;

public partial class ConfigurationDialog : Form
{
    public ConfigurationDialog()
    {
        InitializeComponent();
        Load();
    }

    private const int PropertyControlMargin = 8;
    private void Load()
    {
        var config = UserConfigurationManager.GetAllProps();
        for (int i = 0; i < config.Count; i++)
        {
            var control = new PropertyControl();
            control.Location = new Point(PropertyControlMargin,
                PropertyControlMargin + (i * (control.Height + PropertyControlMargin)));
            var name =config.ElementAt(i).Key;
            var value = config.ElementAt(i).Value;
            control.Set(name, value);
            control.OnUpdate += ControlOnOnUpdate;
            _pnlConfig.Controls.Add(control);
        }
    }

    private void ControlOnOnUpdate(string name, string newValue)
    {
        UserConfigurationManager.Set(name, newValue);
    }
}