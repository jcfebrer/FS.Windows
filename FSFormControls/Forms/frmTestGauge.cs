using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    public partial class frmTestGauge : Form
    {
        public frmTestGauge()
        {
            InitializeComponent();
        }

        private void gauge1_ValueInRangeChanged(object sender, DBGauge.ValueInRangeChangedEventArgs e)
        {
            if (e.valueInRange == 0)
            {
                pictureBox1.BackColor = Color.Green;
            }
            else
            {
                pictureBox1.BackColor = Color.Red;
            }
        }

        private void gauge2_ValueInRangeChanged(object sender, DBGauge.ValueInRangeChangedEventArgs e)
        {
            DBGauge gauge = (DBGauge)sender;

            if (e.valueInRange == 1)
            {
                label1.Text="WARNING!--OBJECTS IN MIRROR MAY APPEAR SLOWER THAN THEY USED TO. " + gauge.CapText;
            }
            else if (e.valueInRange==2)
            {
                label1.Text="IF THIS GAUGE DISPLAYS YOUR CURRENT SPEED AND YOU STILL CAN SEE THIS THEN SOMTHING IS WRONG ;-) " + gauge.CapText;
            }
            else
            {
                label1.Text = "";
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            gauge1.Value = trackBar1.Value;
            gauge2.Value = trackBar1.Value;

            textBox1.Text = gauge1.Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gauge3.Value+=0.5f;
            gauge4.Value += 10;

            if (gauge3.Value >= 50)
            {
                gauge3.Value = 0;
            }

            if (gauge4.Value >= 300)
            {
                gauge4.Value = -300;
            }

            gauge5.Value = gauge3.Value;
            gauge6.Value = (Single)(((Int32)gauge6.Value + 49) % 50);
            gauge7.Value = (Single)(((Int32)gauge7.Value + 49) % 50);
            gauge8.Value = (Single)(((Int32)gauge8.Value + 51) % 50);
            gauge11.Value = (Single)(((Int32)gauge11.Value + 9) % 10);
            gauge9.Value = (Single)(((Int32)gauge9.Value + 99) % 100);
            gauge10.Value = (Single)(((Int32)gauge10.Value + 99) % 100);
            gauge12.Value = (Single)(((Int32)gauge12.Value + 99) % 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if (gauge1.NeedleType == 0)
            {
                gauge1.NeedleType = 1;
            }
            else
            {
                gauge1.NeedleType = 0;
            }

            if (gauge2.NeedleType == 0)
            {
                gauge2.NeedleType = 1;
            }
            else
            {
                gauge2.NeedleType = 0;
            }

            if (gauge3.NeedleType == 0)
            {
                gauge3.NeedleType = 1;
            }
            else
            {
                gauge3.NeedleType = 0;
            }

            if (gauge4.NeedleType == 0)
            {
                gauge4.NeedleType = 1;
            }
            else
            {
                gauge4.NeedleType = 0;
            }

            if (gauge5.NeedleType == 0)
            {
                gauge5.NeedleType = 1;
            }
            else
            {
                gauge5.NeedleType = 0;
            }
        }
    }
}