using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FSLibrary;

namespace FSMouseKeyboardLibrary
{

    /// <summary>
    /// Standard Keyboard Shortcuts used by most applications
    /// </summary>
    public enum StandardShortcut
    {
        Copy,
        Cut,
        Paste,
        SelectAll,
        Save,
        Open,
        New,
        Close,
        Print
    }

    /// <summary>
    /// Simulate keyboard key presses
    /// </summary>
    public static class KeyboardSimulator
    {
        #region Methods

        public static void KeyDown(Keys key)
        {
            Win32API.keybd_event(ParseKey(key), 0, 0, 0);
        }

        /// <summary>
        /// Envia las teclas definidas en la cadena 'text' como eventos de pulsación.
        /// Corresponden a un teclado español.
        /// </summary>
        /// <param name="text"></param>
        public static void SendText(string text)
        {
            text = text.Replace("{shift}", "{s");
            text = text.Replace("{control}", "{c");
            text = text.Replace("{ctrl}", "{c");
            text = text.Replace("{ctr}", "{c");
            text = text.Replace("{alt}", "{a");
            text = text.Replace("{win}", "{w");
            text = text.Replace("{windows}", "{w");

            text = text.Replace("{f1}", "^1");
            text = text.Replace("{f2}", "^2");
            text = text.Replace("{f3}", "^3");
            text = text.Replace("{f4}", "^4");
            text = text.Replace("{f5}", "^5");
            text = text.Replace("{f6}", "^6");
            text = text.Replace("{f7}", "^7");
            text = text.Replace("{f8}", "^8");
            text = text.Replace("{f9}", "^9");
            text = text.Replace("{f10}", "^0");
            text = text.Replace("{f11}", "^o");
            text = text.Replace("{f12}", "^p");
            text = text.Replace("{enter}", "^e");
            text = text.Replace("{esc}", "^x");
            text = text.Replace("{sup}", "^s");
            text = text.Replace("{del}", "^s");
            text = text.Replace("{back}", "^b");
            text = text.Replace("{ins}", "^i");
            text = text.Replace("{avpag}", "^a");
            text = text.Replace("{repag}", "^g");
            text = text.Replace("{end}", "^n");
            text = text.Replace("{home}", "^h");

            text = text.Replace("{up}", "^u");
            text = text.Replace("{left}", "^l");
            text = text.Replace("{right}", "^r");
            text = text.Replace("{down}", "^d");
            text = text.Replace("{tab}", "^t");

            for (int f = 0; f< text.Length; f++)
            {
                char ch = text.Substring(f, 1).ToCharArray()[0];
                char nextCh = '#';
                if (f < text.Length - 1)
                    nextCh = text.Substring(f + 1, 1).ToCharArray()[0];

                bool shift = Char.IsUpper(ch);
                Keys k = Keys.None;

                switch (Char.ToUpper(ch))
                {
                    case '{':
                        switch (nextCh)
                        {
                            case 'c':
                                KeyDown(Keys.Control);
                                break;
                            case 'a':
                                KeyDown(Keys.Alt);
                                break;
                            case 's':
                                KeyDown(Keys.Shift);
                                break;
                            case 'w':
                                KeyDown(Keys.LWin);
                                break;
                        }
                        f++;
                        break;
                    case '^':
                        switch (nextCh)
                        {        
                            case 'l':
                                k = Keys.Left;
                                break;
                            case 'r':
                                k = Keys.Right;
                                break;
                            case 'u':
                                k = Keys.Up;
                                break;
                            case 'd':
                                k = Keys.Down;
                                break;
                            case 'e':
                                k = Keys.Enter;
                                break;
                            case 'b':
                                k = Keys.Back;
                                break;
                            case 'x':
                                k = Keys.Escape;
                                break;
                            case 's':
                                k = Keys.Delete;
                                break;
                            case 'i':
                                k = Keys.Insert;
                                break;
                            case 'a':
                                k = Keys.PageDown;
                                break;
                            case 'g':
                                k = Keys.PageUp;
                                break;
                            case 'n':
                                k = Keys.End;
                                break;
                            case 'h':
                                k = Keys.Home;
                                break;
                            case 't':
                                k = Keys.Tab;
                                break;
                            case '1':
                                k = Keys.F1;
                                break;
                            case '2':
                                k = Keys.F2;
                                break;
                            case '3':
                                k = Keys.F3;
                                break;
                            case '4':
                                k = Keys.F4;
                                break;
                            case '5':
                                k = Keys.F5;
                                break;
                            case '6':
                                k = Keys.F6;
                                break;
                            case '7':
                                k = Keys.F7;
                                break;
                            case '8':
                                k = Keys.F8;
                                break;
                            case '9':
                                k = Keys.F9;
                                break;
                            case '0':
                                k = Keys.F10;
                                break;
                            case 'o':
                                k = Keys.F11;
                                break;
                            case 'p':
                                k = Keys.F12;
                                break;
                        }
                        f++;
                        break;
                    case 'Á':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.A;
                        break;
                    case 'É':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.E;
                        break;
                    case 'Í':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.I;
                        break;
                    case 'Ó':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.O;
                        break;
                    case 'Ú':
                        KeyPress(Keys.OemQuotes);
                        k = Keys.U;
                        break;
                    case ' ':
                        k = Keys.Space;
                        break;
                    case '?':
                        shift = true;
                        k = Keys.OemOpenBrackets;
                        break;
                    case '¿':
                        shift = true;
                        k = Keys.Oem6;
                        break;
                    case '.':
                        k = Keys.OemPeriod;
                        break;
                    case '!':
                        shift = true;
                        k = Keys.D1;
                        break;
                    case '"':
                        shift = true;
                        k = Keys.D2;
                        break;
                    case '·':
                        shift = true;
                        k = Keys.D3;
                        break;
                    case '$':
                        shift = true;
                        k = Keys.D4;
                        break;
                    case '%':
                        shift = true;
                        k = Keys.D5;
                        break;
                    case '&':
                        shift = true;
                        k = Keys.D6;
                        break;
                    case '/':
                        shift = true;
                        k = Keys.D7;
                        break;
                    case '(':
                        shift = true;
                        k = Keys.D8;
                        break;
                    case ')':
                        shift = true;
                        k = Keys.D9;
                        break;
                    case '=':
                        shift = true;
                        k = Keys.D0;
                        break;
                    case ',':
                        k = Keys.Oemcomma;
                        break;
                    case '_':
                        shift = true;
                        k = Keys.Subtract;
                        break;
                    case '-':
                        k = Keys.Subtract;
                        break;
                    case '+':
                        k = Keys.Add;
                        break;
                    case '*':
                        k = Keys.Multiply;
                        break;
                    default:
                        k = (Keys)Char.ToUpper(ch);
                        break;
                }

                if (k != Keys.None)
                {
                    if (shift)
                        KeyDown(Keys.ShiftKey);

                    KeyPress(k);

                    if (shift)
                        KeyUp(Keys.ShiftKey);
                }
            }

            KeyUp(Keys.Control);
            KeyUp(Keys.Alt);
            KeyUp(Keys.Shift);
            KeyUp(Keys.LWin);
        }

