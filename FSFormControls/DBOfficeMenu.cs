#region

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

#endregion

#if NETFRAMEWORK
namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBOfficeMenu.bmp")]
    [ToolboxItem(true)]
    public class DBOfficeMenu : Component
    {
        public static ImageList m_imageList;

        public static NameValueCollection picDetails = new NameValueCollection();
        private Container components;

        public DBOfficeMenu(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public DBOfficeMenu()
        {
            InitializeComponent();
        }

        public ImageList ImageList
        {
            get { return m_imageList; }
            set { m_imageList = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        public void Start(Form form)
        {
            try
            {
                var menu = form.Menu;

                foreach (MenuItem mi in menu.MenuItems)
                {
                    mi.MeasureItem += mainMenuItem_MeasureItem;
                    mi.DrawItem += mainMenuItem_DrawItem;
                    mi.OwnerDraw = true;

                    InitMenuItem(mi);
                }


                var cmenu = form.ContextMenu;
                if (!(cmenu == null)) InitMenuItem(cmenu);

                foreach (Control c in form.Controls)
                    if (!(c.ContextMenu == null))
                        InitMenuItem(c.ContextMenu);
            }
            catch
            {
            }
        }


        public void EndMenu(Form form)
        {
            try
            {
                var menu = form.Menu;

                foreach (MenuItem mi in menu.MenuItems)
                {
                    mi.MeasureItem -= mainMenuItem_MeasureItem;
                    mi.DrawItem -= mainMenuItem_DrawItem;
                    mi.OwnerDraw = false;

                    UninitMenuItem(mi);
                }


                var cmenu = form.ContextMenu;
                if (!(cmenu == null)) UninitMenuItem(cmenu);

                foreach (Control c in form.Controls)
                    if (!(c.ContextMenu == null))
                        UninitMenuItem(c.ContextMenu);
            }
            catch
            {
            }
        }


        private void InitMenuItem(Menu mi)
        {
            foreach (MenuItem m in mi.MenuItems)
            {
                m.MeasureItem += menuItem_MeasureItem;
                m.DrawItem += menuItem_DrawItem;
                m.OwnerDraw = true;

                InitMenuItem(m);
            }
        }


        private void UninitMenuItem(Menu mi)
        {
            foreach (MenuItem m in mi.MenuItems)
            {
                m.MeasureItem -= menuItem_MeasureItem;
                m.DrawItem -= menuItem_DrawItem;
                m.OwnerDraw = false;

                UninitMenuItem(m);
            }
        }


        private void menuItem_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            var mi = (MenuItem) sender;
            if (mi.Text == "-")
            {
                e.ItemHeight = 7;
            }
            else
            {
                var miSize = e.Graphics.MeasureString(mi.Text, Globals.menuFont);
                var scWidth = 0;
                if (mi.Shortcut != Shortcut.None)
                {
                    var scSize = e.Graphics.MeasureString(mi.Shortcut.ToString(), Globals.menuFont);
                    scWidth = Convert.ToInt32(scSize.Width);
                }

                var miHeight = Convert.ToInt32(miSize.Height) + 7;
                if (miHeight < 25) miHeight = Globals.MIN_MENU_HEIGHT;
                e.ItemHeight = miHeight;
                e.ItemWidth = Convert.ToInt32(miSize.Width) + scWidth + Globals.PIC_AREA_SIZE * 2;
            }
        }


        private void menuItem_DrawItem(object sender, DrawItemEventArgs e)
        {
            MenuItemDrawing.DrawMenuItem(e, (MenuItem) sender);
        }


        private void mainMenuItem_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            var mi = (MenuItem) sender;
            var miSize = e.Graphics.MeasureString(mi.Text, Globals.menuFont);
            e.ItemWidth = Convert.ToInt32(miSize.Width);
        }


        private void mainMenuItem_DrawItem(object sender, DrawItemEventArgs e)
        {
            MainMenuItemDrawing.DrawMenuItem(e, (MenuItem) sender);
        }


        public void AddPicture(MenuItem mi, int index)
        {
            picDetails.Add(mi.Handle.ToString(), index.ToString());
        }


        public static Bitmap GetItemPicture(MenuItem mi)
        {
            if (m_imageList == null) return null;

            var picIndex = picDetails.GetValues(mi.Handle.ToString());

            if (picIndex == null)
                return null;
            return (Bitmap) m_imageList.Images[Convert.ToInt32(picIndex[0])];
        }

        #region '"Component Designer generated code"' 

        private void InitializeComponent()
        {
            components = new Container();
        }

        #endregion
    }


    public class MenuItemDrawing
    {
        public static void DrawMenuItem(DrawItemEventArgs e, MenuItem mi)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                DrawSelectionRect(e, mi);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Globals.MenuBgColor), e.Bounds);
                DrawPictureArea(e, mi);
            }

            if ((e.State & DrawItemState.Checked) == DrawItemState.Checked) DrawCheckBox(e, mi);

            DrawMenuText(e, mi);

            DrawItemPicture(e, mi);
        }


        private static void DrawMenuText(DrawItemEventArgs e, MenuItem mi)
        {
            Brush textBrush = new SolidBrush(Globals.TextColor);

            if (mi.Text == "-")
            {
                e.Graphics.DrawLine(new Pen(Globals.MenuLightColor), e.Bounds.X + Globals.PIC_AREA_SIZE + 3,
                    e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Y + 2);
            }
            else
            {
                var sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;

                var rect = new RectangleF(Globals.PIC_AREA_SIZE + 2, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                var miText = mi.Text.Replace("&", "");

                if (mi.Enabled)
                    textBrush = new SolidBrush(Globals.TextColor);
                else
                    textBrush = new SolidBrush(Globals.TextDisabledColor);

                e.Graphics.DrawString(miText, Globals.menuFont, textBrush, rect, sf);

                DrawShortCutText(e, mi);
            }
        }


        private static void DrawShortCutText(DrawItemEventArgs e, MenuItem mi)
        {
            if ((mi.Shortcut != Shortcut.None) & mi.ShowShortcut)
            {
                var scSize = e.Graphics.MeasureString(mi.Shortcut.ToString(), Globals.menuFont);

                var rect = new Rectangle(e.Bounds.Width - Convert.ToInt32(scSize.Width) - Globals.PIC_AREA_SIZE,
                    e.Bounds.Y, Convert.ToInt32(scSize.Width) + 5, e.Bounds.Height);

                var sf = new StringFormat();
                sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                sf.LineAlignment = StringAlignment.Center;

                if (mi.Enabled)
                {
                    var rectF = new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                    e.Graphics.DrawString(mi.Shortcut.ToString(), Globals.menuFont, new SolidBrush(Globals.TextColor),
                        rectF, sf);
                }
                else
                {
                    var rectF = new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                    e.Graphics.DrawString(mi.Shortcut.ToString(), Globals.menuFont,
                        new SolidBrush(Globals.TextDisabledColor), rectF, sf);
                }
            }
        }


        private static void DrawPictureArea(DrawItemEventArgs e, MenuItem mi)
        {
            var rect = new Rectangle(e.Bounds.X - 1, e.Bounds.Y, Globals.PIC_AREA_SIZE, e.Bounds.Height);

            Brush b = new LinearGradientBrush(rect, Globals.MenuDarkColor2, Globals.MenuLightColor2, 180.0F, false);

            e.Graphics.FillRectangle(b, rect);
        }


        private static void DrawItemPicture(DrawItemEventArgs e, MenuItem mi)
        {
            const int MAX_PIC_SIZE = 16;

            Image img = DBOfficeMenu.GetItemPicture(mi);

            if (!(img == null))
            {
                var width = img.Width > MAX_PIC_SIZE ? MAX_PIC_SIZE : img.Width;
                var height = img.Height > MAX_PIC_SIZE ? MAX_PIC_SIZE : img.Height;


                var x = e.Bounds.X + 2;
                var y = Convert.ToInt32(e.Bounds.Y + (e.Bounds.Height - height) / 2);

                var rect = new Rectangle(x, y, width, height);

                if (mi.Enabled)
                {
                    e.Graphics.DrawImage(img, x, y, width, height);
                }
                else
                {
                    var myColorMatrix = new ColorMatrix();
                    myColorMatrix.Matrix00 = 1.0F;
                    myColorMatrix.Matrix11 = 1.0F;
                    myColorMatrix.Matrix22 = 1.0F;
                    myColorMatrix.Matrix33 = 1.3F;
                    myColorMatrix.Matrix44 = 1.0F;

                    var imageAttr = new ImageAttributes();
                    imageAttr.SetColorMatrix(myColorMatrix);

                    e.Graphics.DrawImage(img, rect, 0, 0, width, height, GraphicsUnit.Pixel, imageAttr);
                }
            }
        }


        private static void DrawSelectionRect(DrawItemEventArgs e, MenuItem mi)
        {
            if (mi.Enabled)
            {
                e.Graphics.FillRectangle(new SolidBrush(Globals.SelectionColor), e.Bounds);
                e.Graphics.DrawRectangle(new Pen(Globals.MenuDarkColor), e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1,
                    e.Bounds.Height - 1);
            }
        }


        private static void DrawCheckBox(DrawItemEventArgs e, MenuItem mi)
        {
            var cbSize = Globals.PIC_AREA_SIZE - 5;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(e.Bounds.X + 1, Convert.ToInt32(e.Bounds.Y + (e.Bounds.Height - cbSize) / 2),
                cbSize, cbSize);

            var pen = new Pen(Color.Black, 1.7F);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(new SolidBrush(Globals.DarkCheckBoxColor), rect);
            else
                e.Graphics.FillRectangle(new SolidBrush(Globals.CheckBoxColor), rect);

            e.Graphics.DrawRectangle(new Pen(Globals.MenuDarkColor), rect);

            var img = DBOfficeMenu.GetItemPicture(mi);

            if (img == null)
            {
                e.Graphics.DrawLine(pen, e.Bounds.X + 7, e.Bounds.Y + 10, e.Bounds.X + 10, e.Bounds.Y + 14);
                e.Graphics.DrawLine(pen, e.Bounds.X + 10, e.Bounds.Y + 14, e.Bounds.X + 15, e.Bounds.Y + 9);
            }
        }
    }


    public class MainMenuItemDrawing
    {
        public static void DrawMenuItem(DrawItemEventArgs e, MenuItem mi)
        {
            if ((e.State & DrawItemState.HotLight) == DrawItemState.HotLight)
            {
                DrawHoverRect(e, mi);
            }
            else if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                DrawSelectionRect(e, mi);
            }
            else
            {
                var rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - 1);

                e.Graphics.FillRectangle(new SolidBrush(Globals.MainColor), rect);
                e.Graphics.DrawRectangle(new Pen(Globals.MainColor), rect);
            }

            var sf = new StringFormat();

            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            var miText = mi.Text.Replace("&", "");
            var rectF = new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            e.Graphics.DrawString(miText, Globals.menuFont, new SolidBrush(Globals.TextColor), rectF, sf);
        }


        private static void DrawHoverRect(DrawItemEventArgs e, MenuItem mi)
        {
            var rect = new Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height - 2);

            Brush b = new LinearGradientBrush(rect, Color.White, Globals.CheckBoxColor, 90.0F, false);

            e.Graphics.FillRectangle(b, rect);

            e.Graphics.DrawRectangle(new Pen(Color.Black), rect);
        }


        private static void DrawSelectionRect(DrawItemEventArgs e, MenuItem mi)
        {
            var rect = new Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height - 2);

            Brush b = new LinearGradientBrush(rect, Globals.MenuBgColor, Globals.MenuDarkColor2, 90.0F, false);

            e.Graphics.FillRectangle(b, rect);

            e.Graphics.DrawRectangle(new Pen(Color.Black), rect);
        }
    }


    class Globals
    {
        public static int PIC_AREA_SIZE = 24;
        public static int MIN_MENU_HEIGHT = 22;
        public static Font menuFont = SystemInformation.MenuFont;
        public static Color CheckBoxColor = Color.FromArgb(255, 192, 111);
        public static Color DarkCheckBoxColor = Color.FromArgb(254, 128, 62);
        public static Color SelectionColor = Color.FromArgb(255, 238, 194);
        public static Color TextColor = Color.FromKnownColor(KnownColor.MenuText);
        public static Color TextDisabledColor = Color.FromKnownColor(KnownColor.GrayText);
        public static Color MenuBgColor = Color.White;
        public static Color MainColor = Color.FromKnownColor(KnownColor.Control);
        public static Color MenuDarkColor = Color.FromKnownColor(KnownColor.ActiveCaption);
        public static Color MenuDarkColor2 = Color.FromArgb(110, Color.FromKnownColor(KnownColor.ActiveCaption));
        public static Color MenuLightColor = Color.FromKnownColor(KnownColor.InactiveCaption);
        public static Color MenuLightColor2 = Color.FromArgb(50, Color.FromKnownColor(KnownColor.InactiveCaption));
    }
}
#endif