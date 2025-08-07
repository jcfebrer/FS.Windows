#region

using FSException;
using FSMouseKeyboardLibrary;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmKeyboard : Form
    {
        public Keyboard dbk = new Keyboard();
        public DBTextBoxEx m_TextBox;

        public DBTextBoxEx TextBox
        {
            get { return m_TextBox; }
            set { m_TextBox = value; }
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            TextBox.Text = txtKeyboard.Text;
            Close();
        }

        private void frmKeyboard_Load(object sender, EventArgs e)
        {
            ShowKeyboard(dbk.CapsLock);

            if (TextBox != null)
                TypeChar(TextBox.Text);
            else
                throw new ExceptionUtil("Propiedad TextBox no definida.");
        }


        private void ShowKeyboard(bool mayus)
        {
            if (mayus)
            {
                bq.Text = "Q";
                bw.Text = "W";
                be.Text = "E";
                br.Text = "R";
                bt.Text = "T";
                by.Text = "Y";
                bu.Text = "U";
                bi.Text = "I";
                bo.Text = "O";
                bp.Text = "P";
                ba.Text = "A";
                bs.Text = "S";
                bd.Text = "D";
                bf.Text = "F";
                bg.Text = "G";
                bh.Text = "H";
                bj.Text = "J";
                bk.Text = "K";
                bl.Text = "L";
                bñ.Text = "Ñ";
                bz.Text = "Z";
                bx.Text = "X";
                bc.Text = "C";
                bv.Text = "V";
                bb.Text = "B";
                bn.Text = "N";
                bm.Text = "M";
                dbk.TroggleLed(Keyboard.LedKeys.CapsLock, true);
            }
            else
            {
                bq.Text = "q";
                bw.Text = "w";
                be.Text = "e";
                br.Text = "r";
                bt.Text = "t";
                by.Text = "y";
                bu.Text = "u";
                bi.Text = "i";
                bo.Text = "o";
                bp.Text = "p";
                ba.Text = "a";
                bs.Text = "s";
                bd.Text = "d";
                bf.Text = "f";
                bg.Text = "g";
                bh.Text = "h";
                bj.Text = "j";
                bk.Text = "k";
                bl.Text = "l";
                bñ.Text = "ñ";
                bz.Text = "z";
                bx.Text = "x";
                bc.Text = "c";
                bv.Text = "v";
                bb.Text = "b";
                bn.Text = "n";
                bm.Text = "m";
                dbk.TroggleLed(Keyboard.LedKeys.CapsLock, false);
            }
        }


        private void bSpacio_Click(object sender, EventArgs e)
        {
            SendChar(" ");
        }


        private void bMul_Click(object sender, EventArgs e)
        {
            SendChar("{ENTER}");
        }


        private void TypeChar(string car)
        {
            txtKeyboard.Text = txtKeyboard.Text + car;
            txtKeyboard.ScrollToCaret();
        }


        private void bBorrar_Click(object sender, EventArgs e)
        {
            SendChar("{BACKSPACE}");
        }


        private void bLeft_Click(object sender, EventArgs e)
        {
            SendChar("{LEFT}");
        }


        private void bRight_Click(object sender, EventArgs e)
        {
            SendChar("{RIGHT}");
        }


        private void SendChar(string car)
        {
            txtKeyboard.Focus();
            SendKeys.Send(car);
        }


        private void bMayus_Click(object sender, EventArgs e)
        {
            ShowKeyboard(!dbk.CapsLock);
        }


        private void bTab_Click(object sender, EventArgs e)
        {
            SendChar("        ");
        }


        private void bComa_Click(object sender, EventArgs e)
        {
            SendChar(",");
        }


        private void bPunto_Click(object sender, EventArgs e)
        {
            SendChar(".");
        }


        private void bGuion_Click(object sender, EventArgs e)
        {
            SendChar("-");
        }


        private void bMas_Click(object sender, EventArgs e)
        {
            SendChar("+");
        }


        private void bIntFin_Click(object sender, EventArgs e)
        {
            SendChar("'");
        }


        private void bIntIni_Click(object sender, EventArgs e)
        {
            SendChar("?");
        }


        private void bMenorque_Click(object sender, EventArgs e)
        {
            SendChar("<");
        }


        private void boa_Click(object sender, EventArgs e)
        {
            SendChar("?");
        }


        private void bControl_Click(object sender, EventArgs e)
        {
            txtKeyboard.Text = "";
        }


        private void bMayusDer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void b1_Click(object sender, EventArgs e)
        {
            SendChar(((Button) sender).Text.ToLower());
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
        internal Button bAlt;
        internal Button bBorrar;
        internal Button bCedilla;
        internal Button bComa;
        internal Button bComita;
        internal Button bControl;
        internal Button bDieresis;
        internal Button bGuion;
        internal Button bIntFin;
        internal Button bIntIni;
        internal Button bLeft;
        internal Button bMas;
        internal Button bMayus;
        internal Button bMayusDer;
        internal Button bMayusIzq;
        internal Button bMenorque;
        internal Button bMul;
        internal Button bPunto;
        internal Button bRight;
        internal Button bSend;
        internal Button bSpacio;
        internal Button bTab;
        internal Button ba;
        internal Button bb;
        internal Button bc;
        internal Button bd;
        internal Button be;
        internal Button bf;
        internal Button bg;
        internal Button bh;
        internal Button bi;
        internal Button bj;
        internal Button bk;
        internal Button bl;
        internal Button bm;
        internal Button bn;
        internal Button bo;
        internal Button boa;
        internal Button bp;
        internal Button bq;
        internal Button br;
        internal Button bs;
        internal Button bt;
        internal Button bu;
        internal Button bv;
        internal Button bw;
        internal Button bx;
        internal Button by;
        internal Button bz;
        internal Button bñ;
        internal TextBox txtKeyboard;

        public frmKeyboard()
        {
            InitializeComponent();

            b3.Click += b1_Click;
            b2.Click += b1_Click;
            b1.Click += b1_Click;
            boa.Click += b1_Click;
            b0.Click += b1_Click;
            b9.Click += b1_Click;
            b8.Click += b1_Click;
            b7.Click += b1_Click;
            b6.Click += b1_Click;
            b5.Click += b1_Click;
            b4.Click += b1_Click;
            bComita.Click += b1_Click;
            bp.Click += b1_Click;
            bo.Click += b1_Click;
            bi.Click += b1_Click;
            bu.Click += b1_Click;
            by.Click += b1_Click;
            bt.Click += b1_Click;
            br.Click += b1_Click;
            be.Click += b1_Click;
            bw.Click += b1_Click;
            bq.Click += b1_Click;
            bCedilla.Click += b1_Click;
            bDieresis.Click += b1_Click;
            bl.Click += b1_Click;
            bñ.Click += b1_Click;
            bk.Click += b1_Click;
            bj.Click += b1_Click;
            bh.Click += b1_Click;
            bg.Click += b1_Click;
            bf.Click += b1_Click;
            bd.Click += b1_Click;
            bs.Click += b1_Click;
            ba.Click += b1_Click;
            bm.Click += b1_Click;
            bn.Click += b1_Click;
            bb.Click += b1_Click;
            bv.Click += b1_Click;
            bc.Click += b1_Click;
            bx.Click += b1_Click;
            bz.Click += b1_Click;

            bSend.Click += bSend_Click;
            Load += frmKeyboard_Load;
            bSpacio.Click += bSpacio_Click;
            bMul.Click += bMul_Click;
            bBorrar.Click += bBorrar_Click;
            bLeft.Click += bLeft_Click;
            bRight.Click += bRight_Click;
            bMayus.Click += bMayus_Click;
            bTab.Click += bTab_Click;
            bComa.Click += bComa_Click;
            bPunto.Click += bPunto_Click;
            bGuion.Click += bGuion_Click;
            bMas.Click += bMas_Click;
            bIntFin.Click += bIntFin_Click;
            bIntIni.Click += bIntIni_Click;
            bMenorque.Click += bMenorque_Click;
            boa.Click += boa_Click;
            bControl.Click += bControl_Click;
            bMayusDer.Click += bMayusDer_Click;
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
            this.b3 = new System.Windows.Forms.Button();
            this.b2 = new System.Windows.Forms.Button();
            this.b1 = new System.Windows.Forms.Button();
            this.boa = new System.Windows.Forms.Button();
            this.bMul = new System.Windows.Forms.Button();
            this.bIntFin = new System.Windows.Forms.Button();
            this.bIntIni = new System.Windows.Forms.Button();
            this.b0 = new System.Windows.Forms.Button();
            this.b9 = new System.Windows.Forms.Button();
            this.b8 = new System.Windows.Forms.Button();
            this.b7 = new System.Windows.Forms.Button();
            this.b6 = new System.Windows.Forms.Button();
            this.b5 = new System.Windows.Forms.Button();
            this.b4 = new System.Windows.Forms.Button();
            this.bMayus = new System.Windows.Forms.Button();
            this.bSpacio = new System.Windows.Forms.Button();
            this.bTab = new System.Windows.Forms.Button();
            this.bBorrar = new System.Windows.Forms.Button();
            this.bMas = new System.Windows.Forms.Button();
            this.bComita = new System.Windows.Forms.Button();
            this.bp = new System.Windows.Forms.Button();
            this.bo = new System.Windows.Forms.Button();
            this.bi = new System.Windows.Forms.Button();
            this.bu = new System.Windows.Forms.Button();
            this.by = new System.Windows.Forms.Button();
            this.bt = new System.Windows.Forms.Button();
            this.br = new System.Windows.Forms.Button();
            this.be = new System.Windows.Forms.Button();
            this.bw = new System.Windows.Forms.Button();
            this.bq = new System.Windows.Forms.Button();
            this.bCedilla = new System.Windows.Forms.Button();
            this.bDieresis = new System.Windows.Forms.Button();
            this.bl = new System.Windows.Forms.Button();
            this.bñ = new System.Windows.Forms.Button();
            this.bk = new System.Windows.Forms.Button();
            this.bj = new System.Windows.Forms.Button();
            this.bh = new System.Windows.Forms.Button();
            this.bg = new System.Windows.Forms.Button();
            this.bf = new System.Windows.Forms.Button();
            this.bd = new System.Windows.Forms.Button();
            this.bs = new System.Windows.Forms.Button();
            this.ba = new System.Windows.Forms.Button();
            this.bGuion = new System.Windows.Forms.Button();
            this.bPunto = new System.Windows.Forms.Button();
            this.bComa = new System.Windows.Forms.Button();
            this.bm = new System.Windows.Forms.Button();
            this.bn = new System.Windows.Forms.Button();
            this.bb = new System.Windows.Forms.Button();
            this.bv = new System.Windows.Forms.Button();
            this.bc = new System.Windows.Forms.Button();
            this.bx = new System.Windows.Forms.Button();
            this.bz = new System.Windows.Forms.Button();
            this.bMenorque = new System.Windows.Forms.Button();
            this.bMayusIzq = new System.Windows.Forms.Button();
            this.bMayusDer = new System.Windows.Forms.Button();
            this.bControl = new System.Windows.Forms.Button();
            this.bAlt = new System.Windows.Forms.Button();
            this.bSend = new System.Windows.Forms.Button();
            this.txtKeyboard = new System.Windows.Forms.TextBox();
            this.bLeft = new System.Windows.Forms.Button();
            this.bRight = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b3
            // 
            this.b3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b3.Location = new System.Drawing.Point(128, 80);
            this.b3.Name = "b3";
            this.b3.Size = new System.Drawing.Size(32, 32);
            this.b3.TabIndex = 28;
            this.b3.TabStop = false;
            this.b3.Text = "3";
            this.b3.UseVisualStyleBackColor = false;
            // 
            // b2
            // 
            this.b2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b2.Location = new System.Drawing.Point(88, 80);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(32, 32);
            this.b2.TabIndex = 27;
            this.b2.TabStop = false;
            this.b2.Text = "2";
            this.b2.UseVisualStyleBackColor = false;
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b1.Location = new System.Drawing.Point(48, 80);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(32, 32);
            this.b1.TabIndex = 26;
            this.b1.TabStop = false;
            this.b1.Text = "1";
            this.b1.UseVisualStyleBackColor = false;
            // 
            // boa
            // 
            this.boa.BackColor = System.Drawing.Color.LightSkyBlue;
            this.boa.Location = new System.Drawing.Point(8, 80);
            this.boa.Name = "boa";
            this.boa.Size = new System.Drawing.Size(32, 32);
            this.boa.TabIndex = 39;
            this.boa.TabStop = false;
            this.boa.Text = "º";
            this.boa.UseVisualStyleBackColor = false;
            // 
            // bMul
            // 
            this.bMul.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bMul.Location = new System.Drawing.Point(568, 120);
            this.bMul.Name = "bMul";
            this.bMul.Size = new System.Drawing.Size(40, 72);
            this.bMul.TabIndex = 40;
            this.bMul.TabStop = false;
            this.bMul.Text = "Enter";
            this.bMul.UseVisualStyleBackColor = false;
            // 
            // bIntFin
            // 
            this.bIntFin.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bIntFin.Location = new System.Drawing.Point(448, 80);
            this.bIntFin.Name = "bIntFin";
            this.bIntFin.Size = new System.Drawing.Size(32, 32);
            this.bIntFin.TabIndex = 44;
            this.bIntFin.TabStop = false;
            this.bIntFin.Text = "\'";
            this.bIntFin.UseVisualStyleBackColor = false;
            // 
            // bIntIni
            // 
            this.bIntIni.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bIntIni.Location = new System.Drawing.Point(488, 80);
            this.bIntIni.Name = "bIntIni";
            this.bIntIni.Size = new System.Drawing.Size(32, 32);
            this.bIntIni.TabIndex = 45;
            this.bIntIni.TabStop = false;
            this.bIntIni.Text = "¡";
            this.bIntIni.UseVisualStyleBackColor = false;
            // 
            // b0
            // 
            this.b0.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b0.Location = new System.Drawing.Point(408, 80);
            this.b0.Name = "b0";
            this.b0.Size = new System.Drawing.Size(32, 32);
            this.b0.TabIndex = 35;
            this.b0.TabStop = false;
            this.b0.Text = "0";
            this.b0.UseVisualStyleBackColor = false;
            // 
            // b9
            // 
            this.b9.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b9.Location = new System.Drawing.Point(368, 80);
            this.b9.Name = "b9";
            this.b9.Size = new System.Drawing.Size(32, 32);
            this.b9.TabIndex = 34;
            this.b9.TabStop = false;
            this.b9.Text = "9";
            this.b9.UseVisualStyleBackColor = false;
            // 
            // b8
            // 
            this.b8.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b8.Location = new System.Drawing.Point(328, 80);
            this.b8.Name = "b8";
            this.b8.Size = new System.Drawing.Size(32, 32);
            this.b8.TabIndex = 33;
            this.b8.TabStop = false;
            this.b8.Text = "8";
            this.b8.UseVisualStyleBackColor = false;
            // 
            // b7
            // 
            this.b7.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b7.Location = new System.Drawing.Point(288, 80);
            this.b7.Name = "b7";
            this.b7.Size = new System.Drawing.Size(32, 32);
            this.b7.TabIndex = 32;
            this.b7.TabStop = false;
            this.b7.Text = "7";
            this.b7.UseVisualStyleBackColor = false;
            // 
            // b6
            // 
            this.b6.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b6.Location = new System.Drawing.Point(248, 80);
            this.b6.Name = "b6";
            this.b6.Size = new System.Drawing.Size(32, 32);
            this.b6.TabIndex = 31;
            this.b6.TabStop = false;
            this.b6.Text = "6";
            this.b6.UseVisualStyleBackColor = false;
            // 
            // b5
            // 
            this.b5.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b5.Location = new System.Drawing.Point(208, 80);
            this.b5.Name = "b5";
            this.b5.Size = new System.Drawing.Size(32, 32);
            this.b5.TabIndex = 30;
            this.b5.TabStop = false;
            this.b5.Text = "5";
            this.b5.UseVisualStyleBackColor = false;
            // 
            // b4
            // 
            this.b4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.b4.Location = new System.Drawing.Point(168, 80);
            this.b4.Name = "b4";
            this.b4.Size = new System.Drawing.Size(32, 32);
            this.b4.TabIndex = 29;
            this.b4.TabStop = false;
            this.b4.Text = "4";
            this.b4.UseVisualStyleBackColor = false;
            // 
            // bMayus
            // 
            this.bMayus.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bMayus.Location = new System.Drawing.Point(8, 160);
            this.bMayus.Name = "bMayus";
            this.bMayus.Size = new System.Drawing.Size(72, 32);
            this.bMayus.TabIndex = 36;
            this.bMayus.TabStop = false;
            this.bMayus.Text = "Bloq Mayús";
            this.bMayus.UseVisualStyleBackColor = false;
            // 
            // bSpacio
            // 
            this.bSpacio.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bSpacio.Location = new System.Drawing.Point(184, 240);
            this.bSpacio.Name = "bSpacio";
            this.bSpacio.Size = new System.Drawing.Size(264, 32);
            this.bSpacio.TabIndex = 37;
            this.bSpacio.TabStop = false;
            this.bSpacio.Text = "Space";
            this.bSpacio.UseVisualStyleBackColor = false;
            // 
            // bTab
            // 
            this.bTab.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bTab.Location = new System.Drawing.Point(8, 120);
            this.bTab.Name = "bTab";
            this.bTab.Size = new System.Drawing.Size(56, 32);
            this.bTab.TabIndex = 43;
            this.bTab.TabStop = false;
            this.bTab.Text = "Tab";
            this.bTab.UseVisualStyleBackColor = false;
            // 
            // bBorrar
            // 
            this.bBorrar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bBorrar.Location = new System.Drawing.Point(528, 80);
            this.bBorrar.Name = "bBorrar";
            this.bBorrar.Size = new System.Drawing.Size(80, 32);
            this.bBorrar.TabIndex = 42;
            this.bBorrar.TabStop = false;
            this.bBorrar.Text = "<--";
            this.bBorrar.UseVisualStyleBackColor = false;
            // 
            // bMas
            // 
            this.bMas.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bMas.Location = new System.Drawing.Point(512, 120);
            this.bMas.Name = "bMas";
            this.bMas.Size = new System.Drawing.Size(32, 32);
            this.bMas.TabIndex = 60;
            this.bMas.TabStop = false;
            this.bMas.Text = "+";
            this.bMas.UseVisualStyleBackColor = false;
            // 
            // bComita
            // 
            this.bComita.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bComita.Location = new System.Drawing.Point(472, 120);
            this.bComita.Name = "bComita";
            this.bComita.Size = new System.Drawing.Size(32, 32);
            this.bComita.TabIndex = 59;
            this.bComita.TabStop = false;
            this.bComita.Text = "\'";
            this.bComita.UseVisualStyleBackColor = false;
            // 
            // bp
            // 
            this.bp.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bp.Location = new System.Drawing.Point(432, 120);
            this.bp.Name = "bp";
            this.bp.Size = new System.Drawing.Size(32, 32);
            this.bp.TabIndex = 58;
            this.bp.TabStop = false;
            this.bp.Text = "p";
            this.bp.UseVisualStyleBackColor = false;
            // 
            // bo
            // 
            this.bo.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bo.Location = new System.Drawing.Point(392, 120);
            this.bo.Name = "bo";
            this.bo.Size = new System.Drawing.Size(32, 32);
            this.bo.TabIndex = 57;
            this.bo.TabStop = false;
            this.bo.Text = "o";
            this.bo.UseVisualStyleBackColor = false;
            // 
            // bi
            // 
            this.bi.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bi.Location = new System.Drawing.Point(352, 120);
            this.bi.Name = "bi";
            this.bi.Size = new System.Drawing.Size(32, 32);
            this.bi.TabIndex = 56;
            this.bi.TabStop = false;
            this.bi.Text = "i";
            this.bi.UseVisualStyleBackColor = false;
            // 
            // bu
            // 
            this.bu.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bu.Location = new System.Drawing.Point(312, 120);
            this.bu.Name = "bu";
            this.bu.Size = new System.Drawing.Size(32, 32);
            this.bu.TabIndex = 55;
            this.bu.TabStop = false;
            this.bu.Text = "u";
            this.bu.UseVisualStyleBackColor = false;
            // 
            // by
            // 
            this.by.BackColor = System.Drawing.Color.LightSkyBlue;
            this.by.Location = new System.Drawing.Point(272, 120);
            this.by.Name = "by";
            this.by.Size = new System.Drawing.Size(32, 32);
            this.by.TabIndex = 54;
            this.by.TabStop = false;
            this.by.Text = "y";
            this.by.UseVisualStyleBackColor = false;
            // 
            // bt
            // 
            this.bt.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bt.Location = new System.Drawing.Point(232, 120);
            this.bt.Name = "bt";
            this.bt.Size = new System.Drawing.Size(32, 32);
            this.bt.TabIndex = 53;
            this.bt.TabStop = false;
            this.bt.Text = "t";
            this.bt.UseVisualStyleBackColor = false;
            // 
            // br
            // 
            this.br.BackColor = System.Drawing.Color.LightSkyBlue;
            this.br.Location = new System.Drawing.Point(192, 120);
            this.br.Name = "br";
            this.br.Size = new System.Drawing.Size(32, 32);
            this.br.TabIndex = 52;
            this.br.TabStop = false;
            this.br.Text = "r";
            this.br.UseVisualStyleBackColor = false;
            // 
            // be
            // 
            this.be.BackColor = System.Drawing.Color.LightSkyBlue;
            this.be.Location = new System.Drawing.Point(152, 120);
            this.be.Name = "be";
            this.be.Size = new System.Drawing.Size(32, 32);
            this.be.TabIndex = 51;
            this.be.TabStop = false;
            this.be.Text = "e";
            this.be.UseVisualStyleBackColor = false;
            // 
            // bw
            // 
            this.bw.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bw.Location = new System.Drawing.Point(112, 120);
            this.bw.Name = "bw";
            this.bw.Size = new System.Drawing.Size(32, 32);
            this.bw.TabIndex = 50;
            this.bw.TabStop = false;
            this.bw.Text = "w";
            this.bw.UseVisualStyleBackColor = false;
            // 
            // bq
            // 
            this.bq.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bq.Location = new System.Drawing.Point(72, 120);
            this.bq.Name = "bq";
            this.bq.Size = new System.Drawing.Size(32, 32);
            this.bq.TabIndex = 49;
            this.bq.TabStop = false;
            this.bq.Text = "q";
            this.bq.UseVisualStyleBackColor = false;
            // 
            // bCedilla
            // 
            this.bCedilla.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bCedilla.Location = new System.Drawing.Point(528, 160);
            this.bCedilla.Name = "bCedilla";
            this.bCedilla.Size = new System.Drawing.Size(32, 32);
            this.bCedilla.TabIndex = 72;
            this.bCedilla.TabStop = false;
            this.bCedilla.Text = "ç";
            this.bCedilla.UseVisualStyleBackColor = false;
            // 
            // bDieresis
            // 
            this.bDieresis.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bDieresis.Location = new System.Drawing.Point(488, 160);
            this.bDieresis.Name = "bDieresis";
            this.bDieresis.Size = new System.Drawing.Size(32, 32);
            this.bDieresis.TabIndex = 71;
            this.bDieresis.TabStop = false;
            this.bDieresis.Text = "´";
            this.bDieresis.UseVisualStyleBackColor = false;
            // 
            // bl
            // 
            this.bl.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bl.Location = new System.Drawing.Point(408, 160);
            this.bl.Name = "bl";
            this.bl.Size = new System.Drawing.Size(32, 32);
            this.bl.TabIndex = 69;
            this.bl.TabStop = false;
            this.bl.Text = "l";
            this.bl.UseVisualStyleBackColor = false;
            // 
            // bñ
            // 
            this.bñ.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bñ.Location = new System.Drawing.Point(448, 160);
            this.bñ.Name = "bñ";
            this.bñ.Size = new System.Drawing.Size(32, 32);
            this.bñ.TabIndex = 70;
            this.bñ.TabStop = false;
            this.bñ.Text = "ñ";
            this.bñ.UseVisualStyleBackColor = false;
            // 
            // bk
            // 
            this.bk.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bk.Location = new System.Drawing.Point(368, 160);
            this.bk.Name = "bk";
            this.bk.Size = new System.Drawing.Size(32, 32);
            this.bk.TabIndex = 68;
            this.bk.TabStop = false;
            this.bk.Text = "k";
            this.bk.UseVisualStyleBackColor = false;
            // 
            // bj
            // 
            this.bj.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bj.Location = new System.Drawing.Point(328, 160);
            this.bj.Name = "bj";
            this.bj.Size = new System.Drawing.Size(32, 32);
            this.bj.TabIndex = 67;
            this.bj.TabStop = false;
            this.bj.Text = "j";
            this.bj.UseVisualStyleBackColor = false;
            // 
            // bh
            // 
            this.bh.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bh.Location = new System.Drawing.Point(288, 160);
            this.bh.Name = "bh";
            this.bh.Size = new System.Drawing.Size(32, 32);
            this.bh.TabIndex = 66;
            this.bh.TabStop = false;
            this.bh.Text = "h";
            this.bh.UseVisualStyleBackColor = false;
            // 
            // bg
            // 
            this.bg.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bg.Location = new System.Drawing.Point(248, 160);
            this.bg.Name = "bg";
            this.bg.Size = new System.Drawing.Size(32, 32);
            this.bg.TabIndex = 65;
            this.bg.TabStop = false;
            this.bg.Text = "g";
            this.bg.UseVisualStyleBackColor = false;
            // 
            // bf
            // 
            this.bf.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bf.Location = new System.Drawing.Point(208, 160);
            this.bf.Name = "bf";
            this.bf.Size = new System.Drawing.Size(32, 32);
            this.bf.TabIndex = 64;
            this.bf.TabStop = false;
            this.bf.Text = "f";
            this.bf.UseVisualStyleBackColor = false;
            // 
            // bd
            // 
            this.bd.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bd.Location = new System.Drawing.Point(168, 160);
            this.bd.Name = "bd";
            this.bd.Size = new System.Drawing.Size(32, 32);
            this.bd.TabIndex = 63;
            this.bd.TabStop = false;
            this.bd.Text = "d";
            this.bd.UseVisualStyleBackColor = false;
            // 
            // bs
            // 
            this.bs.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bs.Location = new System.Drawing.Point(128, 160);
            this.bs.Name = "bs";
            this.bs.Size = new System.Drawing.Size(32, 32);
            this.bs.TabIndex = 62;
            this.bs.TabStop = false;
            this.bs.Text = "s";
            this.bs.UseVisualStyleBackColor = false;
            // 
            // ba
            // 
            this.ba.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ba.Location = new System.Drawing.Point(88, 160);
            this.ba.Name = "ba";
            this.ba.Size = new System.Drawing.Size(32, 32);
            this.ba.TabIndex = 61;
            this.ba.TabStop = false;
            this.ba.Text = "a";
            this.ba.UseVisualStyleBackColor = false;
            // 
            // bGuion
            // 
            this.bGuion.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bGuion.Location = new System.Drawing.Point(472, 200);
            this.bGuion.Name = "bGuion";
            this.bGuion.Size = new System.Drawing.Size(32, 32);
            this.bGuion.TabIndex = 85;
            this.bGuion.TabStop = false;
            this.bGuion.Text = "-";
            this.bGuion.UseVisualStyleBackColor = false;
            // 
            // bPunto
            // 
            this.bPunto.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bPunto.Location = new System.Drawing.Point(432, 200);
            this.bPunto.Name = "bPunto";
            this.bPunto.Size = new System.Drawing.Size(32, 32);
            this.bPunto.TabIndex = 84;
            this.bPunto.TabStop = false;
            this.bPunto.Text = ".";
            this.bPunto.UseVisualStyleBackColor = false;
            // 
            // bComa
            // 
            this.bComa.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bComa.Location = new System.Drawing.Point(392, 200);
            this.bComa.Name = "bComa";
            this.bComa.Size = new System.Drawing.Size(32, 32);
            this.bComa.TabIndex = 83;
            this.bComa.TabStop = false;
            this.bComa.Text = ",";
            this.bComa.UseVisualStyleBackColor = false;
            // 
            // bm
            // 
            this.bm.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bm.Location = new System.Drawing.Point(352, 200);
            this.bm.Name = "bm";
            this.bm.Size = new System.Drawing.Size(32, 32);
            this.bm.TabIndex = 82;
            this.bm.TabStop = false;
            this.bm.Text = "m";
            this.bm.UseVisualStyleBackColor = false;
            // 
            // bn
            // 
            this.bn.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bn.Location = new System.Drawing.Point(312, 200);
            this.bn.Name = "bn";
            this.bn.Size = new System.Drawing.Size(32, 32);
            this.bn.TabIndex = 81;
            this.bn.TabStop = false;
            this.bn.Text = "n";
            this.bn.UseVisualStyleBackColor = false;
            // 
            // bb
            // 
            this.bb.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bb.Location = new System.Drawing.Point(272, 200);
            this.bb.Name = "bb";
            this.bb.Size = new System.Drawing.Size(32, 32);
            this.bb.TabIndex = 80;
            this.bb.TabStop = false;
            this.bb.Text = "b";
            this.bb.UseVisualStyleBackColor = false;
            // 
            // bv
            // 
            this.bv.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bv.Location = new System.Drawing.Point(232, 200);
            this.bv.Name = "bv";
            this.bv.Size = new System.Drawing.Size(32, 32);
            this.bv.TabIndex = 79;
            this.bv.TabStop = false;
            this.bv.Text = "v";
            this.bv.UseVisualStyleBackColor = false;
            // 
            // bc
            // 
            this.bc.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bc.Location = new System.Drawing.Point(192, 200);
            this.bc.Name = "bc";
            this.bc.Size = new System.Drawing.Size(32, 32);
            this.bc.TabIndex = 78;
            this.bc.TabStop = false;
            this.bc.Text = "c";
            this.bc.UseVisualStyleBackColor = false;
            // 
            // bx
            // 
            this.bx.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bx.Location = new System.Drawing.Point(152, 200);
            this.bx.Name = "bx";
            this.bx.Size = new System.Drawing.Size(32, 32);
            this.bx.TabIndex = 77;
            this.bx.TabStop = false;
            this.bx.Text = "x";
            this.bx.UseVisualStyleBackColor = false;
            // 
            // bz
            // 
            this.bz.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bz.Location = new System.Drawing.Point(112, 200);
            this.bz.Name = "bz";
            this.bz.Size = new System.Drawing.Size(32, 32);
            this.bz.TabIndex = 76;
            this.bz.TabStop = false;
            this.bz.Text = "z";
            this.bz.UseVisualStyleBackColor = false;
            // 
            // bMenorque
            // 
            this.bMenorque.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bMenorque.Location = new System.Drawing.Point(72, 200);
            this.bMenorque.Name = "bMenorque";
            this.bMenorque.Size = new System.Drawing.Size(32, 32);
            this.bMenorque.TabIndex = 75;
            this.bMenorque.TabStop = false;
            this.bMenorque.Text = "<";
            this.bMenorque.UseVisualStyleBackColor = false;
            // 
            // bMayusIzq
            // 
            this.bMayusIzq.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bMayusIzq.Location = new System.Drawing.Point(8, 200);
            this.bMayusIzq.Name = "bMayusIzq";
            this.bMayusIzq.Size = new System.Drawing.Size(56, 32);
            this.bMayusIzq.TabIndex = 74;
            this.bMayusIzq.TabStop = false;
            this.bMayusIzq.Text = "Shift";
            this.bMayusIzq.UseVisualStyleBackColor = false;
            // 
            // bMayusDer
            // 
            this.bMayusDer.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bMayusDer.Location = new System.Drawing.Point(512, 200);
            this.bMayusDer.Name = "bMayusDer";
            this.bMayusDer.Size = new System.Drawing.Size(96, 32);
            this.bMayusDer.TabIndex = 73;
            this.bMayusDer.TabStop = false;
            this.bMayusDer.Text = "Cerrar";
            this.bMayusDer.UseVisualStyleBackColor = false;
            // 
            // bControl
            // 
            this.bControl.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bControl.Location = new System.Drawing.Point(8, 240);
            this.bControl.Name = "bControl";
            this.bControl.Size = new System.Drawing.Size(72, 32);
            this.bControl.TabIndex = 87;
            this.bControl.TabStop = false;
            this.bControl.Text = "Borrar";
            this.bControl.UseVisualStyleBackColor = false;
            // 
            // bAlt
            // 
            this.bAlt.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bAlt.Location = new System.Drawing.Point(120, 240);
            this.bAlt.Name = "bAlt";
            this.bAlt.Size = new System.Drawing.Size(56, 32);
            this.bAlt.TabIndex = 88;
            this.bAlt.TabStop = false;
            this.bAlt.Text = "Alt";
            this.bAlt.UseVisualStyleBackColor = false;
            // 
            // bSend
            // 
            this.bSend.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bSend.Location = new System.Drawing.Point(536, 240);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(72, 32);
            this.bSend.TabIndex = 89;
            this.bSend.TabStop = false;
            this.bSend.Text = "Enviar";
            this.bSend.UseVisualStyleBackColor = false;
            // 
            // txtKeyboard
            // 
            this.txtKeyboard.AcceptsReturn = true;
            this.txtKeyboard.Location = new System.Drawing.Point(8, 8);
            this.txtKeyboard.Multiline = true;
            this.txtKeyboard.Name = "txtKeyboard";
            this.txtKeyboard.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtKeyboard.Size = new System.Drawing.Size(600, 64);
            this.txtKeyboard.TabIndex = 90;
            // 
            // bLeft
            // 
            this.bLeft.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bLeft.Location = new System.Drawing.Point(456, 240);
            this.bLeft.Name = "bLeft";
            this.bLeft.Size = new System.Drawing.Size(32, 32);
            this.bLeft.TabIndex = 38;
            this.bLeft.TabStop = false;
            this.bLeft.Text = "<--";
            this.bLeft.UseVisualStyleBackColor = false;
            // 
            // bRight
            // 
            this.bRight.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bRight.Location = new System.Drawing.Point(496, 240);
            this.bRight.Name = "bRight";
            this.bRight.Size = new System.Drawing.Size(32, 32);
            this.bRight.TabIndex = 91;
            this.bRight.TabStop = false;
            this.bRight.Text = "-->";
            this.bRight.UseVisualStyleBackColor = false;
            // 
            // frmKeyboard
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(617, 279);
            this.Controls.Add(this.bRight);
            this.Controls.Add(this.txtKeyboard);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.bAlt);
            this.Controls.Add(this.bControl);
            this.Controls.Add(this.bGuion);
            this.Controls.Add(this.bPunto);
            this.Controls.Add(this.bComa);
            this.Controls.Add(this.bm);
            this.Controls.Add(this.bn);
            this.Controls.Add(this.bb);
            this.Controls.Add(this.bv);
            this.Controls.Add(this.bc);
            this.Controls.Add(this.bx);
            this.Controls.Add(this.bz);
            this.Controls.Add(this.bMenorque);
            this.Controls.Add(this.bMayusIzq);
            this.Controls.Add(this.bMayusDer);
            this.Controls.Add(this.bCedilla);
            this.Controls.Add(this.bDieresis);
            this.Controls.Add(this.bñ);
            this.Controls.Add(this.bl);
            this.Controls.Add(this.bk);
            this.Controls.Add(this.bj);
            this.Controls.Add(this.bh);
            this.Controls.Add(this.bg);
            this.Controls.Add(this.bf);
            this.Controls.Add(this.bd);
            this.Controls.Add(this.bs);
            this.Controls.Add(this.ba);
            this.Controls.Add(this.bMas);
            this.Controls.Add(this.bComita);
            this.Controls.Add(this.bp);
            this.Controls.Add(this.bo);
            this.Controls.Add(this.bi);
            this.Controls.Add(this.bu);
            this.Controls.Add(this.by);
            this.Controls.Add(this.bt);
            this.Controls.Add(this.br);
            this.Controls.Add(this.be);
            this.Controls.Add(this.bw);
            this.Controls.Add(this.bq);
            this.Controls.Add(this.bBorrar);
            this.Controls.Add(this.bIntIni);
            this.Controls.Add(this.bIntFin);
            this.Controls.Add(this.bTab);
            this.Controls.Add(this.bMul);
            this.Controls.Add(this.boa);
            this.Controls.Add(this.bLeft);
            this.Controls.Add(this.bSpacio);
            this.Controls.Add(this.bMayus);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmKeyboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Teclado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}