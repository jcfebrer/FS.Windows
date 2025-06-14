#region

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using FSLibrary;
using FSException;
using FSGraphics;

#endregion


namespace FSFormControls
{
    public enum Shapes
    {
        Rectangle,
        RoundedRectangle,
        Pentagon,
        Octagon,
        Circle,
        Hexagon,
        Triangle
    }


    public enum BorderStyles
    {
        None,
        FixedSingle,
        Fixed3D,
        Sunken
    }


    public enum Shadowpositions
    {
        TopLeft,
        TopRight,
        BottomRight,
        BottomLeft
    }


    [DefaultProperty("Shape")]
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBShape.bmp")]
    [ToolboxItem(true)]
    public class DBShape : DBUserControl
    {
        private readonly IContainer components = null;
        private Region Fill_Region;
        private Point[] Hex_Points = new Point[6];
        private Point[] Hex_Shadow_Points = new Point[6];

        //private Region Shadow_Region; 

        private byte m_AlphaBlend;
        private Bitmap m_BackgroundTexture;

        private bool m_DrawGradiant;
        private bool m_DrawHatch;
        private bool m_DrawShadow;
        private bool m_DrawText;
        private bool m_DrawTexture;
        private Color m_FillColor;

        private Shapes m_Shape;

        private SmoothingMode m_SmoothMode;

        private Point[] Oct_Points = new Point[8];
        private Point[] Oct_Shadow_Points = new Point[8];
        private Rectangle Rect_Points;
        private Rectangle Rect_Shadow_Points;
        private Point[] Tri_Points = new Point[3];
        private Point[] Tri_Shadow_Points = new Point[3];

        public DBShape()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            PropertyChanged = OnPropertyChange;
            //PropertyChanged += OnPropertyChange;
            LoadDefaults();


            ShadowOptions.PropertyChanged += m_ShadowOptions_PropertyChanged;
            GradiantOptions.PropertyChanged += m_GradiantOptions_PropertyChanged;
            HatchOptions.PropertyChanged += m_HatchOptions_PropertyChanged;
            TextOptions.PropertyChanged += m_TextOptions_PropertyChanged;
            BorderOptions.PropertyChanged += m_BorderOptions_PropertyChanged;

            Resize += BorderEx_Resize;
        }


