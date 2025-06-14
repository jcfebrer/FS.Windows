using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBarManager : ISupportInitialize
    {
        public DBToolBarManager()
        {
            DBToolBar toolbar = new DBToolBar();
            toolbar.ItemClicked += new ToolStripItemClickedEventHandler(Toolbar_ButtonClick);

            toolbars.Add(toolbar);
        }

        private void Toolbar_ButtonClick(object sender, ToolStripItemClickedEventArgs e)
        {
            if (this.ItemClick != null)
            {
                this.ItemClick(sender, e);
            }
        }

        public enum DBRuntimeCustomizationOptions
        {
            None = 0,
            AllowCustomizeDialog = 1,
            AllowAltClickToolDragging = 2,
            AllowToolbarLocking = 4,
            AllowImageEditing = 8,
            All = -1
        }

        public int DesignerFlags { get; set; }
        public object DockWithinContainer { get; set; }
        public bool LockToolbars { get; set; }
        public bool MdiMergeable { get; set; }
        public DBRuntimeCustomizationOptions RuntimeCustomizationOptions { get; set; }
        public bool ShowShortcutsInToolTips { get; set; }
        List<DBToolBar> toolbars = new List<DBToolBar>();

        /// <summary>
        /// Esta propiedad la utiliza infragistics para manejar multiples Toolbar.
        /// </summary>
        public List<DBToolBar> Toolbars
        {
            get { return toolbars; }
            set { toolbars = value; }
        }

        public bool Exists(string name)
        {
            if (toolbars.Exists(n => n.Name == name))
                return true;
            return false;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public ToolStripItemCollection Items
        {
            get { return toolbars[0].Items; }
        }
        
        public bool Visible {
            get { return toolbars[0].Visible; }
            set { toolbars[0].Visible = value; }
        }

        public event ToolStripItemClickEventHandler ItemClick;
        public delegate void ToolStripItemClickEventHandler(object sender, ToolStripItemClickedEventArgs e);
    }
}