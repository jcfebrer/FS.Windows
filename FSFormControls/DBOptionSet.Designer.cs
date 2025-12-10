namespace FSFormControls
{
    partial class DBOptionSet
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dbGroupBox1 = new FSFormControls.DBGroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dbGroupBox1)).BeginInit();
            this.dbGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbGroupBox1
            // 
            this.dbGroupBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dbGroupBox1.Caption = "Seleccione la opción:";
            this.dbGroupBox1.Controls.Add(this.listBox1);
            this.dbGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbGroupBox1.HeaderAppearance = null;
            this.dbGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.dbGroupBox1.Name = "dbGroupBox1";
            this.dbGroupBox1.Size = new System.Drawing.Size(150, 150);
            this.dbGroupBox1.TabIndex = 0;
            this.dbGroupBox1.TabStop = false;
            this.dbGroupBox1.Text = "Seleccione la opción:";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(15, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(116, 108);
            this.listBox1.TabIndex = 1;
            // 
            // DBOptionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dbGroupBox1);
            this.Name = "DBOptionSet";
            ((System.ComponentModel.ISupportInitialize)(this.dbGroupBox1)).EndInit();
            this.dbGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DBGroupBox dbGroupBox1;
        private System.Windows.Forms.ListBox listBox1;
    }
}
