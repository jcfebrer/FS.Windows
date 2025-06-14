using FSLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using static FSLibrary.Win32APIEnums;

namespace FSSystemInfo
{
    /// <summary>
    /// Clase con utilidades relacionadas con los procesos
    /// </summary>
    public class ProcessUtil
    {
        /// <summary>
        /// Clase ProcessData
        /// </summary>
        public class ProcessData
        {
            /// <summary>
            /// Nombre
            /// </summary>
            public string Name;
            /// <summary>
            /// Título
            /// </summary>
            public string DisplayTitle;
        }

        /// <summary>
        /// Determines whether [is already running].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is already running]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlreadyRunning()
        {
            var strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            var sExeName = fileInfo.Name;
            bool bCreatedNew;

            var mutex = new Mutex(true, "Global\\" + sExeName, out bCreatedNew);
            if (bCreatedNew)
                mutex.ReleaseMutex();

            return !bCreatedNew;
        }

        /// <summary>
        /// Shows the open with dialog.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void ShowOpenWithDialog(string path)
        {
            var args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
            args += ",OpenAs_RunDLL " + path;
            OpenDocument("rundll32.exe", args);
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="DocName">Name of the document.</param>
        /// <returns></returns>
        public static bool OpenDocument(string DocName)
        {
            return OpenDocument(DocName, "");
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="documentName">Name of the document.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static bool OpenDocument(string documentName, string args)
        {
            if (documentName == null)
                throw new Exception("Debes indicar el nombre del documento a abrir.");

            try
            {
                if (!documentName.Contains("http"))
                {
                    if (!documentName.Contains(@"\"))
                        documentName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                  @"\" + documentName;
                }

                if (args != "")
                    Process.Start(documentName, args);
                else
                    Process.Start(documentName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Ejecuta el fichero o url
        /// </summary>
        /// <param name="file">Fichero o Url</param>
        /// <returns></returns>
        public static Process Execute(string file)
        {
            return Process.Start(file);
        }

        /// <summary>
        /// Hides the active window.
        /// </summary>
        public static void HideActiveWindow()
        {
            //ocultamos la ventana selecionada
            //leemos el caption de la ventana
            var handle = Win32API.FindWindow(null, Win32API.GetActiveWindowTitle());

            if (handle != IntPtr.Zero)
            {
                int style = Win32API.GetWindowLong(handle, Win32APIEnums.GWL_STYLE);

                if ((style & Win32APIEnums.WS_VISIBLE) != 0)
                {
                    Win32API.ShowWindow(handle, Win32APIEnums.SW_HIDE);
                }
            }
        }

        /// <summary>
        /// Shows all process with tittle.
        /// </summary>
        public static void ShowAllProcessWithTittle()
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.MainWindowTitle != "")
                    ShowProcessByTitle(process.MainWindowTitle);
            }
        }

        /// <summary>
        /// Shows the process by title.
        /// </summary>
        /// <param name="title">The title.</param>
        public static void ShowProcessByTitle(string title)
        {
            var handle = Win32API.FindWindow(null, title);

            if (handle != IntPtr.Zero)
            {
                Win32API.ShowWindow(handle, Win32APIEnums.SW_SHOW);
            }
        }

        /// <summary>
        /// Hides the process by title.
        /// </summary>
        /// <param name="title">The title.</param>
        public static void HideProcessByTitle(string title)
        {
            var handle = Win32API.FindWindow(null, title);

            if (handle != IntPtr.Zero)
            {
                int style = Win32API.GetWindowLong(handle, Win32APIEnums.GWL_STYLE);

                if ((style & Win32APIEnums.WS_VISIBLE) != 0)
                {
                    Win32API.ShowWindow(handle, Win32APIEnums.SW_HIDE);
                }
            }
        }

        /// <summary>
        /// Envia el proceso actual al frente.
        /// </summary>
        public static void ActivateCurrentProcess()
        {
            Process process = Process.GetCurrentProcess();
            if (process == null)
                return;

            ActivateByProcess(process);
        }

        /// <summary>
        /// Envia el proceso 'processName' al frente.
        /// </summary>
        /// <param name="processName"></param>
        public static void ActivateByProcessName(string processName)
        {
            Process process = null;
            Process[] procesess = Process.GetProcessesByName(processName);
            foreach (Process p in procesess)
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                    process = p;

            if(process != null)
                ActivateByProcess(process);
        }

        /// <summary>
        /// Activamos el proceso indicado en "windowName".
        /// </summary>
        /// <param name="windowName"></param>
        public static void ActivateByWindowName(string windowName)
        {
            IntPtr hWnd = Win32API.FindWindow(null, windowName);
            if (Win32API.IsIconic(hWnd))
                Win32API.ShowWindow(hWnd, Win32APIEnums.WindowShowStyle.Restore);
            Win32API.SetForegroundWindow(hWnd);
        }


        /// <summary>
        /// Activamos el proceso indicado en "process".
        /// </summary>
        /// <param name="process"></param>
        public static void ActivateByProcess(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            if (Win32API.IsIconic(hWnd))
                Win32API.ShowWindow(hWnd, Win32APIEnums.WindowShowStyle.Restore);
            Win32API.SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// Ocultamos el proceso indicado en "process".
        /// </summary>
        /// <param name="process"></param>
        public static void HideByProcess(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            Win32API.ShowWindow(hWnd, Win32APIEnums.WindowShowStyle.Hide);
        }

        /// <summary>
        /// Mostramos el proceso indicado en "process".
        /// </summary>
        /// <param name="process"></param>
        public static void ShowByProcess(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            Win32API.ShowWindow(hWnd, Win32APIEnums.WindowShowStyle.Show);
            Win32API.SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// Mostramos el proceso indicado en "handle".
        /// </summary>
        /// <param name="handle"></param>
        public static void ShowByHandle(IntPtr hWnd)
        {
            Win32API.ShowWindow(hWnd, Win32APIEnums.WindowShowStyle.Show);
            Win32API.SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// Ocultamos el proceso indicado en "handle".
        /// </summary>
        /// <param name="handle"></param>
        public static void HideByHandle(IntPtr hWnd)
        {
            Win32API.ShowWindow(hWnd, Win32APIEnums.WindowShowStyle.Hide);
        }

        /// <summary>
        /// Devolvemos el estado del proceso indicado.
        /// </summary>
        /// <param name="process"></param>
        public static int StateByProcess(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            int style = Win32API.GetWindowLong(hWnd, Win32APIEnums.GWL_STYLE);

            return style;
        }

        /// <summary>
        /// Desocultar un proceso por el nombre del proceso.
        /// </summary>
        /// <param name="processName"></param>
        public static void UnhideProcess(Process process)
        {
            int processId;
            IntPtr handle = Win32API.FindWindowEx(IntPtr.Zero, process.Handle, null, IntPtr.Zero);

            Win32API.GetWindowThreadProcessId(handle, out processId);

            IntPtr intPtr = new IntPtr(processId);
            ShowByHandle(intPtr);
        }

        /// <summary>
        /// Devolvemos la posición de la ventana del proceso indicado.
        /// </summary>
        /// <param name="process"></param>
        public static WINDOWPLACEMENT GetWindowPlacement(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;

            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            Win32API.GetWindowPlacement(hWnd, ref placement);
            
            return placement;
        }

        /// <summary>
        /// Devuelve un listado de los servicios activos
        /// </summary>
        /// <returns></returns>
        public static List<ProcessData> GetProcesses()
        {
            List<ProcessData> listProcess = new List<ProcessData>();
            foreach (Process process in Process.GetProcesses())
            {
                ProcessData processData = new ProcessData();
                processData.DisplayTitle = process.MainWindowTitle;
                processData.Name = process.ProcessName;

                listProcess.Add(processData);
            }

            return listProcess;
        }

        /// <summary>
        /// Comprueba si un proceso se esta ejecutando
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool IsRunning(string processName)
        {
            Process[] pname = Process.GetProcessesByName(processName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Obtiene el texto de la ventana
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static string GetText(IntPtr hWnd)
        {
            int length = Win32API.GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            Win32API.GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        /// <summary>
        /// Obtiene el texto de la ventana dialogo
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static StringBuilder GetEditText(IntPtr hWnd)
        {
            Int32 dwID = Win32API.GetWindowLong(hWnd, Win32APIEnums.GWL_ID);
            IntPtr hWndParent = Win32API.GetParent(hWnd);
            StringBuilder title = new StringBuilder(128);
            Win32API.SendDlgItemMessage(hWndParent, dwID, Win32APIEnums.WM_GETTEXT, 128, title);
            return title;
        }

        /// <summary>
        /// Obtiene las dimensiones de la ventana
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static Rectangle GetRectangle(IntPtr hWnd)
        {
            Rectangle rect = new Rectangle();
            Win32API.GetWindowRect(hWnd, ref rect);

            return rect;
        }

        /// <summary> Get the text for the window pointed to by hWnd </summary>
        public static string GetWindowText(IntPtr hWnd)
        {
            int size = Win32API.GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                Win32API.GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return String.Empty;
        }

        /// <summary> Find all windows that match the given filter </summary>
        /// <param name="filter"> A delegate that returns true for windows
        ///    that should be returned and false for windows that should
        ///    not be returned </param>
        public static IntPtr[] FindWindows(Win32API.EnumWindowsProc filter)
        {
            List<IntPtr> windows = new List<IntPtr>();

            Win32API.EnumWindows(delegate (IntPtr wnd, int param)
            {
                if (filter(wnd, param))
                {
                    windows.Add(wnd);
                }

                return true;
            }, 0);

            return windows.ToArray();
        }

        /// <summary>
        /// Devuelve todas las ventanas.
        /// </summary>
        /// <returns></returns>
        public static IntPtr[] FindAllWindows()
        {
            List<IntPtr> windows = new List<IntPtr>();

            Win32API.EnumWindows(delegate (IntPtr wnd, int param)
            {
                windows.Add(wnd);
                
                return true;
            }, 0);

            return windows.ToArray();
        }


        /// <summary>
        /// Devuelve todas los threads de una ventana.
        /// </summary>
        /// <returns></returns>
        public static IntPtr[] FindThreads(int thread)
        {
            List<IntPtr> windows = new List<IntPtr>();

            Win32API.EnumWindows(delegate (IntPtr wnd, int param)
            {
                int processID = 0;
                int threadID = Win32API.GetWindowThreadProcessId(wnd, out processID);
                if (threadID == param)
                {
                    windows.Add(wnd);
                    Win32API.EnumChildWindows(wnd, delegate (IntPtr wndChild, int paramChild)
                    {
                        windows.Add(wndChild);

                        return true;
                    }, threadID);
                }

                return true;
            }, thread);

            return windows.ToArray();
        }

        /// <summary> Find all windows that contain the given title text </summary>
        /// <param name="titleText"> The text that the window title must contain. </param>
        public static IEnumerable<IntPtr> FindWindowsWithText(string titleText)
        {
            return FindWindows(delegate (IntPtr wnd, int param)
            {
                return GetWindowText(wnd).Contains(titleText);
            });
        }
    }
}