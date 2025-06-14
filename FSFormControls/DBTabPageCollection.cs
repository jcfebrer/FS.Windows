using System.Windows.Forms;
using static System.Windows.Forms.TabControl;

namespace FSFormControls
{
    public class DBTabPageCollection : TabPageCollection
    {
        public DBTabPageCollection(TabControl owner) : base(owner)
        {
        }

        public new DBTabPage this[int index]
        {
            get { return (DBTabPage) base[index]; }
            set { base[index] = value; }
        }

        public new DBTabPage this[string name] => (DBTabPage) base[name];
    }
}