using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxItem(true)]
    public class DBToolBarContainer : ToolStripContainer
    {
        public DockStyle DockedPosition
        {
            get { return this.Dock; }
            set { this.Dock = value; }
        }

        public DBToolBarManager ToolbarsManager { get; set; }
    }
}
