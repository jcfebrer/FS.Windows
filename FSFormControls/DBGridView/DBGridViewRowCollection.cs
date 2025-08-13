using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBGridViewRowCollection : DataGridViewRowCollection
    {
        public DBGridViewRowCollection(DataGridView dataGridView) : base(dataGridView)
        {
        }
    }
}