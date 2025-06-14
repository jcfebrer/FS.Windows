#region

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using FSGraphics;
using FSLibrary;

#endregion

namespace FSFormControls.DBGraph
{
    #region '"Bar"' 

    public class BarChart : GraphBase
    {
        public BarPieceCollection BarSliceCollection = new BarPieceCollection();

        public BarChart()
        {
            base.ChartType = ChartType.Bar;
        }

        public BarChart(BarPieceCollection BarPieceCollection)
        {
            base.ChartType = ChartType.Bar;
            BarSliceCollection = BarPieceCollection;
        }

        public BarTypes Alignment { get; set; } = BarTypes.HorizontalLeft;

        public new ChartType ChartType => base.ChartType;
    }

    public class BarSlice : BaseChunk
    {
        public BarSlice(decimal decValue, Color cColor, string sKeyName)
        {
            Value = decValue;
            Color = cColor;
            KeyName = sKeyName;
            DrawKey = true;
        }

        public BarSlice(decimal decValue, Color cColor, string sKeyName, bool bDrawKey)
        {
            Value = decValue;
            Color = cColor;
            KeyName = sKeyName;
            DrawKey = bDrawKey;
        }
    }

    public class BarPieceCollection : BaseChunkCollection
    {
        public BarPieceCollection()
        {
            List.Clear();
        }

        public int Add(BarSlice oBarPiece)
        {
            return base.Add(oBarPiece);
        }
    }

    #endregion

    #region '"Line"' 

    public class LineChart : GraphBase
    {
        public LinePlotCollection LinePlotCollection = new LinePlotCollection();

        public LineChart()
        {
            ChartType = ChartType.Line;
        }

        public LineChart(Size ImgSize)
        {
            ChartType = ChartType.Line;
            ImageSize = ImgSize;
        }

        public LineChart(bool ImgAutoSize)
        {
            ChartType = ChartType.Line;
            AutoSize = ImgAutoSize;
        }

        public LineChart(bool ImgAutoSize, LinePlotCollection linePlotCollection)
        {
            ChartType = ChartType.Line;
            AutoSize = ImgAutoSize;
            LinePlotCollection = linePlotCollection;
        }

        public LineChart(LinePlotCollection linePlotCollection)
        {
            ChartType = ChartType.Line;
            LinePlotCollection = linePlotCollection;
        }

        public LineTypes Alignment { get; set; } = LineTypes.Horizontal;

        public Color LineColor { get; set; } = Color.Black;
    }


    public class LineSlice : BaseChunk
    {
        internal Point Point = new Point(0, 0);

        public LineSlice(decimal decValue, Color cColor, string sKeyName)
        {
            Value = decValue;
            Color = cColor;
            KeyName = sKeyName;
            DrawKey = true;
        }

        public LineSlice(decimal decValue, Color cColor, string sKeyName, bool bDrawKey)
        {
            Value = decValue;
            Color = cColor;
            KeyName = sKeyName;
            DrawKey = bDrawKey;
        }
    }


    public class LinePlotCollection : BaseChunkCollection
    {
        public LinePlotCollection()
        {
            List.Clear();
        }

        public int Add(LineSlice oPiePiece)
        {
            return base.Add(oPiePiece);
        }
    }

    #endregion

    #region '"Pie"' 

    public class PieChart : GraphBase
    {
        public float Diameter;
        public PiePieceCollection PieSliceCollection = new PiePieceCollection();
        public float Thickness;

        public PieChart()
        {
            ChartType = ChartType.Pie;
        }

        public PieChart(Size ImgSize)
        {
            ChartType = ChartType.Pie;
            ImageSize = ImgSize;
        }

        public PieChart(bool ImgAutoSize)
        {
            ChartType = ChartType.Pie;
            AutoSize = ImgAutoSize;
        }

        public PieChart(bool ImgAutoSize, PiePieceCollection PiePieceCollection)
        {
            ChartType = ChartType.Pie;
            AutoSize = ImgAutoSize;
            PieSliceCollection = PiePieceCollection;
        }

        public PieChart(Size ImgSize, PiePieceCollection PiePieceCollection)
        {
            ChartType = ChartType.Pie;
            PieSliceCollection = PiePieceCollection;
        }
    }


