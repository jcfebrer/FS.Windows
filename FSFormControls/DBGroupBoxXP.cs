#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FSFormControls.Properties;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [Designer(typeof(DBGroupBoxXPDesigner))]
    [DesignTimeVisibleAttribute(true)]
    [ToolboxItem(true)]
    public class DBGroupBoxXP : DBUserControl
    {
        #region '"Members"' 

        #region CaptionStyleEnum enum

        public enum CaptionStyleEnum
        {
            Normal,
            FlatLine,
            WrapAroundLine,
            None
        }

        #endregion

        #region CaptionTextAlignment enum

        public enum CaptionTextAlignment
        {
            Left,
            Middle,
            Right
        }

        #endregion

        #region ChevronAlignment enum

        public enum ChevronAlignment
        {
            Left,
            Right
        }

        #endregion

        #region ChevronStyleEnum enum

        public enum ChevronStyleEnum
        {
            Image,
            ArrowsInCircle,
            Triangle,
            PlusMinus,
            None
        }

        #endregion

        #region FormatFlag enum

        public enum FormatFlag
        {
            NoWrap,
            Wrap
        }

        #endregion

        #region GroupState enum

        public enum GroupState
        {
            Expanding,
            Collapsing,
            Standby
        }

        #endregion

        private readonly ToolTip m_Tooltip = new ToolTip();
        private Border3DStyle m_BorderStyle = Border3DStyle.Flat;
        private Color m_CaptionColor = Color.FromArgb(198, 210, 248);
        private int m_CaptionCurveRadius = 7;
        private Font m_CaptionFont = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
        private FormatFlag m_CaptionFormatFlag = FormatFlag.NoWrap;
        private int m_CaptionHeight = 25;
        private Color m_CaptionLeftColor = Color.White;
        private Color m_CaptionRightColor = Color.FromArgb(198, 210, 248);
        private CaptionStyleEnum m_CaptionStyle = CaptionStyleEnum.Normal;
        private string m_CaptionText = "DBGroupBoxXP Group";
        private CaptionTextAlignment m_CaptionTextAlign = CaptionTextAlignment.Left;
        private Color m_CaptionTextColor = Color.FromArgb(33, 93, 198);
        private Color m_CaptionTextHighlightColor = Color.FromArgb(66, 142, 255);
        private ChevronStyleEnum m_ChevronStyle = ChevronStyleEnum.Image;
        private Bitmap m_CollapsedHImage;
        private Bitmap m_CollapsedImage;
        private Bitmap m_ExpandedHImage;
        private Bitmap m_ExpandedImage;
        private ImageAttributes m_GrayAttributes;
        private ColorMatrix m_GrayMatrix;
        private GroupState m_GroupState = GroupState.Standby;
        private Image m_HeaderImage;
        private int m_ImageOffsetY = 10;
        private bool m_IsCaptionHighlighted;
        private Color m_PaneBottomRightColor = Color.FromArgb(214, 223, 247);
        private Color m_PaneOutlineColor = Color.White;
        private Color m_PaneTopLeftColor = Color.White;

        private int m_TransitionAlphaChannel;
        private int m_TransitionSizeDelta;
        private bool m_bCanToggle = true;
        private bool m_bDrawBorder;
        private bool m_bShowTooltips = true;

        #endregion

        #region '"Custom Events"' 

        #region Delegates

        public delegate void CollapsedHandler(DBGroupBoxXP x);

        public delegate void CollapsingHandler(DBGroupBoxXP x, int delta);

        public delegate void ExpandedHandler(DBGroupBoxXP x);

        public delegate void ExpandingHandler(DBGroupBoxXP x, int delta);

        #endregion

        public event CollapsingHandler Collapsing;

        public event CollapsedHandler Collapsed;

        public event CollapsingHandler Expanding;

        public event ExpandedHandler Expanded;

        #endregion

        #region '"Windows Form Designer generated code"' 

        internal Timer Timer1;
        private IContainer components;

        public DBGroupBoxXP()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);

            BackColor = Color.FromArgb(214, 223, 247);

            Load += DBGroupBoxXP_Load;
            Timer1.Tick += Timer1_Tick;
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
            Timer1 = new Timer(components);
            SuspendLayout();
            // 
            // DBGroupBoxXP
            // 
            Name = "DBGroupBoxXP";
            Size = new Size(39, 46);
            ResumeLayout(false);
        }

        #endregion

        #region '"Public Properties"' 

        [Description("Determines the ending (dark) color of the caption gradient fill.")]
        [DefaultValue(typeof(Color), "198, 210, 248")]
        [Category("Caption")]
        public Color CaptionRightColor
        {
            get { return m_CaptionRightColor; }

            set
            {
                m_CaptionRightColor = value;
                Invalidate();
            }
        }

        [Description("Offset for image above the caption.")]
        [DefaultValue(10)]
        [Category("Caption")]
        public int ImageOffset
        {
            get { return m_ImageOffsetY; }

            set
            {
                m_ImageOffsetY = value;
                Invalidate();
            }
        }

        [Description("Determines the border style.")]
        [DefaultValue(typeof(Border3DStyle), "Border3DStyle.Flat")]
        [Category("Appearance")]
        public new Border3DStyle BorderStyle
        {
            get { return m_BorderStyle; }

            set
            {
                m_BorderStyle = value;
                Invalidate();
            }
        }

        [Description("Determines the Caption Text Alignment.")]
        [DefaultValue(typeof(CaptionTextAlignment), "CaptionTextAlignment.Left")]
        [Category("Caption")]
        public CaptionTextAlignment CaptionTextAlign
        {
            get { return m_CaptionTextAlign; }

            set
            {
                m_CaptionTextAlign = value;
                Invalidate();
            }
        }

        [Description("Format flags for Caption Text.")]
        [DefaultValue(typeof(FormatFlag), "FormatFlag.NoWrap")]
        [Category("Caption")]
        public FormatFlag CaptionFormatFlag
        {
            get { return m_CaptionFormatFlag; }

            set
            {
                m_CaptionFormatFlag = value;
                Invalidate();
            }
        }

        [Description("Specify whether to draw a border or not.")]
        [DefaultValue(false)]
        [Category("Appearance")]
        public bool DrawBorder
        {
            get { return m_bDrawBorder; }

            set
            {
                m_bDrawBorder = value;
                Invalidate();
            }
        }

        [Description("Determines the starting (light) color of the caption gradient fill.")]
        [DefaultValue(typeof(Color), "255,255,255")]
        [Category("Caption")]
        public Color CaptionLeftColor
        {
            get { return m_CaptionLeftColor; }

            set
            {
                m_CaptionLeftColor = value;
                Invalidate();
            }
        }

        [Description("Height of caption.")]
        [DefaultValue(25)]
        [Category("Caption")]
        public int CaptionHeight
        {
            get { return m_CaptionHeight; }

            set
            {
                RepositionChildControls(value - m_CaptionHeight);
                m_CaptionHeight = value;
                Invalidate();
            }
        }

        [Description("Caption curve radius.")]
        [DefaultValue(7)]
        [Category("Caption")]
        public int CaptionCurveRadius
        {
            get { return m_CaptionCurveRadius; }

            set
            {
                if (value < 0)
                    m_CaptionCurveRadius = 0;
                else
                    m_CaptionCurveRadius = value;
                Invalidate();
            }
        }

        [Description("Caption text.")]
        [DefaultValue("")]
        [Category("Caption")]
        [Localizable(true)]
        public string CaptionText
        {
            get { return m_CaptionText; }

            set
            {
                m_CaptionText = value;
                Invalidate();
            }
        }

        [Description("Pane Outline color.")]
        [DefaultValue(typeof(Color), "255, 255, 255")]
        [Category("Appearance")]
        public Color PaneOutlineColor
        {
            get { return m_PaneOutlineColor; }

            set
            {
                m_PaneOutlineColor = value;
                Invalidate();
            }
        }

        [Description("Pane Top Left color.")]
        [DefaultValue(typeof(Color), "255, 255, 255")]
        [Category("Appearance")]
        public Color PaneTopLeftColor
        {
            get { return m_PaneTopLeftColor; }

            set
            {
                m_PaneTopLeftColor = value;
                Invalidate();
            }
        }

        [Description("Pane Bottom Right color.")]
        [DefaultValue(typeof(Color), "214, 223, 247")]
        [Category("Appearance")]
        public Color PaneBottomRightColor
        {
            get { return m_PaneBottomRightColor; }

            set
            {
                m_PaneBottomRightColor = value;
                Invalidate();
            }
        }

        [Description("Caption text color.")]
        [DefaultValue(typeof(Color), "33,93,198")]
        [Category("Caption")]
        public Color CaptionTextColor
        {
            get { return m_CaptionTextColor; }

            set
            {
                m_CaptionTextColor = value;
                Invalidate();
            }
        }

        [Description("Caption text color when the mouse is hovering over it.")]
        [DefaultValue(typeof(Color), "66, 142, 255")]
        [Category("Caption")]
        public Color CaptionTextHighlightColor
        {
            get { return m_CaptionTextHighlightColor; }

            set
            {
                m_CaptionTextHighlightColor = value;
                Invalidate();
            }
        }

        [Description("Image to be shown in Header")]
        [DefaultValue(typeof(Image), "Nothing")]
        [Category("Caption")]
        public Image Image
        {
            get { return m_HeaderImage; }

            set
            {
                if ((m_HeaderImage == null) & (value == null)) return;
                m_HeaderImage = value;
                Invalidate();
            }
        }

        [Description("Image of Collapsed Chevron")]
        [Category("Chevron")]
        public Bitmap CollapsedImage
        {
            get { return m_CollapsedImage; }

            set
            {
                if (value == null)
                    m_CollapsedImage = Resources.DBGroupBoxXPCollapse;
                else
                    m_CollapsedImage = value;
                Invalidate();
            }
        }

        [Description("Image of Expanded Chevron")]
        [Category("Chevron")]
        public Bitmap ExpandedImage
        {
            get { return m_ExpandedImage; }

            set
            {
                if (value == null)
                    m_ExpandedImage = Resources.DBGroupBoxXPExpand;
                else
                    m_ExpandedImage = value;
                Invalidate();
            }
        }

        [Description("Image of Collapsed Chevron when highlighted")]
        [Category("Chevron")]
        public Bitmap CollapsedHighlightImage
        {
            get { return m_CollapsedHImage; }

            set
            {
                if (value == null)
                    m_CollapsedHImage = Resources.DBGroupBoxXPCollapse_h;
                else
                    m_CollapsedHImage = value;
                Invalidate();
            }
        }

        [Description("Image of Expanded Chevron when highlighted")]
        [Category("Chevron")]
        public Bitmap ExpandedHighlightImage
        {
            get { return m_ExpandedHImage; }

            set
            {
                if (value == null)
                    m_ExpandedHImage = Resources.DBGroupBoxXPExpand_h;
                else
                    m_ExpandedHImage = value;
                Invalidate();
            }
        }

        [Description("Caption Font.")]
        [Category("Caption")]
        public Font CaptionFont
        {
            get { return m_CaptionFont; }

            set
            {
                m_CaptionFont = value;
                Invalidate();
            }
        }

        [Description("Time taken to Collapse or Expand")]
        [DefaultValue(100)]
        [Category("Behavior")]
        public int AnimationTime { get; set; } = 100;

        [Description("Can the user toggle between collapse/expand states.")]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool CanToggle
        {
            get { return m_bCanToggle; }

            set
            {
                m_bCanToggle = value;
                Invalidate();
            }
        }

        [Description("Chevron Style.")]
        [DefaultValue(typeof(ChevronStyleEnum), "ChevronStyleEnum.Image")]
        [Category("Chevron")]
        public ChevronStyleEnum ChevronStyle
        {
            get { return m_ChevronStyle; }

            set
            {
                m_ChevronStyle = value;
                Invalidate();
            }
        }

        [Description("Caption Style.")]
        [DefaultValue(typeof(CaptionStyleEnum), "CaptionStyleEnum.Normal")]
        [Category("Caption")]
        public CaptionStyleEnum CaptionStyle
        {
            get { return m_CaptionStyle; }

            set
            {
                m_CaptionStyle = value;
                if (m_CaptionStyle == CaptionStyleEnum.WrapAroundLine)
                {
                }

                Invalidate();
            }
        }

        [Description("Animation during collapse/expand.")]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool Animated { get; set; }

        [Description("Show tooltips.")]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool ShowTooltips
        {
            get { return m_bShowTooltips; }

            set
            {
                m_bShowTooltips = value;
                if (m_bShowTooltips == false) m_Tooltip.Active = false;
            }
        }

        [Description("Tooltip Text")]
        [Category("Behavior")]
        public string TooltipText { get; set; }

        [Description("Height of the control when expanded")]
        [Browsable(false)]
        [DesignOnly(true)]
        public int ExpandedHeight { get; set; } = 10;

        public bool IsExpanded { get; private set; } = true;

        #endregion

        #region '"Overrides"' 

        private void DBGroupBoxXP_Load(object sender, EventArgs e)
        {
            m_CollapsedImage = Resources.DBGroupBoxXPCollapse;
            // new Bitmap(this.GetType(), "DBGroupBoxXPCollapse.jpg");
            m_CollapsedHImage = Resources.DBGroupBoxXPCollapse_h;
            //new Bitmap(this.GetType(), "DBGroupBoxXPCollapse_h.jpg");
            m_ExpandedImage = Resources.DBGroupBoxXPExpand; //new Bitmap(this.GetType(), "DBGroupBoxXPExpand.jpg");
            m_ExpandedHImage = Resources.DBGroupBoxXPExpand_h; //new Bitmap(this.GetType(), "DBGroupBoxXPExpand_h.jpg");

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            DockPadding.Top = m_CaptionHeight;
            BackColor = Color.Transparent;

            m_GrayMatrix = new ColorMatrix();
            m_GrayMatrix.Matrix00 = 1 / 3.0F;
            m_GrayMatrix.Matrix01 = 1 / 3.0F;
            m_GrayMatrix.Matrix02 = 1 / 3.0F;
            m_GrayMatrix.Matrix10 = 1 / 3.0F;
            m_GrayMatrix.Matrix11 = 1 / 3.0F;
            m_GrayMatrix.Matrix12 = 1 / 3.0F;
            m_GrayMatrix.Matrix20 = 1 / 3.0F;
            m_GrayMatrix.Matrix21 = 1 / 3.0F;
            m_GrayMatrix.Matrix22 = 1 / 3.0F;
            m_GrayAttributes = new ImageAttributes();
            m_GrayAttributes.SetColorMatrix(m_GrayMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            m_Tooltip.AutoPopDelay = 2000;
            m_Tooltip.InitialDelay = 500;
            m_Tooltip.ReshowDelay = 300;

            if (m_bShowTooltips)
            {
                m_Tooltip.SetToolTip(this, TooltipText);
                m_Tooltip.Active = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var capcolor = new Color();
            if (Enabled)
            {
                if (m_IsCaptionHighlighted)
                    capcolor = m_CaptionTextHighlightColor;
                else
                    capcolor = m_CaptionTextColor;
            }
            else
            {
                capcolor = SystemColors.GrayText;
            }

            switch (m_CaptionStyle)
            {
                case CaptionStyleEnum.Normal:
                    DrawNormalStyleCaption(e.Graphics, capcolor);
                    if (Height > m_CaptionHeight)
                    {
                        e.Graphics.DrawLine(new Pen(m_PaneOutlineColor), 1, CaptionHeight, 1, Height);
                        e.Graphics.DrawLine(new Pen(m_PaneOutlineColor), Width - 1, CaptionHeight, Width - 1, Height);
                        e.Graphics.DrawLine(new Pen(m_PaneOutlineColor), 0, Height - 1, Width - 1, Height - 1);
                    }

                    break;
                case CaptionStyleEnum.FlatLine:
                    DrawFlatLineStyleCaption(e.Graphics, capcolor);
                    break;
                case CaptionStyleEnum.WrapAroundLine:
                    DrawWrapAroundLineStyleCaption(e.Graphics, capcolor);
                    break;
                case CaptionStyleEnum.None:
                    DrawNoneStyleCaption(e.Graphics, capcolor);
                    break;
                default:
                    DrawNoneStyleCaption(e.Graphics, capcolor);
                    break;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            if (Height > CaptionHeight)
            {
                var rect = new Rectangle(0, CaptionHeight, Width, Height - CaptionHeight);
                var b = new LinearGradientBrush(rect, m_PaneTopLeftColor, m_PaneBottomRightColor,
                    LinearGradientMode.ForwardDiagonal);
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pevent.Graphics.FillRectangle(b, rect);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Y <= CaptionHeight)
            {
                if (m_bCanToggle) Cursor.Current = Cursors.Hand;
                m_IsCaptionHighlighted = true;
                if (m_bShowTooltips) m_Tooltip.Active = true;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                m_IsCaptionHighlighted = false;
                m_Tooltip.Active = false;
            }

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if ((e.Button != MouseButtons.Left) | (e.Y > CaptionHeight) | (m_GroupState != GroupState.Standby) |
                (m_bCanToggle == false))
                return;
            ChangeHeight();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (m_IsCaptionHighlighted)
            {
                m_IsCaptionHighlighted = false;
                Cursor.Current = Cursors.Default;
                Invalidate();
            }
        }

        #endregion

        #region '"Public Methods"' 

        public void Expand()
        {
            if ((IsExpanded == false) & (m_GroupState == GroupState.Standby)) ChangeHeight();
        }

        public void Collapse()
        {
            if (IsExpanded & (m_GroupState == GroupState.Standby)) ChangeHeight();
        }

        #endregion

        #region '"Private Helpers"' 

        private void ChangeHeight()
        {
            if (IsExpanded)
            {
                ExpandedHeight = Height;
                m_GroupState = GroupState.Collapsing;
            }
            else
            {
                m_GroupState = GroupState.Expanding;
            }

            if (Animated & (AnimationTime >= 5))
            {
                Timer1.Interval = AnimationTime;
                Timer1.Enabled = true;
            }
            else
            {
                QuickToggle();
            }
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            if (m_TransitionSizeDelta == 0) m_TransitionSizeDelta = 1;
            if (Timer1.Interval > AnimationTime / 5)
            {
                if (Timer1.Interval - AnimationTime / 5 <= 0)
                    Timer1.Interval = 1;
                else
                    Timer1.Interval -= Convert.ToInt32(AnimationTime / 5);
            }
            else
            {
                m_TransitionSizeDelta += 2;
            }


            if (m_TransitionAlphaChannel == 0)
            {
                m_TransitionAlphaChannel = 10;
            }
            else
            {
                if (m_TransitionAlphaChannel + 10 < 255) m_TransitionAlphaChannel += 10;
            }

            if (m_GroupState == GroupState.Expanding)
            {
                if (Height + m_TransitionSizeDelta < ExpandedHeight)
                {
                    SetControlsOpacity(m_TransitionAlphaChannel);
                    m_PaneBottomRightColor = Color.FromArgb(m_TransitionAlphaChannel, m_PaneBottomRightColor);
                    m_PaneTopLeftColor = Color.FromArgb(m_TransitionAlphaChannel, m_PaneTopLeftColor);
                    m_PaneOutlineColor = Color.FromArgb(m_TransitionAlphaChannel, m_PaneOutlineColor);
                    Height += m_TransitionSizeDelta;
                    SetControlsVisible();
                }
                else
                {
                    SetControlsOpacity(255);
                    m_PaneBottomRightColor = Color.FromArgb(255, m_PaneBottomRightColor);
                    m_PaneTopLeftColor = Color.FromArgb(255, m_PaneTopLeftColor);
                    m_PaneOutlineColor = Color.FromArgb(255, m_PaneOutlineColor);
                    m_TransitionAlphaChannel = 0;
                    m_TransitionSizeDelta = ExpandedHeight - Height;
                    Height = ExpandedHeight;
                    IsExpanded = true;
                    m_GroupState = GroupState.Standby;
                    SetControlsVisible();
                }

                if (null != Expanding) Expanding(this, m_TransitionSizeDelta);
                Invalidate();
                Timer1.Enabled = true;
            }
            else if (m_GroupState == GroupState.Collapsing)
            {
                if (Height - m_TransitionSizeDelta > CaptionHeight)
                {
                    SetControlsOpacity(m_TransitionAlphaChannel);
                    Height -= m_TransitionSizeDelta;
                    m_PaneBottomRightColor = Color.FromArgb(255 - m_TransitionAlphaChannel, m_PaneBottomRightColor);
                    m_PaneTopLeftColor = Color.FromArgb(255 - m_TransitionAlphaChannel, m_PaneTopLeftColor);
                    m_PaneOutlineColor = Color.FromArgb(255 - m_TransitionAlphaChannel, m_PaneOutlineColor);
                    SetControlsVisible();
                }
                else
                {
                    m_TransitionAlphaChannel = 0;
                    SetControlsOpacity(0);
                    m_PaneBottomRightColor = Color.FromArgb(0, m_PaneBottomRightColor);
                    m_PaneTopLeftColor = Color.FromArgb(0, m_PaneTopLeftColor);
                    m_PaneOutlineColor = Color.FromArgb(0, m_PaneOutlineColor);
                    m_TransitionSizeDelta = (CaptionHeight - Height) * -1;
                    Height = CaptionHeight;
                    IsExpanded = false;
                    m_GroupState = GroupState.Standby;
                    SetControlsVisible();
                }

                if (null != Collapsing) Collapsing(this, m_TransitionSizeDelta);
                Invalidate();
                Timer1.Enabled = true;
            }
            else if (m_GroupState == GroupState.Standby)
            {
                Timer1.Enabled = false;
                m_TransitionSizeDelta = 0;
                Invalidate();
            }
        }

        private void SetControlsOpacity(int opacity)
        {
            object[] args = {ControlStyles.SupportsTransparentBackColor};
            try
            {
                foreach (Control c in Controls)
                    if (!(c.GetType() == typeof(TextBox)) & !(c.GetType() == typeof(ComboBox)))
                    {
                        if (m_GroupState == GroupState.Collapsing)
                        {
                            if (c.BackColor.ToArgb() != Color.Transparent.ToArgb())
                                c.BackColor = Color.FromArgb(255 - opacity, c.BackColor);
                            c.ForeColor = Color.FromArgb(255 - opacity, c.ForeColor);
                        }
                        else if (m_GroupState == GroupState.Expanding)
                        {
                            if (c.BackColor.ToArgb() != Color.Transparent.ToArgb())
                                c.BackColor = Color.FromArgb(opacity, c.BackColor);
                            c.ForeColor = Color.FromArgb(opacity, c.ForeColor);
                        }
                    }
            }
            catch (ArgumentException)
            {
            }
        }

        private void SetControlsVisible()
        {
            foreach (Control c in Controls)
                if (c.Top < CaptionHeight)
                    c.Visible = false;
                else
                    c.Visible = true;
        }


        private void QuickToggle()
        {
            if (m_GroupState == GroupState.Expanding)
            {
                SetControlsOpacity(255);
                m_PaneBottomRightColor = Color.FromArgb(255, m_PaneBottomRightColor);
                m_PaneTopLeftColor = Color.FromArgb(255, m_PaneTopLeftColor);
                m_PaneOutlineColor = Color.FromArgb(255, m_PaneOutlineColor);
                m_TransitionAlphaChannel = 0;
                Height = ExpandedHeight;
                IsExpanded = true;
                m_GroupState = GroupState.Standby;
                SetControlsVisible();
                if (null != Expanded) Expanded(this);
            }
            else if (m_GroupState == GroupState.Collapsing)
            {
                m_TransitionAlphaChannel = 0;
                SetControlsOpacity(0);
                m_PaneBottomRightColor = Color.FromArgb(0, m_PaneBottomRightColor);
                m_PaneTopLeftColor = Color.FromArgb(0, m_PaneTopLeftColor);
                m_PaneOutlineColor = Color.FromArgb(0, m_PaneOutlineColor);
                Height = CaptionHeight;
                IsExpanded = false;
                m_GroupState = GroupState.Standby;
                SetControlsVisible();
                if (null != Collapsed) Collapsed(this);
            }

            Invalidate();
        }


        private void RepositionChildControls(int Offset)
        {
            if (Offset == 0) return;
            foreach (Control ctl in Controls) ctl.Top += Offset;
        }


        private RectangleF DrawCaptionText(Graphics g, RectangleF rect, Color capcolor)
        {
            var format = new StringFormat();
            format.Trimming = StringTrimming.EllipsisCharacter;
            if (m_CaptionFormatFlag == FormatFlag.NoWrap) format.FormatFlags = StringFormatFlags.NoWrap;

            var invcolor = Color.FromArgb(70, capcolor.R, capcolor.G, capcolor.B);

            var size = g.MeasureString(CaptionText, m_CaptionFont, new SizeF(rect.Width, rect.Height), format);
            var lft = rect.Left;
            if (m_CaptionTextAlign == CaptionTextAlignment.Right)
                lft += rect.Width - size.Width;
            else if (m_CaptionTextAlign == CaptionTextAlignment.Middle) lft += rect.Width / 2 - size.Width / 2;

            //float top = 0; 
            var CaptionRectF = new RectangleF(lft, rect.Top, size.Width, rect.Height);
            if (Enabled)
                g.DrawString(CaptionText, m_CaptionFont, new SolidBrush(capcolor), CaptionRectF, format);
            else
                ControlPaint.DrawStringDisabled(g, CaptionText, m_CaptionFont, SystemColors.GrayText, CaptionRectF,
                    format);
            return CaptionRectF;
        }

        #region '" Captions"' 

        private void DrawWrapAroundLineStyleCaption(Graphics g, Color capcolor)
        {
            var path = new GraphicsPath();
            var path2 = new GraphicsPath();
            var LineCaptionGap = 1;
            float x = m_CaptionCurveRadius + LineCaptionGap + 5;
            var hgt = Convert.ToSingle(m_CaptionHeight);
            float y = 5;
            var wdth = Width - x * 2;
            var CaptionRectF = DrawCaptionText(g, new RectangleF(x, y, wdth, hgt), capcolor);
            var toprow = CaptionRectF.Top + 5;

            if (CaptionRectF.Right + LineCaptionGap < Width - m_CaptionCurveRadius * 2)
                path.AddLine(CaptionRectF.Right + LineCaptionGap, toprow, Width - m_CaptionCurveRadius * 2, toprow);
            if (m_CaptionCurveRadius > 0)
                path.AddArc(Width - m_CaptionCurveRadius * 2, toprow, m_CaptionCurveRadius * 2,
                    toprow + m_CaptionCurveRadius * 2, 270, 90);

            if (IsExpanded)
            {
                path.AddLine(Width - 1, m_CaptionCurveRadius + toprow, Width - 1, Height - 1);
                path.AddLine(Width - 1, Height - 1, 1, Height - 1);
                path2.AddLine(1, Height - 1, 1, m_CaptionCurveRadius + toprow + 2);
            }
            else
            {
                path.AddLine(Width - 1, m_CaptionCurveRadius + toprow, Width - 1, CaptionHeight - 1);
                path.AddLine(Width - 1, CaptionHeight - 1, 1, CaptionHeight - 1);
                path2.AddLine(1, CaptionHeight - 1, 1, m_CaptionCurveRadius + toprow + 2);
            }

            if (m_CaptionCurveRadius > 0)
                path2.AddArc(1, toprow, m_CaptionCurveRadius * 2 + 1, toprow + m_CaptionCurveRadius * 2, 180, 90);
            path2.AddLine(m_CaptionCurveRadius, toprow, CaptionRectF.Left - LineCaptionGap, toprow);

            g.DrawPath(new Pen(m_PaneOutlineColor), path);
            g.DrawPath(new Pen(m_PaneOutlineColor), path2);
        }

        private void DrawFlatLineStyleCaption(Graphics g, Color capcolor)
        {
            float left = 0;
            float top = 0;
            float wdth = 0;
            var chevwidth = 0;
            var LineCaptionGap = 10;
            var MinLineWidth = 8;
            var margin = 4;

            if (m_bCanToggle)
            {
                var ptX = 0;
                if ((m_CaptionTextAlign == CaptionTextAlignment.Left) |
                    (m_CaptionTextAlign == CaptionTextAlignment.Middle)) ptX = Width - MinLineWidth;
                if (m_ChevronStyle == ChevronStyleEnum.Triangle)
                    chevwidth = DrawChevronTriangle(g, ptX, Convert.ToInt32(m_CaptionHeight / 2), MinLineWidth, 2,
                        capcolor);
                else if (m_ChevronStyle == ChevronStyleEnum.PlusMinus)
                    chevwidth = DrawChevronPlusMinus(g, ptX, Convert.ToInt32(m_CaptionHeight / 2 - MinLineWidth / 2),
                        MinLineWidth, capcolor);
            }

            wdth = Convert.ToSingle(Width - chevwidth - margin);
            if (m_CaptionTextAlign == CaptionTextAlignment.Middle)
            {
                left = MinLineWidth + LineCaptionGap;
                wdth -= Convert.ToSingle(MinLineWidth + 2 * LineCaptionGap);
            }
            else if (m_CaptionTextAlign == CaptionTextAlignment.Right)
            {
                left = chevwidth + margin;
            }

            var CaptionRectF = new RectangleF();
            CaptionRectF = DrawCaptionText(g, new RectangleF(left, top, wdth, m_CaptionHeight), capcolor);

            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            y1 = Convert.ToInt32(m_CaptionHeight / 2);
            y2 = Convert.ToInt32(m_CaptionHeight / 2);
            if (m_CaptionTextAlign == CaptionTextAlignment.Right)
            {
                x1 = 0;
                x2 = Convert.ToInt32(CaptionRectF.Left - LineCaptionGap);
                if (x2 < MinLineWidth) x2 = MinLineWidth;
            }
            else if (m_CaptionTextAlign == CaptionTextAlignment.Left)
            {
                x1 = Convert.ToInt32(CaptionRectF.Width + LineCaptionGap);
                x2 = Width;
                if (x2 - x1 < MinLineWidth) x1 = x2 - MinLineWidth;
            }
            else if (m_CaptionTextAlign == CaptionTextAlignment.Middle)
            {
                x1 = 0;
                x2 = Convert.ToInt32(CaptionRectF.Left - LineCaptionGap);
                if (x2 < MinLineWidth) x2 = MinLineWidth;
                g.DrawLine(new Pen(capcolor), x1, y1, x2, y2);
                x1 = Convert.ToInt32(CaptionRectF.Left + CaptionRectF.Width + LineCaptionGap);
                x2 = Width;
                if (x2 - x1 < MinLineWidth) x1 = x2 - MinLineWidth;
            }

            g.DrawLine(new Pen(capcolor), x1, y1, x2, y2);
        }


        private void DrawNormalStyleCaption(Graphics g, Color capcolor)
        {
            var rc = new Rectangle(0, 0, Width, CaptionHeight);
            var path = new GraphicsPath();
            var imageOffsetY = 0;
            if (!(Image == null)) imageOffsetY = m_ImageOffsetY;

            path.AddLine(m_CaptionCurveRadius, imageOffsetY, Width - m_CaptionCurveRadius * 2, imageOffsetY);
            if (m_CaptionCurveRadius > 0)
                path.AddArc(Width - m_CaptionCurveRadius * 2 - 1, imageOffsetY, m_CaptionCurveRadius * 2,
                    m_CaptionCurveRadius * 2, 270, 90);
            path.AddLine(Width, m_CaptionCurveRadius - imageOffsetY, Width, CaptionHeight);
            path.AddLine(Width, CaptionHeight, 0, CaptionHeight);
            path.AddLine(0, CaptionHeight, 0, m_CaptionCurveRadius - imageOffsetY);
            if (m_CaptionCurveRadius > 0)
                path.AddArc(0, imageOffsetY, m_CaptionCurveRadius * 2, m_CaptionCurveRadius * 2, 180, 90);

            LinearGradientBrush CaptionBrush = null;
            if ((rc.Width > 0) & (rc.Height > 0))
            {
                CaptionBrush = new LinearGradientBrush(rc, m_CaptionLeftColor, m_CaptionRightColor,
                    LinearGradientMode.Vertical);
                g.FillPath(CaptionBrush, path);
            }

            if (m_bDrawBorder)
                ControlPaint.DrawBorder3D(g, new Rectangle(0, CaptionHeight, Width, Height), m_BorderStyle);

            var graphicsUnit = GraphicsUnit.Display;
            var iconBorder = 2;
            var imageOffsetX = 2;

            if (!(Image == null))
            {
                imageOffsetX += Image.Width + iconBorder;
                var srcRectF = Image.GetBounds(ref graphicsUnit);
                var destRect = new Rectangle(iconBorder, iconBorder, Image.Width, Image.Height);
                if (Enabled)
                    g.DrawImage(Image, destRect, srcRectF.Left, srcRectF.Top, srcRectF.Width, srcRectF.Height,
                        graphicsUnit);
                else
                    g.DrawImage(Image, destRect, srcRectF.Left, srcRectF.Top, srcRectF.Width, srcRectF.Height,
                        graphicsUnit, m_GrayAttributes);
            }

            var ChevronWidth = 8;
            var ChevronMargin = 7;
            if (m_bCanToggle)
            {
                if (m_ChevronStyle == ChevronStyleEnum.Image)
                    ChevronWidth = DrawChevronImage(g, 2 + imageOffsetY);
                else if (m_ChevronStyle == ChevronStyleEnum.ArrowsInCircle)
                    ChevronWidth = DrawChevronArrowsInCircle(g, imageOffsetY);
                else if (m_ChevronStyle == ChevronStyleEnum.Triangle)
                    ChevronWidth = DrawChevronTriangle(g, Width - ChevronWidth - ChevronMargin, imageOffsetY + 10,
                        ChevronWidth, 0, capcolor);
                else if (m_ChevronStyle == ChevronStyleEnum.PlusMinus)
                    ChevronWidth = DrawChevronPlusMinus(g, Width - ChevronWidth - ChevronMargin, imageOffsetY + 4,
                        ChevronWidth, capcolor);
            }

            float x = 10 + imageOffsetX;
            var hgt = Convert.ToSingle(m_CaptionHeight - imageOffsetY);
            var y = Convert.ToSingle(imageOffsetY + 4);
            float wdth = 0;
            if (m_bCanToggle)
                wdth = Width - x - ChevronMargin - ChevronWidth;
            else
                wdth = Width - x - ChevronMargin;

            DrawCaptionText(g, new RectangleF(x, y, wdth, hgt), capcolor);
        }


        private void DrawNoneStyleCaption(Graphics g, Color capcolor)
        {
            float left = 0;
            float top = 0;
            float wdth = 0;
            var chevwidth = 0;
            var margin = 0;
            var DesiredChevWidth = 8;

            if (m_bCanToggle)
            {
                var ptX = 0;
                if ((m_CaptionTextAlign == CaptionTextAlignment.Left) |
                    (m_CaptionTextAlign == CaptionTextAlignment.Middle)) ptX = Width - DesiredChevWidth;
                if (m_ChevronStyle == ChevronStyleEnum.Triangle)
                {
                    chevwidth = DrawChevronTriangle(g, ptX, Convert.ToInt32(m_CaptionHeight / 2), DesiredChevWidth, 2,
                        capcolor);
                    margin = 4;
                }
                else if (m_ChevronStyle == ChevronStyleEnum.PlusMinus)
                {
                    chevwidth = DrawChevronPlusMinus(g, ptX,
                        Convert.ToInt32(m_CaptionHeight / 2 - DesiredChevWidth / 2 - 1),
                        DesiredChevWidth, capcolor);
                    margin = 4;
                }
            }

            wdth = Convert.ToSingle(Width - chevwidth - margin);
            if (m_CaptionTextAlign == CaptionTextAlignment.Right) left = chevwidth + margin;

            DrawCaptionText(g, new RectangleF(left, top, wdth, m_CaptionHeight), capcolor);
        }

        #endregion

        #region '" Chevrons"' 

        private int DrawChevronTriangle(Graphics g, int ptX, int ptY, int TBase, int OffsetX, Color clr)
        {
            var p = new GraphicsPath();
            var th = TBase;
            var ofst = OffsetX;

            if (IsExpanded)
            {
                p.AddLine(ptX, ptY - ofst, ptX + th, ptY - ofst);
                p.AddLine(ptX + th, ptY - ofst, ptX + Convert.ToInt32(th / 2), ptY - th);
                p.AddLine(ptX + Convert.ToInt32(th / 2), ptY - th, ptX, ptY - ofst);
            }
            else
            {
                p.AddLine(ptX, ptY + ofst, ptX + th, ptY + ofst);
                p.AddLine(ptX + th, ptY + ofst, ptX + Convert.ToInt32(th / 2), ptY + th);
                p.AddLine(ptX + Convert.ToInt32(th / 2), ptY + th, ptX, ptY + ofst);
            }

            g.FillPath(new SolidBrush(clr), p);
            return TBase;
        }

        private int DrawChevronPlusMinus(Graphics g, int ptX, int ptY, int wdth, Color clr)
        {
            var p = new GraphicsPath();
            var hgt = wdth;
            var margin = 2;
            g.DrawRectangle(new Pen(clr), new Rectangle(ptX, ptY, wdth, hgt));
            if (IsExpanded == false)
                g.DrawLine(new Pen(clr), Convert.ToInt32(ptX + wdth / 2), ptY + margin, Convert.ToInt32(ptX + wdth / 2),
                    ptY + hgt - margin);
            g.DrawLine(new Pen(clr), ptX + margin, Convert.ToInt32(ptY + hgt / 2), ptX + wdth - margin,
                Convert.ToInt32(ptY + hgt / 2));
            return wdth;
        }

        private int DrawChevronImage(Graphics g, float imageOffsetY)
        {
            var ChevronWidth = 0;
            var ChevronMargin = 7;
            if (IsExpanded)
            {
                if (Enabled == false)
                {
                    ControlPaint.DrawImageDisabled(g, m_ExpandedImage, Width - m_ExpandedImage.Width - ChevronMargin,
                        Convert.ToInt32(2 + imageOffsetY), CaptionRightColor);
                    ChevronWidth = m_ExpandedImage.Width;
                }
                else if (m_IsCaptionHighlighted)
                {
                    g.DrawImage(m_ExpandedHImage, Width - m_ExpandedHImage.Width - ChevronMargin, 2 + imageOffsetY);
                    ChevronWidth = m_ExpandedHImage.Width;
                }
                else
                {
                    g.DrawImage(m_ExpandedImage, Width - m_ExpandedImage.Width - ChevronMargin, 2 + imageOffsetY);
                    ChevronWidth = m_ExpandedImage.Width;
                }
            }
            else
            {
                if (Enabled == false)
                {
                    ControlPaint.DrawImageDisabled(g, m_CollapsedImage, Width - m_CollapsedImage.Width - ChevronMargin,
                        Convert.ToInt32(2 + imageOffsetY), CaptionRightColor);
                    ChevronWidth = m_CollapsedImage.Width;
                }
                else if (m_IsCaptionHighlighted)
                {
                    g.DrawImage(m_CollapsedHImage, Width - m_CollapsedHImage.Width - ChevronMargin, 2 + imageOffsetY);
                    ChevronWidth = m_CollapsedHImage.Width;
                }
                else
                {
                    g.DrawImage(m_CollapsedImage, Width - m_CollapsedImage.Width - ChevronMargin, 2 + imageOffsetY);
                    ChevronWidth = m_CollapsedImage.Width;
                }
            }

            return ChevronWidth;
        }

        private void DrawChevronsAIC(Graphics g, int x, int y, int offset)
        {
            if (IsExpanded == false)
            {
                DrawChevronAIC(g, x + offset, y + 1 * offset, -offset);
                DrawChevronAIC(g, x + offset, y + 2 * offset, -offset);
            }
            else
            {
                DrawChevronAIC(g, x + offset, y + 2 * offset, offset);
                DrawChevronAIC(g, x + offset, y + 3 * offset, offset);
            }
        }

        private void DrawChevronAIC(Graphics g, int x, int y, int offset)
        {
            Pen p = null;

            if (m_IsCaptionHighlighted)
                p = new Pen(m_CaptionTextHighlightColor);
            else
                p = new Pen(m_CaptionTextColor);
            Point[] points =
            {
                new Point(x, y), new Point(x + Math.Abs(offset), y - offset),
                new Point(x + 2 * Math.Abs(offset), y)
            };
            g.DrawLines(p, points);
        }

        private int DrawChevronArrowsInCircle(Graphics g, int OffsetY)
        {
            var dm = 15;
            var btnOrigin = new Point(Width - dm - 5, 5 + OffsetY);
            var btnSize = new Size(dm, dm);
            var btnRect = new Rectangle(btnOrigin, btnSize);
            g.DrawEllipse(new Pen(CaptionTextColor), btnRect);
            DrawChevronsAIC(g, btnRect.X, btnRect.Y, Convert.ToInt32(btnRect.Width / 4));
            return dm;
        }

        #endregion

        #endregion
    }

    #region '"DBGroupBoxXPDesigner"' 

    public class DBGroupBoxXPDesigner : ParentControlDesigner
    {
        private readonly Pen m_BorderPen = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
        private DBGroupBoxXP m_Control;

        public DBGroupBoxXPDesigner()
        {
            m_BorderPen.DashStyle = DashStyle.Dash;
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            m_Control = (DBGroupBoxXP) Control;
        }


        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            base.OnPaintAdornments(pe);
            pe.Graphics.DrawRectangle(m_BorderPen, 0, 0, m_Control.Width - 2, m_Control.Height - 2);

            m_Control.ExpandedHeight = m_Control.Height;
        }
    }

    #endregion
}