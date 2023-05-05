using System.ComponentModel;

namespace SimpleHomeAssistantUi.Forms;

partial class ConfigurationDialog
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _pnlConfig = new Panel();
        SuspendLayout();
        // 
        // _pnlConfig
        // 
        _pnlConfig.BackColor = SystemColors.ActiveCaption;
        _pnlConfig.Location = new Point(12, 12);
        _pnlConfig.Name = "_pnlConfig";
        _pnlConfig.Size = new Size(470, 569);
        _pnlConfig.TabIndex = 0;
        // 
        // ConfigurationDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(494, 593);
        Controls.Add(_pnlConfig);
        Name = "ConfigurationDialog";
        Text = "ConfigurationDialog";
        ResumeLayout(false);
    }

    #endregion

    private Panel _pnlConfig;
}