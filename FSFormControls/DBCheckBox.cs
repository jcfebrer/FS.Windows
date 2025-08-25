using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBCheckBox : CheckBox, ISupportInitialize
    {
        public new DBAppearance Appearance { get; set; }

        public DBCheckBox()
        {
            Appearance = new DBAppearance();
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}
