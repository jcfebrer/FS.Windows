#region

using System.ComponentModel;

#endregion

namespace FSFormControls
{
    public class DBHtmlText : DBHtmlNode
    {
        protected string mText;

        public DBHtmlText(string text)
        {
            mText = text;
        }

        [Category("General")]
        [Description("The text located in this text node")]
        public string Text
        {
            get { return mText; }
            set { mText = value; }
        }


        internal bool NoEscaping
        {
            get
            {
                if (mParent == null)
                    return false;
                return mParent.NoEscaping;
            }
        }

        public override string HTML
        {
            get
            {
                if (NoEscaping)
                    return Text;
                return DBHtmlEncoder.EncodeValue(Text);
            }
        }

        public override string XHTML => DBHtmlEncoder.EncodeValue(Text);

        public override string ToString()
        {
            return Text;
        }
    }
}