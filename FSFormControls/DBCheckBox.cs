using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBCheckBox.bmp")]
    [DefaultEvent("CheckedChanged")]
    [ToolboxItem(true)]
    public class DBCheckBox : CheckBox, ISupportInitialize
    {
        public new DBAppearance Appearance { get; set; }
        public string About { get; set; }

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