    public class PieSlice : BaseChunk
    {
        private decimal m_sweepAngle;


        public PieSlice(decimal decValue, Color cColor, string sKeyName)
        {
            Value = decValue;
            Color = cColor;
            KeyName = sKeyName;
            DrawKey = true;
        }

        public PieSlice(decimal decValue, Color cColor, string sKeyName, bool bDrawKey)
        {
            Value = decValue;
            Color = cColor;
            KeyName = sKeyName;
            DrawKey = bDrawKey;
        }

        internal decimal PiecePercent { get; set; }

        internal decimal SweepAngle
        {
            get { return 360 * (PiecePercent / 100); }
            set { m_sweepAngle = value; }
        }
    }


    public class PiePieceCollection : BaseChunkCollection
    {
        public PiePieceCollection()
        {
            List.Clear();
        }

        public int Add(PieSlice oPiePiece)
        {
            return base.Add(oPiePiece);
        }

        internal void CalcPercent()
        {
            var lTvalue = TotalValue;
            foreach (PieSlice oBaseChunk in List)
                try
                {
                    oBaseChunk.PiecePercent = decimal.Round(oBaseChunk.Value / lTvalue * 100, 2);
                }
                catch
                {
                }
        }

        internal void CalcPercent(bool CalcIsPercent)
        {
            var lTvalue = TotalValue;
            foreach (PieSlice oBaseChunk in List)
                try
                {
                    oBaseChunk.PiecePercent = oBaseChunk.Value;
                }
                catch
                {
                }
        }
    }

    #endregion

    #region '"Base Classes"' 

    public abstract class BaseChunk : textStruct
    {
        public string KeyName { get; set; }

        public decimal Value { get; set; }

        public bool DrawKey { get; set; }
    }


    public abstract class GraphBase : textStruct
    {
        public string KeyFontName { get; set; } = "Arial";

        public int KeyFontSize { get; set; } = 10;

        public FontStyle KeyFontStyle { get; set; } = FontStyle.Regular;

        public Color KeyBackColor { get; set; } = Color.White;

        public string KeyTitle { get; set; } = "";

        public string ChartTitle { get; set; } = "";

        public int Group { get; set; }

        public string ChartPieTitle { get; set; } = "";

        public Size KeySize { get; set; } = new Size(0, 0);

        public Color KeyTitleColor { get; set; } = Color.Black;

        public string KeyTitleFontName { get; set; } = "Arial";

        public int KeyTitleFontSize { get; set; } = 10;

        public FontStyle KeyTitleFontStyle { get; set; } = FontStyle.Regular;

        public bool AutoSize { get; set; }

        public bool DisplayUnits { get; set; } = true;

        public Size ImageSize { get; set; } = new Size(100, 100);

        public ChartValueType ValueType { get; set; } = ChartValueType.ValueTotal;

        public ChartType ChartType { get; set; } = ChartType.Pie;

        public bool GraphBorder { get; set; } = true;

        protected internal Rectangle GraphRect { get; set; } = new Rectangle(0, 0, 0, 0);

        public BarTypes GraphAlign { get; set; } = BarTypes.HorizontalLeft;
    }

    public abstract class textStruct
    {
        public Color Color { get; set; } = Color.Black;
    }


    public class BaseChunkCollection : CollectionBase
    {
        public int MaxKeyNameLength
        {
            get
            {
                var m_MaxValue = 0;

                foreach (BaseChunk oBaseChunk in List)
                    if (m_MaxValue < oBaseChunk.KeyName.Length)
                        m_MaxValue = oBaseChunk.KeyName.Length;
                return m_MaxValue;
            }
        }

        public decimal MaxValue
        {
            get
            {
                var m_MaxValue = decimal.MinValue;
                foreach (var oBaseChunk in List)
                    if (m_MaxValue < ((BaseChunk) oBaseChunk).Value)
                        m_MaxValue = ((BaseChunk) oBaseChunk).Value;
                return m_MaxValue;
            }
        }

