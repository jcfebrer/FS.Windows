#region

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class frmListView : DBForm
    {
        public DataRow SelectedRow;

        private void frmListView_Load(object sender, EventArgs e)
        {
            var f = 0;

            FunctionsForms.Center(this);

            if (DataControl == null) throw new ExceptionUtil("Datacontrol no especificado!");

            DbGrid1.AllowDelete = false;
            DbGrid1.DataControl = DataControl;
            DbGrid1.Mode = Global.AccessMode.ReadMode;

            for (f = 0; f <= Convert.ToInt32(DbGrid1.DataControl.FieldsCount() - 1); f++)
                DbGrid1.Columns.Add(DbGrid1.DataControl.FieldName(f),
                    TextUtil.PCase(DbGrid1.DataControl.FieldName(f)));

            DbGrid1.Fill();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void DbGrid1_DoubleClick(object sender, EventArgs e)
        {
            selectData();
            Close();
        }


        private void DbGrid1_Click(object sender, EventArgs e)
        {
            selectData();
        }


        private void selectData()
        {
            var s = DbGrid1.ActiveRow.Index;

            if (DbGrid1.DataControl.Paging) s = s + DbGrid1.DataControl.Page * DbGrid1.DataControl.PagingSize;

            try
            {
                SelectedRow = DbGrid1.DataControl.DataTable.DefaultView[s].Row;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal DBButton DbButton1;
        internal DBGridView DbGrid1;

        public frmListView()
        {
            InitializeComponent();

            Load += frmListView_Load;
            DbButton1.Click += Button1_Click;
            DbGrid1.DoubleClick += DbGrid1_DoubleClick;
            DbGrid1.Click += DbGrid1_Click;
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
            DbGrid1 = new DBGridView();
            DbButton1 = new DBButton();
            SuspendLayout();
            // 
            // DbGrid1
            // 
            DbGrid1.AllowAddNew = true;
            DbGrid1.AllowDelete = true;
            DbGrid1.AllowDrop = true;
            //DbGrid1.AllowSort = true;
            DbGrid1.AlternatingColor = Color.Empty;
            DbGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                              | AnchorStyles.Left
                                              | AnchorStyles.Right;
            DbGrid1.AutoSave = true;
            DbGrid1.AutoSizeColumns = true;
            //DbGrid1.BackGroundColor = Color.LightGray;
            DbGrid1.BorderStyle = BorderStyle.Fixed3D;
            //DbGrid1.CaptionBackColor = SystemColors.ActiveCaption;
            //DbGrid1.CaptionFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            //DbGrid1.CaptionForeColor = SystemColors.ActiveCaptionText;
            DbGrid1.CaptionText = null;
            //DbGrid1.CaptionVisible = true;
            //DbGrid1.ColumnHeadersVisible = true;
            //DbGrid1.CurrentRowIndex = -1;
            //DbGrid1.CustomColumnHeaders = false;
            DbGrid1.DataControl = null;
            DbGrid1.DefaultDecimals = 2;
            DbGrid1.DefaultHeaderFont =
                new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DbGrid1.Editable = true;
            //DbGrid1.FlatMode = true;
            //DbGrid1.GridLineColor = SystemColors.Control;
            //DbGrid1.GridLineStyle = DataGridLineStyle.Solid;
            //DbGrid1.HeaderBackColor = SystemColors.Control;
            //DbGrid1.HeaderFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //DbGrid1.HeaderForeColor = SystemColors.ControlText;
            //DbGrid1.LastCol = -1;
            //DbGrid1.LastRow = -1;
            DbGrid1.Location = new Point(16, 40);
            DbGrid1.Mode = Global.AccessMode.ReadMode;
            DbGrid1.Name = "DbGrid1";
            DbGrid1.RecordMode = false;
            //DbGrid1.RowHeadersVisible = true;
            //DbGrid1.RowHeight = 16;
            //DbGrid1.RowSel = -1;
            DbGrid1.RowsInCaption = 2;
            DbGrid1.ShowRecordScrollBar = true;
            DbGrid1.ShowTotals = false;
            DbGrid1.Size = new Size(672, 320);
            DbGrid1.TabIndex = 0;
            DbGrid1.TotalOperation = DBColumn.OperationTypes.Sum;
            //DbGrid1.XMLName = "";
            // 
            // DbButton1
            // 
            DbButton1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DbButton1.ButtonStyle = DBButton.ButtonStyleType.Normal;
            DbButton1.DropDownMenu = null;
            DbButton1.FillColorEnd = Color.White;
            DbButton1.FillColorStart = Color.LightGray;
            DbButton1.FillHoverColorEnd = Color.Beige;
            DbButton1.FillHoverColorStart = Color.Beige;
            DbButton1.FlatStyle = FlatStyle.Standard;
            DbButton1.Gradient = false;
            DbButton1.GradientMode = LinearGradientMode.Horizontal;
            DbButton1.Image = null;
            DbButton1.ImageAlign = ContentAlignment.MiddleCenter;
            DbButton1.Location = new Point(612, 366);
            DbButton1.Name = "DbButton1";
            DbButton1.Size = new Size(76, 20);
            DbButton1.TabIndex = 1;
            DbButton1.Text = "Cerrar";
            DbButton1.TextAlign = ContentAlignment.MiddleCenter;
            DbButton1.TextColorEnd = Color.Black;
            DbButton1.TextColorStart = Color.Blue;
            DbButton1.TextFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DbButton1.ToolTip = "";
            // 
            // frmListView
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(704, 412);
            Controls.Add(DbButton1);
            Controls.Add(DbGrid1);
            Name = "frmListView";
            ShowInTaskbar = false;
            Text = "Listado";
            Controls.SetChildIndex(DbGrid1, 0);
            Controls.SetChildIndex(DbButton1, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}