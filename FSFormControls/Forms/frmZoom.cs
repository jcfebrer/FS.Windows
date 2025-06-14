#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmZoom : Form
    {
        public string m_ZoomText;

        public string ZoomText
        {
            get { return m_ZoomText; }
            set
            {
                m_ZoomText = value;
                DbTextBox1.Text = value;
            }
        }

        public int MaxLength
        {
            get { return DbTextBox1.MaxLength; }
            set { DbTextBox1.MaxLength = value; }
        }

        private void frmListView_Load(object sender, EventArgs e)
        {
            FunctionsForms.Center(this);
            DbTextBox1.Focus();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            m_ZoomText = DbTextBox1.Text;
            Close();
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            m_ZoomText = null;
            Close();
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal DBTextBox DbTextBox1;
        internal DBButton cmdClose;
        internal DBButton cmdSave;

        public frmZoom()
        {
            InitializeComponent();

            Load += frmListView_Load;
            cmdSave.Click += cmdSave_Click;
            cmdClose.Click += cmdClose_Click;
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
            cmdClose = new DBButton();
            DbTextBox1 = new DBTextBox();
            cmdSave = new DBButton();
            SuspendLayout();
            // 
            // cmdClose
            // 
            cmdClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdClose.ButtonStyle = DBButton.ButtonStyleType.Normal;
            cmdClose.DropDownMenu = null;
            cmdClose.FillColorEnd = Color.White;
            cmdClose.FillColorStart = Color.LightGray;
            cmdClose.FillHoverColorEnd = Color.Beige;
            cmdClose.FillHoverColorStart = Color.Beige;
            cmdClose.FlatStyle = FlatStyle.Standard;
            cmdClose.Gradient = false;
            cmdClose.GradientMode = LinearGradientMode.Horizontal;
            cmdClose.Image = null;
            cmdClose.ImageAlign = ContentAlignment.MiddleCenter;
            cmdClose.Location = new Point(488, 168);
            cmdClose.Name = "cmdClose";
            cmdClose.Size = new Size(76, 20);
            cmdClose.TabIndex = 1;
            cmdClose.Text = "Cerrar";
            cmdClose.TextAlign = ContentAlignment.MiddleCenter;
            cmdClose.TextColorEnd = Color.Black;
            cmdClose.TextColorStart = Color.Blue;
            cmdClose.TextFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdClose.ToolTip = "";
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
            DbTextBox1.DataType = DBTextBox.TypeData.All;
            DbTextBox1.DateFormat = "dd/MM/yyyy";
            DbTextBox1.DBField = null;
            DbTextBox1.DBFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DbTextBox1.Decimals = 0;
            DbTextBox1.DefaultValue = null;
            DbTextBox1.DotNumber = false;
            DbTextBox1.Editable = true;
            DbTextBox1.Encrypted = false;
            DbTextBox1.Expression = "";
            DbTextBox1.FormatString = "";
            DbTextBox1.GridOperation = DBColumn.OperationTypes.Sum;
            DbTextBox1.Location = new Point(16, 16);
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
            DbTextBox1.Size = new Size(548, 148);
            DbTextBox1.TabIndex = 2;
            DbTextBox1.TextAlign = HorizontalAlignment.Left;
            DbTextBox1.ToolTip = "";
            DbTextBox1.XMLName = null;
            // 
            // cmdSave
            // 
            cmdSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdSave.ButtonStyle = DBButton.ButtonStyleType.Normal;
            cmdSave.FillColorEnd = Color.White;
            cmdSave.FillColorStart = Color.LightGray;
            cmdSave.FillHoverColorEnd = Color.Beige;
            cmdSave.FillHoverColorStart = Color.Beige;
            cmdSave.FlatStyle = FlatStyle.Standard;
            cmdSave.Gradient = false;
            cmdSave.GradientMode = LinearGradientMode.Horizontal;
            cmdSave.Image = null;
            cmdSave.ImageAlign = ContentAlignment.MiddleCenter;
            cmdSave.Location = new Point(400, 168);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new Size(76, 20);
            cmdSave.TabIndex = 3;
            cmdSave.Text = "Guardar";
            cmdSave.TextAlign = ContentAlignment.MiddleCenter;
            cmdSave.TextColorEnd = Color.Black;
            cmdSave.TextColorStart = Color.Blue;
            cmdSave.TextFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdSave.ToolTip = "";
            // 
            // frmZoom
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(580, 202);
            Controls.Add(cmdSave);
            Controls.Add(cmdClose);
            Controls.Add(DbTextBox1);
            Name = "frmZoom";
            ShowInTaskbar = false;
            Text = "Zoom";
            ResumeLayout(false);
        }

        #endregion
    }
}