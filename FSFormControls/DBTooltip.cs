using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxItem(true)]
    public class DBTooltip : ToolTip
    {
        public DBTooltip()
        {
        }

        public DBTooltip(string text)
        {
            ToolTipText = text;
        }

        public string ToolTipText { get; set; }
    }
}