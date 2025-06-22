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
        //FEBRER: Implementar
        public bool ReadOnly { get; set; }

        public DBDataTable()
        {
        }
    }
}