#region

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using FSDatabase;
using FSLibrary;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBCombo.bmp")]
    [DefaultEvent("SelectedValueChanged")]
    [Designer(typeof(DBComboControlDesigner))]
    [ToolboxItem(true)]
    public class DBCombo : DBUserControl, ISupportInitialize
    {
        public enum SortStyleEnum
        {
            Ascending,
            AscendingByValue,
            Descending,
            DescendingByValue,
            None
        }

        //public const int WM_ERASEBKGND = 0X14;
        //public const int WM_PAINT = 0XF;
        //public const int WM_NC_PAINT = 0X85;
        //public const int WM_PRINTCLIENT = 0X318;
        private Button cmdEdit;
        private bool doTextChanged = true;
        private bool filled;

        private DBControl m_DBControlList;
        private string m_DBFieldData = "";
        private string m_DBFieldList;
        private ComboBoxStyle m_DropDownStyle = ComboBoxStyle.DropDownList;
        private bool m_Editable = true;
        //private bool m_FlatMode;
        private ImageList m_ImageList = new ImageList();
        private DBComboValues m_Items;
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;
        private bool m_Obligatory;
        private object m_SelectedOption;
        private bool m_ShowEdit;
        private DBControl m_DataControl;
        private string m_DBField;



        #region Delegates

        public delegate void SelectionChangedEventHandler(object sender, EventArgs e);
        public delegate void DropDownEventHandler(object sender, EventArgs e);
        public delegate void EnterEventHandler(object sender, EventArgs e);
        public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);
        public delegate void LeaveEventHandler(object sender, EventArgs e);
        public delegate void LostFocusEventHandler(object sender, EventArgs e);
        public delegate void SelectedIndexChangedEventHandler(object sender, EventArgs e);
        public delegate void SelectedValueChangedEventHandler(object sender, EventArgs e);
        public delegate void SelectionChangeCommittedEventHandler(object sender, EventArgs e);
        public delegate void TextChangedEventHandler(object sender, EventArgs e);
        public delegate void ValidatedEventHandler(object sender, EventArgs e);
        public delegate void VisibleChangedEventHandler(object sender, EventArgs e);
        public delegate void MouseEnterElementEventHandler(object sender, DBEditorButtonEventArgs e);

        #endregion

        #region Events

        public event EventHandler SelectionChanged;
        public new event EventHandler TextChanged;
        public event SelectedValueChangedEventHandler SelectedValueChanged;
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public new event VisibleChangedEventHandler VisibleChanged;
        public new event ValidatedEventHandler Validated;
        public new event EventHandler Enter;
        public event DropDownEventHandler DropDown;
        public new event LostFocusEventHandler LostFocus;
        public new event EventHandler GotFocus;
        public new event EventHandler MouseEnter;
        public event SelectionChangeCommittedEventHandler SelectionChangeCommitted;
        public new event KeyEventHandler KeyDown;
        public new event LeaveEventHandler Leave;
        public new event KeyPressEventHandler KeyPress;
        public event EventHandler AfterEnterEditMode;
        public event EventHandler AfterExitEditMode;
        public event EventHandler ValueChanged;
        public event DBEditorButtonEventHandler EditorButtonClick;
        public event MouseEnterElementEventHandler MouseEnterElement;

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        private ComboBox combobox;

        public DBCombo()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            combobox.MouseUp += Combo1_MouseUp;
            combobox.VisibleChanged += Combo1_VisibleChanged;
            //Combo1.DrawItem += DrawItem;
            //Combo1.MeasureItem += MeasureItem;
            combobox.LostFocus += Combo1_LostFocus;
            combobox.SelectionChangeCommitted += Combo1_SelectionChangeCommitted;
            combobox.SelectedIndexChanged += Combo1_SelectedIndexChanged;
            combobox.Leave += Combo1_Leave;
            combobox.Enter += Combo1_Enter;
            combobox.MouseDown += Combo1_MouseDown;
            combobox.MouseEnter += Combo1_MouseEnter;
            combobox.KeyPress += Combo1_KeyPress;
            combobox.KeyUp += Combo1_KeyUp;
            combobox.SelectedValueChanged += Combo1_SelectedValueChanged;
            combobox.Validated += Combo1_Validated;
            combobox.KeyDown += Combo1_KeyDown;
            combobox.DropDown += Combo1_DropDown;
            combobox.GotFocus += Combo1_GotFocus;
            combobox.TextChanged += Combo1_TextChanged;

            Resize += Control_Resize;

            cmdEdit.Click += cmdEdit_Click;

            Load += DBCombo_Load;
        }

        private void DBCombo_Load(object sender, EventArgs e)
        {
            InitializeButtons();
        }

        private void Combo1_MouseEnter(object sender, EventArgs e)
        {
            if (MouseEnter != null)
                MouseEnter(sender, e);
        }

        private void Combo1_GotFocus(object sender, EventArgs e)
        {
            if (null != GotFocus)
                GotFocus(sender, e);

            if (null != AfterEnterEditMode)
                AfterEnterEditMode(sender, e);
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (DataControlList != null)
            {
                var frmR = new frmRecord();
                var dbc = new DBControl();
                dbc.Parent = frmR;
                dbc.Selection = DataControlList.Selection;
                dbc.TypeDB = DataControlList.TypeDB;
                dbc.DataTable = DataControlList.DataTable;
                dbc.DataSet = DataControlList.DataSet;
                dbc.DataView = DataControlList.DataView;

                frmR.DataControl = dbc;
                frmR.Show();
            }
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
            this.combobox = new System.Windows.Forms.ComboBox();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // combobox
            // 
            this.combobox.Location = new System.Drawing.Point(0, 0);
            this.combobox.Name = "combobox";
            this.combobox.Size = new System.Drawing.Size(123, 21);
            this.combobox.TabIndex = 1;
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(176, 0);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(16, 21);
            this.cmdEdit.TabIndex = 2;
            this.cmdEdit.Text = "E";
            // 
            // DBCombo
            // 
            this.About = null;
            this.Controls.Add(this.combobox);
            this.Controls.Add(this.cmdEdit);
            this.Name = "DBCombo";
            this.Size = new System.Drawing.Size(210, 29);
            this.ResumeLayout(false);

        }

        public void BeginInit()
        {
            //((ISupportInitialize)combobox).BeginInit();
        }

        public void EndInit()
        {
            //((ISupportInitialize)combobox).EndInit();
        }

        #endregion

        
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }

        [Description("Campo de la base de datos a enlazar.")]
        public string DBField
        {
            get { return m_DBField; }
            set { m_DBField = value; }
        }

        public bool GridMode { get; set; } = false;

        //public bool FlatMode
        //{
        //    get { return m_FlatMode; }
        //    set
        //    {
        //        m_FlatMode = value;
        //        Invalidate();
        //    }
        //}

        public string DBFieldData
        {
            get
            {
                return m_DBFieldData;
            }
            set
            {
                m_DBFieldData = value;
                combobox.ValueMember = m_DBFieldData;
            }
        }

        public AutoCompleteMode AutoCompleteMode
        {
            get { return combobox.AutoCompleteMode; }
            set {
                if (combobox.DropDownStyle == ComboBoxStyle.DropDownList && combobox.AutoCompleteSource != AutoCompleteSource.ListItems)
                    combobox.AutoCompleteMode = AutoCompleteMode.None;
                else
                    combobox.AutoCompleteMode = value;
            }
        }

        public bool Obligatory
        {
            get { return m_Obligatory; }
            set
            {
                m_Obligatory = value;
                if (value) combobox.BackColor = Global.ObligatoryBackColor;
            }
        }

        [Description("Modo lectura")]
        public bool ReadOnly
        {
            get { return m_Mode == Global.AccessMode.ReadMode; }
            set { Mode = value ? Global.AccessMode.ReadMode : Global.AccessMode.WriteMode; }
        }

        public ComboBoxStyle DropDownStyle
        {
            get { return m_DropDownStyle; }
            set
            {
                m_DropDownStyle = value;
                combobox.DropDownStyle = m_DropDownStyle;
            }
        }

        public DBAppearance Appearance { get; set; }

        public bool IsInEditMode
        {
            get { return m_Mode == Global.AccessMode.WriteMode; }
            set { m_Mode = value ? Global.AccessMode.WriteMode : Global.AccessMode.ReadMode; }
        }

        //public int SelectionStart
        //{
        //    get
        //    {
        //        return Combo1.SelectionStart;
        //    }
        //    set
        //    {
        //        Combo1.SelectionStart = value;
        //    }
        //}

        //public int SelectionLength
        //{
        //    get
        //    {
        //        return Combo1.SelectionLength;
        //    }
        //    set
        //    {
        //        Combo1.SelectionStart = 0;
        //        Combo1.SelectionLength = value;
        //    }
        //}

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string ValueMember
        {
            get { return combobox.ValueMember; }
            set
            {
                doTextChanged = false;
                combobox.ValueMember = value;
                doTextChanged = true;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string DisplayMember
        {
            get { return combobox.DisplayMember; }
            set
            {
                doTextChanged = false;
                combobox.DisplayMember = value;
                doTextChanged = true;
            }
        }

        public ImageList ImageList
        {
            get { return m_ImageList; }
            set
            {
                m_ImageList = value;
                DrawModeVariable();
            }
        }

        public override Color BackColor
        {
            get { return combobox.BackColor; }
            set { combobox.BackColor = value; }
        }

        public bool Sort { get; set; } = true;

        public string DBFieldList
        {
            get { return m_DBFieldList; }
            set
            {
                m_DBFieldList = value;
                combobox.DisplayMember = m_DBFieldList;
            }
        }

        public SortStyleEnum SortStyle { get; set; }

        public Global.AccessMode Mode
        {
            get
            {
                return m_Mode;
            }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        combobox.Enabled = false;
                        combobox.BackColor = Global.NormalBackColor;
                        cmdEdit.Visible = false;
                        break;
                    case Global.AccessMode.WriteMode:
                        combobox.Enabled = true;
                        combobox.BackColor = Global.WriteBackColor;
                        cmdEdit.Visible = true;
                        //Combo1.BringToFront();
                        break;
                    case Global.AccessMode.ProtectedMode:
                        combobox.Enabled = true;
                        combobox.BackColor = Global.ObligatoryBackColor;
                        cmdEdit.Visible = true;
                        //Combo1.BringToFront();
                        break;
                }
            }
        }

        public DBComboValues Items
        {
            get
            {
                if (m_Items == null)
                    m_Items = new DBComboValues(combobox);

                return m_Items;
            }
            set
            {
                m_Items = value;
                if (value != null)
                    foreach (DBComboboxItem v in value)
                        combobox.Items.Add(v);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object SelectedOption
        {
            get { return m_SelectedOption; }
            set
            {
                m_SelectedOption = value;
                combobox.SelectedValue = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int SelectedIndex
        {
            get { return combobox.SelectedIndex; }
            set { combobox.SelectedIndex = value; }
        }

        public bool BlankSelection { get; set; }

        public bool DroppedDown
        {
            get { return combobox.DroppedDown; }
            set { combobox.DroppedDown = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object Selected
        {
            set { combobox.SelectedIndex = combobox.FindStringExact(Convert.ToString(value)); }
        }

        public DBControl DataControlList
        {
            get
            {
                return m_DBControlList;
            }
            set
            {
                m_DBControlList = value;
                //if (!DesignMode)
                //{
                //    if (m_DBControlList != null)
                //        Fill();
                //}
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return combobox.Text; }
            set { combobox.Text = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object Value
        {
            get { return combobox.SelectedValue; }
            set
            {
                if (value == null)
                    return;

                DBComboboxItem dbitem = FindByValue(value.ToString());
                if (dbitem != null)
                    combobox.Text = dbitem.Text;
            }
        }

        public bool ShowCode { get; set; }

        public string OrderBy { get; set; }

        public bool Editable
        {
            get { return m_Editable; }
            set
            {
                m_Editable = value;
                combobox.Enabled = !m_Editable;
                if (m_Editable)
                {
                    if (!m_Obligatory) 
                        combobox.BackColor = Global.NormalBackColor;
                    combobox.TabStop = true;
                }
                else
                {
                    combobox.BackColor = Global.DisableBackColor;
                    combobox.TabStop = false;
                }
            }
        }


        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object SelectedValue
        {
            get { return combobox.SelectedValue; }
            set { combobox.SelectedValue = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object SelectedItem
        {
            get { return combobox.SelectedItem; }
            set { combobox.SelectedItem = value; }
        }


        public new ControlBindingsCollection DataBindings => combobox.DataBindings;

        public bool ShowEdit
        {
            get { return m_ShowEdit; }
            set
            {
                m_ShowEdit = value;
                cmdEdit.Visible = m_ShowEdit;
            }
        }

        public object DataSource
        {
            get { return combobox.DataSource; }
            set { combobox.DataSource = value; }
        }

        public override Color ForeColor
        {
            get { return combobox.ForeColor; }

            set { combobox.ForeColor = value; }
        }

        public int SelectionStart {
            get { return combobox.SelectionStart; }
            set { combobox.SelectionStart = value; }
        }

        public int SelectionLength {
            get { return combobox.SelectionLength; }
            set { combobox.SelectionLength = value; }
        }

        public string SelectedText
        {
            get { return combobox.SelectedText; }
            set { combobox.SelectedText = value; }
        }

        public DBButtonCollection ButtonsRight { get; set; } = new DBButtonCollection();

        public DBButtonCollection ButtonsLeft { get; set; } = new DBButtonCollection();

        public DBButtonCollection ClickedItemsLeft 
        {
            get { return ButtonsLeft; }
            set { ButtonsLeft = value; } 
        }

        public bool IsItemInList()
        {
            if (combobox.Items.Contains(combobox.SelectedText))
                return true;
            return false;
        }

        public string NameControl()
        {
            return Name;
        }

        public void Fill()
        {
            if (filled)
            {
                return;
            }

            if (m_DBControlList == null) return;
            //throw new ExceptionUtil("DataControlList, no especificado.");

            try
            {
                if (DataControlList.DataTable == null)
                {
                    if (DataControlList.Connected == false)
                    {
                        DataControlList.Connect();
                        if (DataControlList.Connected == false)
                            throw new ExceptionUtil("Imposible conectar DataControlList.");

                        doTextChanged = false;
                        combobox.DataSource = DataControlList.DataTable;
                        doTextChanged = true;
                    }
                }
                else
                {
                    doTextChanged = false;
                    combobox.DataSource = DataControlList.DataTable;
                    doTextChanged = true;
                }

                if (DataControlList.TypeDB == DBControl.DbType.Odbc ||
                    DataControlList.TypeDB == DBControl.DbType.OleDB ||
                    DataControlList.TypeDB == DBControl.DbType.SQLServer)
                {
                    var db = new BdUtils(Global.ConnectionString);

                    if (DBFieldList == null)
                        throw new ExceptionUtil("Campo DBFieldList no especificado.");

                    if (db.GetField(DBFieldList, DataControlList.TableName).Tipo == Utils.FieldTypeEnum.String)
                        if (BlankSelection)
                        {
                            DataRow rowblank = null;
                            rowblank = DataControlList.DataTable.NewRow();
                            rowblank[DBFieldList] = "";
                            DataControlList.DataTable.Rows.InsertAt(rowblank, 0);
                        }
                }

                if (Sort)
                {
                    if (!string.IsNullOrEmpty(OrderBy))
                    {
                        DataControlList.DataTable.DefaultView.Sort = OrderBy;
                    }
                    else
                    {
                        if (!(TextUtil.IndexOf(DataControlList.Selection, "order by") > 0))
                            DataControlList.DataTable.DefaultView.Sort = DBFieldList;
                    }
                }

                if (string.IsNullOrEmpty(m_DBFieldData))
                    m_DBFieldData = DataControlList.FieldName(0);

                if (string.IsNullOrEmpty(combobox.DisplayMember))
                    combobox.DisplayMember = DataControlList.FieldExactName(DBFieldList);
                if(string.IsNullOrEmpty(combobox.ValueMember))
                    combobox.ValueMember = DataControlList.FieldExactName(m_DBFieldData);


                if (!string.IsNullOrEmpty(DBField))
                    if (DataControl == null)
                        throw new ExceptionUtil("DataControl, no especificado.");
                //else
                //{
                //    if (!(DataControl.DataTable == null))
                //    {
                //        if (DropDownStyle == ComboBoxStyle.DropDown)
                //        {
                //            Combo1.DataBindings.Add("Text", DataControl.DataTable, DBField);
                //        }
                //        else
                //        {
                //            Combo1.DataBindings.Add("SelectedValue", DataControl.DataTable, DBField);
                //        }

                //        isBinding = true;
                //    }
                //}

                //Combo1.Select(0, 0); //deselecionamos el primer elemento
                //Combo1.SelectionLength = 0;

                filled = true;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e.Message);
            }
        }

        private void DrawModeVariable()
        {
            if (ImageList != null)
                if (ImageList.Images.Count > 0)
                    combobox.DrawMode = DrawMode.OwnerDrawVariable;
        }

        private void Combo1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.ControlKey)
                    if (DataControlList != null)
                        DataControlList.ShowRecord();

                e.Handled = true;
                if (null != KeyDown) 
                    KeyDown(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        private void Control_Resize(object sender, EventArgs e)
        {
            combobox.Location = new Point(0, 0);
            combobox.Size = new Size(this.Width - (16 * ButtonsRight.Count), combobox.Height);
            this.Height = combobox.Height;

            ResizeButtons();
        }

        public void BeginUpdate()
        {
            combobox.BeginUpdate();
        }


        public void EndUpdate()
        {
            combobox.EndUpdate();
        }


        public int FindString(string s)
        {
            return combobox.FindString(s);
        }


        public int FindStringExact(string s)
        {
            return combobox.FindStringExact(s);
        }

        public DBComboboxItem FindByValue(string value)
        {
            if (Items != null)
            {
                foreach (DBComboboxItem dbcol in Items)
                    if (Functions.Value(dbcol.Value).ToLower() == value.ToLower())
                        return dbcol;
            }
            else
            {
                if (m_DBControlList != null)
                    foreach (DataRow row in m_DBControlList.DataTable.Rows)
                        if (row[combobox.ValueMember].ToString().ToLower() == value.ToLower())
                            return new DBComboboxItem(row[combobox.ValueMember].ToString(),
                                row[combobox.DisplayMember].ToString());
            }

            return null;
        }

        private void Combo1_TextChanged(object sender, EventArgs e)
        {
            if (filled & doTextChanged)
                if (null != TextChanged)
                    TextChanged(this, e);
        }


        private void Combo1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (combobox.SelectedIndex != -1 && !GridMode)
                if (DataControl != null && DBField != "")
                    if (DataControl.Connected)
                        if (combobox.SelectedItem is DataRowView)
                        {
                            m_SelectedOption = ((DataRowView) combobox.SelectedItem).Row[0];

                            DataControl.SetField(DBField, m_SelectedOption.ToString());
                        }


            if (doTextChanged)
            {
                if (null != SelectedValueChanged)
                    SelectedValueChanged(this, e);

                if (null != SelectionChanged)
                    SelectionChanged(this, e);

                if (null != ValueChanged)
                    ValueChanged(this, e);
            }
        }


        private void Combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != SelectedIndexChanged)
                SelectedIndexChanged(this, e);
        }


        private void Combo1_VisibleChanged(object sender, EventArgs e)
        {
            if (null != VisibleChanged)
                VisibleChanged(this, e);
        }


        private void Combo1_Validated(object sender, EventArgs e)
        {
            if (null != Validated)
                Validated(this, e);
        }


        private void Combo1_DropDown(object sender, EventArgs e)
        {
            Invalidate();
            if (null != DropDown)
                DropDown(this, e);
        }


        public void Combo1_LostFocus(object sender, EventArgs e)
        {
            if (null != LostFocus)
                LostFocus(this, e);

            if (null != AfterExitEditMode)
                AfterExitEditMode(this, e);
        }


        private void Combo1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (null != SelectionChangeCommitted)
                SelectionChangeCommitted(this, e);
        }


        private void Combo1_Leave(object sender, EventArgs e)
        {
            AutoCompleteCombo_Leave();
            if (null != Leave)
                Leave(this, e);
        }

        public void AutoCompleteCombo_KeyUp(KeyEventArgs e)
        {
            string sTypedText = null;
            var iFoundIndex = 0;
            object oFoundItem = null;
            string sFoundText = null;
            string sAppendText = null;

            switch (e.KeyCode)
            {
                case Keys.Back:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Delete:
                case Keys.Down:
                    return;
            }


            sTypedText = combobox.Text;
            iFoundIndex = combobox.FindString(sTypedText);

            if (iFoundIndex >= 0)
            {
                oFoundItem = combobox.Items[iFoundIndex];

                sFoundText = combobox.GetItemText(oFoundItem);

                sAppendText = sFoundText.Substring(sTypedText.Length);
                combobox.Text = sTypedText + sAppendText;

                combobox.SelectionStart = sTypedText.Length;
                combobox.SelectionLength = sAppendText.Length;
            }
        }


        public void AutoCompleteCombo_Leave()
        {
            var iFoundIndex = 0;
            iFoundIndex = FindStringExact(combobox.Text);
            SelectedIndex = iFoundIndex;
        }


        private void Combo1_KeyUp(object sender, KeyEventArgs e)
        {
            AutoCompleteCombo_KeyUp(e);
            e.Handled = true;
        }

        private void Combo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (null != KeyPress) 
                KeyPress(this, e);
        }

        private void Combo1_Enter(object sender, EventArgs e)
        {
            //Combo1.SelectAll();
            if (null != Enter)
                Enter(this, e);
        }

        //protected override void WndProc(ref Message m)
        //{
        //    if ((DropDownStyle == ComboBoxStyle.Simple) | (FlatMode == false))
        //    {
        //        base.WndProc(ref m);
        //        return;
        //    }

        //    var hDC = Win32API.GetWindowDC(Handle);
        //    var gdc = Graphics.FromHdc(hDC);
        //    switch (m.Msg)
        //    {
        //        case WM_NC_PAINT:
        //            Win32API.SendMessage(Handle, WM_ERASEBKGND, hDC, 0);
        //            SendPrintClientMsg();
        //            PaintFlatControlBorder(this, gdc);
        //            m.Result = new IntPtr(1);
        //            break;
        //        case WM_PAINT:
        //            base.WndProc(ref m);
        //            var c = Enabled ? BackColor : SystemColors.Control;
        //            var p = new Pen(c, 2);
        //            gdc.DrawRectangle(p, new Rectangle(2, 2, Width - 3, Height - 3));
        //            PaintFlatDropDown(this, gdc);
        //            PaintFlatControlBorder(this, gdc);
        //            break;
        //        default:
        //            base.WndProc(ref m);
        //            break;
        //    }

        //    Win32API.ReleaseDC(m.HWnd, hDC);
        //    gdc.Dispose();
        //}

        //private void SendPrintClientMsg()
        //{
        //    var gClient = CreateGraphics();
        //    var ptrClientDC = gClient.GetHdc();
        //    Win32API.SendMessage(Handle, WM_PRINTCLIENT, ptrClientDC, 0);
        //    gClient.ReleaseHdc(ptrClientDC);
        //    gClient.Dispose();
        //}

        //private void PaintFlatControlBorder(Control ctrl, Graphics g)
        //{
        //    var rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
        //    if ((ctrl.Focused == false) | (ctrl.Enabled == false))
        //        ControlPaint.DrawBorder(g, rect, SystemColors.ControlDark, ButtonBorderStyle.Solid);
        //    else
        //        ControlPaint.DrawBorder(g, rect, Color.Black, ButtonBorderStyle.Solid);
        //}

        //public void PaintFlatDropDown(Control ctrl, Graphics g)
        //{
        //    const int DROPDOWNWIDTH = 18;
        //    var rect = new Rectangle(ctrl.Width - DROPDOWNWIDTH, 0, DROPDOWNWIDTH, ctrl.Height);
        //    ControlPaint.DrawComboButton(g, rect, ButtonState.Flat);
        //}

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    Invalidate();
        //}

        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    Invalidate();
        //}

        //protected override void OnGotFocus(EventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    Invalidate();
        //}

        //protected override void OnParentChanged(EventArgs e)
        //{
        //    base.OnParentChanged(e);
        //    Invalidate();
        //}

        private void Combo1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void Combo1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        //public void DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    if (ImageList.Images.Count == 0) return;
        //    if ((e.Index >= ImageList.Images.Count) & !BlankSelection) return;

        //    if (e.Index < 0) return;

        //    var str = Convert.ToString(Items[e.Index]);
        //    var im = e.Index;

        //    //if (sender is ComboBox)
        //    //{
        //    //    im =
        //    //        Convert.ToInt32(
        //    //            Functions.CDbl2(((DataRowView) (((ComboBox) sender).Items[e.Index]))[m_DBFieldData]));
        //    //    str = Convert.ToString(((DataRowView) (((ComboBox) sender).Items[e.Index]))[m_DBFieldList]);
        //    //}
        //    //else
        //    //{
        //    //    ComboBoxIconItem item = new ComboBoxIconItem();
        //    //    str = Convert.ToString(Items[e.Index]);
        //    //}

        //    var g = e.Graphics;
        //    var bBlue = new SolidBrush(Color.Blue);
        //    var bWhite = new SolidBrush(Color.White);


        //    float hgt = 0;

        //    if (im >= ImageList.Images.Count) im = ImageList.Images.Count - 1;
        //    if (im < 0) im = 0;

        //    var myImage = ImageList.Images[im];

        //    if (Combo1.DrawMode == DrawMode.OwnerDrawFixed)
        //    {
        //        hgt =
        //            Convert.ToSingle(Math.Max(myImage.Height,
        //                                 Convert.ToInt32(e.Graphics.MeasureString(str, Font).Height)) *
        //                             1.1);
        //        Combo1.ItemHeight = Convert.ToInt32(hgt);
        //    }

        //    var bHighlightItem = ShouldIHighlight(e.State);

        //    if (bHighlightItem)
        //    {
        //        g.FillRectangle(bBlue, e.Bounds);
        //        if (!((e.Index == 0) & BlankSelection))
        //            g.DrawImage(myImage, 5, e.Bounds.Top + (e.Bounds.Height - myImage.Height) / 2);
        //        g.DrawString(str, Font, bWhite, e.Bounds.Left + myImage.Width + 5, e.Bounds.Top);
        //    }
        //    else
        //    {
        //        g.FillRectangle(bWhite, e.Bounds);
        //        if (!((e.Index == 0) & BlankSelection))
        //            g.DrawImage(myImage, 5, e.Bounds.Top + (e.Bounds.Height - myImage.Height) / 2);
        //        g.DrawString(str, Font, bBlue, e.Bounds.Left + myImage.Width + 5, e.Bounds.Top);
        //    }

        //    bBlue.Dispose();
        //    bWhite.Dispose();
        //    bBlue = null;
        //    bWhite = null;
        //}


        //private bool ShouldIHighlight(DrawItemState State)
        //{
        //    var OSver = Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor;
        //    if (OSver == "5.0")
        //    {
        //        if ((State == (DrawItemState.Selected | DrawItemState.NoFocusRect | DrawItemState.NoAccelerator)) |
        //            (State == (DrawItemState.Selected | DrawItemState.NoFocusRect | DrawItemState.NoAccelerator)) |
        //            (State ==
        //             (DrawItemState.Selected | DrawItemState.Focus | DrawItemState.NoAccelerator |
        //              DrawItemState.NoFocusRect)) | (State == DrawItemState.Selected))
        //            return true;
        //    }
        //    else if (OSver == "5.1")
        //    {
        //        if ((State == (DrawItemState.Selected | DrawItemState.Focus)) | (State == DrawItemState.Selected) |
        //            (State == DrawItemState.Focus))
        //            return true;
        //    }

        //    return false;
        //}


        //private void MeasureItem(object sender, MeasureItemEventArgs e)
        //{
        //    if (ImageList.Images.Count == 0) return;
        //    if ((e.Index >= ImageList.Images.Count) & !BlankSelection) return;

        //    if (e.Index < 0) return;

        //    string str = null;
        //    var im = 0;

        //    if (sender is DBCombo)
        //    {
        //        im =
        //            Convert.ToInt32(
        //                Functions.CDbl2(((DataRowView) ((ComboBox) sender).Items[e.Index])[m_DBFieldData]));
        //        str = Convert.ToString(((DataRowView) ((ComboBox) sender).Items[e.Index])[m_DBFieldList]);
        //    }
        //    else
        //    {
        //        var item = new ComboBoxIconItem();
        //        str = Convert.ToString(Items[e.Index]);
        //    }

        //    object[] transTemp5 = str.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //    var strOneLine = string.Join("", (string[]) transTemp5);

        //    if (im >= ImageList.Images.Count) im = ImageList.Images.Count - 1;
        //    if (im < 0) im = 0;

        //    var img = ImageList.Images[im];

        //    var hgt =
        //        Convert.ToSingle(
        //            Math.Max(img.Height, Convert.ToInt32(e.Graphics.MeasureString(str, Combo1.Font).Height)) * 1.1);
        //    e.ItemHeight = Convert.ToInt32(hgt);

        //    if (Combo1.DropDownStyle == ComboBoxStyle.DropDownList)
        //        Combo1.ItemHeight = Convert.ToInt32(hgt);
        //    else
        //        Combo1.ItemHeight = Convert.ToInt32(e.Graphics.MeasureString(strOneLine, Combo1.Font).Height * 1.1);

        //    e.ItemWidth = Combo1.Width;
        //}

        private void InitializeButtons()
        {
            if (ButtonsRight != null && ButtonsRight.Count > 0)
            {
                foreach (DBButton button in ButtonsRight)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.Width = 16;
                    button.Height = 16;
                    button.Visible = true;
                    button.Top = 0;
                    button.Click += Button_Click;
                    button.ToolTip = button.Text;
                    button.MouseEnter += Button_MouseEnter;

                    button.BringToFront();
                }
            }

            if (ButtonsLeft != null && ButtonsLeft.Count > 0)
            {
                foreach (DBButton button in ButtonsLeft)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.Width = 16;
                    button.Height = 16;
                    button.Visible = true;
                    button.Top = 0;
                    button.Click += Button_Click;
                    button.ToolTip = button.Text;
                    button.MouseEnter += Button_MouseEnter;

                    button.BringToFront();
                }
            }
        }

        private void ResizeButtons()
        {
            int r = 1;
            if (ButtonsRight != null && ButtonsRight.Count > 0)
            {
                foreach (DBButton button in ButtonsRight)
                {
                    button.Left = this.Width - 16 * r;

                    r++;
                }
            }

            int l = 0;

            if (ButtonsLeft != null && ButtonsLeft.Count > 0)
            {
                foreach (DBButton button in ButtonsLeft)
                {
                    button.Left = l * 16;

                    l++;
                }
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (null != MouseEnterElement)
                MouseEnterElement(this, new DBEditorButtonEventArgs());
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (DBButton) sender;

            if (EditorButtonClick != null)
                EditorButtonClick(sender, new DBEditorButtonEventArgs());
        }

        public void SetDataBinding(ArrayList dataSource, string valueMember)
        {
            if (!String.IsNullOrEmpty(valueMember))
                combobox.ValueMember = valueMember;

            combobox.DataSource = dataSource;
        }

        public void SetDataBinding(ArrayList dataSource, string valueMember, string displayMember)
        {
            if (!String.IsNullOrEmpty(valueMember))
                combobox.ValueMember = valueMember;

            if (!String.IsNullOrEmpty(displayMember))
                combobox.DisplayMember = displayMember;

            combobox.DataSource = dataSource;
        }
    }


    internal class DBComboControlDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules =>
            SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable |
            SelectionRules.Visible;
    }


    public class ComboBoxIconItem
    {
        public ComboBoxIconItem()
        {
            Text = "";
        }

        public ComboBoxIconItem(string text)
        {
            Text = text;
        }

        public ComboBoxIconItem(string text, int imageIndex)
        {
            Text = text;
            ImageIndex = imageIndex;
        }

        public string Text { get; set; }

        public int ImageIndex { get; set; }


        public override string ToString()
        {
            return Text;
        }
    }
}