        [DefaultValue(DashStyle.Solid)]
        [Browsable(true)]
        [Category("Design")]
        [Description("Set True if you want to draw gradiant background")]
        public SmoothingMode SmoothMode
        {
            get { return m_SmoothMode; }
            set
            {
                m_SmoothMode = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Browsable(true)]
        [Category("Design")]
        [Description("Set True if you want to draw gradiant background")]
        public bool DrawGradiant
        {
            get { return m_DrawGradiant; }
            set
            {
                m_DrawGradiant = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Browsable(true)]
        [Category("Design")]
        [Description("Set True if you want to draw text")]
        public bool DrawText
        {
            get { return m_DrawText; }
            set
            {
                m_DrawText = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Browsable(true)]
        [Category("Design")]
        [Description("Set True if you want to draw texture")]
        public bool DrawTexture
        {
            get { return m_DrawTexture; }
            set
            {
                m_DrawTexture = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Browsable(true)]
        [Category("Design")]
        [Description("Set True if you want to draw hatch background")]
        public bool DrawHatch
        {
            get { return m_DrawHatch; }
            set
            {
                m_DrawHatch = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Browsable(true)]
        [Category("Design")]
        [Description("Set True if you want to draw shadow")]
        public bool DrawShadow
        {
            get { return m_DrawShadow; }
            set
            {
                m_DrawShadow = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("Values")]
        [Description("Set the properties for border")]
        public BorderProperties BorderOptions { get; set; } = new BorderProperties(2, Color.Black, false,
            DashStyle.Solid,
            Border3DStyle.Flat, Color.Empty, Color.Empty,
            LinearGradientMode.Horizontal);

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("Values")]
        [Description("Set the properties for text")]
        public TextProperties TextOptions { get; set; } = new TextProperties("", new Font("Arial", 8), Color.Black,
            ContentAlignment.TopCenter);

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("Values")]
        [Description("Set the properties for gradiant")]
        public GradiantProperties GradiantOptions { get; set; } = new GradiantProperties(Color.Empty, Color.Empty,
            LinearGradientMode.Horizontal);

        [Browsable(true)]
        [Category("Values")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("Set the properties for background texture")]
        public Bitmap BackgroundTexture
        {
            get { return m_BackgroundTexture; }
            set
            {
                m_BackgroundTexture = value;
                if (m_DrawTexture) Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Values")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("Set the properties for shadow")]
        public ShadowProperties ShadowOptions { get; set; } = new ShadowProperties(Color.DarkGray,
            Shadowpositions.BottomRight,
            6);

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("Values")]
        [Description("Set the properties for hatch desing")]
        public HatchProperties HatchOptions { get; set; } = new HatchProperties(Color.Empty, Color.Transparent,
            HatchStyle.BackwardDiagonal);

        [DefaultValue(0)]
        [Browsable(true)]
        [Category("Design")]
        [Description("Set the shape")]
        public Shapes Shape
        {
            get { return m_Shape; }
            set
            {
                if ((value == Shapes.RoundedRectangle) | (value == Shapes.Pentagon))
                    throw new ExceptionUtil("Selected shape " + value + " is not implemented in this version");

                m_Shape = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Design")]
        [Description("Set the shape fill color")]
        public Color FillColor
        {
            get { return m_FillColor; }
            set
            {
                m_FillColor = value;
                Invalidate();
            }
        }

        [DefaultValue(255)]
        [Browsable(true)]
        [Category("Design")]
        [Description("Set the alpha blend for the shape")]
        public byte AlphaBlend
        {
            get { return m_AlphaBlend; }
            set
            {
                m_AlphaBlend = value;
                Invalidate();
            }
        }

        //private Rectangle m_Rect; 
        //private Rectangle m_ShadowRect; 

        private event PropertyChangedEventHandler PropertyChanged;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // DBShape
            // 
            Name = "DBShape";
            Size = new Size(153, 148);
            ResumeLayout(false);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            ReDrawControl(ref g);
        }


        private void BorderEx_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }


        public void LoadDefaults()
        {
            var transTemp19 = this;
            transTemp19.m_FillColor = SystemColors.Control;
            ForeColor = Color.Black;
            transTemp19.m_Shape = Shapes.Rectangle;
            transTemp19.m_AlphaBlend = 255;
            transTemp19.m_DrawShadow = false;
            transTemp19.m_DrawGradiant = false;
            transTemp19.m_DrawTexture = false;
            transTemp19.m_DrawHatch = false;
        }


        public bool ShouldSerializeFillColor()
        {
            return !(m_FillColor.ToArgb() == Color.Black.ToArgb());
        }


        private void ReDrawControl(ref Graphics g)
        {
            CreateRegion();
            if (m_DrawShadow) Draw_Shadow(g);
            if (!(m_FillColor.ToKnownColor() == Color.Empty.ToKnownColor())) Draw_Fill(ref g);
            if (m_DrawGradiant) Draw_Gradient(ref g);
            if (m_DrawTexture) Draw_Texture(ref g);
            if (m_DrawHatch) Draw_Hatch(ref g);
            Draw_Border(ref g);
            Draw_Border3D(ref g);
            if (m_DrawText) Draw_Text(ref g);
        }


        private void CreateRegion()
        {
            var graphics_path = new GraphicsPath();
            var ShadowWidth = ShadowOptions.ShadowWidth;

            switch (Shape)
            {
                case Shapes.Octagon:
                    if (m_DrawShadow)
                        switch (ShadowOptions.ShadowPostion)
                        {
                            case Shadowpositions.BottomLeft:
                                Oct_Points =
                                    GenerateOctPoints(new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Oct_Shadow_Points =
                                    GenerateOctPoints(new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                            case Shadowpositions.BottomRight:
                                Oct_Points =
                                    GenerateOctPoints(new Rectangle(0, 0, Width - ShadowWidth, Height - ShadowWidth));
                                Oct_Shadow_Points =
                                    GenerateOctPoints(new Rectangle(ShadowWidth, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                            case Shadowpositions.TopLeft:
                                Oct_Points =
                                    GenerateOctPoints(new Rectangle(ShadowWidth, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Oct_Shadow_Points =
                                    GenerateOctPoints(new Rectangle(0, 0, Width - ShadowWidth, Height - ShadowWidth));
                                break;
                            case Shadowpositions.TopRight:
                                Oct_Points =
                                    GenerateOctPoints(new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Oct_Shadow_Points =
                                    GenerateOctPoints(new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                        }
                    else
                        Oct_Points = GenerateOctPoints(new Rectangle(0, 0, Width, Height));

                    graphics_path.AddPolygon(Oct_Points);
                    break;
                case Shapes.Hexagon:
                    if (m_DrawShadow)
                        switch (ShadowOptions.ShadowPostion)
                        {
                            case Shadowpositions.BottomLeft:
                                Hex_Points =
                                    GenerateHexPoints(new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Hex_Shadow_Points =
                                    GenerateHexPoints(new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                            case Shadowpositions.BottomRight:
                                Hex_Points =
                                    GenerateHexPoints(new Rectangle(0, 0, Width - ShadowWidth, Height - ShadowWidth));
                                Hex_Shadow_Points =
                                    GenerateHexPoints(new Rectangle(ShadowWidth, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                            case Shadowpositions.TopLeft:
                                Hex_Points =
                                    GenerateHexPoints(new Rectangle(ShadowWidth, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Hex_Shadow_Points =
                                    GenerateHexPoints(new Rectangle(0, 0, Width - ShadowWidth, Height - ShadowWidth));
                                break;
                            case Shadowpositions.TopRight:
                                Hex_Points =
                                    GenerateHexPoints(new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Hex_Shadow_Points =
                                    GenerateHexPoints(new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                        }
                    else
                        Hex_Points = GenerateHexPoints(new Rectangle(0, 0, Width, Height));

                    graphics_path.AddPolygon(Hex_Points);
                    break;
                case Shapes.Rectangle:
                    if (m_DrawShadow)
                        switch (ShadowOptions.ShadowPostion)
                        {
                            case Shadowpositions.BottomLeft:
                                Rect_Points = new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                Rect_Shadow_Points = new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                break;
                            case Shadowpositions.BottomRight:
                                Rect_Points = new Rectangle(0, 0, Width - ShadowWidth, Height - ShadowWidth);
                                Rect_Shadow_Points = new Rectangle(ShadowWidth, ShadowWidth, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                break;
                            case Shadowpositions.TopLeft:
                                Rect_Points = new Rectangle(ShadowWidth, ShadowWidth, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                Rect_Shadow_Points = new Rectangle(0, 0, Width - ShadowWidth, Height - ShadowWidth);
                                break;
                            case Shadowpositions.TopRight:
                                Rect_Points = new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                Rect_Shadow_Points = new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                break;
                        }
                    else
                        Rect_Points = new Rectangle(0, 0, Width - BorderOptions.BorderWidth,
                            Height - BorderOptions.BorderWidth);

                    graphics_path.AddRectangle(Rect_Points);
                    break;
                case Shapes.Circle:
                    if (m_DrawShadow)
                        switch (ShadowOptions.ShadowPostion)
                        {
                            case Shadowpositions.BottomLeft:
                                Rect_Points = new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                Rect_Shadow_Points = new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                break;
                            case Shadowpositions.BottomRight:
                                Rect_Points = new Rectangle(0, 0, Width - ShadowWidth, Height - ShadowWidth);
                                Rect_Shadow_Points = new Rectangle(ShadowWidth, ShadowWidth, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                break;
                            case Shadowpositions.TopLeft:
                                Rect_Points = new Rectangle(ShadowWidth, ShadowWidth, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                Rect_Shadow_Points = new Rectangle(0, 0, Width - ShadowWidth, Height - ShadowWidth);
                                break;
                            case Shadowpositions.TopRight:
                                Rect_Points = new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                Rect_Shadow_Points = new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                    Height - ShadowWidth);
                                break;
                        }
                    else
                        Rect_Points = new Rectangle(0, 0, Width, Height);

                    graphics_path.AddEllipse(Rect_Points);
                    break;
                case Shapes.Triangle:
                    if (m_DrawShadow)
                        switch (ShadowOptions.ShadowPostion)
                        {
                            case Shadowpositions.BottomLeft:
                                Tri_Points =
                                    GenerateTrianglePoints(new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Tri_Shadow_Points =
                                    GenerateTrianglePoints(new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                            case Shadowpositions.BottomRight:
                                Tri_Points =
                                    GenerateTrianglePoints(new Rectangle(0, 0, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Tri_Shadow_Points =
                                    GenerateTrianglePoints(new Rectangle(ShadowWidth, ShadowWidth,
                                        Width - ShadowWidth, Height - ShadowWidth));
                                break;
                            case Shadowpositions.TopLeft:
                                Tri_Points =
                                    GenerateTrianglePoints(new Rectangle(ShadowWidth, ShadowWidth,
                                        Width - ShadowWidth, Height - ShadowWidth));
                                Tri_Shadow_Points =
                                    GenerateTrianglePoints(new Rectangle(0, 0, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                            case Shadowpositions.TopRight:
                                Tri_Points =
                                    GenerateTrianglePoints(new Rectangle(0, ShadowWidth, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                Tri_Shadow_Points =
                                    GenerateTrianglePoints(new Rectangle(ShadowWidth, 0, Width - ShadowWidth,
                                        Height - ShadowWidth));
                                break;
                        }
                    else
                        Tri_Points = GenerateTrianglePoints(new Rectangle(0, 0, Width, Height));

                    graphics_path.AddPolygon(Tri_Points);
                    break;
            }

            Fill_Region = new Region(graphics_path);
            if (!m_DrawShadow) Region = Fill_Region;
            graphics_path.Dispose();
        }


        private void Draw_Border(ref Graphics g)
        {
            LinearGradientBrush _Brush = null;
            Pen _Pen = null;
            try
            {
                if (BorderOptions.BorderGradiant)
                {
                    _Brush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), BorderOptions.StartColor,
                        BorderOptions.EndColor, BorderOptions.GradiantStyle);
                    _Pen = new Pen(_Brush, BorderOptions.BorderWidth);
                }
                else
                {
                    _Pen = new Pen(BorderOptions.BorderColor, BorderOptions.BorderWidth);
                }

                _Pen.Alignment = PenAlignment.Inset;
                _Pen.DashStyle = BorderOptions.BorderDashStyle;
                g.SmoothingMode = SmoothMode;
                switch (Shape)
                {
                    case Shapes.Octagon:
                        g.DrawPolygon(_Pen, Oct_Points);
                        break;
                    case Shapes.Hexagon:
                        g.DrawPolygon(_Pen, Hex_Points);
                        break;
                    case Shapes.Rectangle:
                        g.DrawRectangle(_Pen, Rect_Points);
                        break;
                    case Shapes.Circle:
                        g.DrawEllipse(_Pen, Rect_Points);
                        break;
                    case Shapes.Triangle:
                        g.DrawPolygon(_Pen, Tri_Points);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                object transTemp0 = _Pen;
                if (!(transTemp0 == null)) _Pen.Dispose();
                object transTemp1 = _Brush;
                if (!(transTemp1 == null)) _Brush.Dispose();
            }
        }


        private void Draw_Text(ref Graphics g)
        {
            SolidBrush _TextBrush = null;
            var StartPosition = 0;
            var Topposition = 0;
            var StringSize = new SizeF();
            try
            {
                _TextBrush = new SolidBrush(TextOptions.TextColor);
                StringSize = g.MeasureString(TextOptions.Text, TextOptions.TextFont);
                g.SmoothingMode = SmoothMode;
                switch (TextOptions.TextAlignment)
                {
                    case ContentAlignment.BottomCenter:
                        StartPosition = Convert.ToInt32((Width - StringSize.Width) / 2);
                        Topposition = Height - FontHeight;
                        break;
                    case ContentAlignment.BottomLeft:
                        StartPosition = 5;
                        Topposition = Height - FontHeight;
                        break;
                    case ContentAlignment.BottomRight:
                        StartPosition = Convert.ToInt32(Width - StringSize.Width);
                        Topposition = Height - FontHeight;
                        break;
                    case ContentAlignment.MiddleCenter:
                        StartPosition = Convert.ToInt32((Width - StringSize.Width) / 2);
                        Topposition = Convert.ToInt32(Height / 2);
                        break;
                    case ContentAlignment.MiddleLeft:
                        StartPosition = 5;
                        Topposition = Convert.ToInt32(Height / 2);
                        break;
                    case ContentAlignment.MiddleRight:
                        StartPosition = Convert.ToInt32(Width - StringSize.Width);
                        Topposition = Convert.ToInt32(Height / 2);
                        break;
                    case ContentAlignment.TopCenter:
                        StartPosition = Convert.ToInt32((Width - StringSize.Width) / 2);
                        Topposition = 5;
                        break;
                    case ContentAlignment.TopLeft:
                        StartPosition = 0;
                        Topposition = 5;
                        break;
                    case ContentAlignment.TopRight:
                        StartPosition = Convert.ToInt32(Width - StringSize.Width);
                        Topposition = 5;
                        break;
                }

                g.DrawString(TextOptions.Text, TextOptions.TextFont, _TextBrush, StartPosition, Topposition);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void Draw_Fill(ref Graphics g)
        {
            SolidBrush m_SolidBrush = null;
            try
            {
                m_SolidBrush = new SolidBrush(FillColor);
                switch (m_Shape)
                {
                    case Shapes.Circle:
                        g.FillEllipse(m_SolidBrush, Rect_Points);
                        break;
                    case Shapes.Rectangle:
                        g.FillRectangle(m_SolidBrush, Rect_Points);
                        break;
                    case Shapes.Hexagon:
                        g.FillPolygon(m_SolidBrush, Hex_Points);
                        break;
                    case Shapes.Octagon:
                        g.FillPolygon(m_SolidBrush, Oct_Points);
                        break;
                    case Shapes.Triangle:
                        g.FillPolygon(m_SolidBrush, Tri_Points);
                        break;
                    case Shapes.RoundedRectangle:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                m_SolidBrush.Dispose();
            }
        }


        private void Draw_Gradient(ref Graphics g)
        {
            LinearGradientBrush m_GradientBrush = null;
            try
            {
                m_GradientBrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height),
                    GradiantOptions.StartColor, GradiantOptions.EndColor,
                    GradiantOptions.GradiantStyle);
                switch (m_Shape)
                {
                    case Shapes.Circle:
                        g.FillEllipse(m_GradientBrush, Rect_Points);
                        break;
                    case Shapes.Rectangle:
                        g.FillRectangle(m_GradientBrush, Rect_Points);
                        break;
                    case Shapes.Hexagon:
                        g.FillPolygon(m_GradientBrush, Hex_Points);
                        break;
                    case Shapes.Octagon:
                        g.FillPolygon(m_GradientBrush, Oct_Points);
                        break;
                    case Shapes.Triangle:
                        g.FillPolygon(m_GradientBrush, Tri_Points);
                        break;
                    case Shapes.RoundedRectangle:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                m_GradientBrush.Dispose();
            }
        }


        private void Draw_Texture(ref Graphics g)
        {
            Image m_TextureImage = null;
            TextureBrush m_TextureBrush = null;
            object transTemp2 = m_BackgroundTexture;
            if (!(transTemp2 == null))
                try
                {
                    m_TextureImage = new Bitmap(m_BackgroundTexture);
                    m_TextureBrush = new TextureBrush(m_TextureImage);
                    switch (m_Shape)
                    {
                        case Shapes.Circle:
                            g.FillEllipse(m_TextureBrush, Rect_Points);
                            break;
                        case Shapes.Rectangle:
                            g.FillRectangle(m_TextureBrush, Rect_Points);
                            break;
                        case Shapes.Hexagon:
                            g.FillPolygon(m_TextureBrush, Hex_Points);
                            break;
                        case Shapes.Octagon:
                            g.FillPolygon(m_TextureBrush, Oct_Points);
                            break;
                        case Shapes.Triangle:
                            g.FillPolygon(m_TextureBrush, Tri_Points);
                            break;
                        case Shapes.RoundedRectangle:
                            GraphicsUtil.DrawRoundedRectangle(g, m_TextureBrush, Rect_Points, 10);
                            break;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    m_TextureBrush.Dispose();
                }
        }


        private void Draw_Hatch(ref Graphics g)
        {
            HatchBrush m_HatchBrush = null;
            try
            {
                m_HatchBrush = new HatchBrush(HatchOptions.HatchStyle, HatchOptions.PenColor,
                    HatchOptions.BackColor);
                g.SmoothingMode = SmoothMode;
                switch (m_Shape)
                {
                    case Shapes.Circle:
                        g.FillEllipse(m_HatchBrush, Rect_Points);
                        break;
                    case Shapes.Rectangle:
                        g.FillRectangle(m_HatchBrush, Rect_Points);
                        break;
                    case Shapes.Hexagon:
                        g.FillPolygon(m_HatchBrush, Hex_Points);
                        break;
                    case Shapes.Octagon:
                        g.FillPolygon(m_HatchBrush, Oct_Points);
                        break;
                    case Shapes.Triangle:
                        g.FillPolygon(m_HatchBrush, Tri_Points);
                        break;
                    case Shapes.RoundedRectangle:
                        GraphicsUtil.DrawRoundedRectangle(g, m_HatchBrush, Rect_Points, 10);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                m_HatchBrush.Dispose();
            }
        }


        private void Draw_Border3D(ref Graphics g)
        {
            if (!(BorderOptions.BorderStyle == Border3DStyle.Flat))
                try
                {
                    switch (Shape)
                    {
                        case Shapes.Rectangle:
                            ControlPaint.DrawBorder3D(g, Rect_Points, BorderOptions.BorderStyle);
                            break;
                        case Shapes.Circle:
                            break;
                        case Shapes.Hexagon:
                            break;
                        case Shapes.Octagon:
                            break;
                        case Shapes.Triangle:
                            break;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
        }


        private void Draw_Shadow(Graphics G)
        {
            SolidBrush m_SolidBrush = null;
            try
            {
                m_SolidBrush = new SolidBrush(ShadowOptions.ShadowColor);
                switch (m_Shape)
                {
                    case Shapes.Circle:
                        G.FillEllipse(m_SolidBrush, Rect_Shadow_Points);
                        break;
                    case Shapes.Hexagon:
                        G.FillPolygon(m_SolidBrush, Hex_Shadow_Points);
                        break;
                    case Shapes.Octagon:
                        G.FillPolygon(m_SolidBrush, Oct_Shadow_Points);
                        break;
                    case Shapes.Rectangle:
                        G.FillRectangle(m_SolidBrush, Rect_Shadow_Points);
                        break;
                    case Shapes.RoundedRectangle:
                        GraphicsUtil.DrawRoundedRectangle(G, m_SolidBrush, Rect_Shadow_Points, 10);
                        break;
                    case Shapes.Triangle:
                        G.FillPolygon(m_SolidBrush, Tri_Shadow_Points);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                m_SolidBrush.Dispose();
            }
        }


        private Point[] GenerateTrianglePoints(Rectangle Rect)
        {
            var Tri_Points = new Point[3];
            var _Width = Rect.Width;
            var _Height = Rect.Height;
            var CenterPoint = 0;
            var LeftPoint = 0;
            var RightPoint = 0;
            try
            {
                CenterPoint = Convert.ToInt32(_Width / 2 + Rect.X);
                LeftPoint = _Height + Rect.Y;
                RightPoint = _Width + Rect.X;
                Tri_Points[0] = new Point(CenterPoint, Rect.Y);
                Tri_Points[1] = new Point(Rect.X, LeftPoint);
                Tri_Points[2] = new Point(_Width + Rect.X, _Height + Rect.Y);
                return Tri_Points;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private Point[] GenerateHexPoints(Rectangle Rect)
        {
            var HexPoints = new Point[6];
            var _Width = Rect.Width;
            var _Height = Rect.Height;
            var TopPoint = 0;
            var BottomPoint = 0;
            var Center = 0;
            var SideBar = 0;

            try
            {
                Center = Convert.ToInt32((_Width + Rect.X) / 2);
                SideBar = Convert.ToInt32(_Width / 2);
                TopPoint = Convert.ToInt32((_Height - SideBar) / 2);
                BottomPoint = TopPoint + SideBar;
                TopPoint += Rect.Y;

                HexPoints[0] = new Point(Center, Rect.Y);
                HexPoints[1] = new Point(_Width, TopPoint);
                HexPoints[2] = new Point(_Width, BottomPoint);
                HexPoints[3] = new Point(Center, _Height);
                HexPoints[4] = new Point(Rect.X, BottomPoint);
                HexPoints[5] = new Point(Rect.X, TopPoint);
                return HexPoints;
            }
            catch
            {
                return null;
            }
        }


        private Point[] GenerateOctPoints(Rectangle Rect)
        {
            var OctPoints = new Point[8];
            var _Width = Rect.Width;
            var _Height = Rect.Height;
            double P1 = 0;
            double P2 = 0;
            double P3 = 0;
            double P4 = 0;

            try
            {
                P1 = (_Width + Rect.X) / 3;
                P2 = P1 * 2;
                P3 = (_Height + Rect.Y) / 3;
                P4 = P3 * 2;

                OctPoints[0] = new Point(Convert.ToInt32(P1), Rect.Y);
                OctPoints[1] = new Point(Convert.ToInt32(P2), Rect.Y);
                OctPoints[2] = new Point(_Width, Convert.ToInt32(P3));
                OctPoints[3] = new Point(_Width, Convert.ToInt32(P4));
                OctPoints[4] = new Point(Convert.ToInt32(P2), _Height);
                OctPoints[5] = new Point(Convert.ToInt32(P1), _Height);
                OctPoints[6] = new Point(Rect.X, Convert.ToInt32(P4));
                OctPoints[7] = new Point(Rect.Y, Convert.ToInt32(P3));

                return OctPoints;
            }
            catch
            {
                return null;
            }
        }


        private void OnPropertyChange()
        {
            var g = CreateGraphics();
            ReDrawControl(ref g);
        }


        private void m_ShadowOptions_PropertyChanged()
        {
            if (m_DrawShadow) Invalidate();
        }


        private void m_GradiantOptions_PropertyChanged()
        {
            if (m_DrawGradiant) Invalidate();
        }


        private void m_HatchOptions_PropertyChanged()
        {
            if (m_DrawHatch) Invalidate();
        }


        private void m_TextOptions_PropertyChanged()
        {
            if (m_DrawText) Invalidate();
        }


        private void m_BorderOptions_PropertyChanged()
        {
            Invalidate();
        }

        #region Nested type: PropertyChangedEventHandler

        private delegate void PropertyChangedEventHandler();

        #endregion
    }


    [TypeConverter(typeof(ShadowPropertiesConverter))]
    public class ShadowProperties
    {
        #region Delegates

        public delegate void PropertyChangedEventHandler();

        #endregion

        private Color m_ShadowColor;
        private Shadowpositions m_ShadowPostion;
        private short m_ShadowWidth;

        public ShadowProperties(Color ShadowColor, Shadowpositions ShadowPosition, short ShadowWidth)
        {
            var transTemp20 = this;
            transTemp20.m_ShadowColor = ShadowColor;
            transTemp20.m_ShadowPostion = ShadowPosition;
            transTemp20.m_ShadowWidth = ShadowWidth;
        }


        [Browsable(true)]
        [DefaultValue(5)]
        [Description("Set the border width for shadow")]
        [NotifyParentProperty(true)]
        public short ShadowWidth
        {
            get { return m_ShadowWidth; }
            set
            {
                m_ShadowWidth = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Select the shadow color")]
        [NotifyParentProperty(true)]
        public Color ShadowColor
        {
            get { return m_ShadowColor; }
            set
            {
                m_ShadowColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [DefaultValue(0)]
        [Description("Select the shadow position")]
        [NotifyParentProperty(true)]
        public Shadowpositions ShadowPostion
        {
            get { return m_ShadowPostion; }
            set
            {
                m_ShadowPostion = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    internal class ShadowPropertiesConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            try
            {
                if (sourceType == typeof(string))
                    return true;
                return base.CanConvertFrom(context, sourceType);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible CanConvertFrom: ShadowPropertiesConverter", ex);
            }
        }


        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            try
            {
                if ((destinationType == typeof(InstanceDescriptor)) | (destinationType == typeof(string))) return true;
                return base.CanConvertTo(context, destinationType);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible CanConvertTo: ShadowPropertiesConverter", ex);
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var m_Value = "";
            try
            {
                m_Value = Convert.ToString(value);
                if (!(m_Value == null))
                {
                    m_Value = m_Value.Trim();
                    if (m_Value.Length != 0)
                    {
                        if (culture == null) culture = CultureInfo.CurrentCulture;
                        var paramsIdent = m_Value.Split(char.Parse(","));
                        var shadowColor = new Color();
                        Shadowpositions ShadowOption = 0;
                        short ShadowWidth = 0;
                        shadowColor = ColorTranslator.FromHtml(paramsIdent[0]);
                        ShadowOption = (Shadowpositions) long.Parse(paramsIdent[1]);
                        ShadowWidth = Convert.ToInt16(double.Parse(paramsIdent[2]));
                        return new ShadowProperties(shadowColor, ShadowOption, ShadowWidth);
                    }

                    return base.ConvertFrom(context, culture, value);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible convertir: " + m_Value, ex);
            }

            return string.Empty;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            ShadowProperties m_ShadowProperties = null;
            try
            {
                m_ShadowProperties = (ShadowProperties) value;
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var argTypes = new Type[3];
                    argTypes[0] = typeof(Color);
                    argTypes[1] = typeof(Shadowpositions);
                    argTypes[2] = typeof(short);
                    var constructor = typeof(ShadowProperties).GetConstructor(argTypes);
                    var arguments = new object[3];
                    arguments[0] = m_ShadowProperties.ShadowColor;
                    arguments[1] = m_ShadowProperties.ShadowPostion;
                    arguments[2] = m_ShadowProperties.ShadowWidth;
                    return new InstanceDescriptor(constructor, arguments);
                }

                if (destinationType == typeof(string))
                {
                    if (culture == null) culture = CultureInfo.CurrentCulture;
                    var Params = new string[4];
                    Params[0] = ColorTranslator.ToHtml(m_ShadowProperties.ShadowColor);
                    Params[1] = Convert.ToString(m_ShadowProperties.ShadowPostion);
                    Params[2] = Convert.ToString(m_ShadowProperties.ShadowWidth);

                    return string.Join(",", Params);
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible convertir.", ex);
            }
        }
    }


    [TypeConverter(typeof(HatchPropertiesConverter))]
    public class HatchProperties
    {
        #region Delegates

        public delegate void PropertyChangedEventHandler();

        #endregion

        private Color m_BackColor;
        private HatchStyle m_HatchStyle;
        private Color m_PenColor;

        public HatchProperties(Color penColor, Color BackColor, HatchStyle HatchStyle)
        {
            m_PenColor = penColor;
            m_BackColor = BackColor;
            m_HatchStyle = HatchStyle;
        }


        public Color PenColor
        {
            get { return m_PenColor; }
            set
            {
                m_PenColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        public Color BackColor
        {
            get { return m_BackColor; }
            set
            {
                m_BackColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        public HatchStyle HatchStyle
        {
            get { return m_HatchStyle; }
            set
            {
                m_HatchStyle = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    internal class HatchPropertiesConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) | (destinationType == typeof(string))) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var m_Value = "";
            try
            {
                m_Value = Convert.ToString(value);
                if (!(m_Value == null))
                {
                    m_Value = m_Value.Trim();
                    if (m_Value.Length != 0)
                    {
                        if (culture == null) culture = CultureInfo.CurrentCulture;
                        var paramsIdent = m_Value.Split(char.Parse(","));
                        var _PenColor = new Color();
                        var _BackColor = new Color();
                        HatchStyle _HatchStyle = 0;
                        object transTemp5 = paramsIdent[0];
                        if (transTemp5 == null || paramsIdent[0] == "")
                            _PenColor = Color.Black;
                        else
                            _PenColor = ColorTranslator.FromHtml(paramsIdent[0]);
                        object transTemp6 = paramsIdent[1];
                        if (transTemp6 == null || paramsIdent[1] == "")
                            _BackColor = Color.Transparent;
                        else
                            _BackColor = ColorTranslator.FromHtml(paramsIdent[1]);
                        object transTemp7 = paramsIdent[2];
                        if (transTemp7 == null || paramsIdent[2] == "")
                            _HatchStyle = 0;
                        else
                            _HatchStyle = (HatchStyle) long.Parse(paramsIdent[2]);
                        return new HatchProperties(_PenColor, _BackColor, _HatchStyle);
                    }

                    return base.ConvertFrom(context, culture, value);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible convertir: " + m_Value, ex);
            }

            return string.Empty;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            HatchProperties m_HatchProperties = null;
            try
            {
                m_HatchProperties = (HatchProperties) value;
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var argTypes = new Type[3];
                    argTypes[0] = typeof(Color);
                    argTypes[1] = typeof(Color);
                    argTypes[2] = typeof(HatchStyle);
                    var constructor = typeof(HatchProperties).GetConstructor(argTypes);
                    var arguments = new object[3];
                    arguments[0] = m_HatchProperties.PenColor;
                    arguments[1] = m_HatchProperties.BackColor;
                    arguments[2] = m_HatchProperties.HatchStyle;
                    return new InstanceDescriptor(constructor, arguments);
                }

                if (destinationType == typeof(string))
                {
                    if (culture == null) culture = CultureInfo.CurrentCulture;
                    var Params = new string[4];
                    Params[0] = ColorTranslator.ToHtml(m_HatchProperties.PenColor);
                    Params[1] = ColorTranslator.ToHtml(m_HatchProperties.BackColor);
                    Params[2] = Convert.ToString(m_HatchProperties.HatchStyle);

                    return string.Join(",", Params);
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible realizar conversión: ConvertTo - HatchPropertiesConverter", ex);
            }
        }
    }


    [TypeConverter(typeof(GradiantPropertiesConverter))]
    public class GradiantProperties
    {
        #region Delegates

        public delegate void PropertyChangedEventHandler();

        #endregion

        private Color m_EndColor;
        private LinearGradientMode m_GradiantStyle;
        private Color m_StartColor;

        public GradiantProperties(Color Start_Color, Color End_Color, LinearGradientMode Options)
        {
            m_StartColor = Start_Color;
            m_EndColor = End_Color;
            m_GradiantStyle = Options;
        }


        [Browsable(true)]
        [Description("Select the starting color for gradiant")]
        [NotifyParentProperty(true)]
        public Color StartColor
        {
            get { return m_StartColor; }
            set
            {
                m_StartColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Select the end color for gradiant")]
        [NotifyParentProperty(true)]
        public Color EndColor
        {
            get { return m_EndColor; }
            set
            {
                m_EndColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [DefaultValue(0)]
        [Description("Select the gradiant style")]
        [NotifyParentProperty(true)]
        public LinearGradientMode GradiantStyle
        {
            get { return m_GradiantStyle; }
            set
            {
                m_GradiantStyle = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    internal class GradiantPropertiesConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }


        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) | (destinationType == typeof(string))) return true;
            return base.CanConvertTo(context, destinationType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var sValue = Convert.ToString(value);
            if (!Convert.IsDBNull(sValue))
            {
                var v = sValue.Split(char.Parse(","));
                return new GradiantProperties(ColorTranslator.FromHtml(v[0]), ColorTranslator.FromHtml(v[1]),
                    (LinearGradientMode) Convert.ToInt32(v[2]));
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
                return
                    new InstanceDescriptor(
                        typeof(GradiantProperties).GetConstructor(new[]
                        {
                            typeof(Color), typeof(Color),
                            typeof(LinearGradientMode)
                        }),
                        new object[]
                        {
                            ((GradiantProperties) value).StartColor, ((GradiantProperties) value).EndColor,
                            ((GradiantProperties) value).GradiantStyle
                        });
            if (destinationType == typeof(string))
                return string.Format("{0},{1},{2}", ColorTranslator.ToHtml(((GradiantProperties) value).StartColor),
                    ColorTranslator.ToHtml(((GradiantProperties) value).EndColor),
                    ((GradiantProperties) value).GradiantStyle);
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }


    [TypeConverter(typeof(TextPropertiesConverter))]
    public class TextProperties
    {
        #region Delegates

        public delegate void PropertyChangedEventHandler();

        #endregion

        private const string DEFAULT_TEXT = "";
        private const ContentAlignment DEFAULT_TEXT_ALIGNMENT = ContentAlignment.TopCenter;
        private string m_Text;
        private ContentAlignment m_TextALignment;
        private Color m_TextColor;
        private Font m_TextFont;

        public TextProperties(string Text, Font TextFont, Color TextColor, ContentAlignment TextAlignment)
        {
            m_Text = Text;
            m_TextFont = TextFont;
            m_TextColor = TextColor;
            m_TextALignment = TextAlignment;
        }

        [DefaultValue(DEFAULT_TEXT)]
        [Browsable(true)]
        [Description("Set the text")]
        [NotifyParentProperty(true)]
        public string Text
        {
            get { return m_Text; }
            set
            {
                m_Text = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Set the text font")]
        [NotifyParentProperty(true)]
        public Font TextFont
        {
            get { return m_TextFont; }
            set
            {
                m_TextFont = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Set the text color")]
        [NotifyParentProperty(true)]
        public Color TextColor
        {
            get { return m_TextColor; }
            set
            {
                m_TextColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [DefaultValue(DEFAULT_TEXT_ALIGNMENT)]
        [Browsable(true)]
        [Description("Set the text alignment")]
        [NotifyParentProperty(true)]
        public ContentAlignment TextAlignment
        {
            get { return m_TextALignment; }
            set
            {
                m_TextALignment = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    public class TextPropertiesConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) | (destinationType == typeof(string))) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var m_Value = "";
            try
            {
                m_Value = Convert.ToString(value);
                if (!(m_Value == null))
                {
                    m_Value = m_Value.Trim();
                    if (m_Value.Length != 0)
                    {
                        if (culture == null) culture = CultureInfo.CurrentCulture;

                        var paramsIdent = m_Value.Split(char.Parse(","));
                        string Text = null;
                        var TextColor = new Color();
                        Font TextFont = null;
                        ContentAlignment TextAlignment = 0;
                        Text = paramsIdent[0];
                        object transTemp10 = paramsIdent[1];
                        if (transTemp10 == null || paramsIdent[1] == "")
                            TextFont = null;
                        else
                            TextFont = (Font) new FontConverter().ConvertFromString(paramsIdent[1]);
                        object transTemp11 = paramsIdent[2];
                        if (transTemp11 == null || paramsIdent[2] == "")
                            TextColor = Color.Empty;
                        else
                            TextColor = ColorTranslator.FromHtml(paramsIdent[2]);
                        object transTemp12 = paramsIdent[3];
                        if (transTemp12 == null || paramsIdent[3] == "")
                            TextAlignment = 0;
                        else
                            TextAlignment = (ContentAlignment) long.Parse(paramsIdent[3]);
                        return new TextProperties(Text, TextFont, TextColor, TextAlignment);
                    }

                    return base.ConvertFrom(context, culture, value);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible convertir: " + m_Value, ex);
            }

            return string.Empty;
        }


        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            TextProperties m_TextProperties = null;
            try
            {
                m_TextProperties = (TextProperties) value;
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var argTypes = new Type[4];
                    argTypes[0] = typeof(string);
                    argTypes[1] = typeof(Font);
                    argTypes[2] = typeof(Color);
                    argTypes[3] = typeof(ContentAlignment);
                    var constructor = typeof(TextProperties).GetConstructor(argTypes);
                    var arguments = new object[4];
                    arguments[0] = m_TextProperties.Text;
                    arguments[1] = m_TextProperties.TextFont;
                    arguments[2] = m_TextProperties.TextColor;
                    arguments[3] = m_TextProperties.TextAlignment;
                    return new InstanceDescriptor(constructor, arguments);
                }

                if (destinationType == typeof(string))
                {
                    if (culture == null) culture = CultureInfo.CurrentCulture;
                    var Params = new string[4];
                    Params[0] = m_TextProperties.Text;
                    Params[1] = TypeDescriptor.GetConverter(typeof(Font)).ConvertToString(m_TextProperties.TextFont);
                    Params[2] = ColorTranslator.ToHtml(m_TextProperties.TextColor);
                    Params[3] = Convert.ToString(m_TextProperties.TextAlignment);

                    return string.Join(",", Params);
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible convertir.", ex);
            }
        }
    }


    [TypeConverter(typeof(BorderPropertiesConverter))]
    public class BorderProperties
    {
        #region Delegates

        public delegate void PropertyChangedEventHandler();

        #endregion

        private Color m_BorderColor;
        private DashStyle m_BorderDashStyle;
        private bool m_BorderGradiant;
        private Border3DStyle m_BorderStyle;
        private short m_BorderWidth;
        private Color m_EndColor;
        private LinearGradientMode m_GradiantStyle;
        private Color m_StartColor;

        public BorderProperties()
        {
        }

        public BorderProperties(short Border_Width, Color Border_Color, bool Border_Gradiant,
            DashStyle Border_DashStyle, Border3DStyle Border_Style, Color Start_Color,
            Color End_Color, LinearGradientMode Gradiant_Style)
        {
            m_StartColor = Start_Color;
            m_EndColor = End_Color;
            m_GradiantStyle = Gradiant_Style;
            m_BorderColor = Border_Color;
            m_BorderDashStyle = Border_DashStyle;
            m_BorderGradiant = Border_Gradiant;
            m_BorderStyle = Border_Style;
            m_BorderWidth = Border_Width;
        }


        [Browsable(true)]
        [Description("Set true to draw gradiant border")]
        [NotifyParentProperty(true)]
        public bool BorderGradiant
        {
            get { return m_BorderGradiant; }
            set
            {
                m_BorderGradiant = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Select the Border dash style")]
        [NotifyParentProperty(true)]
        public DashStyle BorderDashStyle
        {
            get { return m_BorderDashStyle; }
            set
            {
                m_BorderDashStyle = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Select the starting color for border gradiant")]
        [NotifyParentProperty(true)]
        public Border3DStyle BorderStyle
        {
            get { return m_BorderStyle; }
            set
            {
                m_BorderStyle = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Select the starting color for border gradiant")]
        [NotifyParentProperty(true)]
        public short BorderWidth
        {
            get { return m_BorderWidth; }
            set
            {
                m_BorderWidth = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Select the border color")]
        [NotifyParentProperty(true)]
        public Color BorderColor
        {
            get { return m_BorderColor; }
            set
            {
                m_BorderColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Select the starting color for border gradiant")]
        [NotifyParentProperty(true)]
        public Color StartColor
        {
            get { return m_StartColor; }
            set
            {
                m_StartColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [Description("Select the end color for border gradiant")]
        [NotifyParentProperty(true)]
        public Color EndColor
        {
            get { return m_EndColor; }
            set
            {
                m_EndColor = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        [Browsable(true)]
        [DefaultValue(0)]
        [Description("Select the gradiant style for border")]
        [NotifyParentProperty(true)]
        public LinearGradientMode GradiantStyle
        {
            get { return m_GradiantStyle; }
            set
            {
                m_GradiantStyle = value;
                if (null != PropertyChanged) PropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    internal class BorderPropertiesConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if ((destinationType == typeof(InstanceDescriptor)) | (destinationType == typeof(string))) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var m_Value = "";
            try
            {
                m_Value = Convert.ToString(value);
                if (!(m_Value == null))
                {
                    m_Value = m_Value.Trim();
                    if (m_Value.Length != 0)
                    {
                        if (culture == null) culture = CultureInfo.CurrentCulture;
                        var paramsIdent = m_Value.Split(char.Parse(","));
                        var Start_Color = new Color();
                        var End_Color = new Color();
                        LinearGradientMode Gradiant_Style = 0;
                        var Border_Color = new Color();
                        DashStyle Border_dashStyle = 0;
                        var Border_Gradiant = false;
                        Border3DStyle Border_Style = 0;
                        short Border_Width = 0;

                        Border_Width = Convert.ToInt16(double.Parse(paramsIdent[0]));
                        Border_Color = ColorTranslator.FromHtml(paramsIdent[1]);
                        Border_Gradiant = Convert.ToBoolean(double.Parse(paramsIdent[2]));
                        Border_dashStyle = (DashStyle) long.Parse(paramsIdent[3]);
                        Border_Style = (Border3DStyle) long.Parse(paramsIdent[4]);
                        Start_Color = ColorTranslator.FromHtml(paramsIdent[5]);
                        End_Color = ColorTranslator.FromHtml(paramsIdent[6]);
                        Gradiant_Style = (LinearGradientMode) long.Parse(paramsIdent[7]);
                        return new BorderProperties(Border_Width, Border_Color, Border_Gradiant, Border_dashStyle,
                            Border_Style, Start_Color, End_Color, Gradiant_Style);
                    }

                    return base.ConvertFrom(context, culture, value);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible convertir: " + m_Value, ex);
            }

            return string.Empty;
        }


        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            BorderProperties m_BorderProperties = null;
            try
            {
                if (value is BorderProperties) m_BorderProperties = (BorderProperties) value;
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var argTypes = new Type[8];
                    argTypes[0] = typeof(short);
                    argTypes[1] = typeof(Color);
                    argTypes[2] = typeof(bool);
                    argTypes[3] = typeof(DashStyle);
                    argTypes[4] = typeof(Border3DStyle);
                    argTypes[5] = typeof(Color);
                    argTypes[6] = typeof(Color);
                    argTypes[7] = typeof(LinearGradientMode);
                    var constructor = typeof(BorderProperties).GetConstructor(argTypes);
                    var arguments = new object[8];
                    if (m_BorderProperties is BorderProperties)
                    {
                        arguments[0] = m_BorderProperties.BorderWidth;
                        arguments[1] = m_BorderProperties.BorderColor;
                        arguments[2] = m_BorderProperties.BorderGradiant;
                        arguments[3] = m_BorderProperties.BorderDashStyle;
                        arguments[4] = m_BorderProperties.BorderStyle;
                        arguments[5] = m_BorderProperties.StartColor;
                        arguments[6] = m_BorderProperties.EndColor;
                        arguments[7] = m_BorderProperties.GradiantStyle;
                    }

                    return new InstanceDescriptor(constructor, arguments);
                }

                if (destinationType == typeof(string))
                {
                    if (culture == null) culture = CultureInfo.CurrentCulture;
                    var Params = new string[8];
                    if (m_BorderProperties is BorderProperties)
                    {
                        Params[0] = Convert.ToString(m_BorderProperties.BorderWidth);
                        Params[1] = ColorTranslator.ToHtml(m_BorderProperties.BorderColor);
                        Params[2] = Convert.ToString(m_BorderProperties.BorderGradiant);
                        Params[3] = Convert.ToString(m_BorderProperties.BorderDashStyle);
                        Params[4] = Convert.ToString(m_BorderProperties.BorderStyle);
                        Params[5] = ColorTranslator.ToHtml(m_BorderProperties.StartColor);
                        Params[6] = ColorTranslator.ToHtml(m_BorderProperties.EndColor);
                        Params[7] = Convert.ToString(m_BorderProperties.GradiantStyle);
                    }

                    return string.Join(",", Params);
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Imposible convertir.", ex);
            }
        }
    }
}