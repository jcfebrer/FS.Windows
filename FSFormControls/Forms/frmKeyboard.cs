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
        public DBTextBox m_TextBox;

        public DBTextBox TextBox
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
            b3 = new Button();
            b2 = new Button();
            b1 = new Button();
            boa = new Button();
            bMul = new Button();
            bIntFin = new Button();
            bIntIni = new Button();
            b0 = new Button();
            b9 = new Button();
            b8 = new Button();
            b7 = new Button();
            b6 = new Button();
            b5 = new Button();
            b4 = new Button();
            bMayus = new Button();
            bSpacio = new Button();
            bTab = new Button();
            bBorrar = new Button();
            bMas = new Button();
            bComita = new Button();
            bp = new Button();
            bo = new Button();
            bi = new Button();
            bu = new Button();
            by = new Button();
            bt = new Button();
            br = new Button();
            be = new Button();
            bw = new Button();
            bq = new Button();
            bCedilla = new Button();
            bDieresis = new Button();
            bl = new Button();
            bñ = new Button();
            bk = new Button();
            bj = new Button();
            bh = new Button();
            bg = new Button();
            bf = new Button();
            bd = new Button();
            bs = new Button();
            ba = new Button();
            bGuion = new Button();
            bPunto = new Button();
            bComa = new Button();
            bm = new Button();
            bn = new Button();
            bb = new Button();
            bv = new Button();
            bc = new Button();
            bx = new Button();
            bz = new Button();
            bMenorque = new Button();
            bMayusIzq = new Button();
            bMayusDer = new Button();
            bControl = new Button();
            bAlt = new Button();
            bSend = new Button();
            txtKeyboard = new TextBox();
            bLeft = new Button();
            bRight = new Button();
            SuspendLayout();
            // 
            // b3
            // 
            b3.BackColor = Color.LightSkyBlue;
            b3.Location = new Point(128, 80);
            b3.Name = "b3";
            b3.Size = new Size(32, 32);
            b3.TabIndex = 28;
            b3.TabStop = false;
            b3.Text = "3";
            b3.UseVisualStyleBackColor = false;
            // 
            // b2
            // 
            b2.BackColor = Color.LightSkyBlue;
            b2.Location = new Point(88, 80);
            b2.Name = "b2";
            b2.Size = new Size(32, 32);
            b2.TabIndex = 27;
            b2.TabStop = false;
            b2.Text = "2";
            b2.UseVisualStyleBackColor = false;
            // 
            // b1
            // 
            b1.BackColor = Color.LightSkyBlue;
            b1.Location = new Point(48, 80);
            b1.Name = "b1";
            b1.Size = new Size(32, 32);
            b1.TabIndex = 26;
            b1.TabStop = false;
            b1.Text = "1";
            b1.UseVisualStyleBackColor = false;
            // 
            // boa
            // 
            boa.BackColor = Color.LightSkyBlue;
            boa.Location = new Point(8, 80);
            boa.Name = "boa";
            boa.Size = new Size(32, 32);
            boa.TabIndex = 39;
            boa.TabStop = false;
            boa.Text = "º";
            boa.UseVisualStyleBackColor = false;
            // 
            // bMul
            // 
            bMul.BackColor = Color.LightSkyBlue;
            bMul.Location = new Point(568, 120);
            bMul.Name = "bMul";
            bMul.Size = new Size(40, 72);
            bMul.TabIndex = 40;
            bMul.TabStop = false;
            bMul.Text = "Enter";
            bMul.UseVisualStyleBackColor = false;
            // 
            // bIntFin
            // 
            bIntFin.BackColor = Color.LightSkyBlue;
            bIntFin.Location = new Point(448, 80);
            bIntFin.Name = "bIntFin";
            bIntFin.Size = new Size(32, 32);
            bIntFin.TabIndex = 44;
            bIntFin.TabStop = false;
            bIntFin.Text = "\'";
            bIntFin.UseVisualStyleBackColor = false;
            // 
            // bIntIni
            // 
            bIntIni.BackColor = Color.LightSkyBlue;
            bIntIni.Location = new Point(488, 80);
            bIntIni.Name = "bIntIni";
            bIntIni.Size = new Size(32, 32);
            bIntIni.TabIndex = 45;
            bIntIni.TabStop = false;
            bIntIni.Text = "¡";
            bIntIni.UseVisualStyleBackColor = false;
            // 
            // b0
            // 
            b0.BackColor = Color.LightSkyBlue;
            b0.Location = new Point(408, 80);
            b0.Name = "b0";
            b0.Size = new Size(32, 32);
            b0.TabIndex = 35;
            b0.TabStop = false;
            b0.Text = "0";
            b0.UseVisualStyleBackColor = false;
            // 
            // b9
            // 
            b9.BackColor = Color.LightSkyBlue;
            b9.Location = new Point(368, 80);
            b9.Name = "b9";
            b9.Size = new Size(32, 32);
            b9.TabIndex = 34;
            b9.TabStop = false;
            b9.Text = "9";
            b9.UseVisualStyleBackColor = false;
            // 
            // b8
            // 
            b8.BackColor = Color.LightSkyBlue;
            b8.Location = new Point(328, 80);
            b8.Name = "b8";
            b8.Size = new Size(32, 32);
            b8.TabIndex = 33;
            b8.TabStop = false;
            b8.Text = "8";
            b8.UseVisualStyleBackColor = false;
            // 
            // b7
            // 
            b7.BackColor = Color.LightSkyBlue;
            b7.Location = new Point(288, 80);
            b7.Name = "b7";
            b7.Size = new Size(32, 32);
            b7.TabIndex = 32;
            b7.TabStop = false;
            b7.Text = "7";
            b7.UseVisualStyleBackColor = false;
            // 
            // b6
            // 
            b6.BackColor = Color.LightSkyBlue;
            b6.Location = new Point(248, 80);
            b6.Name = "b6";
            b6.Size = new Size(32, 32);
            b6.TabIndex = 31;
            b6.TabStop = false;
            b6.Text = "6";
            b6.UseVisualStyleBackColor = false;
            // 
            // b5
            // 
            b5.BackColor = Color.LightSkyBlue;
            b5.Location = new Point(208, 80);
            b5.Name = "b5";
            b5.Size = new Size(32, 32);
            b5.TabIndex = 30;
            b5.TabStop = false;
            b5.Text = "5";
            b5.UseVisualStyleBackColor = false;
            // 
            // b4
            // 
            b4.BackColor = Color.LightSkyBlue;
            b4.Location = new Point(168, 80);
            b4.Name = "b4";
            b4.Size = new Size(32, 32);
            b4.TabIndex = 29;
            b4.TabStop = false;
            b4.Text = "4";
            b4.UseVisualStyleBackColor = false;
            // 
            // bMayus
            // 
            bMayus.BackColor = Color.LightSkyBlue;
            bMayus.Location = new Point(8, 160);
            bMayus.Name = "bMayus";
            bMayus.Size = new Size(72, 32);
            bMayus.TabIndex = 36;
            bMayus.TabStop = false;
            bMayus.Text = "Bloq Mayús";
            bMayus.UseVisualStyleBackColor = false;
            // 
            // bSpacio
            // 
            bSpacio.BackColor = Color.LightSkyBlue;
            bSpacio.Location = new Point(184, 240);
            bSpacio.Name = "bSpacio";
            bSpacio.Size = new Size(264, 32);
            bSpacio.TabIndex = 37;
            bSpacio.TabStop = false;
            bSpacio.Text = "Space";
            bSpacio.UseVisualStyleBackColor = false;
            // 
            // bTab
            // 
            bTab.BackColor = Color.LightSkyBlue;
            bTab.Location = new Point(8, 120);
            bTab.Name = "bTab";
            bTab.Size = new Size(56, 32);
            bTab.TabIndex = 43;
            bTab.TabStop = false;
            bTab.Text = "Tab";
            bTab.UseVisualStyleBackColor = false;
            // 
            // bBorrar
            // 
            bBorrar.BackColor = Color.LightSkyBlue;
            bBorrar.Location = new Point(528, 80);
            bBorrar.Name = "bBorrar";
            bBorrar.Size = new Size(80, 32);
            bBorrar.TabIndex = 42;
            bBorrar.TabStop = false;
            bBorrar.Text = "<--";
            bBorrar.UseVisualStyleBackColor = false;
            // 
            // bMas
            // 
            bMas.BackColor = Color.LightSkyBlue;
            bMas.Location = new Point(512, 120);
            bMas.Name = "bMas";
            bMas.Size = new Size(32, 32);
            bMas.TabIndex = 60;
            bMas.TabStop = false;
            bMas.Text = "+";
            bMas.UseVisualStyleBackColor = false;
            // 
            // bComita
            // 
            bComita.BackColor = Color.LightSkyBlue;
            bComita.Location = new Point(472, 120);
            bComita.Name = "bComita";
            bComita.Size = new Size(32, 32);
            bComita.TabIndex = 59;
            bComita.TabStop = false;
            bComita.Text = "\'";
            bComita.UseVisualStyleBackColor = false;
            // 
            // bp
            // 
            bp.BackColor = Color.LightSkyBlue;
            bp.Location = new Point(432, 120);
            bp.Name = "bp";
            bp.Size = new Size(32, 32);
            bp.TabIndex = 58;
            bp.TabStop = false;
            bp.Text = "p";
            bp.UseVisualStyleBackColor = false;
            // 
            // bo
            // 
            bo.BackColor = Color.LightSkyBlue;
            bo.Location = new Point(392, 120);
            bo.Name = "bo";
            bo.Size = new Size(32, 32);
            bo.TabIndex = 57;
            bo.TabStop = false;
            bo.Text = "o";
            bo.UseVisualStyleBackColor = false;
            // 
            // bi
            // 
            bi.BackColor = Color.LightSkyBlue;
            bi.Location = new Point(352, 120);
            bi.Name = "bi";
            bi.Size = new Size(32, 32);
            bi.TabIndex = 56;
            bi.TabStop = false;
            bi.Text = "i";
            bi.UseVisualStyleBackColor = false;
            // 
            // bu
            // 
            bu.BackColor = Color.LightSkyBlue;
            bu.Location = new Point(312, 120);
            bu.Name = "bu";
            bu.Size = new Size(32, 32);
            bu.TabIndex = 55;
            bu.TabStop = false;
            bu.Text = "u";
            bu.UseVisualStyleBackColor = false;
            // 
            // by
            // 
            by.BackColor = Color.LightSkyBlue;
            by.Location = new Point(272, 120);
            by.Name = "by";
            by.Size = new Size(32, 32);
            by.TabIndex = 54;
            by.TabStop = false;
            by.Text = "y";
            by.UseVisualStyleBackColor = false;
            // 
            // bt
            // 
            bt.BackColor = Color.LightSkyBlue;
            bt.Location = new Point(232, 120);
            bt.Name = "bt";
            bt.Size = new Size(32, 32);
            bt.TabIndex = 53;
            bt.TabStop = false;
            bt.Text = "t";
            bt.UseVisualStyleBackColor = false;
            // 
            // br
            // 
            br.BackColor = Color.LightSkyBlue;
            br.Location = new Point(192, 120);
            br.Name = "br";
            br.Size = new Size(32, 32);
            br.TabIndex = 52;
            br.TabStop = false;
            br.Text = "r";
            br.UseVisualStyleBackColor = false;
            // 
            // be
            // 
            be.BackColor = Color.LightSkyBlue;
            be.Location = new Point(152, 120);
            be.Name = "be";
            be.Size = new Size(32, 32);
            be.TabIndex = 51;
            be.TabStop = false;
            be.Text = "e";
            be.UseVisualStyleBackColor = false;
            // 
            // bw
            // 
            bw.BackColor = Color.LightSkyBlue;
            bw.Location = new Point(112, 120);
            bw.Name = "bw";
            bw.Size = new Size(32, 32);
            bw.TabIndex = 50;
            bw.TabStop = false;
            bw.Text = "w";
            bw.UseVisualStyleBackColor = false;
            // 
            // bq
            // 
            bq.BackColor = Color.LightSkyBlue;
            bq.Location = new Point(72, 120);
            bq.Name = "bq";
            bq.Size = new Size(32, 32);
            bq.TabIndex = 49;
            bq.TabStop = false;
            bq.Text = "q";
            bq.UseVisualStyleBackColor = false;
            // 
            // bCedilla
            // 
            bCedilla.BackColor = Color.LightSkyBlue;
            bCedilla.Location = new Point(528, 160);
            bCedilla.Name = "bCedilla";
            bCedilla.Size = new Size(32, 32);
            bCedilla.TabIndex = 72;
            bCedilla.TabStop = false;
            bCedilla.Text = "ç";
            bCedilla.UseVisualStyleBackColor = false;
            // 
            // bDieresis
            // 
            bDieresis.BackColor = Color.LightSkyBlue;
            bDieresis.Location = new Point(488, 160);
            bDieresis.Name = "bDieresis";
            bDieresis.Size = new Size(32, 32);
            bDieresis.TabIndex = 71;
            bDieresis.TabStop = false;
            bDieresis.Text = "´";
            bDieresis.UseVisualStyleBackColor = false;
            // 
            // bl
            // 
            bl.BackColor = Color.LightSkyBlue;
            bl.Location = new Point(408, 160);
            bl.Name = "bl";
            bl.Size = new Size(32, 32);
            bl.TabIndex = 69;
            bl.TabStop = false;
            bl.Text = "l";
            bl.UseVisualStyleBackColor = false;
            // 
            // bñ
            // 
            bñ.BackColor = Color.LightSkyBlue;
            bñ.Location = new Point(448, 160);
            bñ.Name = "bñ";
            bñ.Size = new Size(32, 32);
            bñ.TabIndex = 70;
            bñ.TabStop = false;
            bñ.Text = "ñ";
            bñ.UseVisualStyleBackColor = false;
            // 
            // bk
            // 
            bk.BackColor = Color.LightSkyBlue;
            bk.Location = new Point(368, 160);
            bk.Name = "bk";
            bk.Size = new Size(32, 32);
            bk.TabIndex = 68;
            bk.TabStop = false;
            bk.Text = "k";
            bk.UseVisualStyleBackColor = false;
            // 
            // bj
            // 
            bj.BackColor = Color.LightSkyBlue;
            bj.Location = new Point(328, 160);
            bj.Name = "bj";
            bj.Size = new Size(32, 32);
            bj.TabIndex = 67;
            bj.TabStop = false;
            bj.Text = "j";
            bj.UseVisualStyleBackColor = false;
            // 
            // bh
            // 
            bh.BackColor = Color.LightSkyBlue;
            bh.Location = new Point(288, 160);
            bh.Name = "bh";
            bh.Size = new Size(32, 32);
            bh.TabIndex = 66;
            bh.TabStop = false;
            bh.Text = "h";
            bh.UseVisualStyleBackColor = false;
            // 
            // bg
            // 
            bg.BackColor = Color.LightSkyBlue;
            bg.Location = new Point(248, 160);
            bg.Name = "bg";
            bg.Size = new Size(32, 32);
            bg.TabIndex = 65;
            bg.TabStop = false;
            bg.Text = "g";
            bg.UseVisualStyleBackColor = false;
            // 
            // bf
            // 
            bf.BackColor = Color.LightSkyBlue;
            bf.Location = new Point(208, 160);
            bf.Name = "bf";
            bf.Size = new Size(32, 32);
            bf.TabIndex = 64;
            bf.TabStop = false;
            bf.Text = "f";
            bf.UseVisualStyleBackColor = false;
            // 
            // bd
            // 
            bd.BackColor = Color.LightSkyBlue;
            bd.Location = new Point(168, 160);
            bd.Name = "bd";
            bd.Size = new Size(32, 32);
            bd.TabIndex = 63;
            bd.TabStop = false;
            bd.Text = "d";
            bd.UseVisualStyleBackColor = false;
            // 
            // bs
            // 
            bs.BackColor = Color.LightSkyBlue;
            bs.Location = new Point(128, 160);
            bs.Name = "bs";
            bs.Size = new Size(32, 32);
            bs.TabIndex = 62;
            bs.TabStop = false;
            bs.Text = "s";
            bs.UseVisualStyleBackColor = false;
            // 
            // ba
            // 
            ba.BackColor = Color.LightSkyBlue;
            ba.Location = new Point(88, 160);
            ba.Name = "ba";
            ba.Size = new Size(32, 32);
            ba.TabIndex = 61;
            ba.TabStop = false;
            ba.Text = "a";
            ba.UseVisualStyleBackColor = false;
            // 
            // bGuion
            // 
            bGuion.BackColor = Color.LightSkyBlue;
            bGuion.Location = new Point(472, 200);
            bGuion.Name = "bGuion";
            bGuion.Size = new Size(32, 32);
            bGuion.TabIndex = 85;
            bGuion.TabStop = false;
            bGuion.Text = "-";
            bGuion.UseVisualStyleBackColor = false;
            // 
            // bPunto
            // 
            bPunto.BackColor = Color.LightSkyBlue;
            bPunto.Location = new Point(432, 200);
            bPunto.Name = "bPunto";
            bPunto.Size = new Size(32, 32);
            bPunto.TabIndex = 84;
            bPunto.TabStop = false;
            bPunto.Text = ".";
            bPunto.UseVisualStyleBackColor = false;
            // 
            // bComa
            // 
            bComa.BackColor = Color.LightSkyBlue;
            bComa.Location = new Point(392, 200);
            bComa.Name = "bComa";
            bComa.Size = new Size(32, 32);
            bComa.TabIndex = 83;
            bComa.TabStop = false;
            bComa.Text = ",";
            bComa.UseVisualStyleBackColor = false;
            // 
            // bm
            // 
            bm.BackColor = Color.LightSkyBlue;
            bm.Location = new Point(352, 200);
            bm.Name = "bm";
            bm.Size = new Size(32, 32);
            bm.TabIndex = 82;
            bm.TabStop = false;
            bm.Text = "m";
            bm.UseVisualStyleBackColor = false;
            // 
            // bn
            // 
            bn.BackColor = Color.LightSkyBlue;
            bn.Location = new Point(312, 200);
            bn.Name = "bn";
            bn.Size = new Size(32, 32);
            bn.TabIndex = 81;
            bn.TabStop = false;
            bn.Text = "n";
            bn.UseVisualStyleBackColor = false;
            // 
            // bb
            // 
            bb.BackColor = Color.LightSkyBlue;
            bb.Location = new Point(272, 200);
            bb.Name = "bb";
            bb.Size = new Size(32, 32);
            bb.TabIndex = 80;
            bb.TabStop = false;
            bb.Text = "b";
            bb.UseVisualStyleBackColor = false;
            // 
            // bv
            // 
            bv.BackColor = Color.LightSkyBlue;
            bv.Location = new Point(232, 200);
            bv.Name = "bv";
            bv.Size = new Size(32, 32);
            bv.TabIndex = 79;
            bv.TabStop = false;
            bv.Text = "v";
            bv.UseVisualStyleBackColor = false;
            // 
            // bc
            // 
            bc.BackColor = Color.LightSkyBlue;
            bc.Location = new Point(192, 200);
            bc.Name = "bc";
            bc.Size = new Size(32, 32);
            bc.TabIndex = 78;
            bc.TabStop = false;
            bc.Text = "c";
            bc.UseVisualStyleBackColor = false;
            // 
            // bx
            // 
            bx.BackColor = Color.LightSkyBlue;
            bx.Location = new Point(152, 200);
            bx.Name = "bx";
            bx.Size = new Size(32, 32);
            bx.TabIndex = 77;
            bx.TabStop = false;
            bx.Text = "x";
            bx.UseVisualStyleBackColor = false;
            // 
            // bz
            // 
            bz.BackColor = Color.LightSkyBlue;
            bz.Location = new Point(112, 200);
            bz.Name = "bz";
            bz.Size = new Size(32, 32);
            bz.TabIndex = 76;
            bz.TabStop = false;
            bz.Text = "z";
            bz.UseVisualStyleBackColor = false;
            // 
            // bMenorque
            // 
            bMenorque.BackColor = Color.LightSkyBlue;
            bMenorque.Location = new Point(72, 200);
            bMenorque.Name = "bMenorque";
            bMenorque.Size = new Size(32, 32);
            bMenorque.TabIndex = 75;
            bMenorque.TabStop = false;
            bMenorque.Text = "<";
            bMenorque.UseVisualStyleBackColor = false;
            // 
            // bMayusIzq
            // 
            bMayusIzq.BackColor = Color.LightSkyBlue;
            bMayusIzq.Location = new Point(8, 200);
            bMayusIzq.Name = "bMayusIzq";
            bMayusIzq.Size = new Size(56, 32);
            bMayusIzq.TabIndex = 74;
            bMayusIzq.TabStop = false;
            bMayusIzq.Text = "Shift";
            bMayusIzq.UseVisualStyleBackColor = false;
            // 
            // bMayusDer
            // 
            bMayusDer.BackColor = Color.LightSkyBlue;
            bMayusDer.Location = new Point(512, 200);
            bMayusDer.Name = "bMayusDer";
            bMayusDer.Size = new Size(96, 32);
            bMayusDer.TabIndex = 73;
            bMayusDer.TabStop = false;
            bMayusDer.Text = "Cerrar";
            bMayusDer.UseVisualStyleBackColor = false;
            // 
            // bControl
            // 
            bControl.BackColor = Color.LightSkyBlue;
            bControl.Location = new Point(8, 240);
            bControl.Name = "bControl";
            bControl.Size = new Size(72, 32);
            bControl.TabIndex = 87;
            bControl.TabStop = false;
            bControl.Text = "Borrar";
            bControl.UseVisualStyleBackColor = false;
            // 
            // bAlt
            // 
            bAlt.BackColor = Color.LightSkyBlue;
            bAlt.Location = new Point(120, 240);
            bAlt.Name = "bAlt";
            bAlt.Size = new Size(56, 32);
            bAlt.TabIndex = 88;
            bAlt.TabStop = false;
            bAlt.Text = "Alt";
            bAlt.UseVisualStyleBackColor = false;
            // 
            // bSend
            // 
            bSend.BackColor = Color.LightSkyBlue;
            bSend.Location = new Point(536, 240);
            bSend.Name = "bSend";
            bSend.Size = new Size(72, 32);
            bSend.TabIndex = 89;
            bSend.TabStop = false;
            bSend.Text = "Enviar";
            bSend.UseVisualStyleBackColor = false;
            // 
            // txtKeyboard
            // 
            txtKeyboard.AcceptsReturn = true;
            txtKeyboard.Location = new Point(8, 8);
            txtKeyboard.Multiline = true;
            txtKeyboard.Name = "txtKeyboard";
            txtKeyboard.ScrollBars = ScrollBars.Vertical;
            txtKeyboard.Size = new Size(600, 64);
            txtKeyboard.TabIndex = 90;
            // 
            // bLeft
            // 
            bLeft.BackColor = Color.LightSkyBlue;
            bLeft.Location = new Point(456, 240);
            bLeft.Name = "bLeft";
            bLeft.Size = new Size(32, 32);
            bLeft.TabIndex = 38;
            bLeft.TabStop = false;
            bLeft.Text = "<--";
            bLeft.UseVisualStyleBackColor = false;
            // 
            // bRight
            // 
            bRight.BackColor = Color.LightSkyBlue;
            bRight.Location = new Point(496, 240);
            bRight.Name = "bRight";
            bRight.Size = new Size(32, 32);
            bRight.TabIndex = 91;
            bRight.TabStop = false;
            bRight.Text = "-->";
            bRight.UseVisualStyleBackColor = false;
            // 
            // frmKeyboard
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(617, 279);
            Controls.Add(bRight);
            Controls.Add(txtKeyboard);
            Controls.Add(bSend);
            Controls.Add(bAlt);
            Controls.Add(bControl);
            Controls.Add(bGuion);
            Controls.Add(bPunto);
            Controls.Add(bComa);
            Controls.Add(bm);
            Controls.Add(bn);
            Controls.Add(bb);
            Controls.Add(bv);
            Controls.Add(bc);
            Controls.Add(bx);
            Controls.Add(bz);
            Controls.Add(bMenorque);
            Controls.Add(bMayusIzq);
            Controls.Add(bMayusDer);
            Controls.Add(bCedilla);
            Controls.Add(bDieresis);
            Controls.Add(bñ);
            Controls.Add(bl);
            Controls.Add(bk);
            Controls.Add(bj);
            Controls.Add(bh);
            Controls.Add(bg);
            Controls.Add(bf);
            Controls.Add(bd);
            Controls.Add(bs);
            Controls.Add(ba);
            Controls.Add(bMas);
            Controls.Add(bComita);
            Controls.Add(bp);
            Controls.Add(bo);
            Controls.Add(bi);
            Controls.Add(bu);
            Controls.Add(by);
            Controls.Add(bt);
            Controls.Add(br);
            Controls.Add(be);
            Controls.Add(bw);
            Controls.Add(bq);
            Controls.Add(bBorrar);
            Controls.Add(bIntIni);
            Controls.Add(bIntFin);
            Controls.Add(bTab);
            Controls.Add(bMul);
            Controls.Add(boa);
            Controls.Add(bLeft);
            Controls.Add(bSpacio);
            Controls.Add(bMayus);
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
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "frmKeyboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Teclado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}