#region

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FSException;
using FSLibrary;
using FSTrace;
using DateTimeUtil = FSLibrary.DateTimeUtil;

#endregion

namespace FSFormControls
{
    public class Error
    {
        public static void ErrorMessage(Form frm, object sender, string message, string title, MessageBoxIcon icon,
            Exception ex, bool Silent)
        {
            if (FSException.ExceptionUtil.IsCritical(ex))
                throw ex;

            try
            {
                string mess = null;
                var frmE = new frmError();
                var senderName = "Nothing";
                var assemblyVersion = "";
                try
                {
                    assemblyVersion = "Assembly Version: " +
                          FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileMajorPart + "." +
                          FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileMinorPart;
                }
                catch
                {
                    assemblyVersion = "Null";
                }


                Cursor.Current = Cursors.Default;

                try
                {
                    if (sender != null && sender is Control)
                        senderName = ((Control) sender).Name;
                }
                catch
                {
                    senderName = "Nothing";
                }

                mess = "InstallDir: " + Environment.CurrentDirectory + "\r\n";
                mess = mess + "Machine: " + Environment.MachineName + "\r\n";
                mess = mess + "OSVersion: " + Environment.OSVersion + "\r\n";
                try
                {
                    mess = mess + "User Domain: " + Environment.UserDomainName + "\r\n";
                }
                catch
                {
                    mess = mess + "User Domain: Null" + "\r\n";
                }

                mess = mess + "User Name: " + Environment.UserName + "\r\n";
                mess = mess + "Version: " + Environment.Version + "\r\n";
                mess = mess + assemblyVersion + "\r\n";
                mess = mess + "Sender: [" + senderName + "]" + "\r\n";

                if (sender is DBControl)
                    mess = mess + "SQL: " + ((DBControl) sender).Selection + "\r\n";

                if (frm != null)
                    mess = mess + "Form: " + frm.Name + "\r\n";

                if (ex == null)
                {
                    if (String.IsNullOrEmpty(message))
                        message = "Error no controlado de la aplicación. Pulse en 'Desplegar', para una información mas ampliada del error.";
                }
                else
                {
                    if (String.IsNullOrEmpty(message))
                        message = ex.Message;
                    mess = mess + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace + "\r\n";
                    if (ex.InnerException != null)
                        mess = mess + "\r\n[InnerException]\r\n" + ex.InnerException + "\r\n";
                }

                if (String.IsNullOrEmpty(title)) 
                    title = "Gestor de Errores";
                mess = mess + message;

                Log.TraceError(mess);

                if (!Global.SilentError)
                    if (!Silent)
                        frmE.ShowDialog(message, mess, title);

                if (ex == null)
                    Global.Errors.Add(new Exception(mess + "\r\n" + message));
                else
                    Global.Errors.Add(ex);
            }
            catch (Exception e)
            {
                MessageBox.Show("Imposible gestionar error. Función: ErrorMessage. Error: " + e, "Febrer Software");
            }
        }

        public static void ErrorMessage(Form frm, object sender, string message)
        {
            ErrorMessage(frm, sender, message);
        }

        public static void ErrorMessage(Form frm, object sender, string message, string title)
        {
            ErrorMessage(frm, sender, message, title, MessageBoxIcon.Error);
        }

        public static void ErrorMessage(Form frm, object sender, string message, string title, MessageBoxIcon icon)
        {
            ErrorMessage(frm, sender, message, title, icon, null);
        }

        public static void ErrorMessage(Form frm, object sender, string message, string title, MessageBoxIcon icon,
            Exception ex)
        {
            ErrorMessage(frm, sender, message, title, icon, ex, false);
        }


        public static void ErrorMessage(Exception e)
        {
            ErrorMessage(null, null, "", "", MessageBoxIcon.Error, e, false);
        }


        public static void ErrorMessage(Form frm, Exception e)
        {
            ErrorMessage(frm, null, "", "", MessageBoxIcon.Error, e, false);
        }


        public static void ErrorMessage(Form frm, Exception e, string message)
        {
            ErrorMessage(frm, null, message, "", MessageBoxIcon.Error, e, false);
        }


        public static void ErrorMessage(Form frm, string message)
        {
            ErrorMessage(frm, null, message, "", MessageBoxIcon.Error, null, false);
        }


        public static void ErrorMessage(string message)
        {
            ErrorMessage(null, null, message, "", MessageBoxIcon.Error, null, false);
        }


        public static void ErrorMessage(Exception e, string message)
        {
            ErrorMessage(null, null, message, "", MessageBoxIcon.Error, e, false);
        }


        public static void ErrorMessage(Form frm, object sender, Exception e)
        {
            ErrorMessage(frm, sender, "", "", MessageBoxIcon.Error, e, false);
        }


        public static void ErrorMessage(Form frm, Exception e, bool silent)
        {
            ErrorMessage(frm, null, "", "", MessageBoxIcon.Error, e, silent);
        }


        public static void ErrorMessage(Form frm, string message, bool silent)
        {
            ErrorMessage(frm, null, message, "", MessageBoxIcon.Error, null, silent);
        }


        public static void ErrorMessage(Form frm, object sender, Exception e, bool silent)
        {
            ErrorMessage(frm, sender, "", "", MessageBoxIcon.Error, e, silent);
        }
    }
}