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
        _btnConfiguration = new Button();
        _btnAccount = new Button();
        _deviceCardsPanel = new Controls.DeviceCardsPanel();
        _btnRefresh = new Button();
        _btnStatistics = new Button();
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
        _lblDateTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblDateTime.AutoSize = true;
        _lblDateTime.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblDateTime.Location = new Point(559, 9);
        _lblDateTime.Name = "_lblDateTime";
        _lblDateTime.Size = new Size(173, 25);
        _lblDateTime.TabIndex = 0;
        _lblDateTime.Text = "01:01:01 01.01.0001";
        // 
        // _btnConfiguration
        // 
        _btnConfiguration.Location = new Point(12, 9);
        _btnConfiguration.Name = "_btnConfiguration";
        _btnConfiguration.Size = new Size(108, 25);
        _btnConfiguration.TabIndex = 1;
        _btnConfiguration.Text = "Configuration";
        _btnConfiguration.UseVisualStyleBackColor = true;
        _btnConfiguration.Click += _btnConfiguration_Click;
        // 
        // _btnAccount
        // 
        _btnAccount.Location = new Point(240, 9);
        _btnAccount.Name = "_btnAccount";
        _btnAccount.Size = new Size(108, 25);
        _btnAccount.TabIndex = 2;
        _btnAccount.Text = "Account";
        _btnAccount.UseVisualStyleBackColor = true;
        _btnAccount.Click += _btnAccount_Click;
        // 
        // _deviceCardsPanel
        // 
        _deviceCardsPanel.AutoSize = true;
        _deviceCardsPanel.Location = new Point(12, 40);
        _deviceCardsPanel.MaximumSize = new Size(1222, 800);
        _deviceCardsPanel.MinimumSize = new Size(500, 300);
        _deviceCardsPanel.Name = "_deviceCardsPanel";
        _deviceCardsPanel.Service = null;
        _deviceCardsPanel.Size = new Size(721, 680);
        _deviceCardsPanel.TabIndex = 5;
        // 
        // _btnRefresh
        // 
        _btnRefresh.Location = new Point(354, 9);
        _btnRefresh.Name = "_btnRefresh";
        _btnRefresh.Size = new Size(108, 25);
        _btnRefresh.TabIndex = 6;
        _btnRefresh.Text = "Refresh";
        _btnRefresh.UseVisualStyleBackColor = true;
        _btnRefresh.Click += _btnRefresh_Click;
        // 
        // _btnStatistics
        // 
        _btnStatistics.Location = new Point(126, 9);
        _btnStatistics.Name = "_btnStatistics";
        _btnStatistics.Size = new Size(108, 25);
        _btnStatistics.TabIndex = 7;
        _btnStatistics.Text = "Statistics";
        _btnStatistics.UseVisualStyleBackColor = true;
        _btnStatistics.Click += _btnStatics_Click;
        // 
        // MainOverview
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(744, 731);
        Controls.Add(_btnStatistics);
        Controls.Add(_btnRefresh);
        Controls.Add(_deviceCardsPanel);
        Controls.Add(_btnAccount);
        Controls.Add(_btnConfiguration);
        Controls.Add(_lblDateTime);
        Name = "MainOverview";
        SizeGripStyle = SizeGripStyle.Hide;
        Text = "MainOverview";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Timer _updater;
    private Label _lblDateTime;
    private Button _btnConfiguration;
    private Button _btnAccount;
    private Controls.DeviceCardsPanel _deviceCardsPanel;
    private Button _btnRefresh;
    private Button _btnStatistics;
}