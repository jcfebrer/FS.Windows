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

                if(mail.SendErrorMail(ErrorMessage))
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
            Label1 = new Label();
            txtName = new TextBox();
            txtEmail = new TextBox();
            Label2 = new Label();
            GroupBox1 = new GroupBox();
            Label3 = new Label();
            txtError = new TextBox();
            dbbSend = new Button();
            dbbCancel = new Button();
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(8, 24);
            Label1.Name = "Label1";
            Label1.Size = new Size(47, 13);
            Label1.TabIndex = 0;
            Label1.Text = "Nombre:";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left
                                              | AnchorStyles.Right;
            txtName.Location = new Point(64, 24);
            txtName.Name = "txtName";
            txtName.Size = new Size(250, 20);
            txtName.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left
                                               | AnchorStyles.Right;
            txtEmail.Location = new Point(64, 56);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 20);
            txtEmail.TabIndex = 3;
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(8, 56);
            Label2.Name = "Label2";
            Label2.Size = new Size(35, 13);
            Label2.TabIndex = 2;
            Label2.Text = "Email:";
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                | AnchorStyles.Left
                                                | AnchorStyles.Right;
            GroupBox1.Controls.Add(Label3);
            GroupBox1.Controls.Add(txtError);
            GroupBox1.Controls.Add(Label2);
            GroupBox1.Controls.Add(txtName);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Controls.Add(txtEmail);
            GroupBox1.Location = new Point(16, 8);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(346, 232);
            GroupBox1.TabIndex = 4;
            GroupBox1.TabStop = false;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(8, 94);
            Label3.Name = "Label3";
            Label3.Size = new Size(196, 13);
            Label3.TabIndex = 4;
            Label3.Text = "Información para reproducir el problema:";
            // 
            // txtError
            // 
            txtError.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                               | AnchorStyles.Left
                                               | AnchorStyles.Right;
            txtError.Location = new Point(8, 112);
            txtError.Multiline = true;
            txtError.Name = "txtError";
            txtError.ScrollBars = ScrollBars.Vertical;
            txtError.Size = new Size(330, 112);
            txtError.TabIndex = 5;
            // 
            // dbbSend
            // 
            dbbSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            dbbSend.Location = new Point(274, 248);
            dbbSend.Name = "dbbSend";
            dbbSend.Size = new Size(88, 24);
            dbbSend.TabIndex = 5;
            dbbSend.Text = "Enviar";
            dbbSend.Click += dbbSend_Click;
            // 
            // dbbCancel
            // 
            dbbCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            dbbCancel.Location = new Point(178, 248);
            dbbCancel.Name = "dbbCancel";
            dbbCancel.Size = new Size(88, 24);
            dbbCancel.TabIndex = 6;
            dbbCancel.Text = "Cancelar";
            dbbCancel.Click += dbbCancel_Click;
            // 
            // frmWebError
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(378, 285);
            Controls.Add(dbbCancel);
            Controls.Add(dbbSend);
            Controls.Add(GroupBox1);
            Name = "frmWebError";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Formulario Error";
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}