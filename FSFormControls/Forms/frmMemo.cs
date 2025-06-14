#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmMemo : Form
    {
        public DBTextBox m_DBTextbox;

        public string Value => DbTextBox1.Text;

        public DBTextBox DBTextbox
        {
            get { return m_DBTextbox; }
            set
            {
                m_DBTextbox = value;
                DbTextBox1.Text = m_DBTextbox.Text;
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            m_DBTextbox.Text = DbTextBox1.Text;
            Close();
        }


        private void frmMemo_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        internal DBTextBox DbTextBox1;
        internal Label Label1;

        public frmMemo()
        {
            InitializeComponent();

            Label1.Click += Label1_Click;
            Deactivate += frmMemo_Deactivate;
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
            DbTextBox1 = new DBTextBox();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.AutoSize = true;
            Label1.BackColor = SystemColors.Control;
            Label1.Cursor = Cursors.Hand;
            Label1.ForeColor = Color.Blue;
            Label1.Location = new Point(388, 4);
            Label1.Name = "Label1";
            Label1.Size = new Size(21, 13);
            Label1.TabIndex = 1;
            Label1.Text = "Ok";
            // 
            // DbTextBox1
            // 
            DbTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                 | AnchorStyles.Left
                                                 | AnchorStyles.Right;
            DbTextBox1.AsociatedCombo = null;
            DbTextBox1.AsociatedDBFindTextBox = null;
            DbTextBox1.BackColorRead = Color.WhiteSmoke;
            DbTextBox1.BorderStyle = BorderStyle.Fixed3D;
            DbTextBox1.Capitalize = DBTextBox.TypeString.Normal;
            DbTextBox1.DataControl = null;
            DbTextBox1.DataType = DBTextBox.TypeData.All;
            DbTextBox1.DateFormat = "dd/MM/yyyy";
            DbTextBox1.DBField = null;
            DbTextBox1.DBFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DbTextBox1.Decimals = 0;
            DbTextBox1.DefaultValue = "";
            DbTextBox1.DotNumber = false;
            DbTextBox1.Editable = true;
            DbTextBox1.Encrypted = false;
            DbTextBox1.Expression = "";
            DbTextBox1.FormatString = "";
            DbTextBox1.GridOperation = DBColumn.OperationTypes.Sum;
            DbTextBox1.Location = new Point(2, 20);
            DbTextBox1.MaskInput = null;
            DbTextBox1.MaxLength = 32767;
            DbTextBox1.MaxValue = decimal.MaxValue;
            DbTextBox1.Mode = Global.AccessMode.WriteMode;
            DbTextBox1.Multiline = true;
            DbTextBox1.Name = "DbTextBox1";
            DbTextBox1.Obligatory = false;
            DbTextBox1.PasswordChar = '\0';
            DbTextBox1.ReadOnly = false;
            DbTextBox1.ShowScrollBars = ScrollBars.Both;
            DbTextBox1.Shadow = false;
            DbTextBox1.ShadowColor = Color.Gray;
            DbTextBox1.ShadowSize = 4;
            DbTextBox1.ShowAsCombo = false;
            DbTextBox1.ShowKeyboard = false;
            DbTextBox1.Size = new Size(407, 166);
            DbTextBox1.TabIndex = 3;
            DbTextBox1.TextAlign = HorizontalAlignment.Left;
            DbTextBox1.ToolTip = "";
            DbTextBox1.XMLName = null;
            // 
            // frmMemo
            // 
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = SystemColors.Control;
            ClientSize = new Size(411, 189);
            ControlBox = false;
            Controls.Add(DbTextBox1);
            Controls.Add(Label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMemo";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Memo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}