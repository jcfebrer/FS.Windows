#region

using System;
using System.ComponentModel;
using System.Text;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class DBHtmlDocument
    {
        public DBHtmlNodeCollection mNodes = new DBHtmlNodeCollection(null);


        public DBHtmlDocument(string html, bool wantSpaces)
        {
            var parser = new DBHtmlParser();
            parser.RemoveEmptyElementText = !wantSpaces;
            try
            {
                mNodes = parser.Parse(html);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        [Category("General")]
        [Description("This is the DOCTYPE for XHTML production")]
        public string DocTypeXHTML { get; set; } =
            "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN" + @"""" + " " + @"""" +
            "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd" + @"""" + ">";

        public DBHtmlNodeCollection Nodes => mNodes;


        [Category("Output")]
        [Description("The HTML version of this document")]
        public string HTML
        {
            get
            {
                var ohtml = new StringBuilder();
                foreach (DBHtmlNode node in Nodes) ohtml.Append(node.HTML);
                return ohtml.ToString();
            }
        }

        [Category("Output")]
        [Description("The XHTML version of this document")]
        public string XHTML
        {
            get
            {
                var html = new StringBuilder();
                if (!(DocTypeXHTML == null))
                {
                    html.Append(DocTypeXHTML);
                    html.Append("\r\n");
                }

                foreach (DBHtmlNode node in Nodes) html.Append(node.XHTML);
                return html.ToString();
            }
        }

        public static DBHtmlDocument Create(string html)
        {
            return new DBHtmlDocument(html, false);
        }


        public static DBHtmlDocument Create(string html, bool wantSpaces)
        {
            return new DBHtmlDocument(html, wantSpaces);
        }
    }
}