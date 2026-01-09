using System;
using System.Collections.Generic;
using System.Text;

namespace FSFormControls
{
    public class DBComboItem
    {
        public object Value { get; set; }
        public string DisplayText { get; set; }

        public DBComboItem(object value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }

        public override string ToString() => DisplayText;
    }
}
