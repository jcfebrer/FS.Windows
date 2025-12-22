#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBStatusBar.bmp")]
    [ToolboxItem(true)]
    public class DBStatusBar : StatusStrip, ISupportInitialize
    {
        //private DBStatusBarPanelCollection m_Panels = new DBStatusBarPanelCollection();

        public DBStatusBar()
        {
            this.Name = "DBStatusBar1";
            this.Text = "DBStatusBar1";
            this.ShowItemToolTips = true;
            this.ViewStyle = ViewStyleEnum.Default;
            this.WrapText = false;

            //// Initialize the status bar with a default item "mensaje"
            //ToolStripItem toolStripItem = new ToolStripStatusLabel
            //{
            //    Name = "mensaje",
            //    Text = "Mensaje",
            //    AutoSize = false,
            //    Width = 200,
            //    Alignment = ToolStripItemAlignment.Right
            //};

            //this.Items.Add(toolStripItem);
        }

        public DBStatusBar(string name, string text) : this()
        {
            this.Key = name;
            this.Name = name;
            this.Text = text;
        }

        public enum ViewStyleEnum
        {
            Default
        }

        //public new DBStatusBarPanelCollection Panels
        //{
        //    get { return (DBStatusBarPanelCollection)base.Panels; }
        //}

        public ViewStyleEnum ViewStyle { get; set; }

        public bool WrapText { get; set; }

        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }

        //public string get_Text()
        //{
        //    return this.Items[2].Text;
        //}

        //public void set_Text(string Value)
        //{
        //    set_Text(0, Value);
        //}

        //public void set_Text(int Panel, string Value)
        //{
        //    if (Panel <= this.Items.Count)
        //    {
        //        this.Items[Panel].Text = Value;
        //    }
        //}

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}