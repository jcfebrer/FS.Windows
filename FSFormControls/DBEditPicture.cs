#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public class DBEditPicture : DBUserControl
    {
        #region DrawObjectType enum

        public enum DrawObjectType
        {
            Line = 0,
            Circle = 1,
            Rectangle = 2,
            Point = 3
        }

        #endregion

        private readonly ArrayList DrawObjects = new ArrayList();

        private int _X, _Y;
        private Bitmap bmpBack;
        private Color colorSel = Color.Black;
        private bool drawing;
        private DrawObject drawingObject;
        private DrawObjectType drawType = DrawObjectType.Point;
        private int firstX, firstY;
        private bool isSelected;
        private string m_Data;
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;

        private int secondX, secondY;

        public int PointSizeX { get; set; } = 9;

        public int PointSizeY { get; set; } = 9;

        public new BorderStyle BorderStyle
        {
            get { return PictureBox1.BorderStyle; }
            set { PictureBox1.BorderStyle = value; }
        }

        public PictureBoxSizeMode SizeMode
        {
            get { return PictureBox1.SizeMode; }
            set { PictureBox1.SizeMode = value; }
        }


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }


        private string m_DBField;
        [Description("Campo de la base de datos a enlazar.")]
        public string DBField
        {
            get { return m_DBField; }
            set { m_DBField = value; }
        }

        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;

                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        ContextMenu1.Items[0].Enabled = false;
                        ToolBar1.Visible = false;
                        ToolBar2.Visible = false;
                        break;
                    case Global.AccessMode.WriteMode:
                        ContextMenu1.Items[0].Enabled = true;
                        ToolBar1.Visible = true;
                        ToolBar2.Visible = true;
                        break;
                }
            }
        }

        public Image Image
        {
            get { return PictureBox1.Image; }
            set
            {
                PictureBox1.Image = value;
                if (value != null)
                {
                    bmpBack = (Bitmap) PictureBox1.Image;
                    PictureBox1.Image = (Image) bmpBack.Clone();
                    PictureBox1.SendToBack();
                }
            }
        }

        public string Data
        {
            get
            {
                m_Data = GenerateData();
                return m_Data;
            }
            set
            {
                m_Data = value;
                DrawFromData(value);
            }
        }

        public void UpdateImage()
        {
            //if (DataBindings.Count > 0) return;

            //Binding dbnPicture = new Binding("Data", DataControl.DataTable, DBField);

            Data = DataControl.GetField(DBField).ToString();

            //try
            //{
            //    DataBindings.Add(dbnPicture);
            //}
            //catch (System.Exception e)
            //{
            //    throw new ExceptionUtil(e);
            //}
        }

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog1.Filter =
                    "Todos los formatos gr?ficos|*.gif;*.jpg;*.ico;*.wmf|JPEG (*.jpg)|*.jpg|Mapa de bits (*.bmp)|*.bmp|GIF (*.gif)|*.gif|Metarchivo (*.wmf)|*.wmf|Icono (*.ico)|*.ico|Todos los archivos (*.*)|*.*";
                OpenFileDialog1.ShowDialog();
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName);
            }
            catch (Exception)
            {
                throw new ExceptionUtil("Formato de imagen, no valido.");
            }
        }


        private void ToolBar1_ButtonClick(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            switch (Convert.ToString(tsi.Tag))
            {
                case "ERASE":
                    if (DrawObjects.Count == 0) return;

                    var dobj = (DrawObject) DrawObjects[DrawObjects.Count - 1];
                    PictureBox1.Controls.Remove(dobj.mark1);
                    PictureBox1.Controls.Remove(dobj.mark2);
                    PictureBox1.Controls.Remove(dobj.mark3);

                    DrawObjects.Remove(dobj);

                    Redraw();
                    break;
                case "LINE":
                    drawType = DrawObjectType.Line;

                    UnPush(ToolBar1);
                    //tsi.Pushed = true;
                    break;
                case "CIRCLE":
                    drawType = DrawObjectType.Circle;

                    UnPush(ToolBar1);
                    //tsi.Pushed = true;
                    break;
                case "POINT":
                    drawType = DrawObjectType.Point;

                    UnPush(ToolBar1);
                    //tsi.Pushed = true;
                    break;
                case "RECTANGLE":
                    drawType = DrawObjectType.Rectangle;

                    UnPush(ToolBar1);
                    //tsi.Pushed = true;
                    break;
            }
        }


        private void UnPush(ToolStrip tb)
        {
            //var f = 0;
            //for (f = 0; f <= tb.Items.Count - 1; f++) 
                //tb.Items[f].Pushed = false;
        }


        private void Draw(DrawObject obj)
        {
            Graphics g = null;
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            var t = 0;

            g = Graphics.FromImage(PictureBox1.Image);

            switch (obj.Type)
            {
                case DrawObjectType.Line:
                    g.DrawLine(new Pen(obj.Color, obj.Width), obj.mark1.Center.X, obj.mark1.Center.Y,
                        obj.mark2.Center.X,
                        obj.mark2.Center.Y);
                    break;
                case DrawObjectType.Circle:
                    g.DrawEllipse(new Pen(obj.Color, obj.Width), obj.mark1.Center.X, obj.mark1.Center.Y,
                        obj.mark2.Center.X - obj.mark1.Center.X, obj.mark2.Center.Y - obj.mark1.Center.Y);
                    break;
                case DrawObjectType.Rectangle:
                    x1 = obj.mark1.Center.X;
                    y1 = obj.mark1.Center.Y;
                    x2 = obj.mark2.Center.X;
                    y2 = obj.mark2.Center.Y;
                    if (x1 > x2)
                    {
                        t = x1;
                        x1 = x2;
                        x2 = t;
                    }

                    if (y1 > y2)
                    {
                        t = y1;
                        y1 = y2;
                        y2 = t;
                    }

                    g.DrawRectangle(new Pen(obj.Color, obj.Width), x1, y1, x2 - x1, y2 - y1);
                    break;
            }


            if (!string.IsNullOrEmpty(obj.Info))
                g.DrawString(obj.Info, new Font("Arial", 7, FontStyle.Regular), new SolidBrush(Color.DarkSlateBlue),
                    obj.mark1.X, obj.mark1.Y + obj.mark1.SizeY);

            g.Dispose();
        }


        private void Draw(int x1, int y1, int x2, int y2, Color color, DrawObjectType type)
        {
            Graphics g = null;
            var t = 0;

            g = Graphics.FromImage(PictureBox1.Image);

            switch (type)
            {
                case DrawObjectType.Line:
                    g.DrawLine(new Pen(color, 1), x1, y1, x2, y2);
                    break;
                case DrawObjectType.Circle:
                    g.DrawEllipse(new Pen(color, 1), x1, y1, x2 - x1, y2 - y1);
                    break;
                case DrawObjectType.Rectangle:
                    if (x1 > x2)
                    {
                        t = x1;
                        x1 = x2;
                        x2 = t;
                    }

                    if (y1 > y2)
                    {
                        t = y1;
                        y1 = y2;
                        y2 = t;
                    }

                    g.DrawRectangle(new Pen(color, 1), x1, y1, x2 - x1, y2 - y1);
                    break;
            }


            g.Dispose();
        }


        private void Redraw(DrawObject obj, Point p)
        {
            if (bmpBack != null)
                Graphics.FromImage(PictureBox1.Image).DrawImage(bmpBack, 0, 0, PictureBox1.Image.Width,
                    PictureBox1.Image.Height);

            foreach (DrawObject d in DrawObjects) Draw(d);

            var r = getRegionByObject(obj, p);

            PictureBox1.Invalidate(r);
            PictureBox1.Update();
        }


        public void Redraw()
        {
            if (!(bmpBack == null))
            {
                PictureBox1.Image = (Image) bmpBack.Clone();
            }
            else
            {
                PictureBox1.Image = new Bitmap(PictureBox1.Width, PictureBox1.Height);
                Graphics.FromImage(PictureBox1.Image).Clear(Color.Transparent);
            }

            PictureBox1.SendToBack();

            foreach (DrawObject d in DrawObjects) Draw(d);
            PictureBox1.Refresh();
        }


        private void Mark_MouseDown(object sender, MouseEventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode) return;

            SuspendLayout();
            isSelected = true;
            _X = e.X;
            _Y = e.Y;
        }


        private void Mark_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode) return;

            if (isSelected)
            {
                var mc1 = (DBMarkControl) sender;
                var o = getObjectByMark(mc1);

                var p = new Point(e.X - _X + mc1.Left, e.Y - _Y + mc1.Top);
                mc1.Location = p;

                if (!(o == null))
                {
                    if (mc1.IsMoveMark)
                    {
                        var p1 = new Point(e.X - _X + o.mark1.Left, e.Y - _Y + o.mark1.Top);
                        var p2 = new Point(e.X - _X + o.mark2.Left, e.Y - _Y + o.mark2.Top);

                        o.mark1.Location = p1;
                        o.mark2.Location = p2;
                    }

                    o.mark3.Location = new Point(Convert.ToInt32((o.mark1.Location.X + o.mark2.Location.X) / 2),
                        Convert.ToInt32((o.mark1.Location.Y + o.mark2.Location.Y) / 2));
                    Redraw(o, p);
                }
            }
        }


        private void Mark_MouseUp(object sender, MouseEventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode) return;

            isSelected = false;
            ResumeLayout();
            Redraw();
        }


        private DrawObject getObjectByMark(DBMarkControl m)
        {
            foreach (DrawObject o in DrawObjects)
                if ((o.mark1 == m) | (o.mark2 == m) | (o.mark3 == m))
                    return o;
            return null;
        }


        private Region getRegionByObject(DrawObject o, Point p)
        {
            var gp = new GraphicsPath();
            gp.AddPolygon(new[] {o.mark1.Center, o.mark2.Center, p, o.mark1.Center});

            var rf = gp.GetBounds();
            gp.Dispose();
            rf.Inflate(100.0F, 100.0F);

            return new Region(rf);
        }


        private Region getRegionByObject(int X1, int Y1, int X2, int Y2, Point p)
        {
            var gp = new GraphicsPath();
            gp.AddPolygon(new[] {new Point(X1, Y1), new Point(X2, Y2), p, new Point(X1, Y1)});

            var rf = gp.GetBounds();
            gp.Dispose();
            rf.Inflate(100.0F, 100.0F);

            return new Region(rf);
        }


        private void ToolBar2_ButtonClick(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            switch (Convert.ToString(tsi.Tag))
            {
                case "BLACK":
                    colorSel = Color.Black;

                    UnPush(ToolBar2);
                    //e.Button.Pushed = true;
                    break;
                case "YELLOW":
                    colorSel = Color.Yellow;

                    UnPush(ToolBar2);
                    //e.Button.Pushed = true;
                    break;
                case "RED":
                    colorSel = Color.Red;

                    UnPush(ToolBar2);
                    //e.Button.Pushed = true;
                    break;
                case "WHITE":
                    colorSel = Color.White;

                    UnPush(ToolBar2);
                    //e.Button.Pushed = true;
                    break;
                case "CYAN":
                    colorSel = Color.Cyan;

                    UnPush(ToolBar2);
                    //e.Button.Pushed = true;
                    break;
                case "MAGENTA":
                    colorSel = Color.Magenta;

                    UnPush(ToolBar2);
                    //e.Button.Pushed = true;
                    break;
                case "BLUE":
                    colorSel = Color.Blue;

                    UnPush(ToolBar2);
                    //e.Button.Pushed = true;
                    break;
                case "GREEN":
                    colorSel = Color.Green;

                    UnPush(ToolBar2);
                    //e.Button.Pushed = true;
                    break;
            }
        }


        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode) return;

            if (PictureBox1.Image == null) return;

            if (drawing)
            {
                secondX = e.X;
                secondY = e.Y;

                if (bmpBack != null)
                    Graphics.FromImage(PictureBox1.Image).DrawImage(bmpBack, 0, 0, PictureBox1.Image.Width,
                        PictureBox1.Image.Height);

                Draw(firstX, firstY, secondX, secondY, colorSel, drawType);

                foreach (DrawObject d in DrawObjects) Draw(d);

                var r = getRegionByObject(firstX, firstY, secondX, secondY, new Point(secondX, secondY));

                PictureBox1.Invalidate(r);
                PictureBox1.Update();
            }
            else
            {
                firstX = e.X;
                firstY = e.Y;
            }
        }


        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode) return;

            if (e.Button == MouseButtons.Right) return;

            if (!drawing)
            {
                drawing = true;
                SuspendLayout();
            }
            else
            {
                CreateDrawObject(e.X, e.Y, false);
            }

            base.OnMouseDown(e);
        }


        private void CreateDrawObject(int x, int y, bool isPoint)
        {
            if (isPoint)
            {
                secondX = x;
                secondY = y;
            }

            drawingObject = new DrawObject(PointSizeX, PointSizeY);
            drawingObject.Color = colorSel;
            drawingObject.mark1.Location = new Point(Convert.ToInt32(firstX - (drawingObject.mark1.SizeX - 1) / 2),
                Convert.ToInt32(firstY - (drawingObject.mark1.SizeY - 1) / 2));
            drawingObject.mark2.Location = new Point(Convert.ToInt32(x - (drawingObject.mark1.SizeX - 1) / 2),
                Convert.ToInt32(y - (drawingObject.mark1.SizeY - 1) / 2));
            drawingObject.mark3.Location =
                new Point(
                    Convert.ToInt32((firstX - (drawingObject.mark1.SizeX - 1) / 2 + x -
                                     (drawingObject.mark1.SizeX - 1) / 2) / 2),
                    Convert.ToInt32((firstY - (drawingObject.mark1.SizeY - 1) / 2 + y -
                                     (drawingObject.mark1.SizeY - 1) / 2) / 2));

            drawingObject.mark3.IsMoveMark = true;
            if (isPoint)
                drawingObject.mark3.BackColor = colorSel;
            else
                drawingObject.mark3.BackColor = Color.Green;

            drawingObject.Type = drawType;

            drawingObject.mark1.ContextMenuStrip = objectMenu;
            drawingObject.mark2.ContextMenuStrip = objectMenu;
            drawingObject.mark3.ContextMenuStrip = objectMenu;

            if (!isPoint)
            {
                drawingObject.mark1.MouseUp += Mark_MouseUp;
                drawingObject.mark1.MouseDown += Mark_MouseDown;
                drawingObject.mark1.MouseMove += Mark_MouseMove;

                drawingObject.mark2.MouseUp += Mark_MouseUp;
                drawingObject.mark2.MouseDown += Mark_MouseDown;
                drawingObject.mark2.MouseMove += Mark_MouseMove;
            }

            drawingObject.mark3.MouseUp += Mark_MouseUp;
            drawingObject.mark3.MouseDown += Mark_MouseDown;
            drawingObject.mark3.MouseMove += Mark_MouseMove;

            DrawObjects.Add(drawingObject);

            if (!isPoint)
            {
                PictureBox1.Controls.Add(drawingObject.mark1);
                PictureBox1.Controls.Add(drawingObject.mark2);
            }

            PictureBox1.Controls.Add(drawingObject.mark3);
            PictureBox1.SendToBack();

            Redraw();
            drawing = false;

            ResumeLayout();
        }


        private string GenerateData()
        {
            try
            {
                var s = "";
                foreach (DrawObject o in DrawObjects)
                {
                    switch (o.Type)
                    {
                        case DrawObjectType.Line:
                            s = s + "0;";
                            break;
                        case DrawObjectType.Circle:
                            s = s + "1;";
                            break;
                        case DrawObjectType.Rectangle:
                            s = s + "2;";
                            break;
                        case DrawObjectType.Point:
                            s = s + "3;";
                            break;
                    }


                    s = s + o.mark1.Location.X + ";";
                    s = s + o.mark1.Location.Y + ";";
                    s = s + o.mark2.Location.X + ";";
                    s = s + o.mark2.Location.Y + ";";
                    s = s + o.mark3.Location.X + ";";
                    s = s + o.mark3.Location.Y + ";";

                    s = s + o.Width + ";";
                    s = s + o.Color.ToArgb() + ";";
                    s = s + o.Info;
                    s = s + "#";
                }

                return s;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public void Clear()
        {
            PictureBox1.Controls.Clear();
            DrawObjects.Clear();
            Redraw();
        }


        private void DrawFromData(string data)
        {
            try
            {
                Clear();

                var transTemp0 = "#";
                var s = data.Split(transTemp0.Split("".ToCharArray()), StringSplitOptions.None);
                string c = null;
                var f = 0;

                for (f = 0; f <= s.Length - 1; f++)
                {
                    c = s[f];
                    if (c == "") break;

                    var transTemp1 = ";";
                    var s1 = c.Split(transTemp1.Split("".ToCharArray()), StringSplitOptions.None);

                    Add((DrawObjectType) int.Parse(s1[0]), int.Parse(s1[1]), int.Parse(s1[2]), int.Parse(s1[3]),
                        int.Parse(s1[4]), int.Parse(s1[7]), Color.FromArgb(int.Parse(s1[8])), s1[9]);
                }

                PictureBox1.SendToBack();
                Redraw();
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public void Add(DrawObjectType drawObjectType, int x1, int y1, int x2, int y2, int width, Color color,
            string info)
        {
            try
            {
                var drw = new DrawObject(PointSizeX, PointSizeY);

                drw.Type = drawObjectType;
                drw.mark1.Location = new Point(x1, y1);
                drw.mark2.Location = new Point(x2, y2);
                drw.mark3.Location = new Point(Convert.ToInt32((x1 + x2) / 2), Convert.ToInt32((y1 + y2) / 2));

                drw.Width = width;
                drw.Color = color;
                drw.Info = info;

                drw.mark1.ContextMenuStrip = objectMenu;
                drw.mark2.ContextMenuStrip = objectMenu;
                drw.mark3.ContextMenuStrip = objectMenu;

                drw.mark3.IsMoveMark = true;

                if (drw.Type == DrawObjectType.Point)
                    drw.mark3.BackColor = drw.Color;
                else
                    drw.mark3.BackColor = Color.Green;

                if (!(drw.Type == DrawObjectType.Point))
                {
                    drw.mark1.MouseUp += Mark_MouseUp;
                    drw.mark1.MouseDown += Mark_MouseDown;
                    drw.mark1.MouseMove += Mark_MouseMove;

                    drw.mark2.MouseUp += Mark_MouseUp;
                    drw.mark2.MouseDown += Mark_MouseDown;
                    drw.mark2.MouseMove += Mark_MouseMove;
                }

                drw.mark3.MouseUp += Mark_MouseUp;
                drw.mark3.MouseDown += Mark_MouseDown;
                drw.mark3.MouseMove += Mark_MouseMove;

                if (!(drw.Type == DrawObjectType.Point))
                {
                    PictureBox1.Controls.Add(drw.mark1);
                    PictureBox1.Controls.Add(drw.mark2);
                }

                PictureBox1.Controls.Add(drw.mark3);

                DrawObjects.Add(drw);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode) return;

            if (e.Button == MouseButtons.Right) return;

            if (drawType == DrawObjectType.Point)
            {
                drawing = false;
                CreateDrawObject(e.X, e.Y, true);
            }

            base.OnMouseDown(e);
        }


        private void MenuItem2_Click(object sender, EventArgs e)
        {
            var m = (DBMarkControl) objectMenu.SourceControl;

            var drwobj = getObjectByMark(m);
            if (drwobj != null)
            {
                DialogResult result = InputBox.ShowDialog("Editar información: ", "Información", drwobj.Info, InputBox.Icon.Information);
                drwobj.Info = InputBox.ResultValue;
                Redraw();
            }
        }


        private void MenuItem3_Click(object sender, EventArgs e)
        {
            var m = (DBMarkControl) objectMenu.SourceControl;

            var drwobj = getObjectByMark(m);
            if (drwobj != null)
                if (!string.IsNullOrEmpty(drwobj.Info))
                    MessageBox.Show(drwobj.Info);
        }


        private void MenuItem4_Click(object sender, EventArgs e)
        {
            var m = (DBMarkControl) objectMenu.SourceControl;

            var drwobj = getObjectByMark(m);

            object transTemp4 = drwobj;
            if (!(transTemp4 == null))
            {
                PictureBox1.Controls.Remove(drwobj.mark1);
                PictureBox1.Controls.Remove(drwobj.mark2);
                PictureBox1.Controls.Remove(drwobj.mark3);

                DrawObjects.Remove(drwobj);

                Redraw();
            }
        }

        #region Nested type: DrawObject

        private class DrawObject
        {
            private readonly int m_PointSizeX = 9;
            private readonly int m_PointSizeY = 9;

            public readonly DBMarkControl mark1;
            public readonly DBMarkControl mark2;
            public readonly DBMarkControl mark3;
            public Color Color;
            public string Info = "";
            public DrawObjectType Type;
            public int Width = 1;

            public DrawObject(int sizeX, int sizeY)
            {
                m_PointSizeX = sizeX;
                m_PointSizeY = sizeY;

                mark1 = new DBMarkControl(m_PointSizeX, m_PointSizeY);
                mark2 = new DBMarkControl(m_PointSizeX, m_PointSizeY);
                mark3 = new DBMarkControl(m_PointSizeX, m_PointSizeY);
            }
        }

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        internal ContextMenuStrip ContextMenu1;
        internal ImageList ImageList1;
        internal ToolStripMenuItem MenuItem1;
        internal ToolStripMenuItem MenuItem2;
        internal ToolStripMenuItem MenuItem3;
        internal ToolStripMenuItem MenuItem4;
        internal OpenFileDialog OpenFileDialog1;
        internal PictureBox PictureBox1;
        internal ToolStrip ToolBar1;
        internal ToolStrip ToolBar2;
        internal ToolStripButton ToolBarButton10;
        internal ToolStripButton ToolBarButton11;
        internal ToolStripButton ToolBarButton12;
        internal ToolStripButton ToolBarButton13;
        internal ToolStripButton ToolBarButton14;
        internal ToolStripButton ToolBarButton2;
        internal ToolStripButton ToolBarButton3;
        internal ToolStripButton ToolBarButton4;
        internal ToolStripButton ToolBarButton5;
        internal ToolStripButton ToolBarButton6;
        internal ToolStripButton ToolBarButton7;
        internal ToolStripButton ToolBarButton8;
        internal ToolStripButton ToolBarButton9;
        internal ToolTip ToolTip1;
        private IContainer components;
        public ContextMenuStrip objectMenu;

        public DBEditPicture()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            MenuItem1.Click += MenuItem1_Click;
            ToolBar1.ItemClicked += ToolBar1_ButtonClick;
            ToolBar2.ItemClicked += ToolBar2_ButtonClick;
            PictureBox1.MouseMove += PictureBox1_MouseMove;
            PictureBox1.MouseDown += PictureBox1_MouseDown;
            PictureBox1.MouseUp += PictureBox1_MouseUp;
            MenuItem2.Click += MenuItem2_Click;
            MenuItem3.Click += MenuItem3_Click;
            MenuItem4.Click += MenuItem4_Click;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(DBEditPicture));
            ContextMenu1 = new ContextMenuStrip(components);
            MenuItem1 = new ToolStripMenuItem();
            OpenFileDialog1 = new OpenFileDialog();
            ToolTip1 = new ToolTip(components);
            ToolBar1 = new ToolStrip();
            ImageList1 = new ImageList(components);
            ToolBarButton9 = new ToolStripButton();
            ToolBarButton2 = new ToolStripButton();
            ToolBarButton3 = new ToolStripButton();
            ToolBarButton4 = new ToolStripButton();
            ToolBarButton5 = new ToolStripButton();
            ToolBar2 = new ToolStrip();
            ToolBarButton10 = new ToolStripButton();
            ToolBarButton11 = new ToolStripButton();
            ToolBarButton12 = new ToolStripButton();
            ToolBarButton13 = new ToolStripButton();
            ToolBarButton14 = new ToolStripButton();
            ToolBarButton6 = new ToolStripButton();
            ToolBarButton7 = new ToolStripButton();
            ToolBarButton8 = new ToolStripButton();
            PictureBox1 = new PictureBox();
            objectMenu = new ContextMenuStrip(components);
            MenuItem2 = new ToolStripMenuItem();
            MenuItem3 = new ToolStripMenuItem();
            MenuItem4 = new ToolStripMenuItem();
            ContextMenu1.SuspendLayout();
            ToolBar1.SuspendLayout();
            ToolBar2.SuspendLayout();
            ((ISupportInitialize)PictureBox1).BeginInit();
            objectMenu.SuspendLayout();
            SuspendLayout();
            // 
            // ContextMenu1
            // 
            ContextMenu1.ImageScalingSize = new Size(20, 20);
            ContextMenu1.Items.AddRange(new ToolStripItem[] { MenuItem1 });
            ContextMenu1.Name = "ContextMenu1";
            ContextMenu1.Size = new Size(209, 28);
            // 
            // MenuItem1
            // 
            MenuItem1.Name = "MenuItem1";
            MenuItem1.Size = new Size(208, 24);
            MenuItem1.Text = "Seleccionar Imagen";
            // 
            // ToolBar1
            // 
            ToolBar1.AutoSize = false;
            ToolBar1.Dock = DockStyle.None;
            ToolBar1.ImageList = ImageList1;
            ToolBar1.ImageScalingSize = new Size(20, 20);
            ToolBar1.Items.AddRange(new ToolStripItem[] { ToolBarButton9, ToolBarButton2, ToolBarButton3, ToolBarButton4, ToolBarButton5 });
            ToolBar1.LayoutStyle = ToolStripLayoutStyle.Flow;
            ToolBar1.Location = new Point(406, 3);
            ToolBar1.Name = "ToolBar1";
            ToolBar1.Size = new Size(99, 191);
            ToolBar1.TabIndex = 0;
            // 
            // ImageList1
            // 
            ImageList1.ColorDepth = ColorDepth.Depth8Bit;
            ImageList1.ImageStream = (ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
            ImageList1.TransparentColor = Color.Transparent;
            ImageList1.Images.SetKeyName(0, "");
            ImageList1.Images.SetKeyName(1, "");
            ImageList1.Images.SetKeyName(2, "");
            ImageList1.Images.SetKeyName(3, "");
            ImageList1.Images.SetKeyName(4, "");
            ImageList1.Images.SetKeyName(5, "");
            ImageList1.Images.SetKeyName(6, "");
            ImageList1.Images.SetKeyName(7, "");
            ImageList1.Images.SetKeyName(8, "");
            ImageList1.Images.SetKeyName(9, "");
            ImageList1.Images.SetKeyName(10, "");
            ImageList1.Images.SetKeyName(11, "");
            ImageList1.Images.SetKeyName(12, "");
            ImageList1.Images.SetKeyName(13, "");
            ImageList1.Images.SetKeyName(14, "");
            // 
            // ToolBarButton9
            // 
            ToolBarButton9.ImageIndex = 6;
            ToolBarButton9.Name = "ToolBarButton9";
            ToolBarButton9.Size = new Size(97, 24);
            ToolBarButton9.Tag = "POINT";
            ToolBarButton9.Text = "Punto";
            ToolBarButton9.ToolTipText = "Punto";
            // 
            // ToolBarButton2
            // 
            ToolBarButton2.ImageIndex = 5;
            ToolBarButton2.Name = "ToolBarButton2";
            ToolBarButton2.Size = new Size(97, 24);
            ToolBarButton2.Tag = "LINE";
            ToolBarButton2.Text = "Linea";
            ToolBarButton2.ToolTipText = "Linea";
            // 
            // ToolBarButton3
            // 
            ToolBarButton3.ImageIndex = 1;
            ToolBarButton3.Name = "ToolBarButton3";
            ToolBarButton3.Size = new Size(97, 24);
            ToolBarButton3.Tag = "CIRCLE";
            ToolBarButton3.Text = "Circulo";
            ToolBarButton3.ToolTipText = "C?rculo";
            // 
            // ToolBarButton4
            // 
            ToolBarButton4.ImageIndex = 4;
            ToolBarButton4.Name = "ToolBarButton4";
            ToolBarButton4.Size = new Size(97, 24);
            ToolBarButton4.Tag = "RECTANGLE";
            ToolBarButton4.Text = "Cuadrado";
            ToolBarButton4.ToolTipText = "Rect?ngulo";
            // 
            // ToolBarButton5
            // 
            ToolBarButton5.ImageIndex = 2;
            ToolBarButton5.Name = "ToolBarButton5";
            ToolBarButton5.Size = new Size(97, 24);
            ToolBarButton5.Tag = "ERASE";
            ToolBarButton5.Text = "Borrar";
            ToolBarButton5.ToolTipText = "Borrar";
            // 
            // ToolBar2
            // 
            ToolBar2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ToolBar2.Dock = DockStyle.None;
            ToolBar2.ImageList = ImageList1;
            ToolBar2.ImageScalingSize = new Size(20, 20);
            ToolBar2.Items.AddRange(new ToolStripItem[] { ToolBarButton10, ToolBarButton11, ToolBarButton12, ToolBarButton13, ToolBarButton14, ToolBarButton6, ToolBarButton7, ToolBarButton8 });
            ToolBar2.Location = new Point(0, 325);
            ToolBar2.Name = "ToolBar2";
            ToolBar2.Size = new Size(245, 27);
            ToolBar2.TabIndex = 1;
            // 
            // ToolBarButton10
            // 
            ToolBarButton10.ImageIndex = 14;
            ToolBarButton10.Name = "ToolBarButton10";
            ToolBarButton10.Size = new Size(29, 24);
            ToolBarButton10.Tag = "BLACK";
            // 
            // ToolBarButton11
            // 
            ToolBarButton11.ImageIndex = 13;
            ToolBarButton11.Name = "ToolBarButton11";
            ToolBarButton11.Size = new Size(29, 24);
            ToolBarButton11.Tag = "WHITE";
            // 
            // ToolBarButton12
            // 
            ToolBarButton12.ImageIndex = 8;
            ToolBarButton12.Name = "ToolBarButton12";
            ToolBarButton12.Size = new Size(29, 24);
            ToolBarButton12.Tag = "RED";
            // 
            // ToolBarButton13
            // 
            ToolBarButton13.ImageIndex = 7;
            ToolBarButton13.Name = "ToolBarButton13";
            ToolBarButton13.Size = new Size(29, 24);
            ToolBarButton13.Tag = "YELLOW";
            // 
            // ToolBarButton14
            // 
            ToolBarButton14.ImageIndex = 11;
            ToolBarButton14.Name = "ToolBarButton14";
            ToolBarButton14.Size = new Size(29, 24);
            ToolBarButton14.Tag = "CYAN";
            // 
            // ToolBarButton6
            // 
            ToolBarButton6.ImageIndex = 10;
            ToolBarButton6.Name = "ToolBarButton6";
            ToolBarButton6.Size = new Size(29, 24);
            ToolBarButton6.Tag = "GREEN";
            // 
            // ToolBarButton7
            // 
            ToolBarButton7.ImageIndex = 12;
            ToolBarButton7.Name = "ToolBarButton7";
            ToolBarButton7.Size = new Size(29, 24);
            ToolBarButton7.Tag = "BLUE";
            // 
            // ToolBarButton8
            // 
            ToolBarButton8.ImageIndex = 9;
            ToolBarButton8.Name = "ToolBarButton8";
            ToolBarButton8.Size = new Size(29, 24);
            ToolBarButton8.Tag = "MAGENTA";
            // 
            // PictureBox1
            // 
            PictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PictureBox1.BorderStyle = BorderStyle.FixedSingle;
            PictureBox1.Location = new Point(3, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(400, 319);
            PictureBox1.TabIndex = 2;
            PictureBox1.TabStop = false;
            // 
            // objectMenu
            // 
            objectMenu.ImageScalingSize = new Size(20, 20);
            objectMenu.Items.AddRange(new ToolStripItem[] { MenuItem2, MenuItem3, MenuItem4 });
            objectMenu.Name = "objectMenu";
            objectMenu.Size = new Size(202, 76);
            // 
            // MenuItem2
            // 
            MenuItem2.Name = "MenuItem2";
            MenuItem2.Size = new Size(201, 24);
            MenuItem2.Text = "Editar Información";
            // 
            // MenuItem3
            // 
            MenuItem3.Name = "MenuItem3";
            MenuItem3.Size = new Size(201, 24);
            MenuItem3.Text = "Ver Información";
            // 
            // MenuItem4
            // 
            MenuItem4.Name = "MenuItem4";
            MenuItem4.Size = new Size(201, 24);
            MenuItem4.Text = "Borrar";
            // 
            // DBEditPicture
            // 
            Controls.Add(ToolBar2);
            Controls.Add(ToolBar1);
            Controls.Add(PictureBox1);
            Name = "DBEditPicture";
            Size = new Size(505, 352);
            ContextMenu1.ResumeLayout(false);
            ToolBar1.ResumeLayout(false);
            ToolBar1.PerformLayout();
            ToolBar2.ResumeLayout(false);
            ToolBar2.PerformLayout();
            ((ISupportInitialize)PictureBox1).EndInit();
            objectMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}