        public decimal MinValue
        {
            get
            {
                var m_MinValue = decimal.MaxValue;
                foreach (var oBaseChunk in List)
                    if (m_MinValue > ((BaseChunk) oBaseChunk).Value)
                        m_MinValue = ((BaseChunk) oBaseChunk).Value;
                return m_MinValue;
            }
        }

        public decimal TotalValue { get; private set; }

        public int Add(object Chunk)
        {
            TotalValue += ((BaseChunk) Chunk).Value;
            return List.Add(Chunk);
        }

        public bool Remove(int Index)
        {
            try
            {
                TotalValue -= ((BaseChunk) List[Index]).Value;
                List.RemoveAt(Index);
            }
            catch
            {
            }

            return true;
        }
    }

    public struct Padding
    {
        public Padding(int Padding, int Spacing, int CellBorder)
        {
            CellPadding = Padding;
            CellSpacing = Spacing;
            Border = CellBorder;
        }

        public int CellPadding { get; set; }

        public int CellSpacing { get; set; }

        public int Border { get; set; }
    }

    public struct MathInfo
    {
        private int m_Divisor;

        public long Divisor
        {
            get { return m_Divisor; }
            set { m_Divisor = Convert.ToInt32(value); }
        }

        public string Multiplier { get; set; }

        public long Max { get; set; }
    }

    #endregion

    #region '"Base Enums"' 

    public enum BarTypes
    {
        HorizontalLeft = 0,
        HorizontalRight = 1,
        VerticalTop = 2,
        VerticalBottom = 3
    }

    public enum LineTypes
    {
        Horizontal = 1,
        Vertical = 2
    }

    public enum ChartValueType
    {
        ValuePercent = 1,
        ValueTotal = 2
    }

    public enum ChartType
    {
        Pie = 0,
        Bar = 1,
        Line = 2
    }

    #endregion

    #region '"Render"' 

    public class Render
    {
        public Bitmap bMap;
        public Padding GraphPadding = new Padding(4, 4, 2);

        public decimal maxValue;

        public Image DrawChart(GraphBase graph)
        {
            switch (graph.ChartType)
            {
                case ChartType.Bar:
                    return DrawBarChart((BarChart) graph);
                case ChartType.Line:
                    return DrawLineChart((LineChart) graph);
                case ChartType.Pie:
                    return DrawPieChart((PieChart) graph);

                default:
                    return null;
            }
        }

        public void DrawChart(GraphBase graph, Stream retStream)
        {
            switch (graph.ChartType)
            {
                case ChartType.Bar:
                    DrawBarChart((BarChart) graph, retStream);
                    break;
                case ChartType.Line:
                    DrawLineChart((LineChart) graph, retStream);
                    break;
                case ChartType.Pie:
                    DrawPieChart((PieChart) graph, retStream);
                    break;
            }
        }

