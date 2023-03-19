﻿using System.Text.Json.Nodes;
using System.Windows.Forms;
using SimpleHomeAssistantUi.Interfaces;

namespace SimpleHomeAssistantUi.Controls;

public partial class DeviceCardsPanel : UserControl
{
    public DeviceCardsPanel()
    {
        InitializeComponent();
    }

    public void LoadDevices(JsonNode devices)
    {
        var infos = devices.AsArray();
        for (var i = 0; i < infos.Count; i++)
        {
            if (infos[i] != null)
            {
                var card = CreateCurrentCard(infos[i]);
                if (card == null)
                    return;
                _pnlCards.Controls.Add(card);
                Invalidate();
            }
        }
    }

    private const int TOP_MARGIN = 16;
    private const int SIDE_MARGIN = 32;
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        for (int i = 0; i < _pnlCards.Controls.Count; i++)
        {
            var control = _pnlCards.Controls[i];
            int lineCapacity = Width / control.Width;
            int margin = (Width - lineCapacity * control.Width) / (lineCapacity + 1);
            control.Location = new Point(margin + (control.Width + margin) * (i % lineCapacity),
                TOP_MARGIN + (+(control.Height + TOP_MARGIN) * (i / lineCapacity)));
        }
    }

    private UserControl CreateCurrentCard(JsonNode? device)
    {
        switch (device?["Status"]?["Module"]?.ToString())
        {
            case "1":
            case "8":
            case "9":
                {
                    var card = new SwitchCard();
                    card.LoadInfo(device);
                    return card;
                }

            case "5":
            case "39":
                {
                    var card = new DoubleSwitchCard();
                    card.LoadInfo(device);
                    return card;
                }

            case "999":
                {
                    var card = new SensorCard();
                    card.LoadInfo(device);
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