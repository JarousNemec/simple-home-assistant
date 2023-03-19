using System.ComponentModel;

namespace SimpleHomeAssistantUi.Controls;

partial class DeviceCardsPanel
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _pnlCards = new Panel();
        SuspendLayout();
        // 
        // _pnlCards
        // 
        _pnlCards.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _pnlCards.AutoScroll = true;
        _pnlCards.BorderStyle = BorderStyle.FixedSingle;
        _pnlCards.Location = new Point(0, 0);
        _pnlCards.Name = "_pnlCards";
        _pnlCards.Size = new Size(1118, 644);
        _pnlCards.TabIndex = 0;
        // 
        // DeviceCardsPanel
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_pnlCards);
        Name = "DeviceCardsPanel";
        Size = new Size(1118, 644);
        SizeChanged += DeviceCardsPanel_SizeChanged;
        ResumeLayout(false);
    }

    #endregion

    private Panel _pnlCards;
}