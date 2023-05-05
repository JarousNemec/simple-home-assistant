using System.Windows.Forms;

namespace SimpleHomeAssistantUi.Controls;

public partial class PropertyControl : UserControl
{
    public PropertyControl()
    {
        InitializeComponent();
    }

    public void Set(string name, string value)
    {
        _lblName.Text = name;
        _txtValue.Text = value;
    }

    public new delegate void Update(string name, string newValue);

    public event Update? OnUpdate;

    private void _btnUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtValue.Text))
            MessageBox.Show("Property value need to be set");
        else
            OnUpdate?.Invoke(_lblName.Text, _txtValue.Text);
    }
}