#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public class DBDocument : DBUserControl
    {
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;


        //[Description("DataBindings.")]
        //public new ControlBindingsCollection DataBindings
        //{
        //    get { return Label1.DataBindings; }
        //}

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

        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;

                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        Label1.ContextMenuStrip = null;
                        Label1.Enabled = false;
                        break;
                    case Global.AccessMode.WriteMode:
                        Label1.ContextMenuStrip = ContextMenu1;
                        Label1.Enabled = false;
                        break;
                }
            }
        }

        public void Bind()
        {
            //if (Label1.DataBindings.Count > 0) return;

            //Binding dbnDocument = new Binding("Text", DataControl.DataTable, DBField);

            if (DataControl != null)
                Label1.Text = DataControl.GetField(DBField).ToString();

            //dbnDocument.Format += dbnFormat;

            //try
            //{
            //    Label1.DataBindings.Add(dbnDocument);
            //}
            //catch (System.Exception e)
            //{
            //    throw new ExceptionUtil(e);
            //}
        }


        private void dbnFormat(object sender, ConvertEventArgs e)
        {
            if (e.Value is DBNull)
            {
                var b = new Bitmap(1, 1);
                e.Value = b;
                Label1.Visible = true;
                return;
            }

            var img = (byte[]) e.Value;

            var ms = new MemoryStream();
            var offset = 0;
            ms.Write(img, offset, img.Length - offset);
            var bmp = new Bitmap(ms);
            ms.Close();

            e.Value = bmp;
        }


        private void MenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog1.Filter = "JPEG|*.jpg|Mapa de bits|*.bmp|GIF|*.gif|Metarchivo|*.wmf|Icono|*.ico";
                OpenFileDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal ContextMenuStrip ContextMenu1;
        internal Label Label1;
        internal ToolStripMenuItem MenuItem1;
        internal OpenFileDialog OpenFileDialog1;

        public DBDocument()
        {
            InitializeComponent();

            MenuItem1.Click += MenuItem1_Click;
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
            this.ContextMenu1 = new System.Windows.Forms.ContextMenuStrip();
            this.MenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ContextMenu1
            // 
            this.ContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.MenuItem1});
            // 
            // MenuItem1
            // 
            this.MenuItem1.ImageIndex = 0;
            this.MenuItem1.Text = "Seleccionar Imagen";
            // 
            // Label1
            // 
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label1.ContextMenuStrip = this.ContextMenu1;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(150, 105);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "DBDocument";
            this.Label1.Visible = false;
            // 
            // DBDocument
            // 
            this.Controls.Add(this.Label1);
            this.Name = "DBDocument";
            this.Size = new System.Drawing.Size(150, 105);
            this.ResumeLayout(false);

        }

        #endregion
    }
}