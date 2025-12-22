#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBScroll.bmp")]
    [ToolboxItem(true)]
    public class DBScroll : DBUserControl
    {
        public int Value
        {
            get { return HScroll1.Value; }
            set
            {
                if (DataControl == null) return;
                try
                {
                    if (value != HScroll1.Value)
                    {
                        if (value <= HScroll1.Maximum)
                        {
                            HScroll1.Value = value;
                            DataControl.Go(HScroll1.Value);
                        }
                        else
                        {
                            HScroll1.Value = HScroll1.Maximum;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }
            }
        }

        public int Max
        {
            get
            {
                var maxReturn = 0;
                maxReturn = HScroll1.Maximum;
                return maxReturn;
            }
            set { HScroll1.Maximum = value; }
        }

        public int Min
        {
            get
            {
                var minReturn = 0;
                minReturn = HScroll1.Minimum;
                return minReturn;
            }
            set { HScroll1.Minimum = value; }
        }

        public event SaveButtonPressEventHandler SaveButtonPress;

        public event AddButtonPressEventHandler AddButtonPress;

        public event DeleteButtonPressEventHandler DeleteButtonPress;

        public event ListButtonPressEventHandler ListButtonPress;

        public event FirstButtonPressEventHandler FirstButtonPress;

        public event LastButtonPressEventHandler LastButtonPress;

        public event ChangeEventHandler Change;


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }

        public void Initialize()
        {
            Form frm = null;

            if (DataControl == null)
            {
                frm = FindForm().ActiveMdiChild;
                if (frm == null) frm = FindForm();
                DataControl = (DBControl) FunctionsForms.FindControlType(frm.Controls, "FSFormControls.DBControl");
            }

            if (DataControl == null) throw new ExceptionUtil("DataControl no especificado.");

            DataControl.ChangeRecord += m_dbcontrol_ChangeRecord;

            HScroll1.SmallChange = 1;
            HScroll1.Minimum = 0;
            HScroll1.Maximum = Convert.ToInt16(DataControl.RecordCount());
            HScroll1.LargeChange = 1;

            HScroll1.Value = Convert.ToInt16(DataControl.DBPosition);
            DataControl.Go(DataControl.DBPosition);

            PonReg();
        }


        private void cmdFirst_Click(object sender, EventArgs e)
        {
            HScroll1.Value = HScroll1.Minimum;
            if (null != FirstButtonPress) FirstButtonPress(this, e);
        }


        private void cmdLast_Click(object sender, EventArgs e)
        {
            HScroll1.Value = HScroll1.Maximum;
            if (null != LastButtonPress) LastButtonPress(this, e);
        }


        private void cmdRegistry_Click(object sender, EventArgs e)
        {
            var pos = 0;
            var ibr = InputBox.ShowDialog("Ir a:", "", "", InputBox.Icon.Question, InputBox.Buttons.Ok, InputBox.Type.TextBox);
            pos = Convert.ToInt32(InputBox.ResultValue);
            if (pos != 0)
                if ((pos >= HScroll1.Minimum) & (pos <= HScroll1.Maximum + 1))
                    HScroll1.Value = pos - 1;
        }


        private void HScroll1_ValueChanged(object sender, EventArgs e)
        {
            if (DataControl == null) return;
            if (HScroll1.Value != -1)
            {
                DataControl.Go(HScroll1.Value);
                PonReg();
            }

            if (null != Change) Change();
        }


        private void PonReg()
        {
            cmdRegistry.Text = HScroll1.Value + 1 + " / " + HScroll1.Maximum + 1;
        }


        private void cmdAdd_Click(object sender, EventArgs e)
        {
            DataControl.AddNew();
            if (null != AddButtonPress) AddButtonPress(this, e);
        }


        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DataControl.Delete();
            if (null != DeleteButtonPress) DeleteButtonPress(this, e);
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataControl.Save();
            if (null != SaveButtonPress) SaveButtonPress(this, e);
        }


        private void cmdList_Click(object sender, EventArgs e)
        {
            var frm = new frmListView();

            frm.DataControl = DataControl;

            frm.ShowDialog();
            frm.Close();
            frm = null;

            if (null != ListButtonPress) ListButtonPress(this, e);
        }


        public override void Refresh()
        {
            HScroll1.SmallChange = 1;
            HScroll1.LargeChange = 1;
            HScroll1.Minimum = 0;
            HScroll1.Maximum = Convert.ToInt16(DataControl.RecordCount());

            PonReg();
            base.Refresh();
        }


        private void m_dbcontrol_ChangeRecord()
        {
            PonReg();
        }
        //public new event ScrollEventHandler Scroll; 

        #region Delegates

        public delegate void AddButtonPressEventHandler(object sender, EventArgs e);

        public delegate void ChangeEventHandler();

        public delegate void ClickEventHandler();

        //public new event ClickEventHandler Click; 
        public delegate void DblClickEventHandler();

        public delegate void DeleteButtonPressEventHandler(object sender, EventArgs e);

        public delegate void FirstButtonPressEventHandler(object sender, EventArgs e);

        //public event DblClickEventHandler DblClick; 
        public delegate void KeyDownEventHandler(int KeyCode, int Shift);

        //public new event KeyDownEventHandler KeyDown; 
        public delegate void KeyPressEventHandler(int KeyAscii);

        //public new event KeyPressEventHandler KeyPress; 
        public delegate void KeyUpEventHandler(int KeyCode, int Shift);

        public delegate void LastButtonPressEventHandler(object sender, EventArgs e);

        public delegate void ListButtonPressEventHandler(object sender, EventArgs e);

        //public new event KeyUpEventHandler KeyUp; 
        public delegate void MouseDownEventHandler(int Button, int Shift, float x, float y);

        //public new event MouseDownEventHandler MouseDown; 
        public delegate void MouseMoveEventHandler(int Button, int Shift, float x, float y);

        //public new event MouseMoveEventHandler MouseMove; 
        public delegate void MouseUpEventHandler(int Button, int Shift, float x, float y);

        public delegate void NextButtonPressEventHandler(object sender, EventArgs e);

        public delegate void PreviousButtonPressEventHandler(object sender, EventArgs e);

        //public new event MouseUpEventHandler MouseUp; 
        public delegate void SaveButtonPressEventHandler(object sender, EventArgs e);

        public delegate void ScrollEventHandler();

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private HScrollBar HScroll1;
        internal ToolTip ToolTip1;
        private Button cmdAdd;
        private Button cmdDelete;
        private Button cmdFirst;
        private Button cmdLast;
        private Button cmdList;
        private Button cmdRegistry;
        private Button cmdSave;
        private IContainer components;
        internal ImageList imgToolbar;

        public DBScroll()
        {
            InitializeComponent();

            cmdRegistry.Click += cmdRegistry_Click;
            HScroll1.ValueChanged += HScroll1_ValueChanged;
            cmdAdd.Click += cmdAdd_Click;
            cmdDelete.Click += cmdDelete_Click;
            cmdSave.Click += cmdSave_Click;
            cmdList.Click += cmdList_Click;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            var resources = new ComponentResourceManager(typeof(DBScroll));
            cmdFirst = new Button();
            imgToolbar = new ImageList(components);
            cmdRegistry = new Button();
            HScroll1 = new HScrollBar();
            cmdLast = new Button();
            cmdAdd = new Button();
            cmdDelete = new Button();
            cmdList = new Button();
            cmdSave = new Button();
            ToolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // cmdFirst
            // 
            cmdFirst.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                               | AnchorStyles.Left;
            cmdFirst.ImageIndex = 6;
            cmdFirst.ImageList = imgToolbar;
            cmdFirst.Location = new Point(128, 0);
            cmdFirst.Name = "cmdFirst";
            cmdFirst.Size = new Size(32, 20);
            cmdFirst.TabIndex = 0;
            ToolTip1.SetToolTip(cmdFirst, "Primero");
            // 
            // imgToolbar
            // 
            imgToolbar.ImageStream = (ImageListStreamer) resources.GetObject("imgToolbar.ImageStream");
            imgToolbar.TransparentColor = Color.Transparent;
            imgToolbar.Images.SetKeyName(0, "");
            imgToolbar.Images.SetKeyName(1, "");
            imgToolbar.Images.SetKeyName(2, "");
            imgToolbar.Images.SetKeyName(3, "");
            imgToolbar.Images.SetKeyName(4, "");
            imgToolbar.Images.SetKeyName(5, "");
            imgToolbar.Images.SetKeyName(6, "");
            imgToolbar.Images.SetKeyName(7, "");
            // 
            // cmdRegistry
            // 
            cmdRegistry.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                  | AnchorStyles.Right;
            cmdRegistry.Location = new Point(404, 0);
            cmdRegistry.Name = "cmdRegistry";
            cmdRegistry.Size = new Size(88, 20);
            cmdRegistry.TabIndex = 1;
            cmdRegistry.Text = "0/0";
            ToolTip1.SetToolTip(cmdRegistry, "Ir A");
            // 
            // HScroll1
            // 
            HScroll1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                               | AnchorStyles.Left
                                               | AnchorStyles.Right;
            HScroll1.LargeChange = 1;
            HScroll1.Location = new Point(160, 0);
            HScroll1.Maximum = 1;
            HScroll1.Minimum = 1;
            HScroll1.Name = "HScroll1";
            HScroll1.Size = new Size(244, 20);
            HScroll1.TabIndex = 2;
            HScroll1.Value = 1;
            // 
            // cmdLast
            // 
            cmdLast.Dock = DockStyle.Right;
            cmdLast.ImageIndex = 7;
            cmdLast.ImageList = imgToolbar;
            cmdLast.Location = new Point(492, 0);
            cmdLast.Name = "cmdLast";
            cmdLast.Size = new Size(32, 20);
            cmdLast.TabIndex = 3;
            ToolTip1.SetToolTip(cmdLast, "?ltimo");
            // 
            // cmdAdd
            // 
            cmdAdd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                             | AnchorStyles.Left;
            cmdAdd.ImageIndex = 0;
            cmdAdd.ImageList = imgToolbar;
            cmdAdd.Location = new Point(0, 0);
            cmdAdd.Name = "cmdAdd";
            cmdAdd.Size = new Size(32, 20);
            cmdAdd.TabIndex = 4;
            ToolTip1.SetToolTip(cmdAdd, "A?adir");
            // 
            // cmdDelete
            // 
            cmdDelete.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                | AnchorStyles.Left;
            cmdDelete.ImageIndex = 1;
            cmdDelete.ImageList = imgToolbar;
            cmdDelete.Location = new Point(32, 0);
            cmdDelete.Name = "cmdDelete";
            cmdDelete.Size = new Size(32, 20);
            cmdDelete.TabIndex = 5;
            ToolTip1.SetToolTip(cmdDelete, "Borrar");
            // 
            // cmdList
            // 
            cmdList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                              | AnchorStyles.Left;
            cmdList.ImageIndex = 3;
            cmdList.ImageList = imgToolbar;
            cmdList.Location = new Point(96, 0);
            cmdList.Name = "cmdList";
            cmdList.Size = new Size(32, 20);
            cmdList.TabIndex = 6;
            ToolTip1.SetToolTip(cmdList, "Modo Lista");
            // 
            // cmdSave
            // 
            cmdSave.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                              | AnchorStyles.Left;
            cmdSave.ImageIndex = 2;
            cmdSave.ImageList = imgToolbar;
            cmdSave.Location = new Point(64, 0);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new Size(32, 20);
            cmdSave.TabIndex = 7;
            ToolTip1.SetToolTip(cmdSave, "Guardar");
            // 
            // DBScroll
            // 
            Controls.Add(cmdSave);
            Controls.Add(cmdList);
            Controls.Add(cmdDelete);
            Controls.Add(cmdAdd);
            Controls.Add(cmdLast);
            Controls.Add(HScroll1);
            Controls.Add(cmdRegistry);
            Controls.Add(cmdFirst);
            Name = "DBScroll";
            Size = new Size(524, 20);
            ResumeLayout(false);
        }

        #endregion
    }
}