#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FSLibrary;
using DateTimeUtil = FSLibrary.DateTimeUtil;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBTextBox.bmp")]
    [DefaultEvent("KeyPress")]
    [Designer(typeof(DBTextBoxControlDesigner))]
    [ToolboxItem(true)]
    public class DBTextBox : DBUserControl, ISupportInitialize
    {
        internal Button cmdAmpliar;
        public string inicVal;
        private char[] m_aMask;
        private char[] m_aMskMask;
        private Color m_BackColor = Global.NormalBackColor;
        private BorderStyle m_BorderStyle = BorderStyle.Fixed3D;

        private TypeData m_DataType = TypeData.All;
        private bool m_DotNumber;
        private bool m_Editable = true;
        private string m_MaskInput = "";
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;
        private bool m_MultiLine;
        private bool m_Obligatory;
        private bool m_Shadow;
        private int m_ShadowSize = 4;
        private bool m_ShowKeyboard;
        private string m_ToolTip = "";
        private string m_XMLName = "";


        public DBTextBox()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);

            //lblShadow.TabStop = false;

            cmdAmpliar.Click += cmdAmpliar_Click;
            cmdCalc.Click += cmdCalc_Click;
            cmdKeyboard.Click += cmdKeyboard_Click;

            Resize += Control_Resize;

            textbox.KeyDown += Text1_KeyDown;
            textbox.Leave += Text1_Leave;
            textbox.KeyUp += Text1_KeyUp;
            textbox.MouseDown += Text1_MouseDown;
            textbox.KeyPress += Text1_KeyPress;
            textbox.Enter += Text1_Enter;
            textbox.LostFocus += Text1_LostFocus;
            textbox.MouseUp += Text1_MouseUp;
            textbox.TextChanged += Text1_Changed;
            textbox.MouseEnter += Text1_MouseEnter;
            textbox.GotFocus += Text1_GotFocus;

            Load += DBTextBox_Load;
        }

        private void DBTextBox_Load(object sender, EventArgs e)
        {
            InitializeButtons();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }


        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return textbox.Text; }
            set { textbox.Text = value; }
        }


        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object Value
        {
            get { return textbox.Text; }
            set { textbox.Text = value.ToString(); }
        }

        public ScrollBars ScrollBars
        {
            get { return textbox.ScrollBars; }
            set { textbox.ScrollBars = value; }
        }

        public NumericTypeEnum NumericType { get; set; } = NumericTypeEnum.Double;

        public char PromptChar { get; set; }

        public bool SendTabAsEnter { get; set; } = true;

        public bool SendCommaAsPoint { get; set; } = true;

        public bool AcceptsReturn
        {
            get { return textbox.AcceptsReturn; }
            set { textbox.AcceptsReturn = value; }
        }

        public decimal MaxValue { get; set; } = decimal.MaxValue;

        public decimal MinValue { get; set; } = decimal.MinValue;

        public EditAsType EditAs { get; set; }

        public TabNavigationEnum TabNavigation { get; set; }

        public int NonAutoSizeHeight { get; set; }

        public SelectAllBehaviorEnum SelectAllBehavior { get; set; }

        public string FormatString { get; set; } = "";

        public ScrollBars ShowScrollBars
        {
            get { return textbox.ScrollBars; }
            set { textbox.ScrollBars = value; }
        }

        public DBButtonCollection ButtonsRight { get; set; } = new DBButtonCollection();

        public DBButtonCollection ButtonsLeft { get; set; } = new DBButtonCollection();

        public bool ShowKeyboard
        {
            get { return m_ShowKeyboard; }
            set
            {
                m_ShowKeyboard = value;

                CalcKeyboard();
            }
        }

        public bool ShowAsCombo { get; set; }

        public bool FireTextChanged { get; set; } = true;

        public DBColumn.OperationTypes GridOperation { get; set; } = DBColumn.OperationTypes.Sum;

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
                        textbox.BackColor = Global.ObligatoryBackColor;
                    else
                        textbox.BackColor = m_BackColor;
                }
            }
        }

        public HorizontalAlignment TextAlign
        {
            get { return textbox.TextAlign; }
            set { textbox.TextAlign = value; }
        }

        [Description("Indicamos si el control es editable o no.")]
        public bool Editable
        {
            get { return m_Editable; }
            set
            {
                m_Editable = value;
                textbox.ReadOnly = !m_Editable;
                if (m_Editable)
                {
                    if (!m_Obligatory) textbox.BackColor = Global.NormalBackColor;
                    textbox.TabStop = true;
                }
                else
                {
                    textbox.BackColor = Global.DisableBackColor;
                    textbox.TabStop = false;
                }
            }
        }


        [Description("Tipo de datos a introducir en el control. Texto, Numérico, Fecha, Porcentaje, ...")]
        public TypeData DataType
        {
            get { return m_DataType; }
            set
            {
                m_DataType = value;

                if (!CalcButton()) MemoButton();
            }
        }

        public bool Encrypted { get; set; }

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
                        textbox.ReadOnly = true;
                        textbox.BackColor = BackColorRead;
                        break;
                    case Global.AccessMode.WriteMode:
                        if (Editable)
                        {
                            textbox.ReadOnly = false;
                            if (m_Obligatory)
                                textbox.BackColor = Global.ObligatoryBackColor;
                            else
                                textbox.BackColor = m_BackColor;
                        }

                        break;
                    case Global.AccessMode.ProtectedMode:
                        textbox.BackColor = Global.ProtectedBackColor;
                        break;
                }


                if (!CalcButton()) MemoButton();
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


        [Description(
            "DBCombo asociado al control. Si se produce un cambio en el combo asociado, se actualiza el contenido de este control."
        )]
        public DBCombo AsociatedCombo { get; set; }

        [Description(
            "DBFindTextBox asociado al control. Si se produce un cambio en el DBFindTextBox asociado, se actualiza el contenido de este control."
        )]
        public DBFindTextBox AsociatedDBFindTextBox { get; set; }

        [Description("Color de fondo del control.")]
        public new Color BackColor
        {
            get { return m_BackColor; }
            set
            {
                m_BackColor = value;
                textbox.BackColor = value;
            }
        }

        [Description("Color de fondo del control en modo lectura.")]
        public Color BackColorRead { get; set; } = Color.WhiteSmoke;

        [Description("Color de escritura del control.")]
        public new Color ForeColor
        {
            get { return textbox.ForeColor; }
            set { textbox.ForeColor = value; }
        }

        public DBAppearance Appearance { get; set; }

        [Description("Modo lectura")]
        public bool ReadOnly
        {
            get { return textbox.ReadOnly; }
            set { textbox.ReadOnly = value; }
        }

        [Description("Estilo de borde del control.")]
        public new BorderStyle BorderStyle
        {
            get { return m_BorderStyle; }
            set
            {
                m_BorderStyle = value;
                textbox.BorderStyle = value;
            }
        }

        [Description("Tipo de letra del control.")]
        public Font DBFont
        {
            get { return textbox.Font; }
            set { textbox.Font = value; }
        }


        [Description(
            "Longitud máxima permitida en la edición. Si existe un campo de la base de datos asociado a este control, este campo se asigna en función del tamaño del campo de la base de datos."
        )]
        public int MaxLength
        {
            get { return textbox.MaxLength; }
            set { textbox.MaxLength = value; }
        }

        [Description("Modo multilinea si/no.")]
        public bool Multiline
        {
            get { return m_MultiLine; }
            set
            {
                m_MultiLine = value;
                textbox.Multiline = value;
                //if (value) textbox.Height = Height;
                if (Shadow)
                    Height = textbox.Height + m_ShadowSize;
                //else
                    //Height = textbox.Height;
            }
        }

        [Description(
            "Indicamos el modo en que debe presentar la cadena. May?sculas / Min?sculas o inicial en may?scula resto min?scula."
        )]
        public TypeString Capitalize { get; set; } = TypeString.Normal;

        [Description("Indicamos la forma de representarse una fecha.")]
        public string DateFormat { get; set; } = "dd/MM/yyyy";

        [Description("Punto decimal en los miles si/no.")]
        public bool DotNumber
        {
            get
            {
                var dotNumberReturn = false;
                dotNumberReturn = m_DotNumber;
                return dotNumberReturn;
            }
            set { m_DotNumber = value; }
        }

        [Description("Caracter utilizado en el campo password.")]
        public char PasswordChar
        {
            get { return textbox.PasswordChar; }
            set { textbox.PasswordChar = value; }
        }

        [Description(
            "Nombre XML del control. Esta propiedad se utiliza para la generación de código XML a partir de un formulario."
        )]
        public string XMLName
        {
            get
            {
                string xMLNameReturn = null;
                xMLNameReturn = m_XMLName;
                return xMLNameReturn;
            }
            set { m_XMLName = value; }
        }

        [Description("Activar sombra si/no.")]
        public bool Shadow
        {
            get { return m_Shadow; }
            set
            {
                m_Shadow = value;
            }
        }

        [Description("Color de la sombra.")]
        public Color ShadowColor { get; set; } = Color.Gray;

        [Description("Tamaño en pixels de la sombra.")]
        public int ShadowSize
        {
            get { return m_ShadowSize; }
            set
            {
                m_ShadowSize = value;
            }
        }

        //[Description("DataBindings.")]
        //public new ControlBindingsCollection DataBindings
        //{
        //    get { return Text1.DataBindings; }
        //}


        [Description("Literal utilizado cuando el cursor se mueve por el control. Aparece con fondo amarillo.")]
        public string ToolTip
        {
            get { return m_ToolTip; }
            set
            {
                m_ToolTip = value;
                ToolTip1.SetToolTip(textbox, m_ToolTip);
            }
        }

        [Description(
            "Valor por defecto del campo. Se puede utilizar las palabras reservadas: #date#, #time#, #shortdate#, #shorttime#, #longdate#, #longtime#, para indicar la fecha y hora actuales."
        )]
        public string DefaultValue { get; set; } = "";


        public int SelectionStart
        {
            get { return textbox.SelectionStart; }
            set { textbox.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return textbox.SelectionLength; }
            set { textbox.SelectionLength = value; }
        }

        public string Expression { get; set; } = "";

        public string LastValue { get; private set; } = "";
		
		public void ScrollToEnd()
		{
			textbox.SelectionStart = textbox.Text.Length;
			textbox.ScrollToCaret();
		}

        private void SetMask()
        {
            var f = 0;

            if (string.IsNullOrEmpty(m_MaskInput)) 
                return;

            try
            {
                FireTextChanged = false;
                textbox.Text = m_MaskInput;
                textbox.Text = textbox.Text.Replace("#", "_");
                textbox.Text = textbox.Text.Replace("&", "_");
                textbox.Text = textbox.Text.Replace("n", "_");
                textbox.Text = textbox.Text.Replace("9", "_");
                FireTextChanged = true;

                m_aMask = new char[textbox.Text.Length];
                m_aMskMask = new char[textbox.Text.Length];

                for (f = 0; f <= m_MaskInput.Length - 1; f++)
                    if ((m_MaskInput.Substring(f, 1) == "#") | (m_MaskInput.Substring(f, 1) == "n") |
                        (m_MaskInput.Substring(f, 1) == "&") | (m_MaskInput.Substring(f, 1) == "9"))
                        m_aMask.SetValue(char.Parse("_"), f);
                    else
                        m_aMask.SetValue(char.Parse(m_MaskInput.Substring(f, 1)), f);

                for (f = 0; f <= m_MaskInput.Length - 1; f++)
                    m_aMskMask.SetValue(char.Parse(m_MaskInput.Substring(f, 1)), f);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        private void Text1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode) return;
            string validkeys = null;
            var t = (Keys) e.KeyChar;

            if (!string.IsNullOrEmpty(m_MaskInput))
            {
                CheckMaskKeyPress(e);
            }
            else
            {
                if ((t == Keys.Delete) | (t == Keys.Back)) return;

                switch (m_DataType)
                {
                    case TypeData.Numeric:
                        validkeys = "1234567890.,-";
                        if (validkeys.IndexOf(e.KeyChar) + 1 == 0) e.Handled = true;
                        break;
                    case TypeData.Date:
                        validkeys = "1234567890/";
                        if (validkeys.IndexOf(e.KeyChar) + 1 == 0) e.Handled = true;
                        break;
                    case TypeData.Time:
                        validkeys = "1234567890:";
                        if (validkeys.IndexOf(e.KeyChar) + 1 == 0) e.Handled = true;
                        break;
                }
            }

            if (null != KeyPress)
                KeyPress(this, e);
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keydata)
        {
            switch (msg.WParam.ToInt32())
            {
                case 190:
                case 110:
                    if ((DataType == TypeData.Numeric) | (DataType == TypeData.Money))
                        if (SendCommaAsPoint)
                        {
                            SendKeys.Send(",");
                            return true;
                        }

                    break;
                case 13:
                    if (Multiline != true)
                        if (SendTabAsEnter)
                        {
                            SendKeys.Send("{TAB}");
                            return true;
                        }

                    break;
            }


            return base.ProcessCmdKey(ref msg, keydata);
        }

        public bool IsAllSelected()
        {
            if (textbox.SelectedText == textbox.Text)
                return true;
            return false;
        }


        public void Text1_LostFocus(object sender, EventArgs e)
        {
            if (null != LostFocus)
                LostFocus(this, e);

            if (null != AfterExitEditMode)
                AfterExitEditMode(this, e);
        }

        private bool CalcButton()
        {
            var showb = false;
            try
            {
                if ((m_DataType == TypeData.Numeric) | (m_DataType == TypeData.Money))
                {
                    if (Mode == Global.AccessMode.WriteMode)
                        showb = Global.ShowCalc;
                    else
                        showb = false;
                }
                else
                {
                    showb = false;
                }

                if (showb)
                {
                    cmdCalc.Visible = true;
                    textbox.Width = Width - 16;

                    cmdCalc.Width = 16;
                    //cmdCalc.Height = Height;
                    cmdCalc.Left = Width - 16;
                }
                else
                {
                    cmdCalc.Visible = false;
                    textbox.Width = Width;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return showb;
        }


        private bool MemoButton()
        {
            var showb = false;
            try
            {
                if (m_DataType == TypeData.Memo)
                {
                    if (Mode == Global.AccessMode.WriteMode)
                        showb = true;
                    else
                        showb = false;
                }
                else
                {
                    showb = false;
                }

                if (showb)
                {
                    cmdAmpliar.Visible = true;
                    textbox.Width = Width - 16;

                    cmdAmpliar.Width = 16;
                    //cmdAmpliar.Height = Height;
                    cmdAmpliar.Left = Width - 16;
                }
                else
                {
                    cmdAmpliar.Visible = false;
                    textbox.Width = Width;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return showb;
        }


        private bool CalcKeyboard()
        {
            try
            {
                if (ShowKeyboard)
                {
                    cmdKeyboard.Visible = true;
                    textbox.Width = Width - 32;

                    cmdKeyboard.Width = 32;
                    //cmdKeyboard.Height = Height;
                    cmdKeyboard.Left = Width - 32;
                }
                else
                {
                    cmdKeyboard.Visible = false;
                    textbox.Width = Width;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return ShowKeyboard;
        }

        //     private void ComboMode()
        //     {
        //         bool showb = m_ShowAsCombo;
        //         try
        //         {
        //             if (showb)
        //             {
        //                 DbCombo1.DataControl = DataControl;
        //                 DbCombo1.DataControlList = DataControl;
        //                 DbCombo1.DBFieldList = DBField;
        //                 DbCombo1.DBFieldData = DBField;
        //                 DbCombo1.DBField = DBField;

        //                 if (DataControl != null)
        //                 {
        //                     DbCombo1.Fill();
        //                 }

        //                 DbCombo1.Visible = true;
        //                 Text1.Visible = false;

        //                 DbCombo1.Width = Width;
        //                 DbCombo1.Left = 0;
        //             }
        //             else
        //             {
        //                 DbCombo1.Visible = false;
        //                 Text1.Visible = true;
        //                 Text1.Height = Height;
        //             }
        //         }
        //catch (System.Exception e)
        //         {
        //             throw new ExceptionUtil(e);
        //         }
        //     }

        private void Control_Resize(object sender, EventArgs e)
        {
            textbox.Location = new Point(0, 0);
            textbox.Size = new Size(this.Width - (16 * ButtonsRight.Count), textbox.Height);
            //this.Height = textbox.Height;
            
            ResizeButtons();
        }


        private void InitializeButtons()
        {
            if (ButtonsRight != null && ButtonsRight.Count > 0)
            {
                foreach (DBButton button in ButtonsRight)
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
                foreach (DBButton button in ButtonsLeft)
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

        private void ResizeButtons()
        {
            int r = 1;
            if (ButtonsRight != null && ButtonsRight.Count > 0)
            {
                foreach (DBButton button in ButtonsRight)
                {
                    button.Left = this.Width - 16 * r;

                    r++;
                }
            }

            int l = 0;
            if (ButtonsLeft != null && ButtonsLeft.Count > 0)
            {
                foreach (DBButton button in ButtonsLeft)
                {
                    button.Left = l * 16;

                    l++;
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
            var button = (DBButton) sender;

            if (EditorButtonClick != null)
                EditorButtonClick(sender, new DBEditorButtonEventArgs());
        }

        private void Text1_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.F2)
            {
                var fZoom = new frmZoom();
                fZoom.ZoomText = Text;
                fZoom.MaxLength = Convert.ToInt32(MaxLength);
                fZoom.ShowDialog();
                if (fZoom.ZoomText != null) 
                    Text = fZoom.ZoomText;
            }

            //e.Handled = true;
            if (null != KeyDown) 
                KeyDown(this, e);

            if (Mode == Global.AccessMode.ReadMode) 
                return;
            if (!string.IsNullOrEmpty(m_MaskInput)) 
                CheckMaskKeyDown(e);
            if (null != KeyDown) 
                KeyDown(sender, e);
        }


        private void Text1_KeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (Mode == Global.AccessMode.ReadMode) return;
            if (null != KeyUp) KeyUp(sender, e);
        }


        private void Text1_Enter(object sender, EventArgs e)
        {
            base.OnEnter(e);

            //Text1.SelectAll();
            if (null != Enter)
                Enter(this, e);
        }

        private void Text1_Changed(object sender, EventArgs e)
        {
            if (Mode == Global.AccessMode.ReadMode)
                return;

            if (LastValue == textbox.Text)
                return;

            if (FireTextChanged == false)
                return;

            if (DataControl != null)
                DataControl.SetField(DBField, textbox.Text);

            if (ValueChanged != null)
                ValueChanged(sender, e);

            LastValue = textbox.Text;
        }

        private void Text1_Leave(object sender, EventArgs e)
        {
            base.OnLeave(e);

            if (null != Leave)
                Leave(this, e);
        }


        private void Text1_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);

            if (null != MouseEnter)
                MouseEnter(this, e);
        }

        private void Text1_GotFocus(object sender, EventArgs e)
        {
            base.OnGotFocus(e);

            if (null != AfterEnterEditMode)
                AfterEnterEditMode(this, e);
        }


        private void CheckMaskKeyDown(KeyEventArgs e)
        {
            var tmpset = textbox.SelectionStart;
            var f = 0;

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        textbox.Text = "";
                        for (f = tmpset; f <= m_aMask.Length - 1; f++)
                            if ((f + 1 < m_aMskMask.Length) & (f + 1 >= 0))
                                switch (Convert.ToString(m_aMskMask.GetValue(f + 1)))
                                {
                                    case ".":
                                    case "-":
                                    case @"\":
                                    case "/":
                                    case ",":
                                    case "(":
                                    case ")":
                                    case ":":
                                    case " ":
                                        m_aMask.SetValue(m_aMask.GetValue(f + 2), f);
                                        f = f + 1;
                                        break;
                                    default:
                                        m_aMask.SetValue(m_aMask.GetValue(f + 1), f);
                                        break;
                                }

                        m_aMask.SetValue(char.Parse("_"), m_aMask.Length - 1);

                        e.Handled = true;

                        textbox.Text = "";
                        for (f = 0; f <= m_aMask.Length - 1; f++) textbox.Text += Convert.ToString(m_aMask.GetValue(f));
                        textbox.SelectionStart = tmpset;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        public Point GetCaretPoint()
        {
            var start = textbox.SelectionStart;
            if (start == textbox.TextLength)
                start--;

            return textbox.GetPositionFromCharIndex(start);
        }

        private void CheckMaskKeyPress(KeyPressEventArgs e)
        {
            var tmpset = textbox.SelectionStart;
            var f = 0;

            try
            {
                if (Convert.ToInt32(e.KeyChar) == 8)
                {
                    if (m_aMask != null)
                    {
                        if (tmpset == 0)
                        {
                            e.Handled = true;
                            return;
                        }

                        if ((tmpset - 1 < m_aMskMask.Length) & (tmpset - 1 >= 0))
                            switch (Convert.ToString(m_aMskMask.GetValue(tmpset - 1)))
                            {
                                case ".":
                                case "-":
                                case @"\":
                                case "/":
                                case ",":
                                case "(":
                                case ")":
                                case ":":
                                case " ":
                                    tmpset = tmpset - 2;
                                    if ((tmpset < m_aMask.Length) & (tmpset >= 0))
                                        m_aMask.SetValue(char.Parse("_"), tmpset);
                                    tmpset = tmpset - 1;
                                    break;
                                default:
                                    tmpset = tmpset - 1;
                                    if ((tmpset < m_aMask.Length) & (tmpset >= 0))
                                        m_aMask.SetValue(char.Parse("_"), tmpset);
                                    tmpset = tmpset - 1;
                                    break;
                            }

                        e.Handled = true;

                        textbox.Text = "";
                        for (f = 0; f <= m_aMask.Length - 1; f++) textbox.Text += Convert.ToString(m_aMask.GetValue(f));
                        if (tmpset >= -1) textbox.SelectionStart = tmpset + 1;
                    }
                }
                else if (char.IsControl(e.KeyChar))
                {
                }
                else
                {
                    if (m_aMskMask != null)
                    {
                        if ((tmpset < m_aMskMask.Length) & (tmpset >= 0))
                        {
                            var mask = Convert.ToString(m_aMskMask.GetValue(tmpset));
                            switch (mask)
                            {
                                case ".":
                                case "-":
                                case @"\":
                                case "/":
                                case ",":
                                case "(":
                                case ")":
                                case ":":
                                case " ":
                                    tmpset = tmpset + 1;
                                    if (mask == "#" || mask == "n" || mask == "9")
                                    {
                                        if (char.IsDigit(e.KeyChar))
                                            m_aMask.SetValue(e.KeyChar, tmpset);
                                        else
                                            tmpset = tmpset - 1;
                                    }
                                    else if (mask == "&")
                                    {
                                        m_aMask.SetValue(e.KeyChar, tmpset);
                                    }

                                    break;
                                default:
                                    if (mask == "#" || mask == "n" || mask == "9")
                                    {
                                        if (char.IsDigit(e.KeyChar))
                                            m_aMask.SetValue(e.KeyChar, tmpset);
                                        else
                                            tmpset = tmpset - 1;
                                    }
                                    else if (mask == "&")
                                    {
                                        m_aMask.SetValue(e.KeyChar, tmpset);
                                    }

                                    break;
                            }
                        }

                        e.Handled = true;

                        textbox.Text = "";
                        for (f = 0; f <= m_aMask.Length - 1; f++) textbox.Text += Convert.ToString(m_aMask.GetValue(f));

                        textbox.SelectionStart = tmpset + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        //public void SetTextMask(string strText)
        //{
        //    int f = 0;
        //    try
        //    {
        //        if (strText == "")
        //        {
        //            if (m_MaskInput != null)
        //            {
        //                for (f = 0; f <= m_MaskInput.Length - 1; f++)
        //                {
        //                    if (f < m_MaskInput.Length)
        //                    {
        //                        if (m_MaskInput.Substring(f, 1) == "#" | m_MaskInput.Substring(f, 1) == "&")
        //                        {
        //                            m_aMask.SetValue(char.Parse("_"), f);
        //                        }
        //                        else
        //                        {
        //                            m_aMask.SetValue(char.Parse(m_MaskInput.Substring(f, 1)), f);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (f = 0; f <= strText.Length - 1; f++)
        //            {
        //                if (m_MaskInput != null)
        //                {
        //                    if (f < m_MaskInput.Length)
        //                    {
        //                        if (m_MaskInput.Substring(f, 1) == "#" | m_MaskInput.Substring(f, 1) == "&")
        //                        {
        //                            m_aMask.SetValue(char.Parse(strText.Substring(f, 1)), f);
        //                        }
        //                        else
        //                        {
        //                            m_aMask.SetValue(char.Parse(m_MaskInput.Substring(f, 1)), f);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        Text1.Text = "";

        //        if (m_aMask != null)
        //        {
        //            for (f = 0; f <= m_aMask.Length - 1; f++)
        //            {
        //                Text1.Text += Convert.ToString(m_aMask.GetValue(f));
        //            }
        //        }
        //    }
        //    catch (System.Exception e)
        //    {
        //        throw new ExceptionUtil(e);
        //    }
        //}


        public void UpdateText()
        {
            //Binding dbnText = null;
            string exactField = null;
            //bool fldErr = false;

            //if (Text1.DataBindings.Count > 0) return;

            try
            {
                if ((DataType == TypeData.Formula) | (DataType == TypeData.Money) | (DataType == TypeData.Numeric) |
                    (DataType == TypeData.Pecentage))
                    textbox.TextAlign = HorizontalAlignment.Right;


                if (DataControl != null)
                    if (!String.IsNullOrEmpty(DBField))
                    {
                        exactField = DataControl.FieldExactName(DBField);
                        if (exactField == "")
                            throw new ExceptionUtil("Campo:" + DBField + ", inexistente.");
                        //fldErr = true;

                        //dbnText = new Binding("Text", DataControl.DataTable, exactField);
                        textbox.Text = DataControl.GetField(exactField).ToString();

                        if (ToolTip == "")
                        {
                            if (Expression != "")
                                ToolTip = Expression;
                            else
                                ToolTip = exactField;
                        }

                        if (DefaultValue != "")
                        {
                            switch (DefaultValue.ToLower())
                            {
                                case "#date#":
                                case "#shortdate#":
                                    DefaultValue = DateTimeUtil.ShortDate(System.DateTime.Now);
                                    break;
                                case "#longdate#":
                                    DefaultValue = DateTimeUtil.LongDate(System.DateTime.Now);
                                    break;
                                case "#time#":
                                case "#shorttime#":
                                    DefaultValue = DateTimeUtil.ShortDate(System.DateTime.Now);
                                    break;
                                case "#longtime#":
                                    DefaultValue = DateTimeUtil.LongDate(System.DateTime.Now);
                                    break;
                            }

                            DataControl.GetColumn(DBField).DefaultValue = DefaultValue;
                        }
                    }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil("Campo: " + DBField, e);
            }
        }


        //     protected void dbnFormat(object sender, ConvertEventArgs e)
        //     {
        //         try
        //         {
        //             switch (DataType)
        //             {
        //                 case TypeData.Date:
        //                     if (e.Value is DBNull | String.IsNullOrEmpty(e.Value.ToString()))
        //                     {
        //                         e.Value = Global.SINDEFINIR;
        //                     }
        //                     else
        //                     {
        //			System.DateTime dt = Convert.ToDateTime(e.Value);
        //                         e.Value = dt.ToString(m_DateFormat);
        //                     }
        //                     break;
        //                 case TypeData.Time:
        //                     if (e.Value is DBNull | String.IsNullOrEmpty(e.Value.ToString()))
        //                     {
        //                         e.Value = Global.SINDEFINIR;
        //                     }
        //                     else
        //                     {
        //                         e.Value = Convert.ToDateTime(e.Value).ToShortTimeString();
        //                     }
        //                     break;
        //                 case TypeData.Money:
        //                     if (!(NumberUtils.IsNumeric(e.Value)))
        //                     {
        //                         e.Value = "";
        //                     }
        //                     if (e.Value + "".Trim() == "")
        //                     {
        //                         e.Value = "0";
        //                     }
        //                     e.Value = Convert.ToDouble(e.Value).ToString("c");
        //                     if (FSLibrary.Functions.NumeroDecimal(e.Value) > m_MaxValue)
        //                     {
        //                         e.Value = m_MaxValue;
        //                     }
        //                     break;
        //                 case TypeData.Numeric:
        //                     if (!(NumberUtils.IsNumeric(e.Value)))
        //                     {
        //                         e.Value = "";
        //                     }
        //                     if (e.Value + "".Trim() == "")
        //                     {
        //                         e.Value = "0";
        //                     }
        //                     e.Value = Convert.ToDouble(e.Value).ToString("n" + Decimals);
        //                     if (FSLibrary.Functions.NumeroDecimal(e.Value) > m_MaxValue)
        //                     {
        //                         e.Value = m_MaxValue;
        //                     }
        //                     break;
        //                 case TypeData.Formula:
        //                     if (e.Value + "".Trim() == "")
        //                     {
        //                         e.Value = "0";
        //                     }
        //                     e.Value = Convert.ToDouble(e.Value).ToString("n" + Decimals);
        //                     if (FSLibrary.Functions.NumeroDecimal(e.Value) > m_MaxValue)
        //                     {
        //                         e.Value = m_MaxValue;
        //                     }
        //                     break;
        //                 case TypeData.Pecentage:
        //                     double percentageValue = Convert.ToDouble(e.Value);
        //                     percentageValue = percentageValue/100;
        //                     e.Value = percentageValue.ToString("P0");
        //                     break;
        //                 case TypeData.All:
        //                     e.Value = e.Value + "";
        //                     switch (m_Capitalize)
        //                     {
        //                         case TypeString.Uppercase:
        //                             e.Value = e.Value.ToString().ToUpper();
        //                             break;
        //                         case TypeString.Lowercase:
        //                             e.Value = e.Value.ToString().ToLower();
        //                             break;
        //                         case TypeString.Capitalize:
        //                             e.Value = FSLibrary.TextUtil.PCase(Convert.ToString(e.Value));
        //                             break;
        //                         case TypeString.Normal:
        //                             break;
        //                     }

        //                     break;
        //             }


        //             if (m_FormatString != "")
        //             {
        //                 switch (DataType)
        //                 {
        //                     case TypeData.All:
        //                         e.Value = string.Format("{0:" + m_FormatString + "}", e.Value);
        //                         break;
        //                     case TypeData.Date:
        //                     case TypeData.Time:
        //                         e.Value = string.Format("{0:" + m_FormatString + "}", Convert.ToDateTime(e.Value));
        //                         break;
        //                     case TypeData.Formula:
        //                     case TypeData.Money:
        //                     case TypeData.Numeric:
        //                     case TypeData.Pecentage:
        //                         e.Value = string.Format("{0:" + m_FormatString + "}", Functions.CDbl2(e.Value));
        //                         break;
        //                 }
        //             }

        //             if (m_MaskInput != "")
        //             {
        //                 SetTextMask(Convert.ToString(e.Value));
        //             }

        //             if (m_Encrypted)
        //             {
        //		Crypto crypt = new Crypto();
        //                 e.Value = crypt.Decryp(Convert.ToString(e.Value));
        //             }
        //         }
        //catch (System.Exception ex)
        //         {
        //             throw new ExceptionUtil(ex);
        //         }
        //     }


        //     protected void dbnParse(object sender, ConvertEventArgs e)
        //     {
        //         try
        //         {
        //             if (!(DataControl == null))
        //             {
        //                 DataControl.SetError(this, "");
        //             }

        //             switch (DataType)
        //             {
        //                 case TypeData.Date:
        //                     try
        //                     {
        //                         if (Convert.ToString(e.Value) != "")
        //                         {
        //                             e.Value = FSLibrary.DateTime.ShortDate(Convert.ToDateTime(e.Value));
        //                         }
        //                         else
        //                         {
        //                             e.Value = DBNull.Value;
        //                         }
        //                     }
        //		catch (System.Exception)
        //                     {
        //                         if (!(DataControl == null))
        //                         {
        //                             DataControl.SetError(this, "Formato de fecha incorrecto. Formato: dd/mm/aaaa");
        //                         }
        //                     }
        //                     break;
        //                 case TypeData.Time:
        //                     try
        //                     {
        //                         if (Convert.ToString(e.Value) != "")
        //                         {
        //                             e.Value = Convert.ToDateTime(e.Value).ToShortTimeString();
        //                         }
        //                         else
        //                         {
        //                             e.Value = DBNull.Value;
        //                         }
        //                     }
        //		catch (System.Exception)
        //                     {
        //                         if (!(DataControl == null))
        //                         {
        //                             DataControl.SetError(this, "Formato de hora incorrecto. Formato: hh:mm:ss");
        //                         }
        //                     }
        //                     break;
        //                 case TypeData.Money:
        //                     try
        //                     {
        //                         e.Value = Convert.ToDouble(e.Value);
        //                     }
        //		catch (System.Exception)
        //                     {
        //                         if (!(DataControl == null))
        //                         {
        //                             DataControl.SetError(this, "Valor Numérico incorrecto.");
        //                         }
        //                     }
        //                     break;
        //                 case TypeData.Numeric:
        //                     try
        //                     {
        //                         e.Value = Convert.ToDouble(e.Value);
        //                     }
        //		catch (System.Exception)
        //                     {
        //                         if (!(DataControl == null))
        //                         {
        //                             DataControl.SetError(this, "Valor Numérico incorrecto.");
        //                         }
        //                     }
        //                     break;
        //                 case TypeData.Pecentage:
        //                     try
        //                     {
        //                         e.Value = Convert.ToDouble(e.Value);
        //                     }
        //		catch (System.Exception)
        //                     {
        //                         if (!(DataControl == null))
        //                         {
        //                             DataControl.SetError(this, "Valor Numérico incorrecto.");
        //                         }
        //                     }
        //                     break;
        //                 case TypeData.All:
        //                     e.Value = e.Value + "";
        //                     break;
        //             }


        //             try
        //             {
        //                 if (m_Encrypted)
        //                 {
        //			Crypto crypt = new Crypto();
        //			e.Value = crypt.Crypt(Convert.ToString(e.Value));
        //                 }
        //             }
        //	catch (System.Exception ex)
        //             {
        //                 throw new ExceptionUtil(ex);
        //             }
        //         }
        //catch (System.Exception ex2)
        //         {
        //             throw new ExceptionUtil(ex2);
        //         }
        //     }

        private void cmdCalc_Click(object sender, EventArgs e)
        {
            var frmC = new frmCalc();
            if (textbox.PointToScreen(new Point(0, 0)).Y + cmdCalc.Height >
                Screen.PrimaryScreen.Bounds.Height - frmC.Height)
                frmC.Top = textbox.PointToScreen(new Point(0, 0)).Y - frmC.Height - 5;
            else
                frmC.Top = textbox.PointToScreen(new Point(0, 0)).Y + cmdCalc.Height;
            if (textbox.PointToScreen(new Point(0, 0)).X > Screen.PrimaryScreen.Bounds.Width - frmC.Width)
                frmC.Left = Screen.PrimaryScreen.Bounds.Width - frmC.Width;
            else
                frmC.Left = textbox.PointToScreen(new Point(0, 0)).X;
            frmC.DBTextbox = this;
            frmC.Show();
        }


        private void cmdAmpliar_Click(object sender, EventArgs e)
        {
            var frmC = new frmMemo();

            if (textbox.PointToScreen(new Point(0, 0)).Y + cmdAmpliar.Height >
                Screen.PrimaryScreen.Bounds.Height - frmC.Height)
                frmC.Top = textbox.PointToScreen(new Point(0, 0)).Y - frmC.Height - 5;
            else
                frmC.Top = textbox.PointToScreen(new Point(0, 0)).Y + cmdAmpliar.Height;

            if (textbox.PointToScreen(new Point(0, 0)).X > Screen.PrimaryScreen.Bounds.Width - frmC.Width)
                frmC.Left = Screen.PrimaryScreen.Bounds.Width - frmC.Width;
            else
                frmC.Left = textbox.PointToScreen(new Point(0, 0)).X;
            frmC.DBTextbox = this;
            frmC.Show();
        }

        private void cmdKeyboard_Click(object sender, EventArgs e)
        {
            var keyb = new frmKeyboard();
            keyb.TextBox = this;
            keyb.ShowDialog();
        }


        private void Text1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void Text1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region Delegates

        public delegate void EnterEventHandler(object sender, EventArgs e);

        public delegate void KeyDownEventHandler(object sender, KeyEventArgs e);

        public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);

        public delegate void KeyUpEventHandler(object sender, KeyEventArgs e);

        public delegate void LeaveEventHandler(object sender, EventArgs e);

        public delegate void LostFocusEventHandler(object sender, EventArgs e);

        public delegate void MouseEnterEventHandler(object sender, EventArgs e);

        public delegate void MouseEnterElementEventHandler(object sender, DBEditorButtonEventArgs e);

        public delegate void MaskChangedEventHandler(object sender, EventArgs e);

        #endregion

        #region Events

        public new event LostFocusEventHandler LostFocus;
        public new event KeyPressEventHandler KeyPress;
        public new event KeyDownEventHandler KeyDown;
        public new event KeyUpEventHandler KeyUp;
        public new event EventHandler Leave;
        public new event EventHandler MouseEnter;
        public new event EventHandler Enter;
        public event EventHandler ValueChanged;
        public event EventHandler AfterEnterEditMode;
        public event EventHandler AfterExitEditMode;
        public event DBEditorButtonEventHandler EditorButtonClick;
        public event MouseEnterElementEventHandler MouseEnterElement;
        public event MaskChangedEventHandler MaskChanged;

        #endregion

        #region Enums

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

        public enum TypeString
        {
            Normal,
            Uppercase,
            Lowercase,
            Capitalize
        }

        public enum NumericTypeEnum
        {
            Decimal,
            Double,
            Integer
        }

        #endregion

        #region TabNavigation

        public enum TabNavigationEnum
        {
            NextControl,
            NextSection
        }

        public enum SelectAllBehaviorEnum
        {
            SelectAllCharacters,
            SelectEnteredCharacters
        }

        public enum EditAsType
        {
            UseSpecifiedMask
        }

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        internal ToolTip ToolTip1;
        internal Button cmdCalc;
        internal TextBox textbox;
        internal Button cmdKeyboard;
        private IContainer components;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdAmpliar = new System.Windows.Forms.Button();
            this.cmdKeyboard = new System.Windows.Forms.Button();
            this.cmdCalc = new System.Windows.Forms.Button();
            this.textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdAmpliar
            // 
            this.cmdAmpliar.Location = new System.Drawing.Point(162, 0);
            this.cmdAmpliar.Name = "cmdAmpliar";
            this.cmdAmpliar.Size = new System.Drawing.Size(20, 20);
            this.cmdAmpliar.TabIndex = 4;
            this.cmdAmpliar.TabStop = false;
            this.cmdAmpliar.Text = "A";
            this.cmdAmpliar.Visible = false;
            // 
            // cmdKeyboard
            // 
            this.cmdKeyboard.Location = new System.Drawing.Point(208, 0);
            this.cmdKeyboard.Name = "cmdKeyboard";
            this.cmdKeyboard.Size = new System.Drawing.Size(20, 20);
            this.cmdKeyboard.TabIndex = 3;
            this.cmdKeyboard.TabStop = false;
            this.cmdKeyboard.Text = "T";
            this.cmdKeyboard.Visible = false;
            // 
            // cmdCalc
            // 
            this.cmdCalc.Location = new System.Drawing.Point(184, 0);
            this.cmdCalc.Name = "cmdCalc";
            this.cmdCalc.Size = new System.Drawing.Size(20, 20);
            this.cmdCalc.TabIndex = 2;
            this.cmdCalc.TabStop = false;
            this.cmdCalc.Text = "C";
            this.cmdCalc.Visible = false;
            // 
            // textbox
            // 
            this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textbox.Location = new System.Drawing.Point(0, 0);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(266, 20);
            this.textbox.TabIndex = 0;
            // 
            // DBTextBox
            // 
            this.Controls.Add(this.cmdAmpliar);
            this.Controls.Add(this.cmdKeyboard);
            this.Controls.Add(this.cmdCalc);
            this.Controls.Add(this.textbox);
            this.Name = "DBTextBox";
            this.Size = new System.Drawing.Size(266, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void BeginInit()
        {
            //((ISupportInitialize)textbox).BeginInit();
        }

        public void EndInit()
        {
            //((ISupportInitialize)textbox).EndInit();
        }

        #endregion
    }

    internal class DBTextBoxControlDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                if (((DBTextBox) Control).Multiline == false)
                    return SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable |
                           SelectionRules.Visible;
                return SelectionRules.AllSizeable | SelectionRules.Moveable | SelectionRules.Visible;
            }
        }
    }
}