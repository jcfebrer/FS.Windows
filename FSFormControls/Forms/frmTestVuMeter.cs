using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using static FSFormControls.DBVuMeter;

namespace FSFormControls
{
    public partial class frmTestVuMeter : Form
    {
        public frmTestVuMeter()
        {
            InitializeComponent();

            vuMeter2.LevelChanged += VuMeter2_LevelChanged;
            vuMeter11.LevelChanged += VuMeter11_LevelChanged;
        }

        private void VuMeter2_LevelChanged(object sender, IntEventArgs e)
        {
            lblNivel.Text = "Nivel: " + e.Value.ToString();
        }

        private void VuMeter11_LevelChanged(object sender, IntEventArgs e)
        {
            vuMeter11.VuText = "Nivel: " + e.Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            vuMeter1.Level = trackBar1.Value;
            vuMeter2.Level = trackBar1.Value;
            vuMeter3.Level = trackBar1.Value;
            vuMeter4.Level = trackBar1.Value;
            vuMeter5.Level = trackBar1.Value;
            vuMeter6.Level = trackBar1.Value;
            vuMeter7.Level = trackBar1.Value;
            vuMeter8.Level = trackBar1.Value;
            vuMeter9.Level = trackBar1.Value;
            vuMeter10.Level = trackBar1.Value;
            vuMeter10.VuText = trackBar1.Value.ToString();
            vuMeter11.Level = trackBar1.Value;
            vuMeter12.Level = trackBar1.Value;
            vuMeter13.Level = trackBar1.Value;
        }

    }
}