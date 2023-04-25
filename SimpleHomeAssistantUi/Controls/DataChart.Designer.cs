using System.ComponentModel;

namespace SimpleHomeAssistantUi.Controls;

partial class DataChart
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
        _pnlChart = new Panel();
        SuspendLayout();
        // 
        // _pnlChart
        // 
        _pnlChart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _pnlChart.BorderStyle = BorderStyle.FixedSingle;
        _pnlChart.Location = new Point(0, 0);
        _pnlChart.Name = "_pnlChart";
        _pnlChart.Size = new Size(325, 266);
        _pnlChart.TabIndex = 0;
        // 
        // DataChart
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_pnlChart);
        Name = "DataChart";
        Size = new Size(325, 266);
        ResumeLayout(false);
    }

    #endregion

    private Panel _pnlChart;
}