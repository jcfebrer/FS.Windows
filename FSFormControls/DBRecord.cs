#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using FSException;
using FSGraphics;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBToolbar.bmp")]
    [ToolboxItem(true)]
    public class DBRecord : DBUserControl
    {
        #region t_date enum

        public enum t_date
        {
            Normal,
            Graphic
        }

        #endregion

        #region t_showmode enum

        public enum t_showmode
        {
            Vertical,
            Horizontal
        }

        #endregion

        private const int CONTROL_HEIGHT = 18;


        //private bool isInitialize = false;
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;

        public bool ShowToolBar
        {
            get { return DbToolBar1.Visible; }
            set
            {
                DbToolBar1.Visible = value;
                if (value == false)
                    panelRecord.Height = Height;
                else
                    panelRecord.Height = Height - DbToolBar1.Height;
            }
        }

        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                var f = 0;
                object ctr = null;
                m_Mode = value;
                for (f = 0; f <= panelRecord.Controls.Count - 1; f++)
                    ctr = panelRecord.Controls[f];
                // falta
                //ctr.mode = m_Mode; 
            }
        }


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// Asignamos el parent del dbcontrol cuando se user dl dbcontrol sin asignar a un formulario.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set
            {
                if (value != null && value.Parent is null)
                    value.Parent = this;

                m_DataControl = value;
            }
        }


        private int m_PosYLabel = 20;
        public int PosYLabel {
            get { return m_PosYLabel; }
            set { m_PosYLabel = value; }
        }

        private int m_PosXLabel = 20;
        public int PosXLabel
        {
            get { return m_PosXLabel; }
            set { m_PosXLabel = value; }
        }

        private int m_LabelYIncrement = 25;
        public int LabelYIncrement
        {
           get { return m_LabelYIncrement; }
           set { m_LabelYIncrement = value; }
        }

        public bool DoubleHeightInLargeText { get; set; }

        public t_showmode ShowMode { get; set; } = t_showmode.Vertical;

        public HorizontalAlignment LabelAlign { get; set; } = HorizontalAlignment.Left;

        public bool AutoSizeColumns { get; set; } = true;

        public t_date DateType { get; set; } = t_date.Normal;

        public bool TextBoxShadow { get; set; }

        public DBColumnCollection Columns { get; set; }

        public new BorderStyle BorderStyle
        {
            get { return panelRecord.BorderStyle; }
            set { panelRecord.BorderStyle = value; }
        }

        public bool AllowNavigate
        {
            get { return DbToolBar1.AllowNavigate; }
            set { DbToolBar1.AllowNavigate = value; }
        }

        public bool ShowComboEdit { get; set; } = false;

        public bool AllowSearch
        {
            get { return DbToolBar1.AllowSearch; }
            set { DbToolBar1.AllowSearch = value; }
        }

        public bool AllowCancel
        {
            get { return DbToolBar1.AllowCancel; }
            set { DbToolBar1.AllowCancel = value; }
        }

        public bool AllowSave
        {
            get { return DbToolBar1.AllowSave; }
            set { DbToolBar1.AllowSave = value; }
        }

        public bool AllowAddNew
        {
            get { return DbToolBar1.AllowAddNew; }
            set { DbToolBar1.AllowAddNew = value; }
        }

        public bool AllowPrint
        {
            get { return DbToolBar1.AllowPrint; }
            set { DbToolBar1.AllowPrint = value; }
        }

        public bool AllowFilter
        {
            get { return DbToolBar1.AllowFilter; }
            set { DbToolBar1.AllowFilter = value; }
        }

        public bool AllowRecord
        {
            get { return DbToolBar1.AllowRecord; }
            set { DbToolBar1.AllowRecord = value; }
        }

        public bool AllowEdit
        {
            get { return DbToolBar1.AllowEdit; }
            set { DbToolBar1.AllowEdit = value; }
        }

        public bool AllowDelete
        {
            get { return DbToolBar1.AllowDelete; }
            set { DbToolBar1.AllowDelete = value; }
        }

        public bool AllowList
        {
            get { return DbToolBar1.AllowList; }
            set { DbToolBar1.AllowList = value; }
        }

        public bool ShowNavigate
        {
            get { return DbToolBar1.ShowNavigateButton; }
            set { DbToolBar1.ShowNavigateButton = value; }
        }

        public bool ShowSearch
        {
            get { return DbToolBar1.ShowSearchButton; }
            set { DbToolBar1.ShowSearchButton = value; }
        }

        public bool ShowCancel
        {
            get { return DbToolBar1.ShowCancelButton; }
            set { DbToolBar1.ShowCancelButton = value; }
        }

        public bool ShowSave
        {
            get { return DbToolBar1.ShowSaveButton; }
            set { DbToolBar1.ShowSaveButton = value; }
        }

        public bool ShowClose
        {
            get { return DbToolBar1.ShowCloseButton; }
            set { DbToolBar1.ShowCloseButton = value; }
        }

        public bool ShowAddNew
        {
            get { return DbToolBar1.ShowAddNewButton; }
            set { DbToolBar1.ShowAddNewButton = value; }
        }

        public bool ShowPrint
        {
            get { return DbToolBar1.ShowPrintButton; }
            set { DbToolBar1.ShowPrintButton = value; }
        }

        public bool ShowFilter
        {
            get { return DbToolBar1.ShowFilterButton; }
            set { DbToolBar1.ShowFilterButton = value; }
        }

        public bool ShowRecord
        {
            get { return DbToolBar1.ShowRecordButton; }
            set { DbToolBar1.ShowRecordButton = value; }
        }

        public bool ShowScrollBar
        {
            get { return DbToolBar1.ShowScrollBar; }
            set { DbToolBar1.ShowScrollBar = value; }
        }

        public bool ShowEdit
        {
            get { return DbToolBar1.ShowEditButton; }
            set { DbToolBar1.ShowEditButton = value; }
        }

        public bool ShowDelete
        {
            get { return DbToolBar1.ShowDeleteButton; }
            set { DbToolBar1.ShowDeleteButton = value; }
        }

        public bool ShowList
        {
            get { return DbToolBar1.ShowListButton; }
            set { DbToolBar1.ShowListButton = value; }
        }

        public void Fill()
        {
            var posXlabel = m_PosXLabel;
            var posYlabel = m_PosYLabel;
            var f = 0;
            var posXData = 0;
            var posYData = 0;
            var lastWidth = -m_LabelYIncrement;
            var lastHeight = 0;

            FunctionsGrid.GenerateColumns(DataControl, Columns, 2, AutoSizeColumns, CreateGraphics(), Font);

            for (f = 0; f <= Columns.Count - 1; f++)
                try
                {
                    if (ShowMode == t_showmode.Horizontal)
                    {
                        posXlabel = posXlabel + lastWidth + m_LabelYIncrement;
                        posXData = posXlabel;
                        posYData = posYlabel + m_LabelYIncrement;

                        if (posXData + Columns[f].Width > Width)
                        {
                            posYlabel = posYlabel + 50;
                            posYData = posYlabel + m_LabelYIncrement;
                            posXlabel = m_PosXLabel;
                            posXData = posXlabel;
                        }
                    }
                    else
                    {
                        posYlabel = posYlabel + (f > 0 ? m_LabelYIncrement + lastHeight : 0);
                        posYData = posYlabel;
                        posXData = 200;
                        posXlabel = m_PosXLabel;
                    }

                    AddColumn(f, posXlabel, posXData, posYlabel, posYData);

                    if (DoubleHeightInLargeText && Columns[f].ColumnType == DBColumn.ColumnTypes.TextColumn &&
                        Columns[f].Width == Convert.ToInt32(Global.MAX_COLUMN_WIDTH / 1.5))
                        lastHeight = CONTROL_HEIGHT * 2;
                    else
                        lastHeight = 0;

                    lastWidth = Convert.ToInt32(Columns[f].Width);
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }

            DbToolBar1.DataControl = DataControl;
            DbToolBar1.Initialize();
        }


        //public void ResizeColumns()
        //{
        //    if (AutoSize)
        //    {
        //        FunctionsGrid.AutoSizeColumnsToContent(DataControl, Columns, CreateGraphics(), Font, null);
        //    }
        //    else
        //    {
        //        FunctionsGrid.ColumnsSetSize(Columns, DataControl, false);
        //    }
        //    Redraw();
        //}


        private void AddColumn(int col, int posXLabel, int posXData, int posYLabel, int posYData)
        {
            var labelCol = new DBLabel();
            var textCol = new DBTextBox();

            var selCol = Columns[col];

            if (selCol.HeaderCaption == "")
                selCol.HeaderCaption = TextUtil.PCase(selCol.FieldDB);

            labelCol.Font = selCol.Font;
            labelCol.AutoSize = true;
            labelCol.BackColor = selCol.ColumnBackColor;
            labelCol.ForeColor = selCol.ColumnForeColor;
            labelCol.Left = posXLabel;
            labelCol.Top = posYLabel;
            var header = selCol.HeaderCaption;

            if (header.Substring(header.Length - 1) != ":")
                labelCol.Text = selCol.HeaderCaption + ":";
            else
                labelCol.Text = selCol.HeaderCaption;

            labelCol.TabStop = false;
            if (ShowMode == t_showmode.Vertical)
            {
                labelCol.Width = 200 - 20;
            }
            else
            {
                if (CreateGraphics().MeasureString(labelCol.Text, labelCol.Font).Width < selCol.Width)
                    labelCol.Width = Convert.ToInt32(selCol.Width);
                else
                    labelCol.Width =
                        Convert.ToInt32(CreateGraphics().MeasureString(labelCol.Text, labelCol.Font).Width);
            }

            labelCol.TextAlign = GraphicsUtil.ConvertHorizontalAlignToContentAlign(LabelAlign);
            panelRecord.Controls.Add(labelCol);

            switch (selCol.ColumnType)
            {
                case DBColumn.ColumnTypes.CheckColumn:
                    var boolCol = new DBCheckBox();
                    boolCol.DataControl = DataControl;
                    boolCol.Mode = Mode;
                    boolCol.Font = selCol.Font;

                    boolCol.DBField = selCol.FieldDB;
                    boolCol.Width = Convert.ToInt32(selCol.Width);
                    boolCol.Left = posXData;
                    boolCol.Top = posYData - 7;
                    boolCol.Text = "";
                    panelRecord.Controls.Add(boolCol);
                    break;
                case DBColumn.ColumnTypes.FormulaColumn:
                    {
                        textCol.DataControl = DataControl;
                        textCol.BackColor = selCol.ColumnBackColor;
                        textCol.ForeColor = selCol.ColumnForeColor;
                        textCol.MaskInput = selCol.MaskInput;
                        textCol.Enabled = !selCol.ReadColumn;
                        textCol.Mode = Mode;
                        textCol.Font = selCol.Font;

                        DataControl.DataTable.Columns.Add("_" + selCol.FieldDB, Type.GetType("System.Decimal"),
                            selCol.Expression);
                        textCol.DBField = "_" + selCol.FieldDB;
                        textCol.Width = Convert.ToInt32(selCol.Width);
                        textCol.DataType = DBTextBox.TypeData.Formula;
                        textCol.Decimals = selCol.Decimals;
                        textCol.MaxLength = selCol.MaxLength;
                        textCol.DefaultValue = selCol.DefaultValue;
                        textCol.Left = posXData;
                        textCol.Top = posYData;
                        textCol.Shadow = TextBoxShadow;
                        textCol.MaskInput = selCol.MaskInput;
                        panelRecord.Controls.Add(textCol);
                    }
                    break;
                case DBColumn.ColumnTypes.TextColumn:
                    {
                        textCol.DataControl = DataControl;
                        textCol.BackColor = selCol.ColumnBackColor;
                        textCol.ForeColor = selCol.ColumnForeColor;
                        textCol.MaskInput = selCol.MaskInput;
                        textCol.Enabled = !selCol.ReadColumn;
                        textCol.Mode = Mode;
                        textCol.Font = selCol.Font;

                        textCol.DBField = selCol.FieldDB;
                        textCol.DataType = DBTextBox.TypeData.All;

                        if (selCol.Width == Global.MAX_COLUMN_WIDTH)
                        {
                            textCol.DataType = DBTextBox.TypeData.Memo;
                            textCol.Width = Convert.ToInt32(Global.MAX_COLUMN_WIDTH / 1.5);
                            textCol.Multiline = true;
                            textCol.ShowScrollBars = ScrollBars.Vertical;

                            if (DoubleHeightInLargeText) textCol.Height = textCol.Height * 2;
                            selCol.Width = textCol.Width;
                        }
                        else
                        {
                            textCol.Width = Convert.ToInt32(selCol.Width);
                        }

                        textCol.TextAlign = selCol.Alignment;
                        textCol.MaxLength = selCol.MaxLength;
                        textCol.DefaultValue = selCol.DefaultValue;
                        textCol.Obligatory = selCol.Obligatory;
                        textCol.Left = posXData;
                        textCol.Top = posYData;
                        textCol.Shadow = TextBoxShadow;
                        textCol.MaskInput = selCol.MaskInput;
                        panelRecord.Controls.Add(textCol);
                    }
                    break;
                case DBColumn.ColumnTypes.MoneyColumn:
                    {
                        textCol.DataControl = DataControl;
                        textCol.BackColor = selCol.ColumnBackColor;
                        textCol.ForeColor = selCol.ColumnForeColor;
                        textCol.MaskInput = selCol.MaskInput;
                        textCol.Enabled = !selCol.ReadColumn;
                        textCol.Mode = Mode;
                        textCol.Font = selCol.Font;

                        textCol.DBField = selCol.FieldDB;
                        textCol.Width = Convert.ToInt32(selCol.Width);
                        textCol.Decimals = selCol.Decimals;
                        textCol.DataType = DBTextBox.TypeData.Money;
                        textCol.TextAlign = HorizontalAlignment.Right;
                        textCol.MaxLength = selCol.MaxLength;
                        textCol.DefaultValue = selCol.DefaultValue;
                        textCol.Obligatory = selCol.Obligatory;
                        textCol.Left = posXData;
                        textCol.Top = posYData;
                        textCol.Shadow = TextBoxShadow;
                        textCol.MaskInput = selCol.MaskInput;
                        panelRecord.Controls.Add(textCol);
                    }
                    break;
                case DBColumn.ColumnTypes.DateColumn:
                    {
                        textCol.DataControl = DataControl;
                        textCol.BackColor = selCol.ColumnBackColor;
                        textCol.ForeColor = selCol.ColumnForeColor;
                        textCol.MaskInput = selCol.MaskInput;
                        textCol.Enabled = !selCol.ReadColumn;
                        textCol.Mode = Mode;
                        textCol.Font = selCol.Font;

                        if (DateType == t_date.Graphic)
                        {
                            var dateCol = new DBDate();
                            dateCol.DataControl = DataControl;
                            dateCol.Mode = Mode;

                            dateCol.DBField = selCol.FieldDB;
                            dateCol.Width = Convert.ToInt32(selCol.Width);
                            dateCol.Left = posXData;
                            dateCol.Top = posYData;
                            panelRecord.Controls.Add(dateCol);
                        }
                        else
                        {
                            textCol.DBField = selCol.FieldDB;
                            textCol.Width = Convert.ToInt32(selCol.Width);
                            textCol.DataType = DBTextBox.TypeData.Date;
                            textCol.TextAlign = selCol.Alignment;
                            textCol.MaxLength = selCol.MaxLength;
                            textCol.DefaultValue = selCol.DefaultValue;
                            textCol.Obligatory = selCol.Obligatory;
                            textCol.Left = posXData;
                            textCol.Top = posYData;
                            textCol.Shadow = TextBoxShadow;
                            textCol.MaskInput = selCol.MaskInput;
                            panelRecord.Controls.Add(textCol);
                        }
                    }
                    break;
                case DBColumn.ColumnTypes.TimeColumn:
                    textCol.DataControl = DataControl;
                    textCol.BackColor = selCol.ColumnBackColor;
                    textCol.ForeColor = selCol.ColumnForeColor;
                    textCol.MaskInput = selCol.MaskInput;
                    textCol.Enabled = !selCol.ReadColumn;
                    textCol.Mode = Mode;
                    textCol.Font = selCol.Font;

                    textCol.DBField = selCol.FieldDB;
                    textCol.Width = Convert.ToInt32(selCol.Width);
                    textCol.DataType = DBTextBox.TypeData.Time;
                    textCol.TextAlign = selCol.Alignment;
                    textCol.MaxLength = selCol.MaxLength;
                    textCol.DefaultValue = selCol.DefaultValue;
                    textCol.Obligatory = selCol.Obligatory;
                    textCol.Left = posXData;
                    textCol.Top = posYData;
                    textCol.Shadow = TextBoxShadow;
                    textCol.MaskInput = selCol.MaskInput;
                    panelRecord.Controls.Add(textCol);
                    break;
                case DBColumn.ColumnTypes.NumberColumn:
                    {
                        textCol.DataControl = DataControl;
                        textCol.BackColor = selCol.ColumnBackColor;
                        textCol.ForeColor = selCol.ColumnForeColor;
                        textCol.MaskInput = selCol.MaskInput;
                        textCol.Enabled = !selCol.ReadColumn;
                        textCol.Mode = Mode;
                        textCol.Font = selCol.Font;

                        textCol.DBField = selCol.FieldDB;
                        textCol.Width = Convert.ToInt32(selCol.Width);
                        textCol.Decimals = selCol.Decimals;
                        textCol.DataType = DBTextBox.TypeData.Numeric;
                        textCol.TextAlign = HorizontalAlignment.Right;
                        textCol.MaxLength = selCol.MaxLength;
                        textCol.DefaultValue = selCol.DefaultValue;
                        textCol.Obligatory = selCol.Obligatory;
                        textCol.Left = posXData;
                        textCol.Top = posYData;
                        textCol.Shadow = TextBoxShadow;
                        textCol.MaskInput = selCol.MaskInput;
                        panelRecord.Controls.Add(textCol);
                    }
                    break;
                case DBColumn.ColumnTypes.ComboColumn:
                    var comboCol = new DBCombo();
                    comboCol.DataControl = DataControl;
                    comboCol.Mode = Mode;
                    comboCol.Font = selCol.Font;

                    comboCol.DBField = selCol.FieldDB;
                    comboCol.Width = Convert.ToInt32(selCol.Width);
                    comboCol.DataControl = DataControl;
                    comboCol.DataControlList = selCol.ColumnDBControl;
                    comboCol.DBField = selCol.FieldDB;
                    comboCol.DBFieldList = selCol.ComboListField;
                    //comboCol.Editable = !(selCol.ReadColumn);
                    comboCol.DropDownStyle = ComboBoxStyle.DropDownList;

                    comboCol.ShowEdit = ShowComboEdit;

                    if (selCol.ColumnDBFieldData == "") selCol.ColumnDBFieldData = selCol.ColumnDBControl.FieldName(0);
                    comboCol.DBFieldData = selCol.ColumnDBFieldData;
                    comboCol.Obligatory = selCol.Obligatory;

                    comboCol.Left = posXData;
                    comboCol.Top = posYData;
                    panelRecord.Controls.Add(comboCol);

                    break;
                case DBColumn.ColumnTypes.ButtonColumn:
                    var buttonCol = new DBFindTextBox();
                    buttonCol.DataControl = DataControl;
                    buttonCol.Mode = Mode;
                    buttonCol.Font = selCol.Font;

                    if (selCol.ColumnDBControl == null)
                        throw new ExceptionUtil("Error! ColumnDBControl, no especificado.");

                    buttonCol.DBField = selCol.FieldDB;
                    buttonCol.Width = Convert.ToInt32(selCol.Width);
                    buttonCol.MaxLength = selCol.MaxLength;
                    buttonCol.DataControlList = selCol.ColumnDBControl;
                    buttonCol.DBFieldData = selCol.ComboListField;
                    buttonCol.Obligatory = selCol.Obligatory;
                    buttonCol.Left = posXData;
                    buttonCol.Top = posYData;
                    buttonCol.MaskInput = selCol.MaskInput;
                    panelRecord.Controls.Add(buttonCol);
                    break;
                case DBColumn.ColumnTypes.DescriptionColumn:
                    {
                        textCol.DataControl = DataControl;
                        textCol.BackColor = selCol.ColumnBackColor;
                        textCol.ForeColor = selCol.ColumnForeColor;
                        textCol.MaskInput = selCol.MaskInput;
                        textCol.Enabled = !selCol.ReadColumn;
                        textCol.Mode = Mode;
                        textCol.Font = selCol.Font;

                        if (!DataControl.DataTable.Columns.Contains(selCol.FieldDB))
                            DataControl.DataTable.Columns.Add(selCol.FieldDB);

                        textCol.DBField = selCol.FieldDB;
                        textCol.Width = Convert.ToInt32(selCol.Width);
                        textCol.Editable = false;
                        textCol.TextAlign = selCol.Alignment;
                        textCol.MaxLength = selCol.MaxLength;
                        textCol.DefaultValue = selCol.DefaultValue;
                        textCol.Obligatory = selCol.Obligatory;
                        textCol.Left = posXData;
                        textCol.Top = posYData;
                        textCol.Shadow = TextBoxShadow;
                        textCol.MaskInput = selCol.MaskInput;
                        panelRecord.Controls.Add(textCol);
                    }
                    break;
            }
            //isInitialize = true; 
        }

        public void Redraw()
        {
            var f = 0;
            object ctr = null;

            for (f = 0; f <= Columns.Count - 1; f++)
            {
                try
                {
                    ctr = FunctionsForms.GetControlByDBField(panelRecord.Controls, Columns[f].FieldDB);
                    if (ctr != null) 
                        ((UserControl)ctr).Width = Columns[f].Width;
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }
            }
        }


        public DBTextBox get_GetDBTextBoxByDBField(string DBField)
        {
            foreach (Control ctr in panelRecord.Controls)
                if (ctr is DBTextBox)
                    if (((DBTextBox) ctr).DBField.ToLower() == DBField.ToLower())
                        return (DBTextBox) ctr;
            return null;
        }


        private void panelRecord_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void panelRecord_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region '" Windows Form Designer generated code "' 

        private readonly IContainer components = null;

        internal DBToolBarEx DbToolBar1;
        internal Panel panelRecord;

        public DBRecord()
        {
            InitializeComponent();

            if (Columns == null) Columns = new DBColumnCollection();

            panelRecord.MouseDown += panelRecord_MouseDown;
            panelRecord.MouseUp += panelRecord_MouseUp;
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
            this.panelRecord = new System.Windows.Forms.Panel();
            this.DbToolBar1 = new FSFormControls.DBToolBarEx();
            this.SuspendLayout();
            // 
            // panelRecord
            // 
            this.panelRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRecord.AutoScroll = true;
            this.panelRecord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRecord.Location = new System.Drawing.Point(0, 0);
            this.panelRecord.Name = "panelRecord";
            this.panelRecord.Size = new System.Drawing.Size(897, 365);
            this.panelRecord.TabIndex = 2;
            // 
            // DbToolBar1
            // 
            this.DbToolBar1.About = "";
            this.DbToolBar1.AllowAddNew = true;
            this.DbToolBar1.AllowCancel = true;
            this.DbToolBar1.AllowClose = true;
            this.DbToolBar1.AllowDelete = true;
            this.DbToolBar1.AllowEdit = true;
            this.DbToolBar1.AllowFilter = true;
            this.DbToolBar1.AllowList = true;
            this.DbToolBar1.AllowNavigate = true;
            this.DbToolBar1.AllowPrint = true;
            this.DbToolBar1.AllowRecord = true;
            this.DbToolBar1.AllowSave = true;
            this.DbToolBar1.AllowSearch = true;
            this.DbToolBar1.DataControl = null;
            this.DbToolBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DbToolBar1.Location = new System.Drawing.Point(0, 251);
            this.DbToolBar1.Name = "DbToolBar1";
            this.DbToolBar1.ShowAddNewButton = true;
            this.DbToolBar1.ShowCancelButton = true;
            this.DbToolBar1.ShowCloseButton = true;
            this.DbToolBar1.ShowDeleteButton = true;
            this.DbToolBar1.ShowEditButton = true;
            this.DbToolBar1.ShowFilterButton = true;
            this.DbToolBar1.ShowListButton = true;
            this.DbToolBar1.ShowNavigateButton = true;
            this.DbToolBar1.ShowPrintButton = true;
            this.DbToolBar1.ShowRecordButton = true;
            this.DbToolBar1.ShowSaveButton = true;
            this.DbToolBar1.ShowScrollBar = true;
            this.DbToolBar1.ShowSearchButton = true;
            this.DbToolBar1.ShowText = true;
            this.DbToolBar1.Size = new System.Drawing.Size(1020, 138);
            this.DbToolBar1.TabIndex = 3;
            this.DbToolBar1.TabStop = false;
            this.DbToolBar1.Value = 0;
            this.DbToolBar1.VisibleScroll = true;
            this.DbToolBar1.VisibleTotalRecord = true;
            // 
            // DBRecord
            // 
            this.Controls.Add(this.DbToolBar1);
            this.Controls.Add(this.panelRecord);
            this.Name = "DBRecord";
            this.Size = new System.Drawing.Size(897, 389);
            this.ResumeLayout(false);

        }

        #endregion
    }
}