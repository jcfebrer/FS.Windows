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
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBCheckBox.bmp")]
    [DefaultEvent("CheckedChanged")]
    [ToolboxItem(true)]
    public class DBCheckBox : DBUserControl, ISupportInitialize
    {
        private CheckBox checkbox;
        private Color m_BackColor = Color.Transparent;
        private bool m_Editable = true;
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;
        private string m_Text = "";

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return m_Text; }
            set
            {
                m_Text = value;
                checkbox.Text = m_Text;
            }
        }

        public CheckBox CheckBox {
            get { return checkbox; }
            set { checkbox = value; }
        }

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

        public DBAppearance Appearance { get; set; }

        public bool Checked
        {
            get { return checkbox.Checked; }
            set { checkbox.Checked = value; }
        }

        public CheckState CheckState
        {
            get { return checkbox.CheckState; }
            set { checkbox.CheckState = value; }
        }

        public ContentAlignment CheckAlign
        {
            get { return checkbox.CheckAlign; }
            set { checkbox.CheckAlign = value; }
        }

        [Description("Indicamos si el control es editable o no.")]
        public bool Editable
        {
            get { return m_Editable; }
            set
            {
                m_Editable = value;
                checkbox.Enabled = m_Editable;
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
                        checkbox.Enabled = false;
                        checkbox.BackColor = m_BackColor;
                        break;
                    case Global.AccessMode.WriteMode:
                        checkbox.Enabled = true;
                        checkbox.BackColor = m_BackColor;
                        break;
                    case Global.AccessMode.ProtectedMode:
                        checkbox.BackColor = Global.ObligatoryBackColor;
                        break;
                }
            }
        }

        public override Color BackColor
        {
            get { return m_BackColor; }
            set
            {
                m_BackColor = value;
                checkbox.BackColor = value;
            }
        }

        public FlatStyle FlatStyle
        {
            get { return checkbox.FlatStyle; }
            set { checkbox.FlatStyle = value; }
        }

        public new ControlBindingsCollection DataBindings => checkbox.DataBindings;


        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public event EventHandler CheckedChanged;
        public new event ClickEventHandler Click;


        private void CheckBox1_Click(object sender, EventArgs e)
        {
            if (null != Click)
                Click(this, e);
        }


        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (null != CheckedChanged) CheckedChanged(this, e);
        }


        public void UpdateCheckBox()
        {
            //Binding dbnCheck = null;
            var strError = "";

            if (checkbox.DataBindings.Count > 0) return;

            if (DataControl != null)
            {
                if (!string.IsNullOrEmpty(DBField))
                {
                    //dbnCheck = new Binding("Checked", DataControl.DataTable, DBField);
                    Checked = DataControl.GetFieldBoolean(DBField);
                }
                else
                {
                    strError = "Control " + Name + " sin DBField asociado";

                    throw new ExceptionUtil(strError);
                }
            }
            //else
            //{
            //    dbnCheck = new Binding("Checked", CheckBox1, "CheckState");
            //}

            //dbnCheck.Format += SmallIntToBoolean;
            //dbnCheck.Parse += BooleanToSmallInt;

            //try
            //{
            //    CheckBox1.DataBindings.Add(dbnCheck);
            //}
            //catch (System.Exception e)
            //{
            //    throw new ExceptionUtil("Campo: " + DBField, e);
            //}
        }


        protected void SmallIntToBoolean(object sender, ConvertEventArgs e)
        {
            try
            {
                if (e.Value is DBNull) e.Value = 0;
                if (Convert.ToInt32(e.Value) == Convert.ToInt32(true)) e.Value = 1;
                if (Convert.ToInt32(e.Value) == Convert.ToInt32(false)) e.Value = 0;
                switch (Convert.ToInt32(e.Value))
                {
                    case 1:
                        e.Value = true;
                        break;
                    default:
                        e.Value = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        protected void BooleanToSmallInt(object sender, ConvertEventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(e.Value))
                {
                    case 1:
                        e.Value = 1;
                        break;
                    default:
                        e.Value = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void CheckBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void CheckBox1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region Delegates

        public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

        public delegate void ClickEventHandler(object sender, EventArgs e);

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        public DBCheckBox()
        {
            InitializeComponent();

            checkbox.Click += CheckBox1_Click;
            checkbox.CheckedChanged += CheckBox1_CheckedChanged;
            checkbox.MouseDown += CheckBox1_MouseDown;
            checkbox.MouseUp += CheckBox1_MouseUp;
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
            checkbox = new CheckBox();
            SuspendLayout();
            // 
            // CheckBox1
            // 
            checkbox.Dock = DockStyle.Fill;
            checkbox.Location = new Point(0, 0);
            checkbox.Name = "checkbox";
            checkbox.Size = new Size(169, 33);
            checkbox.TabIndex = 0;
            checkbox.Text = "DBCheckBox1";
            // 
            // DBCheckBox
            // 
            Controls.Add(checkbox);
            Name = "DBCheckBox";
            Size = new Size(169, 33);
            ResumeLayout(false);
        }

        #endregion
    }
}