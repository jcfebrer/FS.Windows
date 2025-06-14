#region

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    internal class frmGlobalEdit : Form
    {
        #region '"Windows Form Designer generated code "' 

        public GroupBox Frame1;
        public GroupBox Frame2;
        public GroupBox Frame3;
        public GroupBox Frame4;
        public Label Label1;
        public Label Label10;
        public Label Label11;
        public Label Label12;
        public Label Label2;
        public Label Label3;
        public Label Label4;
        public Label Label5;
        public Label Label6;
        public Label Label7;
        public Label Label8;
        public Label Label9;
        public ToolTip ToolTip1;
        public Button cmdAceptar;
        public Button cmdAplicar;
        public Button cmdCancelar;
        public Button cmdDBLabelBackColorSelect;
        public Button cmdDBLabelFontSelect;
        public Button cmdDBLabelForeColorSelect;
        public Button cmdDBTextBoxBackColorSelect;
        public Button cmdDBTextBoxFontSelect;
        public Button cmdDBTextBoxForeColorSelect;
        public Button cmdLabelBackColorSelect;
        public Button cmdLabelFontSelect;
        public Button cmdLabelForeColorSelect;
        public Button cmdTextBoxBackColorSelect;
        public Button cmdTextBoxFontSelect;
        public Button cmdTextBoxForeColorSelect;
        private IContainer components;
        public TextBox txtDBLabelBackColor;
        public TextBox txtDBLabelFont;
        public TextBox txtDBLabelForeColor;
        public TextBox txtDBTextBoxBackColor;
        public TextBox txtDBTextBoxFont;
        public TextBox txtDBTextBoxForeColor;
        public TextBox txtLabelBackColor;
        public TextBox txtLabelFont;
        public TextBox txtLabelForeColor;
        public TextBox txtTextBoxBackColor;
        public TextBox txtTextBoxFont;
        public TextBox txtTextBoxForeColor;

        public frmGlobalEdit()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
                if (!(components == null))
                    components.Dispose();
            base.Dispose(Disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            ToolTip1 = new ToolTip(components);
            cmdCancelar = new Button();
            Frame4 = new GroupBox();
            txtLabelFont = new TextBox();
            txtLabelBackColor = new TextBox();
            txtLabelForeColor = new TextBox();
            cmdLabelFontSelect = new Button();
            cmdLabelBackColorSelect = new Button();
            cmdLabelForeColorSelect = new Button();
            Label12 = new Label();
            Label11 = new Label();
            Label10 = new Label();
            Frame3 = new GroupBox();
            txtDBLabelForeColor = new TextBox();
            txtDBLabelBackColor = new TextBox();
            txtDBLabelFont = new TextBox();
            cmdDBLabelFontSelect = new Button();
            cmdDBLabelBackColorSelect = new Button();
            cmdDBLabelForeColorSelect = new Button();
            Label9 = new Label();
            Label8 = new Label();
            Label7 = new Label();
            cmdAceptar = new Button();
            Frame2 = new GroupBox();
            cmdDBTextBoxForeColorSelect = new Button();
            cmdDBTextBoxBackColorSelect = new Button();
            cmdDBTextBoxFontSelect = new Button();
            txtDBTextBoxFont = new TextBox();
            txtDBTextBoxBackColor = new TextBox();
            txtDBTextBoxForeColor = new TextBox();
            Label6 = new Label();
            Label5 = new Label();
            Label4 = new Label();
            Frame1 = new GroupBox();
            cmdTextBoxForeColorSelect = new Button();
            cmdTextBoxBackColorSelect = new Button();
            cmdTextBoxFontSelect = new Button();
            txtTextBoxForeColor = new TextBox();
            txtTextBoxBackColor = new TextBox();
            txtTextBoxFont = new TextBox();
            Label3 = new Label();
            Label2 = new Label();
            Label1 = new Label();
            cmdAplicar = new Button();
            Frame4.SuspendLayout();
            Frame3.SuspendLayout();
            Frame2.SuspendLayout();
            Frame1.SuspendLayout();
            SuspendLayout();
            // 
            // cmdCancelar
            // 
            cmdCancelar.BackColor = SystemColors.Control;
            cmdCancelar.Cursor = Cursors.Default;
            cmdCancelar.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdCancelar.ForeColor = SystemColors.ControlText;
            cmdCancelar.Location = new Point(108, 260);
            cmdCancelar.Name = "cmdCancelar";
            cmdCancelar.RightToLeft = RightToLeft.No;
            cmdCancelar.Size = new Size(77, 28);
            cmdCancelar.TabIndex = 42;
            cmdCancelar.Text = "Cancelar";
            cmdCancelar.UseVisualStyleBackColor = false;
            // 
            // Frame4
            // 
            Frame4.BackColor = SystemColors.Control;
            Frame4.Controls.Add(txtLabelFont);
            Frame4.Controls.Add(txtLabelBackColor);
            Frame4.Controls.Add(txtLabelForeColor);
            Frame4.Controls.Add(cmdLabelFontSelect);
            Frame4.Controls.Add(cmdLabelBackColorSelect);
            Frame4.Controls.Add(cmdLabelForeColorSelect);
            Frame4.Controls.Add(Label12);
            Frame4.Controls.Add(Label11);
            Frame4.Controls.Add(Label10);
            Frame4.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Frame4.ForeColor = SystemColors.ControlText;
            Frame4.Location = new Point(8, 139);
            Frame4.Name = "Frame4";
            Frame4.RightToLeft = RightToLeft.No;
            Frame4.Size = new Size(219, 114);
            Frame4.TabIndex = 32;
            Frame4.TabStop = false;
            Frame4.Text = "Label";
            // 
            // txtLabelFont
            // 
            txtLabelFont.AcceptsReturn = true;
            txtLabelFont.BackColor = SystemColors.Window;
            txtLabelFont.Cursor = Cursors.IBeam;
            txtLabelFont.Enabled = false;
            txtLabelFont.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLabelFont.ForeColor = SystemColors.WindowText;
            txtLabelFont.Location = new Point(75, 26);
            txtLabelFont.MaxLength = 0;
            txtLabelFont.Name = "txtLabelFont";
            txtLabelFont.RightToLeft = RightToLeft.No;
            txtLabelFont.Size = new Size(93, 21);
            txtLabelFont.TabIndex = 38;
            // 
            // txtLabelBackColor
            // 
            txtLabelBackColor.AcceptsReturn = true;
            txtLabelBackColor.BackColor = SystemColors.Window;
            txtLabelBackColor.Cursor = Cursors.IBeam;
            txtLabelBackColor.Enabled = false;
            txtLabelBackColor.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLabelBackColor.ForeColor = SystemColors.WindowText;
            txtLabelBackColor.Location = new Point(75, 52);
            txtLabelBackColor.MaxLength = 0;
            txtLabelBackColor.Name = "txtLabelBackColor";
            txtLabelBackColor.RightToLeft = RightToLeft.No;
            txtLabelBackColor.Size = new Size(93, 21);
            txtLabelBackColor.TabIndex = 37;
            // 
            // txtLabelForeColor
            // 
            txtLabelForeColor.AcceptsReturn = true;
            txtLabelForeColor.BackColor = SystemColors.Window;
            txtLabelForeColor.Cursor = Cursors.IBeam;
            txtLabelForeColor.Enabled = false;
            txtLabelForeColor.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLabelForeColor.ForeColor = SystemColors.WindowText;
            txtLabelForeColor.Location = new Point(75, 78);
            txtLabelForeColor.MaxLength = 0;
            txtLabelForeColor.Name = "txtLabelForeColor";
            txtLabelForeColor.RightToLeft = RightToLeft.No;
            txtLabelForeColor.Size = new Size(93, 21);
            txtLabelForeColor.TabIndex = 36;
            // 
            // cmdLabelFontSelect
            // 
            cmdLabelFontSelect.BackColor = SystemColors.Control;
            cmdLabelFontSelect.Cursor = Cursors.Default;
            cmdLabelFontSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdLabelFontSelect.ForeColor = SystemColors.ControlText;
            cmdLabelFontSelect.Location = new Point(175, 26);
            cmdLabelFontSelect.Name = "cmdLabelFontSelect";
            cmdLabelFontSelect.RightToLeft = RightToLeft.No;
            cmdLabelFontSelect.Size = new Size(27, 19);
            cmdLabelFontSelect.TabIndex = 35;
            cmdLabelFontSelect.Text = "...";
            cmdLabelFontSelect.UseVisualStyleBackColor = false;
            // 
            // cmdLabelBackColorSelect
            // 
            cmdLabelBackColorSelect.BackColor = SystemColors.Control;
            cmdLabelBackColorSelect.Cursor = Cursors.Default;
            cmdLabelBackColorSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdLabelBackColorSelect.ForeColor = SystemColors.ControlText;
            cmdLabelBackColorSelect.Location = new Point(175, 52);
            cmdLabelBackColorSelect.Name = "cmdLabelBackColorSelect";
            cmdLabelBackColorSelect.RightToLeft = RightToLeft.No;
            cmdLabelBackColorSelect.Size = new Size(27, 19);
            cmdLabelBackColorSelect.TabIndex = 34;
            cmdLabelBackColorSelect.Text = "...";
            cmdLabelBackColorSelect.UseVisualStyleBackColor = false;
            // 
            // cmdLabelForeColorSelect
            // 
            cmdLabelForeColorSelect.BackColor = SystemColors.Control;
            cmdLabelForeColorSelect.Cursor = Cursors.Default;
            cmdLabelForeColorSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdLabelForeColorSelect.ForeColor = SystemColors.ControlText;
            cmdLabelForeColorSelect.Location = new Point(175, 78);
            cmdLabelForeColorSelect.Name = "cmdLabelForeColorSelect";
            cmdLabelForeColorSelect.RightToLeft = RightToLeft.No;
            cmdLabelForeColorSelect.Size = new Size(27, 19);
            cmdLabelForeColorSelect.TabIndex = 33;
            cmdLabelForeColorSelect.Text = "...";
            cmdLabelForeColorSelect.UseVisualStyleBackColor = false;
            // 
            // Label12
            // 
            Label12.AutoSize = true;
            Label12.BackColor = SystemColors.Control;
            Label12.Cursor = Cursors.Default;
            Label12.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label12.ForeColor = SystemColors.ControlText;
            Label12.Location = new Point(8, 26);
            Label12.Name = "Label12";
            Label12.RightToLeft = RightToLeft.No;
            Label12.Size = new Size(28, 14);
            Label12.TabIndex = 41;
            Label12.Text = "Font";
            // 
            // Label11
            // 
            Label11.AutoSize = true;
            Label11.BackColor = SystemColors.Control;
            Label11.Cursor = Cursors.Default;
            Label11.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label11.ForeColor = SystemColors.ControlText;
            Label11.Location = new Point(8, 52);
            Label11.Name = "Label11";
            Label11.RightToLeft = RightToLeft.No;
            Label11.Size = new Size(56, 14);
            Label11.TabIndex = 40;
            Label11.Text = "BackColor";
            // 
            // Label10
            // 
            Label10.AutoSize = true;
            Label10.BackColor = SystemColors.Control;
            Label10.Cursor = Cursors.Default;
            Label10.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label10.ForeColor = SystemColors.ControlText;
            Label10.Location = new Point(8, 78);
            Label10.Name = "Label10";
            Label10.RightToLeft = RightToLeft.No;
            Label10.Size = new Size(54, 14);
            Label10.TabIndex = 39;
            Label10.Text = "ForeColor";
            // 
            // Frame3
            // 
            Frame3.BackColor = SystemColors.Control;
            Frame3.Controls.Add(txtDBLabelForeColor);
            Frame3.Controls.Add(txtDBLabelBackColor);
            Frame3.Controls.Add(txtDBLabelFont);
            Frame3.Controls.Add(cmdDBLabelFontSelect);
            Frame3.Controls.Add(cmdDBLabelBackColorSelect);
            Frame3.Controls.Add(cmdDBLabelForeColorSelect);
            Frame3.Controls.Add(Label9);
            Frame3.Controls.Add(Label8);
            Frame3.Controls.Add(Label7);
            Frame3.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Frame3.ForeColor = SystemColors.ControlText;
            Frame3.Location = new Point(233, 139);
            Frame3.Name = "Frame3";
            Frame3.RightToLeft = RightToLeft.No;
            Frame3.Size = new Size(219, 114);
            Frame3.TabIndex = 22;
            Frame3.TabStop = false;
            Frame3.Text = "DBLabel";
            // 
            // txtDBLabelForeColor
            // 
            txtDBLabelForeColor.AcceptsReturn = true;
            txtDBLabelForeColor.BackColor = SystemColors.Window;
            txtDBLabelForeColor.Cursor = Cursors.IBeam;
            txtDBLabelForeColor.Enabled = false;
            txtDBLabelForeColor.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDBLabelForeColor.ForeColor = SystemColors.WindowText;
            txtDBLabelForeColor.Location = new Point(75, 78);
            txtDBLabelForeColor.MaxLength = 0;
            txtDBLabelForeColor.Name = "txtDBLabelForeColor";
            txtDBLabelForeColor.RightToLeft = RightToLeft.No;
            txtDBLabelForeColor.Size = new Size(93, 21);
            txtDBLabelForeColor.TabIndex = 28;
            // 
            // txtDBLabelBackColor
            // 
            txtDBLabelBackColor.AcceptsReturn = true;
            txtDBLabelBackColor.BackColor = SystemColors.Window;
            txtDBLabelBackColor.Cursor = Cursors.IBeam;
            txtDBLabelBackColor.Enabled = false;
            txtDBLabelBackColor.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDBLabelBackColor.ForeColor = SystemColors.WindowText;
            txtDBLabelBackColor.Location = new Point(75, 52);
            txtDBLabelBackColor.MaxLength = 0;
            txtDBLabelBackColor.Name = "txtDBLabelBackColor";
            txtDBLabelBackColor.RightToLeft = RightToLeft.No;
            txtDBLabelBackColor.Size = new Size(93, 21);
            txtDBLabelBackColor.TabIndex = 27;
            // 
            // txtDBLabelFont
            // 
            txtDBLabelFont.AcceptsReturn = true;
            txtDBLabelFont.BackColor = SystemColors.Window;
            txtDBLabelFont.Cursor = Cursors.IBeam;
            txtDBLabelFont.Enabled = false;
            txtDBLabelFont.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDBLabelFont.ForeColor = SystemColors.WindowText;
            txtDBLabelFont.Location = new Point(75, 26);
            txtDBLabelFont.MaxLength = 0;
            txtDBLabelFont.Name = "txtDBLabelFont";
            txtDBLabelFont.RightToLeft = RightToLeft.No;
            txtDBLabelFont.Size = new Size(93, 21);
            txtDBLabelFont.TabIndex = 26;
            // 
            // cmdDBLabelFontSelect
            // 
            cmdDBLabelFontSelect.BackColor = SystemColors.Control;
            cmdDBLabelFontSelect.Cursor = Cursors.Default;
            cmdDBLabelFontSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdDBLabelFontSelect.ForeColor = SystemColors.ControlText;
            cmdDBLabelFontSelect.Location = new Point(175, 26);
            cmdDBLabelFontSelect.Name = "cmdDBLabelFontSelect";
            cmdDBLabelFontSelect.RightToLeft = RightToLeft.No;
            cmdDBLabelFontSelect.Size = new Size(27, 19);
            cmdDBLabelFontSelect.TabIndex = 25;
            cmdDBLabelFontSelect.Text = "...";
            cmdDBLabelFontSelect.UseVisualStyleBackColor = false;
            // 
            // cmdDBLabelBackColorSelect
            // 
            cmdDBLabelBackColorSelect.BackColor = SystemColors.Control;
            cmdDBLabelBackColorSelect.Cursor = Cursors.Default;
            cmdDBLabelBackColorSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdDBLabelBackColorSelect.ForeColor = SystemColors.ControlText;
            cmdDBLabelBackColorSelect.Location = new Point(175, 52);
            cmdDBLabelBackColorSelect.Name = "cmdDBLabelBackColorSelect";
            cmdDBLabelBackColorSelect.RightToLeft = RightToLeft.No;
            cmdDBLabelBackColorSelect.Size = new Size(27, 19);
            cmdDBLabelBackColorSelect.TabIndex = 24;
            cmdDBLabelBackColorSelect.Text = "...";
            cmdDBLabelBackColorSelect.UseVisualStyleBackColor = false;
            // 
            // cmdDBLabelForeColorSelect
            // 
            cmdDBLabelForeColorSelect.BackColor = SystemColors.Control;
            cmdDBLabelForeColorSelect.Cursor = Cursors.Default;
            cmdDBLabelForeColorSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdDBLabelForeColorSelect.ForeColor = SystemColors.ControlText;
            cmdDBLabelForeColorSelect.Location = new Point(175, 78);
            cmdDBLabelForeColorSelect.Name = "cmdDBLabelForeColorSelect";
            cmdDBLabelForeColorSelect.RightToLeft = RightToLeft.No;
            cmdDBLabelForeColorSelect.Size = new Size(27, 19);
            cmdDBLabelForeColorSelect.TabIndex = 23;
            cmdDBLabelForeColorSelect.Text = "...";
            cmdDBLabelForeColorSelect.UseVisualStyleBackColor = false;
            // 
            // Label9
            // 
            Label9.AutoSize = true;
            Label9.BackColor = SystemColors.Control;
            Label9.Cursor = Cursors.Default;
            Label9.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label9.ForeColor = SystemColors.ControlText;
            Label9.Location = new Point(8, 78);
            Label9.Name = "Label9";
            Label9.RightToLeft = RightToLeft.No;
            Label9.Size = new Size(54, 14);
            Label9.TabIndex = 31;
            Label9.Text = "ForeColor";
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.BackColor = SystemColors.Control;
            Label8.Cursor = Cursors.Default;
            Label8.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label8.ForeColor = SystemColors.ControlText;
            Label8.Location = new Point(8, 52);
            Label8.Name = "Label8";
            Label8.RightToLeft = RightToLeft.No;
            Label8.Size = new Size(56, 14);
            Label8.TabIndex = 30;
            Label8.Text = "BackColor";
            // 
            // Label7
            // 
            Label7.AutoSize = true;
            Label7.BackColor = SystemColors.Control;
            Label7.Cursor = Cursors.Default;
            Label7.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label7.ForeColor = SystemColors.ControlText;
            Label7.Location = new Point(8, 26);
            Label7.Name = "Label7";
            Label7.RightToLeft = RightToLeft.No;
            Label7.Size = new Size(28, 14);
            Label7.TabIndex = 29;
            Label7.Text = "Font";
            // 
            // cmdAceptar
            // 
            cmdAceptar.BackColor = SystemColors.Control;
            cmdAceptar.Cursor = Cursors.Default;
            cmdAceptar.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdAceptar.ForeColor = SystemColors.ControlText;
            cmdAceptar.Location = new Point(275, 260);
            cmdAceptar.Name = "cmdAceptar";
            cmdAceptar.RightToLeft = RightToLeft.No;
            cmdAceptar.Size = new Size(77, 28);
            cmdAceptar.TabIndex = 21;
            cmdAceptar.Text = "Aceptar";
            cmdAceptar.UseVisualStyleBackColor = false;
            // 
            // Frame2
            // 
            Frame2.BackColor = SystemColors.Control;
            Frame2.Controls.Add(cmdDBTextBoxForeColorSelect);
            Frame2.Controls.Add(cmdDBTextBoxBackColorSelect);
            Frame2.Controls.Add(cmdDBTextBoxFontSelect);
            Frame2.Controls.Add(txtDBTextBoxFont);
            Frame2.Controls.Add(txtDBTextBoxBackColor);
            Frame2.Controls.Add(txtDBTextBoxForeColor);
            Frame2.Controls.Add(Label6);
            Frame2.Controls.Add(Label5);
            Frame2.Controls.Add(Label4);
            Frame2.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Frame2.ForeColor = SystemColors.ControlText;
            Frame2.Location = new Point(233, 17);
            Frame2.Name = "Frame2";
            Frame2.RightToLeft = RightToLeft.No;
            Frame2.Size = new Size(219, 115);
            Frame2.TabIndex = 8;
            Frame2.TabStop = false;
            Frame2.Text = "DBTextBox";
            // 
            // cmdDBTextBoxForeColorSelect
            // 
            cmdDBTextBoxForeColorSelect.BackColor = SystemColors.Control;
            cmdDBTextBoxForeColorSelect.Cursor = Cursors.Default;
            cmdDBTextBoxForeColorSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdDBTextBoxForeColorSelect.ForeColor = SystemColors.ControlText;
            cmdDBTextBoxForeColorSelect.Location = new Point(175, 78);
            cmdDBTextBoxForeColorSelect.Name = "cmdDBTextBoxForeColorSelect";
            cmdDBTextBoxForeColorSelect.RightToLeft = RightToLeft.No;
            cmdDBTextBoxForeColorSelect.Size = new Size(27, 19);
            cmdDBTextBoxForeColorSelect.TabIndex = 20;
            cmdDBTextBoxForeColorSelect.Text = "...";
            cmdDBTextBoxForeColorSelect.UseVisualStyleBackColor = false;
            // 
            // cmdDBTextBoxBackColorSelect
            // 
            cmdDBTextBoxBackColorSelect.BackColor = SystemColors.Control;
            cmdDBTextBoxBackColorSelect.Cursor = Cursors.Default;
            cmdDBTextBoxBackColorSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdDBTextBoxBackColorSelect.ForeColor = SystemColors.ControlText;
            cmdDBTextBoxBackColorSelect.Location = new Point(175, 52);
            cmdDBTextBoxBackColorSelect.Name = "cmdDBTextBoxBackColorSelect";
            cmdDBTextBoxBackColorSelect.RightToLeft = RightToLeft.No;
            cmdDBTextBoxBackColorSelect.Size = new Size(27, 19);
            cmdDBTextBoxBackColorSelect.TabIndex = 19;
            cmdDBTextBoxBackColorSelect.Text = "...";
            cmdDBTextBoxBackColorSelect.UseVisualStyleBackColor = false;
            // 
            // cmdDBTextBoxFontSelect
            // 
            cmdDBTextBoxFontSelect.BackColor = SystemColors.Control;
            cmdDBTextBoxFontSelect.Cursor = Cursors.Default;
            cmdDBTextBoxFontSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdDBTextBoxFontSelect.ForeColor = SystemColors.ControlText;
            cmdDBTextBoxFontSelect.Location = new Point(175, 26);
            cmdDBTextBoxFontSelect.Name = "cmdDBTextBoxFontSelect";
            cmdDBTextBoxFontSelect.RightToLeft = RightToLeft.No;
            cmdDBTextBoxFontSelect.Size = new Size(27, 19);
            cmdDBTextBoxFontSelect.TabIndex = 18;
            cmdDBTextBoxFontSelect.Text = "...";
            cmdDBTextBoxFontSelect.UseVisualStyleBackColor = false;
            // 
            // txtDBTextBoxFont
            // 
            txtDBTextBoxFont.AcceptsReturn = true;
            txtDBTextBoxFont.BackColor = SystemColors.Window;
            txtDBTextBoxFont.Cursor = Cursors.IBeam;
            txtDBTextBoxFont.Enabled = false;
            txtDBTextBoxFont.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDBTextBoxFont.ForeColor = SystemColors.WindowText;
            txtDBTextBoxFont.Location = new Point(75, 26);
            txtDBTextBoxFont.MaxLength = 0;
            txtDBTextBoxFont.Name = "txtDBTextBoxFont";
            txtDBTextBoxFont.RightToLeft = RightToLeft.No;
            txtDBTextBoxFont.Size = new Size(93, 21);
            txtDBTextBoxFont.TabIndex = 11;
            // 
            // txtDBTextBoxBackColor
            // 
            txtDBTextBoxBackColor.AcceptsReturn = true;
            txtDBTextBoxBackColor.BackColor = SystemColors.Window;
            txtDBTextBoxBackColor.Cursor = Cursors.IBeam;
            txtDBTextBoxBackColor.Enabled = false;
            txtDBTextBoxBackColor.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDBTextBoxBackColor.ForeColor = SystemColors.WindowText;
            txtDBTextBoxBackColor.Location = new Point(75, 52);
            txtDBTextBoxBackColor.MaxLength = 0;
            txtDBTextBoxBackColor.Name = "txtDBTextBoxBackColor";
            txtDBTextBoxBackColor.RightToLeft = RightToLeft.No;
            txtDBTextBoxBackColor.Size = new Size(93, 21);
            txtDBTextBoxBackColor.TabIndex = 10;
            // 
            // txtDBTextBoxForeColor
            // 
            txtDBTextBoxForeColor.AcceptsReturn = true;
            txtDBTextBoxForeColor.BackColor = SystemColors.Window;
            txtDBTextBoxForeColor.Cursor = Cursors.IBeam;
            txtDBTextBoxForeColor.Enabled = false;
            txtDBTextBoxForeColor.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDBTextBoxForeColor.ForeColor = SystemColors.WindowText;
            txtDBTextBoxForeColor.Location = new Point(75, 78);
            txtDBTextBoxForeColor.MaxLength = 0;
            txtDBTextBoxForeColor.Name = "txtDBTextBoxForeColor";
            txtDBTextBoxForeColor.RightToLeft = RightToLeft.No;
            txtDBTextBoxForeColor.Size = new Size(93, 21);
            txtDBTextBoxForeColor.TabIndex = 9;
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.BackColor = SystemColors.Control;
            Label6.Cursor = Cursors.Default;
            Label6.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label6.ForeColor = SystemColors.ControlText;
            Label6.Location = new Point(8, 26);
            Label6.Name = "Label6";
            Label6.RightToLeft = RightToLeft.No;
            Label6.Size = new Size(28, 14);
            Label6.TabIndex = 14;
            Label6.Text = "Font";
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.BackColor = SystemColors.Control;
            Label5.Cursor = Cursors.Default;
            Label5.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label5.ForeColor = SystemColors.ControlText;
            Label5.Location = new Point(8, 52);
            Label5.Name = "Label5";
            Label5.RightToLeft = RightToLeft.No;
            Label5.Size = new Size(56, 14);
            Label5.TabIndex = 13;
            Label5.Text = "BackColor";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.BackColor = SystemColors.Control;
            Label4.Cursor = Cursors.Default;
            Label4.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label4.ForeColor = SystemColors.ControlText;
            Label4.Location = new Point(8, 78);
            Label4.Name = "Label4";
            Label4.RightToLeft = RightToLeft.No;
            Label4.Size = new Size(54, 14);
            Label4.TabIndex = 12;
            Label4.Text = "ForeColor";
            // 
            // Frame1
            // 
            Frame1.BackColor = SystemColors.Control;
            Frame1.Controls.Add(cmdTextBoxForeColorSelect);
            Frame1.Controls.Add(cmdTextBoxBackColorSelect);
            Frame1.Controls.Add(cmdTextBoxFontSelect);
            Frame1.Controls.Add(txtTextBoxForeColor);
            Frame1.Controls.Add(txtTextBoxBackColor);
            Frame1.Controls.Add(txtTextBoxFont);
            Frame1.Controls.Add(Label3);
            Frame1.Controls.Add(Label2);
            Frame1.Controls.Add(Label1);
            Frame1.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Frame1.ForeColor = SystemColors.ControlText;
            Frame1.Location = new Point(8, 17);
            Frame1.Name = "Frame1";
            Frame1.RightToLeft = RightToLeft.No;
            Frame1.Size = new Size(219, 115);
            Frame1.TabIndex = 1;
            Frame1.TabStop = false;
            Frame1.Text = "TextBox";
            // 
            // cmdTextBoxForeColorSelect
            // 
            cmdTextBoxForeColorSelect.BackColor = SystemColors.Control;
            cmdTextBoxForeColorSelect.Cursor = Cursors.Default;
            cmdTextBoxForeColorSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdTextBoxForeColorSelect.ForeColor = SystemColors.ControlText;
            cmdTextBoxForeColorSelect.Location = new Point(175, 78);
            cmdTextBoxForeColorSelect.Name = "cmdTextBoxForeColorSelect";
            cmdTextBoxForeColorSelect.RightToLeft = RightToLeft.No;
            cmdTextBoxForeColorSelect.Size = new Size(27, 19);
            cmdTextBoxForeColorSelect.TabIndex = 17;
            cmdTextBoxForeColorSelect.Text = "...";
            cmdTextBoxForeColorSelect.UseVisualStyleBackColor = false;
            // 
            // cmdTextBoxBackColorSelect
            // 
            cmdTextBoxBackColorSelect.BackColor = SystemColors.Control;
            cmdTextBoxBackColorSelect.Cursor = Cursors.Default;
            cmdTextBoxBackColorSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdTextBoxBackColorSelect.ForeColor = SystemColors.ControlText;
            cmdTextBoxBackColorSelect.Location = new Point(175, 52);
            cmdTextBoxBackColorSelect.Name = "cmdTextBoxBackColorSelect";
            cmdTextBoxBackColorSelect.RightToLeft = RightToLeft.No;
            cmdTextBoxBackColorSelect.Size = new Size(27, 19);
            cmdTextBoxBackColorSelect.TabIndex = 16;
            cmdTextBoxBackColorSelect.Text = "...";
            cmdTextBoxBackColorSelect.UseVisualStyleBackColor = false;
            // 
            // cmdTextBoxFontSelect
            // 
            cmdTextBoxFontSelect.BackColor = SystemColors.Control;
            cmdTextBoxFontSelect.Cursor = Cursors.Default;
            cmdTextBoxFontSelect.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdTextBoxFontSelect.ForeColor = SystemColors.ControlText;
            cmdTextBoxFontSelect.Location = new Point(175, 26);
            cmdTextBoxFontSelect.Name = "cmdTextBoxFontSelect";
            cmdTextBoxFontSelect.RightToLeft = RightToLeft.No;
            cmdTextBoxFontSelect.Size = new Size(27, 19);
            cmdTextBoxFontSelect.TabIndex = 15;
            cmdTextBoxFontSelect.Text = "...";
            cmdTextBoxFontSelect.UseVisualStyleBackColor = false;
            // 
            // txtTextBoxForeColor
            // 
            txtTextBoxForeColor.AcceptsReturn = true;
            txtTextBoxForeColor.BackColor = SystemColors.Window;
            txtTextBoxForeColor.Cursor = Cursors.IBeam;
            txtTextBoxForeColor.Enabled = false;
            txtTextBoxForeColor.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTextBoxForeColor.ForeColor = SystemColors.WindowText;
            txtTextBoxForeColor.Location = new Point(75, 78);
            txtTextBoxForeColor.MaxLength = 0;
            txtTextBoxForeColor.Name = "txtTextBoxForeColor";
            txtTextBoxForeColor.RightToLeft = RightToLeft.No;
            txtTextBoxForeColor.Size = new Size(93, 21);
            txtTextBoxForeColor.TabIndex = 7;
            // 
            // txtTextBoxBackColor
            // 
            txtTextBoxBackColor.AcceptsReturn = true;
            txtTextBoxBackColor.BackColor = SystemColors.Window;
            txtTextBoxBackColor.Cursor = Cursors.IBeam;
            txtTextBoxBackColor.Enabled = false;
            txtTextBoxBackColor.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTextBoxBackColor.ForeColor = SystemColors.WindowText;
            txtTextBoxBackColor.Location = new Point(75, 52);
            txtTextBoxBackColor.MaxLength = 0;
            txtTextBoxBackColor.Name = "txtTextBoxBackColor";
            txtTextBoxBackColor.RightToLeft = RightToLeft.No;
            txtTextBoxBackColor.Size = new Size(93, 21);
            txtTextBoxBackColor.TabIndex = 6;
            // 
            // txtTextBoxFont
            // 
            txtTextBoxFont.AcceptsReturn = true;
            txtTextBoxFont.BackColor = SystemColors.Window;
            txtTextBoxFont.Cursor = Cursors.IBeam;
            txtTextBoxFont.Enabled = false;
            txtTextBoxFont.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTextBoxFont.ForeColor = SystemColors.WindowText;
            txtTextBoxFont.Location = new Point(75, 26);
            txtTextBoxFont.MaxLength = 0;
            txtTextBoxFont.Name = "txtTextBoxFont";
            txtTextBoxFont.RightToLeft = RightToLeft.No;
            txtTextBoxFont.Size = new Size(93, 21);
            txtTextBoxFont.TabIndex = 5;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.BackColor = SystemColors.Control;
            Label3.Cursor = Cursors.Default;
            Label3.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.ForeColor = SystemColors.ControlText;
            Label3.Location = new Point(8, 78);
            Label3.Name = "Label3";
            Label3.RightToLeft = RightToLeft.No;
            Label3.Size = new Size(54, 14);
            Label3.TabIndex = 4;
            Label3.Text = "ForeColor";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.BackColor = SystemColors.Control;
            Label2.Cursor = Cursors.Default;
            Label2.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label2.ForeColor = SystemColors.ControlText;
            Label2.Location = new Point(8, 52);
            Label2.Name = "Label2";
            Label2.RightToLeft = RightToLeft.No;
            Label2.Size = new Size(56, 14);
            Label2.TabIndex = 3;
            Label2.Text = "BackColor";
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.BackColor = SystemColors.Control;
            Label1.Cursor = Cursors.Default;
            Label1.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label1.ForeColor = SystemColors.ControlText;
            Label1.Location = new Point(8, 26);
            Label1.Name = "Label1";
            Label1.RightToLeft = RightToLeft.No;
            Label1.Size = new Size(28, 14);
            Label1.TabIndex = 2;
            Label1.Text = "Font";
            // 
            // cmdAplicar
            // 
            cmdAplicar.BackColor = SystemColors.Control;
            cmdAplicar.Cursor = Cursors.Default;
            cmdAplicar.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdAplicar.ForeColor = SystemColors.ControlText;
            cmdAplicar.Location = new Point(192, 260);
            cmdAplicar.Name = "cmdAplicar";
            cmdAplicar.RightToLeft = RightToLeft.No;
            cmdAplicar.Size = new Size(76, 28);
            cmdAplicar.TabIndex = 0;
            cmdAplicar.Text = "Aplicar";
            cmdAplicar.UseVisualStyleBackColor = false;
            // 
            // frmGlobalEdit
            // 
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = SystemColors.Control;
            ClientSize = new Size(466, 298);
            Controls.Add(cmdCancelar);
            Controls.Add(Frame4);
            Controls.Add(Frame3);
            Controls.Add(cmdAceptar);
            Controls.Add(Frame2);
            Controls.Add(Frame1);
            Controls.Add(cmdAplicar);
            Cursor = Cursors.Default;
            Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Location = new Point(5, 29);
            Name = "frmGlobalEdit";
            RightToLeft = RightToLeft.No;
            Text = "Configuración formulario";
            Frame4.ResumeLayout(false);
            Frame4.PerformLayout();
            Frame3.ResumeLayout(false);
            Frame3.PerformLayout();
            Frame2.ResumeLayout(false);
            Frame2.PerformLayout();
            Frame1.ResumeLayout(false);
            Frame1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}