using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBStatusBarPanel : ToolStripStatusLabel, ISupportInitialize
    {
        public enum SizingModeEnum
        {
            Default,
            Spring
        }

        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }

        public SizingModeEnum SizingMode { get; set; }

        public new bool Visible { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}