using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBToolBarButton : ToolStripButton
    {
        public DBToolBarButton()
        {
        }

        public DBToolBarButton(string name)
        {
            Name = name;
        }

        public string Caption
        {
            get { return Text; }
            set { Text = value; }
        }

        public Shortcut Shortcut { get; set; }

        public new bool Checked { get; set; }

        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }

        public class SharedDef
        {
            public SharedDef()
            {
                AppearancesSmall = new DBAppearance();
            }

            public Shortcut Shortcut;
            public string ToolTipText;

            public DBAppearance AppearancesSmall { get; set; }
        }

        SharedDef sharedProps = new SharedDef();
        public SharedDef SharedProps {
            get { return sharedProps; }
            set { sharedProps = value; }
        }
    }
}