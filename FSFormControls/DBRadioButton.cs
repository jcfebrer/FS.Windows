using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
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