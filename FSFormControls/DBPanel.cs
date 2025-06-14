#region

using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class DBPanel : Panel
    {
        #region Delegates

        public delegate void ScrollEventHandler(object sender);

        #endregion

        public const int WM_HSCROLL = 0X114;
        public const int WM_VSCROLL = 0X115;
        public new event ScrollEventHandler Scroll;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HSCROLL)
            {
                if (null != Scroll) Scroll(this);
            }
            else if (m.Msg == WM_VSCROLL)
            {
                if (null != Scroll) Scroll(this);
            }

            base.WndProc(ref m);
        }
    }
}