using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
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