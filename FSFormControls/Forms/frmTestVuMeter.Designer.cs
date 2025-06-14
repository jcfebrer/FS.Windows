namespace FSFormControls
{
    partial class frmTestVuMeter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestVuMeter));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.vuMeter3 = new FSFormControls.DBVuMeter();
            this.vuMeter13 = new FSFormControls.DBVuMeter();
            this.vuMeter12 = new FSFormControls.DBVuMeter();
            this.vuMeter11 = new FSFormControls.DBVuMeter();
            this.vuMeter10 = new FSFormControls.DBVuMeter();
            this.vuMeter9 = new FSFormControls.DBVuMeter();
            this.vuMeter8 = new FSFormControls.DBVuMeter();
            this.vuMeter7 = new FSFormControls.DBVuMeter();
            this.vuMeter6 = new FSFormControls.DBVuMeter();
            this.vuMeter5 = new FSFormControls.DBVuMeter();
            this.vuMeter4 = new FSFormControls.DBVuMeter();
            this.vuMeter2 = new FSFormControls.DBVuMeter();
            this.vuMeter1 = new FSFormControls.DBVuMeter();
            this.lblNivel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(67, 15);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(147, 45);
            this.trackBar1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 56);
            this.label1.TabIndex = 7;
            this.label1.Text = "Analog and Logarithmic.\r\nPeakindicator selecteble.\r\n1-96 LED:s 32/color.\r\nUser se" +
    "lectable colors.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(264, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 84);
            this.label2.TabIndex = 12;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // vuMeter3
            // 
            this.vuMeter3.About = "";
            this.vuMeter3.AnalogMeter = false;
            this.vuMeter3.BackColor = System.Drawing.Color.DimGray;
            this.vuMeter3.DialBackground = System.Drawing.Color.White;
            this.vuMeter3.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter3.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter3.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter3.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter3.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter3.Led1Count = 6;
            this.vuMeter3.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter3.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter3.Led2Count = 6;
            this.vuMeter3.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter3.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter3.Led3Count = 4;
            this.vuMeter3.LedSize = new System.Drawing.Size(6, 14);
            this.vuMeter3.LedSpace = 3;
            this.vuMeter3.Level = 0;
            this.vuMeter3.LevelMax = 100;
            this.vuMeter3.Location = new System.Drawing.Point(5, 15);
            this.vuMeter3.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Log10;
            this.vuMeter3.Name = "vuMeter3";
            this.vuMeter3.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter3.PeakHold = true;
            this.vuMeter3.Peakms = 1000;
            this.vuMeter3.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter3.ShowDialOnly = false;
            this.vuMeter3.ShowLedPeak = false;
            this.vuMeter3.ShowTextInDial = false;
            this.vuMeter3.Size = new System.Drawing.Size(12, 275);
            this.vuMeter3.TabIndex = 2;
            this.vuMeter3.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter3.UseLedLight = false;
            this.vuMeter3.VerticalBar = true;
            this.vuMeter3.VuText = "VU";
            // 
            // vuMeter13
            // 
            this.vuMeter13.About = "";
            this.vuMeter13.AnalogMeter = true;
            this.vuMeter13.BackColor = System.Drawing.Color.White;
            this.vuMeter13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.vuMeter13.DialBackground = System.Drawing.Color.White;
            this.vuMeter13.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter13.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter13.DialTextZero = System.Drawing.Color.Red;
            this.vuMeter13.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter13.Led1ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.vuMeter13.Led1Count = 3;
            this.vuMeter13.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter13.Led2ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.vuMeter13.Led2Count = 1;
            this.vuMeter13.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter13.Led3ColorOn = System.Drawing.Color.Black;
            this.vuMeter13.Led3Count = 3;
            this.vuMeter13.LedSize = new System.Drawing.Size(2, 4);
            this.vuMeter13.LedSpace = 3;
            this.vuMeter13.Level = 50;
            this.vuMeter13.LevelMax = 100;
            this.vuMeter13.Location = new System.Drawing.Point(345, 193);
            this.vuMeter13.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Analog;
            this.vuMeter13.Name = "vuMeter13";
            this.vuMeter13.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter13.PeakHold = false;
            this.vuMeter13.Peakms = 1000;
            this.vuMeter13.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter13.ShowDialOnly = true;
            this.vuMeter13.ShowLedPeak = true;
            this.vuMeter13.ShowTextInDial = true;
            this.vuMeter13.Size = new System.Drawing.Size(155, 124);
            this.vuMeter13.TabIndex = 15;
            this.vuMeter13.TextInDial = new string[] {
        "-15",
        "-10",
        "-5",
        "0",
        "+5",
        "+10",
        "+15"};
            this.vuMeter13.UseLedLight = true;
            this.vuMeter13.VerticalBar = false;
            this.vuMeter13.VuText = "Volt";
            // 
            // vuMeter12
            // 
            this.vuMeter12.About = "";
            this.vuMeter12.AnalogMeter = true;
            this.vuMeter12.BackColor = System.Drawing.Color.Silver;
            this.vuMeter12.DialBackground = System.Drawing.Color.Silver;
            this.vuMeter12.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter12.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter12.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter12.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vuMeter12.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter12.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter12.Led1Count = 20;
            this.vuMeter12.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter12.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter12.Led2Count = 2;
            this.vuMeter12.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter12.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter12.Led3Count = 20;
            this.vuMeter12.LedSize = new System.Drawing.Size(5, 25);
            this.vuMeter12.LedSpace = 3;
            this.vuMeter12.Level = 0;
            this.vuMeter12.LevelMax = 100;
            this.vuMeter12.Location = new System.Drawing.Point(314, 323);
            this.vuMeter12.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Analog;
            this.vuMeter12.Name = "vuMeter12";
            this.vuMeter12.NeedleColor = System.Drawing.Color.Blue;
            this.vuMeter12.PeakHold = false;
            this.vuMeter12.Peakms = 1000;
            this.vuMeter12.PeakNeedleColor = System.Drawing.Color.Black;
            this.vuMeter12.ShowDialOnly = true;
            this.vuMeter12.ShowLedPeak = true;
            this.vuMeter12.ShowTextInDial = false;
            this.vuMeter12.Size = new System.Drawing.Size(186, 148);
            this.vuMeter12.TabIndex = 14;
            this.vuMeter12.TextInDial = new string[] {
        "-50",
        "0",
        "+50"};
            this.vuMeter12.UseLedLight = true;
            this.vuMeter12.VerticalBar = false;
            this.vuMeter12.VuText = "No values";
            // 
            // vuMeter11
            // 
            this.vuMeter11.About = "";
            this.vuMeter11.AnalogMeter = true;
            this.vuMeter11.BackColor = System.Drawing.Color.White;
            this.vuMeter11.DialBackground = System.Drawing.Color.White;
            this.vuMeter11.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter11.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter11.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter11.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter11.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter11.Led1Count = 6;
            this.vuMeter11.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter11.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter11.Led2Count = 6;
            this.vuMeter11.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter11.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter11.Led3Count = 4;
            this.vuMeter11.LedSize = new System.Drawing.Size(6, 60);
            this.vuMeter11.LedSpace = 3;
            this.vuMeter11.Level = 0;
            this.vuMeter11.LevelMax = 100;
            this.vuMeter11.Location = new System.Drawing.Point(-16, 216);
            this.vuMeter11.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Log10;
            this.vuMeter11.Name = "vuMeter11";
            this.vuMeter11.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter11.PeakHold = true;
            this.vuMeter11.Peakms = 1000;
            this.vuMeter11.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter11.ShowDialOnly = true;
            this.vuMeter11.ShowLedPeak = false;
            this.vuMeter11.ShowTextInDial = true;
            this.vuMeter11.Size = new System.Drawing.Size(396, 316);
            this.vuMeter11.TabIndex = 13;
            this.vuMeter11.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter11.UseLedLight = true;
            this.vuMeter11.VerticalBar = false;
            this.vuMeter11.VuText = "VU";
            // 
            // vuMeter10
            // 
            this.vuMeter10.About = null;
            this.vuMeter10.AnalogMeter = true;
            this.vuMeter10.BackColor = System.Drawing.Color.Maroon;
            this.vuMeter10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.vuMeter10.DialBackground = System.Drawing.Color.Cyan;
            this.vuMeter10.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter10.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter10.DialTextZero = System.Drawing.Color.Red;
            this.vuMeter10.Led1ColorOff = System.Drawing.Color.Cyan;
            this.vuMeter10.Led1ColorOn = System.Drawing.Color.Blue;
            this.vuMeter10.Led1Count = 12;
            this.vuMeter10.Led2ColorOff = System.Drawing.Color.Cyan;
            this.vuMeter10.Led2ColorOn = System.Drawing.Color.Lime;
            this.vuMeter10.Led2Count = 24;
            this.vuMeter10.Led3ColorOff = System.Drawing.Color.Cyan;
            this.vuMeter10.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter10.Led3Count = 12;
            this.vuMeter10.LedSize = new System.Drawing.Size(3, 8);
            this.vuMeter10.LedSpace = 3;
            this.vuMeter10.Level = 0;
            this.vuMeter10.LevelMax = 100;
            this.vuMeter10.Location = new System.Drawing.Point(370, 108);
            this.vuMeter10.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Analog;
            this.vuMeter10.Name = "vuMeter10";
            this.vuMeter10.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter10.PeakHold = false;
            this.vuMeter10.Peakms = 1000;
            this.vuMeter10.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter10.ShowDialOnly = false;
            this.vuMeter10.ShowLedPeak = true;
            this.vuMeter10.ShowTextInDial = true;
            this.vuMeter10.Size = new System.Drawing.Size(99, 79);
            this.vuMeter10.TabIndex = 11;
            this.vuMeter10.TextInDial = new string[] {
        "0",
        "25",
        "50",
        "75",
        "100"};
            this.vuMeter10.UseLedLight = true;
            this.vuMeter10.VerticalBar = false;
            this.vuMeter10.VuText = "Percent";
            // 
            // vuMeter9
            // 
            this.vuMeter9.About = "";
            this.vuMeter9.AnalogMeter = true;
            this.vuMeter9.BackColor = System.Drawing.Color.DimGray;
            this.vuMeter9.DialBackground = System.Drawing.Color.White;
            this.vuMeter9.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter9.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter9.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter9.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vuMeter9.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter9.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter9.Led1Count = 6;
            this.vuMeter9.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter9.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter9.Led2Count = 6;
            this.vuMeter9.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter9.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter9.Led3Count = 4;
            this.vuMeter9.LedSize = new System.Drawing.Size(2, 10);
            this.vuMeter9.LedSpace = 3;
            this.vuMeter9.Level = 0;
            this.vuMeter9.LevelMax = 100;
            this.vuMeter9.Location = new System.Drawing.Point(264, 107);
            this.vuMeter9.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Log10;
            this.vuMeter9.Name = "vuMeter9";
            this.vuMeter9.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter9.PeakHold = true;
            this.vuMeter9.Peakms = 1000;
            this.vuMeter9.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter9.ShowDialOnly = false;
            this.vuMeter9.ShowLedPeak = true;
            this.vuMeter9.ShowTextInDial = true;
            this.vuMeter9.Size = new System.Drawing.Size(100, 80);
            this.vuMeter9.TabIndex = 10;
            this.vuMeter9.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter9.UseLedLight = true;
            this.vuMeter9.VerticalBar = false;
            this.vuMeter9.VuText = "VU";
            // 
            // vuMeter8
            // 
            this.vuMeter8.About = "";
            this.vuMeter8.AnalogMeter = false;
            this.vuMeter8.BackColor = System.Drawing.Color.DimGray;
            this.vuMeter8.DialBackground = System.Drawing.Color.White;
            this.vuMeter8.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter8.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter8.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter8.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter8.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter8.Led1Count = 0;
            this.vuMeter8.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter8.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter8.Led2Count = 16;
            this.vuMeter8.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter8.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter8.Led3Count = 0;
            this.vuMeter8.LedSize = new System.Drawing.Size(12, 4);
            this.vuMeter8.LedSpace = 2;
            this.vuMeter8.Level = 0;
            this.vuMeter8.LevelMax = 100;
            this.vuMeter8.Location = new System.Drawing.Point(23, 179);
            this.vuMeter8.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Analog;
            this.vuMeter8.Name = "vuMeter8";
            this.vuMeter8.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter8.PeakHold = true;
            this.vuMeter8.Peakms = 1000;
            this.vuMeter8.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter8.ShowDialOnly = false;
            this.vuMeter8.ShowLedPeak = false;
            this.vuMeter8.ShowTextInDial = false;
            this.vuMeter8.Size = new System.Drawing.Size(226, 8);
            this.vuMeter8.TabIndex = 9;
            this.vuMeter8.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter8.UseLedLight = false;
            this.vuMeter8.VerticalBar = false;
            this.vuMeter8.VuText = "VU";
            // 
            // vuMeter7
            // 
            this.vuMeter7.About = "";
            this.vuMeter7.AnalogMeter = false;
            this.vuMeter7.BackColor = System.Drawing.Color.DimGray;
            this.vuMeter7.DialBackground = System.Drawing.Color.White;
            this.vuMeter7.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter7.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter7.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter7.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter7.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter7.Led1Count = 32;
            this.vuMeter7.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter7.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter7.Led2Count = 32;
            this.vuMeter7.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter7.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter7.Led3Count = 32;
            this.vuMeter7.LedSize = new System.Drawing.Size(2, 5);
            this.vuMeter7.LedSpace = 0;
            this.vuMeter7.Level = 0;
            this.vuMeter7.LevelMax = 100;
            this.vuMeter7.Location = new System.Drawing.Point(41, 168);
            this.vuMeter7.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Analog;
            this.vuMeter7.Name = "vuMeter7";
            this.vuMeter7.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter7.PeakHold = true;
            this.vuMeter7.Peakms = 1000;
            this.vuMeter7.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter7.ShowDialOnly = false;
            this.vuMeter7.ShowLedPeak = false;
            this.vuMeter7.ShowTextInDial = false;
            this.vuMeter7.Size = new System.Drawing.Size(192, 5);
            this.vuMeter7.TabIndex = 8;
            this.vuMeter7.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter7.UseLedLight = false;
            this.vuMeter7.VerticalBar = false;
            this.vuMeter7.VuText = "VU";
            // 
            // vuMeter6
            // 
            this.vuMeter6.About = "";
            this.vuMeter6.AnalogMeter = false;
            this.vuMeter6.BackColor = System.Drawing.Color.Black;
            this.vuMeter6.DialBackground = System.Drawing.Color.White;
            this.vuMeter6.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter6.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter6.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter6.Led1ColorOff = System.Drawing.Color.Black;
            this.vuMeter6.Led1ColorOn = System.Drawing.Color.White;
            this.vuMeter6.Led1Count = 6;
            this.vuMeter6.Led2ColorOff = System.Drawing.Color.Red;
            this.vuMeter6.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter6.Led2Count = 6;
            this.vuMeter6.Led3ColorOff = System.Drawing.Color.Gray;
            this.vuMeter6.Led3ColorOn = System.Drawing.Color.Lime;
            this.vuMeter6.Led3Count = 4;
            this.vuMeter6.LedSize = new System.Drawing.Size(14, 6);
            this.vuMeter6.LedSpace = 2;
            this.vuMeter6.Level = 0;
            this.vuMeter6.LevelMax = 100;
            this.vuMeter6.Location = new System.Drawing.Point(240, 33);
            this.vuMeter6.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Log10;
            this.vuMeter6.Name = "vuMeter6";
            this.vuMeter6.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter6.PeakHold = true;
            this.vuMeter6.Peakms = 2000;
            this.vuMeter6.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter6.ShowDialOnly = false;
            this.vuMeter6.ShowLedPeak = false;
            this.vuMeter6.ShowTextInDial = false;
            this.vuMeter6.Size = new System.Drawing.Size(18, 130);
            this.vuMeter6.TabIndex = 6;
            this.vuMeter6.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter6.UseLedLight = false;
            this.vuMeter6.VerticalBar = true;
            this.vuMeter6.VuText = "VU";
            // 
            // vuMeter5
            // 
            this.vuMeter5.About = "";
            this.vuMeter5.AnalogMeter = false;
            this.vuMeter5.BackColor = System.Drawing.Color.DimGray;
            this.vuMeter5.DialBackground = System.Drawing.Color.White;
            this.vuMeter5.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter5.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter5.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter5.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter5.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter5.Led1Count = 12;
            this.vuMeter5.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter5.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter5.Led2Count = 12;
            this.vuMeter5.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter5.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter5.Led3Count = 8;
            this.vuMeter5.LedSize = new System.Drawing.Size(12, 3);
            this.vuMeter5.LedSpace = 1;
            this.vuMeter5.Level = 0;
            this.vuMeter5.LevelMax = 100;
            this.vuMeter5.Location = new System.Drawing.Point(220, 33);
            this.vuMeter5.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Log10;
            this.vuMeter5.Name = "vuMeter5";
            this.vuMeter5.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter5.PeakHold = false;
            this.vuMeter5.Peakms = 1000;
            this.vuMeter5.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter5.ShowDialOnly = false;
            this.vuMeter5.ShowLedPeak = false;
            this.vuMeter5.ShowTextInDial = false;
            this.vuMeter5.Size = new System.Drawing.Size(14, 129);
            this.vuMeter5.TabIndex = 5;
            this.vuMeter5.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter5.UseLedLight = false;
            this.vuMeter5.VerticalBar = true;
            this.vuMeter5.VuText = "VU";
            // 
            // vuMeter4
            // 
            this.vuMeter4.About = "";
            this.vuMeter4.AnalogMeter = false;
            this.vuMeter4.BackColor = System.Drawing.Color.DimGray;
            this.vuMeter4.DialBackground = System.Drawing.Color.White;
            this.vuMeter4.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter4.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter4.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter4.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter4.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter4.Led1Count = 1;
            this.vuMeter4.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter4.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter4.Led2Count = 14;
            this.vuMeter4.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter4.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter4.Led3Count = 1;
            this.vuMeter4.LedSize = new System.Drawing.Size(14, 6);
            this.vuMeter4.LedSpace = 3;
            this.vuMeter4.Level = 0;
            this.vuMeter4.LevelMax = 100;
            this.vuMeter4.Location = new System.Drawing.Point(41, 15);
            this.vuMeter4.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Analog;
            this.vuMeter4.Name = "vuMeter4";
            this.vuMeter4.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter4.PeakHold = true;
            this.vuMeter4.Peakms = 1000;
            this.vuMeter4.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter4.ShowDialOnly = false;
            this.vuMeter4.ShowLedPeak = false;
            this.vuMeter4.ShowTextInDial = false;
            this.vuMeter4.Size = new System.Drawing.Size(20, 147);
            this.vuMeter4.TabIndex = 4;
            this.vuMeter4.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter4.UseLedLight = false;
            this.vuMeter4.VerticalBar = true;
            this.vuMeter4.VuText = "VU";
            // 
            // vuMeter2
            // 
            this.vuMeter2.About = "";
            this.vuMeter2.AnalogMeter = false;
            this.vuMeter2.BackColor = System.Drawing.Color.DimGray;
            this.vuMeter2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.vuMeter2.DialBackground = System.Drawing.Color.White;
            this.vuMeter2.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter2.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter2.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter2.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter2.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter2.Led1Count = 6;
            this.vuMeter2.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter2.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter2.Led2Count = 6;
            this.vuMeter2.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter2.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter2.Led3Count = 4;
            this.vuMeter2.LedSize = new System.Drawing.Size(6, 14);
            this.vuMeter2.LedSpace = 3;
            this.vuMeter2.Level = 0;
            this.vuMeter2.LevelMax = 100;
            this.vuMeter2.Location = new System.Drawing.Point(67, 116);
            this.vuMeter2.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Analog;
            this.vuMeter2.Name = "vuMeter2";
            this.vuMeter2.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter2.PeakHold = true;
            this.vuMeter2.Peakms = 1000;
            this.vuMeter2.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter2.ShowDialOnly = false;
            this.vuMeter2.ShowLedPeak = false;
            this.vuMeter2.ShowTextInDial = false;
            this.vuMeter2.Size = new System.Drawing.Size(147, 20);
            this.vuMeter2.TabIndex = 1;
            this.vuMeter2.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter2.UseLedLight = false;
            this.vuMeter2.VerticalBar = false;
            this.vuMeter2.VuText = "VU";
            // 
            // vuMeter1
            // 
            this.vuMeter1.About = "";
            this.vuMeter1.AnalogMeter = false;
            this.vuMeter1.BackColor = System.Drawing.Color.Transparent;
            this.vuMeter1.DialBackground = System.Drawing.Color.White;
            this.vuMeter1.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter1.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter1.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter1.Led1ColorOff = System.Drawing.Color.Black;
            this.vuMeter1.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter1.Led1Count = 6;
            this.vuMeter1.Led2ColorOff = System.Drawing.Color.Black;
            this.vuMeter1.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter1.Led2Count = 6;
            this.vuMeter1.Led3ColorOff = System.Drawing.Color.Black;
            this.vuMeter1.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter1.Led3Count = 4;
            this.vuMeter1.LedSize = new System.Drawing.Size(6, 14);
            this.vuMeter1.LedSpace = 3;
            this.vuMeter1.Level = 0;
            this.vuMeter1.LevelMax = 100;
            this.vuMeter1.Location = new System.Drawing.Point(67, 142);
            this.vuMeter1.MeterScale = FSFormControls.DBVuMeter.MeterScaleEnum.Log10;
            this.vuMeter1.Name = "vuMeter1";
            this.vuMeter1.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter1.PeakHold = true;
            this.vuMeter1.Peakms = 1000;
            this.vuMeter1.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter1.ShowDialOnly = false;
            this.vuMeter1.ShowLedPeak = false;
            this.vuMeter1.ShowTextInDial = false;
            this.vuMeter1.Size = new System.Drawing.Size(147, 20);
            this.vuMeter1.TabIndex = 0;
            this.vuMeter1.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter1.UseLedLight = false;
            this.vuMeter1.VerticalBar = false;
            this.vuMeter1.VuText = "VU";
            // 
            // lblNivel
            // 
            this.lblNivel.AutoSize = true;
            this.lblNivel.Location = new System.Drawing.Point(24, 203);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(43, 13);
            this.lblNivel.TabIndex = 16;
            this.lblNivel.Text = "Nivel: 0";
            // 
            // frmTestVuMeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 427);
            this.Controls.Add(this.lblNivel);
            this.Controls.Add(this.vuMeter3);
            this.Controls.Add(this.vuMeter13);
            this.Controls.Add(this.vuMeter12);
            this.Controls.Add(this.vuMeter11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.vuMeter10);
            this.Controls.Add(this.vuMeter9);
            this.Controls.Add(this.vuMeter8);
            this.Controls.Add(this.vuMeter7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vuMeter6);
            this.Controls.Add(this.vuMeter5);
            this.Controls.Add(this.vuMeter4);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.vuMeter2);
            this.Controls.Add(this.vuMeter1);
            this.Name = "frmTestVuMeter";
            this.Text = "LED Level Meter";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DBVuMeter vuMeter1;
        private DBVuMeter vuMeter2;
        private DBVuMeter vuMeter3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar1;
        private DBVuMeter vuMeter4;
        private DBVuMeter vuMeter5;
        private DBVuMeter vuMeter6;
        private System.Windows.Forms.Label label1;
        private DBVuMeter vuMeter7;
        private DBVuMeter vuMeter8;
        private DBVuMeter vuMeter9;
        private DBVuMeter vuMeter10;
        private System.Windows.Forms.Label label2;
        private DBVuMeter vuMeter11;
        private DBVuMeter vuMeter12;
        private DBVuMeter vuMeter13;
        private System.Windows.Forms.Label lblNivel;
    }
}

