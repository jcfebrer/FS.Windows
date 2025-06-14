#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FSLibrary;
using DateTime = System.DateTime;
using FSException;
using FSDisk;
using FSSystemInfo;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public class DBFile : DBUserControl
    {
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
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

        public bool ShowText { get; set; } = true;

        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;

                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        cmdLoad.Enabled = false;
                        break;
                    case Global.AccessMode.WriteMode:
                        cmdLoad.Enabled = true;
                        break;
                }


                if (null != ModeChanged) ModeChanged(m_Mode);
            }
        }

        public string FieldFileName { get; set; } = "nombre";

        public string FieldDateTime { get; set; } = "fecha";

        [Browsable(false)] public byte[] Data { get; set; }

        public event FileChangedEventHandler FileChanged;
        public event ModeChangedEventHandler ModeChanged;

        public void UpdateFile()
        {
            //if (DataBindings.Count > 0) return;

            if (FieldFileName == "") throw new ExceptionUtil("Propiedad FieldFileName, sin especificar");
            if (FieldDateTime == "") throw new ExceptionUtil("Propiedad FieldDateTime, sin especificar");

            try
            {
                //Binding dbnFile = new Binding("Data", DataControl.DataTable, DBField);

                Data = DataControl.GetFieldByte(DBField);

                txtFileName.Text = DataControl.GetField(FieldFileName).ToString();
                txtDateTime.Text = DataControl.GetField(FieldDateTime).ToString();

                //DataBindings.Add(dbnFile);
                //txtFileName.DataBindings.Add("Text", DataControl.DataTable, FieldFileName);
                //txtDateTime.DataBindings.Add("Text", DataControl.DataTable, FieldDateTime);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        private void DBFile_Resize(object sender, EventArgs e)
        {
            Height = txtFileName.Height;

            txtFileName.Visible = ShowText;
            txtDateTime.Visible = ShowText;
        }


        private void cmdLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.ShowDialog();

            if (OpenFileDialog1.FileName == "") return;

            var fs = new FileStream(OpenFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Read);

            Data = new byte[fs.Length];
            fs.Read(Data, 0, Convert.ToInt32(fs.Length));

            txtFileName.Text = OpenFileDialog1.FileName;
            txtDateTime.Text = Convert.ToString(DateTime.Now);

            DataControl.SetField(FieldFileName, txtFileName.Text);
            DataControl.SetField(FieldDateTime, txtDateTime.Text);

            if (null != FileChanged)
                FileChanged(sender, e);
        }


        private void cmdExecute_Click(object sender, EventArgs e)
        {
            if (Data == null) throw new ExceptionUtil("No existen datos!");

            var ext = FileUtils.FileExtension(txtFileName.Text);
            var ArraySize = 0;

            ArraySize = Data.GetUpperBound(0);

            if (ArraySize == -1) throw new ExceptionUtil("Datos incorrectos!");

            var fs = new FileStream(Application.StartupPath + @"\temp." + ext, FileMode.Create, FileAccess.Write);
            fs.Write(Data, 0, ArraySize);
            fs.Close();

            if (!ProcessUtil.OpenDocument(Application.StartupPath + @"\temp." + ext))
                throw new ExceptionUtil("Error, ejecutando aplicación: " + Application.StartupPath + @"\temp." + ext);
        }


        private void txtFileName_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void txtFileName_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }


        private void txtDateTime_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void txtDateTime_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        internal OpenFileDialog OpenFileDialog1;

        internal Button cmdExecute;
        internal Button cmdLoad;
        internal TextBox txtDateTime;
        internal TextBox txtFileName;

        public DBFile()
        {
            InitializeComponent();

            txtFileName.MouseUp += txtFileName_MouseUp;
            txtDateTime.MouseDown += txtDateTime_MouseDown;
            txtDateTime.MouseUp += txtDateTime_MouseUp;
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
            var resources = new ComponentResourceManager(typeof(DBFile));
            cmdExecute = new Button();
            txtFileName = new TextBox();
            cmdLoad = new Button();
            OpenFileDialog1 = new OpenFileDialog();
            txtDateTime = new TextBox();
            SuspendLayout();
            // 
            // cmdExecute
            // 
            cmdExecute.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdExecute.Image = (Image) resources.GetObject("cmdExecute.Image");
            cmdExecute.Location = new Point(485, 0);
            cmdExecute.Name = "cmdExecute";
            cmdExecute.Size = new Size(28, 20);
            cmdExecute.TabIndex = 0;
            // 
            // txtFileName
            // 
            txtFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left
                                                  | AnchorStyles.Right;
            txtFileName.Location = new Point(0, 0);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(345, 20);
            txtFileName.TabIndex = 1;
            txtFileName.Text = "DBFile1";
            // 
            // cmdLoad
            // 
            cmdLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdLoad.Image = (Image) resources.GetObject("cmdLoad.Image");
            cmdLoad.Location = new Point(457, 0);
            cmdLoad.Name = "cmdLoad";
            cmdLoad.Size = new Size(28, 20);
            cmdLoad.TabIndex = 2;
            cmdLoad.Text = "...";
            // 
            // txtDateTime
            // 
            txtDateTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtDateTime.Location = new Point(345, 0);
            txtDateTime.Name = "txtDateTime";
            txtDateTime.ReadOnly = true;
            txtDateTime.Size = new Size(112, 20);
            txtDateTime.TabIndex = 3;
            txtDateTime.Text = "DBFile1";
            // 
            // DBFile
            // 
            Controls.Add(txtDateTime);
            Controls.Add(cmdLoad);
            Controls.Add(txtFileName);
            Controls.Add(cmdExecute);
            Name = "DBFile";
            Size = new Size(513, 24);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        #region Delegates

        public delegate void FileChangedEventHandler(object sender, EventArgs e);

        public delegate void ModeChangedEventHandler(Global.AccessMode mode);

        #endregion
    }


    internal class DBFileControlDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules =>
            SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable |
            SelectionRules.Visible;
    }
}