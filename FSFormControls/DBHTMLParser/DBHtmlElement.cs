#region

using System;
using System.ComponentModel;
using System.Text;

#endregion

namespace FSFormControls
{
    public class DBHtmlElement : DBHtmlNode
    {
        protected DBHtmlAttributeCollection mAttributes;
        protected bool mIsExplicitlyTerminated;
        protected bool mIsTerminated;
        protected string mName;
        protected DBHtmlNodeCollection mNodes;

        public DBHtmlElement(string name)
        {
            mNodes = new DBHtmlNodeCollection(this);
            mAttributes = new DBHtmlAttributeCollection(this);
            mName = name;
            mIsTerminated = false;
        }

        [Category("General")]
        [Description("The name of the tag/element")]
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        [Category("General")]
        [Description("The set of child nodes")]
        public DBHtmlNodeCollection Nodes
        {
            get
            {
                if (IsText()) throw new InvalidOperationException("An DBHtmlText node does not have child nodes");
                return mNodes;
            }
        }

        [Category("General")]
        [Description("The set of attributes associated with this element")]
        public DBHtmlAttributeCollection Attributes => mAttributes;

        internal bool IsTerminated
        {
            get
            {
                if (Nodes.Count > 0)
                    return false;
                return mIsTerminated | mIsExplicitlyTerminated;
            }
            set { mIsTerminated = value; }
        }

        internal bool IsExplicitlyTerminated
        {
            get { return mIsExplicitlyTerminated; }
            set { mIsExplicitlyTerminated = value; }
        }

        internal bool NoEscaping => "script".Equals(Name.ToLower()) | "style".Equals(Name.ToLower());


        [Category("General")]
        [Description("A concatination of all the text associated with this element")]
        public string Text
        {
            get
            {
                var stringBuilder = new StringBuilder();
                foreach (DBHtmlNode node in Nodes)
                    if (node is DBHtmlText)
                        stringBuilder.Append(((DBHtmlText) node).Text);
                return stringBuilder.ToString();
            }
        }

        [Category("Output")]
        public override string HTML
        {
            get
            {
                var shtml = new StringBuilder();
                shtml.Append("<" + mName);
                foreach (DBHtmlAttribute attribute in Attributes) shtml.Append(" " + attribute.HTML);
                if (Nodes.Count > 0)
                {
                    shtml.Append(">");
                    foreach (DBHtmlNode node in Nodes) shtml.Append(node.HTML);
                    shtml.Append("</" + mName + ">");
                }
                else
                {
                    if (IsExplicitlyTerminated)
                        shtml.Append("></" + mName + ">");
                    else if (IsTerminated)
                        shtml.Append("/>");
                    else
                        shtml.Append(">");
                }

                return shtml.ToString();
            }
        }

        [Category("Output")]
        public override string XHTML
        {
            get
            {
                if ("html".Equals(mName) & (Attributes["xmlns"] == null))
                    Attributes.Add(new DBHtmlAttribute("xmlns", "http://www.w3.org/1999/xhtml"));
                var html = new StringBuilder();
                html.Append("<" + mName.ToLower());
                foreach (DBHtmlAttribute attribute in Attributes) html.Append(" " + attribute.XHTML);
                if (IsTerminated)
                {
                    html.Append("/>");
                }
                else
                {
                    if (Nodes.Count > 0)
                    {
                        html.Append(">");
                        foreach (DBHtmlNode node in Nodes) html.Append(node.XHTML);
                        html.Append("</" + mName.ToLower() + ">");
                    }
                    else
                    {
                        html.Append("/>");
                    }
                }

                return html.ToString();
            }
        }

        public override string ToString()
        {
            var value = "<" + mName;
            foreach (DBHtmlAttribute attribute in Attributes) value += " " + attribute;
            value += ">";
            return value;
        }
    }
}