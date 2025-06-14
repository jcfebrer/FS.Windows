#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FSGraphics;
using FSLibrary;

#endregion


namespace FSFormControls
{
    [Designer(typeof(ParentControlDesigner))]
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBFrame.bmp")]
    [ToolboxItem(true)]
    public class DBFrame : DBUserControl
    {
        #region Delegates

        public delegate void CloseClickEventHandler();

        #endregion

        #region FrameType enum

        public enum FrameType
        {
            Rectangle,
            RoundedRectangle
        }

        #endregion

        public Color m_backColor = Color.LightYellow;
        public Color m_borderColor = Color.Black;
        public int m_borderDiameter = 30;
        public int m_borderSize = 1;

        public ToolTip m_CloseToolTip = new ToolTip();

        public FrameType m_FrameType = FrameType.Rectangle;
        public bool m_Shadow = true;
        public Color m_ShadowColor = Color.Gray;
        public int m_shadowSize = 8;
        public bool m_ShowClose;
        public string m_Text = "";
        public Color m_TextColor = Color.Black;
        public int m_xPos = 10;
        public int m_yPos = 10;

        public Color TextColor
        {
            get { return m_TextColor; }
            set
            {
                m_TextColor = value;
                Invalidate();
            }
        }

        public int TextXPos
        {
            get { return m_xPos; }
            set
            {
                m_xPos = value;
                Invalidate();
            }
        }

        public int TextYPos
        {
            get { return m_yPos; }
            set
            {
                m_yPos = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return m_Text; }
            set
            {
                m_Text = value;
                Invalidate();
            }
        }

        public bool Shadow
        {
            get { return m_Shadow; }
            set
            {
                m_Shadow = value;
                Invalidate();
            }
        }

        public bool ShowClose
        {
            get { return m_ShowClose; }
            set
            {
                m_ShowClose = value;
                Invalidate();
            }
        }

        public FrameType Type
        {
            get { return m_FrameType; }
            set
            {
                m_FrameType = value;
                Invalidate();
            }
        }

        public int BorderDiameter
        {
            get { return m_borderDiameter; }
            set
            {
                m_borderDiameter = value;
                Invalidate();
            }
        }

        public int BorderSize
        {
            get { return m_borderSize; }
            set
            {
                m_borderSize = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return m_borderColor; }
            set
            {
                m_borderColor = value;
                Invalidate();
            }
        }

        public int ShadowSize
        {
            get { return m_shadowSize; }
            set
            {
                m_shadowSize = value;
                Invalidate();
            }
        }

        public Color FrameColor
        {
            get { return m_backColor; }
            set
            {
                m_backColor = value;
                Invalidate();
            }
        }

        public Color ShadowColor
        {
            get { return m_ShadowColor; }
            set
            {
                m_ShadowColor = value;
                Invalidate();
            }
        }

        public event CloseClickEventHandler CloseClick;


        protected override void OnControlAdded(ControlEventArgs e)
        {
        }


        private void DBFrame_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var sha = m_shadowSize;
            if (!Shadow) sha = 0;

            switch (m_FrameType)
            {
                case FrameType.Rectangle:
                    g.FillRectangle(new SolidBrush(m_ShadowColor),
                        new Rectangle(sha, sha, Width - 1 - sha, Height - 1 - sha));
                    g.FillRectangle(new SolidBrush(m_backColor),
                        new Rectangle(0, 0, Width - 1 - sha, Height - 1 - sha));
                    g.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, Width - 1 - sha, Height - 1 - sha));
                    break;
                case FrameType.RoundedRectangle:
                    GraphicsUtil.DrawRoundedRectangle(g, new SolidBrush(m_ShadowColor),
                        new Rectangle(m_shadowSize, sha, Width - 1 - sha,
                            Height - 1 - sha), m_borderDiameter);
                    GraphicsUtil.DrawRoundedRectangle(g, new SolidBrush(m_backColor),
                        new Rectangle(0, 0, Width - 1 - sha, Height - 1 - sha),
                        m_borderDiameter);
                    GraphicsUtil.DrawRoundedRectangle(g, Pens.Black,
                        new Rectangle(0, 0, Width - 1 - sha, Height - 1 - sha),
                        m_borderDiameter);
                    break;
            }


            g.DrawString(m_Text, Font, new SolidBrush(m_TextColor), m_xPos, m_yPos);

            if (m_ShowClose)
                g.DrawString("X", new Font("Arial", 8, FontStyle.Bold), new SolidBrush(Color.Black), Width - 15 - sha,
                    5);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            var sha = m_shadowSize;
            if (!Shadow) sha = 0;

            if ((e.Y <= 15) & (e.X >= Width - 15 - sha))
            {
                Cursor.Current = Cursors.Hand;
                m_CloseToolTip.Active = true;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                m_CloseToolTip.Active = false;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var sha = m_shadowSize;
            if (!Shadow) sha = 0;

            if (e.Button != MouseButtons.Left) return;
            if ((e.Y <= 15) & (e.X >= Width - 15 - sha))
            {
                Visible = false;
                if (null != CloseClick) CloseClick();
            }
        }


        private void DBFrame_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        public DBFrame()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

            m_CloseToolTip.SetToolTip(this, "Cerrar");
            m_CloseToolTip.Active = false;

            Paint += DBFrame_Paint;
            Resize += DBFrame_Resize;
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
            SuspendLayout();
            // 
            // DBFrame
            // 
            Name = "DBFrame";
            Size = new Size(275, 182);
            ResumeLayout(false);
        }

        #endregion
    }
}