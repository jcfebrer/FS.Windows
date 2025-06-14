#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FSLibrary;
using DateTime = System.DateTime;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBDate.bmp")]
    [Designer(typeof(DBDateControlDesigner))]
    [ToolboxItem(true)]
    public class DBDate : DBUserControl, ISupportInitialize
    {
        private Color m_BackColor = Global.NormalBackColor;
        private string m_CustomFormat = "dd/MM/yyyy";
        private bool m_Editable = true;
        private DateTimePickerFormat m_Format = DateTimePickerFormat.Custom;

        private bool m_IsNull;
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;
        private bool m_Obligatory;
        private string m_oldCustomFormat;
        private DateTimePickerFormat m_oldFormat;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return FSLibrary.DateTimeUtil.ShortDate(DateTimePicker1.Value); }
            set
            {
                if (FSLibrary.DateTimeUtil.IsDate(value)) DateTimePicker1.Value = DateTime.Parse(value);
            }
        }


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// Asignamos el parent del dbcontrol cuando se user dl dbcontrol sin asignar a un formulario.
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

        public DateTime Date
        {
            get { return DateTimePicker1.Value; }
            set { DateTimePicker1.Value = value; }
        }

        public DateTime DateTime
        {
            get { return DateTimePicker1.Value; }
            set { DateTimePicker1.Value = value; }
        }

        public override Color ForeColor
        {
            get { return DateTimePicker1.ForeColor; }

            set { DateTimePicker1.ForeColor = value; }
        }

        public DateTime? Value
        {
            get { return DateTimePicker1.Value; }
            set
            {
                if (value == null)
                {
                    DateTimePicker1.CustomFormat = " ";
                    DateTimePicker1.Format = DateTimePickerFormat.Custom;
                }
                else
                {
                    DateTimePicker1.Value = (DateTime) value;
                }
            }
        }

        [Description("Modo lectura")]
        public bool ReadOnly
        {
            get { return Mode == Global.AccessMode.ReadMode; }
            set { Mode = value ? Global.AccessMode.ReadMode : Global.AccessMode.WriteMode; }
        }

        public char PromptChar { get; set; }

        public DBAppearance Appearance { get; set; }

        public string MaskInput { get; set; }

        public bool AllowNullValue { get; set; } = true;

        public DateTime MinDate
        {
            get { return DateTimePicker1.MinDate; }
            set { DateTimePicker1.MinDate = value; }
        }

        public DateTime MaxDate
        {
            get { return DateTimePicker1.MaxDate; }
            set { DateTimePicker1.MaxDate = value; }
        }

        public string FormatString { get; set; } = "";

        public bool IsNull
        {
            get { return m_IsNull; }
            set
            {
                m_IsNull = value;

                if (value)
                    ForceChange();
                else
                    ShowDate();
            }
        }

        public string CustomFormat
        {
            get { return m_CustomFormat; }
            set
            {
                m_CustomFormat = value;
                DateTimePicker1.CustomFormat = value;
            }
        }

        public DateTimePickerFormat Format
        {
            get { return m_Format; }
            set
            {
                m_Format = value;
                DateTimePicker1.Format = value;
            }
        }

        [Description("Indicamos si el control es editable o no.")]
        public bool Editable
        {
            get { return m_Editable; }
            set
            {
                m_Editable = value;
                DateTimePicker1.Enabled = m_Editable;
            }
        }

        public bool Obligatory
        {
            get { return m_Obligatory; }
            set
            {
                m_Obligatory = value;
                if (value)
                    DateTimePicker1.BackColor = Global.ObligatoryBackColor;
                else
                    DateTimePicker1.BackColor = m_BackColor;
            }
        }

        public override Color BackColor
        {
            get { return m_BackColor; }
            set
            {
                m_BackColor = value;
                DateTimePicker1.BackColor = value;
            }
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
                        DateTimePicker1.Enabled = false;
                        DateTimePicker1.BackColor = m_BackColor;
                        break;
                    case Global.AccessMode.WriteMode:
                        DateTimePicker1.Enabled = true;
                        DateTimePicker1.BackColor = m_BackColor;
                        break;
                    case Global.AccessMode.ProtectedMode:
                        DateTimePicker1.BackColor = Global.ObligatoryBackColor;
                        break;
                }
            }
        }

        public int SelectionStart { get; set; }

        public int SelectionLength { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public event EventHandler ValueChanged;
        public new event EventHandler Leave;
        public new event EventHandler Enter;
        public new event EventHandler LostFocus;
        public event EventHandler AfterEnterEditMode;
        public event EventHandler AfterExitEditMode;

        //public new ControlBindingsCollection DataBindings
        //{
        //    get { return DateTimePicker1.DataBindings; }
        //}

        private void ForceChange()
        {
            string tmp = null;
            tmp = Text;
            Text = Convert.ToString(DateTimePicker1.MinDate);
            Text = tmp;
            HideDate();
        }

        public string NameControl()
        {
            return Name;
        }

        private void Control_Resize(object sender, EventArgs e)
        {
            Height = DateTimePicker1.Height;
        }


        private void DateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                if (AllowNullValue)
                    IsNull = true;
        }


        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (AllowNullValue)
            {
                if (!FSLibrary.DateTimeUtil.IsDate(Text + "")) HideDate();
                if (IsNull) HideDate();
            }

            if (ValueChanged != null)
                ValueChanged(sender, e);
        }


        private void HideDate()
        {
            Format = DateTimePickerFormat.Custom;
            CustomFormat = " ";
        }


        private void SaveFormat()
        {
            m_oldFormat = Format;
            m_oldCustomFormat = CustomFormat;
        }


        private void RestoreFormat()
        {
            Format = m_oldFormat;
            CustomFormat = m_oldCustomFormat;
        }


        private void ShowDate()
        {
            RestoreFormat();
        }


        public void UpdateText()
        {
            if (DBField == null)
                return;

            //if (DateTimePicker1.DataBindings.Count > 0) return;

            //Binding dbnDate = new Binding("Text", DataControl.DataTable, DBField);
            Text = DataControl.GetField(DBField).ToString();

            //dbnDate.Format += dbnFormat;
            //dbnDate.Parse += dbnParse;

            //try
            //{
            //    DateTimePicker1.DataBindings.Add(dbnDate);
            //}
            //catch (System.Exception e)
            //{
            //    throw new ExceptionUtil("Campo: " + DBField);
            //}
        }


        protected void dbnFormat(object sender, ConvertEventArgs e)
        {
            try
            {
                if (e.Value is DBNull)
                {
                    IsNull = true;
                    e.Value = DateTime.Now.Date;
                }
                else
                {
                    if (Convert.ToDateTime(e.Value) == DateTimePicker1.MinDate)
                    {
                        IsNull = true;
                        e.Value = DateTime.Now.Date;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        protected void dbnParse(object sender, ConvertEventArgs e)
        {
            try
            {
                if (IsNull)
                {
                    if (AllowNullValue) e.Value = DateTimePicker1.MinDate;
                }
                else
                {
                    e.Value = Convert.ToDateTime(e.Value);
                }
            }
            catch (Exception exp)
            {
                throw new ExceptionUtil("Fecha incorrecta: " + exp.Message);
            }
        }


        private void DateTimePicker1_DropDown(object sender, EventArgs e)
        {
            IsNull = false;
        }


        private void DateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void DateTimePicker1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        private void DateTimePicker1_Leave(object sender, EventArgs e)
        {
            base.OnLeave(e);

            if (Leave != null)
                Leave(sender, e);
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        private DateTimePicker DateTimePicker1;

        public DBDate()
        {
            InitializeComponent();


            SaveFormat();

            Resize += Control_Resize;
            DateTimePicker1.KeyDown += DateTimePicker1_KeyDown;
            DateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
            DateTimePicker1.DropDown += DateTimePicker1_DropDown;
            DateTimePicker1.MouseDown += DateTimePicker1_MouseDown;
            DateTimePicker1.MouseUp += DateTimePicker1_MouseUp;
            DateTimePicker1.Leave += DateTimePicker1_Leave;
            DateTimePicker1.Enter += DateTimePicker1_Enter;
            DateTimePicker1.LostFocus += DateTimePicker1_LostFocus;
        }

        private void DateTimePicker1_LostFocus(object sender, EventArgs e)
        {
            if (LostFocus != null)
                LostFocus(sender, e);

            if (AfterExitEditMode != null)
                AfterExitEditMode(sender, e);
        }

        private void DateTimePicker1_Enter(object sender, EventArgs e)
        {
            if (Enter != null)
                Enter(sender, e);

            if (AfterEnterEditMode != null)
                AfterEnterEditMode(sender, e);
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
            this.DateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // DateTimePicker1
            // 
            this.DateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.DateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.DateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePicker1.Location = new System.Drawing.Point(0, 0);
            this.DateTimePicker1.Name = "DateTimePicker1";
            this.DateTimePicker1.Size = new System.Drawing.Size(101, 20);
            this.DateTimePicker1.TabIndex = 0;
            // 
            // DBDate
            // 
            this.Controls.Add(this.DateTimePicker1);
            this.Name = "DBDate";
            this.Size = new System.Drawing.Size(101, 36);
            this.ResumeLayout(false);

        }

        #endregion
    }


    internal class DBDateControlDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules =>
            SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable |
            SelectionRules.Visible;
    }
}