using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBar : ToolStrip, ISupportInitialize
    {
        public DBToolBar()
        {
            Dock = DockStyle.Top;
        }

        public DBToolBar(string name)
        {
            this.Name = name;
            Dock = DockStyle.Top;
        }

        public object Settings { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}