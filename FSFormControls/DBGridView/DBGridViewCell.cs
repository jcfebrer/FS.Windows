using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBGridViewCell : DataGridViewCell
    {
        public DataGridViewCellStyle Appearance
        {
            get { return this.Style; }
            set { this.Style = value; }
        }
        public DBGridViewColumn Column 
        { 
            get { return (DBGridViewColumn)base.DataGridView.Columns[base.ColumnIndex]; }
        }
    }
}