        public Image DrawKey(GraphBase Graph)
        {
            Bitmap kMap = null;
            BaseChunkCollection oBaseChunkCollection = null;
            var gBrush = new SolidBrush(Graph.KeyTitleColor);
            var bmSize = new Size();
            var minWidth = 0;

            switch (Graph.ChartType)
            {
                case ChartType.Bar:
                    oBaseChunkCollection = ((BarChart) Graph).BarSliceCollection;
                    break;
                case ChartType.Line:
                    oBaseChunkCollection = ((LineChart) Graph).LinePlotCollection;
                    break;
                case ChartType.Pie:
                    oBaseChunkCollection = ((PieChart) Graph).PieSliceCollection;
                    break;
            }


            if (Graph.KeyTitle.Length > oBaseChunkCollection.MaxKeyNameLength)
                minWidth = Graph.KeyTitle.Length * Graph.KeyFontSize;
            else
                minWidth = oBaseChunkCollection.MaxKeyNameLength * Graph.KeyFontSize;

            if ((Graph.KeySize.Width == 0) | (Graph.KeySize.Height == 0))
            {
                bmSize.Width = minWidth;
                bmSize.Height = oBaseChunkCollection.Count * 20 + 25;
            }
            else
            {
                bmSize.Width = Graph.KeySize.Width;
                bmSize.Height = Graph.KeySize.Height;
            }

            kMap = GetBitmap(bmSize);
            var g = Graphics.FromImage(kMap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Graph.KeyBackColor);

            var titleRectF = new RectangleF(0, 0, bmSize.Width + 12, 25);
            var titleFormat = new StringFormat();
            var titleFont = new Font(Graph.KeyTitleFontName, Graph.KeyTitleFontSize, Graph.KeyTitleFontStyle);
            gBrush.Color = Graph.KeyTitleColor;
            titleFormat.Alignment = StringAlignment.Center;
            titleFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(Graph.KeyTitle, titleFont, gBrush, titleRectF, titleFormat);
            titleFormat.Dispose();
            titleFont.Dispose();

            Font drawFont = null;
            var approximateWidth = 0;
            var gPen = new Pen(Color.Black, 2);
            var keyCount = oBaseChunkCollection.Count;

            var f = 1;
            foreach (BaseChunk bChunk in oBaseChunkCollection)
            {
                if (Graph.Group != 0)
                    if (f > Graph.Group)
                        break;
                if (bChunk.DrawKey)
                    if (bChunk.KeyName != "")
                    {
                        drawFont = new Font(Graph.KeyFontName, Graph.KeyFontSize, Graph.KeyFontStyle);
                        var transTemp0 = bChunk.KeyName;
                        var transTemp1 = bChunk.KeyName;
                        if (transTemp0.Length > approximateWidth) approximateWidth = transTemp1.Length;

                        gPen.Width = 1;
                        gBrush.Color = bChunk.Color;
                        g.FillRectangle(gBrush, 5, keyCount + 24, 10, 10);
                        gPen.Color = Color.Black;
                        g.DrawRectangle(gPen, 5, keyCount + 24, 10, 10);
                        gBrush.Color = Color.Black;
                        g.DrawString(bChunk.KeyName, drawFont, gBrush, 17, keyCount + 21);
                        keyCount += 18;
                        f += 1;
                    }
            }

            return kMap;
        }

        public void DrawKey(GraphBase Graph, Stream retStream)
        {
            var b = (Bitmap) DrawChart(Graph);
            b.Save(retStream, ImageFormat.Jpeg);
            b.Dispose();
        }

        #region '"Private methods"' 

        private Image DrawPieChart(PieChart PieChart)
        {
            if (PieChart.AutoSize) PieChart.ImageSize = AutoSize(PieChart, PieChart.PieSliceCollection);

            PieChart.GraphRect = CalcGraph(PieChart);
            if (PieChart.ValueType == ChartValueType.ValueTotal)
                PieChart.PieSliceCollection.CalcPercent();
            else
                PieChart.PieSliceCollection.CalcPercent(true);

            var lBorder = GraphPadding.Border;
            var totalBorder = lBorder * 3;
            PieChart.GraphRect = new Rectangle(lBorder, lBorder, bMap.Width - totalBorder, bMap.Height - totalBorder);

            var g = Graphics.FromImage(bMap);
            var startAngle = 0.0F;
            var sweepAngle = 0.0F;
            var gBrush = new SolidBrush(PieChart.Color);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            var gPen = new Pen(Color.Black, 1);
            var Diameter = Convert.ToInt32(PieChart.Diameter);
            var thickness = Convert.ToInt32(PieChart.Thickness);
            var pieRect = new Rectangle();

            for (var j = Convert.ToInt32(Diameter * thickness * 0.01F); j >= 0; j += -1)
                foreach (PieSlice oPiePiece in PieChart.PieSliceCollection)
                {
                    sweepAngle = Convert.ToSingle(360 * (Convert.ToDouble(oPiePiece.PiecePercent) / 100));
                    pieRect = new Rectangle(PieChart.GraphRect.X,
                        Convert.ToInt32(PieChart.GraphRect.Y + Convert.ToSingle(j)), Diameter,
                        Diameter);

                    var hBrush = new HatchBrush(HatchStyle.Percent50, oPiePiece.Color);
                    g.FillPie(hBrush, pieRect, startAngle, sweepAngle);
                    startAngle += sweepAngle;
                }

            for (var j = Convert.ToInt32(Diameter * 0.01F); j >= 0; j += -1)
                foreach (PieSlice oPiePiece in PieChart.PieSliceCollection)
                {
                    sweepAngle = Convert.ToSingle(360 * (Convert.ToDouble(oPiePiece.PiecePercent) / 100));
                    pieRect = new Rectangle(PieChart.GraphRect.X,
                        Convert.ToInt32(PieChart.GraphRect.Y + Convert.ToSingle(j)), Diameter,
                        Diameter);

                    gBrush.Color = oPiePiece.Color;

                    g.FillPie(gBrush, pieRect, startAngle, sweepAngle);
                    startAngle += sweepAngle;
                }


            g.Dispose();
            gBrush.Dispose();
            return bMap;
        }

