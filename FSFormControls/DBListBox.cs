#region

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBListBox.bmp")]
    [ToolboxItem(true)]
    public class DBListBox : DBUserControl
    {
        public bool isBinding;
        public Color m_BackColor = Global.NormalBackColor;
        public bool m_BlankSelection;
        public DBControl m_DBControl;
        public DBControl m_DBControlList;

        public string m_DBFieldData = "";
        public string m_DBFieldList;
        public bool m_Editable = true;
        public Global.AccessMode m_Mode = Global.AccessMode.WriteMode;
        public bool m_Obligatory;
        public string m_OrderBy;
        public object m_SelectedOption;
        public bool m_ShowCode;


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


        private string m_DBField;
        [Description("Campo de la base de datos a enlazar.")]
        public string DBField
        {
            get { return m_DBField; }
            set { m_DBField = value; }
        }

        public string DBFieldData
        {
            get { return m_DBFieldData; }
            set { m_DBFieldData = value; }
        }

        public bool Obligatory
        {
            get { return m_Obligatory; }
            set
            {
                m_Obligatory = value;
                if (value)
                    ListBox1.BackColor = Global.ObligatoryBackColor;
                else
                    ListBox1.BackColor = m_BackColor;
            }
        }

        public override Color BackColor
        {
            get { return m_BackColor; }
            set
            {
                m_BackColor = value;
                ListBox1.BackColor = m_BackColor;
            }
        }

        public string DBFieldList
        {
            get { return m_DBFieldList; }
            set { m_DBFieldList = value; }
        }

        public Global.AccessMode Mode
        {
            get
            {
                Global.AccessMode modeReturn = 0;
                modeReturn = m_Mode;
                return modeReturn;
            }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        ListBox1.Enabled = false;
                        ListBox1.BackColor = m_BackColor;
                        break;
                    case Global.AccessMode.WriteMode:
                        ListBox1.Enabled = true;
                        ListBox1.BackColor = m_BackColor;
                        break;
                    case Global.AccessMode.ProtectedMode:
                        ListBox1.BackColor = Global.ObligatoryBackColor;
                        break;
                }
            }
        }

        public ListBox.ObjectCollection Items
        {
            get { return ListBox1.Items; }
            set
            {
                var f = 0;
                for (f = 0; f <= value.Count - 1; f++) ListBox1.Items.Add(value[f]);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object SelectedOption
        {
            get { return m_SelectedOption; }
            set { m_SelectedOption = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int SelectedIndex
        {
            get { return ListBox1.SelectedIndex; }
            set { ListBox1.SelectedIndex = value; }
        }

        public bool BlankSelection
        {
            get { return m_BlankSelection; }
            set { m_BlankSelection = value; }
        }


        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object Selected
        {
            set { ListBox1.SelectedIndex = ListBox1.FindStringExact(Convert.ToString(value)); }
        }

        public DBControl DataControlList
        {
            get { return m_DBControlList; }
            set { m_DBControlList = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return ListBox1.Text; }
            set { ListBox1.Text = value; }
        }

        public bool ShowCode
        {
            get
            {
                var showCodeReturn = false;
                showCodeReturn = m_ShowCode;
                return showCodeReturn;
            }
            set { m_ShowCode = value; }
        }

        public string OrderBy
        {
            get
            {
                string orderByReturn = null;
                orderByReturn = m_OrderBy;
                return orderByReturn;
            }
            set { m_OrderBy = value; }
        }

        //public new ControlBindingsCollection DataBindings
        //{
        //    get { return ListBox1.DataBindings; }
        //}

        public bool Editable
        {
            get { return m_Editable; }
            set { m_Editable = value; }
        }

        public new event TextChangedEventHandler TextChanged;
        public event SelectedValueChangedEventHandler SelectedValueChanged;
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public new event VisibleChangedEventHandler VisibleChanged;
        public new event ValidatedEventHandler Validated;
        public new event LostFocusEventHandler LostFocus;
        public new event KeyDownEventHandler KeyDown;

        public string NameControl()
        {
            return /* TRANSERROR: was MyClass */ Name;
        }

        public void Fill()
        {
            if (isBinding) return;

            if (m_DBControlList == null) throw new ExceptionUtil("DataControlList, no especificado.");


            if (DBFieldList == null) throw new ExceptionUtil("Campo DBFieldList no especificado.");

            try
            {
                if (DataControlList.DataTable == null)
                {
                    if (DataControlList.Connected == false)
                    {
                        DataControlList.Connect();
                        ListBox1.DataSource = DataControlList.DataTable;
                    }
                }
                else
                {
                    ListBox1.DataSource = DataControlList.DataTable;
                }

                if (m_BlankSelection)
                {
                    DataRow rowblank = null;
                    rowblank = DataControlList.DataTable.NewRow();
                    rowblank[DBFieldList] = "";
                    DataControlList.DataTable.Rows.InsertAt(rowblank, 0);
                }

                if (string.IsNullOrEmpty(m_DBFieldData)) m_DBFieldData = DataControlList.FieldName(0);

                ListBox1.DisplayMember = DataControlList.FieldExactName(DBFieldList);
                ListBox1.ValueMember = DataControlList.FieldExactName(m_DBFieldData);

                if (!string.IsNullOrEmpty(m_DBField))
                    if (DataControl.DataTable != null)
                    {
                        ListBox1.DataBindings.Add("SelectedValue", DataControl.DataTable, DBField);
                        isBinding = true;
                    }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public int FindStringExact(string s)
        {
            return ListBox1.FindStringExact(s);
        }


        private void ListBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) DataControlList.ShowRecord();
            if (null != KeyDown) KeyDown(this, e);
            e.Handled = true;
        }


        private void ListBox1_TextChanged(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex != -1)
                if (null != TextChanged)
                    TextChanged(this, e);
        }


        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex != -1)
            {
                m_SelectedOption = ListBox1.SelectedValue.ToString();
                if (null != SelectedValueChanged) SelectedValueChanged(this, e);
            }
        }


        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != SelectedIndexChanged) SelectedIndexChanged(this, e);
        }


        private void ListBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (null != VisibleChanged) VisibleChanged(this, e);
        }


        private void ListBox1_Validated(object sender, EventArgs e)
        {
            if (null != Validated) Validated(this, e);
        }


        public void ListBox1_LostFocus(object sender, EventArgs e)
        {
            if (null != LostFocus) LostFocus(this, e);
        }


        private void ListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void ListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region Delegates

        public delegate void DropDownEventHandler(object sender, EventArgs e);

        public delegate void KeyDownEventHandler(object sender, KeyEventArgs e);

        public delegate void LostFocusEventHandler(object sender, EventArgs e);

        public delegate void SelectedIndexChangedEventHandler(object sender, EventArgs e);

        public delegate void SelectedValueChangedEventHandler(object sender, EventArgs e);

        public delegate void SelectionChangeCommittedEventHandler(object sender, EventArgs e);

        public delegate void TextChangedEventHandler(object sender, EventArgs e);

        public delegate void ValidatedEventHandler(object sender, EventArgs e);

        public delegate void VisibleChangedEventHandler(object sender, EventArgs e);

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal ListBox ListBox1;

        public DBListBox()
        {
            InitializeComponent();

            ListBox1.KeyDown += ListBox1_KeyDown;
            ListBox1.TextChanged += ListBox1_TextChanged;
            ListBox1.SelectedValueChanged += ListBox1_SelectedValueChanged;
            ListBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            ListBox1.VisibleChanged += ListBox1_VisibleChanged;
            ListBox1.Validated += ListBox1_Validated;
            ListBox1.LostFocus += ListBox1_LostFocus;
            ListBox1.MouseDown += ListBox1_MouseDown;
            ListBox1.MouseUp += ListBox1_MouseUp;
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
            ListBox1 = new ListBox();
            SuspendLayout();
            // 
            // ListBox1
            // 
            ListBox1.Dock = DockStyle.Fill;
            ListBox1.Location = new Point(0, 0);
            ListBox1.Name = "ListBox1";
            ListBox1.Size = new Size(152, 134);
            ListBox1.TabIndex = 0;
            // 
            // DBListBox
            // 
            Controls.Add(ListBox1);
            Name = "DBListBox";
            Size = new Size(152, 143);
            ResumeLayout(false);
        }

        #endregion
    }
}