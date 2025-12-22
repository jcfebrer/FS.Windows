using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBLabel.bmp")]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    public class DBLabel : Label, ISupportInitialize
    {
        /* INICIO COMPATIBILIDAD INFRAGISTICS */

        public event EventHandler AfterEnterEditMode;
        public event EventHandler AfterExitEditMode;
        public event EventHandler SelectionChanged;
        public event EventHandler ValueChanged;
        public event DBEditorButtonEventHandler EditorButtonClick;

        private DBAppearance m_Appearance = new DBAppearance();
        public DBAppearance Appearance {
            get { return m_Appearance; }
            set {

                if (value != null)
                {
                    this.ForeColor = value.ForeColor;
                    this.BackColor = value.BackColor;

                    if (value.Alignment == HorizontalAlignment.Left)
                    {
                        this.TextAlign = ContentAlignment.MiddleLeft;
                    }
                    else if(value.Alignment == HorizontalAlignment.Center)
                    {
                        this.TextAlign = ContentAlignment.MiddleCenter;
                    }
                    else if(value.Alignment == HorizontalAlignment.Right)
                    {
                        this.TextAlign = ContentAlignment.MiddleRight;
                    }

                    if ((value.TextVAlignAsString == "Middle" && value.TextHAlignAsString == "Center") || value.TextHAlignAsString == "Center")
                    {
                        this.TextAlign = ContentAlignment.MiddleCenter;
                    }
                    else if ((value.TextVAlignAsString == "Middle" && value.TextHAlignAsString == "Left") || value.TextHAlignAsString == "Left")
                    {
                        this.TextAlign = ContentAlignment.MiddleLeft;
                    }
                    else if ((value.TextVAlignAsString == "Middle" && value.TextHAlignAsString == "Right") || value.TextHAlignAsString == "Right")
                    {
                        this.TextAlign = ContentAlignment.MiddleRight;
                    }
                    else if (value.TextVAlignAsString == "Top" && value.TextHAlignAsString == "Center")
                    {
                        this.TextAlign = ContentAlignment.TopCenter;
                    }
                    else if (value.TextVAlignAsString == "Top" && value.TextHAlignAsString == "Left")
                    {
                        this.TextAlign = ContentAlignment.TopLeft;
                    }
                    else if (value.TextVAlignAsString == "Top" && value.TextHAlignAsString == "Right")
                    {
                        this.TextAlign = ContentAlignment.TopRight;
                    }
                    else if (value.TextVAlignAsString == "Bottom" && value.TextHAlignAsString == "Center")
                    {
                        this.TextAlign = ContentAlignment.BottomCenter;
                    }
                    else if (value.TextVAlignAsString == "Bottom" && value.TextHAlignAsString == "Left")
                    {
                        this.TextAlign = ContentAlignment.BottomLeft;
                    }
                    else if (value.TextVAlignAsString == "Bottom" && value.TextHAlignAsString == "Right")
                    {
                        this.TextAlign = ContentAlignment.BottomRight;
                    }
                }
            }
        }
        
        public object DataControl { get; set; }
        public string DBField { get; set; }
        public string DBFieldData { get; set; }
        public bool Editable { get; set; }
        public bool GridMode { get; set; }
        public bool IsInEditMode { get; set; }
        public bool ShowCode { get; set; }
        public string About { get; set; }
        public BorderStyle BorderStyleInner { get; set; }
        public BorderStyle BorderStyleOuter { get; set; }
        public int Decimals { get; set; } = 2;

        /* FIN COMPATIBILIDAD INFRAGISTICS */

        public DBLabel()
        {
            Editable = true;
            GridMode = false;
            ShowCode = false;
            IsInEditMode = false;
        }

        private Color m_startColor = Color.White;
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

        private Color m_endColor = Color.LightSkyBlue;
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

        private Color m_shadowColor = Color.Black;
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

        private float m_xOffset = 1;
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

        private float m_yOffset = 1;
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

        private float m_angle = 0;
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

        private Global.AccessMode m_Mode = Global.AccessMode.ReadMode;
        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;
                if (m_Mode == Global.AccessMode.WriteMode)
                    this.Enabled = true;
                else
                    this.Enabled = true;
            }
        }

        [Description(
            "Indicamos el modo en que debe presentar la cadena. May?sculas / Min?sculas o inicial en may?scula resto min?scula."
        )]
        public DBTextBox.TypeString Capitalize { get; set; } = DBTextBox.TypeString.Normal;

        [Description("Tipo de datos a introducir en el control. Texto, Numérico, Fecha, Porcentaje, ...")]
        public DBTextBox.TypeData DataType { get; set; } = DBTextBox.TypeData.All;

        [Description("Indicamos la forma de representarse una fecha.")]
        public string DateFormat { get; set; } = "dd/MM/yyyy";

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}