        private void DrawPieChart(PieChart PieChart, Stream retStream)
        {
            var b = (Bitmap) DrawChart(PieChart);
            b.Save(retStream, ImageFormat.Jpeg);
            b.Dispose();
        }


        private Image DrawBarChart(BarChart BarChart)
        {
            if (BarChart.AutoSize) BarChart.ImageSize = AutoSize(BarChart, BarChart.BarSliceCollection);

            var g = Graphics.FromImage(bMap);
            g.SmoothingMode = SmoothingMode.HighSpeed;

            var r = CalcGraph(BarChart);
            var recGrid = new Rectangle(r.X, r.Y + 30, r.Width, r.Height - 60);
            BarChart.GraphRect = recGrid;

            DrawGrid(BarChart, ref g);

            recGrid = new Rectangle(r.X + 30, r.Y + 30, r.Width - 60, r.Height - 60);
            BarChart.GraphRect = recGrid;


            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawVerticalBar(BarChart, ref g);

            switch (BarChart.Alignment)
            {
                case BarTypes.HorizontalLeft:
                    bMap.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    break;
                case BarTypes.HorizontalRight:
                    bMap.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case BarTypes.VerticalBottom:
                    break;
                case BarTypes.VerticalTop:
                    bMap.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
            }


            return bMap;
        }

        private object DrawBarChart(BarChart BarChart, Stream retStream)
        {
            var b = (Bitmap) DrawChart(BarChart);
            b.Save(retStream, ImageFormat.Jpeg);
            b.Dispose();
            return null;
        }


        private Image DrawLineChart(LineChart LineChart)
        {
            if (LineChart.AutoSize) LineChart.ImageSize = AutoSize(LineChart, LineChart.LinePlotCollection);


            var g = Graphics.FromImage(bMap);
            g.SmoothingMode = SmoothingMode.HighQuality;

            var r = CalcGraph(LineChart);
            var rec = new Rectangle(r.X + 30, r.Y + 30, r.Width - 60, r.Height - 60);

            LineChart.GraphRect = rec;

            DrawVerticalPlots(LineChart, ref g);

            switch (LineChart.Alignment)
            {
                case LineTypes.Horizontal:
                    break;
                case LineTypes.Vertical:
                    bMap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }


            return bMap;
        }

        private object DrawLineChart(LineChart LineChart, Stream retStream)
        {
            var b = (Bitmap) DrawChart(LineChart);
            b.Save(retStream, ImageFormat.Jpeg);
            b.Dispose();
            return null;
        }


