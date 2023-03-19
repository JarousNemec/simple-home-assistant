namespace SimpleHomeAssistantUi;

partial class MainOverview
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        _updater = new System.Windows.Forms.Timer(components);
        _lblDateTime = new Label();
        _btnSettings = new Button();
        _btnLogin = new Button();
        _tbLog = new TextBox();
        _deviceCardsPanel = new Controls.DeviceCardsPanel();
        _btnRefresh = new Button();
        SuspendLayout();
        // 
        // _updater
        // 
        _updater.Enabled = true;
        _updater.Interval = 1000;
        _updater.Tick += _updater_Tick;
        // 
        // _lblDateTime
        // 
        _lblDateTime.AutoSize = true;
        _lblDateTime.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblDateTime.Location = new Point(12, 9);
        _lblDateTime.Name = "_lblDateTime";
        _lblDateTime.Size = new Size(173, 25);
        _lblDateTime.TabIndex = 0;
        _lblDateTime.Text = "01:01:01 01.01.0001";
        // 
        // _btnSettings
        // 
        _btnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnSettings.Location = new Point(320, 12);
        _btnSettings.Name = "_btnSettings";
        _btnSettings.Size = new Size(108, 25);
        _btnSettings.TabIndex = 1;
        _btnSettings.Text = "Settings";
        _btnSettings.UseVisualStyleBackColor = true;
        // 
        // _btnLogin
        // 
        _btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnLogin.Location = new Point(434, 12);
        _btnLogin.Name = "_btnLogin";
        _btnLogin.Size = new Size(108, 25);
        _btnLogin.TabIndex = 2;
        _btnLogin.Text = "Log In / Out";
        _btnLogin.UseVisualStyleBackColor = true;
        // 
        // _tbLog
        // 
        _tbLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _tbLog.BorderStyle = BorderStyle.FixedSingle;
        _tbLog.Location = new Point(12, 576);
        _tbLog.Multiline = true;
        _tbLog.Name = "_tbLog";
        _tbLog.ReadOnly = true;
        _tbLog.Size = new Size(530, 143);
        _tbLog.TabIndex = 4;
        _tbLog.WordWrap = false;
        // 
        // _deviceCardsPanel
        // 
        _deviceCardsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _deviceCardsPanel.Location = new Point(12, 43);
        _deviceCardsPanel.Name = "_deviceCardsPanel";
        _deviceCardsPanel.Size = new Size(530, 527);
        _deviceCardsPanel.TabIndex = 5;
        // 
        // _btnRefresh
        // 
        _btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnRefresh.Location = new Point(206, 12);
        _btnRefresh.Name = "_btnRefresh";
        _btnRefresh.Size = new Size(108, 25);
        _btnRefresh.TabIndex = 6;
        _btnRefresh.Text = "Refresh";
        _btnRefresh.UseVisualStyleBackColor = true;
        _btnRefresh.Click += _btnRefresh_Click;
        // 
        // MainOverview
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(554, 731);
        Controls.Add(_btnRefresh);
        Controls.Add(_deviceCardsPanel);
        Controls.Add(_tbLog);
        Controls.Add(_btnLogin);
        Controls.Add(_btnSettings);
        Controls.Add(_lblDateTime);
        MinimumSize = new Size(570, 770);
        Name = "MainOverview";
        Text = "MainOverview";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Timer _updater;
    private Label _lblDateTime;
    private Button _btnSettings;
    private Button _btnLogin;
    private TextBox _tbLog;
    private Controls.DeviceCardsPanel _deviceCardsPanel;
    private Button _btnRefresh;
}