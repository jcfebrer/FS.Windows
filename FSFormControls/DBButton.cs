using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBButton.bmp")]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    public class DBButton : Button
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
        public ContextMenuStrip DropDownMenu { get; set; }
        public string About { get; set; }

        private Color m_TextColorEnd = Color.Black;
        private Color m_TextColorStart = Color.Blue;
        private Color m_FillColorEnd = Color.White;
        private Color m_FillColorStart = Color.LightGray;
        private Color m_FillHoverColorEnd = Color.Beige;
        private Color m_FillHoverColorStart = Color.Beige;
        private bool m_Gradient;
        private LinearGradientMode m_GradientMode = LinearGradientMode.Horizontal;
        private string m_ToolTip = "";

        public DBButton()
        {
            Init();
        }

        public DBButton(string text)
        {
            this.Name = text;
            Init();
        }

        private void Init()
        {
            if (DesignMode)
                return;

            this.Click += DBButton_Click;
        }

        private void DBButton_Click(object sender, EventArgs e)
        {
            DropDownMenu_PopUp(this, e);
        }

        private void DropDownMenu_PopUp(object sender, EventArgs e)
        {
            var pos = new Point();

            if (ButtonStyle == ButtonStyleType.DropDown)
                if (DropDownMenu != null)
                {
                    pos = Location;
                    pos.Y = pos.Y + this.Height;

                    DropDownMenu.Show(this, pos);
                }
        }

        public string Key
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        public ButtonStyleType ButtonStyle
        {
            get { return m_ButtonStyle; }
            set
            {
                var resources = new ResourceManager(typeof(DBButtonEx));
                m_ButtonStyle = value;
                if (value == ButtonStyleType.DropDown)
                {
                    this.ImageAlign = ContentAlignment.MiddleRight;
                    this.Image = (Bitmap)resources.GetObject("button.Image");
                }
                else
                {
                    this.ImageAlign = ContentAlignment.MiddleCenter;
                    this.Image = null;
                }

                this.Refresh();
            }
        }

        private DBAppearance m_Appearance = new DBAppearance();
        public DBAppearance Appearance
        {
            get { return m_Appearance; }
            set
            {

                if (value != null)
                {
                    this.ForeColor = value.ForeColor;
                    this.BackColor = value.BackColor;
                    this.Image = value.Image;
                }
            }
        }

        public Color FillColorStart
        {
            get { return m_FillColorStart; }
            set
            {
                m_FillColorStart = value;
                this.Refresh();
            }
        }

        public Color FillColorEnd
        {
            get { return m_FillColorEnd; }
            set
            {
                m_FillColorEnd = value;
                this.Refresh();
            }
        }

        public Color FillHoverColorStart
        {
            get { return m_FillHoverColorStart; }
            set
            {
                m_FillHoverColorStart = value;
                this.Refresh();
            }
        }

        public Color FillHoverColorEnd
        {
            get { return m_FillHoverColorEnd; }
            set
            {
                m_FillHoverColorEnd = value;
                this.Refresh();
            }
        }

        public Color TextColorStart
        {
            get { return m_TextColorStart; }
            set
            {
                m_TextColorStart = value;
                this.Refresh();
            }
        }

        public Color TextColorEnd
        {
            get { return m_TextColorEnd; }
            set
            {
                m_TextColorEnd = value;
                this.Refresh();
            }
        }

        public LinearGradientMode GradientMode
        {
            get { return m_GradientMode; }
            set
            {
                m_GradientMode = value;
                this.Refresh();
            }
        }

        public bool Gradient
        {
            get { return m_Gradient; }
            set
            {
                m_Gradient = value;
                this.Refresh();
            }
        }

        public Font TextFont
        {
            get { return this.Font; }
            set { this.Font = value; }
        }

        public string ToolTip
        {
            get { return m_ToolTip; }
            set
            {
                m_ToolTip = value;
                System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
                toolTip.SetToolTip(this, m_ToolTip);
            }
        }
    }
}
