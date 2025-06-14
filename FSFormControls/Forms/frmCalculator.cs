#region

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmCalculator : Form
    {
        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal DBCalculator DbCalculator1;

        public frmCalculator()
        {
            InitializeComponent();

            DbCalculator1.Initialize();
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
            this.DbCalculator1 = new FSFormControls.DBCalculator();
            this.SuspendLayout();
            // 
            // DbCalculator1
            // 
            this.DbCalculator1.About = "";
            this.DbCalculator1.BackColor = System.Drawing.Color.Silver;
            this.DbCalculator1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DbCalculator1.ButtonSeparation = 10;
            this.DbCalculator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbCalculator1.EurValue = 166.386D;
            this.DbCalculator1.Location = new System.Drawing.Point(0, 0);
            this.DbCalculator1.Name = "DbCalculator1";
            this.DbCalculator1.Size = new System.Drawing.Size(160, 272);
            this.DbCalculator1.TabIndex = 0;
            this.DbCalculator1.TextColor = System.Drawing.Color.Black;
            // 
            // frmCalculator
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(160, 272);
            this.Controls.Add(this.DbCalculator1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmCalculator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculadora";
            this.ResumeLayout(false);

        }

        #endregion
    }
}