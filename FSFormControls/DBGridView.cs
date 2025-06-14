#region

using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FSDatabase;
using FSException;
using FSFormControls.Properties;
using FSLibrary;
using FSExcel;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBGridView.bmp")]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    public class DBGridView : DBUserControl, ISupportInitialize
    {
        private readonly int m_columnMove = -1;
        private readonly List<int> m_rowCurrent = new List<int>();
        private readonly int m_rowDefaultDivider = 0;
        private readonly int m_rowDefaultHeight = 22;
        private readonly int m_rowExpandedDivider = 300 - 22;
        private readonly int m_rowExpandedHeight = 300;
        private Button cmdPageFirst;
        private Button cmdPageLast;
        private Button cmdPageNext;
        private Button cmdPagePrevious;
        private DataGridView dataGridViewTotal;
        private DBRecord DbRecord1;
        private DataGridView datagrid;

        public bool FilledRecord;

        private ImageList imageList_DragDrop = new ImageList();
        private Label lblPage;
        private Color m_alternatingColor;
        private string m_captionText;
        private DBGridView m_childView;
        private bool m_collapseRow;
        private DBControl m_DBControl;
        private Font m_DefaultHeaderFont;
        private int m_LastRowClicked = -1;
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;
        private int m_RowsInCaption = 2;
        private bool m_ShowTotals = false;
        internal PictureBox picRefrescar;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private DBSummarieCollection m_Summaries = new DBSummarieCollection();

        public string RowDraw = "";
        // end variables


        #region Delegates

        public delegate void ColumnChangedEventHandler(object sender, DataColumnChangeEventArgs e);
        public delegate void ColumnChangingEventHandler(object sender, DataColumnChangeEventArgs e);
        public delegate void CurrentCellChangedEventHandler(object sender, EventArgs e);
        public delegate void DoubleClickEventHandler(object sender, EventArgs e);
        public delegate void RowChangedEventHandler(object sender, DataRowChangeEventArgs e);
        public delegate void CellClickEventHandler(object sender, DataGridViewCellEventArgs e);
        public delegate void RowChangingEventHandler(object sender, DataRowChangeEventArgs e);
        public delegate void DataErrorEventHandler(object sender, DataGridViewDataErrorEventArgs e);
        public delegate void ErrorEventHandler(object sender, EventArgs e);
        public delegate void CellValueChangedEventHandler(object sender, DataGridViewCellEventArgs e);
        public delegate void RowsAddedEventHandler(object sender, DataGridViewRowsAddedEventArgs e);
        public delegate void CellBeginEditEventHandler(object sender, DataGridViewCellCancelEventArgs e);
        public delegate void CellEndEditEventHandler(object sender, DataGridViewCellEventArgs e);

        //public delegate void InitializeRowEventHandler(object sender, EventArgs e);
        public delegate void InitializePrintPreviewEventHandler(object sender, EventArgs e);
        public delegate void InitializePrintEventHandler(object sender, EventArgs e);
        public delegate void BeforePrintEventHandler(object sender, EventArgs e);
        public delegate void DoubleClickRowEventHandler(object sender, DataGridViewCellEventArgs e);
        public delegate void BeforeSortChangeEventHandler(object sender, EventArgs e);
        public delegate void BandEventHandler(object sender, EventArgs e);
        public delegate void InitializeLayoutEventHandler(object sender, EventArgs e);

        #endregion

        #region Events

        public event RowChangedEventHandler RowChanged;
        public event ColumnChangedEventHandler ColumnChanged;
        public event RowChangingEventHandler RowChanging;
        public event ColumnChangingEventHandler ColumnChanging;
        public event CellClickEventHandler CellClick;
        public event CellClickEventHandler CellDoubleClick;
        public event DataErrorEventHandler DataError;
        public event ErrorEventHandler Error;
        public event CellValueChangedEventHandler CellValueChanged;
        public event DataGridViewCellFormattingEventHandler CellFormatting;
        public event DataGridViewCellMouseEventHandler ColumnHeaderMouseClick;
        public event RowsAddedEventHandler RowsAdded;
        public event CellEndEditEventHandler CellEndEdit;
        public event CellBeginEditEventHandler CellBeginEdit;
        public event DataGridViewRowEventHandler InitializeRow;
        public event EventHandler SelectionChanged;
        public event EventHandler AfterRowActivate;
        public event DataGridViewRowStateChangedEventHandler RowStateChanged;
        public event EventHandler RowEnter;
        public event DataGridViewRowCancelEventHandler UserDeletingRow;
        public event DataGridViewRowEventHandler UserAddedRow;
        public event DataGridViewRowEventHandler UserDeletedRow;

        //INFRAGISTICS
        //public event InitializeRowEventHandler InitializeRow;
        //public event InitializePrintPreviewEventHandler InitializePrintPreview;
        //public event InitializePrintEventHandler InitializePrint;
        //public event BeforePrintEventHandler BeforePrint;
        public event InitializeLayoutEventHandler InitializeLayout;
        public event DoubleClickRowEventHandler DoubleClickRow;
        public event BeforeSortChangeEventHandler BeforeSortChange;
        public event BandEventHandler AfterSortChange;

        #endregion

        public DBGridView()
        {
            InitializeComponent();

            //Deshabilitar la generación de columnas
            datagrid.AutoGenerateColumns = false;
            dataGridViewTotal.AutoGenerateColumns = false;

            datagrid.AllowUserToAddRows = true;
            dataGridViewTotal.AllowUserToAddRows = false;

            datagrid.MultiSelect = false;
            datagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Filas alternativas con diferente color
            datagrid.RowsDefaultCellStyle.BackColor = Color.White;
            datagrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;

            dataGridViewTotal.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridViewTotal.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;
            dataGridViewTotal.MouseClick += DataGridViewTotal_MouseClick;

            //Eventos
            datagrid.CellBeginEdit += DataGridView1_CellBeginEdit;
            datagrid.RowValidated += DataGridView1_RowValidated;
            datagrid.CellEndEdit += DataGridView1_CellEndEdit;
            datagrid.CellValidating += DataGridView1_CellValidating;
            datagrid.CellClick += DataGridView1_CellClick;
            datagrid.DataError += DataGridView1_DataError;
            datagrid.Leave += DataGridView1_Leave;
            datagrid.CellValueChanged += DataGridView1_CellValueChanged;
            datagrid.RowsAdded += DataGridView1_RowsAdded;
            datagrid.CellDoubleClick += DataGridView1_CellDoubleClick;
            datagrid.DefaultValuesNeeded += DataGridView1_DefaultValuesNeeded;
            datagrid.UserDeletingRow += DataGridView1_UserDeletingRow;
            datagrid.CellFormatting += DataGridView1_CellFormatting;
            datagrid.ColumnHeaderMouseClick += Datagrid_ColumnHeaderMouseClick;
            datagrid.RowEnter += DataGridView1_RowEnter;
            datagrid.RowStateChanged += DataGridView1_RowStateChanged;
            datagrid.MouseClick += Datagrid_MouseClick;
            datagrid.UserAddedRow += DataGridView1_UserAddedRow;
            datagrid.UserDeletedRow += DataGridView1_UserDeletedRow;

            if (Columns == null)
                Columns = new DBColumnCollection();

            // Propiedades y eventos control expand/collapse
            datagrid.Scroll += DataGridView1_Scroll;
            datagrid.RowPostPaint += DataGridView1_RowPostPaint;
            datagrid.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;
            datagrid.SelectionChanged += DataGridView1_SelectionChanged;

            // speedup paint
            datagrid.RowHeadersVisible = true;
            datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            DoubleBuffered = true;
            datagrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        private void DataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if(UserAddedRow != null)
                UserAddedRow(sender, e);
        }

        private void DataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if(UserDeletedRow != null)
                UserDeletedRow(sender, e);
        }

        private void Datagrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ColumnHeaderMouseClick != null)
                ColumnHeaderMouseClick(sender, e);
        }

        private void DataGridViewTotal_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var cmTotal = new ContextMenuStrip();

                cmTotal.Items.Add("&Suma", null, MnuSum);
                cmTotal.Items.Add("&Máximo", null, MnuMax);
                cmTotal.Items.Add("&Mínimo", null, MnuMin);
                cmTotal.Items.Add("&Promedio", null, MnuAverage);

                int currentMouseOverRow = dataGridViewTotal.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    //cmTotal.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                cmTotal.Show(dataGridViewTotal, new Point(e.X, e.Y));
            }
        }

        private void Datagrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var cm = new ContextMenuStrip();

                cm.Items.Add("&Vista preliminar", null, MnuPrintPreview);
                cm.Items.Add("&Imprimir", null, MnuPrint);
                cm.Items.Add("&Exportar a Excel", null, MnuExcelExport);
                cm.Items.Add("-");
                cm.Items.Add("&Ajustar tamaño", null, MnuAutoAdjust);
                cm.Items.Add("&Modo Registro", null, ModeRecord);
                cm.Items.Add("&Refrescar", null, MnuRefrescar);
                cm.Items.Add("-");
                cm.Items.Add("&Copiar registro", null, MnuCopyOneReg);
                cm.Items.Add("Copiar &seleccionados", null, MnuCopySelectedReg);
                cm.Items.Add("Copiar &todos", null, MnuCopyAllReg);
                cm.Items.Add("-");
                cm.Items.Add("&Filtro", null, MnuFilter);
                cm.Items.Add("&Quitar filtro", null, MnuDelFilter);
                cm.Items.Add("-");
                cm.Items.Add("&Buscar", null, MnuFind);
                cm.Items.Add("&Buscar siguiente", null, MnuFindNext);
                cm.Items.Add("-");
                cm.Items.Add("&Editar Columnas", null, MnuColumnEdit);
                cm.Items.Add("&Seleccionar todas", null, MnuSelect);
                cm.Items.Add("-");
                cm.Items.Add("&Totales", null, MnuShowTotals);

                int currentMouseOverRow = datagrid.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    //cm.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                cm.Show(datagrid, new Point(e.X, e.Y));
            }
        }


        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DBControl; }
            set
            {
                m_DBControl = value;

                //Borramos las clumans si estuvieran definidas
                datagrid.Columns.Clear();
                Columns.Clear();

                if (value != null)
                    Fill();
            }
        }


        private string m_DBField;
        [Description("Campo de la base de datos a enlazar.")]
        public string DBField
        {
            get { return m_DBField; }
            set { m_DBField = value; }
        }

        public DBColumnCollection Columns { get; set; } = new DBColumnCollection();

        public DataGridViewColumnCollection ColumnsGrid => datagrid.Columns;

        public int RowHeadersWidth
        {
            get { return datagrid.RowHeadersWidth; }
            set { datagrid.RowHeadersWidth = value; }
        }

        public DataGridView dataGridView { get { return datagrid; } }

        public int CurrentRowIndex => datagrid.CurrentRow.Index;

        public bool AutoSave { get; set; } = true;

        public bool MultiSelect
        {
            get { return datagrid.MultiSelect; }
            set { datagrid.MultiSelect = value; }
        }

        public bool ShowExpand { get; set; }

        public DBGridViewRowCollection Rows => (DBGridViewRowCollection)datagrid.Rows;

        public Color AlternatingColor
        {
            get { return m_alternatingColor; }
            set
            {
                if (value.A != 0)
                {
                    var cs = new DataGridViewCellStyle();
                    m_alternatingColor = value;
                    cs.BackColor = m_alternatingColor;
                    datagrid.AlternatingRowsDefaultCellStyle = cs;
                }
            }
        }

        public string CaptionText
        {
            get { return m_captionText; }
            set
            {
                m_captionText = value;
                if (datagrid.Columns.Count > 0) datagrid.Columns[0].HeaderText = m_captionText;
            }
        }

        public bool ShowRecordScrollBar { get; set; } = true;

        //[Description("DataBindings.")] public new ControlBindingsCollection DataBindings => datagrid.DataBindings;

        public bool AllowAddNew { get; set; } = true;

        public bool AllowDelete { get; set; } = true;

        public bool Editable { get; set; } = true;

        public int DefaultDecimals { get; set; } = 2;

        public int RowsInCaption
        {
            get { return m_RowsInCaption; }
            set
            {
                if (value <= 0) return;
                m_RowsInCaption = value;
            }
        }

        public DBColumn.OperationTypes TotalOperation { get; set; } = DBColumn.OperationTypes.Sum;

        public Font DefaultHeaderFont
        {
            get
            {
                if (m_DefaultHeaderFont == null)
                    m_DefaultHeaderFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point,
                        Convert.ToByte(0));
                return m_DefaultHeaderFont;
            }
            set { m_DefaultHeaderFont = value; }
        }

        public bool RecordMode { get; set; }

        public bool AutoSizeColumns { get; set; }

        public DataGridViewSelectionMode SelectionMode
        {
            get { return datagrid.SelectionMode; }
            set { datagrid.SelectionMode = value; }
        }

        public DataGridViewCellStyle ColumnHeadersDefaultCellStyle
        {
            get { return datagrid.ColumnHeadersDefaultCellStyle; }
            set { datagrid.ColumnHeadersDefaultCellStyle = value; }
        }

        public DataGridViewCellStyle DefaultCellStyle
        {
            get { return datagrid.DefaultCellStyle; }
            set { datagrid.DefaultCellStyle = value; }
        }

        public bool EnableHeadersVisualStyles
        {
            get { return datagrid.EnableHeadersVisualStyles; }
            set { datagrid.EnableHeadersVisualStyles = value; }
        }

        public bool AutoGenerateColumns
        {
            get { return datagrid.AutoGenerateColumns; }
            set { datagrid.AutoGenerateColumns = value; }
        }

        public SortOrder SortOrder
        {
            get { return datagrid.SortOrder; }
        }

        public DataGridViewColumn SortedColumn
        {
            get { return datagrid.SortedColumn; }
        }

        public void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        {
            datagrid.Sort(dataGridViewColumn, direction);
        }

        public void Sort(IComparer comparer)
        {
            datagrid.Sort(comparer);
        }

        public DBGridViewRow ActiveRow
        {
            get
            {
                if (datagrid.SelectedRows.Count > 0)
                    return (DBGridViewRow)datagrid.SelectedRows[0];
                return (DBGridViewRow)datagrid.CurrentRow;
            }
            set
            {
                if (value != null)
                    value.Selected = true;
            }
        }

        public DBGridViewDisplayLayout DisplayLayout { get; set; } = new DBGridViewDisplayLayout();

        //private DBGridViewBandCollection m_Bands = new DBGridViewBandCollection();
        public List<DBGridView> Bands
        {
            get
            {
                var arrGrd = new List<DBGridView>();

                arrGrd.Add(this);

                // Si el DataSource es un DataSet, este asu vez puede contener más DataTables.
                if (m_childView != null)
                {
                    arrGrd.Add(m_childView);
                    if (m_childView.m_childView != null)
                    {
                        arrGrd.Add(m_childView.m_childView);
                        if (m_childView.m_childView.m_childView != null)
                            arrGrd.Add(m_childView.m_childView.m_childView);
                    }
                }

                return arrGrd;
            }
        }

        public DBColumnCollection SortedColumns { get; set; }

        public bool ShowTotals
        {
            get { return m_ShowTotals; }
            set
            {
                m_ShowTotals = value;
                dataGridViewTotal.Visible = value;
                splitContainer1.Panel2Collapsed = !value;
                Resize();
            }
        }

        public object DataSource
        {
            get { return datagrid.DataSource; }
            set
            {
                //if(DataControl == null)
                //    DataControl = new DBControl(value);

                if (DataControl == null && ColumnsGrid.Count == 0 && !AutoGenerateColumns)
                {
                    AutoGenerateColumns = true;
                }
                
                datagrid.DataSource = value;
                //if (value != null)
                //{
                //    BindingSource bindingSource = new BindingSource();
                //    datagrid.DataSource = bindingSource;
                //    bindingSource.DataSource = value;
                //}

                if(Columns.Count == 0 && AutoGenerateColumns)
                    Columns = FunctionsForms.GenerateColumns(ColumnsGrid);
            }
        }

        public Global.AccessMode Mode
        {
            get
            {
                Global.AccessMode modeReturn = 0;
                modeReturn = m_Mode;
                return modeReturn;
            }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        if (DataControl != null)
                            if (DataControl.DataTable != null)
                            {
                                DataControl.DataTable.DefaultView.AllowEdit = false;
                                DataControl.DataTable.DefaultView.AllowDelete = AllowDelete;
                                DataControl.DataTable.DefaultView.AllowNew = false;
                            }

                        if (DbRecord1 != null) DbRecord1.Mode = Global.AccessMode.ReadMode;
                        break;
                    case Global.AccessMode.WriteMode:
                        if (Editable)
                        {
                            if (DataControl != null)
                                if (DataControl.DataTable != null)
                                {
                                    DataControl.DataTable.DefaultView.AllowEdit = true;
                                    DataControl.DataTable.DefaultView.AllowDelete = AllowDelete;
                                    DataControl.DataTable.DefaultView.AllowNew = AllowAddNew;
                                }

                            datagrid.ReadOnly = false;
                        }

                        if (DbRecord1 != null) DbRecord1.Mode = Global.AccessMode.WriteMode;
                        break;
                    case Global.AccessMode.ProtectedMode:
                        if (DataControl != null)
                            if (DataControl.DataTable != null)
                            {
                                DataControl.DataTable.DefaultView.AllowEdit = false;
                                DataControl.DataTable.DefaultView.AllowDelete = AllowDelete;
                                DataControl.DataTable.DefaultView.AllowNew = AllowAddNew;
                            }

                        if (DbRecord1 != null) DbRecord1.Mode = Global.AccessMode.ProtectedMode;
                        break;
                }


                ModeControls(m_Mode);
            }
        }

        public DBGridViewCell ActiveCell
        {
            get { return (DBGridViewCell)datagrid.CurrentCell; }
            set { datagrid.CurrentCell = value; }
        }

        public bool ReadOnly { 
            get { return datagrid.ReadOnly; } 
            set { datagrid.ReadOnly = value; }
        }

        public bool AllowUserToAddRows {
            get { return datagrid.AllowUserToAddRows; }
            set { datagrid.AllowUserToAddRows = value; }
        }

        public bool AllowUserToDeleteRows
        {
            get { return datagrid.AllowUserToDeleteRows; }
            set { datagrid.AllowUserToDeleteRows = value; }
        }

        public bool AllowUserToOrderColumns
        {
            get { return datagrid.AllowUserToOrderColumns; }
            set { datagrid.AllowUserToOrderColumns = value; }
        }

        public int FirstDisplayedScrollingRowIndex 
        { 
            get { return datagrid.FirstDisplayedScrollingRowIndex; }
            set { 
                if(value != -1)
                    datagrid.FirstDisplayedScrollingRowIndex = value; 
            } 
        }

        public DataGridViewRow RowTemplate 
        { 
            get { return datagrid.RowTemplate; } 
            set { datagrid.RowTemplate = value; }
        }

        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode {
            get { return datagrid.ColumnHeadersHeightSizeMode; } 
            set { datagrid.ColumnHeadersHeightSizeMode = value; } 
        }

        public DataGridViewSelectedRowCollection SelectedRows 
        { 
            get { return datagrid.SelectedRows; }
        }

        public DataGridViewAutoSizeRowsMode AutoSizeRowsMode 
        { 
            get { return datagrid.AutoSizeRowsMode; }
            set { datagrid.AutoSizeRowsMode = value; }
        }

        public DBSummarieCollection Summaries {
            get { return m_Summaries; }
            set { m_Summaries = value; }
        }

        public enum UpdateModeEnum
        {
            OnCellChange,
            OnRowChange,
            OnRowLeave,
            OnRowValidated,
            OnRowEnter,
            OnRowStateChanged
        }

        //Infragistics

        public enum SummaryDisplayAreas
        {
            Default,
            None,
            Top,
            TopFixed,
            Bottom,
            BottomFixed,
            InGroupByRows,
            GroupByRowsFooter,
            HideDataRowFooters,
            RootRowsFootersOnly
        }

        public DBAppearance ActiveCellAppearance { get; set; }
        public DBAppearance EditCellAppearance { get; set; }
        public DBAppearance ActiveRowAppearance { get; set; }
        public DBAppearance CardAreaAppearance { get; set; }
        public DBAppearance CellAppearance { get; set; }
        public DBAppearance GroupByRowAppearance { get; set; }
        public DBAppearance HeaderAppearance { get; set; }
        public DBAppearance RowAppearance { get; set; }
        public DBAppearance TemplateAddRowAppearance { get; set; }
        public BorderStyle BorderStyleCell { get; set; }
        public BorderStyle BorderStyleRow { get; set; }
        public DBGridViewDisplayLayout.DBCellClickAction CellClickAction { get; set; }
        public DBGridViewDisplayLayout.DBHeaderClickAction HeaderClickAction { get; set; }
        public bool RowSelectors { get; set; }
        public DBGridViewDisplayLayout.DBHeaderStyle HeaderStyle { get; set; }
        public int CellPadding { get; set; }
        public DBGridViewDisplayLayout.DBAllowColMoving AllowColMoving { get; set; }
        public DBGridViewDisplayLayout.DBAllowColSwapping AllowColSwapping { get; set; }
        public DBGridViewDisplayLayout.DBRowSizing RowSizing { get; set; }
        public DBGridViewDisplayLayout.SelectType SelectTypeRow { get; set; }
        public int MinRowHeight { get; set; }
        public int RowSelectorWidth { get; set; }
        public SummaryDisplayAreas SummaryDisplayArea { get; set; }
        public bool SummaryFooterCaptionVisible { get; set; }
        public int SummaryFooterSpacingAfter { get; set; }
        public UpdateModeEnum UpdateMode { get; set; }

        public object CurrentCell {
            get { return datagrid.CurrentCell; }
            set { datagrid.CurrentCell = (DataGridViewCell)value; }
        }

        public int SummaryFooterSpacingBefore { get; set; }
        public object AllowRowSummaries { get; set; }

        public bool Hidden
        {
            get { return !datagrid.Visible; }
            set { datagrid.Visible = !value; }
        }

        public bool AllowUpdate
        {
            get { return !datagrid.ReadOnly; }
            set { datagrid.ReadOnly = !value; }
        }

        //End Infragistics

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        private void DataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (RowStateChanged != null)
                RowStateChanged(sender, e);
        }

        private void DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (RowEnter != null)
                RowEnter(sender, e);
        }

        private void DrawCounterOnRowHeader(DataGridView grid, DataGridViewRowPostPaintEventArgs e)
        {
            // Ponemos un numero de row en la parte izquierda del DataGridView
            var rowIdx = (e.RowIndex + 1).ToString();

            var font = new Font(FontFamily.GenericMonospace, 6);

            var centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Far;
            centerFormat.LineAlignment = StringAlignment.Center;
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth,
                e.RowBounds.Height - grid.Rows[e.RowIndex].DividerHeight);
            e.Graphics.DrawString(rowIdx, font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(DataControl != null && datagrid.CurrentRow != null)
                DataControl.Go(datagrid.CurrentRow.Index);

            if (ShowExpand)
            {
                SetExpand();
            }

            if (SelectionChanged != null)
                SelectionChanged(sender, e);

            if (AfterRowActivate != null)
                AfterRowActivate(sender, e);
        }

        private void SetExpand()
        {
            if (DataControl == null)
                return;

            // seleccionamos los elementos del grid de detalle en función del row seleccionado
            if (datagrid.CurrentRow == null)
                return;

            var formatFilter = "{0}='{1}'";
            var column = 0;

            if (Columns[column].ColumnType == DBColumn.ColumnTypes.CheckColumn)
                column = 1;

            if (DataControl.DataSet.Tables[0].Columns[column].DataType == typeof(int)
                || DataControl.DataSet.Tables[0].Columns[column].DataType == typeof(double)
                || DataControl.DataSet.Tables[0].Columns[column].DataType == typeof(decimal))
                formatFilter = "{0}={1}";

            if (datagrid.RowCount != 0)
                // Establecemos el filtro al grid de detalle
                ((DataView)m_childView.DataSource).RowFilter = string.Format(formatFilter, Columns[column].FieldDB,
                    datagrid.Rows[datagrid.CurrentRow.Index].Cells[0].Value);
        }

        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (ShowExpand)
            {
                if (m_rowCurrent.Count != 0)
                {
                    m_collapseRow = true;
                    datagrid.ClearSelection();
                    datagrid.Rows[m_rowCurrent[0]].Selected = true;
                }
            }
        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (ShowExpand)
            {
                DrawExpand(grid, e.Graphics, e.RowBounds, e.RowIndex);
            }

            DrawCounterOnRowHeader(grid, e);
        }

        private void DrawExpand(DataGridView grid, Graphics graphics, Rectangle rowBounds, int rowIndex)
        {
            var rect = new Rectangle(rowBounds.X
                                         + (m_rowDefaultHeight - 16)
                                         / 2, rowBounds.Y
                                              + (m_rowDefaultHeight - 16)
                                              / 2, 16, 16);

            if (m_collapseRow)
            {
                if (m_rowCurrent.Contains(rowIndex))
                {
                    grid.Rows[rowIndex].DividerHeight = grid.Rows[rowIndex].Height - m_rowDefaultHeight;
                    graphics.DrawImage(Resources.DBGridViewCollapse, rect);
                    m_childView.Location = new Point(rowBounds.Left + grid.RowHeadersWidth,
                        rowBounds.Top + m_rowDefaultHeight + 5);
                    // childView.Width = rowBounds.Right - grid.rowheaderswidth
                    m_childView.Width = Width;
                    m_childView.Height = grid.Rows[rowIndex].DividerHeight - 10;
                    m_childView.Visible = true;
                }
                else
                {
                    m_childView.Visible = false;
                    graphics.DrawImage(Resources.DBGridViewExpand, rect);
                }

                m_collapseRow = false;
            }
            else if (m_rowCurrent.Contains(rowIndex))
            {
                grid.Rows[rowIndex].DividerHeight = grid.Rows[rowIndex].Height - m_rowDefaultHeight;
                graphics.DrawImage(Resources.DBGridViewCollapse, rect);
                m_childView.Location = new Point(rowBounds.Left + grid.RowHeadersWidth,
                    rowBounds.Top + m_rowDefaultHeight + 5);
                // childView.Width = e.RowBounds.Right - grid.rowheaderswidth
                m_childView.Width = Width;
                m_childView.Height = grid.Rows[rowIndex].DividerHeight - 10;
                m_childView.Visible = true;
            }
            else
            {
                graphics.DrawImage(Resources.DBGridViewExpand, rect);
            }
        }

        /// <summary>
        ///     Control de expand/collapse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ShowExpand)
            {
                SetExpandHeight(e.Location, e.RowIndex);
            }
        }

        private void SetExpandHeight(Point location, int rowIndex)
        {
            var rect = new Rectangle((m_rowDefaultHeight - 16)
                                         / 2, (m_rowDefaultHeight - 16)
                                              / 2, 16, 16);

            if (rect.Contains(location))
            {
                if (m_rowCurrent.Contains(rowIndex))
                {
                    m_rowCurrent.Clear();
                    Rows[rowIndex].Height = m_rowDefaultHeight;
                    Rows[rowIndex].DividerHeight = m_rowDefaultDivider;
                }
                else
                {
                    if (m_rowCurrent.Count != 0)
                    {
                        var eRow = m_rowCurrent[0];
                        m_rowCurrent.Clear();
                        datagrid.Rows[eRow].Height = m_rowDefaultHeight;
                        datagrid.Rows[eRow].DividerHeight = m_rowDefaultDivider;
                        datagrid.ClearSelection();
                        m_collapseRow = true;
                    }

                    m_rowCurrent.Add(rowIndex);
                    datagrid.Rows[rowIndex].Height = m_rowExpandedHeight;
                    datagrid.Rows[rowIndex].DividerHeight = m_rowExpandedDivider;
                }

                datagrid.ClearSelection();
                m_collapseRow = true;
            }
            else
            {
                m_collapseRow = false;
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // if(error)e.CellStyle.BackColor = Color.Red;

            if (CellFormatting != null)
                CellFormatting(sender, e);
        }

        private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //if (!e.Row.IsNewRow)
            //{
            //    DialogResult res = MessageBox.Show("¿Estás seguro que deseas borrar esta fila?", "Confirmación de borrado",
            //             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (res == DialogResult.No)
            //        e.Cancel = true;
            //}

            if (UserDeletingRow != null)
                UserDeletingRow(sender, e);
        }

        private void DataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (InitializeRow != null)
                InitializeRow(sender, e);
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CellDoubleClick != null)
                CellDoubleClick(sender, e);

            if(DoubleClickRow != null)
                DoubleClickRow(sender, e);
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (RowsAdded != null)
                RowsAdded(sender, e);
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (CellValueChanged != null)
                CellValueChanged(sender, e);
        }

        private void DataGridView1_Leave(object sender, EventArgs e)
        {
            if (AutoSave)
                if (DataControl != null)
                    if (DataControl.HasDataToSave() != null)
                        if (DataControl.TypeDB != DBControl.DbType.Data && DataControl.TypeDB != DBControl.DbType.XML)
                            DataControl.Save();
        }

        public void ScrollToRow(int row)
        {
            if (datagrid.Rows.Count >= row && row >= 1)
            {
                datagrid.FirstDisplayedScrollingRowIndex = row;
                datagrid.Rows[row].Selected = true;
            }
        }

        public void ScrollToTop()
        {
            datagrid.FirstDisplayedScrollingRowIndex = 0;
            datagrid.Rows[0].Selected = true;
        }

        public void ScrollToBottom()
        {
            datagrid.FirstDisplayedScrollingRowIndex = Rows.Count - 1;
            datagrid.Rows[Rows.Count - 1].Selected = true;
        }

        public int VisibleRowCount()
        {
            return datagrid.Rows.GetRowCount(DataGridViewElementStates.Visible);
        }

        public object RowValue(int column)
        {
            return RowValue(column, -1);
        }

        public object RowValue(int column, int row)
        {
            if (row == -1) row = datagrid.CurrentCell.RowIndex;
            if (VisibleRowCount() == 0) return null;
            try
            {
                object value = datagrid.Rows[row].Cells[column].Value;
                if(value is DBNull) 
                    return null;
                else
                    return value;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void RowValue(int column, object Value)
        {
            RowValue(column, -1, Value);
        }

        public void RowValue(int column, int row, object Value)
        {
            if (row == -1) row = datagrid.CurrentCell.RowIndex;
            datagrid.Rows[row].Cells[column].Value = Value;
        }

        public object RowValue(string columnName)
        {
            return RowValue(columnName, -1);
        }

        public object RowValue(string columnName, int row)
        {
            if (columnName == "") return null;
            if (row == -1) row = datagrid.CurrentCell.RowIndex;
            if (VisibleRowCount() == 0) return null;
            var i = Columns.GetColumnOrdinal(columnName);
            if (i != -1)
                try
                {
                    return datagrid[row, i];
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }

            return null;
        }

        public void RowValue(string columnName, object Value)
        {
            RowValue(columnName, -1, Value);
        }

        public void RowValue(string columnName, int row, object Value)
        {
            if (row == -1) row = datagrid.CurrentCell.RowIndex;
            datagrid.Rows[row].Cells[DataControl.FieldOrdinal(columnName)].Value = Value;
        }

        public object RowDataValue(int column)
        {
            return RowDataValue(column);
        }

        public object RowDataValue(int column, int row)
        {
            if (row == -1) row = datagrid.CurrentCell.RowIndex;
            try
            {
                return DataControl.DataTable.Rows[row][Columns[column].FieldDB];
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void RowDataValue(int column, object Value)
        {
            RowDataValue(column, -1, Value);
        }

        public void RowDataValue(int column, int row, object Value)
        {
            if (row == -1) row = datagrid.CurrentCell.RowIndex;
            DataControl.DataTable.Rows[row][Columns[column].FieldDB] = Value;
        }

        public object RowDataValue(string columnName)
        {
            return RowDataValue(columnName, -1);
        }

        public object RowDataValue(string columnName, int row)
        {
            if (DataControl.DataTable.Rows.Count == 0) 
                return null;
            if (row == -1) 
                row = datagrid.CurrentCell.RowIndex;

            try
            {
                return DataControl.DataTable.Rows[row][columnName];
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void RowDataValue(string columnName, object Value)
        {
            RowDataValue(columnName, -1, Value);
        }

        public void RowDataValue(string columnName, int row, object Value)
        {
            if (row == -1) row = datagrid.CurrentCell.RowIndex;
            DataControl.DataTable.Rows[row][columnName] = Value;
        }


        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int f = 0;
            foreach (DBColumn col in Columns)
            {
                if (f == e.ColumnIndex)
                {
                    if (col.Obligatory)
                    {
                        if (e.FormattedValue == null)
                        {
                            datagrid.Rows[e.RowIndex].ErrorText = "Valor obligatorio. Columna: [" + col.HeaderCaption + "]";
                            e.Cancel = true;
                            return;
                        }
                    }

                    string errorText = "";
                    bool check = true;

                    if (e.FormattedValue != null)
                    {
                        switch (col.ColumnType)
                        {
                            case DBColumn.ColumnTypes.NumberColumn:
                                check = NumberUtils.IsNumeric(e.FormattedValue.ToString());
                                errorText = "No es un valor numérico. Columna: [" + col.HeaderCaption + "] Valor: [" + e.FormattedValue.ToString() + "]";
                                break;
                            case DBColumn.ColumnTypes.TextColumn:
                                //check = TextUtil.IsEmail(e.FormattedValue.ToString());
                                //errorText = "No es una cuenta de correo válida. Columna: [" + col.HeaderCaption + "] Valor: [" + e.FormattedValue.ToString() + "]";
                                break;
                            case DBColumn.ColumnTypes.DateColumn:
                                check = FSLibrary.DateTimeUtil.IsDate(e.FormattedValue.ToString());
                                errorText = "No es una fecha válida. Columna: [" + col.HeaderCaption + "] Valor: [" + e.FormattedValue.ToString() + "]";
                                break;
                        }
                        if (!check)
                        {
                            datagrid.Rows[e.RowIndex].ErrorText = errorText;
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                f++;
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            datagrid.Rows[e.RowIndex].ErrorText = string.Empty;

            if (CellEndEdit != null)
                CellEndEdit(sender, e);
        }

        private void DataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            ////TODO: ASIGNACION DE FORECOLOR Y BACKCOLOR POR CELDA
            //if (datagrid.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Red)
            //{
            //    datagrid.CurrentRow.DefaultCellStyle.BackColor = System.Drawing.Color.Aqua;
            //    SaveRow(e.RowIndex);
            //}

            //DataGridViewRow row = datagrid.Rows[e.RowIndex];
            //row.ErrorText = "";

            //foreach (DataGridViewCell cell in row.Cells)
            //{
            //    cell.ErrorText = "";
            //}
        }

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //TODO: ASIGNACION DE COLOR POR CELDA
            //datagrid.CurrentRow.DefaultCellStyle.BackColor = System.Drawing.Color.Red;
        }

        public void AddColumn(DBColumn column)
        {
            try
            {
                if (!Columns.Contains(column))
                    Columns.Add(column);

                if (DataControl != null && !(column.ColumnType == DBColumn.ColumnTypes.DescriptionColumn |
                      column.ColumnType == DBColumn.ColumnTypes.FormulaColumn))
                    column.FieldDB = DataControl.FieldExactName(column.FieldDB);


                if (column.HeaderCaption == "") column.HeaderCaption = TextUtil.PCase(column.FieldDB);

                switch (column.ColumnType)
                {
                    case DBColumn.ColumnTypes.PictureColumn:
                        {
                            var dbic = new DataGridViewImageColumn();

                            dbic.Visible = !column.Hidden;
                            dbic.DataPropertyName = column.FieldDB;
                            dbic.HeaderText = column.HeaderCaption;
                            dbic.ReadOnly = column.ReadColumn;
                            dbic.Width = column.Width;
                            dbic.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            dbic.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            dbic.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            dbic.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            dbic.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(dbic);
                        }
                        break;
                    case DBColumn.ColumnTypes.CheckColumn:
                        {
                            var dbcbc = new DataGridViewCheckBoxColumn();

                            dbcbc.Visible = !column.Hidden;
                            dbcbc.DataPropertyName = column.FieldDB;
                            dbcbc.HeaderText = column.HeaderCaption;
                            dbcbc.ReadOnly = column.ReadColumn;
                            dbcbc.Width = column.Width;
                            dbcbc.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            dbcbc.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            dbcbc.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            dbcbc.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            dbcbc.DefaultCellStyle.NullValue = false;
                            dbcbc.DefaultCellStyle.Format = column.FormatString;
                            dbcbc.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(dbcbc);
                        }
                        break;
                    case DBColumn.ColumnTypes.FormulaColumn:
                        {
                            var textCol = new DataGridViewTextBoxColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = "n" + column.Decimals;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.TextColumn:
                        {
                            var textCol = new DataGridViewTextBoxColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = column.FormatString;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.MaskedColumn:
                        {
                            var cm = new DBGridViewMaskColumn(column.MaskInput);

                            cm.Visible = !column.Hidden;
                            cm.DataPropertyName = column.FieldDB;
                            cm.HeaderText = column.HeaderCaption;
                            cm.ReadOnly = column.ReadColumn;
                            cm.Width = column.Width;
                            cm.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            cm.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            cm.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            cm.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            cm.DefaultCellStyle.Format = column.FormatString;
                            cm.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(cm);

                            break;
                        }
                    case DBColumn.ColumnTypes.TimePickerColumn:
                        {
                            var tp = new DBGridViewDateTimePickerColumn();

                            tp.Visible = !column.Hidden;
                            tp.DataPropertyName = column.FieldDB;
                            tp.HeaderText = column.HeaderCaption;
                            tp.ReadOnly = column.ReadColumn;
                            tp.Width = column.Width;
                            tp.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            tp.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            tp.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            tp.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            tp.DefaultCellStyle.Format = column.FormatString;
                            tp.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(tp);
                        }
                        break;
                    case DBColumn.ColumnTypes.MoneyColumn:
                        {
                            var textCol = new DataGridViewTextBoxColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = "c" + column.Decimals;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.DateColumn:
                        {
                            var textCol = new DBGridViewDateTimePickerColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = Global.DATE_FORMAT;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.TimeColumn:
                        {
                            var textCol = new DataGridViewTextBoxColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = Global.TIME_FORMAT;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.FileColumn:
                        {
                            //DataGridFileColumn fileCol = new DataGridFileColumn( col, Columns[ col ], this ); 

                            //fileCol.MappingName = Columns[ col ].FieldDB; 
                            //fileCol.HeaderText = Columns[ col ].HeaderCaption; 
                            //fileCol.Width = System.Convert.ToInt32( Columns[ col ].Size ); 
                            //fileCol.NullText = ""; 
                            //fileCol.ReadOnly = Columns[ col ].ReadColumn; 
                            //fileCol.Alignment = Columns[ col ].Alignment; 
                            //fileCol.TextBox.MaxLength = Columns[ col ].MaxLength; 
                            //try 
                            //{ 
                            //    dgTableStyle.GridColumnStyles.Add( fileCol ); 
                            //} 
                            //catch ( Exception e ) 
                            //{ 
                            //    Global.Err.ErrorMessage( this.FindForm(), this, e.Message + "\r\n" + "Columna: " + Columns[ col ].FieldDB, "", MessageBoxIcon.Error, null, false ); 
                            //} 
                        }
                        break;
                    case DBColumn.ColumnTypes.NumberColumn:
                    case DBColumn.ColumnTypes.AutoNumericColumn:
                        {
                            var textCol = new DataGridViewTextBoxColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = "n" + column.Decimals;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.ComboColumn:
                        {
                            var comboCol = new DataGridViewComboBoxColumn();

                            comboCol.Visible = !column.Hidden;
                            comboCol.DataSource = column.ColumnDBControl.DataTable;
                            comboCol.DisplayMember = column.ComboListField;
                            comboCol.ValueMember = column.ColumnDBFieldData;
                            comboCol.DataPropertyName = column.FieldDB;
                            comboCol.ReadOnly = column.ReadColumn;
                            comboCol.Width = column.Width;
                            comboCol.HeaderText = column.HeaderCaption;
                            comboCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            comboCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            comboCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            comboCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            comboCol.DefaultCellStyle.Format = column.FormatString;
                            comboCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(comboCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.ButtonColumn:
                        {
                            var textCol = new DataGridViewButtonColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = column.FormatString;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.Button2Column:
                        {
                            var textCol = new DataGridViewButtonColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = column.FormatString;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.DescriptionColumn:
                        {
                            var textCol = new DataGridViewTextBoxColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = column.FormatString;
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.PercentColumn:
                        {
                            var textCol = new DataGridViewTextBoxColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = "p0";
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                    case DBColumn.ColumnTypes.ProgressColumn:
                        {
                            var textCol = new DBGridViewProgressBarColumn();

                            textCol.Visible = !column.Hidden;
                            textCol.DataPropertyName = column.FieldDB;
                            textCol.HeaderText = column.HeaderCaption;
                            textCol.ReadOnly = column.ReadColumn;
                            textCol.Width = column.Width;
                            textCol.DefaultCellStyle.WrapMode = column.Multiline
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            textCol.DefaultCellStyle.BackColor = column.ColumnBackColor;
                            textCol.DefaultCellStyle.ForeColor = column.ColumnForeColor;
                            textCol.DefaultCellStyle.Alignment = column.Alignment == HorizontalAlignment.Center
                                ? DataGridViewContentAlignment.MiddleCenter
                                : column.Alignment == HorizontalAlignment.Left
                                    ? DataGridViewContentAlignment.MiddleLeft
                                    : DataGridViewContentAlignment.MiddleRight;
                            textCol.DefaultCellStyle.Format = "n2";
                            textCol.DefaultCellStyle.NullValue = column.NullValue;

                            datagrid.Columns.Add(textCol);
                        }
                        break;
                }


                if (DataControl != null && DataControl.DataTable != null)
                    try
                    {
                        var dtcol = DataControl.DataTable.Columns[column.FieldDB];
                        if (dtcol != null) dtcol.Unique = column.Unique;
                    }
                    catch (Exception e)
                    {
                        throw new ExceptionUtil(e);
                    }

                if (column.Hidden)
                {
                    HideColumn(column.FieldDB);
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil("Error al Añadir la columna: " + column.FieldDB, e);
            }
        }


        public new void Select()
        {
            datagrid.Select();
        }


        public void Select(int row)
        {
            try
            {
                datagrid.Rows[row].Selected = true;
            }
            catch
            {
            }
        }

        public void Print()
        {
            try
            {
                GridViewPrint.PrintDataGridView(datagrid, false);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public void PrintPreview()
        {
            try
            {
                GridViewPrint.PrintDataGridView(datagrid, true);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        private void ShowTotal()
        {
        }

        public void RefreshTotals()
        {
        }

        private void CopySelectedRowsToClipboard()
        {
            var iRow = 0;
            StringBuilder sb = null;

            var cm = (CurrencyManager) datagrid.BindingContext[datagrid.DataSource, datagrid.DataMember];
            var iRowCount = 0;
            var iMaxColIndex = 0;

            sb = new StringBuilder();

            try
            {
                sb.Append(GetHeaderRow());

                if (!(cm == null))
                {
                    iRowCount = cm.Count - 1;
                    iMaxColIndex = GetMaxColumnIndex();

                    while (iRow <= iRowCount)
                    {
                        if (datagrid.Rows[iRow].Selected) sb.Append(GetGridRow(iRow, iMaxColIndex));
                        iRow += 1;
                    }
                }

                FSFormLibrary.Clipboard.SetDataObject(sb.ToString(), true);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
            finally
            {
                m_LastRowClicked = -1;
            }
        }


        private void CopySingleRowToClipboard()
        {
            if (m_LastRowClicked < 0) return;

            var iRow = m_LastRowClicked;
            var iMaxColIndex = 0;

            var sb = new StringBuilder();


            try
            {
                sb.Append(GetHeaderRow());

                iMaxColIndex = GetMaxColumnIndex();
                sb.Append(GetGridRow(iRow, iMaxColIndex));

                FSFormLibrary.Clipboard.SetDataObject(sb.ToString(), true);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
            finally
            {
                m_LastRowClicked = -1;
            }
        }


        private void CopyAllRowsToClipboard()
        {
            var iRow = 0;
            var cm = (CurrencyManager) datagrid.BindingContext[datagrid.DataSource, datagrid.DataMember];
            StringBuilder sb = null;
            var iRowCount = 0;
            var iMaxColIndex = 0;


            sb = new StringBuilder();

            try
            {
                sb.Append(GetHeaderRow());

                if (!(cm == null))
                {
                    iRowCount = cm.Count - 1;
                    iMaxColIndex = GetMaxColumnIndex();

                    while (iRow <= iRowCount)
                    {
                        sb.Append(GetGridRow(iRow, iMaxColIndex));
                        iRow += 1;
                    }
                }

                FSFormLibrary.Clipboard.SetDataObject(sb.ToString(), true);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
            finally
            {
                m_LastRowClicked = -1;
            }
        }


        private int GetMaxColumnIndex()
        {
            return datagrid.Columns.Count;
        }

        public void SetDataBinding(DataTable dataSource, string dataMember)
        {
            datagrid.DataSource = dataSource;
            datagrid.DataMember = dataMember;
        }

        public void SetDataBinding(ArrayList dataSource, string dataMember)
        {
            datagrid.DataSource = dataSource;
            datagrid.DataMember = dataMember;
        }

        private string GetGridRow(int iRow, int iMaxColIndex)
        {
            StringBuilder sb = null;
            var iCol = 0;
            string strCell = null;

            sb = new StringBuilder();

            try
            {
                for (iCol = 0; iCol <= iMaxColIndex; iCol++)
                {
                    try
                    {
                        strCell = datagrid[iRow, iCol].ToString();
                    }
                    catch
                    {
                        strCell = "";
                    }

                    sb.Append(strCell);
                    sb.Append(Global.Tab);
                }

                sb.Append("\r\n");
            }
            catch
            {
            }

            return sb.ToString();
        }


        private string GetHeaderRow()
        {
            StringBuilder sb = null;

            sb = new StringBuilder();

            try
            {
                foreach (DataGridViewColumn col in datagrid.Columns)
                {
                    sb.Append(col.HeaderText);
                    sb.Append(Global.Tab);
                }

                sb.Append("\r\n");
            }
            catch
            {
            }

            return sb.ToString();
        }

        public void MnuColumnEdit(object sender, EventArgs e)
        {
            //SelectColumns();
        }


        public void MnuRefrescar(object sender, EventArgs e)
        {
            if (DataControl != null)
            {
                DataControl.ReConnect();
                DataControl.Go(0);
            }
        }

        public void Fill()
        {
            var f = 0;

            if (m_DBControl == null)
                return;

            if (m_DBControl.DataTable != null)
            {
                m_DBControl.DataTable.ColumnChanging += ColumnChangingEvt;
                m_DBControl.DataTable.RowChanging += RowChangingEvt;
                m_DBControl.DataTable.ColumnChanged += ColumnChangedEvt;
                m_DBControl.DataTable.RowChanged += RowChangedEvt;
            }

            m_DBControl.AsociatedDBGridView = this;
            m_DBControl.OnReConnect += DataControl_OnReConnect;

            if (!m_DBControl.Connected) return;

            //si existen más de una tabla en el dataset, mostramos el control de expando/collapse
            if (m_DBControl.DataSet != null)
                if (m_DBControl.DataSet.Tables != null)
                    if (m_DBControl.DataSet.Tables.Count > 1)
                    {
                        if (m_childView == null) m_childView = new DBGridView();
                        m_childView.Visible = false;
                        m_childView.DataControl = new DBControl(new DataView(DataControl.DataSet.Tables[1]));
                        datagrid.Controls.Add(m_childView);
                        ShowExpand = true;
                        Editable = false;

                        m_childView.Fill();
                    }


            m_DBControl.Action = DBControl.DbActionTypes.Fill;

            if (string.IsNullOrEmpty(m_DBControl.DBFieldData)) 
                m_DBControl.DBFieldData = m_DBControl.FieldName(0);


            if (m_DBControl.DataView != null)
                datagrid.DataSource = m_DBControl.DataView;
            else
                datagrid.DataSource = m_DBControl.DataTable;

            GenerateColumns();

            FillDescriptionColumns();

            if (RecordMode) 
                FillRecord();

            if (!m_DBControl.Paging)
            {
                cmdPageFirst.Visible = false;
                cmdPageLast.Visible = false;
                cmdPageNext.Visible = false;
                cmdPagePrevious.Visible = false;
                lblPage.Visible = false;
            }
            else
            {
                cmdPageFirst.Visible = true;
                cmdPageLast.Visible = true;
                cmdPageNext.Visible = true;
                cmdPagePrevious.Visible = true;
                lblPage.Visible = true;
                UpdatePage();

                //vGridScrollBar = ((System.Windows.Forms.VScrollBar)(dataGridView1.Controls[1]));
                //vGridScrollBar.Maximum = System.Convert.ToInt32(DataControl.RecordCount());
                //vGridScrollBar.LargeChange = DataControl.PagingSize;
            }

            m_DBControl.Action = DBControl.DbActionTypes.None;

            if (m_DBControl.ColumnMapping.Count == 0)
                for (f = 0; f <= Columns.Count - 1; f++)
                    m_DBControl.ColumnMapping.Add(Columns[f].FieldDB, Columns[f].HeaderCaption);

            if (ShowTotals)
            {
                ShowTotal();
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
            }

            Resize();
        }


        private void GenerateColumns()
        {
            FunctionsGrid.GenerateColumns(DataControl, Columns, 2, AutoSizeColumns, datagrid.CreateGraphics(), datagrid.Font);

            if (datagrid.Columns.Count == 0)
                foreach (DBColumn column in Columns)
                        AddColumn(column);

        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (DataError != null)
                DataError(sender, e);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CellClick != null)
                CellClick(sender, e);

            //ejecutamos el click en la coluna seleccionada
            if (e.ColumnIndex != -1)
                if (e.ColumnIndex < Columns.Count)
                    if (Columns[e.ColumnIndex] != null)
                        Columns[e.ColumnIndex].PerformClick(sender, e);
        }

        private void ColumnChangingEvt(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                if (DataControl.Action == DBControl.DbActionTypes.Change) return;
                DataControl.Action = DBControl.DbActionTypes.Change;

                CheckObligatoryColumn(e);

                if (null != ColumnChanging) ColumnChanging(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        private void RowChangingEvt(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                if (DataControl.Action == DBControl.DbActionTypes.Change) return;
                DataControl.Action = DBControl.DbActionTypes.Change;


                CheckObligatoryRow(e.Row);

                if (null != RowChanging) RowChanging(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        private void RowChangedEvt(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                if (DataControl.Action == DBControl.DbActionTypes.Change) return;
                DataControl.Action = DBControl.DbActionTypes.Change;

                if (null != RowChanged)
                    RowChanged(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        private void ColumnChangedEvt(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                if (DataControl.Action == DBControl.DbActionTypes.Change) return;
                DataControl.Action = DBControl.DbActionTypes.Change;

                if (!(Columns.Find(e.Column.ColumnName) == null))
                    switch (Columns.Find(e.Column.ColumnName).ColumnType)
                    {
                        case DBColumn.ColumnTypes.ButtonColumn:
                            UpdateAsociatedColumns(e.Row, false, true);
                            break;
                    }

                if (null != ColumnChanged) ColumnChanged(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            //RefreshTotals();

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        private void CheckUniqueRow(DataRow row)
        {
            var arr = new ArrayList();
            var arrRow = new ArrayList();
            var g = 0;
            var f = 0;

            for (f = 0; f <= Columns.Count - 1; f++)
                if (Columns[f].Unique)
                    arr.Add(row[Columns[f].FieldDB, DataRowVersion.Proposed]);

            if (arr.Count == 0) return;

            for (f = 0; f <= DataControl.DataTable.Rows.Count - 1; f++)
            {
                arrRow.Clear();

                for (g = 0; g <= Columns.Count - 1; g++)
                    if (Columns[g].Unique)
                        arrRow.Add(DataControl.DataTable.Rows[f][Columns[g].FieldDB, DataRowVersion.Current]);

                if (Functions.ArrayListCompare(arr, arrRow))
                {
                    row.RowError = "Clave duplicada!";
                    break;
                }

                row.RowError = "";
            }
        }


        private void CheckObligatoryRow(DataRow row)
        {
            var f = 0;

            if (row.RowState == DataRowState.Unchanged) return;
            if (row.RowState == DataRowState.Deleted) return;
            if (row.RowState == DataRowState.Detached) return;

            for (f = 0; f <= Columns.Count - 1; f++)
                if (Columns[f].Obligatory)
                {
                    if (row[Columns[f].FieldDB] is DBNull)
                        row.SetColumnError(Columns[f].FieldDB, "Columna obligatoria!");
                    else
                        row.SetColumnError(Columns[f].FieldDB, "");
                }
        }


        private void CheckObligatoryColumn(DataColumnChangeEventArgs e)
        {
            if (!(Columns.Find(e.Column.ColumnName) == null))
                if (Columns.Find(e.Column.ColumnName).Obligatory)
                    if (!(e.ProposedValue == DBNull.Value))
                        e.Row.SetColumnError(e.Column, "");
        }


        public void UpdateAsociatedColumns(int row, bool updateDescriptionColumn, bool acceptChanges)
        {
            UpdateAsociatedColumns(DataControl.DataTable.Rows[row], updateDescriptionColumn, acceptChanges);
        }


        public void UpdateAsociatedColumns(DataRow row, bool updateDescriptionColumn, bool acceptChanges)
        {
            string findField = null;

            if (row == null) return;

            foreach (DBColumn col in Columns)
                if (col.ColumnType == DBColumn.ColumnTypes.DescriptionColumn)
                    try
                    {
                        if (col.AsociatedButtonColumn != -1)
                        {
                            //si tiene una columna con DBControl, lo conectamos.
                            if (Columns[col.AsociatedButtonColumn].ColumnDBControl != null)
                                if (Columns[col.AsociatedButtonColumn].ColumnDBControl.Connected == false)
                                    Columns[col.AsociatedButtonColumn].ColumnDBControl.Connect();

                            if (Columns[col.AsociatedButtonColumn].ColumnType == DBColumn.ColumnTypes.ButtonColumn)
                                if (col.ReadColumn | updateDescriptionColumn)
                                {
                                    if (Columns[col.AsociatedButtonColumn].ColumnDBFieldData != "")
                                        findField = Columns[col.AsociatedButtonColumn].ColumnDBFieldData;
                                    else
                                        findField = Columns[col.AsociatedButtonColumn].FieldDB;
                                    if (col.FormatString == "")
                                        switch (col.DescriptionType)
                                        {
                                            case DBColumn.DescriptionTypes.DateDescription:
                                                row[col.FieldDB] = DateTime.Parse(Columns[col.AsociatedButtonColumn]
                                                    .ColumnDBControl.Find(findField,
                                                        row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                        col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.NumberDescription:
                                                row[col.FieldDB] = NumberUtils.NumberDouble(Columns[col.AsociatedButtonColumn]
                                                    .ColumnDBControl.Find(findField,
                                                        row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                        col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.CheckDescription:
                                                row[col.FieldDB] = NumberUtils.NumberByte(Columns[col.AsociatedButtonColumn]
                                                    .ColumnDBControl.Find(findField,
                                                        row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                        col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.TextDescription:
                                                row[col.FieldDB] = Columns[col.AsociatedButtonColumn].ColumnDBControl
                                                    .Find(findField,
                                                        row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                        col.FieldDB);
                                                break;
                                        }
                                    else
                                        switch (col.DescriptionType)
                                        {
                                            case DBColumn.DescriptionTypes.DateDescription:
                                                row[col.FieldDB] = string.Format("{0:" + col.FormatString + "}",
                                                    DateTime.Parse(Columns[col.AsociatedButtonColumn].ColumnDBControl
                                                        .Find(findField,
                                                            row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                            col.FieldDB)));
                                                break;
                                            case DBColumn.DescriptionTypes.NumberDescription:
                                                row[col.FieldDB] = string.Format("{0:" + col.FormatString + "}",
                                                    NumberUtils.NumberDouble(Columns[col.AsociatedButtonColumn].ColumnDBControl
                                                        .Find(findField,
                                                            row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                            col.FieldDB)));
                                                break;
                                            case DBColumn.DescriptionTypes.CheckDescription:
                                                row[col.FieldDB] = string.Format("{0:" + col.FormatString + "}",
                                                    NumberUtils.NumberByte(Columns[col.AsociatedButtonColumn].ColumnDBControl
                                                        .Find(findField,
                                                            row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                            col.FieldDB)));
                                                break;
                                        }
                                }
                        }

                        if (col.AsociatedComboColumn != -1)
                            if (Columns[col.AsociatedComboColumn].ColumnType == DBColumn.ColumnTypes.ComboColumn)
                                if (col.ReadColumn | updateDescriptionColumn)
                                {
                                    if (Columns[col.AsociatedComboColumn].ColumnDBFieldData != "")
                                        findField = Columns[col.AsociatedComboColumn].ColumnDBFieldData;
                                    else
                                        findField = Columns[col.AsociatedComboColumn].FieldDB;
                                    if (col.FormatString == "")
                                        switch (col.DescriptionType)
                                        {
                                            case DBColumn.DescriptionTypes.DateDescription:
                                                row[col.FieldDB] = DateTime.Parse(Columns[col.AsociatedComboColumn]
                                                    .ColumnDBControl.Find(findField,
                                                        row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                        col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.NumberDescription:
                                                row[col.FieldDB] = NumberUtils.NumberDouble(Columns[col.AsociatedComboColumn]
                                                    .ColumnDBControl.Find(findField,
                                                        row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                        col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.CheckDescription:
                                                row[col.FieldDB] = NumberUtils.NumberByte(Columns[col.AsociatedComboColumn]
                                                    .ColumnDBControl.Find(findField,
                                                        row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                        col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.TextDescription:
                                                row[col.FieldDB] = Columns[col.AsociatedComboColumn].ColumnDBControl
                                                    .Find(findField,
                                                        row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                        col.FieldDB);
                                                break;
                                        }
                                    else
                                        switch (col.DescriptionType)
                                        {
                                            case DBColumn.DescriptionTypes.DateDescription:
                                                row[col.FieldDB] = string.Format("{0:" + col.FormatString + "}",
                                                    DateTime.Parse(Columns[col.AsociatedComboColumn].ColumnDBControl
                                                        .Find(findField,
                                                            row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                            col.FieldDB)));
                                                break;
                                            case DBColumn.DescriptionTypes.NumberDescription:
                                                row[col.FieldDB] = string.Format("{0:" + col.FormatString + "}",
                                                    NumberUtils.NumberDouble(Columns[col.AsociatedComboColumn].ColumnDBControl
                                                        .Find(findField,
                                                            row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                            col.FieldDB)));
                                                break;
                                            case DBColumn.DescriptionTypes.CheckDescription:
                                                row[col.FieldDB] = string.Format("{0:" + col.FormatString + "}",
                                                    NumberUtils.NumberByte(Columns[col.AsociatedComboColumn].ColumnDBControl
                                                        .Find(findField,
                                                            row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                            col.FieldDB)));
                                                break;
                                        }
                                }
                    }
                    catch (Exception e)
                    {
                        throw new ExceptionUtil(e);
                    }
                else if (col.ColumnType != DBColumn.ColumnTypes.FormulaColumn)
                    try
                    {
                        if (col.AsociatedButtonColumn != -1)
                            if (Columns[col.AsociatedButtonColumn].ColumnType == DBColumn.ColumnTypes.ButtonColumn)
                            {
                                if (Columns[col.AsociatedButtonColumn].ColumnDBFieldData != "")
                                    findField = Columns[col.AsociatedButtonColumn].ColumnDBFieldData;
                                else
                                    findField = Columns[col.AsociatedButtonColumn].FieldDB;
                                if (Convert.IsDBNull(
                                    DataControl.DataTable.Rows[datagrid.CurrentCell.RowIndex][col.FieldDB]))
                                {
                                    row[col.FieldDB] = Columns[col.AsociatedButtonColumn].ColumnDBControl
                                        .Find(findField, row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                            col.FieldDB);
                                }
                                else
                                {
                                    if ((row[col.FieldDB].ToString() == "") | (Convert.ToDouble(row[col.FieldDB]) == 0))
                                        row[col.FieldDB] = Columns[col.AsociatedButtonColumn].ColumnDBControl
                                            .Find(findField, row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                col.FieldDB);
                                }
                            }

                        if (col.AsociatedComboColumn != -1)
                            if (Columns[col.AsociatedComboColumn].ColumnType == DBColumn.ColumnTypes.ButtonColumn)
                            {
                                if (Columns[col.AsociatedComboColumn].ColumnDBFieldData != "")
                                    findField = Columns[col.AsociatedComboColumn].ColumnDBFieldData;
                                else
                                    findField = Columns[col.AsociatedComboColumn].FieldDB;
                                if (Convert.IsDBNull(
                                    DataControl.DataTable.Rows[datagrid.CurrentCell.RowIndex][col.FieldDB]))
                                {
                                    row[col.FieldDB] = Columns[col.AsociatedComboColumn].ColumnDBControl.Find(findField,
                                        row[Columns[col.AsociatedComboColumn].FieldDB].ToString(), col.FieldDB);
                                }
                                else
                                {
                                    if ((row[col.FieldDB].ToString() == "") | (Convert.ToDouble(row[col.FieldDB]) == 0))
                                        row[col.FieldDB] = Columns[col.AsociatedComboColumn].ColumnDBControl
                                            .Find(findField, row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                col.FieldDB);
                                }
                            }
                    }
                    catch (Exception e)
                    {
                        throw new ExceptionUtil(e);
                    }

            if (acceptChanges)
            {
                //no se si esto es necesarío. De momento lo comento.
                //DataControl.DataTable.AcceptChanges();
            }
        }


        private void UpdatePage()
        {
            lblPage.Text = DataControl.Page + 1 + "/" + DataControl.PageCount();
        }


        public new void Refresh()
        {
            datagrid.Refresh();
            if (DataControl != null)
            {
                DataControl.ReConnect();
                DataControl.Go(0);
            }
        }


        public new void Update()
        {
            datagrid.Update();
        }

        private void MnuExcelExport(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        public void ExportToExcel(string fileName)
        {
            FSExcel.Excel excel = new FSExcel.Excel();
            excel.Export(ExportDBGridView(), fileName);
        }

        public void ExportToExcel()
        {
            FSExcel.Excel excel = new FSExcel.Excel();
            excel.Export(ExportDBGridView());
        }

        public DataTable ExportDBGridView()
        {
            if (DataControl != null && DataControl.DataTable != null)
                return DataControl.DataTable;

            string data;
            DataTable dataTable = new DataTable();

            for (int f = 0; f <= this.Rows.Count - 1; f++)
            {
                DataRow row = dataTable.NewRow();

                for (int g = 0; g <= this.Columns.Count - 1; g++)
                {
                    if (this.Columns[g].ColumnType == FSFormControls.DBColumn.ColumnTypes.ComboColumn)
                    {
                        data = this.Columns[g].ColumnDBControl.Find(this.Columns[g].ColumnDBFieldData,
                                                                                     Convert.ToString(
                                                                                         this.RowValue(
                                                                                             this.Columns[g].FieldDB, -1)),
                                                                                     this.Columns[g].ComboListField);
                    }
                    else
                    {
                        data = Convert.ToString(this.RowValue(this.Columns[g].FieldDB, f));
                    }

                    row[g] = data;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private void UnCheckTotalMenu()
        {
            if (dataGridViewTotal.ContextMenuStrip == null)
                return;

            //var f = 0;
            //for (f = 0; f <= dataGridViewTotal.ContextMenuStrip.Items.Count - 1; f++)
            //    dataGridViewTotal.ContextMenuStrip.Items[f].Checked = false;
        }


        private void MnuCopyOneReg(object sender, EventArgs e)
        {
            CopySingleRowToClipboard();
        }


        private void MnuCopySelectedReg(object sender, EventArgs e)
        {
            CopySelectedRowsToClipboard();
        }


        private void MnuCopyAllReg(object sender, EventArgs e)
        {
            CopyAllRowsToClipboard();
        }


        private void MnuSum(object sender, EventArgs e)
        {
            TotalOperation = DBColumn.OperationTypes.Sum;
            UnCheckTotalMenu();
            ((ToolStripMenuItem) sender).Checked = true;
            RefreshTotals();
        }


        private void MnuMax(object sender, EventArgs e)
        {
            TotalOperation = DBColumn.OperationTypes.Max;
            UnCheckTotalMenu();
            ((ToolStripMenuItem) sender).Checked = true;
            RefreshTotals();
        }


        private void MnuMin(object sender, EventArgs e)
        {
            TotalOperation = DBColumn.OperationTypes.Min;
            UnCheckTotalMenu();
            ((ToolStripMenuItem) sender).Checked = true;
            RefreshTotals();
        }


        private void MnuAverage(object sender, EventArgs e)
        {
            TotalOperation = DBColumn.OperationTypes.Average;
            UnCheckTotalMenu();
            ((ToolStripMenuItem) sender).Checked = true;
            RefreshTotals();
        }


        private void MnuShowTotals(object sender, EventArgs e)
        {
            if (ShowTotals == false)
            {
                ShowTotals = true;
                splitContainer1.Panel2Collapsed = false;
                ShowTotal();
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                ShowTotals = false;
            }

            ((ToolStripMenuItem) sender).Checked = ShowTotals;
        }


        private void MnuFind(object sender, EventArgs e)
        {
            if (!(DataControl == null)) DataControl.ShowFind();
        }


        private void MnuFindNext(object sender, EventArgs e)
        {
            if (!(DataControl == null)) DataControl.FindNext();
        }


        private void MnuFilter(object sender, EventArgs e)
        {
            if (!(DataControl == null)) DataControl.ShowFilter();
        }


        private void MnuDelFilter(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.DeleteFilter();
        }


        private void MnuPrintPreview(object sender, EventArgs e)
        {
            //dataGridPrinter1.PageNumber = 1;
            //dataGridPrinter1.RowCount = 0;
            //this.PrintDocument1.DefaultPageSettings = DBPageSetup.PageSettings;
            //if (PrintPreviewDialog1.ShowDialog() == DialogResult.OK)
            //{
            //}
        }


        private void MnuSelect(object sender, EventArgs e)
        {
            var f = 0;
            for (f = 0; f <= VisibleRowCount() - 1; f++) Select(f);
        }


        private void MnuPrint(object sender, EventArgs e)
        {
            //dataGridPrinter1.PageNumber = 1;
            //dataGridPrinter1.RowCount = 0;
            //this.PrintDocument1.DefaultPageSettings = DBPageSetup.PageSettings;
            //if (PrintDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    PrintDocument1.Print();
            //}
        }


        private void MnuAutoAdjust(object sender, EventArgs e)
        {
            FunctionsGrid.AutoSizeColumnsToContent(DataControl, Columns, CreateGraphics(), Font);
        }


        private void HideMenu(object sender, EventArgs e)
        {
            if (m_columnMove != -1)
            {
                HideColumn(m_columnMove);
                //datagrid.ContextMenuStrip.Items[m_columnMove].Checked = true;
            }
        }

        private void ShowMenu(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ShowColumn(((ToolStripMenuItem) sender).ImageIndex);
                ((ToolStripMenuItem) sender).Checked = false;
            }
        }

        public void ModeControls(Global.AccessMode mode)
        {
            if (DataControl != null) DataControl.ModeDBControls(datagrid.Controls, mode);
        }


        private void FillDescriptionColumns()
        {
            DataControl.Action = DBControl.DbActionTypes.Change;

            for (var f = 0; f <= DataControl.RecordCount() - 1; f++)
                UpdateAsociatedColumns(DataControl.DataTable.Rows[f], false, true);

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        public void HideColumn(string columnName)
        {
            HideColumn(Columns.GetColumnOrdinal(columnName));
        }


        public void ShowColumn(string columnName)
        {
            ShowColumn(Columns.GetColumnOrdinal(columnName));
        }


        public void HideColumn(int column)
        {
            Columns[column].Hidden = true;
        }


        public void ShowColumn(int column)
        {
            Columns[column].Hidden = false;
        }

        private void DataControl_OnReConnect()
        {
            SetAutoIncrement();
            FillDescriptionColumns();
        }

        private void SetAutoIncrement()
        {
            var f = 0;

            try
            {
                for (f = 0; f <= Columns.Count - 1; f++)
                    if (Columns[f].ColumnType == DBColumn.ColumnTypes.AutoNumericColumn)
                        DataControl.DataTable.Columns[Columns[f].FieldDB].AutoIncrementSeed =
                            Convert.ToInt64(Utils.MaxColumn(DataControl.DataTable, Columns[f].FieldDB) + 1);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public long RecordCount()
        {
            return DataControl.RecordCount();
        }


        public long Cols()
        {
            return Columns.Count;
        }


        private void cmdPageFirst_Click(object sender, EventArgs e)
        {
            DataControl.MoveFirstPage();
            UpdatePage();
        }


        private void cmdPageLast_Click(object sender, EventArgs e)
        {
            DataControl.MoveLastPage();
            UpdatePage();
        }


        private void cmdPagePrevious_Click(object sender, EventArgs e)
        {
            DataControl.MovePreviousPage();
            UpdatePage();
        }


        private void cmdPageNext_Click(object sender, EventArgs e)
        {
            DataControl.MoveNextPage();
            UpdatePage();
        }

        private void ModeRecord(object sender, EventArgs e)
        {
            FillRecord();

            if (RecordMode)
            {
                DbRecord1.Visible = false;
                datagrid.Visible = true;
                RecordMode = false;
            }
            else
            {
                DbRecord1.Visible = true;
                datagrid.Visible = false;
                RecordMode = true;
            }
        }


        private void FillRecord()
        {
            if (FilledRecord) return;

            DbRecord1 = new DBRecord();
            Controls.Add(DbRecord1);

            DbRecord1.ShowScrollBar = ShowRecordScrollBar;

            DbRecord1.Columns = Columns;
            DbRecord1.DataControl = DataControl;

            DbRecord1.Left = 0;
            DbRecord1.Top = 0;
            DbRecord1.Width = Width;
            DbRecord1.Height = Height;

            DbRecord1.Fill();

            DbRecord1.DataControl.UpdateControls(DbRecord1.Controls);
            DbRecord1.DataControl.FillComboControls(DbRecord1.Controls);

            var cmRecord = new ContextMenuStrip();
            cmRecord.Items.Add("&Modo Grid", null, ModeRecord);
            DbRecord1.ContextMenuStrip = cmRecord;

            FilledRecord = true;
        }

        public new void Resize()
        {
            if (ShowTotals)
            {
                dataGridViewTotal.Left = 0;
                dataGridViewTotal.Height = 55;
                dataGridViewTotal.Top = Height - dataGridViewTotal.Height;
                dataGridViewTotal.Width = Width;

                datagrid.Top = 0;
                datagrid.Left = 0;
                datagrid.Height = Height - dataGridViewTotal.Height;
                datagrid.Width = Width;
            }
            else
            {
                datagrid.Top = 0;
                datagrid.Left = 0;
                datagrid.Height = Height;
                datagrid.Width = Width;
            }
        }

        public void UpdateData()
        {
            datagrid.Update();
        }

        //Variables para control de expand/collapse
        private enum RowHeaderIcons
        {
            Expand = 0,
            Collapse = 1
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        internal ToolTip ToolTip1;
        private SplitContainer splitContainer1;
        private IContainer components;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                {
                    components.Dispose();
                }

            base.Dispose(disposing);
        }


        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.picRefrescar = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblPage = new System.Windows.Forms.Label();
            this.cmdPageNext = new System.Windows.Forms.Button();
            this.cmdPagePrevious = new System.Windows.Forms.Button();
            this.cmdPageFirst = new System.Windows.Forms.Button();
            this.cmdPageLast = new System.Windows.Forms.Button();
            this.datagrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTotal = new System.Windows.Forms.DataGridView();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.picRefrescar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // picRefrescar
            // 
            this.picRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picRefrescar.Image = global::FSFormControls.Properties.Resources.DBGridViewRefresh;
            this.picRefrescar.Location = new System.Drawing.Point(337, 8);
            this.picRefrescar.Name = "picRefrescar";
            this.picRefrescar.Size = new System.Drawing.Size(16, 16);
            this.picRefrescar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRefrescar.TabIndex = 18;
            this.picRefrescar.TabStop = false;
            this.ToolTip1.SetToolTip(this.picRefrescar, "Actualizar");
            this.picRefrescar.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblPage);
            this.splitContainer1.Panel1.Controls.Add(this.cmdPageNext);
            this.splitContainer1.Panel1.Controls.Add(this.cmdPagePrevious);
            this.splitContainer1.Panel1.Controls.Add(this.cmdPageFirst);
            this.splitContainer1.Panel1.Controls.Add(this.cmdPageLast);
            this.splitContainer1.Panel1.Controls.Add(this.picRefrescar);
            this.splitContainer1.Panel1.Controls.Add(this.datagrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewTotal);
            this.splitContainer1.Size = new System.Drawing.Size(360, 286);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.TabIndex = 1;
            // 
            // lblPage
            // 
            this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblPage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPage.Location = new System.Drawing.Point(182, 9);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(80, 12);
            this.lblPage.TabIndex = 17;
            this.lblPage.Text = "1/1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPage.Visible = false;
            // 
            // cmdPageNext
            // 
            this.cmdPageNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPageNext.BackColor = System.Drawing.Color.LightGray;
            this.cmdPageNext.Image = global::FSFormControls.Properties.Resources.DBGridViewNext;
            this.cmdPageNext.Location = new System.Drawing.Point(302, 8);
            this.cmdPageNext.Name = "cmdPageNext";
            this.cmdPageNext.Size = new System.Drawing.Size(16, 16);
            this.cmdPageNext.TabIndex = 16;
            this.cmdPageNext.UseVisualStyleBackColor = false;
            this.cmdPageNext.Visible = false;
            // 
            // cmdPagePrevious
            // 
            this.cmdPagePrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPagePrevious.BackColor = System.Drawing.Color.LightGray;
            this.cmdPagePrevious.Image = global::FSFormControls.Properties.Resources.DBGridViewPrevious;
            this.cmdPagePrevious.Location = new System.Drawing.Point(286, 8);
            this.cmdPagePrevious.Name = "cmdPagePrevious";
            this.cmdPagePrevious.Size = new System.Drawing.Size(16, 16);
            this.cmdPagePrevious.TabIndex = 15;
            this.cmdPagePrevious.UseVisualStyleBackColor = false;
            this.cmdPagePrevious.Visible = false;
            // 
            // cmdPageFirst
            // 
            this.cmdPageFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPageFirst.BackColor = System.Drawing.Color.LightGray;
            this.cmdPageFirst.Image = global::FSFormControls.Properties.Resources.DBGridViewFirst;
            this.cmdPageFirst.Location = new System.Drawing.Point(270, 8);
            this.cmdPageFirst.Name = "cmdPageFirst";
            this.cmdPageFirst.Size = new System.Drawing.Size(16, 16);
            this.cmdPageFirst.TabIndex = 14;
            this.cmdPageFirst.UseVisualStyleBackColor = false;
            this.cmdPageFirst.Visible = false;
            // 
            // cmdPageLast
            // 
            this.cmdPageLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPageLast.BackColor = System.Drawing.Color.LightGray;
            this.cmdPageLast.Image = global::FSFormControls.Properties.Resources.DBGridViewLast;
            this.cmdPageLast.Location = new System.Drawing.Point(318, 8);
            this.cmdPageLast.Name = "cmdPageLast";
            this.cmdPageLast.Size = new System.Drawing.Size(16, 16);
            this.cmdPageLast.TabIndex = 13;
            this.cmdPageLast.UseVisualStyleBackColor = false;
            this.cmdPageLast.Visible = false;
            // 
            // datagrid
            // 
            this.datagrid.AllowUserToOrderColumns = true;
            this.datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagrid.Location = new System.Drawing.Point(0, 0);
            this.datagrid.Name = "datagrid";
            this.datagrid.Size = new System.Drawing.Size(360, 198);
            this.datagrid.TabIndex = 1;
            // 
            // dataGridViewTotal
            // 
            this.dataGridViewTotal.AllowUserToOrderColumns = true;
            this.dataGridViewTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTotal.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewTotal.Name = "dataGridViewTotal";
            this.dataGridViewTotal.Size = new System.Drawing.Size(360, 84);
            this.dataGridViewTotal.TabIndex = 2;
            // 
            // DBGridView
            // 
            this.AllowDrop = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DBGridView";
            this.Size = new System.Drawing.Size(360, 286);
            ((System.ComponentModel.ISupportInitialize)(this.picRefrescar)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTotal)).EndInit();
            this.ResumeLayout(false);

        }

        public void EndEdit()
        {
            datagrid.EndEdit();
        }

        public void CancelEdit()
        {
            datagrid.CancelEdit();
        }

        public void BeginEdit(bool v)
        {
            datagrid.BeginEdit(v);
        }

        #endregion
    }
}