        private void DrawGrid(GraphBase chart, ref Graphics g)
        {
            var gBrush = new SolidBrush(Color.Gray);

            var gRect = chart.GraphRect;

            var gpen = new Pen(Color.Gray);
            MathInfo mInfo;
            decimal topValue = 0;
            var steps = Convert.ToSingle(0.0);
            var tStep = 15;

            g.DrawRectangle(gpen, gRect);

            topValue = maxValue;
            var l = Convert.ToInt64(topValue);
            mInfo = getMathInfo(ref l);


            steps = Convert.ToSingle(gRect.Height / tStep);

            float aX = gRect.X;
            float aY = gRect.Y;
            float bX = gRect.Right;
            float bY = 0;
            var Line_Value = NumberUtils.NumberDecimal(Convert.ToDouble(topValue) / tStep);
            long iCount = 0;
            var sLineValue = "";

            for (double iChart = aY; iChart <= gRect.Height + aY; iChart += steps)
            {
                aY = Convert.ToSingle(iChart);
                bY = Convert.ToSingle(iChart);

                if (iCount % 2 == 0)
                    gpen = new Pen(Color.Gray, 1);
                else
                    gpen = new Pen(Color.Gray, 1);

                if (chart.DisplayUnits)
                {
                    sLineValue = Math.Round(Convert.ToDouble(topValue) / mInfo.Divisor).ToString();

                    g.DrawString(sLineValue, new Font("Arial", 8, FontStyle.Regular), gBrush, gRect.Left + 5, aY + 3);
                }

                g.DrawLine(gpen, aX, aY, bX, bY);
                iCount += 1;
                topValue -= Line_Value;
                if (iCount > 5000) break;
            }

            var strFormat = new StringFormat(StringFormatFlags.DirectionVertical);

            g.DrawString("Escala (x" + mInfo.Multiplier + ")", new Font("Tahoma", 8, FontStyle.Regular), gBrush,
                gRect.Right - 20, gRect.Top + 30, strFormat);

            if (chart.ChartTitle != "")
                g.DrawString(chart.ChartTitle, new Font("Tahoma", 12, FontStyle.Bold), new SolidBrush(Color.DarkRed),
                    5, 5);

            if (chart.ChartPieTitle != "")
                g.DrawString(chart.ChartPieTitle, new Font("Tahoma", 8, FontStyle.Bold), new SolidBrush(Color.Black),
                    5, gRect.Bottom + 5);
        }


        public void DrawValues(GraphBase chart, ref Graphics g)
        {
            switch (chart.GraphAlign)
            {
                case BarTypes.HorizontalLeft:

                    break;
                case BarTypes.HorizontalRight:

                    break;
                case BarTypes.VerticalBottom:

                    break;
                case BarTypes.VerticalTop:

                    break;
            }
        }

        private void DrawVerticalBar(BarChart BarChart, ref Graphics g)
        {
            var gBrush = new SolidBrush(BarChart.Color);
            var totalSpacing = BarChart.GraphRect.Width -
                               GraphPadding.CellSpacing * (BarChart.BarSliceCollection.Count - 1);
            totalSpacing -= GraphPadding.Border * 2;
            var Width = Convert.ToInt32(totalSpacing / BarChart.BarSliceCollection.Count);
            var Height = 0;
            float bX = BarChart.GraphRect.Left + GraphPadding.Border;
            float bY = BarChart.GraphRect.Bottom;
            var lbarRectangle = new Rectangle();
            var rbarRectangle = new Rectangle();
            var barRectangle = new Rectangle();
            var shadowRect = new Rectangle();
            var f = 1;

            foreach (BarSlice bSlice in BarChart.BarSliceCollection)
            {
                if (bSlice.Value != 0)
                {
                    Height = Convert.ToInt32(BarChart.GraphRect.Height * (bSlice.Value / maxValue));
                    if (Height == 0) Height = 1;
                    bY = BarChart.GraphRect.Bottom - Height;

                    barRectangle = new Rectangle(Convert.ToInt32(bX), Convert.ToInt32(bY), Width - 2, Height);
                    lbarRectangle = new Rectangle(Convert.ToInt32(bX), Convert.ToInt32(bY),
                        Convert.ToInt32(Width / 2), Height);
                    rbarRectangle = new Rectangle(Convert.ToInt32(bX + Width / 2 - 2), Convert.ToInt32(bY),
                        Convert.ToInt32(Width / 2), Height);

                    var lgBrush = new LinearGradientBrush(lbarRectangle,
                        GraphicsUtil.GetLightColor(
                            bSlice.Color, 80),
                        GraphicsUtil.GetDarkColor(
                            bSlice.Color, 55),
                        LinearGradientMode.BackwardDiagonal);
                    var rgBrush = new LinearGradientBrush(rbarRectangle,
                        GraphicsUtil.GetLightColor(
                            bSlice.Color, 80),
                        GraphicsUtil.GetDarkColor(
                            bSlice.Color, 55),
                        LinearGradientMode.ForwardDiagonal);
                    shadowRect = new Rectangle(Convert.ToInt32(bX + 2), Convert.ToInt32(bY - 3), Width, Height + 3);
                    gBrush.Color = Color.LightGray;
                    g.FillRectangle(gBrush, shadowRect);
                    g.FillRectangle(lgBrush, lbarRectangle);
                    g.FillRectangle(rgBrush, rbarRectangle);
                    var gPen = new Pen(GraphicsUtil.GetDarkColor(bSlice.Color, 1), 1);

                    g.DrawRectangle(gPen, barRectangle);
                }

                if (BarChart.Group > 0)
                {
                    if (f % BarChart.Group == 0)
                        bX += Width + GraphPadding.CellPadding + (BarChart.Group - 1) * 10;
                    else
                        bX += Width - 10 + GraphPadding.CellPadding;
                }
                else
                {
                    bX += Width + GraphPadding.CellPadding;
                }

                f += 1;
            }

            gBrush.Dispose();
        }

