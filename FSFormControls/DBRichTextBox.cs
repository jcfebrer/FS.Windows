#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBTextBox.bmp")]
    [DefaultEvent("KeyPress")]
    [ToolboxItem(true)]
    public class DBRichTextBox : DBUserControl
    {
        #region LinePosition enum

        public enum LinePosition : byte
        {
            LinePosition_End = 0,
            LinePosition_Front = 1
        }

        #endregion

        private const int WM_USER = (int) 0X400L;
        private const int EM_FORMATRANGE = WM_USER + 57;
        private const int EM_GETCHARFORMAT = WM_USER + 58;
        private const int EM_SETCHARFORMAT = WM_USER + 68;
        private const int EM_GETPARAFORMAT = 1085;
        private const int EM_SETPARAFORMAT = 1095;

        private const int EM_SETEVENTMASK = 1073;
        private const int WM_SETREDRAW = 11;

        private const short EM_LINEINDEX = 0XBB;
        private const short EM_LINELENGTH = 0XC1;
        private const short EM_LINEFROMCHAR = 0XC9;
        private const short EM_GETLINECOUNT = 0XBA;
        private const short EM_GETFIRSTVISIBLELINE = 0XCE;

        public const int LF_FACESIZE = 32;

        private const long CFM_BOLD = 0X1L;
        private const long CFM_ITALIC = 0X2L;
        private const long CFM_UNDERLINE = 0X4L;
        private const long CFM_STRIKEOUT = 0X8L;
        private const long CFM_PROTECTED = 0X10L;
        private const long CFM_LINK = 0X20L;
        private const long CFM_SIZE = 0X80000000L;
        private const long CFM_COLOR = 0X40000000L;
        private const long CFM_FACE = 0X20000000L;
        private const long CFM_OFFSET = 0X10000000L;
        private const long CFM_CHARSET = 0X8000000L;

        public const long CFE_BOLD = 0X1;
        public const long CFE_ITALIC = 0X2;
        public const long CFE_UNDERLINE = 0X4;
        public const long CFE_STRIKEOUT = 0X8;
        public const long CFE_PROTECTED = 0X10;
        public const long CFE_LINK = 0X20;
        public const long CFE_AUTOCOLOR = 0X40000000;

        public const long CFE_SUPERSCRIPT = 0X20000;

        public const byte CFU_UNDERLINENONE = 0X0;
        public const byte CFU_UNDERLINE = 0X1;

        public const byte CFU_UNDERLINEDOUBLE = 0X3;

        public const byte CFU_UNDERLINEDOTTED = 0X4;
        public const byte CFU_UNDERLINEDASH = 0X5;
        public const byte CFU_UNDERLINEDASHDOT = 0X6;
        public const byte CFU_UNDERLINEDASHDOTDOT = 0X7;
        public const byte CFU_UNDERLINEWAVE = 0X8;
        public const byte CFU_UNDERLINETHICK = 0X9;

        public const int CFM_SMALLCAPS = 0X40;
        public const int CFM_ALLCAPS = 0X80;
        public const int CFM_HIDDEN = 0X100;
        public const int CFM_OUTLINE = 0X200;
        public const int CFM_SHADOW = 0X400;
        public const int CFM_EMBOSS = 0X800;
        public const int CFM_IMPRINT = 0X1000;

        public const int CFM_DISABLED = 0X2000;
        public const int CFM_REVISED = 0X4000;

        public const int CFM_BACKCOLOR = 0X4000000;
        public const int CFM_LCID = 0X2000000;
        public const int CFM_UNDERLINETYPE = 0X800000;

        public const int CFM_WEIGHT = 0X400000;
        public const int CFM_SPACING = 0X200000;
        public const int CFM_KERNING = 0X100000;

        public const int CFM_STYLE = 0X80000;
        public const int CFM_ANIMATION = 0X40000;

        public const int CFM_REVAUTHOR = 0X8000;

        public const short FW_DONTCARE = 0;
        public const short FW_THIN = 100;
        public const short FW_EXTRALIGHT = 200;
        public const short FW_LIGHT = 300;
        public const short FW_NORMAL = 400;
        public const short FW_MEDIUM = 500;
        public const short FW_SEMIBOLD = 600;
        public const short FW_BOLD = 700;
        public const short FW_EXTRABOLD = 800;
        public const short FW_HEAVY = 900;

        public const short FW_ULTRALIGHT = FW_EXTRALIGHT;
        public const short FW_REGULAR = FW_NORMAL;
        public const short FW_DEMIBOLD = FW_SEMIBOLD;
        public const short FW_ULTRABOLD = FW_EXTRABOLD;
        public const short FW_BLACK = FW_HEAVY;

        public const long PFM_STARTINDENT = 0X1;
        public const long PFM_RIGHTINDENT = 0X2;
        public const long PFM_OFFSET = 0X4;
        public const long PFM_ALIGNMENT = 0X8;
        public const long PFM_TABSTOPS = 0X10;
        public const long PFM_NUMBERING = 0X20;
        public const long PFM_OFFSETINDENT = 0X80000000;

        public const int PFN_BULLET = 0X1;

        public const int PFA_LEFT = 0X1;
        public const int PFA_RIGHT = 0X2;
        public const int PFA_CENTER = 0X3;
        private readonly int SCF_ALL = Convert.ToInt32(0X4L);
        private readonly int SCF_SELECTION = Convert.ToInt32(0X1L);
        private int oldEventMask;
        private int SCF_WORD = Convert.ToInt32(0X2L);
        private int updating;

        public Color SelectionBackColor
        {
            set
            {
                if (RichTextBox1.SelectedText == null) return;
                var sb = new StringBuilder();
                var SelText = RichTextBox1.SelectedRtf;
                string strTemp = null;
                var FontTableEnds = 0;
                var ColorTableBegins = 0;
                var ColorTableEnds = 0;
                var StartLooking = 0;
                var HighlightBlockStart = 0;
                var HighlightBlockEnd = 0;
                var cycl = 0;
                var NewColorIndex = 0;
                FontTableEnds = SelText.IndexOf("}}", 0) + 1;
                sb.Append(TextUtil.Substring(SelText, 1, FontTableEnds + 1));
                var transTemp2 = @"{\colortbl";
                ColorTableBegins = SelText.IndexOf(transTemp2, FontTableEnds - 1) + 1;
                if (ColorTableBegins == 0)
                {
                    sb.Append(@"{\colortbl ;");
                    ColorTableEnds = FontTableEnds;
                    NewColorIndex = 1;
                }
                else
                {
                    ColorTableEnds = SelText.IndexOf("}", ColorTableBegins - 1) + 1;
                    ColorTableEnds -= 1;
                    strTemp = TextUtil.Substring(SelText, FontTableEnds + 2,
                        ColorTableEnds - FontTableEnds - 1);
                    for (cycl = 1; cycl <= strTemp.Length; cycl++)
                        if (TextUtil.Substring(strTemp, cycl, 1) == ";")
                            NewColorIndex += 1;
                    sb.Append(strTemp);
                }

                sb.Append(@"\red" + value.R.ToString().Trim());
                sb.Append(@"\green" + value.G.ToString().Trim());
                sb.Append(@"\blue" + value.B.ToString().Trim());
                sb.Append(";");
                sb.Append("}");
                sb.Append(@"\highlight" + NewColorIndex.ToString().Trim());
                strTemp = TextUtil.Substring(SelText, ColorTableEnds + 2,
                    SelText.Length - ColorTableEnds - 1);
                StartLooking = 1;
                do
                {
                    HighlightBlockStart = strTemp.IndexOf(@"\highlight", StartLooking - 1) + 1;
                    if (HighlightBlockStart == 0)
                    {
                        sb.Append(TextUtil.Substring(strTemp, StartLooking, strTemp.Length - StartLooking));
                        break;
                    }

                    HighlightBlockEnd = HighlightBlockStart + 9;
                    var car = TextUtil.Substring(strTemp, HighlightBlockEnd + 1, 1);
                    do
                    {
                        HighlightBlockEnd += 1;
                        if (TextUtil.Substring(strTemp, HighlightBlockEnd + 1, 1) == " ")
                        {
                            HighlightBlockEnd += 1;
                            break;
                        }
                    } while ("0123456789".IndexOf(car, 0) > 0);

                    sb.Append(TextUtil.Substring(strTemp, StartLooking, HighlightBlockStart - StartLooking));
                    StartLooking = HighlightBlockEnd + 1;
                } while (true);

                RichTextBox1.SelectedRtf = sb.ToString();
            }
        }

        public PARA_FORMAT ParaFormat
        {
            get
            {
                var pf = new PARA_FORMAT();
                pf.cbSize = Marshal.SizeOf(pf);

                var lParam = new IntPtr();
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(pf));
                Marshal.StructureToPtr(pf, lParam, false);

                Win32API.SendMessage(RichTextBox1.Handle, EM_GETPARAFORMAT, (IntPtr) SCF_SELECTION, lParam);

                return pf;
            }
            set
            {
                var pf = value;
                pf.cbSize = Marshal.SizeOf(pf);

                var lParam = new IntPtr();
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(pf));
                Marshal.StructureToPtr(pf, lParam, false);

                Win32API.SendMessage(RichTextBox1.Handle, EM_SETPARAFORMAT, (IntPtr) SCF_SELECTION, lParam);
            }
        }

        public CHAR_FORMAT CharFormat
        {
            get
            {
                var cf = new CHAR_FORMAT();
                cf.cbSize = Marshal.SizeOf(cf);

                var lParam = new IntPtr();
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
                Marshal.StructureToPtr(cf, lParam, false);

                Win32API.SendMessage(RichTextBox1.Handle, EM_GETCHARFORMAT, (IntPtr) SCF_SELECTION, lParam);

                return cf;
            }
            set
            {
                var cf = value;
                cf.cbSize = Marshal.SizeOf(cf);

                var lParam = new IntPtr();
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
                Marshal.StructureToPtr(cf, lParam, false);

                Win32API.SendMessage(RichTextBox1.Handle, EM_SETCHARFORMAT, (IntPtr) SCF_SELECTION, lParam);
            }
        }

        public CHAR_FORMAT DefaultCharFormat
        {
            get
            {
                var cf = new CHAR_FORMAT();
                cf.cbSize = Marshal.SizeOf(cf);

                var lParam = new IntPtr();
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
                Marshal.StructureToPtr(cf, lParam, false);

                Win32API.SendMessage(RichTextBox1.Handle, EM_GETCHARFORMAT, (IntPtr) SCF_ALL, lParam);

                return cf;
            }
            set
            {
                var cf = value;
                cf.cbSize = Marshal.SizeOf(cf);

                var lParam = new IntPtr();
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
                Marshal.StructureToPtr(cf, lParam, false);

                Win32API.SendMessage(RichTextBox1.Handle, EM_SETCHARFORMAT, (IntPtr) SCF_ALL, lParam);
            }
        }

        public PARA_FORMAT DefaultParaFormat
        {
            get
            {
                var pf = new PARA_FORMAT();
                pf.cbSize = Marshal.SizeOf(pf);

                var lParam = new IntPtr();
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(pf));
                Marshal.StructureToPtr(pf, lParam, false);

                Win32API.SendMessage(RichTextBox1.Handle, EM_GETPARAFORMAT, (IntPtr) SCF_ALL, lParam);

                return pf;
            }
            set
            {
                var pf = value;
                pf.cbSize = Marshal.SizeOf(pf);

                var lParam = new IntPtr();
                lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(pf));
                Marshal.StructureToPtr(pf, lParam, false);

                Win32API.SendMessage(RichTextBox1.Handle, EM_SETPARAFORMAT, (IntPtr) SCF_ALL, lParam);
            }
        }

        public int FormatRange(bool measureOnly, PrintPageEventArgs e, int charFrom, int charTo)
        {
            var cr = new STRUCT_CHARRANGE();
            cr.cpMin = charFrom;
            cr.cpMax = charTo;

            var rc = new STRUCT_RECT();
            rc.top = HundredthInchToTwips(e.MarginBounds.Top);
            rc.bottom = HundredthInchToTwips(e.MarginBounds.Bottom);
            rc.left = HundredthInchToTwips(e.MarginBounds.Left);
            rc.right = HundredthInchToTwips(e.MarginBounds.Right);

            var rcPage = new STRUCT_RECT();
            rcPage.top = HundredthInchToTwips(e.PageBounds.Top);
            rcPage.bottom = HundredthInchToTwips(e.PageBounds.Bottom);
            rcPage.left = HundredthInchToTwips(e.PageBounds.Left);
            rcPage.right = HundredthInchToTwips(e.PageBounds.Right);

            var hdc = new IntPtr();
            hdc = e.Graphics.GetHdc();

            var fr = new STRUCT_FORMATRANGE();
            fr.chrg = cr;
            fr.hdc = hdc;
            fr.hdcTarget = hdc;
            fr.rc = rc;
            fr.rcPage = rcPage;

            var wParam = 0;
            if (measureOnly)
                wParam = 0;
            else
                wParam = 1;

            var lParam = new IntPtr();
            lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
            Marshal.StructureToPtr(fr, lParam, false);

            var res = 0;
            res = Win32API.SendMessage(RichTextBox1.Handle, EM_FORMATRANGE, (IntPtr) wParam, lParam);

            Marshal.FreeCoTaskMem(lParam);

            e.Graphics.ReleaseHdc(hdc);

            return res;
        }


        private int HundredthInchToTwips(int n)
        {
            return Convert.ToInt32(n * 14.4);
        }


        public void FormatRangeDone()
        {
            var lParam = new IntPtr(0);
            Win32API.SendMessage(RichTextBox1.Handle, EM_FORMATRANGE, new IntPtr(0), lParam);
        }


        public bool SetSelectionFont(string face)
        {
            var cf = new STRUCT_CHARFORMAT();
            cf.cbSize = Marshal.SizeOf(cf);
            cf.dwMask = Convert.ToUInt32(CFM_FACE);

            cf.szFaceName = new char[32];
            face.CopyTo(0, cf.szFaceName, 0, Math.Min(31, face.Length));

            var lParam = new IntPtr();
            lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lParam, false);

            var res = 0;
            res = Win32API.SendMessage(RichTextBox1.Handle, EM_SETCHARFORMAT, (IntPtr) SCF_SELECTION, lParam);
            if (res == 0)
                return true;
            return false;
        }


        public bool SetSelectionSize(int size)
        {
            var cf = new STRUCT_CHARFORMAT();
            cf.cbSize = Marshal.SizeOf(cf);
            cf.dwMask = Convert.ToUInt32(CFM_SIZE);
            cf.yHeight = size * 20;

            var lParam = new IntPtr();
            lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lParam, false);

            var res = 0;
            res = Win32API.SendMessage(RichTextBox1.Handle, EM_SETCHARFORMAT, (IntPtr) SCF_SELECTION, lParam);
            if (res == 0)
                return true;
            return false;
        }


        public bool SetSelectionBold(bool bold)
        {
            if (bold)
                return SetSelectionStyle(Convert.ToInt32(CFM_BOLD), Convert.ToInt32(CFE_BOLD));
            return SetSelectionStyle(Convert.ToInt32(CFM_BOLD), 0);
        }


        public bool SetSelectionItalic(bool italic)
        {
            if (italic)
                return SetSelectionStyle(Convert.ToInt32(CFM_ITALIC), Convert.ToInt32(CFE_ITALIC));
            return SetSelectionStyle(Convert.ToInt32(CFM_ITALIC), 0);
        }


        public bool SetSelectionUnderlined(bool underlined)
        {
            if (underlined)
                return SetSelectionStyle(Convert.ToInt32(CFM_UNDERLINE), Convert.ToInt32(CFE_UNDERLINE));
            return SetSelectionStyle(Convert.ToInt32(CFM_UNDERLINE), 0);
        }


        public bool SetSelectionStrikeOut(bool strike)
        {
            if (strike)
                return SetSelectionStyle(Convert.ToInt32(CFM_STRIKEOUT), Convert.ToInt32(CFE_STRIKEOUT));
            return SetSelectionStyle(Convert.ToInt32(CFM_STRIKEOUT), 0);
        }


        private bool SetSelectionStyle(int mask, int effect)
        {
            var cf = new STRUCT_CHARFORMAT();
            cf.cbSize = Marshal.SizeOf(cf);
            cf.dwMask = Convert.ToUInt32(mask);
            cf.dwEffects = Convert.ToUInt32(effect);

            var lParam = new IntPtr();
            lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lParam, false);

            var res = 0;
            res = Win32API.SendMessage(RichTextBox1.Handle, EM_SETCHARFORMAT, (IntPtr) SCF_SELECTION, lParam);
            if (res == 0)
                return true;
            return false;
        }


        public int get_LineNumber(RichTextBox rtf)
        {
            return Win32API.SendMessage(RichTextBox1.Handle, EM_LINEFROMCHAR, new IntPtr(-1), new IntPtr(0));
        }


        public string get_LineText(RichTextBox rtf)
        {
            try
            {
                return Convert.ToString(rtf.Lines.GetValue(get_LineNumber(rtf)));
            }
            catch
            {
                return string.Empty;
            }
        }


        public int get_LineStartPosition(RichTextBox rtf)
        {
            return Win32API.SendMessage(RichTextBox1.Handle, EM_LINEINDEX, (IntPtr) get_LineNumber(rtf), new IntPtr(0));
        }


        public int get_LineLength(RichTextBox rtf)
        {
            return Win32API.SendMessage(RichTextBox1.Handle, EM_LINELENGTH, (IntPtr) rtf.SelectionStart, new IntPtr(0));
        }


        public int get_LineCount(RichTextBox rtf)
        {
            return Win32API.SendMessage(RichTextBox1.Handle, EM_GETLINECOUNT, new IntPtr(0), new IntPtr(0));
        }


        public int get_FirstVisibleLine(RichTextBox rtf)
        {
            return Win32API.SendMessage(RichTextBox1.Handle, EM_GETFIRSTVISIBLELINE, new IntPtr(0), new IntPtr(0));
        }

        public void GoToLine(RichTextBox rtf, int LineNumber, LinePosition LinePosition)
        {
            var rtf_LineCount = get_LineCount(rtf);
            if ((LineNumber > rtf_LineCount) | (LineNumber < 1))
            {
                MessageBox.Show("Not a valid line number", "Not Valid", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                rtf.SelectionStart = Win32API.SendMessage(RichTextBox1.Handle, EM_LINEINDEX,
                    new IntPtr(LineNumber - 1), new IntPtr(0));
                if (LinePosition == LinePosition.LinePosition_End)
                    rtf.SelectionStart = rtf.SelectionStart + get_LineLength(rtf);
            }

            rtf.Focus();
        }


        public void GoToLine(RichTextBox rtf, int LineNumber)
        {
            GoToLine(rtf, LineNumber, 0);
        }


        public int get_WordCount(RichTextBox rtf)
        {
            var Counter = 0;
            foreach (Match Match in Regex.Matches(rtf.Text, @"\w+")) Counter = Counter + 1;
            return Counter;
        }


        public void FindHighlight(string SearchText, Color HighlightColor, bool MatchCase, bool WholeWords)
        {
            RichTextBox1.SuspendLayout();
            var StartLooking = 0;
            var FoundAt = 0;
            var SearchLength = 0;
            RichTextBoxFinds RTBfinds = 0;
            if (SearchText == null) return;
            if (MatchCase & WholeWords)
                RTBfinds = RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord;
            if (MatchCase & !WholeWords)
                RTBfinds = RichTextBoxFinds.MatchCase;
            if (WholeWords & !MatchCase)
                RTBfinds = RichTextBoxFinds.WholeWord;
            if (!WholeWords & !MatchCase)
                RTBfinds = RichTextBoxFinds.None;

            SearchLength = SearchText.Length;
            do
            {
                FoundAt = RichTextBox1.Find(SearchText, StartLooking, RTBfinds);
                if (FoundAt > -1) SelectionBackColor = HighlightColor;
                StartLooking = StartLooking + SearchLength;
            } while (FoundAt > -1);

            RichTextBox1.ResumeLayout();
        }


        public void BackColorSetWhole(Color BackColorDefault)
        {
            RichTextBox1.SelectAll();
            SelectionBackColor = BackColorDefault;
        }


        private void UnPushColor()
        {
            //ToolBar1.Items[9].Pushed = false;
            //ToolBar1.Items[10].Pushed = false;
            //ToolBar1.Items[11].Pushed = false;
            //ToolBar1.Items[12].Pushed = false;
            //ToolBar1.Items[13].Pushed = false;
            //ToolBar1.Items[14].Pushed = false;
            //ToolBar1.Items[15].Pushed = false;
            //ToolBar1.Items[16].Pushed = false;
        }


        private void UnPushAlign()
        {
            //ToolBar1.Items[18].Pushed = false;
            //ToolBar1.Items[19].Pushed = false;
            //ToolBar1.Items[20].Pushed = false;
        }


        private void ToolBar1_ButtonClick(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            string tag = toolStripMenuItem.Tag.ToString();
            switch (tag)
            {
                case "BOLD":
                    SetSelectionBold(toolStripMenuItem.Pressed);
                    break;
                case "ITALIC":
                    SetSelectionItalic(toolStripMenuItem.Pressed);
                    break;
                case "UNDERLINE":
                    SetSelectionUnderlined(toolStripMenuItem.Pressed);
                    break;
                case "STRIKEOUT":
                    SetSelectionStrikeOut(toolStripMenuItem.Pressed);
                    break;
                case "BLACK":
                    UnPushColor();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionColor = Color.Black;
                    break;
                case "WHITE":
                    UnPushColor();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionColor = Color.White;
                    break;
                case "YELLOW":
                    UnPushColor();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionColor = Color.Yellow;
                    break;
                case "RED":
                    UnPushColor();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionColor = Color.Red;
                    break;
                case "MAGENTA":
                    UnPushColor();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionColor = Color.Magenta;
                    break;
                case "GREEN":
                    UnPushColor();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionColor = Color.Green;
                    break;
                case "CYAN":
                    UnPushColor();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionColor = Color.Cyan;
                    break;
                case "BLUE":
                    UnPushColor();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionColor = Color.Blue;
                    break;
                case "LEFT":
                    UnPushAlign();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    break;
                case "RIGHT":
                    UnPushAlign();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                    break;
                case "CENTER":
                    UnPushAlign();
                    toolStripMenuItem.Checked = true;
                    RichTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                    break;
                case "BULLET":
                    RichTextBox1.SelectionBullet = toolStripMenuItem.Pressed;
                    break;
                case "UNDO":
                    RichTextBox1.Undo();
                    break;
                case "REDO":
                    RichTextBox1.Redo();
                    break;
                case "LOAD":
                    LoadFile();
                    break;
                case "SAVE":
                    SaveFile();
                    break;
                case "SEARCH":
                    var f = new frmFind();
                    f.DBRichTextBox = this;
                    f.ShowDialog();
                    break;
            }
        }


        public void FindAndReplace(string FindText, string ReplaceText)
        {
            RichTextBox1.Find(FindText);
            if (!(RichTextBox1.SelectionLength == 0))
                RichTextBox1.SelectedText = ReplaceText;
            else
                MessageBox.Show("El texto indicado no ha sido encontrado: " + FindText);
        }


        public void Find(string FindText)
        {
            RichTextBox1.Find(FindText);
            if (RichTextBox1.SelectionLength == 0)
                MessageBox.Show("El texto indicado no ha sido encontrado: " + FindText);
        }


        public void Find(string FindText, bool MatchCase, bool WholeWord)
        {
            if (MatchCase)
            {
                if (WholeWord)
                    RichTextBox1.Find(FindText, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                else
                    RichTextBox1.Find(FindText, RichTextBoxFinds.MatchCase);
            }
            else
            {
                if (WholeWord)
                    RichTextBox1.Find(FindText, RichTextBoxFinds.WholeWord);
                else
                    RichTextBox1.Find(FindText);
            }

            if (RichTextBox1.SelectionLength == 0)
                MessageBox.Show("El texto indicado no ha sido encontrado: " + FindText);
        }


        public void FindAndReplace(string FindText, string ReplaceText, bool ReplaceAll, bool MatchCase, bool WholeWord)
        {
            switch (ReplaceAll)
            {
                case false:
                    if (MatchCase)
                    {
                        if (WholeWord)
                            RichTextBox1.Find(FindText, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                        else
                            RichTextBox1.Find(FindText, RichTextBoxFinds.MatchCase);
                    }
                    else
                    {
                        if (WholeWord)
                            RichTextBox1.Find(FindText, RichTextBoxFinds.WholeWord);
                        else
                            RichTextBox1.Find(FindText);
                    }

                    if (!(RichTextBox1.SelectionLength == 0))
                        RichTextBox1.SelectedText = ReplaceText;
                    else
                        MessageBox.Show("El texto indicado no ha sido encontrado: " + FindText);


                    break;
                case true:


                    var i = 0;
                    for (i = 0; i <= RichTextBox1.TextLength; i++)
                    {
                        if (MatchCase)
                        {
                            if (WholeWord)
                                RichTextBox1.Find(FindText, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                            else
                                RichTextBox1.Find(FindText, RichTextBoxFinds.MatchCase);
                        }
                        else
                        {
                            if (WholeWord)
                                RichTextBox1.Find(FindText, RichTextBoxFinds.WholeWord);
                            else
                                RichTextBox1.Find(FindText);
                        }


                        if (!(RichTextBox1.SelectionLength == 0))
                        {
                            RichTextBox1.SelectedText = ReplaceText;
                        }
                        else
                        {
                            MessageBox.Show(i + " veces reemplazado");
                            break;
                        }
                    }

                    break;
            }
        }


        public void SaveFile()
        {
            if ((SaveFileDialog1.ShowDialog() == DialogResult.OK) & (SaveFileDialog1.FileName.Length > 0))
            {
                var strExt = Path.GetExtension(SaveFileDialog1.FileName).ToLower();

                if (strExt == "") strExt = ".rtf";

                if (strExt == ".rtf")
                    RichTextBox1.SaveFile(SaveFileDialog1.FileName);
                else if (strExt == ".txt")
                    RichTextBox1.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                else if ((strExt == ".htm") | (strExt == ".html"))
                    try
                    {
                        var strText = GetHTML(true, true);

                        if (File.Exists(SaveFileDialog1.FileName)) File.Delete(SaveFileDialog1.FileName);

                        var sr = File.CreateText(SaveFileDialog1.FileName);
                        sr.Write(strText);
                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new ExceptionUtil(
                            "Ocurrió un problema al guardar el fichero: " + OpenFileDialog1.FileName, ex);
                    }
            }
        }


        public void LoadFile()
        {
            try
            {
                if ((OpenFileDialog1.ShowDialog() == DialogResult.OK) & (OpenFileDialog1.FileName.Length > 0))
                {
                    var strExt = Path.GetExtension(OpenFileDialog1.FileName).ToLower();

                    if (strExt == ".rtf")
                    {
                        RichTextBox1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    else if (strExt == ".txt")
                    {
                        RichTextBox1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    }
                    else if ((strExt == ".htm") | (strExt == ".html"))
                    {
                        var sr = File.OpenText(OpenFileDialog1.FileName);
                        var strHTML = sr.ReadToEnd();
                        sr.Close();

                        AddHTML(strHTML);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("Ocurrió un problema al cargar el fichero: " + OpenFileDialog1.FileName, ex);
            }
        }


        public void UncheckFonts()
        {
            MenuItem1.Checked = false;
            MenuItem2.Checked = false;
            MenuItem3.Checked = false;
            MenuItem4.Checked = false;
            MenuItem19.Checked = false;
            MenuItem20.Checked = false;
        }


        public void UncheckSize()
        {
            MenuItem5.Checked = false;
            MenuItem6.Checked = false;
            MenuItem7.Checked = false;
            MenuItem8.Checked = false;
            MenuItem9.Checked = false;
            MenuItem10.Checked = false;
            MenuItem11.Checked = false;
            MenuItem12.Checked = false;
            MenuItem13.Checked = false;
            MenuItem14.Checked = false;
            MenuItem15.Checked = false;
            MenuItem16.Checked = false;
            MenuItem17.Checked = false;
            MenuItem18.Checked = false;
        }


        private void MenuItem1_Click(object sender, EventArgs e)
        {
            SetSelectionFont("Arial");
            UncheckFonts();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem4_Click(object sender, EventArgs e)
        {
            SetSelectionFont("Courier New");
            UncheckFonts();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem2_Click(object sender, EventArgs e)
        {
            SetSelectionFont("Times New Roman");
            UncheckFonts();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem3_Click(object sender, EventArgs e)
        {
            SetSelectionFont("Microsoft Sans Serif");
            UncheckFonts();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem5_Click(object sender, EventArgs e)
        {
            SetSelectionSize(8);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem6_Click(object sender, EventArgs e)
        {
            SetSelectionSize(10);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem7_Click(object sender, EventArgs e)
        {
            SetSelectionSize(12);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem8_Click(object sender, EventArgs e)
        {
            SetSelectionSize(14);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem9_Click(object sender, EventArgs e)
        {
            SetSelectionSize(16);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem10_Click(object sender, EventArgs e)
        {
            SetSelectionSize(18);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem11_Click(object sender, EventArgs e)
        {
            SetSelectionSize(20);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem12_Click(object sender, EventArgs e)
        {
            SetSelectionSize(22);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem13_Click(object sender, EventArgs e)
        {
            SetSelectionSize(24);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem14_Click(object sender, EventArgs e)
        {
            SetSelectionSize(26);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem15_Click(object sender, EventArgs e)
        {
            SetSelectionSize(28);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem16_Click(object sender, EventArgs e)
        {
            SetSelectionSize(36);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem17_Click(object sender, EventArgs e)
        {
            SetSelectionSize(48);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem18_Click(object sender, EventArgs e)
        {
            SetSelectionSize(72);
            UncheckSize();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem19_Click(object sender, EventArgs e)
        {
            SetSelectionFont("Tahoma");
            UncheckFonts();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void MenuItem20_Click(object sender, EventArgs e)
        {
            SetSelectionFont("Verdana");
            UncheckFonts();
            ((ToolStripMenuItem) sender).Checked = true;
        }


        private void RichTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateToolbar();
        }


        private void UpdateToolbar()
        {
            if (!(RichTextBox1.SelectionFont == null))
            {
                //ToolBar1.Items[0].Pushed = RichTextBox1.SelectionFont.Bold;
                //ToolBar1.Items[1].Pushed = RichTextBox1.SelectionFont.Italic;
                //ToolBar1.Items[2].Pushed = RichTextBox1.SelectionFont.Underline;
                //ToolBar1.Items[3].Pushed = RichTextBox1.SelectionFont.Strikeout;

                UnPushColor();
                // falta
                //switch ( RichTextBox1.SelectionColor.ToArgb() ) 
                //{
                //    case Color.Black.ToArgb():
                //        this.ToolBar1.Buttons[ 9 ].Pushed = true; 
                //        break;
                //    case Color.White.ToArgb():
                //        this.ToolBar1.Buttons[ 10 ].Pushed = true; 
                //        break;
                //    case Color.Yellow.ToArgb():
                //        this.ToolBar1.Buttons[ 11 ].Pushed = true; 
                //        break;
                //    case Color.Red.ToArgb():
                //        this.ToolBar1.Buttons[ 12 ].Pushed = true; 
                //        break;
                //    case Color.Magenta.ToArgb():
                //        this.ToolBar1.Buttons[ 13 ].Pushed = true; 
                //        break;
                //    case Color.Green.ToArgb():
                //        this.ToolBar1.Buttons[ 14 ].Pushed = true; 
                //        break;
                //    case Color.Cyan.ToArgb():
                //        this.ToolBar1.Buttons[ 15 ].Pushed = true; 
                //        break;
                //    case Color.Blue.ToArgb():
                //        this.ToolBar1.Buttons[ 16 ].Pushed = true; 
                //        break;
                //}


                UnPushAlign();
                //switch (RichTextBox1.SelectionAlignment)
                //{
                //    case HorizontalAlignment.Left:
                //        ToolBar1.Items[18].Pushed = true;
                //        break;
                //    case HorizontalAlignment.Center:
                //        ToolBar1.Items[19].Pushed = true;
                //        break;
                //    case HorizontalAlignment.Right:
                //        ToolBar1.Items[20].Pushed = true;
                //        break;
                //}


                UncheckFonts();
                switch (RichTextBox1.SelectionFont.Name)
                {
                    case "Arial":
                        MenuItem1.Checked = true;
                        break;
                    case "Time New Roman":
                        MenuItem2.Checked = true;
                        break;
                    case "Sans Serif":
                        MenuItem3.Checked = true;
                        break;
                    case "Courier New":
                        MenuItem4.Checked = true;
                        break;
                    case "Tahoma":
                        MenuItem19.Checked = true;
                        break;
                    case "Verdana":
                        MenuItem20.Checked = true;
                        break;
                }


                UncheckSize();
                switch (RichTextBox1.SelectionFont.Size.ToString())
                {
                    case "8":
                        MenuItem5.Checked = true;
                        break;
                    case "10":
                        MenuItem6.Checked = true;
                        break;
                    case "12":
                        MenuItem7.Checked = true;
                        break;
                    case "14":
                        MenuItem8.Checked = true;
                        break;
                    case "16":
                        MenuItem9.Checked = true;
                        break;
                    case "18":
                        MenuItem10.Checked = true;
                        break;
                    case "20":
                        MenuItem11.Checked = true;
                        break;
                    case "22":
                        MenuItem12.Checked = true;
                        break;
                    case "24":
                        MenuItem13.Checked = true;
                        break;
                    case "26":
                        MenuItem14.Checked = true;
                        break;
                    case "28":
                        MenuItem15.Checked = true;
                        break;
                    case "36":
                        MenuItem16.Checked = true;
                        break;
                    case "48":
                        MenuItem17.Checked = true;
                        break;
                    case "72":
                        MenuItem18.Checked = true;
                        break;
                }
            }
        }


        public void BeginUpdate()
        {
            updating = updating + 1;

            if (updating > 1) return;

            var lParam = new IntPtr(0);

            oldEventMask = Win32API.SendMessage(RichTextBox1.Handle, EM_SETEVENTMASK, new IntPtr(0), lParam);


            Win32API.SendMessage(RichTextBox1.Handle, WM_SETREDRAW, new IntPtr(0), lParam);
        }


        public void EndUpdate()
        {
            updating = updating - 1;

            if (updating > 0) return;

            var lParam = new IntPtr(0);
            Win32API.SendMessage(RichTextBox1.Handle, WM_SETREDRAW, new IntPtr(1), lParam);

            var lParam2 = new IntPtr(oldEventMask);
            Win32API.SendMessage(RichTextBox1.Handle, EM_SETEVENTMASK, new IntPtr(0), lParam2);
        }


        private Color GetColor(int crColor)
        {
            var r = Convert.ToByte(crColor);
            var g = Convert.ToByte(crColor > 8);
            var b = Convert.ToByte(crColor > 16);

            return Color.FromArgb(r, g, b);
        }


        private int GetCOLORREF(int r, int g, int b)
        {
            var r2 = r;
            var g2 = Convert.ToInt32(g < 8);
            var b2 = Convert.ToInt32(b < 16);

            var result = r2 | g2 | b2;

            return result;
        }


        private int GetCOLORREF(Color color)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;

            return GetCOLORREF(r, g, b);
        }


        public string GetHTML(bool bHTML, bool bParaFormat)
        {
            var cf = new CHAR_FORMAT();
            var pf = new PARA_FORMAT();

            var bold = ctformatStates.nctNone;
            var bitalic = ctformatStates.nctNone;
            var bstrikeout = ctformatStates.nctNone;
            var bunderline = ctformatStates.nctNone;

            var bacenter = ctformatStates.nctNone;
            var baleft = ctformatStates.nctNone;
            var baright = ctformatStates.nctNone;
            var bnumbering = ctformatStates.nctNone;

            var strFont = "";
            var crFont = 0;
            var color = new Color();
            var yHeight = 0;
            var i = 0;

            var colFormat = new ArrayList();

            var mfr = new cMyREFormat();

            var nStart = 0;
            var nEnd = 0;
            var strHTML = "";

            RichTextBox1.HideSelection = true;
            BeginUpdate();

            nStart = RichTextBox1.SelectionStart;
            nEnd = RichTextBox1.SelectionLength;

            try
            {
                if (bHTML)
                {
                    char[] ch =
                    {
                        char.Parse("&"), char.Parse("<"), char.Parse(">"), char.Parse(@""""), char.Parse(@"\")
                    };

                    string[] strreplace = {"&amp;", "&lt;", "&gt;", "&quot;", "&apos;"};


                    for (i = 0; i <= ch.Length - 1; i += i + 1)
                    {
                        var ch2 = ch[i];


                        var n = RichTextBox1.Find(Convert.ToString(ch2), 0);
                        while (n != -1)
                        {
                            mfr = new cMyREFormat();

                            mfr.nPos = n;
                            mfr.nLen = 1;
                            mfr.nType = uMyREType.U_MYRE_TYPE_ENTITY;
                            mfr.strValue = strreplace[i];

                            colFormat.Add(mfr);

                            n = RichTextBox1.Find(Convert.ToString(ch2), (RichTextBoxFinds) (n + 1));
                        }
                    }
                }

                var strT = "";

                var k = RichTextBox1.TextLength;
                char[] chtrim = {char.Parse(" "), char.Parse(@"\x0000")};


                for (i = 0; i <= k - 1; i += i + 1)
                {
                    RichTextBox1.Select(i, 1);
                    var strChar = RichTextBox1.SelectedText;

                    if (bHTML)
                    {
                        cf = CharFormat;
                        pf = ParaFormat;

                        var strfname = new string(cf.szFaceName);
                        strfname = strfname.Trim(chtrim);


                        if ((strFont != strfname) | (crFont != cf.crTextColor) | (yHeight != cf.yHeight))
                        {
                            if (strFont != "")
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "</font>";

                                colFormat.Add(mfr);
                            }

                            strFont = strfname;
                            crFont = Convert.ToInt32(cf.crTextColor);
                            yHeight = Convert.ToInt32(cf.yHeight);

                            var fsize = Convert.ToInt32(yHeight / (20 * 5));

                            color = GetColor(crFont);

                            mfr = new cMyREFormat();

                            var strcolor = string.Concat("#", Convert.ToString(color.ToArgb()) + 0XFFFFFF);

                            mfr.nPos = i;
                            mfr.nLen = 0;
                            mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                            mfr.strValue =
                                Convert.ToString(
                                    double.Parse("<font face='" + strFont + "' color='" + strcolor + "' size='") +
                                    fsize +
                                    double.Parse("'") > double.Parse(""));

                            colFormat.Add(mfr);
                        }

                        if ((strChar == @"\r") | (strChar == @"\n"))
                        {
                            if (bParaFormat)
                            {
                                bnumbering = ctformatStates.nctNone;
                                baleft = ctformatStates.nctNone;
                                baright = ctformatStates.nctNone;
                                bacenter = ctformatStates.nctNone;
                            }


                            if (bitalic != ctformatStates.nctNone)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "</i>";

                                colFormat.Add(mfr);

                                bitalic = ctformatStates.nctNone;
                            }

                            if (bold != ctformatStates.nctNone)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "</b>";

                                colFormat.Add(mfr);

                                bold = ctformatStates.nctNone;
                            }

                            if (bunderline != ctformatStates.nctNone)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "</u>";

                                colFormat.Add(mfr);

                                bunderline = ctformatStates.nctNone;
                            }

                            if (bstrikeout != ctformatStates.nctNone)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "</s>";

                                colFormat.Add(mfr);

                                bstrikeout = ctformatStates.nctNone;
                            }
                        }

                        if (bParaFormat)
                        {
                            if (pf.wAlignment == PFA_CENTER)
                            {
                                if (bacenter == ctformatStates.nctNone)
                                    bacenter = ctformatStates.nctNew;
                                else
                                    bacenter = ctformatStates.nctContinue;
                            }
                            else
                            {
                                if (bacenter != ctformatStates.nctNone) bacenter = ctformatStates.nctReset;
                            }

                            if (bacenter == ctformatStates.nctNew)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "<p align='center'>";

                                colFormat.Add(mfr);
                            }
                            else if (bacenter == ctformatStates.nctReset)
                            {
                                bacenter = ctformatStates.nctNone;
                            }

                            if (pf.wAlignment == PFA_LEFT)
                            {
                                if (baleft == ctformatStates.nctNone)
                                    baleft = ctformatStates.nctNew;
                                else
                                    baleft = ctformatStates.nctContinue;
                            }
                            else
                            {
                                if (baleft != ctformatStates.nctNone) baleft = ctformatStates.nctReset;

                                if (baleft == ctformatStates.nctNew)
                                {
                                    mfr = new cMyREFormat();

                                    mfr.nPos = i;
                                    mfr.nLen = 0;
                                    mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                    mfr.strValue = "<p align='left'>";

                                    colFormat.Add(mfr);
                                }
                                else if (baleft == ctformatStates.nctReset)
                                {
                                    baleft = ctformatStates.nctNone;
                                }
                            }

                            if (pf.wAlignment == PFA_RIGHT)
                            {
                                if (baright == ctformatStates.nctNone)
                                    baright = ctformatStates.nctNew;
                                else
                                    baright = ctformatStates.nctContinue;
                            }
                            else
                            {
                                if (baright != ctformatStates.nctNone) baright = ctformatStates.nctReset;

                                if (baright == ctformatStates.nctNew)
                                {
                                    mfr = new cMyREFormat();

                                    mfr.nPos = i;
                                    mfr.nLen = 0;
                                    mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                    mfr.strValue = "<p align='right'>";

                                    colFormat.Add(mfr);
                                }
                                else if (baright == ctformatStates.nctReset)
                                {
                                    baright = ctformatStates.nctNone;
                                }
                            }

                            if (pf.wNumbering == PFN_BULLET)
                            {
                                if (bnumbering == ctformatStates.nctNone)
                                    bnumbering = ctformatStates.nctNew;
                                else
                                    bnumbering = ctformatStates.nctContinue;
                            }
                            else
                            {
                                if (bnumbering != ctformatStates.nctNone) bnumbering = ctformatStates.nctReset;

                                if (bnumbering == ctformatStates.nctNew)
                                {
                                    mfr = new cMyREFormat();

                                    mfr.nPos = i;
                                    mfr.nLen = 0;
                                    mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                    mfr.strValue = "<li>";

                                    colFormat.Add(mfr);
                                }
                                else if (bnumbering == ctformatStates.nctReset)
                                {
                                    bnumbering = ctformatStates.nctNone;
                                }
                            }
                        }

                        if (double.Parse(Convert.ToString(cf.dwEffects) + CFE_BOLD) == CFE_BOLD)
                        {
                            if (bold == ctformatStates.nctNone)
                                bold = ctformatStates.nctNew;
                            else
                                bold = ctformatStates.nctContinue;
                        }
                        else
                        {
                            if (bold != ctformatStates.nctNone) bold = ctformatStates.nctReset;

                            if (bold == ctformatStates.nctNew)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "<b>";

                                colFormat.Add(mfr);
                            }
                            else if (bold == ctformatStates.nctReset)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "</b>";

                                colFormat.Add(mfr);

                                bold = ctformatStates.nctNone;
                            }
                        }

                        if (double.Parse(Convert.ToString(cf.dwEffects) + CFE_ITALIC) == CFE_ITALIC)
                        {
                            if (bitalic == ctformatStates.nctNone)
                                bitalic = ctformatStates.nctNew;
                            else
                                bitalic = ctformatStates.nctContinue;
                        }
                        else
                        {
                            if (bitalic != ctformatStates.nctNone) bitalic = ctformatStates.nctReset;

                            if (bitalic == ctformatStates.nctNew)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "<i>";

                                colFormat.Add(mfr);
                            }
                            else if (bitalic == ctformatStates.nctReset)
                            {
                                mfr = new cMyREFormat();

                                mfr.nPos = i;
                                mfr.nLen = 0;
                                mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                mfr.strValue = "</i>";

                                colFormat.Add(mfr);

                                bitalic = ctformatStates.nctNone;
                            }

                            if (double.Parse(Convert.ToString(cf.dwEffects) + CFM_STRIKEOUT) == CFM_STRIKEOUT)
                            {
                                if (bstrikeout == ctformatStates.nctNone)
                                    bstrikeout = ctformatStates.nctNew;
                                else
                                    bstrikeout = ctformatStates.nctContinue;
                            }
                            else
                            {
                                if (bstrikeout != ctformatStates.nctNone) bstrikeout = ctformatStates.nctReset;

                                if (bstrikeout == ctformatStates.nctNew)
                                {
                                    mfr = new cMyREFormat();

                                    mfr.nPos = i;
                                    mfr.nLen = 0;
                                    mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                    mfr.strValue = "<s>";

                                    colFormat.Add(mfr);
                                }
                                else if (bstrikeout == ctformatStates.nctReset)
                                {
                                    mfr = new cMyREFormat();

                                    mfr.nPos = i;
                                    mfr.nLen = 0;
                                    mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                    mfr.strValue = "</s>";

                                    colFormat.Add(mfr);

                                    bstrikeout = ctformatStates.nctNone;
                                }
                            }

                            if (double.Parse(Convert.ToString(cf.dwEffects) + CFE_UNDERLINE) == CFE_UNDERLINE)
                            {
                                if (bunderline == ctformatStates.nctNone)
                                    bunderline = ctformatStates.nctNew;
                                else
                                    bunderline = ctformatStates.nctContinue;
                            }
                            else
                            {
                                if (bunderline != ctformatStates.nctNone) bunderline = ctformatStates.nctReset;

                                if (bunderline == ctformatStates.nctNew)
                                {
                                    mfr = new cMyREFormat();

                                    mfr.nPos = i;
                                    mfr.nLen = 0;
                                    mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                    mfr.strValue = "<u>";

                                    colFormat.Add(mfr);
                                }
                                else if (bunderline == ctformatStates.nctReset)
                                {
                                    mfr = new cMyREFormat();

                                    mfr.nPos = i;
                                    mfr.nLen = 0;
                                    mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                                    mfr.strValue = "</u>";

                                    colFormat.Add(mfr);

                                    bunderline = ctformatStates.nctNone;
                                }
                            }
                        }
                    }

                    strT += strChar;
                }

                if (bHTML)
                {
                    if (bold != ctformatStates.nctNone)
                    {
                        mfr = new cMyREFormat();

                        mfr.nPos = i;
                        mfr.nLen = 0;
                        mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                        mfr.strValue = "</b>";

                        colFormat.Add(mfr);
                    }

                    if (bitalic != ctformatStates.nctNone)
                    {
                        mfr = new cMyREFormat();

                        mfr.nPos = i;
                        mfr.nLen = 0;
                        mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                        mfr.strValue = "</i>";

                        colFormat.Add(mfr);
                    }

                    if (bstrikeout != ctformatStates.nctNone)
                    {
                        mfr = new cMyREFormat();

                        mfr.nPos = i;
                        mfr.nLen = 0;
                        mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                        mfr.strValue = "</s>";

                        colFormat.Add(mfr);
                    }

                    if (bunderline != ctformatStates.nctNone)
                    {
                        mfr = new cMyREFormat();

                        mfr.nPos = i;
                        mfr.nLen = 0;
                        mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                        mfr.strValue = "</u>";

                        colFormat.Add(mfr);
                    }

                    if (strFont != "")
                    {
                        mfr = new cMyREFormat();

                        mfr.nPos = i;
                        mfr.nLen = 0;
                        mfr.nType = uMyREType.U_MYRE_TYPE_TAG;
                        mfr.strValue = "</font>";

                        colFormat.Add(mfr);
                    }
                }

                k = colFormat.Count;
                for (i = 0; i <= k - 1 - 1; i += i + 1)
                {
                    var j = 0;
                    for (j = i + 1; j <= k - 1; j += j + 1)
                    {
                        mfr = (cMyREFormat) colFormat[i];
                        var mfr2 = (cMyREFormat) colFormat[j];

                        if (mfr2.nPos < mfr.nPos)
                        {
                            colFormat.RemoveAt(j);
                            colFormat.Insert(i, mfr2);
                            j = j - 1;
                        }
                        else if ((mfr2.nPos == mfr.nPos) & (mfr2.nLen < mfr.nLen))
                        {
                            colFormat.RemoveAt(j);
                            colFormat.Insert(i, mfr2);
                            j = j - 1;
                        }
                    }
                }


                var nAcum = 0;
                for (i = 0; i <= k - 1; i += i + 1)
                {
                    mfr = (cMyREFormat) colFormat[i];

                    strHTML += strT.Substring(nAcum, mfr.nPos - nAcum) + mfr.strValue;
                    nAcum = mfr.nPos + mfr.nLen;
                }

                if (nAcum < strT.Length) strHTML += strT.Substring(nAcum);
            }
            catch
            {
            }
            finally
            {
                RichTextBox1.SelectionStart = nStart;
                RichTextBox1.SelectionLength = nEnd;

                EndUpdate();
                RichTextBox1.HideSelection = false;
            }

            return strHTML;
        }


        public void AddHTML(string strHTML)
        {
            var cf = new CHAR_FORMAT();
            var pf = new PARA_FORMAT();

            cf = DefaultCharFormat;
            pf = DefaultParaFormat;

            char[] chtrim = {char.Parse(" "), Convert.ToChar(0X0)};


            RichTextBox1.HideSelection = true;
            BeginUpdate();

            try
            {
                while (strHTML.Length > 0)
                {
                    var strData = strHTML;

                    reinit:


                    var nStart = strHTML.IndexOf('<');
                    if (nStart >= 0)
                    {
                        if (nStart > 0)
                        {
                            strData = strHTML.Substring(0, nStart);
                            strHTML = strHTML.Substring(nStart);
                        }
                        else
                        {
                            var nEnd = strHTML.IndexOf('>', nStart);
                            if (nEnd > nStart)
                            {
                                if (nEnd - nStart > 0)
                                {
                                    var strTag = strHTML.Substring(nStart, nEnd - nStart + 1);
                                    strTag = strTag.ToLower();

                                    if (strTag == "<b>")
                                    {
                                        cf.dwMask = cf.dwMask | CFM_WEIGHT | CFM_BOLD;
                                        cf.dwEffects = cf.dwEffects | CFE_BOLD;
                                        cf.wWeight = FW_BOLD;
                                    }
                                    else if (strTag == "<i>")
                                    {
                                        cf.dwMask = cf.dwMask | CFM_ITALIC;
                                        cf.dwEffects = cf.dwEffects | CFE_ITALIC;
                                    }
                                    else if (strTag == "<u>")
                                    {
                                        cf.dwMask = cf.dwMask | CFM_UNDERLINE | CFM_UNDERLINETYPE;
                                        cf.dwEffects = cf.dwEffects | CFE_UNDERLINE;
                                        cf.bUnderlineType = CFU_UNDERLINE;
                                    }
                                    else if (strTag == "<s>")
                                    {
                                        cf.dwMask = cf.dwMask | CFM_STRIKEOUT;
                                        cf.dwEffects = cf.dwEffects | CFE_STRIKEOUT;
                                    }
                                    else if ((strTag.Length > 2) & (strTag.Substring(0, 2) == "<p"))
                                    {
                                        if (strTag.IndexOf("align='left'") > 0)
                                        {
                                            pf.dwMask = pf.dwMask | PFM_ALIGNMENT;
                                            pf.wAlignment = Convert.ToInt16(PFA_LEFT);
                                        }
                                        else if (strTag.IndexOf("align='right'") > 0)
                                        {
                                            pf.dwMask = pf.dwMask | PFM_ALIGNMENT;
                                            pf.wAlignment = Convert.ToInt16(PFA_RIGHT);
                                        }
                                        else if (strTag.IndexOf("align='center'") > 0)
                                        {
                                            pf.dwMask = pf.dwMask | PFM_ALIGNMENT;
                                            pf.wAlignment = Convert.ToInt16(PFA_CENTER);
                                        }
                                    }
                                    else if ((strTag.Length > 5) & (strTag.Substring(0, 5) == "<font"))
                                    {
                                        var strFont = new string(cf.szFaceName);
                                        strFont = strFont.Trim(chtrim);
                                        var crFont = Convert.ToInt32(cf.crTextColor);
                                        var yHeight = Convert.ToInt32(cf.yHeight);

                                        var nFace = strTag.IndexOf("face=");
                                        if (nFace > 0)
                                        {
                                            var nFaceEnd = strTag.IndexOf(@"""", nFace + 6);
                                            if (nFaceEnd > nFace)
                                                strFont = strTag.Substring(nFace + 6, nFaceEnd - nFace - 6);
                                        }

                                        var nSize = strTag.IndexOf("size=");
                                        if (nSize > 0)
                                        {
                                            var nSizeEnd = strTag.IndexOf(@"""", nSize + 6);
                                            if (nSizeEnd > nSize)
                                            {
                                                yHeight = int.Parse(strTag.Substring(nSize + 6, nSizeEnd - nSize - 6));
                                                yHeight = yHeight * 20 * 5;
                                            }
                                        }

                                        var nColor = strTag.IndexOf("color=");
                                        if (nColor > 0)
                                        {
                                            var nColorEnd = strTag.IndexOf(@"""", nColor + 7);
                                            if (nColorEnd > nColor)
                                            {
                                                if (strTag.Substring(nColor + 7, 1) == "#")
                                                {
                                                    var strCr = strTag.Substring(nColor + 8, nColorEnd - nColor - 8);
                                                    var nCr = Convert.ToInt32(strCr, 16);

                                                    var color = Color.FromArgb(nCr);

                                                    crFont = GetCOLORREF(color);
                                                }
                                                else
                                                {
                                                    crFont =
                                                        int.Parse(strTag.Substring(nColor + 7, nColorEnd - nColor - 7));
                                                }
                                            }
                                        }

                                        cf.szFaceName = new char[LF_FACESIZE + 1];
                                        strFont.CopyTo(0, cf.szFaceName, 0, Math.Min(LF_FACESIZE - 1, strFont.Length));
                                        cf.crTextColor = crFont;
                                        cf.yHeight = yHeight;

                                        cf.dwMask = cf.dwMask | CFM_COLOR | CFM_SIZE | CFM_FACE;
                                        cf.dwEffects = cf.dwEffects &
                                                       Convert.ToInt64(!Convert.ToBoolean(CFE_AUTOCOLOR));
                                    }
                                    else if (strTag == "<li>")
                                    {
                                        if (pf.wNumbering != PFN_BULLET)
                                        {
                                            pf.dwMask = pf.dwMask | PFM_NUMBERING;
                                            pf.wNumbering = Convert.ToInt16(PFN_BULLET);
                                        }
                                    }
                                    else if (strTag == "</b>")
                                    {
                                        cf.dwEffects = cf.dwEffects & Convert.ToInt64(!Convert.ToBoolean(CFE_BOLD));
                                        cf.wWeight = FW_NORMAL;
                                    }
                                    else if (strTag == "</i>")
                                    {
                                        cf.dwEffects = cf.dwEffects & Convert.ToInt64(!Convert.ToBoolean(CFE_ITALIC));
                                    }
                                    else if (strTag == "</u>")
                                    {
                                        cf.dwEffects = cf.dwEffects &
                                                       Convert.ToInt64(!Convert.ToBoolean(CFE_UNDERLINE));
                                    }
                                    else if (strTag == "</s>")
                                    {
                                        cf.dwEffects = cf.dwEffects &
                                                       Convert.ToInt64(!Convert.ToBoolean(CFM_STRIKEOUT));
                                    }
                                    else if (strTag == "</font>")
                                    {
                                    }
                                    else if (strTag == "</p>")
                                    {
                                    }
                                    else if (strTag == "</li>")
                                    {
                                    }

                                    var nStart2 = strHTML.IndexOf("<", nEnd + 1);
                                    if (nStart2 > 0)
                                    {
                                        strData = strHTML.Substring(nEnd + 1, nStart2 - nEnd - 1);
                                        strHTML = strHTML.Substring(nStart2);
                                    }
                                    else
                                    {
                                        if (nEnd + 1 < strHTML.Length)
                                            strData = strHTML.Substring(nEnd + 1);
                                        else
                                            strData = "";

                                        strHTML = "";
                                    }


                                    if (strData.Length > 0)
                                    {
                                        var transTemp12 = 1;
                                        if (strData.Substring(0, transTemp12) == "<") goto reinit;
                                    }
                                }
                                else
                                {
                                    strHTML = "";
                                }
                            }
                            else
                            {
                                strHTML = "";
                            }
                        }
                    }
                    else
                    {
                        strHTML = "";
                    }

                    if (strData.Length > 0)
                    {
                        strData = strData.Replace("&amp;", "&");
                        strData = strData.Replace("&lt;", "<");
                        strData = strData.Replace("&gt;", ">");
                        strData = strData.Replace("&apos;", "'");
                        strData = strData.Replace("&quot;", Convert.ToString(@""""));

                        var strAux = strData;

                        while (strAux.Length > 0)
                        {
                            var nLen = strAux.Length;

                            var nStartCache = RichTextBox1.SelectionStart;
                            var strt = strAux.Substring(0, nLen);

                            RichTextBox1.SelectedText = strt;
                            strAux = strAux.Remove(0, nLen);

                            RichTextBox1.SelectionStart = nStartCache;
                            RichTextBox1.SelectionLength = strt.Length;

                            ParaFormat = pf;
                            CharFormat = cf;


                            RichTextBox1.SelectionStart = RichTextBox1.TextLength + 1;
                            RichTextBox1.SelectionLength = 0;
                        }

                        RichTextBox1.SelectionStart = RichTextBox1.TextLength + 1;
                        RichTextBox1.SelectionLength = 0;

                        if ((strData.IndexOf("\r\n", 0) >= 0) | (strData.IndexOf(Global.Cr, 0) >= 0))
                        {
                            pf.dwMask = PFM_ALIGNMENT | PFM_NUMBERING;
                            pf.wAlignment = Convert.ToInt16(PFA_LEFT);
                            pf.wNumbering = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RichTextBox1.SelectionStart = RichTextBox1.TextLength + 1;
                RichTextBox1.SelectionLength = 0;

                EndUpdate();
                RichTextBox1.HideSelection = false;
            }
        }

        #region Nested type: CHAR_FORMAT

        [StructLayout(LayoutKind.Sequential)]
        public struct CHAR_FORMAT
        {
            public int cbSize;
            public long dwMask;
            public long dwEffects;
            public long yHeight;
            public long yOffset;
            public long crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szFaceName;

            public short wWeight;
            public short sSpacing;
            public long crBackColor;
            public long lcid;
            public long dwReserved;
            public short sStyle;
            public short wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
            public byte bReserved1;
        }

        #endregion

        #region Nested type: PARA_FORMAT

        [StructLayout(LayoutKind.Sequential)]
        public struct PARA_FORMAT
        {
            public int cbSize;
            public long dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;

            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }

        #endregion

        #region Nested type: STRUCT_CHARFORMAT

        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_CHARFORMAT
        {
            public int cbSize;
            public uint dwMask;
            public uint dwEffects;
            public int yHeight;
            public readonly int yOffset;
            public readonly int crTextColor;
            public readonly byte bCharSet;
            public readonly byte bPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szFaceName;
        }

        #endregion

        #region Nested type: STRUCT_CHARRANGE

        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_CHARRANGE
        {
            public int cpMin;
            public int cpMax;
        }

        #endregion

        #region Nested type: STRUCT_FORMATRANGE

        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public STRUCT_RECT rc;
            public STRUCT_RECT rcPage;
            public STRUCT_CHARRANGE chrg;
        }

        #endregion

        #region Nested type: STRUCT_RECT

        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        #endregion

        #region Nested type: cMyREFormat

        private struct cMyREFormat
        {
            public int nLen;
            public int nPos;
            public uMyREType nType;
            public string strValue;
        }

        #endregion

        #region Nested type: ctformatStates

        private enum ctformatStates
        {
            nctNone = 0,
            nctNew = 1,
            nctContinue = 2,
            nctReset = 3
        }

        #endregion

        #region Nested type: uMyREType

        private enum uMyREType
        {
            U_MYRE_TYPE_TAG,
            U_MYRE_TYPE_EMO,
            U_MYRE_TYPE_ENTITY
        }

        #endregion

        #region '" Windows Form Designer generated code "' 

        internal ContextMenuStrip ContextMenu1;
        internal ContextMenuStrip ContextMenu2;
        internal ImageList ImageList1;
        internal ToolStripMenuItem MenuItem1;
        internal ToolStripMenuItem MenuItem10;
        internal ToolStripMenuItem MenuItem11;
        internal ToolStripMenuItem MenuItem12;
        internal ToolStripMenuItem MenuItem13;
        internal ToolStripMenuItem MenuItem14;
        internal ToolStripMenuItem MenuItem15;
        internal ToolStripMenuItem MenuItem16;
        internal ToolStripMenuItem MenuItem17;
        internal ToolStripMenuItem MenuItem18;
        internal ToolStripMenuItem MenuItem19;
        internal ToolStripMenuItem MenuItem2;
        internal ToolStripMenuItem MenuItem20;
        internal ToolStripMenuItem MenuItem3;
        internal ToolStripMenuItem MenuItem4;
        internal ToolStripMenuItem MenuItem5;
        internal ToolStripMenuItem MenuItem6;
        internal ToolStripMenuItem MenuItem7;
        internal ToolStripMenuItem MenuItem8;
        internal ToolStripMenuItem MenuItem9;
        internal OpenFileDialog OpenFileDialog1;
        internal RichTextBox RichTextBox1;
        internal SaveFileDialog SaveFileDialog1;
        internal ToolStrip ToolBar1;
        internal ToolStripButton ToolBarButton1;
        internal ToolStripButton ToolBarButton10;
        internal ToolStripButton ToolBarButton11;
        internal ToolStripButton ToolBarButton12;
        internal ToolStripButton ToolBarButton13;
        internal ToolStripButton ToolBarButton14;
        internal ToolStripButton ToolBarButton15;
        internal ToolStripButton ToolBarButton16;
        internal ToolStripButton ToolBarButton17;
        internal ToolStripButton ToolBarButton18;
        internal ToolStripButton ToolBarButton19;
        internal ToolStripButton ToolBarButton2;
        internal ToolStripButton ToolBarButton20;
        internal ToolStripButton ToolBarButton21;
        internal ToolStripButton ToolBarButton22;
        internal ToolStripButton ToolBarButton23;
        internal ToolStripButton ToolBarButton24;
        internal ToolStripButton ToolBarButton25;
        internal ToolStripButton ToolBarButton26;
        internal ToolStripButton ToolBarButton27;
        internal ToolStripButton ToolBarButton28;
        internal ToolStripButton ToolBarButton29;
        internal ToolStripButton ToolBarButton3;
        internal ToolStripButton ToolBarButton30;
        internal ToolStripButton ToolBarButton4;
        internal ToolStripButton ToolBarButton5;
        internal ToolStripButton ToolBarButton6;
        internal ToolStripButton ToolBarButton7;
        internal ToolStripButton ToolBarButton8;
        internal ToolStripButton ToolBarButton9;
        private IContainer components;

        public DBRichTextBox()
        {
            InitializeComponent();


            ToolBar1.ItemClicked += ToolBar1_ButtonClick;
            MenuItem1.Click += MenuItem1_Click;
            MenuItem4.Click += MenuItem4_Click;
            MenuItem2.Click += MenuItem2_Click;
            MenuItem3.Click += MenuItem3_Click;
            MenuItem5.Click += MenuItem5_Click;
            MenuItem6.Click += MenuItem6_Click;
            MenuItem7.Click += MenuItem7_Click;
            MenuItem8.Click += MenuItem8_Click;
            MenuItem9.Click += MenuItem9_Click;
            MenuItem10.Click += MenuItem10_Click;
            MenuItem11.Click += MenuItem11_Click;
            MenuItem12.Click += MenuItem12_Click;
            MenuItem13.Click += MenuItem13_Click;
            MenuItem14.Click += MenuItem14_Click;
            MenuItem15.Click += MenuItem15_Click;
            MenuItem16.Click += MenuItem16_Click;
            MenuItem17.Click += MenuItem17_Click;
            MenuItem18.Click += MenuItem18_Click;
            MenuItem19.Click += MenuItem19_Click;
            MenuItem20.Click += MenuItem20_Click;
            RichTextBox1.SelectionChanged += RichTextBox1_SelectionChanged;
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
            var resources = new ComponentResourceManager(typeof(DBRichTextBox));
            RichTextBox1 = new RichTextBox();
            ToolBar1 = new ToolStrip();
            ToolBarButton1 = new ToolStripButton();
            ToolBarButton2 = new ToolStripButton();
            ToolBarButton3 = new ToolStripButton();
            ToolBarButton6 = new ToolStripButton();
            ToolBarButton5 = new ToolStripButton();
            ToolBarButton4 = new ToolStripButton();
            ContextMenu1 = new ContextMenuStrip();
            MenuItem1 = new ToolStripMenuItem();
            MenuItem2 = new ToolStripMenuItem();
            MenuItem3 = new ToolStripMenuItem();
            MenuItem4 = new ToolStripMenuItem();
            MenuItem19 = new ToolStripMenuItem();
            MenuItem20 = new ToolStripMenuItem();
            ToolBarButton7 = new ToolStripButton();
            ToolBarButton8 = new ToolStripButton();
            ContextMenu2 = new ContextMenuStrip();
            MenuItem5 = new ToolStripMenuItem();
            MenuItem6 = new ToolStripMenuItem();
            MenuItem7 = new ToolStripMenuItem();
            MenuItem8 = new ToolStripMenuItem();
            MenuItem9 = new ToolStripMenuItem();
            MenuItem10 = new ToolStripMenuItem();
            MenuItem11 = new ToolStripMenuItem();
            MenuItem12 = new ToolStripMenuItem();
            MenuItem13 = new ToolStripMenuItem();
            MenuItem14 = new ToolStripMenuItem();
            MenuItem15 = new ToolStripMenuItem();
            MenuItem16 = new ToolStripMenuItem();
            MenuItem17 = new ToolStripMenuItem();
            MenuItem18 = new ToolStripMenuItem();
            ToolBarButton9 = new ToolStripButton();
            ToolBarButton10 = new ToolStripButton();
            ToolBarButton11 = new ToolStripButton();
            ToolBarButton12 = new ToolStripButton();
            ToolBarButton13 = new ToolStripButton();
            ToolBarButton14 = new ToolStripButton();
            ToolBarButton15 = new ToolStripButton();
            ToolBarButton16 = new ToolStripButton();
            ToolBarButton17 = new ToolStripButton();
            ToolBarButton18 = new ToolStripButton();
            ToolBarButton19 = new ToolStripButton();
            ToolBarButton20 = new ToolStripButton();
            ToolBarButton22 = new ToolStripButton();
            ToolBarButton21 = new ToolStripButton();
            ToolBarButton23 = new ToolStripButton();
            ToolBarButton24 = new ToolStripButton();
            ToolBarButton25 = new ToolStripButton();
            ToolBarButton26 = new ToolStripButton();
            ToolBarButton27 = new ToolStripButton();
            ToolBarButton28 = new ToolStripButton();
            ToolBarButton29 = new ToolStripButton();
            ToolBarButton30 = new ToolStripButton();
            ImageList1 = new ImageList(components);
            OpenFileDialog1 = new OpenFileDialog();
            SaveFileDialog1 = new SaveFileDialog();
            SuspendLayout();
            // 
            // RichTextBox1
            // 
            RichTextBox1.Dock = DockStyle.Fill;
            RichTextBox1.Location = new Point(0, 72);
            RichTextBox1.Name = "RichTextBox1";
            RichTextBox1.Size = new Size(433, 192);
            RichTextBox1.TabIndex = 0;
            RichTextBox1.Text = "DBRichTextBox1";
            // 
            // ToolBar1
            // 
            ToolBar1.Items.AddRange(new ToolStripButton[]
            {
                ToolBarButton1,
                ToolBarButton2,
                ToolBarButton3,
                ToolBarButton6,
                ToolBarButton5,
                ToolBarButton4,
                ToolBarButton7,
                ToolBarButton8,
                ToolBarButton9,
                ToolBarButton10,
                ToolBarButton11,
                ToolBarButton12,
                ToolBarButton13,
                ToolBarButton14,
                ToolBarButton15,
                ToolBarButton16,
                ToolBarButton17,
                ToolBarButton18,
                ToolBarButton19,
                ToolBarButton20,
                ToolBarButton22,
                ToolBarButton21,
                ToolBarButton23,
                ToolBarButton24,
                ToolBarButton25,
                ToolBarButton26,
                ToolBarButton27,
                ToolBarButton28,
                ToolBarButton29,
                ToolBarButton30
            });
            //ToolBar1.DropDownArrows = true;
            ToolBar1.ImageList = ImageList1;
            ToolBar1.Location = new Point(0, 0);
            ToolBar1.Name = "ToolBar1";
            ToolBar1.ShowItemToolTips = true;
            ToolBar1.Size = new Size(433, 72);
            ToolBar1.TabIndex = 1;
            // 
            // ToolBarButton1
            // 
            ToolBarButton1.ImageIndex = 0;
            ToolBarButton1.Name = "ToolBarButton1";
            //ToolBarButton1.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton1.Tag = "BOLD";
            // 
            // ToolBarButton2
            // 
            ToolBarButton2.ImageIndex = 2;
            ToolBarButton2.Name = "ToolBarButton2";
            //ToolBarButton2.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton2.Tag = "ITALIC";
            // 
            // ToolBarButton3
            // 
            ToolBarButton3.ImageIndex = 1;
            ToolBarButton3.Name = "ToolBarButton3";
            //ToolBarButton3.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton3.Tag = "UNDERLINE";
            // 
            // ToolBarButton6
            // 
            ToolBarButton6.ImageIndex = 4;
            ToolBarButton6.Name = "ToolBarButton6";
            //ToolBarButton6.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton6.Tag = "STRIKEOUT";
            // 
            // ToolBarButton5
            // 
            ToolBarButton5.Name = "ToolBarButton5";
            //ToolBarButton5.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton4
            // 
            //ToolBarButton4.DropDownMenu = ContextMenu1;
            ToolBarButton4.ImageIndex = 3;
            ToolBarButton4.Name = "ToolBarButton4";
            //ToolBarButton4.Style = ToolBarButtonStyle.DropDownButton;
            // 
            // ContextMenu1
            // 
            ContextMenu1.Items.AddRange(new ToolStripMenuItem[]
            {
                MenuItem1,
                MenuItem2,
                MenuItem3,
                MenuItem4,
                MenuItem19,
                MenuItem20
            });
            // 
            // MenuItem1
            // 
            MenuItem1.Checked = true;
            //MenuItem1.Index = 0;
            MenuItem1.Text = "Arial";
            // 
            // MenuItem2
            // 
            //MenuItem2.Index = 1;
            MenuItem2.Text = "Times New Roman";
            // 
            // MenuItem3
            // 
            //MenuItem3.Index = 2;
            MenuItem3.Text = "Sans Serif";
            // 
            // MenuItem4
            // 
            //MenuItem4.Index = 3;
            MenuItem4.Text = "Courier New";
            // 
            // MenuItem19
            // 
            //MenuItem19.Index = 4;
            MenuItem19.Text = "Tahoma";
            // 
            // MenuItem20
            // 
            //MenuItem20.Index = 5;
            MenuItem20.Text = "Verdana";
            // 
            // ToolBarButton7
            // 
            ToolBarButton7.Name = "ToolBarButton7";
            //ToolBarButton7.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton8
            // 
            //ToolBarButton8.DropDownMenu = ContextMenu2;
            ToolBarButton8.ImageIndex = 5;
            ToolBarButton8.Name = "ToolBarButton8";
            //ToolBarButton8.Style = ToolBarButtonStyle.DropDownButton;
            // 
            // ContextMenu2
            // 
            ContextMenu2.Items.AddRange(new ToolStripMenuItem[]
            {
                MenuItem5,
                MenuItem6,
                MenuItem7,
                MenuItem8,
                MenuItem9,
                MenuItem10,
                MenuItem11,
                MenuItem12,
                MenuItem13,
                MenuItem14,
                MenuItem15,
                MenuItem16,
                MenuItem17,
                MenuItem18
            });
            // 
            // MenuItem5
            // 
            MenuItem5.Checked = true;
            //MenuItem5.Index = 0;
            MenuItem5.Text = "8";
            // 
            // MenuItem6
            // 
            //MenuItem6.Index = 1;
            MenuItem6.Text = "10";
            // 
            // MenuItem7
            // 
            //MenuItem7.Index = 2;
            MenuItem7.Text = "12";
            // 
            // MenuItem8
            // 
            //MenuItem8.Index = 3;
            MenuItem8.Text = "14";
            // 
            // MenuItem9
            // 
            //MenuItem9.Index = 4;
            MenuItem9.Text = "16";
            // 
            // MenuItem10
            // 
            //MenuItem10.Index = 5;
            MenuItem10.Text = "18";
            // 
            // MenuItem11
            // 
            //MenuItem11.Index = 6;
            MenuItem11.Text = "20";
            // 
            // MenuItem12
            // 
            //MenuItem12.Index = 7;
            MenuItem12.Text = "22";
            // 
            // MenuItem13
            // 
            //MenuItem13.Index = 8;
            MenuItem13.Text = "24";
            // 
            // MenuItem14
            // 
            //MenuItem14.Index = 9;
            MenuItem14.Text = "26";
            // 
            // MenuItem15
            // 
            //MenuItem15.Index = 10;
            MenuItem15.Text = "28";
            // 
            // MenuItem16
            // 
            //MenuItem16.Index = 11;
            MenuItem16.Text = "36";
            // 
            // MenuItem17
            // 
            //MenuItem17.Index = 12;
            MenuItem17.Text = "48";
            // 
            // MenuItem18
            // 
            //MenuItem18.Index = 13;
            MenuItem18.Text = "72";
            // 
            // ToolBarButton9
            // 
            ToolBarButton9.Name = "ToolBarButton9";
            //ToolBarButton9.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton10
            // 
            ToolBarButton10.ImageIndex = 13;
            ToolBarButton10.Name = "ToolBarButton10";
            //ToolBarButton10.Pushed = true;
            //ToolBarButton10.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton10.Tag = "BLACK";
            // 
            // ToolBarButton11
            // 
            ToolBarButton11.ImageIndex = 12;
            ToolBarButton11.Name = "ToolBarButton11";
            //ToolBarButton11.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton11.Tag = "WHITE";
            // 
            // ToolBarButton12
            // 
            ToolBarButton12.ImageIndex = 6;
            ToolBarButton12.Name = "ToolBarButton12";
            //ToolBarButton12.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton12.Tag = "YELLOW";
            // 
            // ToolBarButton13
            // 
            ToolBarButton13.ImageIndex = 7;
            ToolBarButton13.Name = "ToolBarButton13";
            //ToolBarButton13.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton13.Tag = "RED";
            // 
            // ToolBarButton14
            // 
            ToolBarButton14.ImageIndex = 8;
            ToolBarButton14.Name = "ToolBarButton14";
            //ToolBarButton14.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton14.Tag = "MAGENTA";
            // 
            // ToolBarButton15
            // 
            ToolBarButton15.ImageIndex = 9;
            ToolBarButton15.Name = "ToolBarButton15";
            //ToolBarButton15.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton15.Tag = "GREEN";
            // 
            // ToolBarButton16
            // 
            ToolBarButton16.ImageIndex = 10;
            ToolBarButton16.Name = "ToolBarButton16";
            //ToolBarButton16.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton16.Tag = "CYAN";
            // 
            // ToolBarButton17
            // 
            ToolBarButton17.ImageIndex = 11;
            ToolBarButton17.Name = "ToolBarButton17";
            //ToolBarButton17.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton17.Tag = "BLUE";
            // 
            // ToolBarButton18
            // 
            ToolBarButton18.Name = "ToolBarButton18";
            //ToolBarButton18.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton19
            // 
            ToolBarButton19.ImageIndex = 14;
            ToolBarButton19.Name = "ToolBarButton19";
            //ToolBarButton19.Pushed = true;
            //ToolBarButton19.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton19.Tag = "LEFT";
            // 
            // ToolBarButton20
            // 
            ToolBarButton20.ImageIndex = 16;
            ToolBarButton20.Name = "ToolBarButton20";
            //ToolBarButton20.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton20.Tag = "CENTER";
            // 
            // ToolBarButton22
            // 
            ToolBarButton22.ImageIndex = 17;
            ToolBarButton22.Name = "ToolBarButton22";
            //ToolBarButton22.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton22.Tag = "RIGHT";
            // 
            // ToolBarButton21
            // 
            ToolBarButton21.Name = "ToolBarButton21";
            //ToolBarButton21.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton23
            // 
            ToolBarButton23.ImageIndex = 18;
            ToolBarButton23.Name = "ToolBarButton23";
            //ToolBarButton23.Style = ToolBarButtonStyle.ToggleButton;
            ToolBarButton23.Tag = "BULLET";
            // 
            // ToolBarButton24
            // 
            ToolBarButton24.Name = "ToolBarButton24";
            //ToolBarButton24.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton25
            // 
            ToolBarButton25.ImageIndex = 19;
            ToolBarButton25.Name = "ToolBarButton25";
            ToolBarButton25.Tag = "UNDO";
            // 
            // ToolBarButton26
            // 
            ToolBarButton26.ImageIndex = 20;
            ToolBarButton26.Name = "ToolBarButton26";
            ToolBarButton26.Tag = "REDO";
            // 
            // ToolBarButton27
            // 
            ToolBarButton27.Name = "ToolBarButton27";
            //ToolBarButton27.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton28
            // 
            //ToolBarButton28.Index = 22;
            ToolBarButton28.Name = "ToolBarButton28";
            ToolBarButton28.Tag = "SAVE";
            ToolBarButton28.ToolTipText = "Guardar";
            // 
            // ToolBarButton29
            // 
            //ToolBarButton29.Index = 23;
            ToolBarButton29.Name = "ToolBarButton29";
            ToolBarButton29.Tag = "LOAD";
            ToolBarButton29.ToolTipText = "Cargar";
            // 
            // ToolBarButton30
            // 
            //ToolBarButton30.Index = 21;
            ToolBarButton30.Name = "ToolBarButton30";
            ToolBarButton30.Tag = "SEARCH";
            ToolBarButton30.ToolTipText = "B?scar y reemplazar";
            // 
            // ImageList1
            // 
            ImageList1.ImageStream = (ImageListStreamer) resources.GetObject("ImageList1.ImageStream");
            ImageList1.TransparentColor = Color.Transparent;
            ImageList1.Images.SetKeyName(0, "");
            ImageList1.Images.SetKeyName(1, "");
            ImageList1.Images.SetKeyName(2, "");
            ImageList1.Images.SetKeyName(3, "");
            ImageList1.Images.SetKeyName(4, "");
            ImageList1.Images.SetKeyName(5, "");
            ImageList1.Images.SetKeyName(6, "");
            ImageList1.Images.SetKeyName(7, "");
            ImageList1.Images.SetKeyName(8, "");
            ImageList1.Images.SetKeyName(9, "");
            ImageList1.Images.SetKeyName(10, "");
            ImageList1.Images.SetKeyName(11, "");
            ImageList1.Images.SetKeyName(12, "");
            ImageList1.Images.SetKeyName(13, "");
            ImageList1.Images.SetKeyName(14, "");
            ImageList1.Images.SetKeyName(15, "");
            ImageList1.Images.SetKeyName(16, "");
            ImageList1.Images.SetKeyName(17, "");
            ImageList1.Images.SetKeyName(18, "");
            ImageList1.Images.SetKeyName(19, "");
            ImageList1.Images.SetKeyName(20, "");
            ImageList1.Images.SetKeyName(21, "Buscar.bmp");
            ImageList1.Images.SetKeyName(22, "xpGuardar.bmp");
            ImageList1.Images.SetKeyName(23, "folderop.bmp");
            // 
            // DBRichTextBox
            // 
            Controls.Add(RichTextBox1);
            Controls.Add(ToolBar1);
            Name = "DBRichTextBox";
            Size = new Size(433, 264);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}