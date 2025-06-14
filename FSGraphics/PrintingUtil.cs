using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;

namespace FSGraphics
{
    class PrintingUtil
    {
        /// <summary>
        /// Devuelve en aArray una lista de impresoras instaladas
        /// </summary>
        /// <param name="aArray"></param>
        /// <returns></returns>
        public static int PrintersArray(ref string[] aArray)
        {
            var oPrtSettings = new PrinterSettings();

            var se = PrinterSettings.InstalledPrinters.GetEnumerator();
            var i = 0;

            while (se.MoveNext()) i = i + 1;

            aArray = new string[i + 1];
            i = 0;
            se.Reset();
            while (se.MoveNext())
            {
                if (se.Current != null)
                    aArray[i] = se.Current.ToString();
                i = i + 1;
            }


            return aArray.Length;
        }


        public static IEnumerator PrintersCollection()
        {
            var oPrtSettings = new PrinterSettings();

            return PrinterSettings.InstalledPrinters.GetEnumerator();
        }
    }
}