        public static void KeyUp(Keys key)
        {
            Win32API.keybd_event(ParseKey(key), 0, Win32APIEnums.KEYEVENTF_KEYUP, 0);
        }


        public static void KeyPress(Keys key)
        {
            KeyDown(key);
            KeyUp(key);
        }

        public static void SimulateStandardShortcut(StandardShortcut shortcut)
        {
            switch (shortcut)
            {
                case StandardShortcut.Copy:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.C);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Cut:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.X);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Paste:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.V);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.SelectAll:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.A);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Save:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.S);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Open:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.O);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.New:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.N);
                    KeyUp(Keys.Control);
                    break;
                case StandardShortcut.Close:
                    KeyDown(Keys.Alt);
                    KeyPress(Keys.F4);
                    KeyUp(Keys.Alt);
                    break;
                case StandardShortcut.Print:
                    KeyDown(Keys.Control);
                    KeyPress(Keys.P);
                    KeyUp(Keys.Control);
                    break;
            }
        }

        static byte ParseKey(Keys key)
        {

            // Alt, Shift, and Control need to be changed for API function to work with them
            switch (key)
            {
                case Keys.Alt:
                    return (byte)18;
                case Keys.Control:
                    return (byte)17;
                case Keys.Shift:
                    return (byte)16;
                default:
                    return (byte)key;
            }

        } 

        #endregion

    }

}
