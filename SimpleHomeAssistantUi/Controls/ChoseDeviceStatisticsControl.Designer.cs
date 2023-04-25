using System.ComponentModel;

namespace SimpleHomeAssistantUi.Controls;

partial class ChoseDeviceStatisticsControl
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
        _pnlDevices = new Panel();
        SuspendLayout();
        // 
        // _pnlDevices
        // 
        _pnlDevices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _pnlDevices.BorderStyle = BorderStyle.FixedSingle;
        _pnlDevices.Location = new Point(0, 0);
        _pnlDevices.Name = "_pnlDevices";
        _pnlDevices.Size = new Size(238, 406);
        _pnlDevices.TabIndex = 0;
        // 
        // ChoseDeviceStatisticsControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_pnlDevices);
        Name = "ChoseDeviceStatisticsControl";
        Size = new Size(238, 406);
        ResumeLayout(false);
    }

    #endregion

    private Panel _pnlDevices;
}