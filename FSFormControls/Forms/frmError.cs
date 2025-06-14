#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmError : Form
    {
        public bool fullDlg;

        public void ShowDialog(string message, string fullMessage, string title)
        {
            txtFullMessage.Text = fullMessage;
            lblMessage.Text = message;
            Text = title;

            cmdClose.Select();

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
            }
        }


        private void frmDBError_Load(object sender, EventArgs e)
        {
            Height = 148;
        }


        private void cmdContinue_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void cmdWeb_Click(object sender, EventArgs e)
        {
            var frmWeb = new frmWebError();
            frmWeb.ErrorMessage = txtFullMessage.Text;
            frmWeb.TopMost = true;
            frmWeb.Show();
        }


        private void cmdExit_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("¿Deseas salir de la aplicación?", "Salir", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
                Application.ExitThread();
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal PictureBox PictureBox1;
        internal Button cmdClose;
        internal Button cmdExit;
        internal Button cmdMore;
        internal Button cmdWeb;
        internal Label lblMessage;
        internal TextBox txtFullMessage;

        public frmError()
        {
            InitializeComponent();

            Load += frmDBError_Load;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmError));
            this.txtFullMessage = new System.Windows.Forms.TextBox();
            this.cmdMore = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdWeb = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFullMessage
            // 
            this.txtFullMessage.Location = new System.Drawing.Point(16, 111);
            this.txtFullMessage.Multiline = true;
            this.txtFullMessage.Name = "txtFullMessage";
            this.txtFullMessage.ReadOnly = true;
            this.txtFullMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFullMessage.Size = new System.Drawing.Size(384, 177);
            this.txtFullMessage.TabIndex = 0;
            // 
            // cmdMore
            // 
            this.cmdMore.BackColor = System.Drawing.SystemColors.Control;
            this.cmdMore.Location = new System.Drawing.Point(16, 64);
            this.cmdMore.Name = "cmdMore";
            this.cmdMore.Size = new System.Drawing.Size(96, 24);
            this.cmdMore.TabIndex = 1;
            this.cmdMore.Text = "Desplegar   >>";
            this.cmdMore.UseVisualStyleBackColor = false;
            this.cmdMore.Click += new System.EventHandler(this.cmdMore_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(16, 8);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(48, 48);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 2;
            this.PictureBox1.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(72, 8);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(328, 48);
            this.lblMessage.TabIndex = 3;
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.SystemColors.Control;
            this.cmdClose.Location = new System.Drawing.Point(328, 64);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(72, 24);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Cerrar";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdContinue_Click);
            // 
            // cmdWeb
            // 
            this.cmdWeb.Location = new System.Drawing.Point(120, 64);
            this.cmdWeb.Name = "cmdWeb";
            this.cmdWeb.Size = new System.Drawing.Size(120, 24);
            this.cmdWeb.TabIndex = 6;
            this.cmdWeb.Text = "Enviar mensaje";
            this.cmdWeb.Click += new System.EventHandler(this.cmdWeb_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExit.Location = new System.Drawing.Point(248, 64);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(72, 24);
            this.cmdExit.TabIndex = 7;
            this.cmdExit.Text = "Salir";
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // frmError
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(416, 344);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdWeb);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.cmdMore);
            this.Controls.Add(this.txtFullMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmError";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error!";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}