using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FSFormControls;

namespace FSFormControls
{
    public class DBGridViewProgressBarColumn : DataGridViewImageColumn
    {
        public DBGridViewProgressBarColumn()
        {
            CellTemplate = new DBGridViewProgressBarCell();
        }
    }
}