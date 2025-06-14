using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FSFormLibrary
{
    /// <summary>
    /// Funciones relacionadas con el portapapeles
    /// </summary>
    public static class Clipboard
    {
        /// <summary>
        /// Obtiene el texto del portapapeles
        /// </summary>
        /// <returns></returns>
        public static string GetClipBoardText()
        {
            string idat = String.Empty;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        if (System.Windows.Forms.Clipboard.ContainsText(TextDataFormat.Text))
                        {
                            idat = System.Windows.Forms.Clipboard.GetText(TextDataFormat.Text);
                        }
                        else
                            idat = String.Empty;
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            return idat;
        }

        /// <summary>
        /// Obtiene el texto del portapapeles
        /// </summary>
        /// <returns></returns>
        public static string GetText()
        {
            return GetClipBoardText();
        }

        /// <summary>
        /// Obtiene el objecto del portapapeles
        /// </summary>
        /// <returns></returns>
        public static IDataObject GetDataObject()
        {
            IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        idat= System.Windows.Forms.Clipboard.GetDataObject();
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            return idat;
        }

        /// <summary>
        /// Establece el objeto en el portapapeles
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="copy"></param>
        public static void SetDataObject(object obj, bool copy)
        {
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        System.Windows.Forms.Clipboard.SetDataObject(obj, copy);
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }
    }
}
