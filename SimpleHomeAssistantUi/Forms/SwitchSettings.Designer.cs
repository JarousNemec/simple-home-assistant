using System.ComponentModel;

namespace SimpleHomeAssistantUi.Forms;

partial class SwitchSettings
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
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        _txtFrienlyName = new TextBox();
        _txtDeviceName = new TextBox();
        _txtTopic = new TextBox();
        _btnFriendlyNameUpdate = new Button();
        _btnDeviceNameUpdate = new Button();
        _btnTopicUpdate = new Button();
        label4 = new Label();
        _btnPowerConsumptionUpdate = new Button();
        _lblName = new Label();
        _btnSetTimes = new Button();
        _numPowerConsumption = new NumericUpDown();
        ((ISupportInitialize)_numPowerConsumption).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(37, 45);
        label1.Name = "label1";
        label1.Size = new Size(85, 15);
        label1.TabIndex = 0;
        label1.Text = "Friendly name:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(44, 96);
        label2.Name = "label2";
        label2.Size = new Size(78, 15);
        label2.TabIndex = 1;
        label2.Text = "Device name:";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(84, 145);
        label3.Name = "label3";
        label3.Size = new Size(38, 15);
        label3.TabIndex = 2;
        label3.Text = "Topic:";
        // 
        // _txtFrienlyName
        // 
        _txtFrienlyName.Location = new Point(128, 41);
        _txtFrienlyName.Name = "_txtFrienlyName";
        _txtFrienlyName.Size = new Size(165, 23);
        _txtFrienlyName.TabIndex = 3;
        // 
        // _txtDeviceName
        // 
        _txtDeviceName.Location = new Point(128, 92);
        _txtDeviceName.Name = "_txtDeviceName";
        _txtDeviceName.Size = new Size(165, 23);
        _txtDeviceName.TabIndex = 4;
        // 
        // _txtTopic
        // 
        _txtTopic.Location = new Point(128, 141);
        _txtTopic.Name = "_txtTopic";
        _txtTopic.Size = new Size(165, 23);
        _txtTopic.TabIndex = 5;
        // 
        // _btnFriendlyNameUpdate
        // 
        _btnFriendlyNameUpdate.Location = new Point(300, 41);
        _btnFriendlyNameUpdate.Name = "_btnFriendlyNameUpdate";
        _btnFriendlyNameUpdate.Size = new Size(75, 23);
        _btnFriendlyNameUpdate.TabIndex = 6;
        _btnFriendlyNameUpdate.Text = "Update";
        _btnFriendlyNameUpdate.UseVisualStyleBackColor = true;
        _btnFriendlyNameUpdate.Click += _btnFriendlyNameUpdate_Click;
        // 
        // _btnDeviceNameUpdate
        // 
        _btnDeviceNameUpdate.Location = new Point(300, 92);
        _btnDeviceNameUpdate.Name = "_btnDeviceNameUpdate";
        _btnDeviceNameUpdate.Size = new Size(75, 23);
        _btnDeviceNameUpdate.TabIndex = 7;
        _btnDeviceNameUpdate.Text = "Update";
        _btnDeviceNameUpdate.UseVisualStyleBackColor = true;
        _btnDeviceNameUpdate.Click += _btnDeviceNameUpdate_Click;
        // 
        // _btnTopicUpdate
        // 
        _btnTopicUpdate.Location = new Point(300, 141);
        _btnTopicUpdate.Name = "_btnTopicUpdate";
        _btnTopicUpdate.Size = new Size(75, 23);
        _btnTopicUpdate.TabIndex = 8;
        _btnTopicUpdate.Text = "Update";
        _btnTopicUpdate.UseVisualStyleBackColor = true;
        _btnTopicUpdate.Click += _btnTopicUpdate_Click;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(12, 196);
        label4.Name = "label4";
        label4.Size = new Size(117, 15);
        label4.TabIndex = 9;
        label4.Text = "Power consumption:";
        // 
        // _btnPowerConsumptionUpdate
        // 
        _btnPowerConsumptionUpdate.Location = new Point(300, 191);
        _btnPowerConsumptionUpdate.Name = "_btnPowerConsumptionUpdate";
        _btnPowerConsumptionUpdate.Size = new Size(75, 23);
        _btnPowerConsumptionUpdate.TabIndex = 11;
        _btnPowerConsumptionUpdate.Text = "Update";
        _btnPowerConsumptionUpdate.UseVisualStyleBackColor = true;
        _btnPowerConsumptionUpdate.Click += _btnPowerConsumptionUpdate_Click;
        // 
        // _lblName
        // 
        _lblName.AutoSize = true;
        _lblName.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblName.Location = new Point(12, 9);
        _lblName.Name = "_lblName";
        _lblName.Size = new Size(44, 25);
        _lblName.TabIndex = 12;
        _lblName.Text = "----";
        // 
        // _btnSetTimes
        // 
        _btnSetTimes.Location = new Point(122, 247);
        _btnSetTimes.Name = "_btnSetTimes";
        _btnSetTimes.Size = new Size(131, 45);
        _btnSetTimes.TabIndex = 13;
        _btnSetTimes.Text = "Set timers";
        _btnSetTimes.UseVisualStyleBackColor = true;
        _btnSetTimes.Click += _btnSetTimers_Click;
        // 
        // _numPowerConsumption
        // 
        _numPowerConsumption.Location = new Point(129, 193);
        _numPowerConsumption.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        _numPowerConsumption.Name = "_numPowerConsumption";
        _numPowerConsumption.Size = new Size(165, 23);
        _numPowerConsumption.TabIndex = 14;
        // 
        // SwitchSettings
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(387, 304);
        Controls.Add(_numPowerConsumption);
        Controls.Add(_btnSetTimes);
        Controls.Add(_lblName);
        Controls.Add(_btnPowerConsumptionUpdate);
        Controls.Add(label4);
        Controls.Add(_btnTopicUpdate);
        Controls.Add(_btnDeviceNameUpdate);
        Controls.Add(_btnFriendlyNameUpdate);
        Controls.Add(_txtTopic);
        Controls.Add(_txtDeviceName);
        Controls.Add(_txtFrienlyName);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Name = "SwitchSettings";
        Text = "SwitchSettings";
        ((ISupportInitialize)_numPowerConsumption).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private Label label3;
    private TextBox _txtFrienlyName;
    private TextBox _txtDeviceName;
    private TextBox _txtTopic;
    private Button _btnFriendlyNameUpdate;
    private Button _btnDeviceNameUpdate;
    private Button _btnTopicUpdate;
    private Label label4;
    private Button _btnPowerConsumptionUpdate;
    private Label _lblName;
    private Button _btnSetTimes;
    private NumericUpDown _numPowerConsumption;
}