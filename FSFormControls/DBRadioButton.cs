using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxItem(true)]
    [Serializable]
    public class DBRadioButton : RadioButton
    {
        public string DisplayText
        {
            get { return Text; }
            set { Text = value; }
        }

        public object DataValue { get; set; }
    }
}