using System;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBGridViewRowCollection : DataGridViewRowCollection
    {
        public DBGridViewRowCollection(DataGridView dataGridView) : base(dataGridView)
        {
        }

        public new DBGridViewRow this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");
                }
                return (DBGridViewRow)base[index];
            }
        }

        public DBGridViewFilterCollection ColumnFilters { get; set; }
    }
}