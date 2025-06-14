using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBGridViewColumn : DataGridViewColumn
    {
        public int MinValue { 
            get { return this.MinValue; }
            set { this.MinValue = value; }
        }

        public int MaxValue
        {
            get { return this.MaxValue; }
            set { this.MaxValue = value; }
        }
    }
}
