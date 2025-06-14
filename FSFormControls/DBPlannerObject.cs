#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FSLibrary;
using FSException;
using FSGraphics;

#endregion

namespace FSFormControls
{
    public partial class DBPlannerObject
    {
        public Color m_BackColor = Color.White;

        public DBPlannerCollectionData m_Blocks = new DBPlannerCollectionData();
        public bool m_DrawBorder = true;
        public bool m_FixedSize;

        public DBPlannerObject()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            // events handled by DBPlannerObject_Resize

            Resize += DBPlannerObject_Resize;
        }

        public bool FixedSize
        {
            get { return m_FixedSize; }
            set { m_FixedSize = value; }
        }

        public DBPlannerCollectionData Blocks
        {
            get { return m_Blocks; }
            set { m_Blocks = value; }
        }

        public bool DrawBorder
        {
            get { return m_DrawBorder; }
            set { m_DrawBorder = value; }
        }

        public new Color BackColor
        {
            get { return m_BackColor; }
            set { m_BackColor = value; }
        }

        public event MouseOverEventHandler MouseOver;
        public new event MouseClickEventHandler MouseClick;

        private void DBPlannerObject_Resize(object sender, EventArgs e)
        {
            var f = 0;

            if (m_FixedSize) return;

            for (f = 0; f <= m_Blocks.Count - 1; f++)
            {
                Controls[m_Blocks.get_Item(f).Name].Width =
                    Convert.ToInt32(m_Blocks.get_Item(f).Size * Width / m_Blocks.TotalSize());

                if (f > 0)
                    Controls[m_Blocks.get_Item(f).Name].Left = Controls[m_Blocks.get_Item(f - 1).Name].Left +
                                                               Controls[m_Blocks.get_Item(f - 1).Name].Width;
                else
                    Controls[m_Blocks.get_Item(f).Name].Left = 0;
            }
        }

