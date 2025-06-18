using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.TabControl;

namespace FSFormControls
{
    [ToolboxItem(true)]
    [Serializable]
    public class DBTabPageCollection : TabPageCollection
    {
        public DBTabPageCollection() : base(new TabControl())
        {
        }

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