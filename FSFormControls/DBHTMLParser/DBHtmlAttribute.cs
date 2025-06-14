#region

using System.Collections;
using System.ComponentModel;

#endregion

namespace FSFormControls
{
    public class DBHtmlAttribute
    {
        protected string mName;
        protected string mValue;

        public DBHtmlAttribute()
        {
            mName = "Sinnombre";
            mValue = "";
        }

        public DBHtmlAttribute(string name, string value)
        {
            mName = name;
            mValue = value;
        }

        [Category("General")]
        [Description("The name of the attribute")]
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        [Category("General")]
        [Description("The value of the attribute")]
        public string Value
        {
            get { return mValue; }
            set { mValue = value; }
        }


        [Category("Output")]
        [Description("The HTML to represent this attribute")]
        public string HTML
        {
            get
            {
                if (mValue == null)
                    return mName;
                return mName + "=" + @"""" + DBHtmlEncoder.EncodeValue(mValue) + @"""";
            }
        }

        [Category("Output")]
        [Description("The XHTML to represent this attribute")]
        public string XHTML
        {
            get
            {
                if (mValue == null)
                    return mName.ToLower();
                return mName + "=" + @"""" + DBHtmlEncoder.EncodeValue(mValue.ToLower()) + @"""";
            }
        }

        public override string ToString()
        {
            if (mValue == null)
                return mName;
            return mName + "=" + @"""" + mValue + @"""";
        }
    }


    public class DBHtmlAttributeCollection : CollectionBase
    {
        public DBHtmlElement mElement;

        public DBHtmlAttributeCollection()
        {
            mElement = null;
        }

        internal DBHtmlAttributeCollection(DBHtmlElement element)
        {
            mElement = element;
        }


        public DBHtmlAttribute this[int index]
        {
            get { return (DBHtmlAttribute) List[index]; }
            set { List[index] = value; }
        }

        public DBHtmlAttribute this[string name] => FindByName(name);

        public int Add(DBHtmlAttribute attribute)
        {
            return List.Add(attribute);
        }

        public DBHtmlAttribute FindByName(string name)
        {
            var index = IndexOf(name);
            if (index == -1)
                return null;
            return (DBHtmlAttribute) List[IndexOf(name)];
        }


        public int IndexOf(string name)
        {
            var index = 0;
            for (index = 0; index <= List.Count - 1; index++)
                if (((DBHtmlAttribute) List[index]).Name.ToLower().Equals(name.ToLower()))
                    return index;
            return -1;
        }
    }
}