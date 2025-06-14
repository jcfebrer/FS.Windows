#region

using FSException;
using FSLibrary;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBCombo.bmp")]
    [ToolboxItem(true)]
    public class DBFontCombo : ComboBox
    {
        private bool Loading;

        public DBFontCombo()
        {
            Text = "";
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            LoadFonts();
        }

        private void LoadFonts()
        {
            Loading = true;
            var mFC = new FontConverter();
            try
            {
                foreach (var family in FontFamily.Families)
                {
                    var cmbItm = new ComboBoxFontItem();
                    if (family.IsStyleAvailable(FontStyle.Regular))
                    {
                        cmbItm.Text = family.Name;
                        cmbItm.Font = (Font) mFC.ConvertFromString(family.Name);
                        Items.Add(cmbItm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var index = e.Index;
            e.DrawBackground();
            e.DrawFocusRectangle();
            var item = new ComboBoxFontItem();
            var bounds = new Rectangle();
            bounds = e.Bounds;

            if (index == -1)
                index = 0;

            try
            {
                item = (ComboBoxFontItem) Items[index];
                e.Graphics.DrawString(item.Text, item.Font, new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
            }
            catch (Exception)
            {
                e.Graphics.DrawString(index.ToString(), e.Font, new SolidBrush(e.ForeColor), bounds.Left,
                    bounds.Top);
            }

            base.OnDrawItem(e);
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            Loading = false;
            base.OnMouseLeave(e);
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            Loading = false;
            base.OnMouseUp(e);
        }


        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            try
            {
                if (!Loading)
                {
                    var OFC = new FontConverter();

                    Font = (Font) OFC.ConvertFromString(Text);
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }
    }

    public class ComboBoxFontItem
    {
        public ComboBoxFontItem()
        {
            Text = "";
        }

        public ComboBoxFontItem(string text)
        {
            Text = text;
        }

        public ComboBoxFontItem(string text, int imageIndex, string AltValue)
        {
            Text = text;
        }

        public string Text { get; set; }

        public Font Font { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}