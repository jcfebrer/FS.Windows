#region

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using FSLibrary;
using Encoder = System.Drawing.Imaging.Encoder;
using FSException;

#endregion

namespace FSFormControls
{
    public class DBFormPrint
    {
        #region Delegates

        public delegate void ControlPrinting(
            Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls);

        #endregion

        #region OrientationENum enum

        public enum OrientationENum
        {
            Automatic = 1,
            Portrait = 2,
            Lanscape = 3
        }

        #endregion

        #region ParentControlPrinting enum

        public enum ParentControlPrinting
        {
            BeforeChilds = 1,
            AfterChilds = 2
        }

        #endregion

        private readonly ArrayList m_DelegatesforControls;
        private readonly Control m_f;
        private readonly ArrayList m_TextBoxLikeControl;
        public float BottomMargin;
        public ControlPrinting DelegatePrintingReportTitle;

        public bool DisabledControlsInGray;
        public bool LabelInBold = true;
        private Brush m_Brush;
        private int m_indent;
        private MultiPageManagement m_MultiPage;
        private Pen m_Pen;
        private StringBuilder m_traceLog;
        private float m_xform;
        public OrientationENum Orientation = OrientationENum.Automatic;
        public bool PageNumbering = false;
        public string PageNumberingFormat = "Página {0}";
        public bool PrintPreview = true;
        public bool TabControlBoxed = true;
        public bool TextBoxBoxed;
        public float TopMargin;

        public DBFormPrint(Control f)
        {
            m_f = f;
            m_TextBoxLikeControl = new ArrayList();
            m_DelegatesforControls = new ArrayList();
            m_Pen = new Pen(Color.Black);
            AddTextBoxLikeControl("ComboBox");
            AddTextBoxLikeControl("DBCombo");
            AddTextBoxLikeControl("DateTimePicker");
            AddTextBoxLikeControl("DateTimeSlicker");
            AddTextBoxLikeControl("NumericUpDown");
            AddDelegateToPrintControl("TextBox", PrintTextBox);
            AddDelegateToPrintControl("DBTextBox", PrintTextBox);
            AddDelegateToPrintControl("System.Windows.Forms.ToolBar", PrintSkipControl);
            AddDelegateToPrintControl("FSFormControls.DBToolBar", PrintSkipControl);
            AddDelegateToPrintControl("System.Windows.Forms.Label", PrintLabel);
            AddDelegateToPrintControl("FSFormControls.DBLabel", PrintLabel);
            AddDelegateToPrintControl("System.Windows.Forms.CheckBox", PrintCheckBox);
            AddDelegateToPrintControl("FSFormControls.DBCheckBox", PrintCheckBox);
            AddDelegateToPrintControl("System.Windows.Forms.RadioButton", PrintRadioButton);
            AddDelegateToPrintControl("System.Windows.Forms.GroupBox", PrintGroupBox);
            AddDelegateToPrintControl("System.Windows.Forms.Panel", PrintPanel);
            AddDelegateToPrintControl("FSFormControls.DBPanel", PrintPanel);
            AddDelegateToPrintControl("System.Windows.Forms.TabControl", PrintTabControl);
            AddDelegateToPrintControl("System.Windows.Forms.PictureBox", PrintPictureBox);
            AddDelegateToPrintControl("System.Windows.Forms.ListBox", PrintListBox);
            AddDelegateToPrintControl("System.Windows.Forms.DataGrid", PrintDataGrid);
            AddDelegateToPrintControl("FSFormControls.DBGrid", PrintDataGrid);
            AddDelegateToPrintControl("FSFormControls.DBGridView", PrintDataGridView);
            AddDelegateToPrintControl("System.Windows.Forms.ListView", PrintListView);
        }

        public void AddTextBoxLikeControl(string stringType)
        {
            m_TextBoxLikeControl.Add(stringType);
        }


        public void AddDelegateToPrintControl(string stringType, ControlPrinting printFunction)
        {
            var d = new m_DelegateforControls();
            d.typ = stringType;
            d.PrintFunction = printFunction;
            m_DelegatesforControls.Add(d);
        }


