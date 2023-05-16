using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SimpleHomeAssistantUi.Models;

namespace SimpleHomeAssistantUi.Controls;

public partial class DataChart : UserControl
{
    public string Yax { get; set; }
    public string Xax { get; set; }
    public Point ChartZero { get; set; }
    public ChartViewModes Mode { get; set; }
    private List<Point> _points;
    private Dictionary<int, double> _data;

    public DataChart()
    {
        InitializeComponent();
    }

    public void SetData(Dictionary<int, double> data)
    {
        _data = data;
        CalculatePoints();
        _pnlChart.Invalidate();
    }

    public void SetInit(string yax, string xax)
    {
        Yax = yax;
        Xax = xax;
        ChartZero = new Point(axesMargin, _pnlChart.Height - axesMargin);
        _pnlChart.Invalidate();
    }

    private void _pnlChart_Paint(object sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        DrawAxes(g);
    }

    private const int axesMargin = 30;

    public Panel GetChartPanel()
    {
        return _pnlChart;
    }
    private void DrawAxes(Graphics g)
    {
        
        g.DrawLine(Pens.Black, new Point(axesMargin, 0), new Point(axesMargin, _pnlChart.Height));

        //X
        g.DrawLine(Pens.Black, new Point(0, _pnlChart.Height - axesMargin), new Point(_pnlChart.Width, _pnlChart.Height - axesMargin));

        //Ytext
        g.DrawString(Yax, new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, 5, 5);

        //Xtext
        g.DrawString(Xax, new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, _pnlChart.Width - 20, _pnlChart.Height - 25);

        DrawChart(g);
        //Y
    }

    private const double SCALE = 10;

    private void CalculatePoints()
    {
        if (_data == null) return;
        _points = new List<Point>();
        var count = _data.Count;
        var space = (_pnlChart.Width - ChartZero.X - axesMargin) / (count - 1);
        for (int i = 0; i < count; i++)
        {
            _points.Add(new Point(ChartZero.X + i * space, (int)(ChartZero.Y - _data[i + 1] / SCALE)));
        }
    }

    private void DrawChart(Graphics g)
    {
        if (_points == null) return;
        for (int i = 0; i < _points.Count; i++)
        {
            var message = Math.Round(_data[i + 1], 0).ToString();
            if (message != "0")
            {
                g.FillRectangle(Brushes.Red, _points[i].X - 4, _points[i].Y, 8, ChartZero.Y - _points[i].Y);
            }

            g.DrawString(message, new Font(FontFamily.GenericSansSerif, 10),
                Brushes.Black, _points[i].X, _points[i].Y - 14);
            g.DrawString((i + 1).ToString(), new Font(FontFamily.GenericSansSerif, 10),
                Brushes.Black, _points[i].X, ChartZero.Y + 10);
            g.FillEllipse(Brushes.DarkRed, _points[i].X - 3, _points[i].Y - 3, 6, 6);
        }
    }
}