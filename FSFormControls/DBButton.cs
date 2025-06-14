#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBButton.bmp")]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    public class DBButton : DBUserControl, IButtonControl
    {
        #region ButtonStyleType enum

        public enum ButtonStyleType
        {
            DropDown,
            Normal,
            Push,
            Troggle
        }

        #endregion

        private ButtonStyleType m_ButtonStyle = ButtonStyleType.Normal;
        private bool m_DownMouse;
        private Color m_FillColorEnd = Color.White;
        private Color m_FillColorStart = Color.LightGray;
        private Color m_FillHoverColorEnd = Color.Beige;
        private Color m_FillHoverColorStart = Color.Beige;
        private bool m_Gradient;
        private LinearGradientMode m_GradientMode = LinearGradientMode.Horizontal;
        private Color m_OutlineColor = Color.White;
        private bool m_SwapMouse;
        private Color m_TextColorEnd = Color.Black;
        private Color m_TextColorStart = Color.Blue;
        private string m_ToolTip = "";


        #region Events

        public new event EventHandler Click;

        #endregion


        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return button.Text; }
            set { button.Text = value; }
        }

        public Color FillColorStart
        {
            get { return m_FillColorStart; }
            set
            {
                m_FillColorStart = value;
                button.Refresh();
            }
        }

        public string Key
        {
            get { return button.Name; }
            set { button.Name = value; }
        }

        public Color FillColorEnd
        {
            get { return m_FillColorEnd; }
            set
            {
                m_FillColorEnd = value;
                button.Refresh();
            }
        }

        public Color FillHoverColorStart
        {
            get { return m_FillHoverColorStart; }
            set
            {
                m_FillHoverColorStart = value;
                button.Refresh();
            }
        }

        public Color FillHoverColorEnd
        {
            get { return m_FillHoverColorEnd; }
            set
            {
                m_FillHoverColorEnd = value;
                button.Refresh();
            }
        }

        public Color TextColorStart
        {
            get { return m_TextColorStart; }
            set
            {
                m_TextColorStart = value;
                button.Refresh();
            }
        }

        public Color TextColorEnd
        {
            get { return m_TextColorEnd; }
            set
            {
                m_TextColorEnd = value;
                button.Refresh();
            }
        }

        public Font TextFont
        {
            get { return button.Font; }
            set { button.Font = value; }
        }

        public DBAppearance Appearance { get; set; }

        public ContentAlignment TextAlign
        {
            get { return button.TextAlign; }
            set { button.TextAlign = value; }
        }

        public FlatStyle FlatStyle
        {
            get { return button.FlatStyle; }
            set { button.FlatStyle = value; }
        }

        public ButtonStyleType ButtonStyle
        {
            get { return m_ButtonStyle; }
            set
            {
                var resources = new ResourceManager(typeof(DBButton));
                m_ButtonStyle = value;
                if (value == ButtonStyleType.DropDown)
                {
                    button.ImageAlign = ContentAlignment.MiddleRight;
                    button.Image = (Bitmap) resources.GetObject("button.Image");
                }
                else
                {
                    button.ImageAlign = ContentAlignment.MiddleCenter;
                    button.Image = null;
                }

                button.Refresh();
            }
        }

        public ContextMenuStrip DropDownMenu { get; set; }

        public string ToolTip
        {
            get { return m_ToolTip; }
            set
            {
                m_ToolTip = value;
                ToolTip1.SetToolTip(button, m_ToolTip);
            }
        }

        public Image Image
        {
            get { return button.Image; }
            set { button.Image = value; }
        }

        public ContentAlignment ImageAlign
        {
            get { return button.ImageAlign; }
            set { button.ImageAlign = value; }
        }

        public LinearGradientMode GradientMode
        {
            get { return m_GradientMode; }
            set
            {
                m_GradientMode = value;
                button.Refresh();
            }
        }

        public bool Gradient
        {
            get { return m_Gradient; }
            set
            {
                m_Gradient = value;
                button.Refresh();
            }
        }

        public DialogResult DialogResult
        {
            get { return button.DialogResult; }

            set { button.DialogResult = value; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (null != Click) 
                Click(this, e);

            DropDownMenu_PopUp(this, e);
        }


        private void DropDownMenu_PopUp(object sender, EventArgs e)
        {
            var pos = new Point();

            if (ButtonStyle == ButtonStyleType.DropDown)
                if (DropDownMenu != null)
                {
                    pos = Location;
                    pos.Y = pos.Y + button.Height;

                    DropDownMenu.Show(ParentForm, pos);
                }
        }

        private void Button1_Paint(object sender, PaintEventArgs e)
        {
            Brush GradiantBrush = null;
            Brush TextBrush = null;
            Brush HoverBrush = null;
            var StringSize = new SizeF();

            if (m_Gradient == false) 
                return;

            var recF = new RectangleF(0, 0, Width, Height);
            HoverBrush = new LinearGradientBrush(recF, FillHoverColorStart, FillHoverColorEnd, m_GradientMode);
            GradiantBrush = new LinearGradientBrush(recF, m_FillColorStart, m_FillColorEnd, m_GradientMode);
            TextBrush = new LinearGradientBrush(recF, m_TextColorStart, m_TextColorEnd, m_GradientMode);

            if (m_SwapMouse)
                e.Graphics.FillRectangle(HoverBrush, ClientRectangle);
            else
                e.Graphics.FillRectangle(GradiantBrush, ClientRectangle);

            if (m_DownMouse)
                ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Bump);
            else
                ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Etched);

            StringSize = e.Graphics.MeasureString(button.Text, TextFont);
            switch (TextAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                    e.Graphics.DrawString(button.Text, TextFont, TextBrush,
                        Convert.ToInt32(Width / 2) - Convert.ToInt32(StringSize.Width / 2),
                        Convert.ToInt32(Height / 2) - Convert.ToInt32(StringSize.Height / 2));
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.TopLeft:
                    e.Graphics.DrawString(button.Text, TextFont, TextBrush, Convert.ToInt32(0 + 5),
                        Convert.ToInt32(Height / 2) - Convert.ToInt32(StringSize.Height / 2));
                    break;
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                    e.Graphics.DrawString(button.Text, TextFont, TextBrush,
                        Convert.ToInt32(Width - StringSize.Width - 5),
                        Convert.ToInt32(Height / 2) - Convert.ToInt32(StringSize.Height / 2));
                    break;
            }
        }


        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            m_SwapMouse = false;
            button.Refresh();
            base.OnMouseLeave(new EventArgs());
        }


        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            m_SwapMouse = true;
            button.Refresh();
            base.OnMouseEnter(new EventArgs());
        }


        private void Button1_MouseDown(object sender, MouseEventArgs e)
        {
            m_DownMouse = true;
            button.Refresh();
            base.OnMouseDown(e);
        }


        private void Button1_MouseUp(object sender, MouseEventArgs e)
        {
            m_DownMouse = false;
            button.Refresh();
            base.OnMouseUp(e);
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private Button button;
        internal ToolTip ToolTip1;
        private IContainer components;


        private void Init()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);

            button.Click += Button1_Click;
            button.Paint += Button1_Paint;
            button.MouseLeave += Button1_MouseLeave;
            button.MouseEnter += Button1_MouseEnter;
            button.MouseDown += Button1_MouseDown;
            button.MouseUp += Button1_MouseUp;
        }

        public DBButton()
        {
            Init();
        }

        public DBButton(string text)
        {
            Init();

            Text = text;
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
            button = new Button();
            ToolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // Button1
            // 
            button.Dock = DockStyle.Fill;
            button.ImageAlign = ContentAlignment.MiddleRight;
            button.Location = new Point(0, 0);
            button.Name = "Button1";
            button.Size = new Size(95, 39);
            button.TabIndex = 0;
            // 
            // DBButton
            // 
            Controls.Add(button);
            Name = "DBButton";
            Size = new Size(95, 39);
            ResumeLayout(false);
        }

        public void NotifyDefault(bool value)
        {
            button.NotifyDefault(value);
        }

        public void PerformClick()
        {
            button.PerformClick();
        }

        #endregion
    }
}