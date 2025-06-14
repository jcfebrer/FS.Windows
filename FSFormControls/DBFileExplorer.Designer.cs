namespace FSFormControls
{
    partial class DBFileExplorer
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cmbDrives = new System.Windows.Forms.ComboBox();
            dbTreeView1 = new DBTreeView();
            ((System.ComponentModel.ISupportInitialize)dbTreeView1).BeginInit();
            SuspendLayout();
            // 
            // cmbDrives
            // 
            cmbDrives.Dock = System.Windows.Forms.DockStyle.Top;
            cmbDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbDrives.FormattingEnabled = true;
            cmbDrives.Location = new System.Drawing.Point(0, 0);
            cmbDrives.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbDrives.Name = "cmbDrives";
            cmbDrives.Size = new System.Drawing.Size(318, 28);
            cmbDrives.TabIndex = 28;
            cmbDrives.SelectedIndexChanged += cmbDrives_SelectedIndexChanged;
            // 
            // dbTreeView1
            // 
            dbTreeView1.About = "";
            dbTreeView1.AllowLoadXML = false;
            dbTreeView1.AllowSaveXML = true;
            dbTreeView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dbTreeView1.DataControl = null;
            dbTreeView1.EnableReArrange = false;
            dbTreeView1.HideSelection = true;
            dbTreeView1.HotTracking = false;
            dbTreeView1.Level = 0;
            dbTreeView1.Location = new System.Drawing.Point(0, 42);
            dbTreeView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            dbTreeView1.Name = "dbTreeView1";
            dbTreeView1.SelectedNode = null;
            dbTreeView1.ShowLines = true;
            dbTreeView1.ShowRootLines = true;
            dbTreeView1.Size = new System.Drawing.Size(318, 269);
            dbTreeView1.TabIndex = 27;
            dbTreeView1.NodeMouseClick += dbTreeView1_NodeMouseClick;
            dbTreeView1.NodeMouseDoubleClick += dbTreeView1_NodeMouseDoubleClick;
            // 
            // DBFileExplorer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(cmbDrives);
            Controls.Add(dbTreeView1);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "DBFileExplorer";
            Size = new System.Drawing.Size(318, 315);
            ((System.ComponentModel.ISupportInitialize)dbTreeView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDrives;
        private DBTreeView dbTreeView1;
    }
}
