using System.ComponentModel;

namespace SimpleHomeAssistantUi.Controls;

partial class SensorCard
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
        _btnMore = new Button();
        _picDeviceIcon = new PictureBox();
        ((ISupportInitialize)_picDeviceIcon).BeginInit();
        SuspendLayout();
        // 
        // _lblFriendlyName
        // 
        _lblFriendlyName.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblFriendlyName.Location = new Point(12, 9);
        _lblFriendlyName.Name = "_lblFriendlyName";
        _lblFriendlyName.Size = new Size(247, 30);
        _lblFriendlyName.TabIndex = 0;
        _lblFriendlyName.Text = "frienlyName";
        _lblFriendlyName.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // label2
        // 
        label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        label2.Location = new Point(14, 177);
        label2.Name = "label2";
        label2.Size = new Size(34, 34);
        label2.TabIndex = 1;
        label2.Text = "IP:";
        label2.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblIpAddress
        // 
        _lblIpAddress.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblIpAddress.Location = new Point(54, 177);
        _lblIpAddress.Name = "_lblIpAddress";
        _lblIpAddress.Size = new Size(205, 34);
        _lblIpAddress.TabIndex = 2;
        _lblIpAddress.Text = "-----";
        _lblIpAddress.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _btnMore
        // 
        _btnMore.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _btnMore.Location = new Point(14, 267);
        _btnMore.Name = "_btnMore";
        _btnMore.Size = new Size(245, 41);
        _btnMore.TabIndex = 3;
        _btnMore.Text = "More";
        _btnMore.UseVisualStyleBackColor = true;
        // 
        // _picDeviceIcon
        // 
        _picDeviceIcon.Location = new Point(14, 42);
        _picDeviceIcon.Name = "_picDeviceIcon";
        _picDeviceIcon.Size = new Size(245, 132);
        _picDeviceIcon.TabIndex = 4;
        _picDeviceIcon.TabStop = false;
        // 
        // SensorCard
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(_picDeviceIcon);
        Controls.Add(_btnMore);
        Controls.Add(_lblIpAddress);
        Controls.Add(label2);
        Controls.Add(_lblFriendlyName);
        Name = "SensorCard";
        Size = new Size(273, 318);
        ((ISupportInitialize)_picDeviceIcon).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label _lblFriendlyName;
    private Label label2;
    private Label _lblIpAddress;
    private Button _btnMore;
    private PictureBox _picDeviceIcon;
}