        public void Print()
        {
            try
            {
                var pd = new System.Drawing.Printing.PrintDocument();
                InitPrinting(ref pd);
                pd.PrintPage += pdm_PrintPage;
                pd.DocumentName = m_f.Text;
                if (PrintPreview)
                {
                    var pp = new PrintPreviewDialog();
                    pp.Document = pd;
                    pp.WindowState = FormWindowState.Maximized;
                    pp.ShowDialog();
                }
                else
                {
                    pd.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void PrintToTifFile(string fileName)
        {
            try
            {
                var pd = new System.Drawing.Printing.PrintDocument();
                InitPrinting(ref pd);
                Bitmap bitmapAllPages = null;
                Bitmap bitmapAdditionnalPage = null;
                bitmapAllPages = new Bitmap(pd.DefaultPageSettings.Bounds.Width, pd.DefaultPageSettings.Bounds.Height);
                bitmapAdditionnalPage = new Bitmap(pd.DefaultPageSettings.Bounds.Width,
                    pd.DefaultPageSettings.Bounds.Height);
                Graphics g = null;
                EncoderParameters eps = null;
                eps = new EncoderParameters();
                var firstPage = true;
                do
                {
                    if (firstPage)
                        g = Graphics.FromImage(bitmapAllPages);
                    else
                        g = Graphics.FromImage(bitmapAdditionnalPage);
                    g.Clear(Color.White);
                    m_MultiPage.NewPage(g);
                    float extendedHeight = 0;
                    float y = 0;
                    y = 0;
                    extendedHeight = 0;
                    var scanForChildControls = false;
                    if (DelegatePrintingReportTitle == null)
                        PrintReportTitle(m_f, ParentControlPrinting.BeforeChilds, m_MultiPage, m_xform, y,
                            ref extendedHeight, ref scanForChildControls);
                    else
                        DelegatePrintingReportTitle(m_f, ParentControlPrinting.BeforeChilds, m_MultiPage, m_xform, y,
                            ref extendedHeight, ref scanForChildControls);
                    y += extendedHeight;
                    float globalExtendedHeight = 0;
                    PrintControls(m_f, m_MultiPage, m_xform, y, ref globalExtendedHeight);
                    if (firstPage)
                    {
                        eps.Param[0] = new EncoderParameter(Encoder.SaveFlag,
                            Convert.ToByte(Convert.ToInt64(EncoderValue.MultiFrame)));
                        var infos = ImageCodecInfo.GetImageEncoders();
                        var n = 0;
                        while (!(infos[n].MimeType == "image/tiff")) n += 1;
                        bitmapAllPages.Save(fileName + ".tif", infos[n], eps);
                    }
                    else
                    {
                        eps.Param[0] = new EncoderParameter(Encoder.SaveFlag,
                            Convert.ToByte(
                                Convert.ToInt64(EncoderValue.FrameDimensionPage)));
                        bitmapAllPages.SaveAdd(bitmapAdditionnalPage, eps);
                    }

                    firstPage = false;
                } while (!m_MultiPage.LastPage());

                eps.Param[0] = new EncoderParameter(Encoder.SaveFlag,
                    Convert.ToByte(Convert.ToInt64(EncoderValue.Flush)));
                bitmapAllPages.SaveAdd(eps);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public string GetTrace()
        {
            return m_traceLog.ToString();
        }


        private void InitPrinting(ref System.Drawing.Printing.PrintDocument pd)
        {
            m_traceLog = new StringBuilder();
            m_indent = 0;
            switch (Orientation)
            {
                case OrientationENum.Automatic:
                    if (m_f.Size.Width >
                        pd.DefaultPageSettings.Bounds.Width - pd.DefaultPageSettings.Margins.Right -
                        pd.DefaultPageSettings.Margins.Left)
                        pd.DefaultPageSettings.Landscape = true;
                    break;
                case OrientationENum.Lanscape:
                    pd.DefaultPageSettings.Landscape = true;
                    break;
                case OrientationENum.Portrait:
                    pd.DefaultPageSettings.Landscape = false;
                    break;
            }

            m_xform = Convert.ToSingle((pd.DefaultPageSettings.Bounds.Width - m_f.Size.Width) / 2);
            float pageTop = pd.DefaultPageSettings.Margins.Top;
            float pageBottom = pd.DefaultPageSettings.Bounds.Height - pd.DefaultPageSettings.Margins.Bottom;
            float pageLeft = pd.DefaultPageSettings.Margins.Left;
            float pageRight = pd.DefaultPageSettings.Bounds.Width - pd.DefaultPageSettings.Margins.Right;
            if (TopMargin != 0) pageTop = TopMargin;
            if (BottomMargin != 0) pageTop = BottomMargin;
            m_MultiPage = new MultiPageManagement(pageTop, pageBottom, pageLeft, pageRight, m_f.Font, PageNumbering,
                PageNumberingFormat);
        }


        private void pdm_PrintPage(object sender, PrintPageEventArgs ev)
        {
            m_MultiPage.NewPage(ev.Graphics);
            float extendedHeight = 0;
            float y = 0;
            y = 0;
            extendedHeight = 0;
            var scanForChildControls = false;
            if (DelegatePrintingReportTitle == null)
                PrintReportTitle(m_f, ParentControlPrinting.BeforeChilds, m_MultiPage, m_xform, y, ref extendedHeight,
                    ref scanForChildControls);
            else
                DelegatePrintingReportTitle(m_f, ParentControlPrinting.BeforeChilds, m_MultiPage, m_xform, y,
                    ref extendedHeight, ref scanForChildControls);
            y += extendedHeight;
            float globalExtendedHeight = 0;
            PrintControls(m_f, m_MultiPage, m_xform, y, ref globalExtendedHeight);
            if (m_MultiPage.LastPage())
            {
                ev.HasMorePages = false;
                m_MultiPage.ResetPage();
            }
            else
            {
                ev.HasMorePages = true;
            }
        }


        public void PrintReportTitle(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x,
            float y, ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;
            var printFont = new Font(c.Font.Name, Convert.ToSingle(c.Font.Size * 1.3), FontStyle.Bold);
            var fontHeight = mp.FontHeight(printFont);
            var pen = new Pen(Color.Black, 2);
            extendedHeight = fontHeight + 3 + pen.Width + 1;
            mp.BeginPrintUnit(y, extendedHeight);
            mp.DrawString(c.Text, printFont, Brushes.Black, x, y, c.Width, fontHeight);
            y += fontHeight + 3;
            mp.DrawLines(pen, x, y, x + c.Size.Width, y);
            mp.EndPrintUnit();
        }


        public void PrintControls(Control c, MultiPageManagement mp, float x, float y, ref float globalExtendedHeight)
        {
            var nbCtrl = c.Controls.Count;
            var yPos = new float[nbCtrl - 1 + 1];
            var controls = new Control[nbCtrl - 1 + 1];
            var i = 0;
            for (i = 0; i <= nbCtrl - 1; i++)
            {
                controls[i] = c.Controls[i];
                yPos[i] = c.Controls[i].Location.Y;
            }

            Array.Sort(yPos, controls);
            globalExtendedHeight = 0;
            var extendedYPos = new ArrayList();

            for (i = 0; i <= nbCtrl - 1; i++)
            {
                float pushDownHeight = 0;
                foreach (Element e in extendedYPos)
                    if (controls[i].Location.Y > e.originalBottom)
                        if (e.totalPushDown > pushDownHeight)
                            pushDownHeight = e.totalPushDown;
                var cp = controls[i].Location.Y + pushDownHeight;
                float extendedHeight = 0;
                PrintControl(controls[i], mp, x + controls[i].Location.X, y + cp, ref extendedHeight);
                if (extendedHeight > 0)
                {
                    var e = new Element();
                    e.originalBottom = controls[i].Location.Y + controls[i].Height;
                    e.printedBottom = cp + controls[i].Height + extendedHeight;
                    extendedYPos.Add(e);
                }
            }

            globalExtendedHeight = 0;
            foreach (Element e in extendedYPos)
                if (e.totalPushDown > globalExtendedHeight)
                    globalExtendedHeight = e.totalPushDown;
        }


        public void PrintControl(Control c, MultiPageManagement mp, float x, float y, ref float extendedHeight)
        {
            extendedHeight = 0;
            if (c.Visible)
            {
                var scanForChildControls = false;
                try
                {
                    PrintOneControl(c, ParentControlPrinting.BeforeChilds, mp, x, y, ref extendedHeight,
                        ref scanForChildControls);
                    if (scanForChildControls)
                    {
                        y += extendedHeight;
                        m_indent += 1;
                        float ChildControlsExtendedHeight = 0;
                        PrintControls(c, mp, x, y, ref ChildControlsExtendedHeight);
                        m_indent -= 1;
                        PrintOneControl(c, ParentControlPrinting.AfterChilds, mp, x, y, ref ChildControlsExtendedHeight,
                            ref scanForChildControls);
                        extendedHeight += ChildControlsExtendedHeight;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error printing control of type " + c.GetType() + Environment.NewLine +
                                    Environment.NewLine + ex);
                }
            }
        }


        public void PrintOneControl(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x,
            float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            if (c.Enabled || !DisabledControlsInGray)
            {
                m_Pen = new Pen(Color.Black);
                m_Brush = Brushes.Black;
            }
            else
            {
                m_Pen = new Pen(Color.Silver);
                m_Brush = Brushes.Silver;
            }

            ScanForChildControls = true;
            var s = c.GetType().ToString();
            m_traceLog.Append(m_indent + " - " + typePrint + " " + s + " (" + c.Text + ")" + Environment.NewLine);
            var founded = false;
            if (!founded)
                foreach (string sType in m_TextBoxLikeControl)
                    if (s.IndexOf(sType) >= 0)
                    {
                        float h = 0;
                        h = mp.FontHeight(new Font(c.Font.Name, c.Font.Size));
                        extendedHeight = mp.BeginPrintUnit(y, h);
                        PrintText(c, mp, x, y, TextBoxBoxed, false, TextBoxBoxed, HorizontalAlignment.Left);
                        mp.EndPrintUnit();
                        founded = true;
                        ScanForChildControls = false;
                    }

            if (!founded)
            {
                var i = m_DelegatesforControls.Count - 1;
                while (i >= 0)
                {
                    var d = (m_DelegateforControls) m_DelegatesforControls[i];
                    if (s.EndsWith(d.typ))
                        d.PrintFunction(c, typePrint, mp, x, y, ref extendedHeight, ref ScanForChildControls);
                    i -= 1;
                }
            }
        }


        public void PrintText(Control c, MultiPageManagement mp, float x, float y, bool tbBoxed, bool inBold,
            bool verticalCentering, HorizontalAlignment hAlignment)
        {
            var yAdjusted = y;
            if (tbBoxed) mp.DrawRectangle(m_Pen, x, y, c.Width, c.Height);
            Font printFont = null;
            if (inBold)
                printFont = new Font(c.Font.Name, c.Font.Size, FontStyle.Bold);
            else
                printFont = new Font(c.Font.Name, c.Font.Size);
            if (verticalCentering)
            {
                var fontHeight = printFont.GetHeight(mp.Graphics());
                var deltaHeight = (c.Height - fontHeight) / 2;
                yAdjusted += deltaHeight;
            }
            else
            {
                yAdjusted += 2;
            }

            mp.DrawString(c.Text, printFont, m_Brush, x, yAdjusted, c.Width, c.Height, BuildFormat(hAlignment));
        }


        public StringFormat BuildFormat(HorizontalAlignment hAlignment)
        {
            var drawFormat = new StringFormat();
            switch (hAlignment)
            {
                case HorizontalAlignment.Left:
                    drawFormat.Alignment = StringAlignment.Near;
                    break;
                case HorizontalAlignment.Center:
                    drawFormat.Alignment = StringAlignment.Center;
                    break;
                case HorizontalAlignment.Right:
                    drawFormat.Alignment = StringAlignment.Far;
                    break;
            }

            return drawFormat;
        }


        public string TrimBlankLines(string s)
        {
            if (s == null) return s;

            var i = s.Length;
            while (i == 1)
            {
                if ((s.Substring(i, 1) != Keys.Enter.ToString()) &&
                    (s.Substring(i, 1) != Keys.LineFeed.ToString()) && (s.Substring(i, 1) != " "))
                    return s.Substring(0, i);
                i -= 1;
            }

            return s;
        }


        public void PrintTextBox(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;
            TextBox tb = null;

            if (c is DBTextBox)
                tb = ((DBTextBox) c).textbox;
            else if (c is TextBox)
                tb = (TextBox) c;
            else
                return;

            var h = mp.FontHeight(new Font(tb.Font.Name, tb.Font.Size));
            if (!tb.Multiline)
            {
                extendedHeight = mp.BeginPrintUnit(y, h);
                PrintText(c, mp, x, y, TextBoxBoxed, tb.Font.Bold, TextBoxBoxed, tb.TextAlign);
                mp.EndPrintUnit();
            }
            else
            {
                var lines = new ArrayList();
                var g = mp.Graphics();
                var areaForOneLineOfText = new SizeF();
                areaForOneLineOfText.Height = h;
                areaForOneLineOfText.Width = tb.Width;
                var charactersFitted = 0;
                var linesFilled = 0;
                var separatorCharPos = 0;
                var printFont = new Font(tb.Font.Name, tb.Font.Size);
                var pos = 0;
                do
                {
                    g.MeasureString(tb.Text.Substring(pos), printFont, areaForOneLineOfText, new StringFormat(),
                        out charactersFitted, out linesFilled);
                    separatorCharPos = charactersFitted;
                    do
                    {
                        separatorCharPos -= 1;
                    } while (separatorCharPos > pos && !char.IsSeparator(tb.Text, pos + separatorCharPos));

                    if (separatorCharPos == pos)
                        separatorCharPos = charactersFitted;
                    else
                        separatorCharPos += 1;
                    lines.Add(tb.Text.Substring(pos, separatorCharPos));
                    pos += separatorCharPos;
                } while (pos < tb.Text.Length && charactersFitted > 0);

                var yItem = y;
                float extraHeight = 0;
                float extraHeightFirstLine = 0;
                var i = 0;
                while (i < lines.Count)
                {
                    extraHeight = mp.BeginPrintUnit(yItem, h);
                    if (i == 0) extraHeightFirstLine = extraHeight;
                    mp.DrawString(Convert.ToString(lines[i]), printFont, m_Brush, x, yItem, tb.Width, h);
                    mp.EndPrintUnit();
                    yItem += h + extraHeight;
                    i += 1;
                }

                if (yItem - y > tb.Height) extendedHeight = yItem - y - tb.Height;
                if (TextBoxBoxed)
                {
                    m_Pen = new Pen(Color.Gray);
                    mp.DrawFrame(m_Pen, x, y + extraHeightFirstLine, tb.Width,
                        tb.Height + extendedHeight - extraHeightFirstLine);
                }
            }
        }


        public void PrintLabel(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;
            HorizontalAlignment ha = 0;
            var ss = c.Text;

            var ha2 = new ContentAlignment();

            if (c is DBLabel)
                ha2 = ((DBLabel) c).TextAlign;
            else
                ha2 = ((Label) c).TextAlign;

            switch (ha2)
            {
                case ContentAlignment.BottomLeft:
                    ha = HorizontalAlignment.Left;
                    break;
                case ContentAlignment.TopLeft:
                    ha = HorizontalAlignment.Left;
                    break;
                case ContentAlignment.MiddleLeft:
                    ha = HorizontalAlignment.Left;
                    break;
                case ContentAlignment.BottomCenter:
                    ha = HorizontalAlignment.Center;
                    break;
                case ContentAlignment.TopCenter:
                    ha = HorizontalAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    ha = HorizontalAlignment.Center;
                    break;
                case ContentAlignment.BottomRight:
                    ha = HorizontalAlignment.Right;
                    break;
                case ContentAlignment.TopRight:
                    ha = HorizontalAlignment.Right;
                    break;
                case ContentAlignment.MiddleRight:
                    ha = HorizontalAlignment.Right;
                    break;
                default:
                    ha = HorizontalAlignment.Left;
                    break;
            }

            var h = mp.FontHeight(new Font(c.Font.Name, c.Font.Size));
            if (c.Height > h) h = c.Height;
            extendedHeight = mp.BeginPrintUnit(y, h);
            PrintText(c, mp, x, y, false, LabelInBold, false, ha);
            mp.EndPrintUnit();
        }


        public void PrintCheckBox(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;

            CheckBox cb = null;
            if (c is DBCheckBox)
                cb = ((DBCheckBox) c).CheckBox;
            else
                cb = (CheckBox) c;

            var h = mp.FontHeight(new Font(cb.Font.Name, cb.Font.Size));
            extendedHeight = mp.BeginPrintUnit(y, h);
            mp.DrawRectangle(m_Pen, x, y, h, h);
            if (cb.Checked)
            {
                float d = 3;
                mp.DrawLines(m_Pen, x + d, y + d, x + h - d, y + h - d);
                PointF[] points2 = {new PointF(x + h - d, y + d), new PointF(x + d, y + h - d)};
                mp.DrawLines(m_Pen, x + h - d, y + d, x + d, y + h - d);
            }

            PrintText(cb, mp, Convert.ToSingle(x + h * 1.4), y - 2, false, false, false, HorizontalAlignment.Left);
            mp.EndPrintUnit();
        }


        public void PrintRadioButton(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x,
            float y, ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;
            var h = mp.FontHeight(new Font(c.Font.Name, c.Font.Size));
            extendedHeight = mp.BeginPrintUnit(y, h);
            mp.DrawEllipse(m_Pen, x, y, h, h);
            if (((RadioButton) c).Checked)
            {
                float d = 3;
                mp.FillEllipse(m_Brush, x + d, y + d, h - d - d, h - d - d);
            }

            PrintText(c, mp, Convert.ToSingle(x + h * 1.4), y - 2, false, false, false, HorizontalAlignment.Left);
            mp.EndPrintUnit();
        }


        public void PrintPanel(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = true;
            if (typePrint == ParentControlPrinting.AfterChilds)
                if (!(((Panel) c).BorderStyle == BorderStyle.None))
                {
                    if (((Panel) c).BorderStyle == BorderStyle.Fixed3D) m_Pen = new Pen(Color.Silver);
                    if (c.Height < 10 && c.Controls.Count == 0)
                    {
                        extendedHeight += mp.BeginPrintUnit(y, 1);
                        mp.DrawLines(m_Pen, x, y, x + c.Width, y);
                        mp.EndPrintUnit();
                    }
                    else
                    {
                        mp.DrawFrame(m_Pen, x, y, c.Width, c.Height + extendedHeight);
                    }
                }
        }


        public void PrintGroupBox(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = true;
            var printFont = new Font(c.Font.Name, c.Font.Size);
            var h = mp.FontHeight(printFont);
            m_Pen = new Pen(Color.Silver);
            switch (typePrint)
            {
                case ParentControlPrinting.BeforeChilds:
                    if (c.Height < 10 && c.Controls.Count == 0)
                    {
                        extendedHeight += mp.BeginPrintUnit(y, 1);
                        mp.DrawLines(m_Pen, x, y, x + c.Width, y);
                        mp.EndPrintUnit();
                    }
                    else
                    {
                        var extraHeight = mp.BeginPrintUnit(y, h);
                        mp.DrawString(c.Text, printFont, Brushes.Black, x + h, y, c.Width - h - h, h);
                        mp.EndPrintUnit();
                        extendedHeight += extraHeight;
                    }

                    break;
                case ParentControlPrinting.AfterChilds:
                    mp.DrawFrame(m_Pen, x, y, c.Width, c.Height + extendedHeight);
                    break;
            }
        }


        public void PrintTabControl(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x,
            float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = true;
            var tc = (TabControl) c;
            m_Pen = new Pen(Color.Gray);
            switch (typePrint)
            {
                case ParentControlPrinting.BeforeChilds:
                    var extraHeight = mp.BeginPrintUnit(y, tc.ItemSize.Height);
                    var tp = tc.SelectedTab;
                    var printFont = new Font(tp.Font.Name, tp.Font.Size, FontStyle.Bold);
                    var h = mp.FontHeight(printFont);
                    if (h > tc.ItemSize.Height) h = tc.ItemSize.Height;
                    mp.DrawString(tp.Text, printFont, Brushes.Black, x, y + (tc.ItemSize.Height - h) / 2, tp.Width, h);
                    mp.DrawLines(m_Pen, x, y + tc.ItemSize.Height, x + tc.Width, y + tc.ItemSize.Height);
                    mp.EndPrintUnit();
                    extendedHeight += extraHeight;
                    break;
                case ParentControlPrinting.AfterChilds:
                    if (TabControlBoxed) mp.DrawFrame(m_Pen, x, y, c.Width, c.Height + extendedHeight);
                    break;
            }
        }


        public void PrintPictureBox(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x,
            float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;
            extendedHeight = mp.BeginPrintUnit(y, c.Height);
            var pic = (PictureBox) c;
            mp.DrawImage(pic.Image, x, y, c.Width, c.Height);
            mp.EndPrintUnit();
        }


        public void PrintSkipControl(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x,
            float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = true;
            extendedHeight = mp.BeginPrintUnit(y, c.Height);
            m_Pen = new Pen(Color.Gray);
            mp.DrawFrame(m_Pen, x, y, c.Width, c.Height);
            mp.EndPrintUnit();
        }

        public void PrintListBox(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;
            var lb = (ListBox) c;
            var yItem = y;
            float extraHeight = 0;
            float extraHeightFirstLine = 0;
            var printFont = new Font(lb.Font.Name, lb.Font.Size, FontStyle.Bold);
            var oldPos = lb.SelectedIndex;
            var i = 0;
            while (i < lb.Items.Count)
            {
                extraHeight = mp.BeginPrintUnit(yItem, lb.ItemHeight);
                if (i == 0) extraHeightFirstLine = extraHeight;
                lb.SelectedIndex = i;
                mp.DrawString(lb.Text, printFont, m_Brush, x, yItem, lb.Width, lb.ItemHeight);
                mp.EndPrintUnit();
                yItem += lb.ItemHeight + extraHeight;
                i += 1;
            }

            lb.SelectedIndex = oldPos;
            if (yItem - y > lb.Height) extendedHeight = yItem - y - lb.Height;
            m_Pen = new Pen(Color.Gray);
            mp.DrawFrame(m_Pen, x, y + extraHeightFirstLine, c.Width, c.Height + extendedHeight - extraHeightFirstLine);
        }


        public void PrintListView(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;
            var lb = (ListView) c;
            var yItem = y;
            float extraHeight = 0;
            float extraHeightFirstLine = 0;
            var headerFont = new Font(lb.Font.Name, lb.Font.Size, FontStyle.Bold);
            var printFont = new Font(lb.Font.Name, lb.Font.Size, FontStyle.Regular);
            var xt = Convert.ToInt32(x);

            var he = 0;
            if (lb.Items.Count > 0)
                he = lb.Items[0].Bounds.Height;


            foreach (ColumnHeader co in lb.Columns)
            {
                extraHeight = mp.BeginPrintUnit(yItem, he);

                mp.DrawString(co.Text, headerFont, m_Brush, xt, yItem, co.Width, he);
                mp.EndPrintUnit();

                xt += co.Width;
            }

            yItem += he + extraHeight;

            var i = 0;

            while (i < lb.Items.Count)
            {
                xt = Convert.ToInt32(x);
                var p = 0;
                foreach (ListViewItem.ListViewSubItem l in lb.Items[i].SubItems)
                {
                    extraHeight = mp.BeginPrintUnit(yItem, l.Bounds.Height);

                    mp.DrawString(l.Text, printFont, m_Brush, xt, yItem, lb.Columns[p].Width, l.Bounds.Height);
                    mp.EndPrintUnit();

                    xt += lb.Columns[p].Width;
                    p += 1;
                }

                yItem += he + extraHeight;
                i += 1;
            }

            if (yItem - y > lb.Height) extendedHeight = yItem - y - lb.Height;
            m_Pen = new Pen(Color.Gray);
            mp.DrawFrame(m_Pen, x, y + extraHeightFirstLine, c.Width, c.Height + extendedHeight - extraHeightFirstLine);
        }


        public void PrintDataGrid(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;

            DataGridView dg = null;
            if (c is DBGridView)
                dg = ((DBGridView) c).dataGridView;
            else
                dg = (DataGridView) c;

            float extraHeight = 0;
            float extraHeightHeaderLine = 0;
            var printFont = new Font(dg.Font.Name, dg.Font.Size, FontStyle.Bold);
            var h = mp.FontHeight(printFont);

            var xPos = x;
            var yPos = y;
            float w = 0;
            extraHeightHeaderLine = mp.BeginPrintUnit(yPos, h + 1);
            var i = 0;
            while (i < dg.Columns.Count)
            {
                var caption = dg.Columns[i].HeaderText;
                w = dg.Columns[i].Width;
                if (xPos + w > x + dg.Width) w = x + dg.Width - xPos;
                if (xPos < x + dg.Width) mp.DrawString(caption, printFont, m_Brush, xPos, yPos, w, h);
                if (i == 0) mp.DrawLines(m_Pen, x, yPos + h, x + dg.Width, yPos + h);
                xPos += w;
                i += 1;
            }

            mp.EndPrintUnit();
            yPos += h + 1 + extraHeightHeaderLine;
            DataTable dt = null;
            if (dg.DataSource is DataTable)
            {
                dt = (DataTable) dg.DataSource;
            }
            else
            {
                if (dg.DataSource is DataSet && (dg.DataMember != null))
                {
                    var ds = (DataSet) dg.DataSource;
                    if (ds.Tables.Contains(dg.DataMember)) dt = ds.Tables[dg.DataMember];
                }
                if (dg.DataSource is DataView)
                {
                    dt = ((DataView)dg.DataSource).Table;
                }
            }

            if (dt != null)
                foreach (DataRow dr in dt.Rows)
                {
                    extraHeight = mp.BeginPrintUnit(yPos, h);
                    xPos = x;
                    i = 0;
                    while (i < dg.Columns.Count)
                    {
                        var caption = dr[i].ToString();
                        w = dg.Columns[i].Width;
                        if (xPos + w > x + dg.Width) w = x + dg.Width - xPos;
                        if (xPos < x + dg.Width) mp.DrawString(caption, printFont, m_Brush, xPos, yPos, w, h);
                        xPos += w;
                        i += 1;
                    }

                    mp.EndPrintUnit();
                    yPos += h + extraHeight;
                }

            if (yPos < y + dg.Height + extraHeightHeaderLine) yPos = y + dg.Height + extraHeightHeaderLine;
            mp.BeginPrintUnit(yPos, 1);
            mp.DrawLines(m_Pen, x, yPos, x + dg.Width, yPos);
            mp.EndPrintUnit();
            if (yPos - y > dg.Height) extendedHeight = yPos - y - dg.Height;
        }

        public void PrintDataGridView(Control c, ParentControlPrinting typePrint, MultiPageManagement mp, float x, float y,
            ref float extendedHeight, ref bool ScanForChildControls)
        {
            ScanForChildControls = false;

            DataGridView dg = null;
            if (c is DBGridView)
                dg = ((DBGridView)c).dataGridView;
            else
                dg = (DataGridView)c;

            float extraHeight = 0;
            float extraHeightHeaderLine = 0;
            var printFont = new Font(dg.Font.Name, dg.Font.Size, FontStyle.Bold);
            var h = mp.FontHeight(printFont);

            var xPos = x;
            var yPos = y;
            float w = 0;
            extraHeightHeaderLine = mp.BeginPrintUnit(yPos, h + 1);
            var i = 0;
            while (i < dg.Columns.Count)
            {
                var caption = dg.Columns[i].HeaderText;
                w = dg.Columns[i].Width;
                if (xPos + w > x + dg.Width) w = x + dg.Width - xPos;
                if (xPos < x + dg.Width) mp.DrawString(caption, printFont, m_Brush, xPos, yPos, w, h);
                if (i == 0) mp.DrawLines(m_Pen, x, yPos + h, x + dg.Width, yPos + h);
                xPos += w;
                i += 1;
            }

            mp.EndPrintUnit();
            yPos += h + 1 + extraHeightHeaderLine;
            DataTable dt = null;
            if (dg.DataSource is DataTable)
            {
                dt = (DataTable)dg.DataSource;
            }
            else
            {
                if (dg.DataSource is DataSet && (dg.DataMember != null))
                {
                    var ds = (DataSet)dg.DataSource;
                    if (ds.Tables.Contains(dg.DataMember)) dt = ds.Tables[dg.DataMember];
                }
                if (dg.DataSource is DataView)
                {
                    dt = ((DataView)dg.DataSource).Table;
                }
            }

            if (dt != null)
                foreach (DataRow dr in dt.Rows)
                {
                    extraHeight = mp.BeginPrintUnit(yPos, h);
                    xPos = x;
                    i = 0;
                    while (i < dg.Columns.Count)
                    {
                        var caption = dr[i].ToString();
                        w = dg.Columns[i].Width;
                        if (xPos + w > x + dg.Width) w = x + dg.Width - xPos;
                        if (xPos < x + dg.Width) mp.DrawString(caption, printFont, m_Brush, xPos, yPos, w, h);
                        xPos += w;
                        i += 1;
                    }

                    mp.EndPrintUnit();
                    yPos += h + extraHeight;
                }

            if (yPos < y + dg.Height + extraHeightHeaderLine) yPos = y + dg.Height + extraHeightHeaderLine;
            mp.BeginPrintUnit(yPos, 1);
            mp.DrawLines(m_Pen, x, yPos, x + dg.Width, yPos);
            mp.EndPrintUnit();
            if (yPos - y > dg.Height) extendedHeight = yPos - y - dg.Height;
        }

        #region Nested type: Element

        private class Element
        {
            public float originalBottom;
            public float printedBottom;

            public float totalPushDown => printedBottom - originalBottom;
        }

        #endregion

        #region Nested type: MultiPageManagement

        public class MultiPageManagement
        {
            private readonly Font m_FontForPageNumering;
            private readonly bool m_pageNumbering;
            private readonly string m_PageNumberingFormat;
            private readonly float m_realPageHeight;
            private readonly float m_realPageLeft;
            private readonly float m_realPageRight;
            private readonly float m_realPageTop;
            private float m_CurrentPageBottom;
            private float m_CurrentPageTop;
            private Graphics m_G;
            private int m_PageNumber;
            private bool m_PageOverflow;
            private bool m_PrintInCurrentPage;
            private bool m_PrintUnit;
            private float m_PrintUnitPullDown;
            private float m_UsablePageHeight;


            public MultiPageManagement(float pageTop, float pageBottom, float pageLeft, float pageRight, Font formFont,
                bool pageNumbering, string pageNumberingFormat)
            {
                m_realPageTop = pageTop;
                m_realPageHeight = pageBottom - pageTop;
                m_realPageLeft = pageLeft;
                m_realPageRight = pageRight;
                m_pageNumbering = pageNumbering;
                if (m_pageNumbering)
                {
                    m_PageNumberingFormat = pageNumberingFormat;
                    m_FontForPageNumering = new Font(formFont.Name, Convert.ToSingle(formFont.Size * 0.8));
                }
            }

            public Graphics Graphics()
            {
                return m_G;
            }

            public bool LastPage()
            {
                return !m_PageOverflow;
            }


            public void NewPage(Graphics g)
            {
                m_G = g;
                m_PageNumber += 1;
                m_UsablePageHeight = m_realPageHeight;
                if (m_pageNumbering)
                {
                    var fontHeightForPageNumbering = FontHeight(m_FontForPageNumering);
                    m_UsablePageHeight -= Convert.ToSingle(fontHeightForPageNumbering * 1.5);
                    var m_recForPageNumbering = new RectangleF();
                    m_recForPageNumbering.X = m_realPageLeft;
                    m_recForPageNumbering.Y = m_realPageTop + m_realPageHeight - fontHeightForPageNumbering;
                    m_recForPageNumbering.Width = m_realPageRight - m_realPageLeft;
                    m_recForPageNumbering.Height = fontHeightForPageNumbering;
                    var drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Far;
                    m_G.DrawString(string.Format(m_PageNumberingFormat, m_PageNumber), m_FontForPageNumering,
                        Brushes.Black, m_recForPageNumbering, drawFormat);
                }

                m_CurrentPageTop = m_UsablePageHeight * (m_PageNumber - 1);
                m_CurrentPageBottom = m_CurrentPageTop + m_UsablePageHeight;
                m_PageOverflow = false;
            }


            public void ResetPage()
            {
                m_PageNumber = 0;
            }


            public float BeginPrintUnit(float y, float neededHeight)
            {
                if (neededHeight > m_UsablePageHeight)
                    throw new ExceptionUtil(
                        Convert.ToString(double.Parse("Needed height cannot exceed 1 page. Page height = ") +
                                         m_UsablePageHeight));
                m_PrintUnit = true;
                float pageBreakPos = 0;
                var printingPos = y;
                pageBreakPos = m_UsablePageHeight;
                pageBreakPos = m_UsablePageHeight;
                while (pageBreakPos < y + neededHeight)
                {
                    if (y <= pageBreakPos && y + neededHeight - 1 > pageBreakPos) printingPos = pageBreakPos;
                    pageBreakPos += m_UsablePageHeight;
                }

                m_PrintUnitPullDown = printingPos - y;
                m_PrintInCurrentPage = false;
                if (printingPos + neededHeight - 1 > m_CurrentPageBottom)
                {
                    m_PageOverflow = true;
                }
                else
                {
                    if (printingPos >= m_CurrentPageTop) m_PrintInCurrentPage = true;
                }

                return m_PrintUnitPullDown;
            }


            private float m_ConvertToPage(float y)
            {
                var newY = y - m_CurrentPageTop + m_realPageTop;
                if (m_PrintUnit)
                {
                    newY += m_PrintUnitPullDown;
                    return newY;
                }

                return newY;
            }


            public void EndPrintUnit()
            {
                m_PrintUnit = false;
            }


            private bool PrintUnitIsInCurrentPage()
            {
                if (!m_PrintUnit) throw new ExceptionUtil("Must be in a print unit to print");
                return m_PrintInCurrentPage;
            }


            public float FontHeight(Font font)
            {
                return font.GetHeight(m_G);
            }


            public void DrawLines(Pen pen, float x1, float y1, float x2, float y2)
            {
                if (PrintUnitIsInCurrentPage())
                {
                    var y1page = m_ConvertToPage(y1);
                    var y2page = m_ConvertToPage(y2);
                    var points = new PointF[2];
                    points[0].X = x1;
                    points[0].Y = y1page;
                    points[1].X = x2;
                    points[1].Y = y2page;
                    m_G.DrawLines(pen, points);
                }
            }


            public void DrawString(string s, Font printFont, Brush brush, float x, float y, float w, float h)
            {
                DrawString(s, printFont, brush, x, y, w, h, new StringFormat());
            }


            public void DrawString(string s, Font printFont, Brush brush, float x, float y, float w, float h,
                StringFormat sf)
            {
                if (PrintUnitIsInCurrentPage())
                {
                    var yPage = m_ConvertToPage(y);
                    var r = new RectangleF();
                    r.X = x;
                    r.Y = yPage;
                    r.Width = w;
                    r.Height = h;
                    m_G.DrawString(s, printFont, brush, r, sf);
                }
            }


            public void DrawRectangle(Pen pen, float x, float y, float w, float h)
            {
                if (PrintUnitIsInCurrentPage())
                {
                    var yPage = m_ConvertToPage(y);
                    m_G.DrawRectangle(pen, x, yPage, w, h);
                }
            }


            public void DrawEllipse(Pen pen, float x, float y, float w, float h)
            {
                if (PrintUnitIsInCurrentPage())
                {
                    var yPage = m_ConvertToPage(y);
                    m_G.DrawEllipse(pen, x, yPage, w, h);
                }
            }


            public void FillEllipse(Brush brush, float x, float y, float w, float h)
            {
                if (PrintUnitIsInCurrentPage())
                {
                    var yPage = m_ConvertToPage(y);
                    m_G.FillEllipse(brush, x, yPage, w, h);
                }
            }


            public void DrawImage(Image image, float x, float y, float w, float h)
            {
                if (image != null)
                    if (PrintUnitIsInCurrentPage())
                    {
                        var yPage = m_ConvertToPage(y);
                        m_G.DrawImage(image, x, yPage, w, h);
                    }
            }


            public void DrawFrame(Pen pen, float x, float y, float w, float h)
            {
                var points = new PointF[2];
                var yTop = m_CurrentPageTop;
                var yBottom = m_CurrentPageBottom;
                if (y + h <= m_CurrentPageTop) return;
                if (y >= m_CurrentPageBottom)
                {
                    m_PageOverflow = true;
                    return;
                }

                points[0].X = x;
                points[1].X = x + w;
                if (y >= m_CurrentPageTop)
                {
                    yTop = y;
                    points[0].Y = m_ConvertToPage(yTop);
                    points[1].Y = m_ConvertToPage(yTop);
                    m_G.DrawLines(pen, points);
                }

                if (y + h <= m_CurrentPageBottom)
                {
                    yBottom = y + h;
                    points[0].Y = m_ConvertToPage(yBottom);
                    points[1].Y = m_ConvertToPage(yBottom);
                    m_G.DrawLines(pen, points);
                }
                else
                {
                    m_PageOverflow = true;
                }

                points[0].Y = m_ConvertToPage(yTop);
                points[1].Y = m_ConvertToPage(yBottom);
                points[0].X = x;
                points[1].X = x;
                m_G.DrawLines(pen, points);
                points[0].X = x + w;
                points[1].X = x + w;
                m_G.DrawLines(pen, points);
            }
        }

        #endregion

        #region Nested type: m_DelegateforControls

        private class m_DelegateforControls
        {
            public ControlPrinting PrintFunction;
            public string typ;
        }

        #endregion
    }
}