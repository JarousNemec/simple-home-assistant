using System.Windows.Forms;
using SimpleHomeAssistantUi.Models;

namespace SimpleHomeAssistantUi.Controls;

public partial class DataChart : UserControl
{
    public string Yax { get; set; }
    public string Xax { get; set; }
    public Point ChartZero { get; set; }
    public ChartViewModes Mode { get; set; }
    private Dictionary<int, int> _marks;
    public DataChart()
    {
        InitializeComponent();
        
    }

    public void SetData(string yax, string xax, ChartViewModes mode)
    {
        Yax = yax;
        Xax = xax;
        ChartZero = new Point(axesMargin, Height - axesMargin);
        Mode = mode;
        _pnlChart.Invalidate();
    }

    private void _pnlChart_Paint(object sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        DrawAxes(g);
    }

    private const int axesMargin = 30;
    private void DrawAxes(Graphics g)
    {
        //Y
        g.DrawLine(Pens.Black, new Point(axesMargin, 0), new Point(axesMargin, Height));

        //X
        g.DrawLine(Pens.Black, new Point(0, Height - axesMargin), new Point(Width, Height - axesMargin));
        
        //Ytext
        g.DrawString(Yax, new Font(FontFamily.GenericSansSerif, 10 ), Brushes.Black, 5,5);
        
        //Xtext
        g.DrawString(Xax, new Font(FontFamily.GenericSansSerif, 10 ), Brushes.Black, Width-20,Height-25);

        switch (Mode)
        {
            case ChartViewModes.Day:
            {
                DrawXMarks(g, 24);
            }
                break;
            case ChartViewModes.Week:
                DrawXMarks(g, 7);
                break;
            case ChartViewModes.Month:
                //todo: parametrize
                DrawXMarks(g, DateTime.DaysInMonth(2023, 5));
                break;
            case ChartViewModes.Year:
                DrawXMarks(g, 12);
                break;
        }
    }

    private void DrawXMarks(Graphics g, int count)
    {
        _marks = new Dictionary<int, int>();
        var space = (Width - ChartZero.X-axesMargin) / (count-1);
        for (int i = 0; i < count; i++)
        {
            _marks.Add(i, ChartZero.X+space*i);
            g.FillEllipse(Brushes.Gold, _marks[i]-3, ChartZero.Y-3, 6,6);
        }
    }
}