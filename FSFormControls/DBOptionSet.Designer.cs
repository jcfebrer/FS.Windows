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
            this.SuspendLayout();
            // 
            // dbGroupBox1
            // 
            this.dbGroupBox1.Caption = "Seleccione la opción:";
            this.dbGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.dbGroupBox1.Name = "dbGroupBox1";
            this.dbGroupBox1.Size = new System.Drawing.Size(150, 150);
            this.dbGroupBox1.TabIndex = 0;
            // 
            // DBOptionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dbGroupBox1);
            this.Name = "DBOptionSet";
            this.ResumeLayout(false);

        }

        #endregion

        private DBGroupBox dbGroupBox1;
    }
}
