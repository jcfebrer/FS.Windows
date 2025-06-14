using FSLibrary;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FSMouseKeyboardLibrary
{
    /// <summary>
    /// Método 2 para la captura global de teclado y Ratón.
    /// This class allows you to tap keyboard and mouse and / or to detect their activity even when an 
    /// application runes in background or does not have any user interface at all. This class raises 
    /// common .NET events with KeyEventArgs and MouseEventArgs so you can easily retrive any information you need.
    /// </summary>
    public class Hook
    {
        /// <summary>
        /// Creates an instance of UserActivityHook object and sets mouse and keyboard hooks.
        /// </summary>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        public Hook()
        {
            Start();
        }

        /// <summary>
        /// Creates an instance of UserActivityHook object and installs both or one of mouse and/or keyboard hooks and starts rasing events
        /// </summary>
        /// <param name="InstallMouseHook"><b>true</b> if mouse events must be monitored</param>
        /// <param name="InstallKeyboardHook"><b>true</b> if keyboard events must be monitored</param>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        /// <remarks>
        /// To create an instance without installing hooks call new UserActivityHook(false, false)
        /// </remarks>
        public Hook(bool InstallMouseHook, bool InstallKeyboardHook)
        {
            Start(InstallMouseHook, InstallKeyboardHook);
        }

        /// <summary>
        /// Destruction.
        /// </summary>
        ~Hook()
        {
            //uninstall hooks and do not throw exceptions
            Stop(true, true, false);
        }

        /// <summary>
        /// Occurs when the user moves the mouse, presses any mouse button or scrolls the wheel
        /// </summary>
        public event MouseEventHandler MouseMove;

        /// <summary>
        /// Occurs when the user presses a key
        /// </summary>
        public event KeyEventHandler KeyDown;

        /// <summary>
        /// Occurs when the user presses and releases 
        /// </summary>
        public event KeyPressEventHandler KeyPress;

        /// <summary>
        /// Occurs when the user releases a key
        /// </summary>
        public event KeyEventHandler KeyUp;


        /// <summary>
        /// Stores the handle to the mouse hook procedure.
        /// </summary>
        private int hMouseHook = 0;

        /// <summary>
        /// Stores the handle to the keyboard hook procedure.
        /// </summary>
        private int hKeyboardHook = 0;


        /// <summary>
        /// Declare MouseHookProcedure as HookProc type.
        /// </summary>
        private static Win32API.HookProc MouseHookProcedure;

        /// <summary>
        /// Declare KeyboardHookProcedure as HookProc type.
        /// </summary>
        private static Win32API.HookProc KeyboardHookProcedure;

        private static int lastVKCode = 0;
        private static int lastScanCode = 0;
        private static byte[] lastKeyState = new byte[255];
        private static bool lastIsDead = false;

        // Tiempo en ms desde el último click.
        private static System.DateTime lastClickTime = System.DateTime.Now;

        /// <summary>
        /// Installs both mouse and keyboard hooks and starts rasing events
        /// </summary>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        public void Start()
        {
           Start(true, true);
        }

        public bool IsStarted = false;
        /// <summary>
        /// Installs both or one of mouse and/or keyboard hooks and starts rasing events
        /// </summary>
        /// <param name="InstallMouseHook"><b>true</b> if mouse events must be monitored</param>
        /// <param name="InstallKeyboardHook"><b>true</b> if keyboard events must be monitored</param>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        public void Start(bool InstallMouseHook, bool InstallKeyboardHook)
        {
            IsStarted = true;
            // install Mouse hook only if it is not installed and must be installed
            if (hMouseHook == 0 && InstallMouseHook)
            {
                // Create an instance of HookProc.
                MouseHookProcedure = new Win32API.HookProc(MouseHookProc);
                //install hook
                using (ProcessModule curModule = Process.GetCurrentProcess().MainModule)
                {
                    hMouseHook = Win32API.SetWindowsHookEx(
                        Win32APIEnums.WH_MOUSE_LL,
                        MouseHookProcedure,
                        Win32API.GetModuleHandle(curModule.ModuleName),
                        0);
                }
                //If SetWindowsHookEx fails.
                if (hMouseHook == 0)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //do cleanup
                    Stop(true, false, false);
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }

            // install Keyboard hook only if it is not installed and must be installed
            if (hKeyboardHook == 0 && InstallKeyboardHook)
            {
                // Create an instance of HookProc.
                KeyboardHookProcedure = new Win32API.HookProc(KeyboardHookProc);
                //install hook
                using (ProcessModule curModule = Process.GetCurrentProcess().MainModule)
                {
                    hKeyboardHook = Win32API.SetWindowsHookEx(
                        Win32APIEnums.WH_KEYBOARD_LL,
                        KeyboardHookProcedure,
                        Win32API.GetModuleHandle(curModule.ModuleName),
                        0);
                }
                //If SetWindowsHookEx fails.
                if (hKeyboardHook == 0)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //do cleanup
                    Stop(false, true, false);
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }
        }

        /// <summary>
        /// Stops monitoring both mouse and keyboard events and rasing events.
        /// </summary>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        public void Stop()
        {
            Stop(true, true, true);
        }

        /// <summary>
        /// Stops monitoring both or one of mouse and/or keyboard events and rasing events.
        /// </summary>
        /// <param name="UninstallMouseHook"><b>true</b> if mouse hook must be uninstalled</param>
        /// <param name="UninstallKeyboardHook"><b>true</b> if keyboard hook must be uninstalled</param>
        /// <param name="ThrowExceptions"><b>true</b> if exceptions which occured during uninstalling must be thrown</param>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        public void Stop(bool UninstallMouseHook, bool UninstallKeyboardHook, bool ThrowExceptions)
        {
            IsStarted = false;
            //if mouse hook set and must be uninstalled
            if (hMouseHook != 0 && UninstallMouseHook)
            {
                //uninstall hook
                int retMouse = Win32API.UnhookWindowsHookEx(hMouseHook);
                //reset invalid handle
                hMouseHook = 0;
                //if failed and exception must be thrown
                if (retMouse == 0 && ThrowExceptions)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }

            //if keyboard hook set and must be uninstalled
            if (hKeyboardHook != 0 && UninstallKeyboardHook)
            {
                //uninstall hook
                int retKeyboard = Win32API.UnhookWindowsHookEx(hKeyboardHook);
                //reset invalid handle
                hKeyboardHook = 0;
                //if failed and exception must be thrown
                if (retKeyboard == 0 && ThrowExceptions)
                {
                    //Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
                    int errorCode = Marshal.GetLastWin32Error();
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(errorCode);
                }
            }
        }


        /// <summary>
        /// A callback function which will be called every time a mouse activity detected.
        /// </summary>
        /// <param name="nCode">
        /// [in] Specifies whether the hook procedure must process the message. 
        /// If nCode is HC_ACTION, the hook procedure must process the message. 
        /// If nCode is less than zero, the hook procedure must pass the message to the 
        /// CallNextHookEx function without further processing and must return the 
        /// value returned by CallNextHookEx.
        /// </param>
        /// <param name="wParam">
        /// [in] Specifies whether the message was sent by the current thread. 
        /// If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
        /// </param>
        /// <param name="lParam">
        /// [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
        /// </param>
        /// <returns>
        /// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
        /// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx 
        /// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC 
        /// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook 
        /// procedure does not call CallNextHookEx, the return value should be zero. 
        /// </returns>
        private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            // if ok and someone listens to our events
            if ((nCode >= 0) && (MouseMove != null))
            {
                //Marshall the data from callback.
                Win32APIEnums.MouseLLHookStruct mouseHookStruct =
                    (Win32APIEnums.MouseLLHookStruct) Marshal.PtrToStructure(lParam, typeof (Win32APIEnums.MouseLLHookStruct));

                //detect button clicked
                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                switch (wParam)
                {
                    case Win32APIEnums.WM_LBUTTONDOWN:
                        //case WM_LBUTTONUP: 
                        //case WM_LBUTTONDBLCLK: 
                        button = MouseButtons.Left;
                        break;
                    case Win32APIEnums.WM_RBUTTONDOWN:
                        //case WM_RBUTTONUP: 
                        //case WM_RBUTTONDBLCLK: 
                        button = MouseButtons.Right;
                        break;
                    case Win32APIEnums.WM_MOUSEWHEEL:
                        //If the message is WM_MOUSEWHEEL, the high-order word of mouseData member is the wheel delta. 
                        //One wheel click is defined as WHEEL_DELTA, which is 120. 
                        //(value >> 16) & 0xffff; retrieves the high-order word from the given 32-bit value
                        mouseDelta = (short) ((mouseHookStruct.mouseData >> 16) & 0xffff);
                        //TODO: X BUTTONS (I havent them so was unable to test)
                        //If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, 
                        //or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, 
                        //and the low-order word is reserved. This value can be one or more of the following values. 
                        //Otherwise, mouseData is not used. 
                        break;
                }

                //double clicks
                int clickCount = 0;
                if (button != MouseButtons.None)
                    if (wParam == Win32APIEnums.WM_LBUTTONDBLCLK || wParam == Win32APIEnums.WM_RBUTTONDBLCLK) clickCount = 2;
                    else clickCount = 1;

                int totalTime = (int)(System.DateTime.Now - lastClickTime).TotalMilliseconds;
                // Alternativa a utilizar WM_LBUTTONDBLCLK... Este mensaje solo funciona con ventanes de estilo CS_DBLCLKS
                if (clickCount == 1 && (totalTime < Win32API.GetDoubleClickTime()))
                {
                    clickCount = 2;
                }

                if (clickCount != 0)
                    lastClickTime = System.DateTime.Now;

                //generate event 
                MouseEventArgs e = new MouseEventArgs(
                    button,
                    clickCount,
                    mouseHookStruct.pt.x,
                    mouseHookStruct.pt.y,
                    mouseDelta);
                //raise it
                MouseMove(this, e);
            }
            //call next hook
            return Win32API.CallNextHookEx(hMouseHook, nCode, wParam, lParam);
        }


        /// <summary>
        /// Convert VKCode to Unicode.
        /// <remarks>isKeyDown is required for because of keyboard state inconsistencies!</remarks>
        /// </summary>
        /// <param name="VKCode">VKCode</param>
        /// <param name="isKeyDown">Is the key down event?</param>
        /// <returns>String representing single unicode character.</returns>
        public static string VKCodeToString(int VKCode, bool isKeyDown)
        {
            // ToUnicodeEx needs StringBuilder, it populates that during execution.
            System.Text.StringBuilder sbString = new System.Text.StringBuilder(5);

            byte[] bKeyState = new byte[255];
            bool bKeyStateStatus;
            bool isDead = false;

            // Gets the current windows window handle, threadID, processID
            IntPtr currentHWnd = Win32API.GetForegroundWindow();
            int currentProcessID;
            int currentWindowThreadID = Win32API.GetWindowThreadProcessId(currentHWnd, out currentProcessID);

            // This programs Thread ID
            int thisProgramThreadId = Win32API.GetCurrentThreadId();

            // Attach to active thread so we can get that keyboard state
            if (Win32API.AttachThreadInput(thisProgramThreadId, currentWindowThreadID, true))
            {
                // Current state of the modifiers in keyboard
                bKeyStateStatus = Win32API.GetKeyboardState(bKeyState);

                // Detach
                Win32API.AttachThreadInput(thisProgramThreadId, currentWindowThreadID, false);
            }
            else
            {
                // Could not attach, perhaps it is this process?
                bKeyStateStatus = Win32API.GetKeyboardState(bKeyState);
            }

            // On failure we return empty string.
            if (!bKeyStateStatus)
                return "";

            // Gets the layout of keyboard
            IntPtr HKL = Win32API.GetKeyboardLayout(currentWindowThreadID);

            // Maps the virtual keycode
            int lScanCode = Win32API.MapVirtualKeyEx(VKCode, 0, HKL);

            // Keyboard state goes inconsistent if this is not in place. In other words, we need to call above commands in UP events also.
            if (!isKeyDown)
                return "";

            // Converts the VKCode to unicode
            int relevantKeyCountInBuffer = Win32API.ToUnicodeEx(VKCode, lScanCode, bKeyState, sbString, sbString.Capacity, (int)0, HKL);

            string ret = "";

            switch (relevantKeyCountInBuffer)
            {
                // Dead keys (^,`...)
                case -1:
                    isDead = true;

                    // We must clear the buffer because ToUnicodeEx messed it up, see below.
                    ClearKeyboardBuffer(VKCode, lScanCode, HKL);
                    break;

                case 0:
                    break;

                // Single character in buffer
                case 1:
                    ret = sbString[0].ToString();
                    break;

                // Two or more (only two of them is relevant)
                case 2:
                default:
                    ret = sbString.ToString().Substring(0, 2);
                    break;
            }

            // We inject the last dead key back, since ToUnicodeEx removed it.
            // More about this peculiar behavior see e.g: 
            //   http://www.experts-exchange.com/Programming/System/Windows__Programming/Q_23453780.html
            //   http://blogs.msdn.com/michkap/archive/2005/01/19/355870.aspx
            //   http://blogs.msdn.com/michkap/archive/2007/10/27/5717859.aspx
            if (lastVKCode != 0 && lastIsDead)
            {
                System.Text.StringBuilder sbTemp = new System.Text.StringBuilder(5);
                Win32API.ToUnicodeEx(lastVKCode, lastScanCode, lastKeyState, sbTemp, sbTemp.Capacity, (int)0, HKL);
                lastVKCode = 0;

                return ret;
            }

            // Save these
            lastScanCode = lScanCode;
            lastVKCode = VKCode;
            lastIsDead = isDead;
            lastKeyState = (byte[])bKeyState.Clone();

            return ret;
        }

        private static void ClearKeyboardBuffer(int vk, int sc, IntPtr hkl)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(10);

            int rc;
            do
            {
                byte[] lpKeyStateNull = new Byte[255];
                rc = Win32API.ToUnicodeEx(vk, sc, lpKeyStateNull, sb, sb.Capacity, 0, hkl);
            } while (rc < 0);
        }

        //private bool IsDeadKey(int key)
        //{
        //    return ((MapVirtualKey((uint)key, 2) & 2147483648) == 2147483648);
        //}

        /// <summary>
        /// A callback function which will be called every time a keyboard activity detected.
        /// </summary>
        /// <param name="nCode">
        /// [in] Specifies whether the hook procedure must process the message. 
        /// If nCode is HC_ACTION, the hook procedure must process the message. 
        /// If nCode is less than zero, the hook procedure must pass the message to the 
        /// CallNextHookEx function without further processing and must return the 
        /// value returned by CallNextHookEx.
        /// </param>
        /// <param name="wParam">
        /// [in] Specifies whether the message was sent by the current thread. 
        /// If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
        /// </param>
        /// <param name="lParam">
        /// [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
        /// </param>
        /// <returns>
        /// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
        /// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx 
        /// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC 
        /// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook 
        /// procedure does not call CallNextHookEx, the return value should be zero. 
        /// </returns>
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            //indicates if any of underlaing events set e.Handled flag
            bool handled = false;
            //it was ok and someone listens to events

            if ((nCode >= 0) && (KeyDown != null || KeyUp != null || KeyPress != null))
            {
                //read structure KeyboardHookStruct at lParam
                Win32APIEnums.KeyboardHookStruct MyKeyboardHookStruct =
                    (Win32APIEnums.KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof (Win32APIEnums.KeyboardHookStruct));
                
                
                //raise KeyDown
                if (KeyDown != null && (wParam == Win32APIEnums.WM_KEYDOWN || wParam == Win32APIEnums.WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys) MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyDown(this, e);
                    handled = handled || e.Handled;
                }

                // raise KeyPress
                if (KeyPress != null && wParam == Win32APIEnums.WM_KEYDOWN)
                {
                    //bool isDownShift = ((GetKeyState(VK_SHIFT) & 0x80) == 0x80 ? true : false);
                    //bool isDownCapslock = (GetKeyState(VK_CAPITAL) != 0 ? true : false);
                    //bool isDownControl = (GetKeyState(VK_CAPITAL) != 0 ? true : false);
                    //bool isDownAlt = (GetKeyState(VK_ALT) != 0 ? true : false);
                    
                    //byte[] keyState = new byte[256];
                    //GetKeyboardState(keyState);
                    //byte[] inBuffer = new byte[2];
                    //char[] inBuffer = new char[4];

                    //int val = 0;

                    //if (!IsDeadKey(MyKeyboardHookStruct.vkCode))
                    //{
                        //val = ToAscii(MyKeyboardHookStruct.vkCode,
                        //            MyKeyboardHookStruct.scanCode,
                        //            keyState,
                        //            inBuffer,
                        //            MyKeyboardHookStruct.flags);

                        //val = ToUnicode(
                        //        (uint)MyKeyboardHookStruct.vkCode,
                        //        (uint)MyKeyboardHookStruct.scanCode,
                        //        keyState, inBuffer, inBuffer.Length, (uint)MyKeyboardHookStruct.flags
                        //        );

                        //if (val < 0)
                        //{
                        //    val = ToAscii(MyKeyboardHookStruct.vkCode,
                        //                MyKeyboardHookStruct.scanCode,
                        //                keyState,
                        //                inBuffer,
                        //                MyKeyboardHookStruct.flags);

                        //    //val = ToUnicode(
                        //    //    (uint)MyKeyboardHookStruct.vkCode,
                        //    //    (uint)MyKeyboardHookStruct.scanCode,
                        //    //    keyState, inBuffer, inBuffer.Length, (uint)MyKeyboardHookStruct.flags
                        //    //    );
                        //}


                        //if (val == 1)
                        //{
                        //    char key = (char)inBuffer[0];
                        //    if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) key = Char.ToUpper(key);
                        //    KeyPressEventArgs e = new KeyPressEventArgs(key);
                        //    KeyPress(this, e);
                        //    handled = handled || e.Handled;
                        //}
                    //}

                    string key = VKCodeToString((int)Marshal.ReadInt32(lParam),
                        (wParam == Win32APIEnums.WM_KEYDOWN ||
                        wParam == Win32APIEnums.WM_SYSKEYDOWN));
                    if (key != "")
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs(key.ToCharArray()[0]);
                        KeyPress(this, e);
                        handled = handled || e.Handled;
                    }
                }

                // raise KeyUp
                if (KeyUp != null && (wParam == Win32APIEnums.WM_KEYUP || wParam == Win32APIEnums.WM_SYSKEYUP))
                {
                    Keys keyData = (Keys) MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyUp(this, e);
                    handled = handled || e.Handled;
                }

            }

            //if event handled in application do not handoff to other listeners
            if (handled)
                return 1;
            else
                return Win32API.CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }
    }
}