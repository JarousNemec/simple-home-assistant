using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantServer.Models;
using SimpleHomeAssistantUi.Interfaces;
using SimpleHomeAssistantUi.Services;

namespace SimpleHomeAssistantUi.Controls;

public partial class DeviceCardsPanel : UserControl
{
    public HttpService Service { get; set; }
    public DeviceCardsPanel()
    {
        InitializeComponent();
    }

    public void LoadDevices(List<Device> devices)
    {
        _pnlCards.Controls.Clear();
        foreach (var device in devices)
        {
            var card = CreateCurrentCard(device);
            if (card == null)
                return;
            
            _pnlCards.Controls.Add(card);
            Invalidate();
        }
    }

    private const int TOP_MARGIN = 16;
    private const int SIDE_MARGIN = 32;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if(_pnlCards.Controls.Count == 0)return;
        var isEnoughSpace = ((_pnlCards.Controls.Count + 1) * SIDE_MARGIN + _pnlCards.Controls.Count * _pnlCards.Controls[0].Width) < Width;
        for (var i = 0; i < _pnlCards.Controls.Count; i++)
        {
            var control = _pnlCards.Controls[i];
            var lineCapacity = Width / control.Width;
            if (!isEnoughSpace)
            {
                var margin = (Width - lineCapacity * control.Width) / (lineCapacity + 1);
                control.Location = new Point(margin + (control.Width + margin) * (i % lineCapacity),
                    TOP_MARGIN + (control.Height + TOP_MARGIN) * (i / lineCapacity));
            }
            else
            {
                control.Location = new Point(SIDE_MARGIN + (control.Width + SIDE_MARGIN) * i,
                    TOP_MARGIN + (control.Height + TOP_MARGIN) * (i / lineCapacity));
            }
            
        }
    }

    private UserControl CreateCurrentCard(Device device)
    {
        switch (device.Module)
        {
            case 1:
            case 8:
            case 9:
            {
                var card = new SwitchCard();
                card.LoadInfo(device);
                card.Service = Service;
                return card;
            }

            case 5:
            case 39:
            {
                var card = new DoubleSwitchCard();
                card.LoadInfo(device);
                card.Service = Service;
                return card;
            }

            case 999:
            {
                var card = new SensorCard();
                card.LoadInfo(device);
                card.Service = Service;
                return card;
            }

            default:
                return null;
        }
    }

    private void DeviceCardsPanel_SizeChanged(object sender, EventArgs e)
    {
        Invalidate();
    }
}