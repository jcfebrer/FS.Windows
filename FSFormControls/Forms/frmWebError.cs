#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSException;
using FSLibrary;

#endregion

namespace FSFormControls
{
    public class frmWebError : Form
    {
        public string ErrorMessage = "";

        private void dbbCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void dbbSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Debe indicar un nombre de usuario.", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (!TextUtil.IsEmail(txtEmail.Text))
                {
                    MessageBox.Show("Debe indicar una dirección de correo válida.", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (txtError.Text == "")
                {
                    MessageBox.Show("Debe indicar la información para reproducir el error.", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                //DBHttp http = new DBHttp();

                ErrorMessage = ErrorMessage + "\r\n" + "\r\n" + "Proyecto: " + Global.ProjectName;
                ErrorMessage = ErrorMessage + "\r\n" + "Título de la aplicación: " + Global.ApplicationTittle;
                ErrorMessage = ErrorMessage + "\r\n" + "Nombre: " + txtName.Text;
                ErrorMessage = ErrorMessage + "\r\n" + "Email: " + txtEmail.Text;
                ErrorMessage = ErrorMessage + "\r\n" + "Error: " + txtError.Text + "\r\n";

                FSMail.SendMail mail = new FSMail.SendMail(Global.MailServer, Global.MailUserName, Global.MailPassword, Global.MailPort, Global.MailEnableSSL, Global.ProjectName);

                if (mail.SendErrorMail(ErrorMessage))
                    MessageBox.Show("Mensaje enviado.", "Envio correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Problemas en el envio del correo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        internal GroupBox GroupBox1;

        internal Label Label1;
        internal Label Label2;
        internal Label Label3;
        internal Button dbbCancel;
        internal Button dbbSend;
        internal TextBox txtEmail;
        internal TextBox txtError;
        internal TextBox txtName;

        public frmWebError()
        {
            InitializeComponent();
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
            this.Label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.TextBox();
            this.dbbSend = new System.Windows.Forms.Button();
            this.dbbCancel = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 24);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(47, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Nombre:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(64, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 20);
            this.txtName.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(64, 56);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(8, 56);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Email:";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.txtError);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.txtName);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.txtEmail);
            this.GroupBox1.Location = new System.Drawing.Point(16, 8);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(346, 232);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(8, 94);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(196, 13);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Información para reproducir el problema:";
            // 
            // txtError
            // 
            this.txtError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtError.Location = new System.Drawing.Point(8, 112);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtError.Size = new System.Drawing.Size(330, 112);
            this.txtError.TabIndex = 5;
            // 
            // dbbSend
            // 
            this.dbbSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dbbSend.Location = new System.Drawing.Point(274, 248);
            this.dbbSend.Name = "dbbSend";
            this.dbbSend.Size = new System.Drawing.Size(88, 24);
            this.dbbSend.TabIndex = 5;
            this.dbbSend.Text = "Enviar";
            this.dbbSend.Click += new System.EventHandler(this.dbbSend_Click);
            // 
            // dbbCancel
            // 
            this.dbbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dbbCancel.Location = new System.Drawing.Point(178, 248);
            this.dbbCancel.Name = "dbbCancel";
            this.dbbCancel.Size = new System.Drawing.Size(88, 24);
            this.dbbCancel.TabIndex = 6;
            this.dbbCancel.Text = "Cancelar";
            this.dbbCancel.Click += new System.EventHandler(this.dbbCancel_Click);
            // 
            // frmWebError
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(378, 285);
            this.Controls.Add(this.dbbCancel);
            this.Controls.Add(this.dbbSend);
            this.Controls.Add(this.GroupBox1);
            this.Name = "frmWebError";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario Error";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}