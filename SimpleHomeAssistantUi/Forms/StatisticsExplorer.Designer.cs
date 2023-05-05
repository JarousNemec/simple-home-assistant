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
        _radioWeekView = new RadioButton();
        _radioDayView = new RadioButton();
        _btnViewDataLogTable = new Button();
        _btnExportChart = new Button();
        label1 = new Label();
        _lblPowerConsumtion = new Label();
        SuspendLayout();
        // 
        // choseDeviceControl
        // 
        choseDeviceControl.Location = new Point(12, 12);
        choseDeviceControl.Name = "choseDeviceControl";
        choseDeviceControl.Size = new Size(245, 529);
        choseDeviceControl.TabIndex = 0;
        // 
        // _deviceStatisticsChart
        // 
        _deviceStatisticsChart.Location = new Point(273, 77);
        _deviceStatisticsChart.Name = "_deviceStatisticsChart";
        _deviceStatisticsChart.Size = new Size(742, 504);
        _deviceStatisticsChart.TabIndex = 1;
        // 
        // _radioYearView
        // 
        _radioYearView.AutoSize = true;
        _radioYearView.Location = new Point(502, 50);
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
        _radioMonthView.Location = new Point(570, 50);
        _radioMonthView.Name = "_radioMonthView";
        _radioMonthView.Size = new Size(61, 19);
        _radioMonthView.TabIndex = 3;
        _radioMonthView.TabStop = true;
        _radioMonthView.Text = "Month";
        _radioMonthView.UseVisualStyleBackColor = true;
        _radioMonthView.CheckedChanged += _radioMonthView_CheckedChanged;
        // 
        // _radioWeekView
        // 
        _radioWeekView.AutoSize = true;
        _radioWeekView.Location = new Point(658, 50);
        _radioWeekView.Name = "_radioWeekView";
        _radioWeekView.Size = new Size(54, 19);
        _radioWeekView.TabIndex = 4;
        _radioWeekView.Text = "Week";
        _radioWeekView.UseVisualStyleBackColor = true;
        _radioWeekView.CheckedChanged += _radioWeekView_CheckedChanged;
        // 
        // _radioDayView
        // 
        _radioDayView.AutoSize = true;
        _radioDayView.Location = new Point(729, 50);
        _radioDayView.Name = "_radioDayView";
        _radioDayView.Size = new Size(45, 19);
        _radioDayView.TabIndex = 5;
        _radioDayView.Text = "Day";
        _radioDayView.UseVisualStyleBackColor = true;
        _radioDayView.CheckedChanged += _radioDayView_CheckedChanged;
        // 
        // _btnViewDataLogTable
        // 
        _btnViewDataLogTable.Location = new Point(12, 547);
        _btnViewDataLogTable.Name = "_btnViewDataLogTable";
        _btnViewDataLogTable.Size = new Size(245, 34);
        _btnViewDataLogTable.TabIndex = 6;
        _btnViewDataLogTable.Text = "View all data in table";
        _btnViewDataLogTable.UseVisualStyleBackColor = true;
        _btnViewDataLogTable.Click += _btnViewDataLogTable_Click;
        // 
        // _btnExportChart
        // 
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
        label1.Location = new Point(435, 12);
        label1.Name = "label1";
        label1.Size = new Size(251, 25);
        label1.TabIndex = 8;
        label1.Text = "Set power consumtion[W]:";
        // 
        // _lblPowerConsumtion
        // 
        _lblPowerConsumtion.AutoSize = true;
        _lblPowerConsumtion.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        _lblPowerConsumtion.Location = new Point(681, 12);
        _lblPowerConsumtion.Name = "_lblPowerConsumtion";
        _lblPowerConsumtion.Size = new Size(60, 25);
        _lblPowerConsumtion.TabIndex = 9;
        _lblPowerConsumtion.Text = "------";
        // 
        // StatisticsExplorer
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1027, 593);
        Controls.Add(_lblPowerConsumtion);
        Controls.Add(label1);
        Controls.Add(_btnExportChart);
        Controls.Add(_btnViewDataLogTable);
        Controls.Add(_radioDayView);
        Controls.Add(_radioWeekView);
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
    private RadioButton _radioWeekView;
    private RadioButton _radioDayView;
    private Button _btnViewDataLogTable;
    private Button _btnExportChart;
    private Label label1;
    private Label _lblPowerConsumtion;
}