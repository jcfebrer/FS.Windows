#region

using FSFormControls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class DBColumn : Component
    {
        public delegate void CellClickEventHandler(object sender, DataGridViewCellEventArgs e);

        public enum ColumnTypes
        {
            CheckColumn,
            TextColumn,
            MaskedColumn,
            DateColumn,
            ComboColumn,
            ButtonColumn,
            Button2Column,
            MoneyColumn,
            NumberColumn,
            DescriptionColumn,
            FormulaColumn,
            PercentColumn,
            ProgressColumn,
            TimeColumn,
            FileColumn,
            TimePickerColumn,
            AutoNumericColumn,
            PictureColumn
        }


        public enum DescriptionTypes
        {
            TextDescription,
            NumberDescription,
            MoneyDescription,
            DateDescription,
            CheckDescription
        }

        public enum LogicalOperatorEnum
        {
            Or,
            And
        }

        public enum OperationTypes
        {
            Sum,
            Max,
            Min,
            Average
        }

        public enum SortIndicatorEnum
        {
            Ascending,
            Descending
        }

        public DBColumn()
        {
        }

        public DBColumn(string strFieldDB, string strHeaderCaption)
        {
            FieldDB = strFieldDB;
            HeaderCaption = strHeaderCaption;
        }

        public DBColumn(string strFieldDB, string strHeaderCaption, ColumnTypes tColumnType)
        {
            FieldDB = strFieldDB;
            HeaderCaption = strHeaderCaption;
            ColumnType = tColumnType;
        }

        public DBColumn(string strFieldDB, string strHeaderCaption, DBControl dbcColumnDBControl)
        {
            FieldDB = strFieldDB;
            HeaderCaption = strHeaderCaption;
            ColumnDBControl = dbcColumnDBControl;
        }

        public DBColumn(string strFieldDB, string strHeaderCaption, bool bolHidden)
        {
            FieldDB = strFieldDB;
            HeaderCaption = strHeaderCaption;
            Hidden = bolHidden;
        }

        /// <summary>
        ///     Valor máximo
        /// </summary>
        public decimal MaxValue { get; set; } = decimal.MaxValue;

        /// <summary>
        ///     Valor mínimo
        /// </summary>
        public decimal MinValue { get; set; } = decimal.MinValue;

        /// <summary>
        ///     Permitir líneas múltiples
        /// </summary>
        public bool Multiline { get; set; } = false;

        /// <summary>
        /// Columna de sólo lectura (si/no)
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        ///     Permitir valores nulos
        /// </summary>
        public bool AllowNull { get; set; }

        /// <summary>
        ///     Valor a mostrar cuando el contenido sea Null
        /// </summary>
        public object NullValue { get; set; }

        /// <summary>
        /// Permiter cambiar el orden de las columnas
        /// </summary>
        //public int DisplayIndex { get; set; }

        /// <summary>
        ///     Tipo de letra
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        ///     Carácter prompt
        /// </summary>
        public char PromptChar { get; set; }

        /// <summary>
        ///     Lista de imágenes para un combo
        /// </summary>
        public ImageList ComboImageList { get; set; }

        /// <summary>
        ///     Columna encriptada
        /// </summary>
        public bool Encrypted { get; set; }

        /// <summary>
        ///     Permitir la selección de un valor en blanco (Combo)
        /// </summary>
        public bool ComboBlankSelection { get; set; } = true;

        /// <summary>
        ///     Color trasero de la columna
        /// </summary>
        public Color ColumnBackColor { get; set; } = Color.Empty;

        /// <summary>
        ///     Color de las letrar en la columna
        /// </summary>
        public Color ColumnForeColor { get; set; } = Color.Empty;

        /// <summary>
        ///     Nombre del campo para asociar al DataTable
        /// </summary>
        public string FieldDB { get; set; } = "";

        /// <summary>
        ///     Nombre alternativo del campo para asociar al DataTable
        /// </summary>
        public string Key
        {
            get { return FieldDB; }
            set { FieldDB = value; }
        }

        /// <summary>
        ///     Nombre alternativo del campo para asociar al DataTable
        /// </summary>
        public string Name
        {
            get { return FieldDB; }
            set { FieldDB = value; }
        }


        public string ColumnDBFieldData { get; set; } = "";

        /// <summary>
        ///     Título de la columna
        /// </summary>
        public string HeaderCaption { get; set; } = "";

        /// <summary>
        ///     DBControl asociado a la columna
        /// </summary>
        public DBControl ColumnDBControl { get; set; }

        /// <summary>
        ///     Tipo de columna
        /// </summary>
        public ColumnTypes ColumnType { get; set; } = ColumnTypes.TextColumn;

        /// <summary>
        ///     Campo que se mostrará en el combo
        /// </summary>
        public string ComboListField { get; set; } = "";

        /// <summary>
        ///     Valor por defecto
        /// </summary>
        public string DefaultValue { get; set; } = "";

        /// <summary>
        ///     Columna asociada al botón
        /// </summary>
        public int AsociatedButtonColumn { get; set; } = -1;

        /// <summary>
        ///     Columna asocidada al combo
        /// </summary>
        public int AsociatedComboColumn { get; set; } = -1;

        /// <summary>
        ///     Columna de solo lectura (si/no)
        /// </summary>
        public bool ReadColumn { get; set; }

        /// <summary>
        ///     Mostrar un formularío de selección
        /// </summary>
        public bool ShowSelectForm { get; set; } = true;

        /// <summary>
        ///     Columna oculta
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        ///     Longitud máxima
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        ///     Columna con valores únicos
        /// </summary>
        public bool Unique { get; set; }

        /// <summary>
        ///     Columna obligatoria
        /// </summary>
        public bool Obligatory { get; set; }

        /// <summary>
        ///     Ancho de la columna
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     Número de decimales
        /// </summary>
        public int Decimals { get; set; } = 0;

        /// <summary>
        ///     Alineación de los datos
        /// </summary>
        public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.Left;

        /// <summary>
        ///     Expresión o fórmula
        /// </summary>
        public string Expression { get; set; } = "";

        /// <summary>
        ///     Cadena de formato
        /// </summary>
        public string FormatString { get; set; }

        /// <summary>
        ///     Formato
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///     Tipo de descripción para la columna asociada
        /// </summary>
        public DescriptionTypes DescriptionType { get; set; } = DescriptionTypes.TextDescription;

        /// <summary>
        ///     Máscara de entrada
        /// </summary>
        public string MaskInput { get; set; }

        public bool ActiveColumnDBButtonOnReadMode { get; set; } = true;

        /// <summary>
        ///     Último valor de la columna
        /// </summary>
        public bool LastValue { get; set; }

        /// <summary>
        ///     Mostrar un tooltip en la columna
        /// </summary>
        public string ToolTip { get; set; } = "";

        public bool AllowRowFiltering { get; set; }

        public SortIndicatorEnum SortIndicator { get; set; }

        public LogicalOperatorEnum LogicalOperator { get; set; }

        public DBGridViewFilterCollection DBGridViewFilters { get; set; }

        public int Index { get; set; }

        public DBAppearance CellAppearance { get; set; }
        public DBAppearance HeaderAppearance { get; set; }
        public Size CellSizeResolved { get; set; }

        public event CellClickEventHandler CellClick;

        public void PerformClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CellClick != null)
                CellClick(sender, e);
        }
    }
}