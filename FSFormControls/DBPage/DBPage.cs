#region

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class DBPage : DBUserControl
    {
        public int m_Index;
        public string m_Text = "";

        public int Index
        {
            get { return m_Index; }
            set { m_Index = value; }
        }

        public override string Text
        {
            get { return m_Text; }
            set
            {
                m_Text = value;
                lblTitle.Text = value;
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal Label lblTitle;

        public DBPage()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
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
            lblTitle = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.BorderStyle = BorderStyle.FixedSingle;
            lblTitle.Dock = DockStyle.Bottom;
            lblTitle.Location = new Point(0, 130);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(147, 16);
            lblTitle.TabIndex = 0;
            // 
            // DBPage
            // 
            Controls.Add(lblTitle);
            Name = "DBPage";
            Size = new Size(147, 146);
            ResumeLayout(false);
        }

        #endregion
    }
}