        private void DrawVerticalPlots(LineChart LineChart, ref Graphics g)
        {
            var LinePointCurr = new Point();
            var LinePointLast = new Point();
            var LastDotColor = new Color();
            var PlotDotSize = 8;

            var totalSpacing = LineChart.GraphRect.Width -
                               GraphPadding.CellSpacing * (LineChart.LinePlotCollection.Count - 1);
            totalSpacing -= GraphPadding.Border * 2;
            var Width = Convert.ToInt32(totalSpacing / LineChart.LinePlotCollection.Count);
            var Height = 0;
            float bX = LineChart.GraphRect.Left + GraphPadding.Border;
            float bY = LineChart.GraphRect.Bottom;
            var f = 1;
            var firstPass = true;
            var gBrush = new SolidBrush(Color.Blue);
            var gPen = new Pen(LineChart.LineColor, 1);

            foreach (LineSlice LinePlot in LineChart.LinePlotCollection)
            {
                if (LinePlot.Value != 0)
                {
                    Height = Convert.ToInt32(LineChart.GraphRect.Height * (LinePlot.Value / maxValue));
                    if (Height == 0) Height = 1;
                    bY = LineChart.GraphRect.Bottom - Height;

                    LinePointCurr = new Point(Convert.ToInt32(bX), Convert.ToInt32(bY));

                    gBrush.Color = LinePlot.Color;

                    if (firstPass)
                    {
                        firstPass = false;
                    }
                    else
                    {
                        gBrush.Color = LastDotColor;
                        g.DrawLine(gPen, LinePointAdd(LinePointLast, PlotDotSize),
                            LinePointAdd(LinePointCurr, PlotDotSize));
                        g.FillEllipse(gBrush, LinePointLast.X, LinePointLast.Y, PlotDotSize, PlotDotSize);
                    }

                    LinePointLast = LinePointCurr;
                    gBrush.Color = LinePlot.Color;

                    g.FillEllipse(gBrush, LinePointLast.X, LinePointLast.Y, PlotDotSize, PlotDotSize);
                    g.DrawEllipse(gPen, LinePointLast.X, LinePointLast.Y, PlotDotSize, PlotDotSize);
                }

                bX += Width + GraphPadding.CellPadding;
                LastDotColor = LinePlot.Color;

                f += 1;
            }

            gBrush.Dispose();
        }


        private Point LinePointAdd(Point LinePoint, int PlotDotSize)
        {
            return new Point(Convert.ToInt32(LinePoint.X + PlotDotSize / 2),
                Convert.ToInt32(LinePoint.Y + PlotDotSize / 2));
        }


        public Bitmap GetBitmap(Size bmSize)
        {
            return new Bitmap(bmSize.Width, bmSize.Height, PixelFormat.Format24bppRgb);
        }

