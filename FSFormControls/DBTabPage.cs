#region

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBTabControl.bmp")]
    [ToolboxItem(true)]
    public class DBTabPage : TabPage
    {
        //protected override void OnTextChanged(EventArgs e)
        //{
        //    if (base.Parent is DBTabControl)
        //    {
        //        ((DBTabControl) (base.Parent)).RePositionCloseButtons();
        //    }
        //    base.OnTextChanged(e);
        //}


        /// <summary>
        /// Devuelve true si la pestaña esta seleccionada.
        /// </summary>
        public bool Active
        {
            get
            {
                DBTabControl dbTabControl = (DBTabControl)base.Parent;
                return (dbTabControl.SelectedTab == this);
            }
        }

        //public DBAppearance Appearance { get; set; }

        public string Key
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        //private string _Text;
        //public override string Text
        //{
        //    get { return _Text; }
        //    set { _Text = value; }
        //}

        public DBTabPage Tab
        {
            get { return this; }
        }

        public DBAppearance Appearance { get; set; }
    }
}