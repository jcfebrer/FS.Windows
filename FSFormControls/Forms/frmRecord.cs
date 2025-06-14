#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmRecord : DBForm
    {
        private void frmReport_Load(object sender, EventArgs e)
        {
            //DBColumn coldata = null;
            //int f = 0;

            FunctionsForms.Center(this);

            //DbRecord1.DataControl = DataControl;

            //for (f = 0; f <= Convert.ToInt32(DataControl.FieldsCount() - 1); f++)
            //{
            //    coldata = new DBColumn();

            //    coldata.FieldDB = DataControl.FieldName(f);
            //    coldata.HeaderCaption = DBFunctions.PCase(DataControl.FieldName(f)) + ":";
            //    coldata.MaxLength = Convert.ToInt32(DataControl.FieldSize(f));
            //    coldata.ColumnType = DBFunctions.ConvertFieldTypeToColumnType(DataControl.FieldType(f));

            //    DbRecord1.Columns.Add(coldata);
            //}

            DbRecord1.AutoSizeColumns = true;

            DbRecord1.Fill();

            DataControl.UpdateControls(Controls);
        }

        #region '" Windows Form Designer generated code "' 

        private readonly IContainer components = null;

        internal DBRecord DbRecord1;

        //public DBRecord DBRecord
        //{
        //    get { return DbRecord1; }
        //    set { DbRecord1 = value; }
        //}

        public new DBControl DataControl
        {
            get { return base.DataControl; }
            set
            {
                base.DataControl = value;
                DbRecord1.DataControl = base.DataControl;
            }
        }

        public frmRecord()
        {
            InitializeComponent();

            Load += frmReport_Load;
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
            this.DbRecord1 = new FSFormControls.DBRecord();
            this.SuspendLayout();
            // 
            // DbToolBar1
            // 
            this.DbToolBar1.Size = new System.Drawing.Size(722, 28);
            this.DbToolBar1.VisibleScroll = true;
            // 
            // mnuForm
            // 
            this.mnuForm.MergeAction = MergeAction.Replace;
            //this.mnuForm.OwnerDraw = true;
            // 
            // DbRecord1
            // 
            
            this.DbRecord1.AllowAddNew = true;
            this.DbRecord1.AllowCancel = true;
            this.DbRecord1.AllowDelete = true;
            this.DbRecord1.AllowEdit = true;
            this.DbRecord1.AllowFilter = true;
            this.DbRecord1.AllowList = true;
            this.DbRecord1.AllowNavigate = true;
            this.DbRecord1.AllowPrint = true;
            this.DbRecord1.AllowRecord = true;
            this.DbRecord1.AllowSave = true;
            this.DbRecord1.AllowSearch = true;
            this.DbRecord1.AutoSizeColumns = true;
            this.DbRecord1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                        this.DbRecord1.DateType = FSFormControls.DBRecord.t_date.Normal;
            this.DbRecord1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbRecord1.DoubleHeightInLargeText = false;
            this.DbRecord1.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.DbRecord1.LabelYIncrement = 30;
            this.DbRecord1.Location = new System.Drawing.Point(0, 28);
            this.DbRecord1.Mode = FSFormControls.Global.AccessMode.ReadMode;
            this.DbRecord1.Name = "DbRecord1";
            this.DbRecord1.PosXLabel = 20;
            this.DbRecord1.PosYLabel = 20;
            this.DbRecord1.ShowAddNew = true;
            this.DbRecord1.ShowCancel = true;
            this.DbRecord1.ShowClose = true;
            this.DbRecord1.ShowComboEdit = false;
            this.DbRecord1.ShowDelete = true;
            this.DbRecord1.ShowEdit = true;
            this.DbRecord1.ShowFilter = true;
            this.DbRecord1.ShowList = true;
            this.DbRecord1.ShowMode = FSFormControls.DBRecord.t_showmode.Vertical;
            this.DbRecord1.ShowNavigate = true;
            this.DbRecord1.ShowPrint = true;
            this.DbRecord1.ShowRecord = true;
            this.DbRecord1.ShowSave = true;
            this.DbRecord1.ShowScrollBar = false;
            this.DbRecord1.ShowSearch = true;
            this.DbRecord1.ShowToolBar = false;
            this.DbRecord1.Size = new System.Drawing.Size(722, 226);
            this.DbRecord1.TabIndex = 0;
            this.DbRecord1.TextBoxShadow = false;
            // 
            // frmRecord
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(722, 254);
            this.Controls.Add(this.DbRecord1);
            this.Name = "frmRecord";
            this.Text = "Registro";
            this.Controls.SetChildIndex(this.DbToolBar1, 0);
            this.Controls.SetChildIndex(this.DbRecord1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}