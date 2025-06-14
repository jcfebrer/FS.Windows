#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBTabControl.bmp")]
    [ToolboxItem(true)]
    public class DBTabControl : TabControl, ISupportInitialize
    {
        public enum DBTabControlStyle
        {
            Default,
            PropertyPage,
            PropertyPageSelected,
            PropertyPageFlat,
            PropertyPage2003,
            StateButtons,
            VisualStudio,
            Flat,
            Wizard,
            Excel,
            NotePage,
            NotePageFlat,
            VisualStudio2005,
            Office2007Ribbon
        }

        public delegate void SelectedTabChangedEventHandler(object sender, SelectedTabChangedEventArgs e);

        //protected override void OnControlAdded(ControlEventArgs e)
        //{
        //    base.OnControlAdded(e);
        //    TabPage tp = (TabPage) e.Control;
        //    if (TabPages.IndexOf(tp) != -1)
        //    {
        //        Rectangle rect = GetTabRect(TabPages.IndexOf(tp));
        //        Button btn = AddCloseButton(tp);
        //        btn.Size = new Size(rect.Height - 1, rect.Height - 1);
        //        btn.Location = new Point(rect.X + rect.Width - rect.Height - 1, rect.Y + 1);
        //        SetParent(btn.Handle, Handle);
        //        btn.Click += OnCloseButtonClick;
        //        CloseButtonCollection.Add(btn, tp);
        //    }
        //}

        //protected override void OnControlRemoved(ControlEventArgs e)
        //{
        //    Button btn = CloseButtonOfTabPage((TabPage) e.Control);
        //    btn.Click -= OnCloseButtonClick;
        //    CloseButtonCollection.Remove(btn);
        //    SetParent(btn.Handle, IntPtr.Zero);
        //    btn.Dispose();
        //    base.OnControlRemoved(e);
        //}

        //protected override void OnLayout(LayoutEventArgs levent)
        //{
        //    base.OnLayout(levent);
        //    RePositionCloseButtons();
        //}


        //protected virtual void OnCloseButtonClick(object sender, EventArgs e)
        //{
        //    if (!DesignMode)
        //    {
        //        Button btn = (Button) sender;
        //        TabPage tp = CloseButtonCollection[btn];

        //        CancelEventArgs ee = new CancelEventArgs();

        //        if (CloseButtonClick != null)
        //        {
        //            CloseButtonClick(sender, ee);
        //        }

        //        if (!ee.Cancel)
        //        {
        //            TabPages.Remove(tp);
        //            RePositionCloseButtons();
        //        }
        //    }
        //}

        //protected virtual Button AddCloseButton(TabPage tp)
        //{
        //    Button closeButton = new Button();

        //    closeButton.Text = "X";
        //    closeButton.FlatStyle = FlatStyle.Flat;
        //    closeButton.BackColor = Color.FromKnownColor(KnownColor.ButtonFace);
        //    closeButton.ForeColor = Color.White;
        //    closeButton.Font = new Font("Microsoft Sans Serif", 6, FontStyle.Bold);

        //    return closeButton;
        //}

        //public void RePositionCloseButtons()
        //{
        //    foreach (KeyValuePair<Button, TabPage> item in CloseButtonCollection)
        //    {
        //        RePositionCloseButtons(item.Value);
        //    }
        //}

        //private void RePositionCloseButtons(TabPage tp)
        //{
        //    Button btn = CloseButtonOfTabPage(tp);

        //    tp.Text = tp.Text.Trim() + "      ";

        //    if (btn != null)
        //    {
        //        int tpIndex = TabPages.IndexOf(tp);

        //        if (tpIndex >= 0)
        //        {
        //            Rectangle rect = GetTabRect(tpIndex);

        //            if (ReferenceEquals(SelectedTab, tp))
        //            {
        //                btn.BackColor = Color.LightGreen;
        //                btn.Size = new Size(rect.Height - 1, rect.Height - 1);
        //                btn.Location = new Point(rect.X + rect.Width - rect.Height, rect.Y + 1);
        //            }
        //            else
        //            {
        //                btn.BackColor = Color.FromKnownColor(KnownColor.ButtonFace);
        //                btn.Size = new Size(rect.Height - 3, rect.Height - 3);
        //                btn.Location = new Point(rect.X + rect.Width - rect.Height - 1, rect.Y + 1);
        //            }
        //            btn.Visible = ShowCloseButtonOnTabs;
        //            btn.BringToFront();
        //        }
        //    }
        //}

        //protected Button CloseButtonOfTabPage(TabPage tp)
        //{
        //    Button first = null;
        //    foreach (KeyValuePair<Button, TabPage> item in CloseButtonCollection)
        //    {
        //        if (first == null) first = item.Key;
        //        if (item.Value == tp) return item.Key;
        //    }
        //    return first;
        //}

        /// <summary>
        ///     Por defecto, si existe más de una pestaña, devolvemos la primera en caso de que SelectedTab sea null.
        /// </summary>
        public new DBTabPage SelectedTab
        {
            get
            {
                if (base.SelectedTab == null)
                    if (TabPages.Count > 0)
                        base.SelectedTab = TabPages[0];
                return (DBTabPage)base.SelectedTab;
            }
            set { base.SelectedTab = value; }
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public event SelectedTabChangedEventHandler SelectedTabChanged;

        //protected Dictionary<Button, TabPage> CloseButtonCollection = new Dictionary<Button, TabPage>();
        //private bool m_ShowCloseButtonOnTabs = false;


        //[Browsable(true), DefaultValue(true), Category("Behavior"),
        // Description("Indicates whether a close button should be shown on each TabPage")]
        //public bool ShowCloseButtonOnTabs
        //{
        //    get { return m_ShowCloseButtonOnTabs; }

        //    set
        //    {
        //        m_ShowCloseButtonOnTabs = value;

        //        foreach (Button btn in CloseButtonCollection.Keys)
        //        {
        //            btn.Visible = m_ShowCloseButtonOnTabs;
        //        }

        //        RePositionCloseButtons();
        //    }
        //}

        //protected override void OnCreateControl()
        //{
        //    base.OnCreateControl();
        //    RePositionCloseButtons();
        //}

        public DBTabPageCollection Tabs
        {
            get { return (DBTabPageCollection)base.TabPages; }
        }

        public DBTabControlStyle Style { get; set; }

        public DBTabPage SharedControlsPage { get; set; }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            if (SelectedTabChanged != null)
                SelectedTabChanged(this, new SelectedTabChangedEventArgs());
        }

        //public event CancelEventHandler CloseButtonClick;
        public class SelectedTabChangedEventArgs : EventArgs
        {
        }

        public string Key
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        public DBTabPage TabPage { get; set; }
    }
}