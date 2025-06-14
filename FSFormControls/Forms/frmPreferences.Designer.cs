namespace FSFormControls {
    partial class frmPreferences {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tvNodes = new System.Windows.Forms.TreeView();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.comboSecciones = new System.Windows.Forms.ComboBox();
            this.lblSecciones = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tvNodes
            // 
            this.tvNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvNodes.HideSelection = false;
            this.tvNodes.Location = new System.Drawing.Point(12, 47);
            this.tvNodes.Name = "tvNodes";
            this.tvNodes.ShowNodeToolTips = true;
            this.tvNodes.Size = new System.Drawing.Size(333, 339);
            this.tvNodes.TabIndex = 3;
            this.tvNodes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNodes_AfterSelect);
            // 
            // txtValue
            // 
            this.txtValue.AllowDrop = true;
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(12, 405);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtValue.Size = new System.Drawing.Size(333, 65);
            this.txtValue.TabIndex = 4;
            this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.Location = new System.Drawing.Point(12, 476);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Guardar";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(12, 389);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 6;
            this.lblValue.Text = "Valor:";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(270, 476);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // comboSecciones
            // 
            this.comboSecciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSecciones.Location = new System.Drawing.Point(78, 20);
            this.comboSecciones.Name = "comboSecciones";
            this.comboSecciones.Size = new System.Drawing.Size(267, 21);
            this.comboSecciones.TabIndex = 8;
            this.comboSecciones.SelectedIndexChanged += new System.EventHandler(this.comboSecciones_SelectedIndexChanged);
            // 
            // lblSecciones
            // 
            this.lblSecciones.AutoSize = true;
            this.lblSecciones.Location = new System.Drawing.Point(12, 23);
            this.lblSecciones.Name = "lblSecciones";
            this.lblSecciones.Size = new System.Drawing.Size(60, 13);
            this.lblSecciones.TabIndex = 9;
            this.lblSecciones.Text = "Secciones:";
            // 
            // frmPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 520);
            this.Controls.Add(this.lblSecciones);
            this.Controls.Add(this.comboSecciones);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.tvNodes);
            this.KeyPreview = true;
            this.Name = "frmPreferences";
            this.Text = "Administrador de preferencias";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Load += new System.EventHandler(this.frmPreferences_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView tvNodes;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox comboSecciones;
        private System.Windows.Forms.Label lblSecciones;
    }
}

