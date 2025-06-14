#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSDatabase;
using FSLibrary;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBFindTextBox.bmp")]
    [ToolboxItem(true)]
    public class DBFindTextBox : DBUserControl
    {
        public Color m_BackColor = Global.NormalBackColor;
        public DBTextBox.TypeData m_DataType = DBTextBox.TypeData.All;

        public DBControl m_DBControl;
        public DBControl m_DBControlList;
        public string m_DBFieldData;
        public bool m_DisableButton;
        public bool m_Editable = true;
        public double m_MaxValue;
        public Global.AccessMode m_Mode;
        public bool m_Obligatory;
        public bool m_ShowSelectForm = true;


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

        public bool Obligatory
        {
            get { return m_Obligatory; }
            set
            {
                m_Obligatory = value;
                if (value)
                    DbTextBox1.BackColor = Global.ObligatoryBackColor;
                else
                    DbTextBox1.BackColor = m_BackColor;
            }
        }

        public DBControl DataControlList
        {
            get { return m_DBControlList; }
            set { m_DBControlList = value; }
        }

        public string MaskInput
        {
            get { return DbTextBox1.MaskInput; }
            set { DbTextBox1.MaskInput = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return DbTextBox1.Text; }
            set { DbTextBox1.Text = value; }
        }

        [Description("Indicamos si el control es editable o no.")]
        public bool Editable
        {
            get { return m_Editable; }
            set
            {
                m_Editable = value;
                DbTextBox1.Editable = m_Editable;
            }
        }

        public bool DisableButton
        {
            get { return m_DisableButton; }
            set { m_DisableButton = value; }
        }

        public string DBFieldData
        {
            get { return m_DBFieldData; }
            set { m_DBFieldData = value; }
        }

        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        DbTextBox1.Mode = Global.AccessMode.ReadMode;
                        break;
                    case Global.AccessMode.WriteMode:
                        if (m_Editable)
                            DbTextBox1.Mode = Global.AccessMode.WriteMode;
                        else
                            m_Mode = Global.AccessMode.ReadMode;
                        break;
                    case Global.AccessMode.ProtectedMode:
                        DbTextBox1.Mode = Global.AccessMode.ProtectedMode;
                        break;
                }
            }
        }

        //public new ControlBindingsCollection DataBindings
        //{
        //    get { return DbTextBox1.DataBindings; }
        //}

        public int MaxLength
        {
            get { return DbTextBox1.MaxLength; }
            set { DbTextBox1.MaxLength = value; }
        }

        public decimal MaxValue
        {
            get { return DbTextBox1.MaxValue; }
            set { DbTextBox1.MaxValue = value; }
        }


        [Description("Tipo de datos a introducir en el control. Texto, Numérico, Fecha, Porcentaje, ...")]
        public DBTextBox.TypeData DataType
        {
            get { return m_DataType; }
            set
            {
                m_DataType = value;
                DbTextBox1.DataType = m_DataType;
            }
        }

        public bool ShowSelectForm
        {
            get { return m_ShowSelectForm; }
            set { m_ShowSelectForm = value; }
        }

        public override Color BackColor
        {
            get { return m_BackColor; }
            set
            {
                m_BackColor = value;
                DbTextBox1.BackColor = value;
            }
        }

        public string NameControl()
        {
            return Name;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if (m_DisableButton) return;

            var frm = new frmSelectForm();
            if (m_ShowSelectForm)
            {
                if (DataControlList == null)
                    throw new ExceptionUtil("Propiedad DataControlList del control DBFindTextBox(" + Name +
                                            "), no enlazado.");

                if (!DataControlList.Connected) DataControlList.Connect();

                if (Mode == Global.AccessMode.ReadMode)
                    frm.Text = "Formulario de Selección - Solo Lectura";
                else
                    frm.Text = "Formulario de Selección";
                frm.DBFindTextBox = this;
                frm.ShowDialog();

                if (Mode == Global.AccessMode.WriteMode)
                    if (!string.IsNullOrEmpty(frm.SelectedValue))
                    {
                        DbTextBox1.Text = frm.SelectedValue;
                        DbTextBox1.BackColor = m_BackColor;

                        DataControl.UpdateAsociatedDBFindTextBoxAndAsociatedCombo(FindForm().Controls);
                        DataControlList.UpdateRelationDBControls(FindForm().Controls, true, frm.SelectedValue);
                    }
            }

            if (null != FindClick) FindClick(this, e);
        }

        private void Control_Resize(object sender, EventArgs e)
        {
            Height = DbTextBox1.Height;
        }

        public void DBFindTextBox_LostFocus(object sender, EventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode) return;

            CheckInputFindTextBoxData();
            DataControl.UpdateAsociatedDBFindTextBoxAndAsociatedCombo(FindForm().Controls);

            if (null != LostFocus) LostFocus(this, e);
        }


        private void CheckInputFindTextBoxData()
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            object dat = null;
            var ok = false;
            var conc = " where ";

            if (DataControlList == null) return;

            var sel = DataControlList.Selection;

            if (DataControl == null) return;
            if (DataControlList == null) return;
            if (DbTextBox1.Text == "") return;

            if (DBFieldData == "")
            {
                if (!(DataControlList.DataTable == null))
                    DBFieldData = DataControlList.FieldName(0);
                else
                    throw new ExceptionUtil("DBFieldData no especificado.");
            }


            if (TextUtil.IndexOf(sel, "WHERE") != 0) conc = " and ";

            dat = db.ExecuteScalar(sel + conc + DBFieldData + "='" + DbTextBox1.Text + "'");
            if (!(dat == null)) ok = true;


            if (ok == false)
            {
                if (Obligatory)
                {
                    DbTextBox1.Text = "";
                    DbTextBox1.BackColor = Global.ProtectedBackColor;
                    DbTextBox1.Focus();
                }
                else
                {
                    DbTextBox1.BackColor = m_BackColor;
                }
            }
            else
            {
                DbTextBox1.BackColor = m_BackColor;
            }
        }


        private void DbTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) cmdSearch_Click(this, e);
        }


        public void UpdateText()
        {
            DbTextBox1.UpdateText();
        }


        private void DbTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void DbTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region Delegates

        public delegate void FindClickEventHandler(object sender, EventArgs e);

        public delegate void LostFocusEventHandler(object sender, EventArgs e);

        #endregion

        #region Events

        public event FindClickEventHandler FindClick;
        public new event LostFocusEventHandler LostFocus;

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        private DBTextBox DbTextBox1;
        private Button cmdSearch;

        public DBFindTextBox()
        {
            InitializeComponent();

            DbTextBox1.Multiline = false;

            DbTextBox1.DBField = m_DBField;
            DbTextBox1.DataControl = m_DBControl;

            DbTextBox1.LostFocus += DBFindTextBox_LostFocus;
            DbTextBox1.KeyDown += DbTextBox1_KeyDown;
            DbTextBox1.MouseDown += DbTextBox1_MouseDown;
            DbTextBox1.MouseUp += DbTextBox1_MouseUp;
            cmdSearch.Click += cmdSearch_Click;
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
            var resources = new ComponentResourceManager(typeof(DBFindTextBox));
            DbTextBox1 = new DBTextBox();
            cmdSearch = new Button();
            SuspendLayout();
            // 
            // DbTextBox1
            // 
            DbTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                 | AnchorStyles.Left
                                                 | AnchorStyles.Right;
            DbTextBox1.AsociatedCombo = null;
            DbTextBox1.AsociatedDBFindTextBox = null;
            DbTextBox1.BackColorRead = Color.WhiteSmoke;
            DbTextBox1.BorderStyle = BorderStyle.Fixed3D;
            DbTextBox1.Capitalize = DBTextBox.TypeString.Normal;
            DbTextBox1.DataControl = null;
            DbTextBox1.DataType = DBTextBox.TypeData.All;
            DbTextBox1.DateFormat = "dd/MM/yyyy";
            DbTextBox1.DBField = null;
            DbTextBox1.DBFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DbTextBox1.Decimals = 0;
            DbTextBox1.DefaultValue = null;
            DbTextBox1.DotNumber = false;
            DbTextBox1.Editable = true;
            DbTextBox1.Encrypted = false;
            DbTextBox1.Expression = "";
            DbTextBox1.FormatString = "";
            DbTextBox1.GridOperation = DBColumn.OperationTypes.Sum;
            DbTextBox1.Location = new Point(0, 0);
            DbTextBox1.MaskInput = null;
            DbTextBox1.MaxLength = 32767;
            DbTextBox1.MaxValue = decimal.MaxValue;
            DbTextBox1.Mode = Global.AccessMode.WriteMode;
            DbTextBox1.Multiline = true;
            DbTextBox1.Name = "DbTextBox1";
            DbTextBox1.Obligatory = false;
            DbTextBox1.PasswordChar = '\0';
            DbTextBox1.ReadOnly = false;
            DbTextBox1.ShowScrollBars = ScrollBars.None;
            DbTextBox1.Shadow = false;
            DbTextBox1.ShadowColor = Color.Gray;
            DbTextBox1.ShadowSize = 4;
            DbTextBox1.ShowAsCombo = false;
            DbTextBox1.ShowKeyboard = false;
            DbTextBox1.Size = new Size(113, 20);
            DbTextBox1.TabIndex = 0;
            DbTextBox1.TextAlign = HorizontalAlignment.Left;
            DbTextBox1.ToolTip = "";
            DbTextBox1.XMLName = null;
            // 
            // cmdSearch
            // 
            cmdSearch.BackColor = SystemColors.Control;
            cmdSearch.Dock = DockStyle.Right;
            cmdSearch.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdSearch.Image = (Image) resources.GetObject("cmdSearch.Image");
            cmdSearch.Location = new Point(113, 0);
            cmdSearch.Name = "cmdSearch";
            cmdSearch.Size = new Size(20, 20);
            cmdSearch.TabIndex = 1;
            cmdSearch.TabStop = false;
            cmdSearch.UseVisualStyleBackColor = false;
            // 
            // DBFindTextBox
            // 
            Controls.Add(cmdSearch);
            Controls.Add(DbTextBox1);
            Name = "DBFindTextBox";
            Size = new Size(133, 20);
            ResumeLayout(false);
        }

        #endregion
    }
}