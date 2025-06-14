#region

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSDatabase;

#endregion

namespace FSFormControls
{
    public class frmShowTable : Form
    {
        #region button_enum enum

        public enum button_enum
        {
            no = 0,
            yes = 1
        }

        #endregion

        public button_enum button;

        public bool fullDlg;

        public DataTable DataTable
        {
            set { dataGridView1.DataSource = value; }
            get { return (DataTable) dataGridView1.DataSource; }
        }


        private void ShowChanges()
        {
            var f = 0;
            foreach (DataRow row in DataTable.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    var g = 0;
                    foreach (DataColumn col in DataTable.Columns)
                    {
                        if (Utils.hasColumnChanged(row, col))
                        {
                            dataGridView1[g, f].Style.SelectionBackColor = Color.Red;
                            dataGridView1[g, f].Style.ForeColor = Color.Red;
                        }

                        g++;
                    }
                }

                f++;
            }
        }

        public void ShowDialog(string message, string title)
        {
            lblMessage.Text = message;
            Text = title;

            base.ShowDialog();
        }

        private void cmdMore_Click(object sender, EventArgs e)
        {
            if (fullDlg)
            {
                Height = 148;
                fullDlg = false;
                cmdMore.Text = "Desplegar  >>";
            }
            else
            {
                Height = 348;
                fullDlg = true;
                cmdMore.Text = "<<  Desplegar";

                ShowChanges();
            }
        }


        private void frmDBError_Load(object sender, EventArgs e)
        {
            Height = 148;
        }


        private void cmdNo_Click(object sender, EventArgs e)
        {
            button = button_enum.no;
            Close();
        }


        private void cmdYes_Click(object sender, EventArgs e)
        {
            button = button_enum.yes;
            Close();
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal PictureBox PictureBox1;
        internal Button cmdMore;
        internal Button cmdNo;
        internal Button cmdYes;
        private DataGridView dataGridView1;
        internal Label lblMessage;

        public frmShowTable()
        {
            InitializeComponent();

            cmdMore.Click += cmdMore_Click;
            Load += frmDBError_Load;
            cmdNo.Click += cmdNo_Click;
            cmdYes.Click += cmdYes_Click;
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
            var resources = new ComponentResourceManager(typeof(frmShowTable));
            cmdMore = new Button();
            PictureBox1 = new PictureBox();
            lblMessage = new Label();
            cmdNo = new Button();
            cmdYes = new Button();
            dataGridView1 = new DataGridView();
            ((ISupportInitialize) PictureBox1).BeginInit();
            ((ISupportInitialize) dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // cmdMore
            // 
            cmdMore.BackColor = SystemColors.Control;
            cmdMore.Location = new Point(16, 64);
            cmdMore.Name = "cmdMore";
            cmdMore.Size = new Size(96, 24);
            cmdMore.TabIndex = 1;
            cmdMore.Text = "Desplegar   >>";
            cmdMore.UseVisualStyleBackColor = false;
            // 
            // PictureBox1
            // 
            PictureBox1.Image = (Image) resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(16, 8);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(48, 41);
            PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox1.TabIndex = 2;
            PictureBox1.TabStop = false;
            // 
            // lblMessage
            // 
            lblMessage.Location = new Point(72, 8);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(328, 48);
            lblMessage.TabIndex = 3;
            // 
            // cmdNo
            // 
            cmdNo.BackColor = SystemColors.Control;
            cmdNo.Location = new Point(328, 64);
            cmdNo.Name = "cmdNo";
            cmdNo.Size = new Size(72, 24);
            cmdNo.TabIndex = 5;
            cmdNo.Text = "No";
            cmdNo.UseVisualStyleBackColor = false;
            // 
            // cmdYes
            // 
            cmdYes.Location = new Point(248, 64);
            cmdYes.Name = "cmdYes";
            cmdYes.Size = new Size(72, 24);
            cmdYes.TabIndex = 8;
            cmdYes.Text = "Si";
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                    | AnchorStyles.Left
                                                    | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 104);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(384, 228);
            dataGridView1.TabIndex = 9;
            // 
            // frmShowTable
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(416, 344);
            Controls.Add(dataGridView1);
            Controls.Add(cmdYes);
            Controls.Add(cmdNo);
            Controls.Add(lblMessage);
            Controls.Add(PictureBox1);
            Controls.Add(cmdMore);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmShowTable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aviso";
            ((ISupportInitialize) PictureBox1).EndInit();
            ((ISupportInitialize) dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}