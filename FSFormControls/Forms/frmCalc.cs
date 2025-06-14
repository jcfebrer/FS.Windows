#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmCalc : Form
    {
        public DBTextBox m_dbtextbox;

        public double Value => DbCalculator.Value;

        public DBTextBox DBTextbox
        {
            get { return m_dbtextbox; }
            set { m_dbtextbox = value; }
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            if(m_dbtextbox != null)
                m_dbtextbox.Text = Convert.ToString(DbCalculator.Value);
            Close();
        }


        private void frmCalc_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal DBCalculator DbCalculator;
        internal Label Label1;

        public frmCalc()
        {
            InitializeComponent();

            Deactivate += frmCalc_Deactivate;
            Label1.Click += Label1_Click;

            DbCalculator.Initialize();
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
            DbCalculator = new DBCalculator();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.AutoSize = true;
            Label1.BackColor = SystemColors.Control;
            Label1.Cursor = Cursors.Hand;
            Label1.ForeColor = Color.Blue;
            Label1.Location = new Point(125, 1);
            Label1.Name = "Label1";
            Label1.Size = new Size(21, 13);
            Label1.TabIndex = 1;
            Label1.Text = "Ok";
            // 
            // DbCalculator
            // 
            DbCalculator.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                   | AnchorStyles.Left
                                                   | AnchorStyles.Right;
            DbCalculator.BackColor = SystemColors.Control;
            DbCalculator.ButtonColor = SystemColors.Control;
            DbCalculator.ButtonSeparation = 3;
            DbCalculator.EurValue = 166.386D;
            DbCalculator.Location = new Point(8, 12);
            DbCalculator.Name = "DbCalculator";
            DbCalculator.Size = new Size(134, 205);
            DbCalculator.TabIndex = 0;
            DbCalculator.TextColor = Color.Blue;
            // 
            // frmCalc
            // 
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = SystemColors.Control;
            ClientSize = new Size(147, 221);
            ControlBox = false;
            Controls.Add(Label1);
            Controls.Add(DbCalculator);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCalc";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Calculadora";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}