        public void Initialize()
        {
            var f = 0;

            try
            {
                for (f = 0; f <= m_Blocks.Count - 1; f++)
                {
                    var pb = new PictureBox();

                    pb.Name = m_Blocks.get_Item(f).Name;

                    if (m_FixedSize)
                        pb.Width = m_Blocks.get_Item(f).Size;
                    else
                        pb.Width = Convert.ToInt32(m_Blocks.get_Item(f).Size * Width / m_Blocks.TotalSize());
                    pb.Height = Height;

                    if (f > 0)
                        pb.Left = Controls[m_Blocks.get_Item(f - 1).Name].Left +
                                  Controls[m_Blocks.get_Item(f - 1).Name].Width;
                    else
                        pb.Left = 0;


                    pb.MouseHover += OnMouseOver;
                    pb.MouseClick += OnMouseClick;
                    pb.Paint += Paint;

                    Controls.Add(pb);
                }

                if (m_FixedSize) Width = m_Blocks.TotalSize();
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public new void Paint(object sender, PaintEventArgs e)
        {
            var pic = (PictureBox) sender;
            var rec = new Rectangle(0, 0, pic.Width, pic.Height - 1);
            var dbp = m_Blocks.Find(pic.Name);
            var cellformat = new StringFormat();

            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.Clear(m_BackColor);

            var lgBrush = new LinearGradientBrush(rec, GraphicsUtil.GetLightColor(dbp.Color, 80),
                GraphicsUtil.GetDarkColor(dbp.Color, 55),
                LinearGradientMode.BackwardDiagonal);

            if (!string.IsNullOrEmpty(dbp.Text))
            {
                switch (dbp.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        cellformat.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Left:
                        cellformat.Alignment = StringAlignment.Near;
                        break;
                    case HorizontalAlignment.Right:
                        cellformat.Alignment = StringAlignment.Far;
                        break;
                }


                e.Graphics.FillRectangle(lgBrush, rec);
                e.Graphics.DrawString(dbp.Text, dbp.Font, new SolidBrush(dbp.TextColor), rec, cellformat);
            }
            else
            {
                e.Graphics.FillRectangle(lgBrush, rec);
            }

            if (DrawBorder) e.Graphics.DrawRectangle(Pens.Black, rec);
        }


        public void OnMouseOver(object sender, EventArgs e)
        {
            var tt = new ToolTip();
            var dbp = m_Blocks.Find(((PictureBox) sender).Name);
            tt.SetToolTip((Control) sender, dbp.Text);
            if (null != MouseOver) MouseOver(sender, e);
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (null != MouseClick) MouseClick(sender, e);
        }

        #region Delegates

        public delegate void MouseClickEventHandler(object sender, MouseEventArgs e);

        public delegate void MouseOverEventHandler(object sender, EventArgs e);

        #endregion
    }


    public class DBPlannerData
    {
        public Color m_Color;
        public Font m_Font;
        public bool m_middleSize;
        public string m_Name;
        public int m_Size;
        public string m_Text;
        public HorizontalAlignment m_textAlign;
        public Color m_TextColor;

        public DBPlannerData()
        {
            m_Name = "";
            m_Text = "";
            m_Size = 0;
            m_Color = Color.White;
            m_Font = new Font("Arial", 8);
            m_textAlign = HorizontalAlignment.Center;
        }

        public DBPlannerData(string name, int size, Color color)
        {
            m_Name = name;
            m_Text = "";
            m_Size = size;
            m_Color = color;
            m_Font = new Font("Arial", 8);
            m_textAlign = HorizontalAlignment.Center;
        }

        public DBPlannerData(string name, string text, int size, Color color, Font font, Color textColor,
            HorizontalAlignment textAlign)
        {
            m_Name = name;
            m_Text = text;
            m_Size = size;
            m_Color = color;
            m_Font = font;
            m_textAlign = textAlign;
            m_TextColor = textColor;
        }

        public DBPlannerData(string name, string text, int size, Color color, Font font, Color textColor,
            HorizontalAlignment textAlign, bool middleSize)
        {
            m_Name = name;
            m_Text = text;
            m_Size = size;
            m_Color = color;
            m_Font = font;
            m_textAlign = textAlign;
            m_TextColor = textColor;
            m_middleSize = middleSize;
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public int Size
        {
            get { return m_Size; }
            set { m_Size = value; }
        }

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public Color TextColor
        {
            get { return m_TextColor; }
            set { m_TextColor = value; }
        }

        public Font Font
        {
            get { return m_Font; }
            set { m_Font = value; }
        }

        public HorizontalAlignment TextAlign
        {
            get { return m_textAlign; }
            set { m_textAlign = value; }
        }

        public bool MiddleSize
        {
            get { return m_middleSize; }
            set { m_middleSize = value; }
        }
    }


    [DefaultProperty("Item")]
    public class DBPlannerCollectionData : CollectionBase
    {
        public void Add(string name, int size, Color color)
        {
            List.Add(new DBPlannerData(name, size, color));
        }


        public void Add(string name, string text, int size, Color color, Font font, Color textColor,
            HorizontalAlignment textAlign)
        {
            List.Add(new DBPlannerData(name, text, size, color, font, textColor, textAlign));
        }


        public void Add(string name, string text, int size, Color color, Font font, Color textColor,
            HorizontalAlignment textAlign, bool middleSize)
        {
            List.Add(new DBPlannerData(name, text, size, color, font, textColor, textAlign, middleSize));
        }


        public DBPlannerData get_Item(int index)
        {
            return (DBPlannerData) List[index];
        }


        public void set_Item(int index, DBPlannerData value)
        {
            List[index] = value;
        }


        public int TotalSize()
        {
            var f = 0;
            var tot = 0;
            for (f = 0; f <= List.Count - 1; f++) tot = tot + ((DBPlannerData) List[f]).Size;
            return tot;
        }


        public DBPlannerData Find(string name)
        {
            foreach (DBPlannerData dbp in List)
                if (dbp.Name.ToLower() == name.ToLower())
                    return dbp;
            return null;
        }
    }
}