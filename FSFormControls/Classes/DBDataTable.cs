using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBDataTable : DataTable
    {
        private bool m_readOnly = false;
        public bool ReadOnly
        {
            get { return m_readOnly; }
            set
            {
                m_readOnly = value;
                this.DefaultView.AllowEdit = !value;
            }
        }

        public DBDataTable()
        {
        }
    }
}