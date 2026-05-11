using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace FSPdf
{
    public class ReadFile
    {
        private static string CleanupContent(string text)
        {
            string[] patterns = { @"\\\(", @"\\\)", @"\\226", @"\\222", @"\\223", @"\\224", @"\\340", @"\\342", @"\\344", @"\\300", @"\\302", @"\\304", @"\\351", @"\\350", @"\\352", @"\\353", @"\\311", @"\\310", @"\\312", @"\\313", @"\\362", @"\\364", @"\\366", @"\\322", @"\\324", @"\\326", @"\\354", @"\\356", @"\\357", @"\\314", @"\\316", @"\\317", @"\\347", @"\\307", @"\\371", @"\\373", @"\\374", @"\\331", @"\\333", @"\\334", @"\\256", @"\\231", @"\\253", @"\\273", @"\\251", @"\\221" };
            string[] replace = { "(", ")", "-", "'", "\"", "\"", "à", "â", "ä", "À", "Â", "Ä", "é", "è", "ê", "ë", "É", "È", "Ê", "Ë", "ò", "ô", "ö", "Ò", "Ô", "Ö", "ì", "î", "ï", "Ì", "Î", "Ï", "ç", "Ç", "ù", "û", "ü", "Ù", "Û", "Ü", "®", "™", "«", "»", "©", "'" };

            for (int i = 0; i < patterns.Length; i++)
            {
                string regExPattern = patterns[i];
                Regex regex = new Regex(regExPattern, RegexOptions.IgnoreCase);
                text = regex.Replace(text, replace[i]);
            }

            return text;
        }

        public static string ReadPdfFile(string Filename)
        {
            string strText = string.Empty;
            PdfReader reader = new PdfReader(Filename);
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                String s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                strText = strText + s;
            }
            reader.Close();
            return CleanupContent(strText);
        }
    }
}
