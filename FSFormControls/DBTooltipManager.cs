using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBTooltipManager
    {
        private IContainer components;

        public DBTooltipManager(IContainer components)
        {
            this.components = components;
        }

        public int AutoPopDelay { get; set; }
        public bool Enabled { get; set; }
        public Control ContainingControl { get; set; }

        public void ShowToolTip(Control control)
        {
            DBTooltip tooltip = new DBTooltip();
            tooltip.SetToolTip(control, control.Text);
        }

        public void SetUltraToolTip(Control control, DBTooltip tooltip)
        {
            SetToolTip(control, tooltip);
        }

        public DBTooltip GetUltraToolTip(Control control)
        {
            return GetToolTip(control);
        }

        public DBTooltip GetToolTip(Control control)
        {
            DBTooltip tooltip = new DBTooltip(control.Text);
            return tooltip;
        }

        public void SetToolTip(Control control, DBTooltip tooltip)
        {
            tooltip.SetToolTip(control, tooltip.ToolTipText);
        }
    }
}