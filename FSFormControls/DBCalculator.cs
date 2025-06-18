#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxItem(true)]
    [Serializable]
    public class DBCalculator : DBUserControl
    {
        public bool blnClear;
        public bool blnFrstOpen = true;
        public double dblAcc;
        public double dblSec;
        public bool eurData;
        public Color m_ButtonColor = Color.LightBlue;
        public int m_ButtonSeparation = 3;
        public double m_EurValue = 166.386;
        public Color m_TextColor = Color.Black;
        public string strOper = "=";


        public double Value => dblSec;

        public double EurValue
        {
            get { return m_EurValue; }
            set { m_EurValue = value; }
        }

        public int ButtonSeparation
        {
            get { return m_ButtonSeparation; }
            set
            {
                m_ButtonSeparation = value;

                DrawCalc();
            }
        }

        public Color ButtonColor
        {
            get { return m_ButtonColor; }
            set
            {
                m_ButtonColor = value;

                DrawCalc();
            }
        }

        public Color TextColor
        {
            get { return m_TextColor; }
            set
            {
                m_TextColor = value;

                DrawCalc();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ButtonClick(((Button) sender).Text);
        }


        private void ButtonClick(string key)
        {
            if (blnClear) txtCalc.Text = "";

            txtCalc.Text += key;

            if (txtCalc.Text == ",") txtCalc.Text = "0,";

            dblSec = Convert.ToDouble(txtCalc.Text);

            blnClear = false;

            showData();
        }


        private void oper_Click(object sender, EventArgs e)
        {
            OperClick(((Button) sender).Text);
        }

        private void OperClick(string oper)
        {
            strOper = oper;
            if (blnFrstOpen)
                dblAcc = dblSec;
            else
                calc();

            blnFrstOpen = false;
            blnClear = true;
        }


        private void bClr_Click(object sender, EventArgs e)
        {
            clear();
        }


        private void bEqu_Click(object sender, EventArgs e)
        {
            calc();
        }


        private void calc()
        {
            switch (strOper)
            {
                case "+":
                    dblAcc += dblSec;
                    break;
                case "-":
                    dblAcc -= dblSec;
                    break;
                case "*":
                    dblAcc *= dblSec;
                    break;
                case "/":
                    dblAcc /= dblSec;
                    break;
            }


            strOper = "=";
            blnFrstOpen = true;
            txtCalc.Text = Convert.ToString(dblAcc);
            dblSec = dblAcc;

            showData();
        }

        private void showData()
        {
            if (eurData)
                txtCalc2.Text = Convert.ToString(dblSec * m_EurValue);
            else
                txtCalc2.Text = Convert.ToString(dblSec / m_EurValue);
        }


        private void clear()
        {
            dblAcc = 0;
            dblSec = 0;
            blnFrstOpen = true;
            txtCalc.Text = "";
            txtCalc.Focus();

            showData();
        }


        private void bEur_Click(object sender, EventArgs e)
        {
            lblPts.Text = "€";
            lblEur.Text = "Pts";
            eurData = true;

            showData();
        }


        private void bPts_Click(object sender, EventArgs e)
        {
            lblPts.Text = "Pts";
            lblEur.Text = "€";
            eurData = false;

            showData();
        }


        private void DBCalculator_Resize(object sender, EventArgs e)
        {
            DrawCalc();
        }


        private void DrawCalc()
        {
            txtCalc2.Width = Width - lblPts.Width - m_ButtonSeparation;
            txtCalc2.Left = m_ButtonSeparation;
            txtCalc2.Top = 6;

            txtCalc.Width = Width - lblPts.Width - m_ButtonSeparation;
            txtCalc.Left = m_ButtonSeparation;
            txtCalc.Top = txtCalc2.Height + 6;

            lblEur.Left = txtCalc.Left + txtCalc.Width + 2;
            lblEur.Top = 8;

            lblPts.Left = txtCalc.Left + txtCalc.Width + 2;
            lblPts.Top = 8 + txtCalc2.Height;

            double bHeight = (Height - (txtCalc.Top + txtCalc.Height + 6 + m_ButtonSeparation * 5)) / 5;
            double bWidth = (Width - m_ButtonSeparation * 5) / 4;

            bEur.Left = m_ButtonSeparation;
            bEur.Width = Convert.ToInt32(bWidth);
            bEur.Height = Convert.ToInt32(bHeight);
            bEur.Top = txtCalc.Top + txtCalc.Height + m_ButtonSeparation;
            bEur.ForeColor = m_TextColor;
            bEur.BackColor = m_ButtonColor;

            bPts.Left = Convert.ToInt32(bEur.Left + bWidth + m_ButtonSeparation);
            bPts.Width = Convert.ToInt32(bWidth);
            bPts.Height = Convert.ToInt32(bHeight);
            bPts.Top = bEur.Top;
            bPts.ForeColor = m_TextColor;
            bPts.BackColor = m_ButtonColor;

            bPercent.Left = Convert.ToInt32(bPts.Left + bWidth + m_ButtonSeparation);
            bPercent.Width = Convert.ToInt32(bWidth);
            bPercent.Height = Convert.ToInt32(bHeight);
            bPercent.Top = bEur.Top;
            bPercent.ForeColor = m_TextColor;
            bPercent.BackColor = m_ButtonColor;

            bClr.Left = Convert.ToInt32(bPercent.Left + bWidth + m_ButtonSeparation);
            bClr.Width = Convert.ToInt32(bWidth);
            bClr.Height = Convert.ToInt32(bHeight);
            bClr.Top = bEur.Top;
            bClr.ForeColor = m_TextColor;
            bClr.BackColor = m_ButtonColor;

            b7.Left = m_ButtonSeparation;
            b7.Width = Convert.ToInt32(bWidth);
            b7.Height = Convert.ToInt32(bHeight);
            b7.Top = bEur.Top + bEur.Height + m_ButtonSeparation;
            b7.ForeColor = m_TextColor;
            b7.BackColor = m_ButtonColor;

            b8.Left = Convert.ToInt32(b7.Left + bWidth + m_ButtonSeparation);
            b8.Width = Convert.ToInt32(bWidth);
            b8.Height = Convert.ToInt32(bHeight);
            b8.Top = b7.Top;
            b8.ForeColor = m_TextColor;
            b8.BackColor = m_ButtonColor;

            b9.Left = Convert.ToInt32(b8.Left + bWidth + m_ButtonSeparation);
            b9.Width = Convert.ToInt32(bWidth);
            b9.Height = Convert.ToInt32(bHeight);
            b9.Top = b8.Top;
            b9.ForeColor = m_TextColor;
            b9.BackColor = m_ButtonColor;

            bDiv.Left = Convert.ToInt32(b9.Left + bWidth + m_ButtonSeparation);
            bDiv.Width = Convert.ToInt32(bWidth);
            bDiv.Height = Convert.ToInt32(bHeight);
            bDiv.Top = b9.Top;
            bDiv.ForeColor = m_TextColor;
            bDiv.BackColor = m_ButtonColor;

            b4.Left = m_ButtonSeparation;
            b4.Width = Convert.ToInt32(bWidth);
            b4.Height = Convert.ToInt32(bHeight);
            b4.Top = b7.Top + b7.Height + m_ButtonSeparation;
            b4.ForeColor = m_TextColor;
            b4.BackColor = m_ButtonColor;

            b5.Left = Convert.ToInt32(b4.Left + bWidth + m_ButtonSeparation);
            b5.Width = Convert.ToInt32(bWidth);
            b5.Height = Convert.ToInt32(bHeight);
            b5.Top = b4.Top;
            b5.ForeColor = m_TextColor;
            b5.BackColor = m_ButtonColor;

            b6.Left = Convert.ToInt32(b5.Left + bWidth + m_ButtonSeparation);
            b6.Width = Convert.ToInt32(bWidth);
            b6.Height = Convert.ToInt32(bHeight);
            b6.Top = b5.Top;
            b6.ForeColor = m_TextColor;
            b6.BackColor = m_ButtonColor;

            bMul.Left = Convert.ToInt32(b6.Left + bWidth + m_ButtonSeparation);
            bMul.Width = Convert.ToInt32(bWidth);
            bMul.Height = Convert.ToInt32(bHeight);
            bMul.Top = b6.Top;
            bMul.ForeColor = m_TextColor;
            bMul.BackColor = m_ButtonColor;

            b1.Left = m_ButtonSeparation;
            b1.Width = Convert.ToInt32(bWidth);
            b1.Height = Convert.ToInt32(bHeight);
            b1.Top = b4.Top + b4.Height + m_ButtonSeparation;
            b1.ForeColor = m_TextColor;
            b1.BackColor = m_ButtonColor;

            b2.Left = Convert.ToInt32(b1.Left + bWidth + m_ButtonSeparation);
            b2.Width = Convert.ToInt32(bWidth);
            b2.Height = Convert.ToInt32(bHeight);
            b2.Top = b1.Top;
            b2.ForeColor = m_TextColor;
            b2.BackColor = m_ButtonColor;

            b3.Left = Convert.ToInt32(b2.Left + bWidth + m_ButtonSeparation);
            b3.Width = Convert.ToInt32(bWidth);
            b3.Height = Convert.ToInt32(bHeight);
            b3.Top = b2.Top;
            b3.ForeColor = m_TextColor;
            b3.BackColor = m_ButtonColor;

            bSub.Left = Convert.ToInt32(b3.Left + bWidth + m_ButtonSeparation);
            bSub.Width = Convert.ToInt32(bWidth);
            bSub.Height = Convert.ToInt32(bHeight);
            bSub.Top = b3.Top;
            bSub.ForeColor = m_TextColor;
            bSub.BackColor = m_ButtonColor;

            b0.Left = m_ButtonSeparation;
            b0.Width = Convert.ToInt32(bWidth);
            b0.Height = Convert.ToInt32(bHeight);
            b0.Top = b1.Top + b1.Height + m_ButtonSeparation;
            b0.ForeColor = m_TextColor;
            b0.BackColor = m_ButtonColor;

            bDot.Left = Convert.ToInt32(b0.Left + bWidth + m_ButtonSeparation);
            bDot.Width = Convert.ToInt32(bWidth);
            bDot.Height = Convert.ToInt32(bHeight);
            bDot.Top = b0.Top;
            bDot.ForeColor = m_TextColor;
            bDot.BackColor = m_ButtonColor;

            bEqu.Left = Convert.ToInt32(bDot.Left + bWidth + m_ButtonSeparation);
            bEqu.Width = Convert.ToInt32(bWidth);
            bEqu.Height = Convert.ToInt32(bHeight);
            bEqu.Top = bDot.Top;
            bEqu.ForeColor = m_TextColor;
            bEqu.BackColor = m_ButtonColor;

            bPlus.Left = Convert.ToInt32(bEqu.Left + bWidth + m_ButtonSeparation);
            bPlus.Width = Convert.ToInt32(bWidth);
            bPlus.Height = Convert.ToInt32(bHeight);
            bPlus.Top = bEqu.Top;
            bPlus.ForeColor = m_TextColor;
            bPlus.BackColor = m_ButtonColor;
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        internal Button b0;

        internal Button b1;
        internal Button b2;
        internal Button b3;
        internal Button b4;
        internal Button b5;
        internal Button b6;
        internal Button b7;
        internal Button b8;
        internal Button b9;
        internal Button bClr;
        internal Button bDiv;
        internal Button bDot;
        internal Button bEqu;
        internal Button bEur;
        internal Button bMul;
        internal Button bPercent;
        internal Button bPlus;
        internal Button bPts;
        internal Button bSub;
        internal Label lblEur;
        internal Label lblPts;
        internal Label txtCalc;
        internal Label txtCalc2;

        public DBCalculator()
        {
            InitializeComponent();

            SubscribeEvents();
        }

        public void Initialize()
        {
            var frm = FindForm();

            frm.KeyPreview = true;
            frm.KeyDown += DBCalculator_KeyDown;
        }

        private void SubscribeEvents()
        {
            b0.Click += btn_Click;
            b1.Click += btn_Click;
            b2.Click += btn_Click;
            b3.Click += btn_Click;
            b4.Click += btn_Click;
            b5.Click += btn_Click;
            b6.Click += btn_Click;
            b7.Click += btn_Click;
            b8.Click += btn_Click;
            b9.Click += btn_Click;
            bDot.Click += btn_Click;

            bPlus.Click += oper_Click;
            bMul.Click += oper_Click;
            bSub.Click += oper_Click;
            bDiv.Click += oper_Click;
            bPercent.Click += oper_Click;

            bClr.Click += bClr_Click;
            bEqu.Click += bEqu_Click;
            bEur.Click += bEur_Click;
            bPts.Click += bPts_Click;

            Resize += DBCalculator_Resize;

            //base.KeyDown += DBCalculator_KeyDown;
        }

        private void DBCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            KeybCalc(e.KeyValue);
        }

        private void KeybCalc(int keyValue)
        {
            var key = ((char) keyValue).ToString().ToUpper();

            if (keyValue == (int) Keys.Multiply) key = "*";
            if (keyValue == (int) Keys.Divide) key = "/";
            if (keyValue == (int) Keys.Subtract) key = "-";
            if (keyValue == (int) Keys.Oemplus) key = "+";
            if (keyValue == (int) Keys.Add) key = "+";
            if (keyValue == (int) Keys.Oemcomma) key = ",";
            if (keyValue == (int) Keys.Decimal) key = ",";
            if (keyValue == (int) Keys.NumPad0) key = "0";
            if (keyValue == (int) Keys.NumPad1) key = "1";
            if (keyValue == (int) Keys.NumPad2) key = "2";
            if (keyValue == (int) Keys.NumPad3) key = "3";
            if (keyValue == (int) Keys.NumPad4) key = "4";
            if (keyValue == (int) Keys.NumPad5) key = "5";
            if (keyValue == (int) Keys.NumPad6) key = "6";
            if (keyValue == (int) Keys.NumPad7) key = "7";
            if (keyValue == (int) Keys.NumPad8) key = "8";
            if (keyValue == (int) Keys.NumPad9) key = "9";
            if (keyValue == (int) Keys.Enter) key = "=";


            if ("1234567890,".IndexOf(key) >= 0)
                ButtonClick(key);
            else if ("+-/*".IndexOf(key) >= 0)
                OperClick(key);
            else if (key == "=" || key == "\r")
                calc();
            else if (key == "C" || key == "\b") clear();
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
            this.b1 = new System.Windows.Forms.Button();
            this.b2 = new System.Windows.Forms.Button();
            this.b3 = new System.Windows.Forms.Button();
            this.b4 = new System.Windows.Forms.Button();
            this.b5 = new System.Windows.Forms.Button();
            this.b6 = new System.Windows.Forms.Button();
            this.b7 = new System.Windows.Forms.Button();
            this.b8 = new System.Windows.Forms.Button();
            this.b9 = new System.Windows.Forms.Button();
            this.b0 = new System.Windows.Forms.Button();
            this.bDot = new System.Windows.Forms.Button();
            this.bEqu = new System.Windows.Forms.Button();
            this.bPlus = new System.Windows.Forms.Button();
            this.bSub = new System.Windows.Forms.Button();
            this.bMul = new System.Windows.Forms.Button();
            this.bDiv = new System.Windows.Forms.Button();
            this.bClr = new System.Windows.Forms.Button();
            this.bEur = new System.Windows.Forms.Button();
            this.bPts = new System.Windows.Forms.Button();
            this.lblEur = new System.Windows.Forms.Label();
            this.lblPts = new System.Windows.Forms.Label();
            this.bPercent = new System.Windows.Forms.Button();
            this.txtCalc = new System.Windows.Forms.Label();
            this.txtCalc2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b1.Location = new System.Drawing.Point(8, 184);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(32, 32);
            this.b1.TabIndex = 0;
            this.b1.TabStop = false;
            this.b1.Text = "1";
            this.b1.UseVisualStyleBackColor = false;
            // 
            // b2
            // 
            this.b2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b2.Location = new System.Drawing.Point(48, 184);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(32, 32);
            this.b2.TabIndex = 2;
            this.b2.TabStop = false;
            this.b2.Text = "2";
            this.b2.UseVisualStyleBackColor = false;
            // 
            // b3
            // 
            this.b3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b3.Location = new System.Drawing.Point(88, 184);
            this.b3.Name = "b3";
            this.b3.Size = new System.Drawing.Size(32, 32);
            this.b3.TabIndex = 3;
            this.b3.TabStop = false;
            this.b3.Text = "3";
            this.b3.UseVisualStyleBackColor = false;
            // 
            // b4
            // 
            this.b4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b4.Location = new System.Drawing.Point(8, 144);
            this.b4.Name = "b4";
            this.b4.Size = new System.Drawing.Size(32, 32);
            this.b4.TabIndex = 4;
            this.b4.TabStop = false;
            this.b4.Text = "4";
            this.b4.UseVisualStyleBackColor = false;
            // 
            // b5
            // 
            this.b5.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b5.Location = new System.Drawing.Point(48, 144);
            this.b5.Name = "b5";
            this.b5.Size = new System.Drawing.Size(32, 32);
            this.b5.TabIndex = 5;
            this.b5.TabStop = false;
            this.b5.Text = "5";
            this.b5.UseVisualStyleBackColor = false;
            // 
            // b6
            // 
            this.b6.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b6.Location = new System.Drawing.Point(88, 144);
            this.b6.Name = "b6";
            this.b6.Size = new System.Drawing.Size(32, 32);
            this.b6.TabIndex = 6;
            this.b6.TabStop = false;
            this.b6.Text = "6";
            this.b6.UseVisualStyleBackColor = false;
            // 
            // b7
            // 
            this.b7.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b7.Location = new System.Drawing.Point(8, 104);
            this.b7.Name = "b7";
            this.b7.Size = new System.Drawing.Size(32, 32);
            this.b7.TabIndex = 7;
            this.b7.TabStop = false;
            this.b7.Text = "7";
            this.b7.UseVisualStyleBackColor = false;
            // 
            // b8
            // 
            this.b8.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b8.Location = new System.Drawing.Point(48, 104);
            this.b8.Name = "b8";
            this.b8.Size = new System.Drawing.Size(32, 32);
            this.b8.TabIndex = 8;
            this.b8.TabStop = false;
            this.b8.Text = "8";
            this.b8.UseVisualStyleBackColor = false;
            // 
            // b9
            // 
            this.b9.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b9.Location = new System.Drawing.Point(88, 104);
            this.b9.Name = "b9";
            this.b9.Size = new System.Drawing.Size(32, 32);
            this.b9.TabIndex = 9;
            this.b9.TabStop = false;
            this.b9.Text = "9";
            this.b9.UseVisualStyleBackColor = false;
            // 
            // b0
            // 
            this.b0.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b0.Location = new System.Drawing.Point(8, 224);
            this.b0.Name = "b0";
            this.b0.Size = new System.Drawing.Size(32, 32);
            this.b0.TabIndex = 10;
            this.b0.TabStop = false;
            this.b0.Text = "0";
            this.b0.UseVisualStyleBackColor = false;
            // 
            // bDot
            // 
            this.bDot.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bDot.Location = new System.Drawing.Point(48, 224);
            this.bDot.Name = "bDot";
            this.bDot.Size = new System.Drawing.Size(32, 32);
            this.bDot.TabIndex = 11;
            this.bDot.TabStop = false;
            this.bDot.Text = ",";
            this.bDot.UseVisualStyleBackColor = false;
            // 
            // bEqu
            // 
            this.bEqu.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bEqu.Location = new System.Drawing.Point(88, 224);
            this.bEqu.Name = "bEqu";
            this.bEqu.Size = new System.Drawing.Size(32, 32);
            this.bEqu.TabIndex = 12;
            this.bEqu.TabStop = false;
            this.bEqu.Text = "=";
            this.bEqu.UseVisualStyleBackColor = false;
            // 
            // bPlus
            // 
            this.bPlus.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bPlus.Location = new System.Drawing.Point(128, 224);
            this.bPlus.Name = "bPlus";
            this.bPlus.Size = new System.Drawing.Size(32, 32);
            this.bPlus.TabIndex = 13;
            this.bPlus.TabStop = false;
            this.bPlus.Text = "+";
            this.bPlus.UseVisualStyleBackColor = false;
            // 
            // bSub
            // 
            this.bSub.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bSub.Location = new System.Drawing.Point(128, 184);
            this.bSub.Name = "bSub";
            this.bSub.Size = new System.Drawing.Size(32, 32);
            this.bSub.TabIndex = 14;
            this.bSub.TabStop = false;
            this.bSub.Text = "-";
            this.bSub.UseVisualStyleBackColor = false;
            // 
            // bMul
            // 
            this.bMul.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bMul.Location = new System.Drawing.Point(128, 144);
            this.bMul.Name = "bMul";
            this.bMul.Size = new System.Drawing.Size(32, 32);
            this.bMul.TabIndex = 15;
            this.bMul.TabStop = false;
            this.bMul.Text = "*";
            this.bMul.UseVisualStyleBackColor = false;
            // 
            // bDiv
            // 
            this.bDiv.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bDiv.Location = new System.Drawing.Point(128, 104);
            this.bDiv.Name = "bDiv";
            this.bDiv.Size = new System.Drawing.Size(32, 32);
            this.bDiv.TabIndex = 16;
            this.bDiv.TabStop = false;
            this.bDiv.Text = "/";
            this.bDiv.UseVisualStyleBackColor = false;
            // 
            // bClr
            // 
            this.bClr.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bClr.Location = new System.Drawing.Point(128, 64);
            this.bClr.Name = "bClr";
            this.bClr.Size = new System.Drawing.Size(32, 32);
            this.bClr.TabIndex = 18;
            this.bClr.TabStop = false;
            this.bClr.Text = "C";
            this.bClr.UseVisualStyleBackColor = false;
            // 
            // bEur
            // 
            this.bEur.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bEur.Location = new System.Drawing.Point(8, 64);
            this.bEur.Name = "bEur";
            this.bEur.Size = new System.Drawing.Size(32, 32);
            this.bEur.TabIndex = 19;
            this.bEur.TabStop = false;
            this.bEur.Text = "€";
            this.bEur.UseVisualStyleBackColor = false;
            // 
            // bPts
            // 
            this.bPts.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bPts.Location = new System.Drawing.Point(48, 64);
            this.bPts.Name = "bPts";
            this.bPts.Size = new System.Drawing.Size(32, 32);
            this.bPts.TabIndex = 20;
            this.bPts.TabStop = false;
            this.bPts.Text = "P";
            this.bPts.UseVisualStyleBackColor = false;
            // 
            // lblEur
            // 
            this.lblEur.Location = new System.Drawing.Point(160, 16);
            this.lblEur.Name = "lblEur";
            this.lblEur.Size = new System.Drawing.Size(10, 16);
            this.lblEur.TabIndex = 22;
            this.lblEur.Text = "€";
            // 
            // lblPts
            // 
            this.lblPts.Location = new System.Drawing.Point(160, 40);
            this.lblPts.Name = "lblPts";
            this.lblPts.Size = new System.Drawing.Size(12, 16);
            this.lblPts.TabIndex = 23;
            this.lblPts.Text = "P";
            // 
            // bPercent
            // 
            this.bPercent.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bPercent.Location = new System.Drawing.Point(88, 64);
            this.bPercent.Name = "bPercent";
            this.bPercent.Size = new System.Drawing.Size(32, 32);
            this.bPercent.TabIndex = 17;
            this.bPercent.TabStop = false;
            this.bPercent.Text = "%";
            this.bPercent.UseVisualStyleBackColor = false;
            // 
            // txtCalc
            // 
            this.txtCalc.BackColor = System.Drawing.Color.White;
            this.txtCalc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCalc.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCalc.Location = new System.Drawing.Point(8, 32);
            this.txtCalc.Name = "txtCalc";
            this.txtCalc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCalc.Size = new System.Drawing.Size(148, 24);
            this.txtCalc.TabIndex = 24;
            // 
            // txtCalc2
            // 
            this.txtCalc2.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCalc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCalc2.Location = new System.Drawing.Point(8, 12);
            this.txtCalc2.Name = "txtCalc2";
            this.txtCalc2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCalc2.Size = new System.Drawing.Size(148, 16);
            this.txtCalc2.TabIndex = 25;
            // 
            // DBCalculator
            // 
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.txtCalc);
            this.Controls.Add(this.bPercent);
            this.Controls.Add(this.lblPts);
            this.Controls.Add(this.lblEur);
            this.Controls.Add(this.bPts);
            this.Controls.Add(this.bEur);
            this.Controls.Add(this.bClr);
            this.Controls.Add(this.bDiv);
            this.Controls.Add(this.bMul);
            this.Controls.Add(this.bSub);
            this.Controls.Add(this.bPlus);
            this.Controls.Add(this.bEqu);
            this.Controls.Add(this.bDot);
            this.Controls.Add(this.b0);
            this.Controls.Add(this.b9);
            this.Controls.Add(this.b8);
            this.Controls.Add(this.b7);
            this.Controls.Add(this.b6);
            this.Controls.Add(this.b5);
            this.Controls.Add(this.b4);
            this.Controls.Add(this.b3);
            this.Controls.Add(this.b2);
            this.Controls.Add(this.b1);
            this.Controls.Add(this.txtCalc2);
            this.Name = "DBCalculator";
            this.Size = new System.Drawing.Size(174, 264);
            this.ResumeLayout(false);

        }

        #endregion
    }
}