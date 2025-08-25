using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBTextBox.bmp")]
    [ToolboxItem(true)]
    [Serializable]
    public class DBTextBox : System.Windows.Forms.TextBox, ISupportInitialize
    {
        public DBTextBox()
        {
            this.TextChanged += DBTextBox_TextChanged;

            InitializeButtons();
        }

        private void DBTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode)
                return;

            if (LastValue == this.Text)
                return;

            if (FireTextChanged == false)
                return;

            if (DataControl != null)
                DataControl.SetField(DBField, this.Text);

            if (ValueChanged != null)
                ValueChanged(sender, e);

            LastValue = this.Text;
        }

        /* INICIO COMPATIBILIDAD INFRAGISTICS */

        public delegate void MouseEnterElementEventHandler(object sender, DBEditorButtonEventArgs e);
        public delegate void MaskChangedEventHandler(object sender, EventArgs e);

        public event EventHandler AfterEnterEditMode;
        public event EventHandler AfterExitEditMode;
        public event EventHandler ValueChanged;
        public event DBEditorButtonEventHandler EditorButtonClick;
        public event MouseEnterElementEventHandler MouseEnterElement;
        public event MaskChangedEventHandler MaskChanged;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object Value
        {
            get { return this.Text; }
            set { this.Text = value.ToString(); }
        }

        public enum TypeData
        {
            All,
            Numeric,
            Money,
            Pecentage,
            Date,
            Formula,
            Time,
            Memo
        }

        public enum SelectAllBehaviorEnum
        {
            SelectAllCharacters,
            SelectEnteredCharacters
        }

        public enum NumericTypeEnum
        {
            Decimal,
            Double,
            Integer
        }

        public enum TypeString
        {
            Normal,
            Uppercase,
            Lowercase,
            Capitalize
        }

        public enum EditAsType
        {
            UseSpecifiedMask
        }

        public enum TabNavigationEnum
        {
            NextControl,
            NextSection
        }

        private DBAppearance m_Appearance = new DBAppearance();
        public DBAppearance Appearance
        {
            get { return m_Appearance; }
            set
            {

                if (value != null)
                {
                    this.ForeColor = value.ForeColor;
                    this.BackColor = value.BackColor;
                    this.TextAlign = value.Alignment;

                    if(value.TextHAlignAsString == "Left")
                        this.TextAlign = HorizontalAlignment.Left;
                    else if(value.TextHAlignAsString == "Center")
                        this.TextAlign = HorizontalAlignment.Center;
                    else if(value.TextHAlignAsString == "Right")
                        this.TextAlign = HorizontalAlignment.Right;
                }
            }
        }

        public object About { get; set; }
        public char PromptChar { get; set; }
        public SelectAllBehaviorEnum SelectAllBehavior { get; set; }
        public string FormatString { get; set; } = "";
        public bool FireTextChanged { get; set; } = true;
        public EditAsType EditAs { get; set; }
        public bool Encrypted { get; set; }
        public string Expression { get; set; } = "";
        public int NonAutoSizeHeight { get; set; }
        public bool SendTabAsEnter { get; set; } = true;
        public bool SendCommaAsPoint { get; set; } = true;
        public bool ShowAsCombo { get; set; }
        public TabNavigationEnum TabNavigation { get; set; }
        public DBButtonCollection ButtonsRight { get; set; } = new DBButtonCollection();
        public DBButtonCollection ButtonsLeft { get; set; } = new DBButtonCollection();
        public string LastValue { get; private set; } = "";

        private string m_ToolTip = "";
        [Description("Literal utilizado cuando el cursor se mueve por el control. Aparece con fondo amarillo.")]
        public string ToolTip
        {
            get { return m_ToolTip; }
            set
            {
                m_ToolTip = value;
                System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
                toolTip.SetToolTip(this, m_ToolTip);
            }
        }

        private string m_XMLName = "";
        [Description(
            "Nombre XML del control. Esta propiedad se utiliza para la generación de código XML a partir de un formulario."
        )]
        public string XMLName
        {
            get
            {
                return m_XMLName;
            }
            set { m_XMLName = value; }
        }

        private bool m_Shadow = false;
        [Description("Activar sombra si/no.")]
        public bool Shadow
        {
            get { return m_Shadow; }
            set
            {
                m_Shadow = value;
            }
        }

        private bool m_ShowKeyboard = false;    
        public bool ShowKeyboard
        {
            get { return m_ShowKeyboard; }
            set
            {
                m_ShowKeyboard = value;
            }
        }

        public ScrollBars ShowScrollBars
        {
            get { return this.ScrollBars; }
            set { this.ScrollBars = value; }
        }

        [Description("Color de la sombra.")]
        public Color ShadowColor { get; set; } = Color.Gray;

        private int m_ShadowSize = 4;
        [Description("Tamaño en pixels de la sombra.")]
        public int ShadowSize
        {
            get { return m_ShadowSize; }
            set
            {
                m_ShadowSize = value;
            }
        }

        public DBColumn.OperationTypes GridOperation { get; set; } = DBColumn.OperationTypes.Sum;

        [Description(
            "DBComboEx asociado al control. Si se produce un cambio en el combo asociado, se actualiza el contenido de este control."
        )]
        public DBComboEx AsociatedCombo { get; set; }
        [Description(
            "DBFindTextBox asociado al control. Si se produce un cambio en el DBFindTextBox asociado, se actualiza el contenido de este control."
        )]
        public DBFindTextBox AsociatedDBFindTextBox { get; set; }
        [Description("Color de fondo del control en modo lectura.")]
        public Color BackColorRead { get; set; } = Color.WhiteSmoke;
        [Description(
            "Indicamos el modo en que debe presentar la cadena. May?sculas / Min?sculas o inicial en may?scula resto min?scula."
        )]
        public TypeString Capitalize { get; set; } = TypeString.Normal;

        private NumericTypeEnum m_NumericType = NumericTypeEnum.Double;
        public NumericTypeEnum NumericType {
            get { return m_NumericType; }
            set { 
                m_NumericType = value; 
                this.TextAlign = HorizontalAlignment.Right;
            }
        }

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

        [Description("Número de decimales utilizado si el control es Numérico.")]
        public int Decimals { get; set; } = 2;

        private TypeData m_DataType = TypeData.All;
        [Description("Tipo de datos a introducir en el control. Texto, Numérico, Fecha, Porcentaje, ...")]
        public TypeData DataType
        {
            get { return m_DataType; }
            set
            {
                m_DataType = value;
            }
        }

        private bool m_DotNumber = false;
        [Description("Punto decimal en los miles si/no.")]
        public bool DotNumber
        {
            get
            {
                return m_DotNumber;
            }
            set { m_DotNumber = value; }
        }

        private bool m_Obligatory = false;
        [Description("Obligamos al usuario a introducir datos en este control.")]
        public bool Obligatory
        {
            get { return m_Obligatory; }
            set
            {
                m_Obligatory = value;
                if (Mode == Global.AccessMode.WriteMode)
                {
                    if (value)
                        this.BackColor = Global.ObligatoryBackColor;
                    else
                        this.BackColor = Global.NormalBackColor;
                }
            }
        }

        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;
        [Description("Estado del control. Lectura / Escritura / Protegido.")]
        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        this.ReadOnly = true;
                        this.BackColor = BackColorRead;
                        break;
                    case Global.AccessMode.WriteMode:
                        if (Editable)
                        {
                            this.ReadOnly = false;
                            if (m_Obligatory)
                                this.BackColor = Global.ObligatoryBackColor;
                            else
                                this.BackColor = Global.NormalBackColor;
                        }

                        break;
                    case Global.AccessMode.ProtectedMode:
                        this.BackColor = Global.ProtectedBackColor;
                        break;
                }
            }
        }

        private bool m_Editable = false;
        [Description("Indicamos si el control es editable o no.")]
        public bool Editable
        {
            get { return m_Editable; }
            set
            {
                m_Editable = value;
                this.ReadOnly = !m_Editable;
                if (m_Editable)
                {
                    if (!m_Obligatory) this.BackColor = Global.NormalBackColor;
                    this.TabStop = true;
                }
                else
                {
                    this.BackColor = Global.DisableBackColor;
                    this.TabStop = false;
                }
            }
        }

        [Description(
            "Valor por defecto del campo. Se puede utilizar las palabras reservadas: #date#, #time#, #shortdate#, #shorttime#, #longdate#, #longtime#, para indicar la fecha y hora actuales."
        )]
        public string DefaultValue { get; set; } = "";

        [Description("Tipo de letra del control.")]
        public Font DBFont
        {
            get { return this.Font; }
            set { this.Font = value; }
        }

        [Description("Indicamos la forma de representarse una fecha.")]
        public string DateFormat { get; set; } = "dd/MM/yyyy";

        decimal _maxValue;
        public decimal MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        decimal _minValue;
        public decimal MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        private char[] m_aMask;
        private char[] m_aMskMask;
        private string m_MaskInput = string.Empty;
        public string InputMask
        {
            get { return MaskInput; }
            set { MaskInput = value; }
        }

        public string MaskInput
        {
            get { return m_MaskInput; }
            set
            {
                m_MaskInput = value;
                SetMask();
            }
        }

        private void SetMask()
        {
            var f = 0;

            if (string.IsNullOrEmpty(m_MaskInput))
                return;

            FireTextChanged = false;
            this.Text = m_MaskInput;
            this.Text = this.Text.Replace("#", "_");
            this.Text = this.Text.Replace("&", "_");
            this.Text = this.Text.Replace("n", "_");
            this.Text = this.Text.Replace("9", "_");
            FireTextChanged = true;

            m_aMask = new char[this.Text.Length];
            m_aMskMask = new char[this.Text.Length];

            for (f = 0; f <= m_MaskInput.Length - 1; f++)
                if ((m_MaskInput.Substring(f, 1) == "#") | (m_MaskInput.Substring(f, 1) == "n") |
                    (m_MaskInput.Substring(f, 1) == "&") | (m_MaskInput.Substring(f, 1) == "9"))
                    m_aMask.SetValue(char.Parse("_"), f);
                else
                    m_aMask.SetValue(char.Parse(m_MaskInput.Substring(f, 1)), f);

            for (f = 0; f <= m_MaskInput.Length - 1; f++)
                m_aMskMask.SetValue(char.Parse(m_MaskInput.Substring(f, 1)), f);
        }

        private void InitializeButtons()
        {
            if (ButtonsRight != null && ButtonsRight.Count > 0)
            {
                foreach (DBButtonEx button in ButtonsRight)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.Width = 16;
                    button.Height = 16;
                    button.Visible = true;
                    button.Top = 0;
                    button.Click += Button_Click;
                    button.ToolTip = button.Text;
                    button.MouseEnter += Button_MouseEnter;

                    button.BringToFront();
                }
            }

            if (ButtonsLeft != null && ButtonsLeft.Count > 0)
            {
                foreach (DBButtonEx button in ButtonsLeft)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.Width = 16;
                    button.Height = 16;
                    button.Visible = true;
                    button.Top = 0;
                    button.Click += Button_Click;
                    button.ToolTip = button.Text;
                    button.MouseEnter += Button_MouseEnter;

                    button.BringToFront();
                }
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (null != MouseEnterElement)
                MouseEnterElement(this, new DBEditorButtonEventArgs());
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (DBButtonEx)sender;

            if (EditorButtonClick != null)
                EditorButtonClick(sender, new DBEditorButtonEventArgs());
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        /* FIN COMPATIBILIDAD INFRAGISTICS */
    }
}
