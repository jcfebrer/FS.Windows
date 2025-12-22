#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBDate.bmp")]
    [ToolboxItem(true)]
    public class DBDateTimePicker : DateTimePicker
    {
        public DateTime Date
        {
            get { return base.Value; }
            set { base.Value = value; }
        }

        [Browsable(true)]
        public new object Value
        {
            get
            {
                if (Checked)
                    return base.Value;
                return DBNull.Value;
            }
            set
            {
                if (Convert.IsDBNull(value))
                {
                    Checked = true;
                    Checked = false;
                }
                else
                {
                    base.Value = Convert.ToDateTime(value);
                }
            }
        }

        #region '" Windows Form Designer generated code "' 

        private IContainer components;

        public DBDateTimePicker()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }


        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
        }

        #endregion
    }
}