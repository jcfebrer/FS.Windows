#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class TaskBarNotifier : Form
    {
        #region '"Enums"' 

        public enum TaskbarStates
        {
            Hidden = 0,
            Appearing = 1,
            Visible = 2,
            Disappearing = 3
        }

        #endregion

        protected bool m_bCloseButtonClickEnabled = true;
        protected bool m_bDrawFocusRect = true;
        protected bool m_bKeepVisibleOnMouseOver = true;
        protected bool m_bMouseDown;
        protected bool m_bMouseOverClose;
        protected bool m_bMouseOverPopup;
        protected bool m_bMouseOverText;
        protected bool m_bMouseOverTitle;

        protected Bitmap m_bmpBackground;
        protected Bitmap m_bmpCloseButton;
        protected bool m_bReShowOnMouseOver;
        protected bool m_bTextClickEnabled = true;
        protected bool m_bTitleClickEnabled;
        protected Color m_colFocusRect = Color.DarkGray;
        protected Color m_colTextHover = Color.SlateBlue;
        protected Color m_colTextNormal = Color.Black;
        protected Color m_colTitleHover = Color.Red;
        protected Color m_colTitleNormal = Color.Red;
        protected ButtonBorderStyle m_eFocusRectStyle = ButtonBorderStyle.Solid;
        protected TaskbarStates m_eWindowState = TaskbarStates.Hidden;
        protected Font m_fntTextHover = new Font("Tahoma", 10, FontStyle.Underline, GraphicsUnit.Pixel);
        protected Font m_fntTextNormal = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Pixel);
        protected Font m_fntTitleHover = new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);
        protected Font m_fntTitleNormal = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        protected int m_iHideEvents;
        protected int m_iIncrementHide;
        protected int m_iIncrementShow;
        protected int m_iShowEvents;
        protected int m_iVisibleEvents;
        protected Point m_ptCloseButtonLocation;
        protected Rectangle m_rectRealText;
        protected Rectangle m_rectRealTitle;

        protected Rectangle m_rectText;
        protected Rectangle m_rectTitle;
        protected Rectangle m_rectWorkArea;
        protected Size m_sizCloseButtonSize;
        protected string m_sText;
        protected string m_sTitle;
        protected Timer m_tmrAnimation = new Timer();

        #region '"Constructor"' 

        public TaskBarNotifier()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Minimized;
            base.Show();
            base.Hide();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = false;
            TopMost = true;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;
            SetStyle(ControlStyles.CacheText, true);

            m_tmrAnimation.Enabled = true;
            m_tmrAnimation.Tick += OnTimer;
        }

        #endregion

        #region '"OnTimer Event Handler"' 

        protected void OnTimer(object sender, EventArgs e)
        {
            switch (m_eWindowState)
            {
                case TaskbarStates.Appearing:

                    if (Height < m_bmpBackground.Height)
                    {
                        SetBounds(Left, Top - m_iIncrementShow, Width, Height + m_iIncrementShow);
                    }
                    else
                    {
                        m_tmrAnimation.Stop();
                        Height = m_bmpBackground.Height;
                        m_tmrAnimation.Interval = m_iVisibleEvents;
                        m_eWindowState = TaskbarStates.Visible;
                        m_tmrAnimation.Start();
                    }

                    break;
                case TaskbarStates.Visible:

                    m_tmrAnimation.Stop();
                    m_tmrAnimation.Interval = m_iHideEvents;
                    if (m_bKeepVisibleOnMouseOver && !m_bMouseOverPopup || !m_bKeepVisibleOnMouseOver)
                        m_eWindowState = TaskbarStates.Disappearing;
                    m_tmrAnimation.Start();

                    break;
                case TaskbarStates.Disappearing:

                    if (m_bReShowOnMouseOver && m_bMouseOverPopup)
                    {
                        m_eWindowState = TaskbarStates.Appearing;
                    }
                    else
                    {
                        if (Top < m_rectWorkArea.Bottom)
                            SetBounds(Left, Top + m_iIncrementHide, Width, Height - m_iIncrementHide);
                        else
                            Hide();
                    }

                    break;
            }
        }

        #endregion

        [DllImport("User32")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public event CloseClickHandler CloseButtonClick;

        public event TitleClickHandler TitleClick;

        public event TextClickHandler TextClick;

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // TaskBarNotifier
            // 
            ClientSize = new Size(282, 244);
            Name = "TaskBarNotifier";
            ResumeLayout(false);
        }

        #region Delegates

        public delegate void CloseClickHandler(object sender, EventArgs e);

        public delegate void TextClickHandler(object sender, EventArgs e);

        public delegate void TitleClickHandler(object sender, EventArgs e);

        #endregion

        #region '"Public Properties"' 

        public TaskbarStates TaskBarState => m_eWindowState;

        public string TitleText
        {
            get { return m_sTitle; }
            set
            {
                m_sTitle = value;
                Refresh();
            }
        }

        public string ContentText
        {
            get { return m_sText; }
            set
            {
                m_sText = value;
                Refresh();
            }
        }

        public Color NormalTitleColor
        {
            get { return m_colTitleNormal; }
            set
            {
                m_colTitleNormal = value;
                Refresh();
            }
        }

        public Color HoverTitleColor
        {
            get { return m_colTitleHover; }
            set
            {
                m_colTitleHover = value;
                Refresh();
            }
        }

        public Color NormalContentColor
        {
            get { return m_colTextNormal; }
            set
            {
                m_colTextNormal = value;
                Refresh();
            }
        }

        public Color HoverContentColor
        {
            get { return m_colTextHover; }
            set
            {
                m_colTextHover = value;
                Refresh();
            }
        }

        public Font NormalTitleFont
        {
            get { return m_fntTitleNormal; }
            set
            {
                m_fntTitleNormal = value;
                Refresh();
            }
        }

        public Font HoverTitleFont
        {
            get { return m_fntTitleHover; }
            set
            {
                m_fntTitleHover = value;
                Refresh();
            }
        }

        public Font NormalContentFont
        {
            get { return m_fntTextNormal; }
            set
            {
                m_fntTextNormal = value;
                Refresh();
            }
        }

        public Font HoverContentFont
        {
            get { return m_fntTextHover; }
            set
            {
                m_fntTextHover = value;
                Refresh();
            }
        }

        [DefaultValue(true)]
        public bool CloseButtonClickEnabled
        {
            get { return m_bCloseButtonClickEnabled; }
            set { m_bCloseButtonClickEnabled = value; }
        }

        [DefaultValue(false)]
        public bool TitleClickEnabled
        {
            get { return m_bTitleClickEnabled; }
            set { m_bTitleClickEnabled = value; }
        }

        [DefaultValue(true)]
        public bool TextClickEnabled
        {
            get { return m_bTextClickEnabled; }
            set { m_bTextClickEnabled = value; }
        }

        public Rectangle TitleRectangle
        {
            get { return m_rectTitle; }
            set { m_rectTitle = value; }
        }

        public Rectangle TextRectangle
        {
            get { return m_rectText; }
            set { m_rectText = value; }
        }

        [DefaultValue(true)]
        public bool DrawTextFocusRect
        {
            get { return m_bDrawFocusRect; }
            set { m_bDrawFocusRect = value; }
        }

        [DefaultValue(ButtonBorderStyle.Dotted)]
        public ButtonBorderStyle TextFocusRectStyle
        {
            get { return m_eFocusRectStyle; }
            set { m_eFocusRectStyle = value; }
        }

        public Color TextFocusRectColor
        {
            get { return m_colFocusRect; }
            set { m_colFocusRect = value; }
        }

        [DefaultValue(true)]
        public bool KeepVisibleOnMouseOver
        {
            get { return m_bKeepVisibleOnMouseOver; }
            set { m_bKeepVisibleOnMouseOver = value; }
        }

        [DefaultValue(false)]
        public bool ReShowOnMouseOver
        {
            get { return m_bReShowOnMouseOver; }
            set { m_bReShowOnMouseOver = value; }
        }

        #endregion

        #region '"Public Methods"' 

        public void Show(string strTitle, string strContent, int nTimeToShow, int nTimeToStay, int nTimeToHide)
        {
            m_rectWorkArea = Screen.GetWorkingArea(m_rectWorkArea);
            m_sTitle = strTitle;
            m_sText = strContent;
            m_iVisibleEvents = nTimeToStay;

            CalculateMouseRectangles();

            var nEvents = 0;

            if (nTimeToShow > 10)
            {
                nEvents = Math.Min(Convert.ToInt32(nTimeToShow / 10), m_bmpBackground.Height);
                m_iShowEvents = Convert.ToInt32(nTimeToShow / nEvents);
                m_iIncrementShow = Convert.ToInt32(m_bmpBackground.Height / nEvents);
            }
            else
            {
                m_iShowEvents = 10;
                m_iIncrementShow = m_bmpBackground.Height;
            }

            if (nTimeToHide > 10)
            {
                nEvents = Math.Min(Convert.ToInt32(nTimeToHide / 10), m_bmpBackground.Height);
                m_iHideEvents = Convert.ToInt32(nTimeToHide / nEvents);
                m_iIncrementHide = Convert.ToInt32(m_bmpBackground.Height / nEvents);
            }
            else
            {
                m_iHideEvents = 10;
                m_iIncrementHide = m_bmpBackground.Height;
            }

            switch (m_eWindowState)
            {
                case TaskbarStates.Hidden:

                    m_eWindowState = TaskbarStates.Appearing;
                    SetBounds(m_rectWorkArea.Right - m_bmpBackground.Width - 17, m_rectWorkArea.Bottom - 1,
                        m_bmpBackground.Width, 0);
                    m_tmrAnimation.Interval = m_iShowEvents;
                    m_tmrAnimation.Start();

                    ShowWindow(Handle, 4);

                    break;
                case TaskbarStates.Appearing:

                    Refresh();

                    break;
                case TaskbarStates.Visible:

                    m_tmrAnimation.Stop();
                    m_tmrAnimation.Interval = m_iVisibleEvents;
                    m_tmrAnimation.Start();
                    Refresh();

                    break;
                case TaskbarStates.Disappearing:

                    m_tmrAnimation.Stop();
                    m_eWindowState = TaskbarStates.Visible;
                    SetBounds(m_rectWorkArea.Right - m_bmpBackground.Width - 17,
                        m_rectWorkArea.Bottom - m_bmpBackground.Height - 1, m_bmpBackground.Width,
                        m_bmpBackground.Height);
                    m_tmrAnimation.Interval = m_iVisibleEvents;
                    m_tmrAnimation.Start();
                    Refresh();

                    break;
            }
        }


        public new void Hide()
        {
            if (m_eWindowState != TaskbarStates.Hidden)
            {
                m_tmrAnimation.Stop();
                m_eWindowState = TaskbarStates.Hidden;
                base.Hide();
            }
        }


        public void SetBackgroundBitmap(string strFilename, Color transparencyColor)
        {
            m_bmpBackground = new Bitmap(strFilename);
            Width = m_bmpBackground.Width;
            Height = m_bmpBackground.Height;
            Region = BitmapToRegion(m_bmpBackground, transparencyColor);
        }


        public void SetBackgroundBitmap(Image image, Color transparencyColor)
        {
            m_bmpBackground = new Bitmap(image);
            Width = m_bmpBackground.Width;
            Height = m_bmpBackground.Height;
            Region = BitmapToRegion(m_bmpBackground, transparencyColor);
        }


        public void SetCloseBitmap(string strFilename, Color transparencyColor, Point position)
        {
            m_bmpCloseButton = new Bitmap(strFilename);
            m_bmpCloseButton.MakeTransparent(transparencyColor);
            m_sizCloseButtonSize = new Size(Convert.ToInt32(m_bmpCloseButton.Width / 3), m_bmpCloseButton.Height);
            m_ptCloseButtonLocation = position;
        }


        public void SetCloseBitmap(Image image, Color transparencyColor, Point position)
        {
            m_bmpCloseButton = new Bitmap(image);
            m_bmpCloseButton.MakeTransparent(transparencyColor);
            m_sizCloseButtonSize = new Size(Convert.ToInt32(m_bmpCloseButton.Width / 3), m_bmpCloseButton.Height);
            m_ptCloseButtonLocation = position;
        }

        #endregion

        #region '"Protected Methods"' 

        protected void DrawCloseButton(Graphics grfx)
        {
            if (m_bmpCloseButton == null) return;

            var rectDest = new Rectangle(m_ptCloseButtonLocation, m_sizCloseButtonSize);
            var rectSrc = new Rectangle();

            if (m_bMouseOverClose)
            {
                if (m_bMouseDown)
                    rectSrc = new Rectangle(new Point(m_sizCloseButtonSize.Width * 2, 0), m_sizCloseButtonSize);
                else
                    rectSrc = new Rectangle(new Point(m_sizCloseButtonSize.Width, 0), m_sizCloseButtonSize);
            }
            else
            {
                rectSrc = new Rectangle(new Point(0, 0), m_sizCloseButtonSize);
            }

            grfx.DrawImage(m_bmpCloseButton, rectDest, rectSrc, GraphicsUnit.Pixel);
        }


        protected void DrawText(Graphics grfx)
        {
            if (!(m_sTitle == null) && m_sTitle.Length > 0)
            {
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                sf.Trimming = StringTrimming.EllipsisCharacter;

                if (m_bMouseOverTitle)
                    grfx.DrawString(m_sTitle, m_fntTitleHover, new SolidBrush(m_colTitleHover), m_rectTitle, sf);
                else
                    grfx.DrawString(m_sTitle, m_fntTitleNormal, new SolidBrush(m_colTitleNormal), m_rectTitle, sf);
            }

            if (!(m_sText == null) && m_sText.Length > 0)
            {
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                sf.Trimming = StringTrimming.EllipsisWord;

                if (m_bMouseOverText)
                {
                    grfx.DrawString(m_sText, m_fntTextHover, new SolidBrush(m_colTextHover), m_rectText, sf);
                    if (m_bDrawFocusRect) DrawFocusRectangle(grfx);
                }
                else
                {
                    grfx.DrawString(m_sText, m_fntTextNormal, new SolidBrush(m_colTextNormal), m_rectText, sf);
                }
            }
        }


        protected void DrawFocusRectangle(Graphics grfx)
        {
            if (m_bDrawFocusRect)
            {
                var iBorderWidth = 1;

                ControlPaint.DrawBorder(grfx, m_rectRealText, m_colFocusRect, iBorderWidth, m_eFocusRectStyle,
                    m_colFocusRect, iBorderWidth, m_eFocusRectStyle, m_colFocusRect, iBorderWidth,
                    m_eFocusRectStyle, m_colFocusRect, iBorderWidth, m_eFocusRectStyle);
            }
        }


        protected void CalculateMouseRectangles()
        {
            var grfx = CreateGraphics();
            var sf = new StringFormat();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            var sizfTitle = grfx.MeasureString(m_sTitle, m_fntTitleHover, m_rectTitle.Width, sf);
            var sizfText = grfx.MeasureString(m_sText, m_fntTextHover, m_rectText.Width, sf);

            grfx.Dispose();

            if (sizfTitle.Height > m_rectTitle.Height)
                m_rectRealTitle =
                    new Rectangle(m_rectTitle.Left, m_rectTitle.Top, m_rectTitle.Width, m_rectTitle.Height);
            else
                m_rectRealTitle = new Rectangle(m_rectTitle.Left, m_rectTitle.Top, Convert.ToInt32(sizfTitle.Width),
                    Convert.ToInt32(sizfTitle.Height));

            m_rectRealTitle.Inflate(0, 2);

            if (sizfText.Height > m_rectText.Height)
                m_rectRealText = new Rectangle(
                    Convert.ToInt32((m_rectText.Width - sizfText.Width) / 2 + m_rectText.Left), m_rectText.Top,
                    Convert.ToInt32(sizfText.Width), m_rectText.Height);
            else
                m_rectRealText = new Rectangle(
                    Convert.ToInt32((m_rectText.Width - sizfText.Width) / 2 + m_rectText.Left),
                    Convert.ToInt32((m_rectText.Height - sizfText.Height) / 2 + m_rectText.Top),
                    Convert.ToInt32(sizfText.Width), Convert.ToInt32(sizfText.Height));

            m_rectRealText.Inflate(0, 2);
        }


        protected Region BitmapToRegion(Bitmap bitmap, Color transparencyColor)
        {
            if (bitmap == null) throw new ArgumentNullException("Bitmap", "Bitmap cannot be Nothing!");

            var height = bitmap.Height;
            var width = bitmap.Width;

            var path = new GraphicsPath();

            var i = 0;
            var j = 0;

            for (j = 0; j <= height - 1; j++)
            for (i = 0; i <= width - 1; i++)
                if (!bitmap.GetPixel(i, j).Equals(transparencyColor))
                {
                    var x0 = i;

                    while (i < width && !bitmap.GetPixel(i, j).Equals(transparencyColor)) i += 1;

                    path.AddRectangle(new Rectangle(x0, j, i - x0, 1));
                }

            var region = new Region(path);
            path.Dispose();

            return region;
        }

        #endregion

        #region '"Overriden Base Class Methods"' 

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            m_bMouseOverPopup = true;
            Refresh();
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            m_bMouseOverPopup = false;
            m_bMouseOverClose = false;
            m_bMouseOverTitle = false;
            m_bMouseOverText = false;
            Refresh();
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var bContentModified = false;

            if (e.X > m_ptCloseButtonLocation.X && e.X < m_ptCloseButtonLocation.X + m_sizCloseButtonSize.Width &&
                e.Y > m_ptCloseButtonLocation.Y && e.Y < m_ptCloseButtonLocation.Y + m_sizCloseButtonSize.Height &&
                m_bCloseButtonClickEnabled)
            {
                if (!m_bMouseOverClose)
                {
                    m_bMouseOverClose = true;
                    m_bMouseOverTitle = false;
                    m_bMouseOverText = false;
                    Cursor = Cursors.Hand;
                    bContentModified = true;
                }
            }
            else
            {
                if (m_rectRealText.Contains(new Point(e.X, e.Y)) && m_bTextClickEnabled)
                {
                    if (!m_bMouseOverText)
                    {
                        m_bMouseOverClose = false;
                        m_bMouseOverTitle = false;
                        m_bMouseOverText = true;
                        Cursor = Cursors.Hand;
                        bContentModified = true;
                    }
                }
                else
                {
                    if (m_rectRealTitle.Contains(new Point(e.X, e.Y)) && m_bTitleClickEnabled)
                    {
                        if (!m_bMouseOverTitle)
                        {
                            m_bMouseOverClose = false;
                            m_bMouseOverTitle = true;
                            m_bMouseOverText = false;
                            Cursor = Cursors.Hand;
                            bContentModified = true;
                        }
                    }
                    else
                    {
                        if (m_bMouseOverClose || m_bMouseOverTitle || m_bMouseOverText) bContentModified = true;

                        m_bMouseOverClose = false;
                        m_bMouseOverTitle = false;
                        m_bMouseOverText = false;
                        Cursor = Cursors.Default;
                    }
                }
            }

            if (bContentModified) Refresh();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_bMouseDown = true;
            if (m_bMouseOverClose) Refresh();
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_bMouseDown = false;

            if (m_bMouseOverClose)
            {
                Hide();

                if (m_bCloseButtonClickEnabled)
                    if (null != CloseButtonClick)
                        CloseButtonClick(this, new EventArgs());
            }
            else
            {
                if (m_bMouseOverTitle)
                {
                    if (m_bTitleClickEnabled)
                        if (null != TitleClick)
                            TitleClick(this, new EventArgs());
                }
                else
                {
                    if (m_bMouseOverText)
                        if (m_bTextClickEnabled)
                            if (null != TextClick)
                                TextClick(this, new EventArgs());
                }
            }
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var grfx = e.Graphics;
            grfx.PageUnit = GraphicsUnit.Pixel;

            Graphics offScreenGraphics = null;
            Bitmap offScreenBitmap = null;

            offScreenBitmap = new Bitmap(m_bmpBackground.Width, m_bmpBackground.Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap);

            if (!(m_bmpBackground == null))
                offScreenGraphics.DrawImage(m_bmpBackground, 0, 0, m_bmpBackground.Width, m_bmpBackground.Height);

            DrawCloseButton(offScreenGraphics);
            DrawText(offScreenGraphics);

            grfx.DrawImage(offScreenBitmap, 0, 0);
        }

        #endregion
    }
}