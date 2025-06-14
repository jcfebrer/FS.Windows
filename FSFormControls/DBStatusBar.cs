#region

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