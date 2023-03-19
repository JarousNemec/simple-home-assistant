using System.ComponentModel;

namespace SimpleHomeAssistantUi.Controls;

partial class SwitchCard
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
        _lblFriendlyName = new Label();
        _picDeviceIcon = new PictureBox();
        label2 = new Label();
        _lblIpAddress = new Label();
        _btnStateSwitch = new Button();
        _btnMore = new Button();
        ((ISupportInitialize)_picDeviceIcon).BeginInit();
        SuspendLayout();
        // 
        // _lblFriendlyName
        // 
        _lblFriendlyName.BorderStyle = BorderStyle.FixedSingle;
        _lblFriendlyName.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblFriendlyName.Location = new Point(14, 10);
        _lblFriendlyName.Name = "_lblFriendlyName";
        _lblFriendlyName.Size = new Size(245, 30);
        _lblFriendlyName.TabIndex = 0;
        _lblFriendlyName.Text = "frienlyName";
        _lblFriendlyName.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _picDeviceIcon
        // 
        _picDeviceIcon.Location = new Point(14, 43);
        _picDeviceIcon.Name = "_picDeviceIcon";
        _picDeviceIcon.Size = new Size(245, 132);
        _picDeviceIcon.SizeMode = PictureBoxSizeMode.Zoom;
        _picDeviceIcon.TabIndex = 1;
        _picDeviceIcon.TabStop = false;
        // 
        // label2
        // 
        label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        label2.Location = new Point(14, 178);
        label2.Name = "label2";
        label2.Size = new Size(36, 37);
        label2.TabIndex = 2;
        label2.Text = "IP:";
        label2.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblIpAddress
        // 
        _lblIpAddress.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblIpAddress.Location = new Point(56, 178);
        _lblIpAddress.Name = "_lblIpAddress";
        _lblIpAddress.Size = new Size(203, 37);
        _lblIpAddress.TabIndex = 3;
        _lblIpAddress.Text = "-----";
        _lblIpAddress.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _btnStateSwitch
        // 
        _btnStateSwitch.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _btnStateSwitch.Location = new Point(14, 218);
        _btnStateSwitch.Name = "_btnStateSwitch";
        _btnStateSwitch.Size = new Size(245, 40);
        _btnStateSwitch.TabIndex = 4;
        _btnStateSwitch.Text = "On";
        _btnStateSwitch.UseVisualStyleBackColor = true;
        // 
        // _btnMore
        // 
        _btnMore.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _btnMore.Location = new Point(14, 264);
        _btnMore.Name = "_btnMore";
        _btnMore.Size = new Size(245, 40);
        _btnMore.TabIndex = 5;
        _btnMore.Text = "More";
        _btnMore.UseVisualStyleBackColor = true;
        // 
        // SwitchCard
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(_btnMore);
        Controls.Add(_btnStateSwitch);
        Controls.Add(_lblIpAddress);
        Controls.Add(label2);
        Controls.Add(_picDeviceIcon);
        Controls.Add(_lblFriendlyName);
        Name = "SwitchCard";
        Size = new Size(273, 318);
        ((ISupportInitialize)_picDeviceIcon).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label _lblFriendlyName;
    private PictureBox _picDeviceIcon;
    private Label label2;
    private Label _lblIpAddress;
    private Button _btnStateSwitch;
    private Button _btnMore;
}