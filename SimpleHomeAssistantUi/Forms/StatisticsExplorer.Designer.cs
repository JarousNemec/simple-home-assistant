using System.ComponentModel;

namespace SimpleHomeAssistantUi.Forms;

partial class StatisticsExplorer
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
        choseDeviceControl = new Controls.ChoseDeviceStatisticsControl();
        _deviceStatisticsChart = new Controls.DataChart();
        _radioYearView = new RadioButton();
        _radioMonthView = new RadioButton();
        _radioDayView = new RadioButton();
        _btnExportChart = new Button();
        label1 = new Label();
        _lblPowerConsumtion = new Label();
        _btnMoveForward = new Button();
        _btnMoveBack = new Button();
        _lblRecordInterval = new Label();
        SuspendLayout();
        // 
        // choseDeviceControl
        // 
        choseDeviceControl.Location = new Point(12, 12);
        choseDeviceControl.Name = "choseDeviceControl";
        choseDeviceControl.Size = new Size(245, 569);
        choseDeviceControl.TabIndex = 0;
        // 
        // _deviceStatisticsChart
        // 
        _deviceStatisticsChart.ChartZero = new Point(0, 0);
        _deviceStatisticsChart.Location = new Point(273, 103);
        _deviceStatisticsChart.Mode = Models.ChartViewModes.Day;
        _deviceStatisticsChart.Name = "_deviceStatisticsChart";
        _deviceStatisticsChart.Size = new Size(742, 478);
        _deviceStatisticsChart.TabIndex = 1;
        _deviceStatisticsChart.Xax = null;
        _deviceStatisticsChart.Yax = null;
        _deviceStatisticsChart.Paint += _deviceStatisticsChart_Paint;
        // 
        // _radioYearView
        // 
        _radioYearView.AutoSize = true;
        _radioYearView.Location = new Point(516, 50);
        _radioYearView.Name = "_radioYearView";
        _radioYearView.Size = new Size(47, 19);
        _radioYearView.TabIndex = 2;
        _radioYearView.Text = "Year";
        _radioYearView.UseVisualStyleBackColor = true;
        _radioYearView.CheckedChanged += _radioYearView_CheckedChanged;
        // 
        // _radioMonthView
        // 
        _radioMonthView.AutoSize = true;
        _radioMonthView.Checked = true;
        _radioMonthView.Location = new Point(599, 50);
        _radioMonthView.Name = "_radioMonthView";
        _radioMonthView.Size = new Size(61, 19);
        _radioMonthView.TabIndex = 3;
        _radioMonthView.TabStop = true;
        _radioMonthView.Text = "Month";
        _radioMonthView.UseVisualStyleBackColor = true;
        _radioMonthView.CheckedChanged += _radioMonthView_CheckedChanged;
        // 
        // _radioDayView
        // 
        _radioDayView.AutoSize = true;
        _radioDayView.Location = new Point(696, 50);
        _radioDayView.Name = "_radioDayView";
        _radioDayView.Size = new Size(45, 19);
        _radioDayView.TabIndex = 5;
        _radioDayView.Text = "Day";
        _radioDayView.UseVisualStyleBackColor = true;
        _radioDayView.CheckedChanged += _radioDayView_CheckedChanged;
        // 
        // _btnExportChart
        // 
        _btnExportChart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnExportChart.Location = new Point(942, 50);
        _btnExportChart.Name = "_btnExportChart";
        _btnExportChart.Size = new Size(73, 23);
        _btnExportChart.TabIndex = 7;
        _btnExportChart.Text = "Export";
        _btnExportChart.UseVisualStyleBackColor = true;
        _btnExportChart.Click += _btnExportChart_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        label1.Location = new Point(458, 12);
        label1.Name = "label1";
        label1.Size = new Size(262, 25);
        label1.TabIndex = 8;
        label1.Text = "Set power consumtion[Wh]:";
        // 
        // _lblPowerConsumtion
        // 
        _lblPowerConsumtion.AutoSize = true;
        _lblPowerConsumtion.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblPowerConsumtion.Location = new Point(725, 12);
        _lblPowerConsumtion.Name = "_lblPowerConsumtion";
        _lblPowerConsumtion.Size = new Size(60, 25);
        _lblPowerConsumtion.TabIndex = 9;
        _lblPowerConsumtion.Text = "------";
        // 
        // _btnMoveForward
        // 
        _btnMoveForward.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnMoveForward.Location = new Point(968, 74);
        _btnMoveForward.Name = "_btnMoveForward";
        _btnMoveForward.Size = new Size(49, 23);
        _btnMoveForward.TabIndex = 10;
        _btnMoveForward.Text = ">";
        _btnMoveForward.UseVisualStyleBackColor = true;
        _btnMoveForward.Click += _btnMoveForward_Click;
        // 
        // _btnMoveBack
        // 
        _btnMoveBack.Location = new Point(273, 74);
        _btnMoveBack.Name = "_btnMoveBack";
        _btnMoveBack.Size = new Size(49, 23);
        _btnMoveBack.TabIndex = 11;
        _btnMoveBack.Text = "<";
        _btnMoveBack.UseVisualStyleBackColor = true;
        _btnMoveBack.Click += _btnMoveBack_Click;
        // 
        // _lblRecordInterval
        // 
        _lblRecordInterval.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
        _lblRecordInterval.Location = new Point(458, 74);
        _lblRecordInterval.Name = "_lblRecordInterval";
        _lblRecordInterval.Size = new Size(327, 23);
        _lblRecordInterval.TabIndex = 12;
        _lblRecordInterval.Text = "---------------------";
        _lblRecordInterval.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // StatisticsExplorer
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1026, 593);
        Controls.Add(_lblRecordInterval);
        Controls.Add(_btnMoveBack);
        Controls.Add(_btnMoveForward);
        Controls.Add(_lblPowerConsumtion);
        Controls.Add(label1);
        Controls.Add(_btnExportChart);
        Controls.Add(_radioDayView);
        Controls.Add(_radioMonthView);
        Controls.Add(_radioYearView);
        Controls.Add(_deviceStatisticsChart);
        Controls.Add(choseDeviceControl);
        Name = "StatisticsExplorer";
        Text = "StatisticsExplorer";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.ChoseDeviceStatisticsControl choseDeviceControl;
    private Controls.DataChart _deviceStatisticsChart;
    private RadioButton _radioYearView;
    private RadioButton _radioMonthView;
    private RadioButton _radioDayView;
    private Button _btnExportChart;
    private Label label1;
    private Label _lblPowerConsumtion;
    private Button _btnMoveForward;
    private Button _btnMoveBack;
    private Label _lblRecordInterval;
}