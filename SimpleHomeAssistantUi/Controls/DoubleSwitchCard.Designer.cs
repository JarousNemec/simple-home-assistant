using System.ComponentModel;

namespace SimpleHomeAssistantUi.Controls;

partial class DoubleSwitchCard
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
        label2 = new Label();
        _lblIpAddress = new Label();
        _picDeviceIcon = new PictureBox();
        _btnStateSwitch1 = new Button();
        _btnStateSwitch2 = new Button();
        _btnMore = new Button();
        ((ISupportInitialize)_picDeviceIcon).BeginInit();
        SuspendLayout();
        // 
        // _lblFriendlyName
        // 
        _lblFriendlyName.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblFriendlyName.Location = new Point(14, 12);
        _lblFriendlyName.Name = "_lblFriendlyName";
        _lblFriendlyName.Size = new Size(247, 23);
        _lblFriendlyName.TabIndex = 0;
        _lblFriendlyName.Text = "frienlyName";
        _lblFriendlyName.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // label2
        // 
        label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        label2.Location = new Point(14, 182);
        label2.Name = "label2";
        label2.Size = new Size(34, 23);
        label2.TabIndex = 1;
        label2.Text = "IP:";
        label2.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblIpAddress
        // 
        _lblIpAddress.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblIpAddress.Location = new Point(41, 182);
        _lblIpAddress.Name = "_lblIpAddress";
        _lblIpAddress.Size = new Size(218, 23);
        _lblIpAddress.TabIndex = 2;
        _lblIpAddress.Text = "-----";
        _lblIpAddress.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _picDeviceIcon
        // 
        _picDeviceIcon.Location = new Point(14, 38);
        _picDeviceIcon.Name = "_picDeviceIcon";
        _picDeviceIcon.Size = new Size(245, 132);
        _picDeviceIcon.TabIndex = 3;
        _picDeviceIcon.TabStop = false;
        // 
        // _btnStateSwitch1
        // 
        _btnStateSwitch1.Location = new Point(14, 225);
        _btnStateSwitch1.Name = "_btnStateSwitch1";
        _btnStateSwitch1.Size = new Size(122, 37);
        _btnStateSwitch1.TabIndex = 4;
        _btnStateSwitch1.Text = "On";
        _btnStateSwitch1.UseVisualStyleBackColor = true;
        // 
        // _btnStateSwitch2
        // 
        _btnStateSwitch2.Location = new Point(142, 225);
        _btnStateSwitch2.Name = "_btnStateSwitch2";
        _btnStateSwitch2.Size = new Size(117, 37);
        _btnStateSwitch2.TabIndex = 5;
        _btnStateSwitch2.Text = "On";
        _btnStateSwitch2.UseVisualStyleBackColor = true;
        // 
        // _btnMore
        // 
        _btnMore.Location = new Point(14, 268);
        _btnMore.Name = "_btnMore";
        _btnMore.Size = new Size(245, 41);
        _btnMore.TabIndex = 6;
        _btnMore.Text = "More";
        _btnMore.UseVisualStyleBackColor = true;
        // 
        // DoubleSwitchCard
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BorderStyle = BorderStyle.Fixed3D;
        Controls.Add(_btnMore);
        Controls.Add(_btnStateSwitch2);
        Controls.Add(_btnStateSwitch1);
        Controls.Add(_picDeviceIcon);
        Controls.Add(_lblIpAddress);
        Controls.Add(label2);
        Controls.Add(_lblFriendlyName);
        Name = "DoubleSwitchCard";
        Size = new Size(271, 316);
        ((ISupportInitialize)_picDeviceIcon).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label _lblFriendlyName;
    private Label label2;
    private Label _lblIpAddress;
    private PictureBox _picDeviceIcon;
    private Button _btnStateSwitch1;
    private Button _btnStateSwitch2;
    private Button _btnMore;
}