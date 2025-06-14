#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
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
            b1 = new Button();
            b2 = new Button();
            b3 = new Button();
            b4 = new Button();
            b5 = new Button();
            b6 = new Button();
            b7 = new Button();
            b8 = new Button();
            b9 = new Button();
            b0 = new Button();
            bDot = new Button();
            bEqu = new Button();
            bPlus = new Button();
            bSub = new Button();
            bMul = new Button();
            bDiv = new Button();
            bClr = new Button();
            bEur = new Button();
            bPts = new Button();
            lblEur = new Label();
            lblPts = new Label();
            bPercent = new Button();
            txtCalc = new Label();
            txtCalc2 = new Label();
            SuspendLayout();
            // 
            // b1
            // 
            b1.BackColor = Color.LightSkyBlue;
            b1.Location = new Point(8, 184);
            b1.Name = "b1";
            b1.Size = new Size(32, 32);
            b1.TabIndex = 0;
            b1.TabStop = false;
            b1.Text = "1";
            b1.UseVisualStyleBackColor = false;
            // 
            // b2
            // 
            b2.BackColor = Color.LightSkyBlue;
            b2.Location = new Point(48, 184);
            b2.Name = "b2";
            b2.Size = new Size(32, 32);
            b2.TabIndex = 2;
            b2.TabStop = false;
            b2.Text = "2";
            b2.UseVisualStyleBackColor = false;
            // 
            // b3
            // 
            b3.BackColor = Color.LightSkyBlue;
            b3.Location = new Point(88, 184);
            b3.Name = "b3";
            b3.Size = new Size(32, 32);
            b3.TabIndex = 3;
            b3.TabStop = false;
            b3.Text = "3";
            b3.UseVisualStyleBackColor = false;
            // 
            // b4
            // 
            b4.BackColor = Color.LightSkyBlue;
            b4.Location = new Point(8, 144);
            b4.Name = "b4";
            b4.Size = new Size(32, 32);
            b4.TabIndex = 4;
            b4.TabStop = false;
            b4.Text = "4";
            b4.UseVisualStyleBackColor = false;
            // 
            // b5
            // 
            b5.BackColor = Color.LightSkyBlue;
            b5.Location = new Point(48, 144);
            b5.Name = "b5";
            b5.Size = new Size(32, 32);
            b5.TabIndex = 5;
            b5.TabStop = false;
            b5.Text = "5";
            b5.UseVisualStyleBackColor = false;
            // 
            // b6
            // 
            b6.BackColor = Color.LightSkyBlue;
            b6.Location = new Point(88, 144);
            b6.Name = "b6";
            b6.Size = new Size(32, 32);
            b6.TabIndex = 6;
            b6.TabStop = false;
            b6.Text = "6";
            b6.UseVisualStyleBackColor = false;
            // 
            // b7
            // 
            b7.BackColor = Color.LightSkyBlue;
            b7.Location = new Point(8, 104);
            b7.Name = "b7";
            b7.Size = new Size(32, 32);
            b7.TabIndex = 7;
            b7.TabStop = false;
            b7.Text = "7";
            b7.UseVisualStyleBackColor = false;
            // 
            // b8
            // 
            b8.BackColor = Color.LightSkyBlue;
            b8.Location = new Point(48, 104);
            b8.Name = "b8";
            b8.Size = new Size(32, 32);
            b8.TabIndex = 8;
            b8.TabStop = false;
            b8.Text = "8";
            b8.UseVisualStyleBackColor = false;
            // 
            // b9
            // 
            b9.BackColor = Color.LightSkyBlue;
            b9.Location = new Point(88, 104);
            b9.Name = "b9";
            b9.Size = new Size(32, 32);
            b9.TabIndex = 9;
            b9.TabStop = false;
            b9.Text = "9";
            b9.UseVisualStyleBackColor = false;
            // 
            // b0
            // 
            b0.BackColor = Color.LightSkyBlue;
            b0.Location = new Point(8, 224);
            b0.Name = "b0";
            b0.Size = new Size(32, 32);
            b0.TabIndex = 10;
            b0.TabStop = false;
            b0.Text = "0";
            b0.UseVisualStyleBackColor = false;
            // 
            // bDot
            // 
            bDot.BackColor = Color.LightSkyBlue;
            bDot.Location = new Point(48, 224);
            bDot.Name = "bDot";
            bDot.Size = new Size(32, 32);
            bDot.TabIndex = 11;
            bDot.TabStop = false;
            bDot.Text = ",";
            bDot.UseVisualStyleBackColor = false;
            // 
            // bEqu
            // 
            bEqu.BackColor = Color.LightSkyBlue;
            bEqu.Location = new Point(88, 224);
            bEqu.Name = "bEqu";
            bEqu.Size = new Size(32, 32);
            bEqu.TabIndex = 12;
            bEqu.TabStop = false;
            bEqu.Text = "=";
            bEqu.UseVisualStyleBackColor = false;
            // 
            // bPlus
            // 
            bPlus.BackColor = Color.LightSkyBlue;
            bPlus.Location = new Point(128, 224);
            bPlus.Name = "bPlus";
            bPlus.Size = new Size(32, 32);
            bPlus.TabIndex = 13;
            bPlus.TabStop = false;
            bPlus.Text = "+";
            bPlus.UseVisualStyleBackColor = false;
            // 
            // bSub
            // 
            bSub.BackColor = Color.LightSkyBlue;
            bSub.Location = new Point(128, 184);
            bSub.Name = "bSub";
            bSub.Size = new Size(32, 32);
            bSub.TabIndex = 14;
            bSub.TabStop = false;
            bSub.Text = "-";
            bSub.UseVisualStyleBackColor = false;
            // 
            // bMul
            // 
            bMul.BackColor = Color.LightSkyBlue;
            bMul.Location = new Point(128, 144);
            bMul.Name = "bMul";
            bMul.Size = new Size(32, 32);
            bMul.TabIndex = 15;
            bMul.TabStop = false;
            bMul.Text = "*";
            bMul.UseVisualStyleBackColor = false;
            // 
            // bDiv
            // 
            bDiv.BackColor = Color.LightSkyBlue;
            bDiv.Location = new Point(128, 104);
            bDiv.Name = "bDiv";
            bDiv.Size = new Size(32, 32);
            bDiv.TabIndex = 16;
            bDiv.TabStop = false;
            bDiv.Text = "/";
            bDiv.UseVisualStyleBackColor = false;
            // 
            // bClr
            // 
            bClr.BackColor = Color.LightSkyBlue;
            bClr.Location = new Point(128, 64);
            bClr.Name = "bClr";
            bClr.Size = new Size(32, 32);
            bClr.TabIndex = 18;
            bClr.TabStop = false;
            bClr.Text = "C";
            bClr.UseVisualStyleBackColor = false;
            // 
            // bEur
            // 
            bEur.BackColor = Color.LightSkyBlue;
            bEur.Location = new Point(8, 64);
            bEur.Name = "bEur";
            bEur.Size = new Size(32, 32);
            bEur.TabIndex = 19;
            bEur.TabStop = false;
            bEur.Text = "€";
            bEur.UseVisualStyleBackColor = false;
            // 
            // bPts
            // 
            bPts.BackColor = Color.LightSkyBlue;
            bPts.Location = new Point(48, 64);
            bPts.Name = "bPts";
            bPts.Size = new Size(32, 32);
            bPts.TabIndex = 20;
            bPts.TabStop = false;
            bPts.Text = "P";
            bPts.UseVisualStyleBackColor = false;
            // 
            // lblEur
            // 
            lblEur.Location = new Point(160, 16);
            lblEur.Name = "lblEur";
            lblEur.Size = new Size(10, 16);
            lblEur.TabIndex = 22;
            lblEur.Text = "€";
            // 
            // lblPts
            // 
            lblPts.Location = new Point(160, 40);
            lblPts.Name = "lblPts";
            lblPts.Size = new Size(12, 16);
            lblPts.TabIndex = 23;
            lblPts.Text = "P";
            // 
            // bPercent
            // 
            bPercent.BackColor = Color.LightSkyBlue;
            bPercent.Location = new Point(88, 64);
            bPercent.Name = "bPercent";
            bPercent.Size = new Size(32, 32);
            bPercent.TabIndex = 17;
            bPercent.TabStop = false;
            bPercent.Text = "%";
            bPercent.UseVisualStyleBackColor = false;
            // 
            // txtCalc
            // 
            txtCalc.BackColor = Color.White;
            txtCalc.BorderStyle = BorderStyle.FixedSingle;
            txtCalc.Font = new Font("Arial", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            txtCalc.Location = new Point(8, 32);
            txtCalc.Name = "txtCalc";
            txtCalc.RightToLeft = RightToLeft.Yes;
            txtCalc.Size = new Size(148, 24);
            txtCalc.TabIndex = 24;
            // 
            // txtCalc2
            // 
            txtCalc2.BackColor = Color.Gainsboro;
            txtCalc2.BorderStyle = BorderStyle.FixedSingle;
            txtCalc2.Location = new Point(8, 12);
            txtCalc2.Name = "txtCalc2";
            txtCalc2.RightToLeft = RightToLeft.Yes;
            txtCalc2.Size = new Size(148, 16);
            txtCalc2.TabIndex = 25;
            // 
            // DBCalculator
            // 
            BackColor = Color.SteelBlue;
            Controls.Add(txtCalc);
            Controls.Add(bPercent);
            Controls.Add(lblPts);
            Controls.Add(lblEur);
            Controls.Add(bPts);
            Controls.Add(bEur);
            Controls.Add(bClr);
            Controls.Add(bDiv);
            Controls.Add(bMul);
            Controls.Add(bSub);
            Controls.Add(bPlus);
            Controls.Add(bEqu);
            Controls.Add(bDot);
            Controls.Add(b0);
            Controls.Add(b9);
            Controls.Add(b8);
            Controls.Add(b7);
            Controls.Add(b6);
            Controls.Add(b5);
            Controls.Add(b4);
            Controls.Add(b3);
            Controls.Add(b2);
            Controls.Add(b1);
            Controls.Add(txtCalc2);
            Name = "DBCalculator";
            Size = new Size(174, 264);
            ResumeLayout(false);
        }

        #endregion
    }
}