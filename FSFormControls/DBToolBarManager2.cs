using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBarManager2 : DBToolBarContainer, ISupportInitialize
    {
        public DBToolBarManager2()
        {
            Dock = DockStyle.Top;

            //DBToolBar toolbar = new DBToolBar();
            //toolbar.ItemClicked += new ToolStripItemClickedEventHandler(Toolbar_ButtonClick);
            //toolbar.Items.Add("DBToolBar1");

            //toolbars.Add(this);

            //this.ItemClicked += new ToolStripItemClickedEventHandler(Toolbar_ButtonClick);
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
        public object Settings { get; internal set; }

        List<DBToolBar> toolbars = new List<DBToolBar>();

        /// <summary>
        /// Esta propiedad la utiliza infragistics para manejar multiples Toolbar.
        /// </summary>
        public List<DBToolBar> Toolbars
        {
            get
            {
                //List<DBToolBar> toolbar = new List<DBToolBar>();
                //toolbar.Add(this);
                DBToolBar toolbar = new DBToolBar();
                if (toolbars.Count == 0)
                    toolbars.Add(toolbar);
                return toolbars;
            }
            //set { toolbars = value; }
        }

        public ControlCollection Items
        {
            get
            {
                return TopToolStripPanel.Controls;
            }
        }


        public bool Exists(string name)
        {
            //if (toolbars.Exists(n => n.Name == name))
            //    return true;
            return false;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        //public ToolStripItemCollection Items
        //{
        //    get { return this.Items; }
        //}

        //public new bool Visible
        //{
        //    get { return base.Visible; }
        //    set { base.Visible = value; }
        //}

        public event ToolStripItemClickEventHandler ItemClick;
        public delegate void ToolStripItemClickEventHandler(object sender, ToolStripItemClickedEventArgs e);
    }
}