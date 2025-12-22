#region

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxItem(true)]
    public class DBGroupBox : GroupBox, ISupportInitialize
    {
        public DBGroupBox()
        {
        }

        public string Caption
        {
            get { return Text; }
            set { Text = value; }
        }

        public DBAppearance HeaderAppearance { get; set; }

        public BorderStyle BorderStyle { get; set; }

        public RadioButton Selected {
            get
            {
                foreach (Control ctrl in Controls)
                {
                    if (ctrl is RadioButton rb && rb.Checked)
                        return rb;
                }
                return null;
            }
        }

        public int SelectedIndex
        {
            get
            {
                int i = 0;
                foreach (Control ctrl in Controls)
                {
                    if (ctrl is RadioButton rb)
                    {
                        if(rb.Checked)
                            return i;
                        i++;
                    }
                }
                return -1;
            }
            set
            {
                int i = 0;
                foreach (Control ctrl in Controls)
                {
                    if (ctrl is RadioButton rb)
                    {
                        if(i== value)
                            rb.Checked = true;
                        else
                            rb.Checked = false;
                        i++;
                    }
                }
            }
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}