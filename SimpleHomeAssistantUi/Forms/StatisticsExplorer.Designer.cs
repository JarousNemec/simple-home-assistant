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
        _deviceStatiscicsDataView = new DataGridView();
        label1 = new Label();
        _numPowerConcumtion = new NumericUpDown();
        ((ISupportInitialize)_deviceStatiscicsDataView).BeginInit();
        ((ISupportInitialize)_numPowerConcumtion).BeginInit();
        SuspendLayout();
        // 
        // choseDeviceControl
        // 
        choseDeviceControl.Location = new Point(12, 12);
        choseDeviceControl.Name = "choseDeviceControl";
        choseDeviceControl.Size = new Size(245, 593);
        choseDeviceControl.TabIndex = 0;
        // 
        // _deviceStatisticsChart
        // 
        _deviceStatisticsChart.Location = new Point(273, 12);
        _deviceStatisticsChart.Name = "_deviceStatisticsChart";
        _deviceStatisticsChart.Size = new Size(742, 401);
        _deviceStatisticsChart.TabIndex = 1;
        // 
        // _deviceStatiscicsDataView
        // 
        _deviceStatiscicsDataView.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _deviceStatiscicsDataView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _deviceStatiscicsDataView.Location = new Point(273, 430);
        _deviceStatiscicsDataView.Name = "_deviceStatiscicsDataView";
        _deviceStatiscicsDataView.RowTemplate.Height = 25;
        _deviceStatiscicsDataView.Size = new Size(742, 216);
        _deviceStatiscicsDataView.TabIndex = 2;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        label1.AutoSize = true;
        label1.Location = new Point(12, 618);
        label1.Name = "label1";
        label1.Size = new Size(170, 15);
        label1.TabIndex = 3;
        label1.Text = "Device power consumtion [W]:";
        // 
        // _numPowerConcumtion
        // 
        _numPowerConcumtion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        _numPowerConcumtion.Location = new Point(181, 616);
        _numPowerConcumtion.Maximum = new decimal(new int[] { 25000, 0, 0, 0 });
        _numPowerConcumtion.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        _numPowerConcumtion.Name = "_numPowerConcumtion";
        _numPowerConcumtion.Size = new Size(76, 23);
        _numPowerConcumtion.TabIndex = 4;
        _numPowerConcumtion.TextAlign = HorizontalAlignment.Right;
        _numPowerConcumtion.Value = new decimal(new int[] { 1, 0, 0, 0 });
        _numPowerConcumtion.ValueChanged += numericUpDown1_ValueChanged;
        // 
        // StatisticsExplorer
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1027, 659);
        Controls.Add(_numPowerConcumtion);
        Controls.Add(label1);
        Controls.Add(_deviceStatiscicsDataView);
        Controls.Add(_deviceStatisticsChart);
        Controls.Add(choseDeviceControl);
        Name = "StatisticsExplorer";
        Text = "StatisticsExplorer";
        ((ISupportInitialize)_deviceStatiscicsDataView).EndInit();
        ((ISupportInitialize)_numPowerConcumtion).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.ChoseDeviceStatisticsControl choseDeviceControl;
    private Controls.DataChart _deviceStatisticsChart;
    private DataGridView _deviceStatiscicsDataView;
    private Label label1;
    private NumericUpDown _numPowerConcumtion;
}