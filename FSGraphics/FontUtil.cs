using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Text;

namespace FSGraphics
{
    class FontUtil
    {
        /// <summary>
        /// Devuelve en aArray una lista de las fuentes instaladas
        /// </summary>
        /// <param name="aArray"></param>
        /// <returns></returns>
        public static int FontArray(ref string[] aArray)
        {
            var fc = new InstalledFontCollection();

            FontFamily[] afm = null;
            afm = fc.Families;

            aArray = new string[afm.Length + 1];

            var i = 0;
            for (i = 0; i <= afm.Length - 1; i += i + 1) aArray.SetValue(afm[i].Name, i);

            return i;
        }


        public static IEnumerator FontCollection()
        {
            var fc = new InstalledFontCollection();

            return fc.Families.GetEnumerator();
        }
    }
}