        private Size AutoSize(GraphBase Graph, BaseChunkCollection ChunkCollection)
        {
            int lheight = 0, lwidth = 0;

            switch (Graph.ChartType)
            {
                case ChartType.Pie:
                    return new Size(300, 300);
                case ChartType.Bar:
                case ChartType.Line:
                    if ((Graph.GraphAlign == BarTypes.HorizontalLeft) |
                        (Graph.GraphAlign == BarTypes.HorizontalRight))
                    {
                        lwidth = ChunkCollection.Count * 55;
                        lheight = Convert.ToInt32(lwidth * 0.75);
                    }
                    else
                    {
                        lheight = ChunkCollection.Count * 55;
                        lwidth = Convert.ToInt32(lheight * 0.75);
                    }

                    break;
            }


            return new Size(lwidth, lheight);
        }

        private Rectangle CalcGraph(GraphBase Graph)
        {
            var lBorder = Convert.ToInt32(GraphPadding.Border * 2.8);
            var retRect = new Rectangle();
            retRect.X = lBorder;
            retRect.Y = lBorder;
            retRect.Width = Graph.ImageSize.Width - lBorder * 2;
            retRect.Height = Graph.ImageSize.Height - lBorder * 2;

            return retRect;
        }


        private Rectangle CalcGridRect(GraphBase Graph)
        {
            var oRect = CalcGraph(Graph);
            return new Rectangle(oRect.X, oRect.Y, oRect.Width + 3, oRect.Height);
        }

        protected MathInfo getMathInfo(ref long maxValue)
        {
            var retInfo = new MathInfo();


            var selectVal = maxValue;
            if (0 <= selectVal && selectVal <= 99)
            {
                retInfo.Divisor = 1;
                retInfo.Multiplier = "1";
                retInfo.Max += 5;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 10) * 10);
            }
            else if (100 <= selectVal && selectVal <= 999)
            {
                retInfo.Divisor = 10;
                retInfo.Multiplier = "10";
                retInfo.Max += 50;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 100) * 100);
            }
            else if (1000 <= selectVal && selectVal <= 9999)
            {
                retInfo.Divisor = 100;
                retInfo.Multiplier = "100";
                retInfo.Max += 500;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 1000) * 1000);
            }
            else if (10000 <= selectVal && selectVal <= 99999)
            {
                retInfo.Divisor = 1000;
                retInfo.Multiplier = "1K";
                retInfo.Max += 5000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 10000) * 10000);
            }
            else if (100000 <= selectVal && selectVal <= 999999)
            {
                retInfo.Divisor = 10000;
                retInfo.Multiplier = "10K";
                retInfo.Max += 50000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 100000) * 100000);
            }
            else if (1000000 <= selectVal && selectVal <= 9999999)
            {
                retInfo.Divisor = 100000;
                retInfo.Multiplier = "100K";
                retInfo.Max += 500000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 1000000) * 1000000);
            }
            else if (10000000 <= selectVal && selectVal <= 99999999)
            {
                retInfo.Divisor = 1000000;
                retInfo.Multiplier = "1M";
                retInfo.Max += 5000000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 10000000) * 10000000);
            }
            else if (100000000 <= selectVal && selectVal <= 999999999)
            {
                retInfo.Divisor = 10000000;
                retInfo.Multiplier = "10M";
                retInfo.Max += 50000000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 100000000) * 100000000);
            }
            else if (1000000000 <= selectVal && selectVal <= 9999999999)
            {
                retInfo.Divisor = 100000000;
                retInfo.Multiplier = "100M";
                retInfo.Max += 500000000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 1000000000) * 1000000000);
            }
            else if (10000000000 <= selectVal && selectVal <= 99999999999)
            {
                retInfo.Divisor = 1000000000;
                retInfo.Multiplier = "1B";
                retInfo.Max += 5000000000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 10000000000) * 10000000000);
            }
            else if (100000000000 <= selectVal && selectVal <= 999999999999)
            {
                retInfo.Divisor = 10000000000;
                retInfo.Multiplier = "10B";
                retInfo.Max += 50000000000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 100000000000) * 100000000000);
            }
            else if (1000000000000 <= selectVal && selectVal <= 9999999999999)
            {
                retInfo.Divisor = 100000000000;
                retInfo.Multiplier = "100B";
                retInfo.Max += 500000000000;
                retInfo.Max = Convert.ToInt32(Math.Round((double) retInfo.Max / 1000000000000) * 1000000000000);
            }


            return retInfo;
        }

        #endregion
    }

    #endregion
}