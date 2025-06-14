#region

using FSFormControls.DBGraph;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public partial class DBGraphics
    {
        #region t_typeGraph enum

        public enum t_typeGraph
        {
            Line,
            Bar,
            Pie
        }

        #endregion

        public bool isDraw;


        public BarTypes m_AlignmentBar = BarTypes.HorizontalLeft;
        public LineTypes m_AlignmentLine = LineTypes.Horizontal;
        public Color m_Color;
        public bool m_GraphBorder;
        public int m_Group;
        public string m_KeyTitle = "";
        public string m_PieTitle = "";
        public string m_Title = "";
        public t_typeGraph m_typeGraph = t_typeGraph.Bar;
        public BarPieceCollection m_ValuesBar = new BarPieceCollection();
        public LinePlotCollection m_ValuesLine = new LinePlotCollection();
        public LinePlotCollection m_ValuesLine2 = new LinePlotCollection();
        public LinePlotCollection m_ValuesLine3 = new LinePlotCollection();
        public PiePieceCollection m_ValuesPie = new PiePieceCollection();

        public Render renderer = new Render();

        public DBGraphics()
        {
            InitializeComponent();

            // events handled by SplitContainer1_SizeChanged

            SplitContainer1.SizeChanged += SplitContainer1_SizeChanged;
            // events handled by SplitContainer1_SplitterMoved

            SplitContainer1.SplitterMoved += SplitContainer1_SplitterMoved;
        }

        public int Group
        {
            get { return m_Group; }
            set { m_Group = value; }
        }

        public t_typeGraph TypeGraph
        {
            get { return m_typeGraph; }
            set { m_typeGraph = value; }
        }

        public PiePieceCollection ValuesPie
        {
            get { return m_ValuesPie; }
            set { m_ValuesPie = value; }
        }

        public LinePlotCollection ValuesLine
        {
            get { return m_ValuesLine; }
            set { m_ValuesLine = value; }
        }

        public LinePlotCollection ValuesLine2
        {
            get { return m_ValuesLine2; }
            set { m_ValuesLine2 = value; }
        }

        public LinePlotCollection ValuesLine3
        {
            get { return m_ValuesLine3; }
            set { m_ValuesLine3 = value; }
        }

        public BarPieceCollection ValuesBar
        {
            get { return m_ValuesBar; }
            set { m_ValuesBar = value; }
        }

        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        public string KeyTitle
        {
            get { return m_KeyTitle; }
            set { m_KeyTitle = value; }
        }

        public string PieTitle
        {
            get { return m_PieTitle; }
            set { m_PieTitle = value; }
        }

        public BarTypes AlignmentBar
        {
            get { return m_AlignmentBar; }
            set { m_AlignmentBar = value; }
        }

        public LineTypes AlignmentLine
        {
            get { return m_AlignmentLine; }
            set { m_AlignmentLine = value; }
        }

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public bool GraphBorder
        {
            get { return m_GraphBorder; }
            set { m_GraphBorder = value; }
        }

        public void Clear()
        {
            ValuesBar.Clear();
            ValuesLine.Clear();
            ValuesLine2.Clear();
            ValuesLine3.Clear();
            ValuesPie.Clear();

            PictureBox1.Image = null;
            PictureBox2.Image = null;
        }

        public void Draw()
        {
            renderer.bMap = renderer.GetBitmap(new Size(PictureBox1.Width, PictureBox1.Height));
            var g = Graphics.FromImage(renderer.bMap);
            g.Clear(Color.White);

            renderer.maxValue = ValuesBar.MaxValue > ValuesLine.MaxValue ? ValuesBar.MaxValue : ValuesLine.MaxValue;
            renderer.maxValue = renderer.maxValue > ValuesLine2.MaxValue ? renderer.maxValue : ValuesLine2.MaxValue;
            renderer.maxValue = renderer.maxValue > ValuesLine3.MaxValue ? renderer.maxValue : ValuesLine3.MaxValue;

            switch (TypeGraph)
            {
                case t_typeGraph.Bar:
                    if (ValuesBar.Count == 0) return;

                    var barChart = new BarChart();
                    barChart.BarSliceCollection = ValuesBar;
                    barChart.KeyTitle = KeyTitle;
                    barChart.ChartTitle = Title;
                    barChart.Group = Group;
                    barChart.ChartPieTitle = PieTitle;

                    barChart.KeySize = new Size(PictureBox2.Width, PictureBox2.Height);
                    barChart.ImageSize = new Size(PictureBox1.Width, PictureBox1.Height);
                    barChart.AutoSize = false;
                    barChart.Color = m_Color;
                    barChart.Alignment = m_AlignmentBar;
                    barChart.GraphBorder = m_GraphBorder;

                    PictureBox1.Image = renderer.DrawChart(barChart);
                    PictureBox2.Image = renderer.DrawKey(barChart);
                    break;
                case t_typeGraph.Line:
                    if (ValuesLine.Count == 0) return;

                    var lineChart = new LineChart();
                    lineChart.LinePlotCollection = ValuesLine;
                    lineChart.KeyTitle = KeyTitle;
                    lineChart.ChartTitle = Title;
                    lineChart.Group = Group;
                    lineChart.ChartPieTitle = PieTitle;

                    lineChart.KeySize = new Size(PictureBox2.Width, PictureBox2.Height);
                    lineChart.ImageSize = new Size(PictureBox1.Width, PictureBox1.Height);
                    lineChart.AutoSize = false;
                    lineChart.Color = m_Color;
                    lineChart.Alignment = m_AlignmentLine;
                    lineChart.GraphBorder = m_GraphBorder;

                    PictureBox1.Image = renderer.DrawChart(lineChart);
                    PictureBox2.Image = renderer.DrawKey(lineChart);
                    break;
                case t_typeGraph.Pie:
                    if (ValuesPie.Count == 0) return;

                    var pieChart = new PieChart();
                    pieChart.PieSliceCollection = ValuesPie;
                    pieChart.KeyTitle = KeyTitle;
                    pieChart.ChartTitle = Title;
                    pieChart.Group = Group;
                    pieChart.ChartPieTitle = PieTitle;

                    pieChart.KeySize = new Size(PictureBox2.Width, PictureBox2.Height);
                    pieChart.ImageSize = new Size(PictureBox1.Width, PictureBox1.Height);

                    pieChart.Diameter = PictureBox1.Width < PictureBox1.Height
                        ? PictureBox1.Width - 20
                        : PictureBox1.Height - 20;
                    pieChart.Thickness = 15;

                    pieChart.AutoSize = false;
                    pieChart.Color = m_Color;
                    pieChart.GraphBorder = m_GraphBorder;

                    PictureBox1.Image = renderer.DrawChart(pieChart);
                    PictureBox2.Image = renderer.DrawKey(pieChart);
                    break;
            }


            if (ValuesLine.Count != 0)
            {
                var lineChart = new LineChart();
                lineChart.LinePlotCollection = ValuesLine;
                lineChart.KeyTitle = KeyTitle;
                lineChart.ChartTitle = Title;
                lineChart.Group = Group;
                lineChart.ChartPieTitle = PieTitle;

                lineChart.KeySize = new Size(PictureBox2.Width, PictureBox2.Height);
                lineChart.ImageSize = new Size(PictureBox1.Width, PictureBox1.Height);
                lineChart.AutoSize = false;
                lineChart.Color = m_Color;
                lineChart.Alignment = m_AlignmentLine;
                lineChart.GraphBorder = m_GraphBorder;

                PictureBox1.Image = renderer.DrawChart(lineChart);
            }

            if (ValuesLine2.Count != 0)
            {
                var lineChart = new LineChart();
                lineChart.LinePlotCollection = ValuesLine2;
                lineChart.KeyTitle = KeyTitle;
                lineChart.ChartTitle = Title;
                lineChart.Group = Group;
                lineChart.ChartPieTitle = PieTitle;

                lineChart.KeySize = new Size(PictureBox2.Width, PictureBox2.Height);
                lineChart.ImageSize = new Size(PictureBox1.Width, PictureBox1.Height);
                lineChart.AutoSize = false;
                lineChart.Color = m_Color;
                lineChart.Alignment = m_AlignmentLine;
                lineChart.GraphBorder = m_GraphBorder;

                PictureBox1.Image = renderer.DrawChart(lineChart);
            }

            if (ValuesLine3.Count != 0)
            {
                var lineChart = new LineChart();
                lineChart.LinePlotCollection = ValuesLine3;
                lineChart.KeyTitle = KeyTitle;
                lineChart.ChartTitle = Title;
                lineChart.Group = Group;
                lineChart.ChartPieTitle = PieTitle;

                lineChart.KeySize = new Size(PictureBox2.Width, PictureBox2.Height);
                lineChart.ImageSize = new Size(PictureBox1.Width, PictureBox1.Height);
                lineChart.AutoSize = false;
                lineChart.Color = m_Color;
                lineChart.Alignment = m_AlignmentLine;
                lineChart.GraphBorder = m_GraphBorder;

                PictureBox1.Image = renderer.DrawChart(lineChart);
            }

            isDraw = true;
        }


        private void SplitContainer1_SizeChanged(object sender, EventArgs e)
        {
            if (isDraw) Draw();
        }


        private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (isDraw) Draw();
        }
    }
}