#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBLabel.bmp")]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    public class DBLabel : DBUserControl
    {
        private long m_angle;
        private bool m_drawGradient;

        private bool m_drawShadow;
        private Color m_endColor = Color.LightSkyBlue;
        private Global.AccessMode m_Mode;
        private Color m_shadowColor = Color.Black;
        private Color m_startColor = Color.White;
        private ContentAlignment m_TextAlign = ContentAlignment.TopLeft;
        private long m_xOffset = 1;
        private Label label;
        private long m_yOffset = 1;

        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;
                if (m_Mode == Global.AccessMode.WriteMode)
                    label.Enabled = true;
                else
                    label.Enabled = true;
            }
        }

        public int Decimals { get; set; } = 2;

        public BorderStyle BorderStyleInner { get; set; }

        public BorderStyle BorderStyleOuter { get; set; }

        public DBAppearance Appearance { get; set; }

        [Description("Indicamos la forma de representarse una fecha.")]
        public string DateFormat { get; set; } = "dd/MM/yyyy";

        [Description("Tipo de datos a introducir en el control. Texto, Numérico, Fecha, Porcentaje, ...")]
        public DBTextBox.TypeData DataType { get; set; } = DBTextBox.TypeData.All;

        [Description(
            "Indicamos el modo en que debe presentar la cadena. May?sculas / Min?sculas o inicial en may?scula resto min?scula."
        )]
        public DBTextBox.TypeString Capitalize { get; set; } = DBTextBox.TypeString.Normal;


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }


        private string m_DBField;
        [Description("Campo de la base de datos a enlazar.")]
        public string DBField
        {
            get { return m_DBField; }
            set { m_DBField = value; }
        }

        public override bool AutoSize
        {
            get { return label.AutoSize; }
            set { label.AutoSize = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public ContentAlignment TextAlign
        {
            get { return m_TextAlign; }
            set
            {
                m_TextAlign = value;
                label.TextAlign = m_TextAlign;
            }
        }

        public new BorderStyle BorderStyle
        {
            get { return label.BorderStyle; }
            set { label.BorderStyle = value; }
        }


        [Category("Gradient")]
        [Description("Set to true to draw the gradient background")]
        [DefaultValue(false)]
        public bool DrawGradient
        {
            get { return m_drawGradient; }
            set
            {
                m_drawGradient = value;
                Invalidate();
            }
        }

        [Category("Gradient")]
        [Description("The start color of the gradient")]
        [DefaultValue(typeof(Color), "Color.White")]
        public Color StartColor
        {
            get { return m_startColor; }
            set
            {
                m_startColor = value;
                Invalidate();
            }
        }

        [Category("Gradient")]
        [Description("The end color of the gradient")]
        [DefaultValue(typeof(Color), "Color.LightSkyBlue")]
        public Color EndColor
        {
            get { return m_endColor; }
            set
            {
                m_endColor = value;
                Invalidate();
            }
        }

        [Category("Gradient")]
        [Description("The angle of the gradient")]
        [DefaultValue(0)]
        public float Angle
        {
            get { return m_angle; }
            set
            {
                m_angle = Convert.ToInt64(value);
                Invalidate();
            }
        }

        [Category("Drop Shadow")]
        [Description("Set to true to draw the Drop Shadow")]
        [DefaultValue(false)]
        public bool DrawShadow
        {
            get { return m_drawShadow; }
            set
            {
                m_drawShadow = value;
                Invalidate();
            }
        }

        [Category("Drop Shadow")]
        [Description("The X Offset used to draw the shadow")]
        [DefaultValue(1)]
        public float XOffset
        {
            get { return m_xOffset; }
            set
            {
                m_xOffset = Convert.ToInt64(value);
                Invalidate();
            }
        }

        [Category("Drop Shadow")]
        [Description("The Y Offset used to draw the shadow")]
        [DefaultValue(1)]
        public float YOffset
        {
            get { return m_yOffset; }
            set
            {
                m_yOffset = Convert.ToInt64(value);
                Invalidate();
            }
        }

        [Category("Drop Shadow")]
        [Description("The color used to draw the shadow")]
        [DefaultValue(typeof(Color), "Color.Black")]
        public Color ShadowColor
        {
            get { return m_shadowColor; }
            set
            {
                m_shadowColor = value;
                Invalidate();
            }
        }

        //[Description("DataBindings.")]
        //public new ControlBindingsCollection DataBindings
        //{
        //    get { return Label1.DataBindings; }
        //}

        public void UpdateText()
        {
            //Binding dbnText = null;
            string exactField = null;
            //bool fldErr = false;

            //if (Label1.DataBindings.Count > 0) return;

            try
            {
                if (DataControl != null)
                    if ("" + DBField != "")
                    {
                        exactField = DataControl.FieldExactName(DBField);
                        if (exactField == "")
                            throw new ExceptionUtil("Campo:" + DBField + ", inexistente.");
                        //fldErr = true;
                        label.Text = DataControl.GetField(exactField).ToString();
                    }
                //else
                //{
                //    dbnText = new Binding("Text", this, "Text");
                //}
                //else
                //{
                //    dbnText = new Binding("Text", this, "Text");
                //}

                //if (!fldErr)
                //{
                //    dbnText.Format += dbnFormat;

                //    Label1.DataBindings.Add(dbnText);
                //}
            }
            catch (Exception e)
            {
                throw new ExceptionUtil("Campo: " + DBField, e);
            }
        }


        protected void dbnFormat(object sender, ConvertEventArgs e)
        {
            //DBCrypt enc = null; 
            try
            {
                switch (DataType)
                {
                    case DBTextBox.TypeData.Date:
                        if (e.Value is DBNull | string.IsNullOrEmpty(e.Value.ToString()))
                        {
                            e.Value = Global.SINDEFINIR;
                        }
                        else
                        {
                            var dt = Convert.ToDateTime(e.Value);
                            e.Value = dt.ToString(DateFormat);
                        }

                        break;
                    case DBTextBox.TypeData.Time:
                        if (e.Value is DBNull | string.IsNullOrEmpty(e.Value.ToString()))
                            e.Value = Global.SINDEFINIR;
                        else
                            e.Value = Convert.ToDateTime(e.Value).ToShortTimeString();
                        break;
                    case DBTextBox.TypeData.Money:
                        if (!NumberUtils.IsNumeric(e.Value.ToString())) e.Value = "";
                        if (e.Value + "".Trim() == "") e.Value = "0";
                        e.Value = Convert.ToDouble(e.Value).ToString("c");
                        break;
                    case DBTextBox.TypeData.Numeric:
                        if (!NumberUtils.IsNumeric(e.Value.ToString())) e.Value = "";
                        if (e.Value + "".Trim() == "") e.Value = "0";
                        e.Value = Convert.ToDouble(e.Value).ToString("n" + Decimals);
                        break;
                    case DBTextBox.TypeData.Formula:
                        if (e.Value + "".Trim() == "") e.Value = "0";
                        e.Value = Convert.ToDouble(e.Value).ToString("n" + Decimals);
                        break;
                    case DBTextBox.TypeData.Pecentage:
                        var percentageValue = Convert.ToDouble(e.Value);
                        percentageValue = percentageValue / 100;
                        e.Value = percentageValue.ToString("P0");
                        break;
                    case DBTextBox.TypeData.All:
                        e.Value = e.Value + "";
                        switch (Capitalize)
                        {
                            case DBTextBox.TypeString.Uppercase:
                                e.Value = e.Value.ToString().ToUpper();
                                break;
                            case DBTextBox.TypeString.Lowercase:
                                e.Value = e.Value.ToString().ToLower();
                                break;
                            case DBTextBox.TypeString.Capitalize:
                                e.Value = TextUtil.PCase(Convert.ToString(e.Value));
                                break;
                            case DBTextBox.TypeString.Normal:
                                break;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void Label1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }


        private void Label1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (m_drawGradient)
            {
                var brush = new LinearGradientBrush(new Rectangle(0, 0, label.Width, label.Height),
                    m_startColor,
                    m_endColor, m_angle, true);
                e.Graphics.FillRectangle(brush, 0, 0, label.Width, label.Height);
            }

            if (m_drawShadow)
            {
                e.Graphics.DrawString(label.Text, label.Font, new SolidBrush(m_shadowColor), m_xOffset, m_yOffset,
                    StringFormat.GenericDefault);
                e.Graphics.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), 0, 0,
                    StringFormat.GenericDefault);
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        public DBLabel()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);

            TabStop = false;

            label.MouseDown += Label1_MouseDown;
            label.MouseUp += Label1_MouseUp;
            label.Paint += Label1_Paint;
            this.Resize += Label1_Resize;
        }

        private void Label1_Resize(object sender, EventArgs e)
        {
            label.Size = this.Size;
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
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(78, 23);
            this.label.TabIndex = 0;
            this.label.Text = "DBLabel";
            // 
            // DBLabel
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label);
            this.Name = "DBLabel";
            this.Size = new System.Drawing.Size(78, 23);
            this.ResumeLayout(false);

        }

        #endregion
    }
}