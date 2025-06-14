using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using FSLibrary;

namespace FSMouseKeyboardLibrary
{

    /// <summary>
    /// Abstract base class for Mouse and Keyboard hooks
    /// </summary>
    public abstract class GlobalHook
    {
        #region Private Variables

        protected int hookType;
        protected int handleToHook;
        protected bool isStarted;
        protected Win32API.HookProc hookCallback;

        #endregion

        #region Properties

        public bool IsStart
        {
            get
            {
                return isStarted;
            }
        }

        #endregion

        #region Constructor

        public GlobalHook()
        {

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

        }

        #endregion

        #region Methods

        public void Start()
        {

            if (!isStarted &&
                hookType != 0)
            {

                // Make sure we keep a reference to this delegate!
                // If not, GC randomly collects it, and a NullReference exception is thrown
                hookCallback = new Win32API.HookProc(HookCallbackProcedure);

                //Metodo 1:
                //_handleToHook = SetWindowsHookEx(
                //    _hookType,
                //    _hookCallback,
                //    Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                //    0);

                //Metodo 2:
                using (ProcessModule curModule = Process.GetCurrentProcess().MainModule)
                {
                    handleToHook = Win32API.SetWindowsHookEx(
                    hookType,
                    hookCallback,
                    Win32API.GetModuleHandle(curModule.ModuleName),
                    0);
                }

                // Were we able to sucessfully start hook?
                if (handleToHook != 0)
                {
                    isStarted = true;
                }

            }

        }

        public void Stop()
        {

            if (isStarted)
            {

                Win32API.UnhookWindowsHookEx(handleToHook);

                isStarted = false;

            }

        }

        protected virtual int HookCallbackProcedure(int nCode, Int32 wParam, IntPtr lParam)
        {
           
            // This method must be overriden by each extending hook
            return 0;

        }

        protected void Application_ApplicationExit(object sender, EventArgs e)
        {

            if (isStarted)
            {
                Stop();
            }

        }

        #endregion

    }

}
