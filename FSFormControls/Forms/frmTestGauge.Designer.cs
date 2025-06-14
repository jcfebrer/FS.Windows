namespace FSFormControls
{
    partial class frmTestGauge
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.process1 = new System.Diagnostics.Process();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gauge5 = new FSFormControls.DBGauge();
            this.gauge3 = new FSFormControls.DBGauge();
            this.gauge12 = new FSFormControls.DBGauge();
            this.gauge7 = new FSFormControls.DBGauge();
            this.gauge8 = new FSFormControls.DBGauge();
            this.gauge11 = new FSFormControls.DBGauge();
            this.gauge10 = new FSFormControls.DBGauge();
            this.gauge9 = new FSFormControls.DBGauge();
            this.gauge6 = new FSFormControls.DBGauge();
            this.gauge2 = new FSFormControls.DBGauge();
            this.gauge1 = new FSFormControls.DBGauge();
            this.gauge4 = new FSFormControls.DBGauge();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Lime;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 29);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 44);
            this.trackBar1.Maximum = 400;
            this.trackBar1.Minimum = -100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 510);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickFrequency = 100;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 25);
            this.button1.TabIndex = 9;
            this.button1.Text = "change needle types";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(142, 163);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(42, 20);
            this.textBox1.TabIndex = 17;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(318, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 36);
            this.label3.TabIndex = 19;
            this.label3.Text = "Volt";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(302, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 86);
            this.label1.TabIndex = 20;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gauge5
            // 
            this.gauge5.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge5.BaseArcRadius = 40;
            this.gauge5.BaseArcStart = 0;
            this.gauge5.BaseArcSweep = -90;
            this.gauge5.BaseArcWidth = 2;
            this.gauge5.Cap_Idx = ((byte)(1));
            this.gauge5.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge5.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge5.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge5.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge5.CapText = "";
            this.gauge5.Center = new System.Drawing.Point(30, 70);
            this.gauge5.Location = new System.Drawing.Point(695, 411);
            this.gauge5.MaxValue = 50F;
            this.gauge5.MinValue = 0F;
            this.gauge5.Name = "gauge5";
            this.gauge5.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Violet;
            this.gauge5.NeedleColor2 = System.Drawing.Color.Black;
            this.gauge5.NeedleRadius = 40;
            this.gauge5.NeedleType = 0;
            this.gauge5.NeedleWidth = 4;
            this.gauge5.Range_Idx = ((byte)(1));
            this.gauge5.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.gauge5.RangeEnabled = false;
            this.gauge5.RangeEndValue = 400F;
            this.gauge5.RangeInnerRadius = 10;
            this.gauge5.RangeOuterRadius = 40;
            this.gauge5.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge5.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge5.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge5.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge5.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge5.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge5.RangeStartValue = 300F;
            this.gauge5.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gauge5.ScaleLinesInterInnerRadius = 42;
            this.gauge5.ScaleLinesInterOuterRadius = 50;
            this.gauge5.ScaleLinesInterWidth = 1;
            this.gauge5.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gauge5.ScaleLinesMajorInnerRadius = 40;
            this.gauge5.ScaleLinesMajorOuterRadius = 50;
            this.gauge5.ScaleLinesMajorStepValue = 10F;
            this.gauge5.ScaleLinesMajorWidth = 2;
            this.gauge5.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gauge5.ScaleLinesMinorInnerRadius = 43;
            this.gauge5.ScaleLinesMinorNumOf = 1;
            this.gauge5.ScaleLinesMinorOuterRadius = 50;
            this.gauge5.ScaleLinesMinorWidth = 1;
            this.gauge5.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gauge5.ScaleNumbersFormat = null;
            this.gauge5.ScaleNumbersRadius = 62;
            this.gauge5.ScaleNumbersRotation = 90;
            this.gauge5.ScaleNumbersStartScaleLine = 1;
            this.gauge5.ScaleNumbersStepScaleLines = 2;
            this.gauge5.Size = new System.Drawing.Size(106, 95);
            this.gauge5.TabIndex = 8;
            this.gauge5.Text = "FSFormControls.DBGauge5";
            this.gauge5.Value = 0F;
            // 
            // gauge3
            // 
            this.gauge3.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge3.BaseArcRadius = 40;
            this.gauge3.BaseArcStart = 180;
            this.gauge3.BaseArcSweep = 180;
            this.gauge3.BaseArcWidth = 2;
            this.gauge3.Cap_Idx = ((byte)(1));
            this.gauge3.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge3.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge3.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge3.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge3.CapText = "";
            this.gauge3.Center = new System.Drawing.Point(70, 70);
            this.gauge3.Location = new System.Drawing.Point(63, 401);
            this.gauge3.MaxValue = 50F;
            this.gauge3.MinValue = 0F;
            this.gauge3.Name = "gauge3";
            this.gauge3.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Green;
            this.gauge3.NeedleColor2 = System.Drawing.Color.White;
            this.gauge3.NeedleRadius = 40;
            this.gauge3.NeedleType = 0;
            this.gauge3.NeedleWidth = 3;
            this.gauge3.Range_Idx = ((byte)(1));
            this.gauge3.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.gauge3.RangeEnabled = false;
            this.gauge3.RangeEndValue = 400F;
            this.gauge3.RangeInnerRadius = 10;
            this.gauge3.RangeOuterRadius = 40;
            this.gauge3.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge3.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge3.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge3.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge3.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge3.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge3.RangeStartValue = 300F;
            this.gauge3.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gauge3.ScaleLinesInterInnerRadius = 42;
            this.gauge3.ScaleLinesInterOuterRadius = 50;
            this.gauge3.ScaleLinesInterWidth = 1;
            this.gauge3.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gauge3.ScaleLinesMajorInnerRadius = 40;
            this.gauge3.ScaleLinesMajorOuterRadius = 50;
            this.gauge3.ScaleLinesMajorStepValue = 10F;
            this.gauge3.ScaleLinesMajorWidth = 2;
            this.gauge3.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gauge3.ScaleLinesMinorInnerRadius = 43;
            this.gauge3.ScaleLinesMinorNumOf = 1;
            this.gauge3.ScaleLinesMinorOuterRadius = 50;
            this.gauge3.ScaleLinesMinorWidth = 1;
            this.gauge3.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gauge3.ScaleNumbersFormat = null;
            this.gauge3.ScaleNumbersRadius = 62;
            this.gauge3.ScaleNumbersRotation = 90;
            this.gauge3.ScaleNumbersStartScaleLine = 1;
            this.gauge3.ScaleNumbersStepScaleLines = 2;
            this.gauge3.Size = new System.Drawing.Size(143, 95);
            this.gauge3.TabIndex = 6;
            this.gauge3.Text = "FSFormControls.DBGauge3";
            this.gauge3.Value = 0F;
            // 
            // gauge12
            // 
            this.gauge12.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge12.BaseArcRadius = 40;
            this.gauge12.BaseArcStart = 180;
            this.gauge12.BaseArcSweep = 330;
            this.gauge12.BaseArcWidth = 2;
            this.gauge12.Cap_Idx = ((byte)(1));
            this.gauge12.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge12.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge12.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge12.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge12.CapText = "";
            this.gauge12.Center = new System.Drawing.Point(70, 70);
            this.gauge12.Location = new System.Drawing.Point(611, 266);
            this.gauge12.MaxValue = 100F;
            this.gauge12.MinValue = 0F;
            this.gauge12.Name = "gauge12";
            this.gauge12.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Gray;
            this.gauge12.NeedleColor2 = System.Drawing.Color.Black;
            this.gauge12.NeedleRadius = 45;
            this.gauge12.NeedleType = 0;
            this.gauge12.NeedleWidth = 3;
            this.gauge12.Range_Idx = ((byte)(1));
            this.gauge12.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.gauge12.RangeEnabled = false;
            this.gauge12.RangeEndValue = 400F;
            this.gauge12.RangeInnerRadius = 10;
            this.gauge12.RangeOuterRadius = 40;
            this.gauge12.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge12.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge12.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge12.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge12.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge12.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge12.RangeStartValue = 300F;
            this.gauge12.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gauge12.ScaleLinesInterInnerRadius = 42;
            this.gauge12.ScaleLinesInterOuterRadius = 50;
            this.gauge12.ScaleLinesInterWidth = 1;
            this.gauge12.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gauge12.ScaleLinesMajorInnerRadius = 40;
            this.gauge12.ScaleLinesMajorOuterRadius = 50;
            this.gauge12.ScaleLinesMajorStepValue = 10F;
            this.gauge12.ScaleLinesMajorWidth = 2;
            this.gauge12.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gauge12.ScaleLinesMinorInnerRadius = 43;
            this.gauge12.ScaleLinesMinorNumOf = 1;
            this.gauge12.ScaleLinesMinorOuterRadius = 50;
            this.gauge12.ScaleLinesMinorWidth = 1;
            this.gauge12.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gauge12.ScaleNumbersFormat = null;
            this.gauge12.ScaleNumbersRadius = 62;
            this.gauge12.ScaleNumbersRotation = 0;
            this.gauge12.ScaleNumbersStartScaleLine = 1;
            this.gauge12.ScaleNumbersStepScaleLines = 2;
            this.gauge12.Size = new System.Drawing.Size(150, 142);
            this.gauge12.TabIndex = 18;
            this.gauge12.Text = "FSFormControls.DBGauge12";
            this.gauge12.Value = 0F;
            // 
            // gauge7
            // 
            this.gauge7.BackColor = System.Drawing.SystemColors.Control;
            this.gauge7.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge7.BaseArcRadius = 40;
            this.gauge7.BaseArcStart = 180;
            this.gauge7.BaseArcSweep = 180;
            this.gauge7.BaseArcWidth = 2;
            this.gauge7.Cap_Idx = ((byte)(1));
            this.gauge7.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge7.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge7.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge7.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge7.CapText = "";
            this.gauge7.Center = new System.Drawing.Point(70, 70);
            this.gauge7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gauge7.Location = new System.Drawing.Point(547, 411);
            this.gauge7.MaxValue = 50F;
            this.gauge7.MinValue = 0F;
            this.gauge7.Name = "gauge7";
            this.gauge7.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Green;
            this.gauge7.NeedleColor2 = System.Drawing.Color.GreenYellow;
            this.gauge7.NeedleRadius = 40;
            this.gauge7.NeedleType = 0;
            this.gauge7.NeedleWidth = 2;
            this.gauge7.Range_Idx = ((byte)(0));
            this.gauge7.RangeColor = System.Drawing.Color.LightGreen;
            this.gauge7.RangeEnabled = false;
            this.gauge7.RangeEndValue = 300F;
            this.gauge7.RangeInnerRadius = 70;
            this.gauge7.RangeOuterRadius = 80;
            this.gauge7.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge7.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge7.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge7.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge7.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge7.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge7.RangeStartValue = -100F;
            this.gauge7.ScaleLinesInterColor = System.Drawing.Color.Red;
            this.gauge7.ScaleLinesInterInnerRadius = 42;
            this.gauge7.ScaleLinesInterOuterRadius = 50;
            this.gauge7.ScaleLinesInterWidth = 1;
            this.gauge7.ScaleLinesMajorColor = System.Drawing.Color.Red;
            this.gauge7.ScaleLinesMajorInnerRadius = 40;
            this.gauge7.ScaleLinesMajorOuterRadius = 50;
            this.gauge7.ScaleLinesMajorStepValue = 10F;
            this.gauge7.ScaleLinesMajorWidth = 2;
            this.gauge7.ScaleLinesMinorColor = System.Drawing.Color.DarkRed;
            this.gauge7.ScaleLinesMinorInnerRadius = 43;
            this.gauge7.ScaleLinesMinorNumOf = 1;
            this.gauge7.ScaleLinesMinorOuterRadius = 50;
            this.gauge7.ScaleLinesMinorWidth = 1;
            this.gauge7.ScaleNumbersColor = System.Drawing.Color.Red;
            this.gauge7.ScaleNumbersFormat = null;
            this.gauge7.ScaleNumbersRadius = 62;
            this.gauge7.ScaleNumbersRotation = 90;
            this.gauge7.ScaleNumbersStartScaleLine = 1;
            this.gauge7.ScaleNumbersStepScaleLines = 2;
            this.gauge7.Size = new System.Drawing.Size(142, 85);
            this.gauge7.TabIndex = 11;
            this.gauge7.Text = "FSFormControls.DBGauge7";
            this.gauge7.Value = 0F;
            // 
            // gauge8
            // 
            this.gauge8.BackColor = System.Drawing.SystemColors.Control;
            this.gauge8.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge8.BaseArcRadius = 40;
            this.gauge8.BaseArcStart = 180;
            this.gauge8.BaseArcSweep = 90;
            this.gauge8.BaseArcWidth = 2;
            this.gauge8.Cap_Idx = ((byte)(1));
            this.gauge8.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge8.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge8.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge8.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge8.CapText = "";
            this.gauge8.Center = new System.Drawing.Point(70, 70);
            this.gauge8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gauge8.Location = new System.Drawing.Point(511, 163);
            this.gauge8.MaxValue = 50F;
            this.gauge8.MinValue = 0F;
            this.gauge8.Name = "gauge8";
            this.gauge8.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Blue;
            this.gauge8.NeedleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gauge8.NeedleRadius = 40;
            this.gauge8.NeedleType = 0;
            this.gauge8.NeedleWidth = 3;
            this.gauge8.Range_Idx = ((byte)(0));
            this.gauge8.RangeColor = System.Drawing.Color.LightGreen;
            this.gauge8.RangeEnabled = false;
            this.gauge8.RangeEndValue = 300F;
            this.gauge8.RangeInnerRadius = 70;
            this.gauge8.RangeOuterRadius = 80;
            this.gauge8.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge8.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge8.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge8.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge8.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge8.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge8.RangeStartValue = -100F;
            this.gauge8.ScaleLinesInterColor = System.Drawing.Color.RosyBrown;
            this.gauge8.ScaleLinesInterInnerRadius = 42;
            this.gauge8.ScaleLinesInterOuterRadius = 50;
            this.gauge8.ScaleLinesInterWidth = 1;
            this.gauge8.ScaleLinesMajorColor = System.Drawing.Color.RoyalBlue;
            this.gauge8.ScaleLinesMajorInnerRadius = 40;
            this.gauge8.ScaleLinesMajorOuterRadius = 50;
            this.gauge8.ScaleLinesMajorStepValue = 10F;
            this.gauge8.ScaleLinesMajorWidth = 2;
            this.gauge8.ScaleLinesMinorColor = System.Drawing.Color.DarkSalmon;
            this.gauge8.ScaleLinesMinorInnerRadius = 43;
            this.gauge8.ScaleLinesMinorNumOf = 1;
            this.gauge8.ScaleLinesMinorOuterRadius = 50;
            this.gauge8.ScaleLinesMinorWidth = 1;
            this.gauge8.ScaleNumbersColor = System.Drawing.Color.RoyalBlue;
            this.gauge8.ScaleNumbersFormat = null;
            this.gauge8.ScaleNumbersRadius = 62;
            this.gauge8.ScaleNumbersRotation = 90;
            this.gauge8.ScaleNumbersStartScaleLine = 1;
            this.gauge8.ScaleNumbersStepScaleLines = 2;
            this.gauge8.Size = new System.Drawing.Size(84, 85);
            this.gauge8.TabIndex = 16;
            this.gauge8.Text = "FSFormControls.DBGauge8";
            this.gauge8.Value = 22F;
            // 
            // gauge11
            // 
            this.gauge11.BackColor = System.Drawing.SystemColors.Control;
            this.gauge11.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge11.BaseArcRadius = 40;
            this.gauge11.BaseArcStart = -90;
            this.gauge11.BaseArcSweep = 360;
            this.gauge11.BaseArcWidth = 2;
            this.gauge11.Cap_Idx = ((byte)(1));
            this.gauge11.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge11.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge11.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge11.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge11.CapText = "";
            this.gauge11.Center = new System.Drawing.Point(70, 70);
            this.gauge11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gauge11.Location = new System.Drawing.Point(631, 9);
            this.gauge11.MaxValue = 10F;
            this.gauge11.MinValue = 0F;
            this.gauge11.Name = "gauge11";
            this.gauge11.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Green;
            this.gauge11.NeedleColor2 = System.Drawing.Color.Black;
            this.gauge11.NeedleRadius = 40;
            this.gauge11.NeedleType = 0;
            this.gauge11.NeedleWidth = 10;
            this.gauge11.Range_Idx = ((byte)(0));
            this.gauge11.RangeColor = System.Drawing.Color.LightGreen;
            this.gauge11.RangeEnabled = false;
            this.gauge11.RangeEndValue = 300F;
            this.gauge11.RangeInnerRadius = 70;
            this.gauge11.RangeOuterRadius = 80;
            this.gauge11.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge11.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge11.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge11.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge11.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge11.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge11.RangeStartValue = -100F;
            this.gauge11.ScaleLinesInterColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gauge11.ScaleLinesInterInnerRadius = 42;
            this.gauge11.ScaleLinesInterOuterRadius = 50;
            this.gauge11.ScaleLinesInterWidth = 1;
            this.gauge11.ScaleLinesMajorColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.gauge11.ScaleLinesMajorInnerRadius = 40;
            this.gauge11.ScaleLinesMajorOuterRadius = 50;
            this.gauge11.ScaleLinesMajorStepValue = 1F;
            this.gauge11.ScaleLinesMajorWidth = 2;
            this.gauge11.ScaleLinesMinorColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gauge11.ScaleLinesMinorInnerRadius = 43;
            this.gauge11.ScaleLinesMinorNumOf = 1;
            this.gauge11.ScaleLinesMinorOuterRadius = 50;
            this.gauge11.ScaleLinesMinorWidth = 1;
            this.gauge11.ScaleNumbersColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.gauge11.ScaleNumbersFormat = null;
            this.gauge11.ScaleNumbersRadius = 62;
            this.gauge11.ScaleNumbersRotation = 0;
            this.gauge11.ScaleNumbersStartScaleLine = 2;
            this.gauge11.ScaleNumbersStepScaleLines = 2;
            this.gauge11.Size = new System.Drawing.Size(149, 148);
            this.gauge11.TabIndex = 15;
            this.gauge11.Text = "FSFormControls.DBGauge11";
            this.gauge11.Value = 0F;
            // 
            // gauge10
            // 
            this.gauge10.BackColor = System.Drawing.SystemColors.Control;
            this.gauge10.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge10.BaseArcRadius = 40;
            this.gauge10.BaseArcStart = 0;
            this.gauge10.BaseArcSweep = -90;
            this.gauge10.BaseArcWidth = 2;
            this.gauge10.Cap_Idx = ((byte)(1));
            this.gauge10.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge10.CapPosition = new System.Drawing.Point(40, 75);
            this.gauge10.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(40, 75),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge10.CapsText = new string[] {
        "",
        "% Fuel",
        "",
        "",
        ""};
            this.gauge10.CapText = "% Fuel";
            this.gauge10.Center = new System.Drawing.Point(15, 70);
            this.gauge10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gauge10.Location = new System.Drawing.Point(695, 167);
            this.gauge10.MaxValue = 100F;
            this.gauge10.MinValue = 0F;
            this.gauge10.Name = "gauge10";
            this.gauge10.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Red;
            this.gauge10.NeedleColor2 = System.Drawing.Color.Black;
            this.gauge10.NeedleRadius = 40;
            this.gauge10.NeedleType = 0;
            this.gauge10.NeedleWidth = 2;
            this.gauge10.Range_Idx = ((byte)(0));
            this.gauge10.RangeColor = System.Drawing.Color.LightGreen;
            this.gauge10.RangeEnabled = false;
            this.gauge10.RangeEndValue = 300F;
            this.gauge10.RangeInnerRadius = 70;
            this.gauge10.RangeOuterRadius = 80;
            this.gauge10.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge10.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge10.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge10.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge10.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge10.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge10.RangeStartValue = -100F;
            this.gauge10.ScaleLinesInterColor = System.Drawing.Color.DarkRed;
            this.gauge10.ScaleLinesInterInnerRadius = 45;
            this.gauge10.ScaleLinesInterOuterRadius = 50;
            this.gauge10.ScaleLinesInterWidth = 2;
            this.gauge10.ScaleLinesMajorColor = System.Drawing.Color.SaddleBrown;
            this.gauge10.ScaleLinesMajorInnerRadius = 40;
            this.gauge10.ScaleLinesMajorOuterRadius = 50;
            this.gauge10.ScaleLinesMajorStepValue = 50F;
            this.gauge10.ScaleLinesMajorWidth = 2;
            this.gauge10.ScaleLinesMinorColor = System.Drawing.Color.DarkRed;
            this.gauge10.ScaleLinesMinorInnerRadius = 45;
            this.gauge10.ScaleLinesMinorNumOf = 9;
            this.gauge10.ScaleLinesMinorOuterRadius = 50;
            this.gauge10.ScaleLinesMinorWidth = 2;
            this.gauge10.ScaleNumbersColor = System.Drawing.Color.Maroon;
            this.gauge10.ScaleNumbersFormat = null;
            this.gauge10.ScaleNumbersRadius = 62;
            this.gauge10.ScaleNumbersRotation = 90;
            this.gauge10.ScaleNumbersStartScaleLine = 1;
            this.gauge10.ScaleNumbersStepScaleLines = 2;
            this.gauge10.Size = new System.Drawing.Size(85, 93);
            this.gauge10.TabIndex = 14;
            this.gauge10.Text = "FSFormControls.DBGauge10";
            this.gauge10.Value = 0F;
            // 
            // gauge9
            // 
            this.gauge9.BackColor = System.Drawing.SystemColors.Control;
            this.gauge9.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge9.BaseArcRadius = 40;
            this.gauge9.BaseArcStart = 180;
            this.gauge9.BaseArcSweep = 90;
            this.gauge9.BaseArcWidth = 2;
            this.gauge9.Cap_Idx = ((byte)(1));
            this.gauge9.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge9.CapPosition = new System.Drawing.Point(40, 75);
            this.gauge9.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(40, 75),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge9.CapsText = new string[] {
        "",
        "% Fuel",
        "",
        "",
        ""};
            this.gauge9.CapText = "% Fuel";
            this.gauge9.Center = new System.Drawing.Point(70, 70);
            this.gauge9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gauge9.Location = new System.Drawing.Point(611, 167);
            this.gauge9.MaxValue = 100F;
            this.gauge9.MinValue = 0F;
            this.gauge9.Name = "gauge9";
            this.gauge9.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Gray;
            this.gauge9.NeedleColor2 = System.Drawing.Color.Black;
            this.gauge9.NeedleRadius = 40;
            this.gauge9.NeedleType = 0;
            this.gauge9.NeedleWidth = 2;
            this.gauge9.Range_Idx = ((byte)(0));
            this.gauge9.RangeColor = System.Drawing.Color.LightGreen;
            this.gauge9.RangeEnabled = false;
            this.gauge9.RangeEndValue = 300F;
            this.gauge9.RangeInnerRadius = 70;
            this.gauge9.RangeOuterRadius = 80;
            this.gauge9.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge9.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge9.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge9.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge9.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge9.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge9.RangeStartValue = -100F;
            this.gauge9.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gauge9.ScaleLinesInterInnerRadius = 42;
            this.gauge9.ScaleLinesInterOuterRadius = 50;
            this.gauge9.ScaleLinesInterWidth = 2;
            this.gauge9.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gauge9.ScaleLinesMajorInnerRadius = 40;
            this.gauge9.ScaleLinesMajorOuterRadius = 50;
            this.gauge9.ScaleLinesMajorStepValue = 50F;
            this.gauge9.ScaleLinesMajorWidth = 2;
            this.gauge9.ScaleLinesMinorColor = System.Drawing.Color.Black;
            this.gauge9.ScaleLinesMinorInnerRadius = 43;
            this.gauge9.ScaleLinesMinorNumOf = 9;
            this.gauge9.ScaleLinesMinorOuterRadius = 50;
            this.gauge9.ScaleLinesMinorWidth = 2;
            this.gauge9.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gauge9.ScaleNumbersFormat = null;
            this.gauge9.ScaleNumbersRadius = 62;
            this.gauge9.ScaleNumbersRotation = 90;
            this.gauge9.ScaleNumbersStartScaleLine = 1;
            this.gauge9.ScaleNumbersStepScaleLines = 2;
            this.gauge9.Size = new System.Drawing.Size(91, 108);
            this.gauge9.TabIndex = 13;
            this.gauge9.Text = "FSFormControls.DBGauge9";
            this.gauge9.Value = 0F;
            // 
            // gauge6
            // 
            this.gauge6.BackColor = System.Drawing.SystemColors.Control;
            this.gauge6.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge6.BaseArcRadius = 500;
            this.gauge6.BaseArcStart = 180;
            this.gauge6.BaseArcSweep = 90;
            this.gauge6.BaseArcWidth = 0;
            this.gauge6.Cap_Idx = ((byte)(1));
            this.gauge6.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge6.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge6.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge6.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge6.CapText = "";
            this.gauge6.Center = new System.Drawing.Point(70, 70);
            this.gauge6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gauge6.Location = new System.Drawing.Point(82, 266);
            this.gauge6.MaxValue = 50F;
            this.gauge6.MinValue = 0F;
            this.gauge6.Name = "gauge6";
            this.gauge6.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Blue;
            this.gauge6.NeedleColor2 = System.Drawing.Color.RoyalBlue;
            this.gauge6.NeedleRadius = 48;
            this.gauge6.NeedleType = 1;
            this.gauge6.NeedleWidth = 3;
            this.gauge6.Range_Idx = ((byte)(0));
            this.gauge6.RangeColor = System.Drawing.Color.LightGreen;
            this.gauge6.RangeEnabled = false;
            this.gauge6.RangeEndValue = 300F;
            this.gauge6.RangeInnerRadius = 70;
            this.gauge6.RangeOuterRadius = 80;
            this.gauge6.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge6.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge6.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge6.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge6.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge6.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge6.RangeStartValue = -100F;
            this.gauge6.ScaleLinesInterColor = System.Drawing.Color.RosyBrown;
            this.gauge6.ScaleLinesInterInnerRadius = 42;
            this.gauge6.ScaleLinesInterOuterRadius = 55;
            this.gauge6.ScaleLinesInterWidth = 1;
            this.gauge6.ScaleLinesMajorColor = System.Drawing.Color.Gray;
            this.gauge6.ScaleLinesMajorInnerRadius = 40;
            this.gauge6.ScaleLinesMajorOuterRadius = 55;
            this.gauge6.ScaleLinesMajorStepValue = 10F;
            this.gauge6.ScaleLinesMajorWidth = 2;
            this.gauge6.ScaleLinesMinorColor = System.Drawing.Color.DarkSalmon;
            this.gauge6.ScaleLinesMinorInnerRadius = 43;
            this.gauge6.ScaleLinesMinorNumOf = 1;
            this.gauge6.ScaleLinesMinorOuterRadius = 50;
            this.gauge6.ScaleLinesMinorWidth = 1;
            this.gauge6.ScaleNumbersColor = System.Drawing.Color.RoyalBlue;
            this.gauge6.ScaleNumbersFormat = null;
            this.gauge6.ScaleNumbersRadius = 62;
            this.gauge6.ScaleNumbersRotation = 90;
            this.gauge6.ScaleNumbersStartScaleLine = 1;
            this.gauge6.ScaleNumbersStepScaleLines = 2;
            this.gauge6.Size = new System.Drawing.Size(84, 85);
            this.gauge6.TabIndex = 12;
            this.gauge6.Text = "FSFormControls.DBGauge6";
            this.gauge6.Value = 22F;
            // 
            // gauge2
            // 
            this.gauge2.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge2.BaseArcRadius = 130;
            this.gauge2.BaseArcStart = 135;
            this.gauge2.BaseArcSweep = 270;
            this.gauge2.BaseArcWidth = 2;
            this.gauge2.Cap_Idx = ((byte)(2));
            this.gauge2.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge2.CapPosition = new System.Drawing.Point(102, 200);
            this.gauge2.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(142, 100),
        new System.Drawing.Point(102, 200),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge2.CapsText = new string[] {
        "",
        "Speed",
        "x 1000000 [m/s]",
        "",
        ""};
            this.gauge2.CapText = "x 1000000 [m/s]";
            this.gauge2.Center = new System.Drawing.Point(170, 170);
            this.gauge2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gauge2.Location = new System.Drawing.Point(199, 187);
            this.gauge2.MaxValue = 400F;
            this.gauge2.MinValue = -100F;
            this.gauge2.Name = "gauge2";
            this.gauge2.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Yellow;
            this.gauge2.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gauge2.NeedleRadius = 130;
            this.gauge2.NeedleType = 0;
            this.gauge2.NeedleWidth = 5;
            this.gauge2.Range_Idx = ((byte)(1));
            this.gauge2.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.gauge2.RangeEnabled = true;
            this.gauge2.RangeEndValue = 290F;
            this.gauge2.RangeInnerRadius = 100;
            this.gauge2.RangeOuterRadius = 130;
            this.gauge2.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge2.RangesEnabled = new bool[] {
        true,
        true,
        true,
        false,
        false};
            this.gauge2.RangesEndValue = new float[] {
        100F,
        290F,
        400F,
        0F,
        0F};
            this.gauge2.RangesInnerRadius = new int[] {
        100,
        100,
        100,
        70,
        70};
            this.gauge2.RangesOuterRadius = new int[] {
        130,
        130,
        130,
        80,
        80};
            this.gauge2.RangesStartValue = new float[] {
        -100F,
        100F,
        290F,
        0F,
        0F};
            this.gauge2.RangeStartValue = 100F;
            this.gauge2.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gauge2.ScaleLinesInterInnerRadius = 130;
            this.gauge2.ScaleLinesInterOuterRadius = 140;
            this.gauge2.ScaleLinesInterWidth = 2;
            this.gauge2.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gauge2.ScaleLinesMajorInnerRadius = 130;
            this.gauge2.ScaleLinesMajorOuterRadius = 142;
            this.gauge2.ScaleLinesMajorStepValue = 50F;
            this.gauge2.ScaleLinesMajorWidth = 3;
            this.gauge2.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gauge2.ScaleLinesMinorInnerRadius = 130;
            this.gauge2.ScaleLinesMinorNumOf = 9;
            this.gauge2.ScaleLinesMinorOuterRadius = 140;
            this.gauge2.ScaleLinesMinorWidth = 1;
            this.gauge2.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gauge2.ScaleNumbersFormat = null;
            this.gauge2.ScaleNumbersRadius = 160;
            this.gauge2.ScaleNumbersRotation = 0;
            this.gauge2.ScaleNumbersStartScaleLine = 0;
            this.gauge2.ScaleNumbersStepScaleLines = 1;
            this.gauge2.Size = new System.Drawing.Size(361, 299);
            this.gauge2.TabIndex = 3;
            this.gauge2.Text = "FSFormControls.DBGauge2";
            this.gauge2.Value = 0F;
            this.gauge2.ValueInRangeChanged += new FSFormControls.DBGauge.ValueInRangeChangedDelegate(this.gauge2_ValueInRangeChanged);
            // 
            // gauge1
            // 
            this.gauge1.BackColor = System.Drawing.SystemColors.Control;
            this.gauge1.BaseArcColor = System.Drawing.Color.LightSlateGray;
            this.gauge1.BaseArcRadius = 80;
            this.gauge1.BaseArcStart = 135;
            this.gauge1.BaseArcSweep = 270;
            this.gauge1.BaseArcWidth = 1;
            this.gauge1.Cap_Idx = ((byte)(1));
            this.gauge1.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge1.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge1.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge1.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge1.CapText = "";
            this.gauge1.Center = new System.Drawing.Point(100, 105);
            this.gauge1.Location = new System.Drawing.Point(63, 21);
            this.gauge1.MaxValue = 400F;
            this.gauge1.MinValue = -100F;
            this.gauge1.Name = "gauge1";
            this.gauge1.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Gray;
            this.gauge1.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gauge1.NeedleRadius = 80;
            this.gauge1.NeedleType = 0;
            this.gauge1.NeedleWidth = 2;
            this.gauge1.Range_Idx = ((byte)(1));
            this.gauge1.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.gauge1.RangeEnabled = true;
            this.gauge1.RangeEndValue = 400F;
            this.gauge1.RangeInnerRadius = 1;
            this.gauge1.RangeOuterRadius = 75;
            this.gauge1.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge1.RangesEnabled = new bool[] {
        true,
        true,
        false,
        false,
        false};
            this.gauge1.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge1.RangesInnerRadius = new int[] {
        70,
        1,
        70,
        70,
        70};
            this.gauge1.RangesOuterRadius = new int[] {
        75,
        75,
        80,
        80,
        80};
            this.gauge1.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge1.RangeStartValue = 300F;
            this.gauge1.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gauge1.ScaleLinesInterInnerRadius = 76;
            this.gauge1.ScaleLinesInterOuterRadius = 80;
            this.gauge1.ScaleLinesInterWidth = 1;
            this.gauge1.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gauge1.ScaleLinesMajorInnerRadius = 70;
            this.gauge1.ScaleLinesMajorOuterRadius = 80;
            this.gauge1.ScaleLinesMajorStepValue = 50F;
            this.gauge1.ScaleLinesMajorWidth = 2;
            this.gauge1.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gauge1.ScaleLinesMinorInnerRadius = 76;
            this.gauge1.ScaleLinesMinorNumOf = 9;
            this.gauge1.ScaleLinesMinorOuterRadius = 80;
            this.gauge1.ScaleLinesMinorWidth = 1;
            this.gauge1.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gauge1.ScaleNumbersFormat = null;
            this.gauge1.ScaleNumbersRadius = 95;
            this.gauge1.ScaleNumbersRotation = 0;
            this.gauge1.ScaleNumbersStartScaleLine = 0;
            this.gauge1.ScaleNumbersStepScaleLines = 1;
            this.gauge1.Size = new System.Drawing.Size(209, 186);
            this.gauge1.TabIndex = 0;
            this.gauge1.Text = "FSFormControls.DBGauge1";
            this.gauge1.Value = 50F;
            this.gauge1.ValueInRangeChanged += new FSFormControls.DBGauge.ValueInRangeChangedDelegate(this.gauge1_ValueInRangeChanged);
            // 
            // gauge4
            // 
            this.gauge4.BaseArcColor = System.Drawing.Color.Gray;
            this.gauge4.BaseArcRadius = 150;
            this.gauge4.BaseArcStart = 215;
            this.gauge4.BaseArcSweep = 110;
            this.gauge4.BaseArcWidth = 2;
            this.gauge4.Cap_Idx = ((byte)(1));
            this.gauge4.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gauge4.CapPosition = new System.Drawing.Point(10, 10);
            this.gauge4.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gauge4.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gauge4.CapText = "";
            this.gauge4.Center = new System.Drawing.Point(150, 180);
            this.gauge4.Location = new System.Drawing.Point(315, 3);
            this.gauge4.MaxValue = 300F;
            this.gauge4.MinValue = -300F;
            this.gauge4.Name = "gauge4";
            this.gauge4.NeedleColor1 = FSFormControls.DBGauge.NeedleColorEnum.Green;
            this.gauge4.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gauge4.NeedleRadius = 150;
            this.gauge4.NeedleType = 0;
            this.gauge4.NeedleWidth = 2;
            this.gauge4.Range_Idx = ((byte)(1));
            this.gauge4.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.gauge4.RangeEnabled = false;
            this.gauge4.RangeEndValue = 400F;
            this.gauge4.RangeInnerRadius = 10;
            this.gauge4.RangeOuterRadius = 40;
            this.gauge4.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gauge4.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.gauge4.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.gauge4.RangesInnerRadius = new int[] {
        70,
        10,
        70,
        70,
        70};
            this.gauge4.RangesOuterRadius = new int[] {
        80,
        40,
        80,
        80,
        80};
            this.gauge4.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.gauge4.RangeStartValue = 300F;
            this.gauge4.ScaleLinesInterColor = System.Drawing.Color.Red;
            this.gauge4.ScaleLinesInterInnerRadius = 145;
            this.gauge4.ScaleLinesInterOuterRadius = 150;
            this.gauge4.ScaleLinesInterWidth = 2;
            this.gauge4.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gauge4.ScaleLinesMajorInnerRadius = 140;
            this.gauge4.ScaleLinesMajorOuterRadius = 150;
            this.gauge4.ScaleLinesMajorStepValue = 100F;
            this.gauge4.ScaleLinesMajorWidth = 2;
            this.gauge4.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gauge4.ScaleLinesMinorInnerRadius = 145;
            this.gauge4.ScaleLinesMinorNumOf = 9;
            this.gauge4.ScaleLinesMinorOuterRadius = 150;
            this.gauge4.ScaleLinesMinorWidth = 1;
            this.gauge4.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gauge4.ScaleNumbersFormat = null;
            this.gauge4.ScaleNumbersRadius = 162;
            this.gauge4.ScaleNumbersRotation = 90;
            this.gauge4.ScaleNumbersStartScaleLine = 1;
            this.gauge4.ScaleNumbersStepScaleLines = 2;
            this.gauge4.Size = new System.Drawing.Size(297, 121);
            this.gauge4.TabIndex = 7;
            this.gauge4.Text = "FSFormControls.DBGauge4";
            this.gauge4.Value = 0F;
            // 
            // frmTestGauge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gauge5);
            this.Controls.Add(this.gauge3);
            this.Controls.Add(this.gauge12);
            this.Controls.Add(this.gauge7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gauge8);
            this.Controls.Add(this.gauge11);
            this.Controls.Add(this.gauge10);
            this.Controls.Add(this.gauge9);
            this.Controls.Add(this.gauge6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gauge2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gauge1);
            this.Controls.Add(this.gauge4);
            this.Name = "frmTestGauge";
            this.Text = "FSFormControls.DBGauge Test Window";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FSFormControls.DBGauge gauge1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private FSFormControls.DBGauge gauge2;
        private System.Windows.Forms.Timer timer1;
        private FSFormControls.DBGauge gauge3;
        private FSFormControls.DBGauge gauge4;
        private FSFormControls.DBGauge gauge5;
        private System.Windows.Forms.Button button1;
        private FSFormControls.DBGauge gauge6;
        private FSFormControls.DBGauge gauge7;
        private FSFormControls.DBGauge gauge9;
        private FSFormControls.DBGauge gauge10;
        private FSFormControls.DBGauge gauge11;
        private FSFormControls.DBGauge gauge8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Diagnostics.Process process1;
        private FSFormControls.DBGauge gauge12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}

