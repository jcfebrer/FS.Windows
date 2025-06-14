using System.Windows.Forms;

namespace FSFormControls
{
    public class DBGridViewRow : DataGridViewRow
    {
        public bool IsFilteredOut
        {
            get
            {
                //FEBRER: Comprobar valor a devolver en función de si esta filtrado o no
                return false;
            }
        }

        private DBGridViewRow m_parentRow;
        public DBGridViewRow ParentRow
        {
            //FEBRER: Comprobar valor a devolver en función del parent row.
            get { return m_parentRow; }
            set { m_parentRow = value; }
        }
    }
}