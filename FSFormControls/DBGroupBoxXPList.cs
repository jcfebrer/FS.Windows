#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [Designer(typeof(DBGroupBoxXPListDesigner))]
    [DesignTimeVisibleAttribute(true)]
    [ToolboxItem(true)]
    public class DBGroupBoxXPList : DBUserControl
    {
        #region '"Constants"' 

        private const int initialVerticalSpacing = 10;
        private const int horizontalSpacing = 8;
        private const int verticalSpacing = 14;

        private static readonly Color lightBackColor = Color.FromArgb(123, 162, 239);
        private static readonly Color darkBackColor = Color.FromArgb(99, 117, 222);

        #endregion

        #region '"Members"' 

        //private DBGroupBoxXPComparer m_DBGroupBoxXPComparer;
        private readonly SortedList m_ControlList = new SortedList(); //m_DBGroupBoxXPComparer
        private Color m_BgColorDark = darkBackColor;
        private Color m_BgColorLight = lightBackColor;
        private int m_NextControlKey;

        #endregion

        #region '"Windows Form Designer generated code "' 

        private readonly IContainer components = null;

        public DBGroupBoxXPList()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, false);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            Load += DBGroupBoxXPList_Load;
            SizeChanged += DBGroupBoxXPList_SizeChanged;
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
            // DBGroupBoxXPList
            // 
            AutoScroll = true;
            Name = "DBGroupBoxXPList";
            ResumeLayout(false);
        }

        #endregion

        #region '"Public Properties"' 

        [Description("Light color used in gradient for background.")]
        [DefaultValue(typeof(Color), "123,162,239")]
        [Category("Appearance")]
        public Color BackColorLight
        {
            get { return m_BgColorLight; }
            set
            {
                m_BgColorLight = value;
                Invalidate();
            }
        }

        [Description("Dark color used in gradient for background.")]
        [DefaultValue(typeof(Color), "99,117,222")]
        [Category("Appearance")]
        public Color BackColorDark
        {
            get { return m_BgColorDark; }
            set
            {
                m_BgColorDark = value;
                Invalidate();
            }
        }

        #endregion

        #region '"Overrides"' 

        private void DBGroupBoxXPList_Load(object sender, EventArgs e)
        {
            BackColor = m_BgColorDark;
            m_NextControlKey = m_ControlList.Count;
            AutoScroll = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            var rect = new Rectangle(0, AutoScrollPosition.Y, Width, Height);

            var b = new LinearGradientBrush(DisplayRectangle, m_BgColorLight, m_BgColorDark,
                LinearGradientMode.Vertical);

            pevent.Graphics.FillRectangle(b, DisplayRectangle);
        }

        private void DBGroupBoxXPList_SizeChanged(object sender, EventArgs e)
        {
            Control ctl = null;
            var enumerator = m_ControlList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ctl = (Control) enumerator.Value;
                ctl.Width = Width - 3 * horizontalSpacing;
            }

            Invalidate();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control.GetType() == typeof(DBGroupBoxXP))
            {
                var x = (DBGroupBoxXP) e.Control;

                if (x.Width <= Width)
                {
                    x.Left = horizontalSpacing;
                    x.Width = Width - 3 * horizontalSpacing;
                    x.Top = GetNextTopPosition();
                    x.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                }

                x.Collapsed += ControlCollapsed;
                x.Expanded += ControlExpanded;
                x.Collapsing += ControlCollapsing;
                x.Expanding += ControlExpanding;
                x.Tag = m_NextControlKey;
                m_NextControlKey += 1;
                m_ControlList.Add(x.Tag, x);
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            Control ctl = null;
            var prevTop = e.Control.Top;
            var newTop = 0;

            if (e.Control.GetType() == typeof(DBGroupBoxXP))
            {
                var enumerator = m_ControlList.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ctl = (Control) enumerator.Value;
                    if (ctl.Top > prevTop)
                    {
                        newTop = prevTop;
                        prevTop = ctl.Top;
                        ctl.Top = newTop;
                    }
                }

                var x = (DBGroupBoxXP) e.Control;

                x.Collapsed -= ControlCollapsed;
                x.Expanded -= ControlExpanded;
                x.Collapsing -= ControlCollapsing;
                x.Expanding -= ControlExpanding;
                m_ControlList.Remove(e.Control.Tag);
            }
        }

        #endregion

        #region '"Private Helpers"' 

        private int GetNextTopPosition()
        {
            Control ctl = null;
            var max = initialVerticalSpacing;
            var YPos = 0;

            var enumerator = m_ControlList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ctl = (Control) enumerator.Value;
                YPos = ctl.Top + ctl.Height;
                if (YPos > max) max = YPos;
            }

            if (max != initialVerticalSpacing) max += verticalSpacing;

            return max;
        }

        private int FixRGB(int RGBValue)
        {
            if ((RGBValue >= 0) & (RGBValue <= 255))
                return RGBValue;
            if (RGBValue < 0)
                return 0;
            return 255;
        }

        #endregion

        #region '"Public Methods"' 

        public void ControlExpanding(DBGroupBoxXP x, int delta)
        {
            Control ctl = null;

            var enumerator = m_ControlList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ctl = (Control) enumerator.Value;
                if (ctl.Top > x.Top) ctl.Top += delta;
            }
        }

        public void ControlCollapsing(DBGroupBoxXP x, int delta)
        {
            Control ctl = null;

            var enumerator = m_ControlList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ctl = (Control) enumerator.Value;
                if (ctl.Top > x.Top) ctl.Top -= delta;
            }
        }

        public void ControlExpanded(DBGroupBoxXP x)
        {
            Control ctl = null;
            var enumerator = m_ControlList.GetEnumerator();

            while (enumerator.MoveNext())
            {
                ctl = (Control) enumerator.Value;
                if (ctl.Top > x.Top) ctl.Top += x.ExpandedHeight - x.CaptionHeight;
            }
        }

        public void ControlCollapsed(DBGroupBoxXP x)
        {
            Control ctl = null;
            var enumerator = m_ControlList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ctl = (Control) enumerator.Value;
                if (ctl.Top > x.Top) ctl.Top -= x.ExpandedHeight - x.CaptionHeight;
            }
        }

        public void ExpandAll()
        {
            DBGroupBoxXP ctl = null;

            var enumerator = m_ControlList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ctl = (DBGroupBoxXP) enumerator.Value;
                ctl.Expand();
            }
        }

        public void CollapseAll()
        {
            DBGroupBoxXP ctl = null;

            var enumerator = m_ControlList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ctl = (DBGroupBoxXP) enumerator.Value;
                ctl.Collapse();
            }
        }

        #endregion
    }


    public class DBGroupBoxXPComparer : IComparer

        #region '"Public Methods"' 

    {
        // interface methods implemented by Compare

        #region IComparer Members

        int IComparer.Compare(object x, object y)
        {
            return Compare(x, y);
        }

        #endregion

        public int Compare(object x, object y)
        {
            var xp1 = (DBGroupBoxXP) x;
            var xp2 = (DBGroupBoxXP) y;
            var result = 0;

            if (xp1.Top < xp2.Top) result = -1;
            if (xp1.Top > xp2.Top) result = 1;

            return result;
        }

        #endregion
    }

    #region '"DBGroupBoxXPListDesigner"' 

    public class DBGroupBoxXPListDesigner : ParentControlDesigner
    {
        private readonly Pen _borderPen = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
        private DBGroupBoxXPList _control;

        public DBGroupBoxXPListDesigner()
        {
            _borderPen.DashStyle = DashStyle.Dash;
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            _control = (DBGroupBoxXPList) Control;

            _control.AutoScroll = false;
        }


        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            base.OnPaintAdornments(pe);
            pe.Graphics.DrawRectangle(_borderPen, 0, 0, _control.Width - 2, _control.Height - 2);
        }
    }

    #endregion
}