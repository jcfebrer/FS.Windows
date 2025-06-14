#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading;
using System.Windows.Forms;
using FSDatabase;
using FSNetwork;
using FSLibrary;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using DateTime = System.DateTime;
using StringConverter = System.ComponentModel.StringConverter;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [DesignTimeVisible(true)]
    [DefaultProperty("Selection")]
    [ToolboxItem(true)]
    public class DBControl : DBUserControl
    {
        #region Events
        public event ChangeRecordEventHandler ChangeRecord;
        public event OnReConnectEventHandler OnReConnect;
        public event ChangeModeEventHandler ChangeMode;
        public event BeforeSaveEventHandler BeforeSave;
        public event AfterSaveEventHandler AfterSave;
        #endregion

        #region Delegates
        public delegate void AfterSaveEventHandler();
        public delegate void BeforeSaveEventHandler(ref bool Cancel);
        public delegate void ChangeModeEventHandler(Global.AccessMode Mode);
        public delegate void ChangeRecordEventHandler();
        public delegate void ColumnChangedEventHandler(object sender, DataColumnChangeEventArgs e);
        public delegate void ColumnChangingEventHandler(object sender, DataColumnChangeEventArgs e);
        public delegate void OnReConnectEventHandler();
        public delegate void PositionChangedEventHandler(object sender, EventArgs e);
        public delegate void RowChangedEventHandler(object sender, DataRowChangeEventArgs e);
        public delegate void RowChangingEventHandler(object sender, DataRowChangeEventArgs e);
        public delegate void RowDeletedEventHandler(object sender, DataRowChangeEventArgs e);
        #endregion

        #region DbActionTypes enum

        public enum DbActionTypes
        {
            Add,
            Save,
            Change,
            Fill,
            None,
            Connect,
            ReConnect
        }

        #endregion

        #region DbType enum

        public enum DbType
        {
            SQLServer,
            OleDB,
            Odbc,
            XML,
            Data
        }

        #endregion

        private static string[] m_tables;

        public static string[] Tables
        {
            get
            {
                if (m_tables == null)
                {
                    var db = new BdUtils(Global.ConnectionString);
                    m_tables = db.GetTables();
                }
                return m_tables;
            }
            set
            {
                m_tables = value;
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

        public static string[] Fields;


        public DbActionTypes Action = DbActionTypes.None;
        public bool AddError;
        public DBGridView AsociatedDBGridView;
        private IContainer components;
        public bool Connected;
        public bool ConnectError;
        public bool DeleteError;
        public bool FindError;

        private int m_DBPosition;
        private string m_Filter = "";
        private int m_FindPosition;
        private DataRow[] m_FindRows;
        private bool m_isEOF;
        private Global.AccessMode m_Mode = Global.AccessMode.WriteMode;
        private int m_Page;
        private string m_Selection = "";
        private string m_LastSelection = "";
        private string m_tableName = "";
        private string m_xmlFile = "";
        private string m_XMLName = "";
        public string RelationSQL = "";
        public string SQL = "";

        public DbType TypeDB { get; set; }

        public string XmlFile
        {
            get { return m_xmlFile; }
            set
            {
                m_xmlFile = value;
                if (!string.IsNullOrEmpty(m_xmlFile)) Text = "XML: " + m_xmlFile;
            }
        }

        public bool SaveError { get; set; }

        public bool ReadOnly { get; set; }

        public bool SaveOnChangeRecord { get; set; }

        public bool StoreInBase64Format { get; set; } = false;

        public bool Versionable { get; set; }

        public string VersionableTable { get; set; } = "";

        public string VersionableVersionField { get; set; } = "";

        public string VersionableUserField { get; set; } = "";

        public string VersionableDateField { get; set; } = "";

        public DBColumnCollection ColumnMapping { get; set; }

        public PageSettings PageSettings { get; set; }

        public DBParamCollection Parameters { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return Label1.Text; }
            set { Label1.Text = value; }
        }

        public bool AutoConnect { get; set; } = true;

        public ArrayList ArrayList { get; set; }

        public DBControl EraseDBControl { get; set; }

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

        public Global.AccessMode Mode
        {
            get
            {
                return m_Mode;
            }
            set
            {
                if (LOCK != null)
                {
                    var db = new BdUtils(Global.ConnectionString);
                    switch (value)
                    {
                        case Global.AccessMode.ReadMode:
                            LOCK.UnLock(TableName, GetField(db.PrimaryKeyName(TableName)).ToString());
                            break;
                        case Global.AccessMode.WriteMode:
                            if (LOCK.IsLock(TableName, GetField(db.PrimaryKeyName(TableName)).ToString()))
                                throw new ExceptionUtil("El usuario: " +
                                                        LOCK.LockUser(TableName, GetField(db.PrimaryKeyName(TableName)).ToString()) +
                                                        ", tiene bloqueado el registro. Fecha de bloqueo: " +
                                                        LOCK.LockDate(TableName, GetField(db.PrimaryKeyName(TableName)).ToString()));
                            else
                                LOCK.Lock(TableName, GetField(db.PrimaryKeyName(TableName)).ToString());
                            break;
                    }
                }

                m_Mode = value;

                var frm = FindForm();

                if (frm != null)
                {
                    ModeDBControls(frm.Controls, m_Mode);
                    ModeRelationDBControls(frm.Controls, m_Mode);
                }

                if (null != ChangeMode) ChangeMode(m_Mode);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool isEOF
        {
            get
            {
                if (!Connected)
                {
                    m_isEOF = true;
                }
                else
                {
                    if (RecordCount() == 0)
                        m_isEOF = true;
                    else
                        m_isEOF = false;
                }

                return m_isEOF;
            }
            set { m_isEOF = value; }
        }

        public string Selection
        {
            get { return m_Selection; }
            set
            {
                m_Selection = value;
                Text = "SQL: " + m_Selection;
            }
        }

        public string LastSelection
        {
            get { return m_LastSelection; }
            set
            {
                m_LastSelection = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int DBPosition
        {
            get { return m_DBPosition; }
            set
            {
                m_DBPosition = value;
                Go(m_DBPosition);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public DataTable DataTable { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public DataView DataView { get; set; }


        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public DataSet DataSet { get; set; }

        public string RelationDBField { get; set; } = "";

        public string RelationParentDBField { get; set; } = "";

        public DBControl RelationDataControl { get; set; }

        public string DBFieldData { get; set; } = "";

        public bool Paging { get; set; }

        public int PagingSize { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int Page
        {
            get { return m_Page; }
            set
            {
                m_Page = value;
                GoPage(m_Page);
            }
        }

        public string Filter
        {
            get { return m_Filter; }
            set
            {
                if (DataTable == null) return;
                m_Filter = value;
                try
                {
                    DataTable.DefaultView.RowFilter = m_Filter;
                }
                catch (Exception ex)
                {
                    throw new ExceptionUtil("Filtro incorrecto: " + m_Filter, ex);
                }
            }
        }

        public DBLopd LOPD { get; set; }

        public DBLock LOCK { get; set; }


        [TypeConverter(typeof(TableList))]
        [DefaultValueAttribute("")]
        [DescriptionAttribute("Selecciona una tabla de la lista")]
        public string TableName
        {
            get
            {
                if (!String.IsNullOrEmpty(Selection))
                {
                    var fromPos = Selection.ToLower().IndexOf("from");
                    var spacePos = Selection.IndexOf(" ", fromPos + 5);
                    if (fromPos > 0)
                    {
                        if (spacePos > 0)
                            m_tableName = Selection.Substring(fromPos + 5, spacePos - (fromPos + 5));
                        else
                            m_tableName = Selection.Substring(fromPos + 5);
                    }
                    else
                    {
                        m_tableName = Selection;
                    }
                }
                else
                {
                    if (DataTable != null) 
                        m_tableName = DataTable.TableName;
                }

                return m_tableName;
            }
            set { m_tableName = value; }
        }

        public void Connect(DataTable dataTable)
        {
            TypeDB = DbType.Data;
            DataTable = dataTable;
            Connect();
        }

        public void Connect(DataView dataView)
        {
            TypeDB = DbType.Data;
            DataView = dataView;
            DataTable = dataView.ToTable();
            Connect();
        }

        public void Connect(DataSet dataSet)
        {
            TypeDB = DbType.Data;
            DataSet = dataSet;

            if (DataSet.Tables.Count == 0)
                throw new ExceptionUtil("DataSet sin ninguna tabla");

            DataTable = dataSet.Tables[0];
            Connect();
        }

        public void Connect(ArrayList arrayList)
        {
            TypeDB = DbType.Data;
            ArrayList = arrayList;
            DataTable = Utils.ConvertArrayListToDataTable(arrayList);
            Connect();
        }

        public void Connect(string sql)
        {
            Selection = sql;
            Connect();
        }

        public void ConnectThread()
        {
            Thread FillThread = null;
            ThreadStart FillThreadStart = Connect;

            FillThread = new Thread(FillThreadStart);
            FillThread.IsBackground = true;
            FillThread.Name = "FillThread";
            FillThread.Start();
        }


        public void ConnectReader(bool reconect)
        {
            var findForm = FindForm();

            if(!reconect)
                if (Connected && TypeDB != DbType.Data)
                    return;

            Action = DbActionTypes.Connect;

            if (m_Selection.IndexOf("?") + 1 != 0)
            {
                if (RelationDataControl == null)
                {
                    Action = DbActionTypes.None;
                    ConnectError = true;
                    throw new ExceptionUtil("RelationDataControl, no especificado. DBControl: " + Name);
                }

                RelationDataControl.Connect();
            }

            if ((m_Selection == "") & (TypeDB != DbType.XML && TypeDB != DbType.Data))
            {
                Action = DbActionTypes.None;
                ConnectError = true;
                throw new ExceptionUtil("Debes indicar la cadena SQL en la propiedad 'Selection'.");
            }

            isEOF = false;

            Connected = true;

            SQL = Selection;

            if (findForm != null)
            {
                FillRecordControls(findForm.Controls);

                UpdateControls(findForm.Controls);

                FillGridViewControls(findForm.Controls);
                FillComboControls(findForm.Controls);
                FillListBoxControls(findForm.Controls);
                FillListViewControls(findForm.Controls);

                if (TypeDB != DbType.XML && TypeDB != DbType.Data)
                {
                    AssignMaxSize(findForm.Controls);
                    AssignMaxValue(findForm.Controls);
                }
            }

            TabStop = false;

            Action = DbActionTypes.None;

            ErrorProvider1.ContainerControl = findForm;
            ErrorProvider1.DataSource = DataTable;

            if (findForm != null) UpdateRelationDBControls(findForm.Controls, false, null);

            ConnectError = false;
        }

        public void Connect()
        {
            Connect(false);
        }

        public void Connect(bool reconnect)
        {
            var findForm = FindForm();

            if (!reconnect)
                if (Connected && TypeDB != DbType.Data)
                    if (m_LastSelection == m_Selection)
                        return;

            var strSQL = reemplazaMacros(m_Selection);


            if (TypeDB == DbType.XML && m_xmlFile == "")
            {
                Action = DbActionTypes.None;
                ConnectError = true;
                throw new ExceptionUtil("Fichero XML (propiedad xmlFile), no indicado.");
            }
            else if (TypeDB == DbType.Data && DataTable == null && DataView == null)
            {
                Action = DbActionTypes.None;
                ConnectError = true;
                throw new ExceptionUtil("Propiedad 'DataTable/DataView', no indicada.");
            }

            Action = DbActionTypes.Connect;

            if (strSQL.IndexOf("?") > 0)
            {
                if (RelationDataControl == null)
                {
                    Action = DbActionTypes.None;
                    ConnectError = true;
                    throw new ExceptionUtil("RelationDataControl, no especificado. DBControl: " + Name);
                }

                RelationDataControl.Connect();
            }


            if ((strSQL == "") & (TypeDB != DbType.XML && TypeDB != DbType.Data))
            {
                Action = DbActionTypes.None;
                ConnectError = true;
                throw new ExceptionUtil("Debes indicar la cadena SQL en la propiedad 'Selection'.");
            }

            isEOF = false;


            //if (TypeDB != DbType.XML && TypeDB != DbType.Data)
            //    if (Paging)
            //    {
            //        if (PagingTable == "")
            //        {
            //            Action = DbActionTypes.None;
            //            Cursor.Current = Cursors.Default;
            //            ConnectError = true;
            //            throw new ExceptionUtil("Parámetro 'PagingTable', no especificado.");
            //        }

            //        if (PagingSize < 1)
            //        {
            //            Action = DbActionTypes.None;
            //            Cursor.Current = Cursors.Default;
            //            ConnectError = true;
            //            throw new ExceptionUtil("Parámetro 'PagingSize', no especificado.");
            //        }

            //        if (!string.IsNullOrEmpty(PagingOrderField))
            //            Selection = "select * from ( select top " + PagingSize + " * from (select top " +
            //                        PagingSize * (m_Page + 1) + " * from " + PagingTable + " order by " +
            //                        PagingOrderField + ") p order by " + PagingOrderField + " desc) q order by " +
            //                        PagingOrderField;
            //        else
            //            Selection = "select * from ( select top " + PagingSize + " * from (select top " +
            //                        PagingSize * (m_Page + 1) + " * from " + PagingTable +
            //                        " order by 1) p order by 1 desc) q order by 1";
            //    }


            switch (TypeDB)
            {
                case DbType.XML:
                    try
                    {
                        if (m_xmlFile.Substring(0, 7).ToLower() == "http://")
                            DataTable.ReadXml(Http.GetXMLTextReader(m_xmlFile));
                        else
                            DataTable.ReadXml(XmlFile);
                    }
                    catch (Exception e)
                    {
                        Cursor.Current = Cursors.Default;
                        Application.DoEvents();
                        Action = DbActionTypes.None;
                        ConnectError = true;
                        throw new ExceptionUtil("Fichero XML: " + m_xmlFile, e);
                    }

                    break;
                case DbType.Data:
                    break;
                case DbType.Odbc:
                case DbType.OleDB:
                case DbType.SQLServer:
                    var db = new BdUtils(Global.ConnectionString);

                    string sql = ReplaceParameters(Selection);

                    if (Paging)
                        DataTable = db.Execute(sql, m_Page, PagingSize);
                    else
                        DataTable = db.Execute(sql);

                    LastSelection = sql;

                    DataTable.TableName = "Dt" + Name;
                    DataView = new DataView(DataTable);

                    break;
            }

            Connected = true;

            SQL = Selection;

            if (RecordCount() == 0)
                isEOF = true;


            if (findForm != null)
            {
                FillRecordControls(findForm.Controls);

                UpdateControls(findForm.Controls);

                FillGridViewControls(findForm.Controls);
                FillComboControls(findForm.Controls);
                FillListBoxControls(findForm.Controls);
                FillListViewControls(findForm.Controls);

                if (TypeDB != DbType.XML && TypeDB != DbType.Data)
                {
                    AssignMaxSize(findForm.Controls);
                    AssignMaxValue(findForm.Controls);
                }
            }

            TabStop = false;

            if (LOCK != null) LOCK.ClearUser(TableName);


            Action = DbActionTypes.None;

            ErrorProvider1.ContainerControl = findForm;
            ErrorProvider1.DataSource = DataTable;

            if (findForm != null)
                UpdateRelationDBControls(findForm.Controls, false, null);

            DataTable.AcceptChanges();

            ConnectError = false;
        }

        private string ReplaceParameters(string sql)
        {
            if (Parameters is null)
                return sql;

            foreach(DBParam param in Parameters)
            {
                if (sql.IndexOf(param.Name) > 0)
                    sql = sql.Replace(param.Name, param.Value.ToString());
            }
            return sql;
        }

        public bool RelationSaveError(ControlCollection frm)
        {
            if (SaveError) return true;
            if (frm == null) return false;

            foreach (Control ctr in frm)
                if (ctr.HasChildren)
                {
                    if (RelationSaveError(ctr.Controls)) return true;
                }
                else
                {
                    if (ctr is DBControl)
                        if (((DBControl) ctr).RelationDataControl != null)
                            if (((DBControl) ctr).RelationDataControl.NameControl() == Name)
                                if (((DBControl) ctr).SaveError)
                                    return true;
                }

            return false;
        }


        public void Print()
        {
            PageSettings = PageSetup.PageSettings;
            try
            {
                var pd = new PrintDocument(this, DataTable.DefaultView, DataTable, -1, "");

                if (PageSettings != null) pd.DefaultPageSettings = PageSettings;


                var dlg = new PrintDialog();
                dlg.Document = pd;
                var result = dlg.ShowDialog();

                if (result == DialogResult.OK) pd.Print();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public void PrintPreview()
        {
            PageSettings = PageSetup.PageSettings;
            try
            {
                var pd = new PrintDocument(this, DataTable.DefaultView, DataTable, 25,
                    "Desea visualizar más p¨¢ginas antes de imprimir?");

                if (PageSettings != null) pd.DefaultPageSettings = PageSettings;

                var dlg = new frmPrintPreviewExport();
                dlg.TableDocument = pd;
                dlg.WindowState = FormWindowState.Maximized;
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        public int RecordCount()
        {
            if (Connected)
            {
                if (Paging)
                {
                    BdUtils db = new BdUtils(Global.ConnectionString);
                    return db.Counter(TableName);
                }

                return DataTable.Rows.Count;
            }

            return 0;
        }


        public int PageCount()
        {
            return (int) Math.Floor((double) RecordCount() / PagingSize);
        }

        public void Go(string field, string value)
        {
            var pos = FindPosition(field, value);
            Go(pos);
        }


        public void Go(int position)
        {
            var tempPage = 0;
            long pos;

            if (!Connected)
                return;

            //if (position == m_DBPosition)
            //    return;

            if (Paging)
            {
                tempPage = position / PagingSize;
                if (m_Page != tempPage)
                {
                    PageLoad(tempPage);
                    Go(position);
                    return;
                }
            }

            var findForm = FindForm();

            if (isEOF)
            {
                if (findForm != null)
                    ClearFields(findForm.Controls);
                return;
            }

            m_DBPosition = position;

            if (Paging)
                pos = m_DBPosition % PagingSize;
            else
                pos = m_DBPosition;

            if (SaveOnChangeRecord)
                Save();

            if (findForm != null)
            {
                UpdateControls(findForm.Controls);
                UpdateRelationDBControls(findForm.Controls, true, null);
                UpdateAsociatedDBFindTextBoxAndAsociatedCombo(findForm.Controls);
            }
              

            if (LOPD != null)
                LOPD.Save(TableName, DBLopd.Action.Show);

            if (null != ChangeRecord)
                ChangeRecord();
        }


        public void GoPage(int page)
        {
            PageLoad(page);
            Go(page * PagingSize);
        }


        public DataTable HasDataToSave()
        {
            var dt = HasDBControlDataToSave();
            if (dt != null) 
                return dt;

            var findForm = FindForm();
            if (findForm != null)
                return HasDataToSaveRelationDBControls(findForm.Controls);

            return null;
        }

        public DataTable HasDBControlDataToSave()
        {
            DataTable d_tableTemp;

            if (DataTable == null)
                return null;

            if (ReadOnly)
                return null;

            d_tableTemp = DataTable.GetChanges();


            if ((d_tableTemp != null) & !ReadOnly)
                return d_tableTemp;

            return null;
        }

        public virtual void Save()
        {
            Save(-1);
        }

        public virtual void Save(int version)
        {
            var Cancel = false;
            var v = -1;
            var findForm = FindForm();

            if (DataTable == null)
                throw new ExceptionUtil("DataTable null.");

            if (AsociatedDBGridView != null)
                if (AsociatedDBGridView.CurrentRowIndex != -1)
                    if (AsociatedDBGridView.Editable)
                        AsociatedDBGridView.Select(AsociatedDBGridView.CurrentRowIndex);

            SaveError = false;

            Global.Action = DbActionTypes.Save;

            ClearNullRow();

            if (null != BeforeSave)
                BeforeSave(ref Cancel);

            if (Cancel)
            {
                SaveError = true;
                Global.Action = DbActionTypes.None;
                return;
            }

            if (findForm != null && !CheckObligatory(findForm.Controls))
            {
                SaveError = true;
                Global.Action = DbActionTypes.None;
                throw new ExceptionUtil("Debe completar los campos obligatorios.");
            }

            if (DataTable.HasErrors)
            {
                SaveError = true;
                Global.Action = DbActionTypes.None;
                throw new ExceptionUtil("Debe revisar los errores antes de guardar los cambios.");
            }

            CopyExpressionToTable();

            if ((DataTable != null) & !ReadOnly & HaveData(DataTable))
                try
                {
                    if (RelationDataControl != null)
                        ChangeTableField(DataTable, RelationDBField,
                            RelationDataControl.GetField(RelationParentDBField, -1));


                    if (Versionable)
                    {
                        if (!string.IsNullOrEmpty(VersionableTable))
                        {
                            v = SaveVersionableData(DataTable, version);
                        }
                        else
                        {
                            SaveError = true;
                            Global.Action = DbActionTypes.None;

                            throw new ExceptionUtil("Tabla de Versionado no especificada.");
                        }
                    }

                    try
                    {
                        var db = new BdUtils(Global.ConnectionString);

                        foreach (DataRow row in DataTable.Rows)
                            switch (row.RowState)
                            {
                                case DataRowState.Added:
                                    if (db.ExecuteNonQuery(db.CommandInsert(TableName, row, Global.UserName)) == 0)
                                            throw new ExceptionUtil(
                                                "No se ha añadido ningún registro en la tabla: " + TableName);
                                    break;
                                case DataRowState.Modified:
                                    if (db.ExecuteNonQuery(db.CommandUpdate(TableName, row, Global.UserName)) == 0)
                                        throw new ExceptionUtil("No se ha actualizado ningún registro en la tabla: " +
                                                            TableName);
                                    break;
                                case DataRowState.Deleted:
                                    if (db.ExecuteNonQuery(db.CommandDelete(TableName, row)) == 0)
                                        throw new ExceptionUtil(
                                            "No se ha añadido ningún registro en la tabla: " + TableName);
                                    break;
                            }

                        //aceptamos todos los cambios
                        DataTable.AcceptChanges();

                        ReConnect();

                        if (LOPD != null)
                            LOPD.Save(TableName, DBLopd.Action.Modify);
                    }
                    catch (DBConcurrencyException e)
                    {
                        ConcurrencyError(e);

                        throw new ExceptionUtil("Error de concurrencia!");
                    }
                    catch (Exception e)
                    {
                        SaveError = true;
                        Global.Action = DbActionTypes.None;

                        throw new ExceptionUtil(e);
                    }
                }
                catch (Exception eUpdate)
                {
                    DataTable.RejectChanges();
                    SaveError = true;
                    Global.Action = DbActionTypes.None;

                    throw new ExceptionUtil(eUpdate.Message);
                }

            try
            {
                if (findForm != null)
                    SaveRelationDBControls(findForm.Controls, v);
            }
            catch (Exception eSaveRelation)
            {
                SaveError = true;
                throw new ExceptionUtil(eSaveRelation.Message);
            }

            if (null != AfterSave)
                AfterSave();

            Global.Action = DbActionTypes.None;
        }

        public bool HaveData(DataTable table)
        {
            object[] c = null;
            var f = 0;
            var s = "";

            if (table == null) 
                return false;

            foreach (DataRow r in table.Rows)
                if (r.RowState != DataRowState.Deleted)
                {
                    c = r.ItemArray;

                    for (f = c.GetLowerBound(0); f <= c.GetUpperBound(0); f++)
                        s = (s + c[f] + "|").Trim();
                }
                else
                {
                    return true;
                }

            if (s == "")
                return false;

            return true;
        }


        private void ConcurrencyError(DBConcurrencyException dbcx)
        {
            var strOriginal = "Registro Original:" + "\n";
            var strCurrent = "Registro Actual:" + "\n";
            var strPromptText = "Deseas sobreescribir el valor actual por el valor propuesto en la base de datos?" +
                                "\n";
            string strMessage;
            DialogResult response;

            for (int i = 0; i <= dbcx.Row.ItemArray.Length - 1; i++)
            {
                strOriginal += Convert.ToString(dbcx.Row[i, DataRowVersion.Original]) + Global.Tab;
                strCurrent += Convert.ToString(dbcx.Row[i, DataRowVersion.Current]) + Global.Tab;
            }

            strMessage = strOriginal + "\n";
            strMessage += strCurrent + "\n";
            strMessage += strPromptText;

            response = MessageBox.Show(strMessage, "Fallo al guardar", MessageBoxButtons.YesNo);
            processResponse(response);
        }


        private void processResponse(DialogResult response)
        {
            switch (response)
            {
                case DialogResult.Yes:
                    Save();
                    break;
                case DialogResult.No:
                    break;
            }
        }


        private void ClearNullRow()
        {
            var f = 0;
            var isnull = true;

            if (DataTable.Rows.Count == 0) return;
            if (DataTable.Rows[DataTable.Rows.Count - 1].RowState == DataRowState.Deleted) return;

            for (f = 0; f <= DataTable.Columns.Count - 1; f++)
                if ((DataTable.Rows[DataTable.Rows.Count - 1][f] != null) &
                    !(DataTable.Rows[DataTable.Rows.Count - 1][f] is Array))
                {
                    if (DataTable.Rows[DataTable.Rows.Count - 1][f] + "" != "-1")
                    {
                        isnull = false;
                        break;
                    }

                    if (AsociatedDBGridView != null)
                    {
                        if (AsociatedDBGridView.Columns.Find(DataTable.Columns[f].ColumnName) == null)
                        {
                            isnull = false;
                            break;
                        }
                    }
                    else
                    {
                        isnull = false;
                        break;
                    }
                }

            if (isnull)
            {
                DataTable.Rows[DataTable.Rows.Count - 1].RowError = "";
                DataTable.Rows[DataTable.Rows.Count - 1].Delete();
            }
        }


        private int SaveVersionableData(DataTable dtData, int version)
        {
            var db = new BdUtils(Global.ConnectionString);
            DataTable dt = null;
            var f = 0;
            var v = 0;

            dt = db.Execute("select * from " + VersionableTable);

            try
            {
                DataRow dr = null;
                var g = 0;

                for (g = 0; g <= dtData.DefaultView.Count - 1; g++)
                {
                    dr = dt.NewRow();

                    for (f = 0; f <= dt.Columns.Count - 1; f++)
                        try
                        {
                            dr[f] = dtData.DefaultView[g].Row[dt.Columns[f].ColumnName, DataRowVersion.Original];
                        }
                        catch
                        {
                        }

                    if (!string.IsNullOrEmpty(VersionableVersionField))
                    {
                        if (version != -1)
                            v = version;
                        else
                            v = LastVersion(Convert.ToString(dr[db.PrimaryKeyName(TableName)])) + 1;
                        dr[VersionableVersionField] = v;
                    }

                    if (!string.IsNullOrEmpty(VersionableDateField)) dr[VersionableDateField] = DateTime.Now;

                    if (!string.IsNullOrEmpty(VersionableUserField)) dr[VersionableUserField] = LOPDUserValue();

                    dt.Rows.Add(dr);

                    dt.AcceptChanges();
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return v;
        }

        private int SaveVersionableData(DataTable dtData)
        {
            return SaveVersionableData(dtData, -1);
        }


        private int LastVersion(string pk)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            int i;

            try
            {
                i =
                    Convert.ToInt32(
                        db.ExecuteScalar(
                            "select max(" + VersionableVersionField + ") from " + VersionableTable + " where " +
                            db.PrimaryKeyName(TableName) + " = " + pk));
            }
            catch
            {
                i = 0;
            }

            return i;
        }


        private string LOPDUserValue()
        {
            if (LOPD != null)
                return LOPD.User;
            return "Control DBLopd no definido.";
        }

        public void Clear()
        {
            DataTable.Clear();
        }

        public byte[] GetFieldByte(string fieldName)
        {
            return GetFieldByte(fieldName, -1);
        }

        public byte[] GetFieldByte(string fieldName, int pos)
        {
            if (pos == -1) pos = DBPosition;
            if (pos == -1) pos = 0;
            if (DataTable == null) return null;
            if (DataTable.Rows.Count == 0) return null;
            return (byte[]) DataTable.DefaultView[pos][fieldName];
        }

        public bool GetFieldBoolean(string fieldName)
        {
            return GetFieldBoolean(fieldName, -1);
        }

        public bool GetFieldBoolean(string fieldName, int pos)
        {
            return Convert.ToBoolean(GetField(fieldName, pos));
        }

        public object GetField(string fieldName)
        {
            return GetField(fieldName, -1);
        }

        public object GetField(string fieldName, int pos)
        {
            if (pos == -1) pos = DBPosition;
            if (pos == -1) pos = 0;
            if (isEOF || string.IsNullOrEmpty(fieldName))
                return string.Empty;
            try
            {
                if (pos > DataTable.DefaultView.Count - 1)
                    pos = DataTable.DefaultView.Count - 1;
                return DataTable.DefaultView[pos][fieldName];
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void SetField(string fieldName, object value)
        {
            SetField(fieldName, value, -1);
        }

        public void SetField(string fieldName, object value, int pos)
        {
            if (string.IsNullOrEmpty(fieldName))
                return; //throw new ExceptionUtil("Debes indicar el campo FieldName.");

            if (Connected)
                try
                {
                    if (pos == -1) pos = Convert.ToInt32(DBPosition);
                    if (pos == -1) pos = 0;
                    if (DataTable.DefaultView.AllowEdit == false) return;

                    if (DataTable.Columns.Contains(fieldName))
                        DataTable.Rows[pos][fieldName] = value;
                    else
                        throw new ExceptionUtil("Nombre de columna invalido: " + fieldName);
                }
                catch (Exception ex)
                {
                    throw new ExceptionUtil(ex);
                }
        }


        public string FieldExactName(string FieldName)
        {
            if (string.IsNullOrEmpty(FieldName)) return string.Empty;
            if (Connected)
                try
                {
                    if (DataTable.Columns[FieldName] == null)
                        throw new ExceptionUtil("Campo: " + FieldName + ". Inexistente");
                    return DataTable.Columns[FieldName].ColumnName;
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }

            return FieldName;
        }


        public bool FieldExists(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldName");

            if (Connected)
            {
                if (DataTable.Columns[fieldName] == null)
                    return false;
                return true;
            }

            return false;
        }


        public object GetFieldDefaultValue(string FieldName)
        {
            return DataTable.Columns[FieldName].DefaultValue;
        }

        public void SetFieldDefaultValue(string FieldName, object Value)
        {
            if (DataTable == null) throw new ExceptionUtil("DBControl sin incializar.");
            try
            {
                DataTable.Columns[FieldName].DefaultValue = Value;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("Imposible establece el valor por defecto al campo: " + FieldName, ex);
            }
        }


        public void AddNew()
        {
            var findForm = FindForm();
            var antAllowNew = false;
            try
            {
                if (Connected == false) throw new ExceptionUtil("DBControl, no conectado. Imposible añadir registro.");
                try
                {
                    if (DataTable.DefaultView.AllowNew == false)
                    {
                        antAllowNew = false;
                        DataTable.DefaultView.AllowNew = true;
                    }
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }

                Global.Action = DbActionTypes.Add;

                isEOF = false;

                if (RecordCount() == 0)
                    Go(0);
                else
                    Go(RecordCount() - 1);

                if (LOPD != null)
                    LOPD.Save(TableName, DBLopd.Action.Add);

                if (antAllowNew == false) DataTable.DefaultView.AllowNew = false;

                Global.Action = DbActionTypes.None;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public void Insert(DataRow row)
        {
            DataRow r;
            if (row != null)
            {
                r = DataTable.NewRow();
                r.ItemArray = row.ItemArray;
                DataTable.Rows.Add(r);
                Save();
            }
        }


        public bool Delete()
        {
            var allDele = DataTable.DefaultView.AllowDelete;
            var del = false;

            DataTable.DefaultView.AllowDelete = true;

            if (RecordCount() > 0)
                if (MessageBox.Show("¿Está seguro?", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Error) ==
                    DialogResult.Yes)
                {
                    if (EraseDBControl != null)
                    {
                        var row = DataTable.Rows[m_DBPosition];
                        EraseDBControl.Insert(row);
                    }

                    DataTable.DefaultView.Delete(m_DBPosition);

                    if (LOPD != null)
                        LOPD.Save(TableName, DBLopd.Action.Delete);

                    Save();
                    del = true;
                }


            DataTable.DefaultView.AllowDelete = allDele;

            return del;
        }


        public string NameControl()
        {
            return Name;
        }


        public void ClearFields(ControlCollection frm)
        {
            if (frm == null)
                return;

            foreach (Control ctr in frm)
            {
                if (FunctionsForms.IsContainer(ctr))
                {
                    ClearFields(ctr.Controls);
                }
                else
                {
                    if (ctr is DBTextBox)
                        if (((DBTextBox)ctr).DataControl != null)
                            if (((DBTextBox)ctr).DataControl.NameControl() == Name)
                                ((DBTextBox)ctr).Text = "";

                    if (ctr is DBLabel)
                        if (((DBLabel)ctr).DataControl != null)
                            if (((DBLabel)ctr).DataControl.NameControl() == Name)
                                ((DBLabel)ctr).Text = "";

                    if (ctr is DBFindTextBox)
                        if (((DBFindTextBox)ctr).DataControl != null)
                            if (((DBFindTextBox)ctr).DataControl.NameControl() == Name)
                                ((DBFindTextBox)ctr).Text = "";

                    if (ctr is DBDate)
                        if (((DBDate)ctr).DataControl != null)
                            if (((DBDate)ctr).DataControl.NameControl() == Name)
                                ((DBDate)ctr).Text = "";

                    if (ctr is DBCombo)
                        if (((DBCombo)ctr).DataControl != null)
                            if (((DBCombo)ctr).DataControl.NameControl() == Name)
                                ((DBCombo)ctr).Text = "";

                }
            }
        }


        public string FieldName(int column)
        {
            if (DataTable == null) 
                return string.Empty;

            try
            {
                if (DataTable.Columns.Count > 0)
                    return DataTable.Columns[column].ColumnName;
                return string.Empty;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public DataColumn GetColumn(int index)
        {
            if (DataTable != null)
                return DataTable.Columns[index];
            return null;
        }


        public DataColumn GetColumn(string name)
        {
            if (DataTable != null)
                return DataTable.Columns[name];
            return null;
        }


        public int FieldOrdinal(string columnName)
        {
            var i = 0;
            if (columnName == "") return 0;
            if (DataTable == null) return 0;
            try
            {
                i = DataTable.Columns[columnName].Ordinal;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil("Column: " + columnName + "\r\n" + e.Message);
            }

            return i;
        }


        public long FieldsCount()
        {
            if (DataTable == null) return 0;
            return DataTable.Columns.Count;
        }


        public string Find(string field, string value, string fieldName)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            var strField = "";
            DataRow[] foundRow = null;

            if (DataTable == null) throw new ExceptionUtil("DataControl no conectado.");
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (string.IsNullOrEmpty(field)) return string.Empty;
            if (string.IsNullOrEmpty(fieldName)) return string.Empty;

            try
            {
                if (db.GetField(field, TableName).Tipo.ToString().ToLower() == "string")
                    foundRow = DataTable.Select(field + "='" + value + "'");
                else
                    foundRow = DataTable.Select(field + "=" + value + "");

                if (foundRow.Length != 0) strField = foundRow[0][fieldName] + "";
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return strField;
        }


        public DataRow FindRow(string field, string value)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            DataRow[] foundRow = null;

            if (DataTable == null) return null;
            if (string.IsNullOrEmpty(value)) return null;
            if (string.IsNullOrEmpty(field)) return null;

            try
            {
                if (db.GetField(field, TableName).Tipo.ToString().ToLower() == "string")
                    foundRow = DataTable.Select(field + "='" + value + "'");
                else
                    foundRow = DataTable.Select(field + "=" + value + "");

                return foundRow[0];
            }
            catch
            {
                return null;
            }
        }


        public int FindPosition(string field, string value)
        {
            var intPos = 0;
            var foundRow = 0;

            if (DataTable == null) return 0;
            if (value == "") return 0;
            if (field == "") return 0;

            try
            {
                DataTable.DefaultView.Sort = field;
                foundRow = DataTable.DefaultView.Find(value);

                if (foundRow != -1) intPos = foundRow;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            return intPos;
        }

        public void MoveFirstPage()
        {
            PageLoad(0);
            Go(0);
        }

        public void MoveNextPage()
        {
            if (m_Page < PageCount() - 1)
            {
                PageLoad(m_Page + 1);
                Go(m_Page * PagingSize);
            }
        }


        public void MoveLastPage()
        {
            PageLoad(PageCount() - 1);
            Go(m_Page * PagingSize);
        }


        public void MovePreviousPage()
        {
            if (m_Page > 0)
            {
                PageLoad(m_Page - 1);
                Go(m_Page * PagingSize);
            }
        }


        public void MoveNext()
        {
            if (m_DBPosition < RecordCount() - 1) Go(m_DBPosition + 1);
        }

        public void MoveLast()
        {
            Go(RecordCount() - 1);
        }

        public void MoveFirst()
        {
            Go(0);
        }

        public void MovePrevious()
        {
            if (m_DBPosition > 0) Go(m_DBPosition - 1);
        }


        public void ClearErrors()
        {
            foreach (DataRow r in DataTable.Rows) r.ClearErrors();
        }


        public void CancelEdit()
        {
            if (DataTable == null)
                return;

            DataTable.RejectChanges();
            ClearErrors();

            CancelRelationDBControls(FindForm().Controls);
        }


        public void UpdateControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
            {
                if (FunctionsForms.IsContainer(ctr))
                {
                    UpdateControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBFile && ((DBFile)ctr).DataControl == null) 
                        ((DBFile)ctr).DataControl = this;
                    if (ctr is DBImage && ((DBImage)ctr).DataControl == null) 
                            ((DBImage)ctr).DataControl = this;
                    if (ctr is DBEditPicture && ((DBEditPicture)ctr).DataControl == null) 
                        ((DBEditPicture)ctr).DataControl = this;
                    if (ctr is DBCombo && ((DBCombo)ctr).DataControl == null) 
                        ((DBCombo)ctr).DataControl = this;
                    if (ctr is DBTextBox && ((DBTextBox)ctr).DataControl == null)
                        ((DBTextBox)ctr).DataControl = this;
                    if (ctr is DBLabel && ((DBLabel)ctr).DataControl == null) 
                        ((DBLabel)ctr).DataControl = this;
                    if (ctr is DBFindTextBox && ((DBFindTextBox)ctr).DataControl == null) 
                        ((DBFindTextBox)ctr).DataControl = this;
                    if (ctr is DBDate && ((DBDate)ctr).DataControl == null) 
                        ((DBDate)ctr).DataControl = this;
                    if (ctr is DBCheckBox && ((DBCheckBox)ctr).DataControl == null) 
                        ((DBCheckBox)ctr).DataControl = this;

                    if (ctr is DBTextBox && ((DBTextBox)ctr).DataControl != null && ((DBTextBox)ctr).DataControl.NameControl() == Name)
                    {
                        if (((DBTextBox)ctr).DataType == DBTextBox.TypeData.Formula)
                        {
                            if (!string.IsNullOrEmpty(((DBTextBox)ctr).Expression))
                            {
                                AddDataColumn("For" + ctr.Name, typeof(object),
                                    ((DBTextBox)ctr).Expression);
                                try
                                {
                                    ((DBTextBox)ctr).DataBindings.Add("Text", DataTable, "For" + ctr.Name);
                                }
                                catch (Exception e)
                                {
                                    throw new ExceptionUtil(e);
                                }
                            }
                        }
                    }


                    try
                    {
                        if (ctr is DBCheckBox)
                            ((DBCheckBox)ctr).UpdateCheckBox();

                        if (ctr is DBTextBox)
                        {
                            if ((((DBTextBox)ctr).AsociatedDBFindTextBox == null) &
                                (((DBTextBox)ctr).AsociatedCombo == null))
                                ((DBTextBox)ctr).UpdateText();
                        }
                        if (ctr is DBFindTextBox)
                            ((DBFindTextBox)ctr).UpdateText();
                        if (ctr is DBDate)
                            ((DBDate)ctr).UpdateText();
                        if (ctr is DBImage)
                            ((DBImage)ctr).UpdateImage();
                        if (ctr is DBEditPicture)
                            ((DBEditPicture)ctr).UpdateImage();
                        if (ctr is DBFile)
                            ((DBFile)ctr).UpdateFile();
                        if (ctr is DBLabel)
                            ((DBLabel)ctr).UpdateText();
                    }
                    catch (Exception e)
                    {
                        throw new ExceptionUtil(e);
                    }
                }
            }
        }


        public void FillListViewControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    FillListViewControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBListView)
                        if (((DBListView) ctr).DataControl != null)
                            if (((DBListView) ctr).DataControl.NameControl() == Name)
                                ((DBListView) ctr).Fill();
                }
        }


        public void FillListBoxControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    FillListViewControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBListBox)
                        if (((DBListBox) ctr).DataControlList != null)
                        {
                            if (((DBListBox) ctr).DataControl == null)
                            {
                                if (((DBListBox) ctr).DataControlList.NameControl() == Name) ((DBListBox) ctr).Fill();
                            }
                            else
                            {
                                if (((DBListBox) ctr).DataControl.NameControl() == Name) ((DBListBox) ctr).Fill();
                            }
                        }
                }
        }

        public void FillComboControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    FillComboControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBCombo)
                        if (((DBCombo) ctr).DataControlList != null)
                        {
                            if (((DBCombo) ctr).DataControl == null)
                            {
                                if (((DBCombo) ctr).DataControlList.NameControl() == Name) ((DBCombo) ctr).Fill();
                            }
                            else
                            {
                                if (((DBCombo) ctr).DataControl.NameControl() == Name) ((DBCombo) ctr).Fill();
                            }
                        }
                }
        }

        private void FillGridViewControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    FillGridViewControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBGridView)
                        if (((DBGridView) ctr).DataControl != null)
                            if (((DBGridView) ctr).DataControl.NameControl() == Name)
                                ((DBGridView) ctr).Fill();
                }
        }


        private void FillRecordControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (ctr is DBPanel | ctr is Panel | ctr is TabControl | ctr is TabPage | ctr is DBTabControl |
                    ctr is DBTabPage |
                    ctr is GroupBox | ctr is DBGroupBoxXP | ctr is DBGroupBoxXPList | ctr is SplitContainer |
                    ctr is SplitterPanel)
                {
                    FillRecordControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBRecord)
                    {
                        if (((DBRecord) ctr).DataControl == null)
                            throw new ExceptionUtil("DBRecord sin DataControl asignado. DBRecord: " + ctr.Name);

                        if (((DBRecord) ctr).DataControl.NameControl() == Name) ((DBRecord) ctr).Fill();
                    }
                }
        }


        private bool CheckObligatory(ControlCollection frm)
        {
            if (frm == null) return true;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    return CheckObligatory(ctr.Controls);
                }
                else
                {
                    if (ctr is DBCombo | ctr is DBFindTextBox | ctr is DBTextBox | ctr is DBDate)
                        if (ctr.Text == "")
                        {
                            var setErr = false;

                            if (ctr is DBCombo && ((DBCombo)ctr).DataControl != null && ((DBCombo)ctr).DataControl.NameControl() == Name)
                                if (((DBCombo)ctr).Obligatory)
                                    setErr = true;
                            if (ctr is DBFindTextBox && ((DBFindTextBox)ctr).DataControl != null && ((DBFindTextBox)ctr).DataControl.NameControl() == Name)
                                if (((DBFindTextBox)ctr).Obligatory)
                                    setErr = true;
                            if (ctr is DBTextBox && ((DBTextBox)ctr).DataControl != null && ((DBTextBox)ctr).DataControl.NameControl() == Name)
                                if (((DBTextBox)ctr).Obligatory)
                                    setErr = true;
                            if (ctr is DBDate && ((DBDate)ctr).DataControl != null && ((DBDate)ctr).DataControl.NameControl() == Name)
                                if (((DBDate)ctr).Obligatory)
                                    setErr = true;
                            if (setErr)
                            {
                                ctr.Focus();
                                ErrorProvider1.SetError(ctr, "Campo obligatorio.");
                                return false;
                            }
                        }
                }

            return true;
        }


        private void AssignMaxSize(ControlCollection frm)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            var s = 0;
            var f = 0;

            if (frm == null) return;

            foreach (Control ctr in frm)
            {
                if (FunctionsForms.IsContainer(ctr))
                {
                    AssignMaxSize(ctr.Controls);
                }
                else
                {
                    if (ctr is DBTextBox)
                        if (((DBTextBox)ctr).DataControl != null)
                            if (((DBTextBox)ctr).DataControl.NameControl() == Name)
                            {

                                if (((DBTextBox)ctr).MaxLength == Global.MAX_TEXT_LENGTH &&
                                    !string.IsNullOrEmpty(((DBTextBox)ctr).DBField))
                                {
                                    s = db.GetField(((DBTextBox)ctr).DBField, TableName).Tamano;
                                    if ((s != 0) & (s < Global.MAX_TEXT_LENGTH))
                                    {
                                        if (((DBTextBox)ctr).DataType == DBTextBox.TypeData.Date)
                                            s = Convert.ToInt32(Global.DATE_LENGTH);
                                        ((DBTextBox)ctr).MaxLength = s;
                                    }
                                }
                            }

                    if (ctr is DBFindTextBox)
                        if (((DBFindTextBox)ctr).DataControl != null)
                            if (((DBFindTextBox)ctr).DataControl.NameControl() == Name)
                            {

                                if (((DBFindTextBox)ctr).MaxLength == Global.MAX_TEXT_LENGTH &&
                                    !string.IsNullOrEmpty(((DBFindTextBox)ctr).DBField))
                                {
                                    s = db.GetField(((DBFindTextBox)ctr).DBField, TableName).Tamano;
                                    if ((s != 0) & (s < Global.MAX_TEXT_LENGTH))
                                    {
                                        if (((DBFindTextBox)ctr).DataType == DBTextBox.TypeData.Date)
                                            s = Convert.ToInt32(Global.DATE_LENGTH);
                                        ((DBFindTextBox)ctr).MaxLength = s;
                                    }
                                }
                            }
                }

                if (ctr is DBGridView)
                    if (((DBGridView) ctr).DataControl != null)
                        if (((DBGridView) ctr).DataControl.NameControl() == Name)
                            for (f = 0; f <= ((DBGridView) ctr).Columns.Count - 1; f++)
                                if (((DBGridView) ctr).Columns[f].MaxLength == 0 &&
                                    !string.IsNullOrEmpty(((DBGridView) ctr).DBField))
                                {
                                    s = db.GetField(((DBGridView) ctr).Columns[f].FieldDB, DataControl.TableName)
                                        .Tamano;
                                    if ((s != 0) & (s < Global.MAX_TEXT_LENGTH))
                                    {
                                        if (((DBGridView) ctr).Columns[f].ColumnType ==
                                            FSFormControls.DBColumn.ColumnTypes.DateColumn)
                                            s = Convert.ToInt32(Global.DATE_LENGTH);
                                        ((DBGridView) ctr).Columns[f].MaxLength = s;
                                    }
                                }
            }
        }


        private void AssignMaxValue(ControlCollection frm)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            var f = 0;

            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    AssignMaxValue(ctr.Controls);
                }
                else
                {
                    if (ctr is DBTextBox)
                        if (((DBTextBox)ctr).DataControl != null)
                            if (((DBTextBox)ctr).DataControl.NameControl() == Name)
                            {
                                if (ctr is DBTextBox)
                                {
                                    if (((DBTextBox)ctr).MaxValue == decimal.MaxValue &&
                                        !string.IsNullOrEmpty(((DBTextBox)ctr).DBField))
                                        ((DBTextBox)ctr).MaxValue =
                                            db.FieldMaxValue(((DBTextBox)ctr).DBField);
                                }
                            }

                    if (ctr is DBFindTextBox)
                        if (((DBFindTextBox)ctr).DataControl != null)
                            if (((DBFindTextBox)ctr).DataControl.NameControl() == Name)
                            {
                                if (((DBFindTextBox)ctr).MaxValue == decimal.MaxValue &&
                                    !string.IsNullOrEmpty(((DBFindTextBox)ctr).DBField))
                                    ((DBFindTextBox)ctr).MaxValue =
                                        db.FieldMaxValue(((DBFindTextBox)ctr).DBField);
                            }

                    if (ctr is DBGridView)
                        if (((DBGridView) ctr).DataControl != null)
                            if (((DBGridView) ctr).DataControl.NameControl() == Name)
                                for (f = 0; f <= ((DBGridView) ctr).Columns.Count - 1; f++)
                                    if (((DBGridView) ctr).Columns[f].MaxValue == decimal.MaxValue &&
                                        !string.IsNullOrEmpty(((DBGridView) ctr).DBField))
                                        ((DBGridView) ctr).Columns[f].MaxValue =
                                            db.FieldMaxValue(((DBGridView) ctr).Columns[f].FieldDB);
                }
        }


        public void UpdateRelationDBControls(ControlCollection frm, bool boolReConnect, object FieldValue)
        {
            string sel = null;
            string oldValue = null;
            string value = null;

            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    UpdateRelationDBControls(ctr.Controls, boolReConnect, null);
                }
                else
                {
                    if (ctr is DBControl)
                        if (((DBControl) ctr).RelationDataControl != null)
                            if (((DBControl) ctr).RelationDataControl.NameControl() == Name)
                                if (!string.IsNullOrEmpty(((DBControl) ctr).RelationDBField))
                                {
                                    if (string.IsNullOrEmpty(((DBControl) ctr).RelationSQL))
                                    {
                                        sel = ((DBControl) ctr).Selection;
                                        ((DBControl) ctr).RelationSQL = sel;
                                        boolReConnect = true;
                                    }
                                    else
                                    {
                                        sel = ((DBControl) ctr).RelationSQL;
                                    }

                                    oldValue = ((DBControl) ctr).GetField(((DBControl) ctr).RelationDBField).ToString();
                                    value =
                                        ((DBControl) ctr).RelationDataControl.GetField(
                                            ((DBControl) ctr).RelationParentDBField).ToString();

                                    if (string.IsNullOrEmpty(value))
                                        value = "-1";

                                    if (FieldValue != null) value = Convert.ToString(FieldValue);

                                    if (oldValue != value)
                                    {
                                        sel = sel.Replace("?", value);
                                        ((DBControl) ctr).Selection = sel;

                                        if (boolReConnect)
                                        {
                                            Action = DbActionTypes.ReConnect;
                                            ((DBControl) ctr).ReConnect();
                                            Action = DbActionTypes.None;
                                        }

                                        try
                                        {
                                            ((DBControl) ctr).SetFieldDefaultValue(
                                                ((DBControl) ctr).RelationDBField, value);
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new ExceptionUtil(
                                                "Imposible establecer el campo: " + ((DBControl) ctr).RelationDBField +
                                                ", como campo de relación.", ex);
                                        }

                                        ((DBControl) ctr).UpdateRelationDBControls(FindForm().Controls, boolReConnect,
                                            null);
                                    }
                                }
                }
        }

        public void UpdateRelationDBControls(ControlCollection frm, bool boolReConnect)
        {
            UpdateRelationDBControls(frm, boolReConnect, null);
        }


        private void SaveRelationDBControls(ControlCollection frm, int version)
        {
            if (frm == null) return;

            try
            {
                foreach (Control ctr in frm)
                    if (FunctionsForms.IsContainer(ctr))
                    {
                        SaveRelationDBControls(ctr.Controls, version);
                    }
                    else
                    {
                        if (ctr is DBControl)
                            if (((DBControl) ctr).RelationDataControl != null)
                                if (((DBControl) ctr).RelationDataControl.NameControl() == Name)
                                    ((DBControl) ctr).Save(version);
                    }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        private void SaveRelationDBControls(ControlCollection frm)
        {
            SaveRelationDBControls(frm, -1);
        }


        private DataTable HasDataToSaveRelationDBControls(ControlCollection frm)
        {
            DataTable dt = null;

            if (frm == null) return null;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    dt = HasDataToSaveRelationDBControls(ctr.Controls);
                    if (dt != null) return dt;
                }
                else
                {
                    if (ctr is DBControl)
                        if (((DBControl) ctr).RelationDataControl != null)
                            if (((DBControl) ctr).RelationDataControl.NameControl() == Name)
                            {
                                dt = ((DBControl) ctr).HasDBControlDataToSave();
                                if (dt != null) return dt;
                            }
                }

            return null;
        }


        private void CancelRelationDBControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    CancelRelationDBControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl)
                        if (((DBControl) ctr).RelationDataControl != null)
                            if (((DBControl) ctr).RelationDataControl.NameControl() == Name)
                                ((DBControl) ctr).CancelEdit();
                }
        }


        public void ModeDBControls(ControlCollection frm, Global.AccessMode AccMode)
        {
            if (frm == null)
                return;

            foreach (Control ctr in frm)
            {
                if (FunctionsForms.IsContainer(ctr))
                {
                    ModeDBControls(ctr.Controls, AccMode);
                }
                else
                {
                    if (ctr is DBCombo | ctr is DBTextBox | ctr is DBFindTextBox | ctr is DBDate | ctr is DBCheckBox |
                        ctr is DBGridView | ctr is DBImage | ctr is DBFile | ctr is DBEditPicture | ctr is DBLabel)
                    {
                        if (ctr is DBCombo && ((DBCombo)ctr).DataControl != null && ((DBCombo)ctr).DataControl.NameControl() == Name)
                            ((DBCombo)ctr).Mode = AccMode;
                        if (ctr is DBTextBox && ((DBTextBox)ctr).DataControl != null && ((DBTextBox)ctr).DataControl.NameControl() == Name)
                            ((DBTextBox)ctr).Mode = AccMode;
                        if (ctr is DBFindTextBox && ((DBFindTextBox)ctr).DataControl != null && ((DBFindTextBox)ctr).DataControl.NameControl() == Name)
                            ((DBFindTextBox)ctr).Mode = AccMode;
                        if (ctr is DBDate && ((DBDate)ctr).DataControl != null && ((DBDate)ctr).DataControl.NameControl() == Name)
                            ((DBDate)ctr).Mode = AccMode;
                        if (ctr is DBCheckBox && ((DBCheckBox)ctr).DataControl != null && ((DBCheckBox)ctr).DataControl.NameControl() == Name)
                            ((DBCheckBox)ctr).Mode = AccMode;
                        if (ctr is DBGridView && ((DBGridView)ctr).DataControl != null && ((DBGridView)ctr).DataControl.NameControl() == Name)
                            ((DBGridView)ctr).Mode = AccMode;
                        if (ctr is DBImage && ((DBImage)ctr).DataControl != null && ((DBImage)ctr).DataControl.NameControl() == Name)
                            ((DBImage)ctr).Mode = AccMode;
                        if (ctr is DBFile && ((DBFile)ctr).DataControl != null && ((DBFile)ctr).DataControl.NameControl() == Name)
                            ((DBFile)ctr).Mode = AccMode;
                        if (ctr is DBEditPicture && ((DBEditPicture)ctr).DataControl != null && ((DBEditPicture)ctr).DataControl.NameControl() == Name)
                            ((DBEditPicture)ctr).Mode = AccMode;
                        if (ctr is DBLabel && ((DBLabel)ctr).DataControl != null && ((DBLabel)ctr).DataControl.NameControl() == Name)
                            ((DBLabel)ctr).Mode = AccMode;
                    }
                }
            }
        }


        private string reemplazaMacros(string cad)
        {
            var cadR = cad;
            var date = FSLibrary.DateTimeUtil.ShortDate(DateTime.Now);
            cadR = TextUtil.Replace(cadR, "%fecha%", date);
            var time = DateTime.Now.ToShortTimeString();
            cadR = TextUtil.Replace(cadR, "%hora%", time);
            return cadR;
        }


        private void ModeRelationDBControls(ControlCollection frm, Global.AccessMode AccMode)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    ModeRelationDBControls(ctr.Controls, AccMode);
                }
                else
                {
                    if (ctr is DBControl)
                        if (((DBControl) ctr).RelationDataControl != null)
                            if (((DBControl) ctr).RelationDataControl.NameControl() == Name)
                                ((DBControl) ctr).Mode = AccMode;
                }
        }


        public void ReConnect()
        {
            Connect(true);

            if (null != OnReConnect)
                OnReConnect();
        }


        public void PageLoad(int page)
        {
            if (DataTable == null) 
                return;

            if (page == m_Page) 
                return;

            m_Page = page;

            try
            {
                var db = new BdUtils(Global.ConnectionString);
                DataTable = db.Execute(Selection, m_Page, PagingSize);
            }
            catch (Exception e)
            {
                Connected = false;
                Cursor.Current = Cursors.Default;
                Application.DoEvents();

                throw new ExceptionUtil(e);
            }
        }


        private void AddDataColumn(string ColumnName, Type ColumnType, string Expression)
        {
            DataColumn col = null;

            try
            {
                col = new DataColumn(ColumnName, ColumnType, Expression);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }


            col.AutoIncrement = false;
            col.ReadOnly = true;

            try
            {
                DataTable.Columns.Add(col);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public void SetPrimaryKey(string FieldName)
        {
            var keys = new DataColumn[21];
            var f = 0;

            if (FieldName == "") return;
            if (DataTable == null) return;

            try
            {
                foreach (var dc in DataTable.PrimaryKey)
                {
                    if (dc.ColumnName.ToUpper() == FieldName.ToUpper()) return;
                    keys[f] = dc;
                    f += 1;
                }


                keys[f] = DataTable.Columns[FieldName];
                DataTable.PrimaryKey = keys;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public string FindRelationField(string strSel)
        {
            var pos = strSel.IndexOf("?") + 1;
            var f = 0;
            string[] sel = null;

            if (pos != 0)
            {
                strSel = strSel.Replace("=", " = ");
                strSel = OneSpace(strSel);
                sel = strSel.Split(' ');

                for (f = 0; f <= sel.Length - 1; f++)
                    if (sel[f] == "?")
                        return sel[f - 2];
            }

            return string.Empty;
        }


        private string OneSpace(string str)
        {
            var c = "";
            string[] cs = null;
            var f = 0;

            cs = str.Split(' ');

            for (f = 0; f <= cs.Length - 1; f++)
                if (!string.IsNullOrEmpty(cs[f]))
                    c = c + " " + cs[f];
            return c.Trim();
        }


        private void ChangeTableField(DataTable table, string field, object value)
        {
            if (string.IsNullOrEmpty(field)) return;

            foreach (DataRow row in table.Rows)
                if (row.RowState == DataRowState.Added)
                    row[field] = value;
        }


        private void CopyExpressionToTable()
        {
            string colName = null;

            try
            {
                foreach (DataRow row in DataTable.Rows)
                    if (row.RowState != DataRowState.Deleted)
                        foreach (DataColumn col in DataTable.Columns)
                            if (!string.IsNullOrEmpty(col.Expression))
                            {
                                colName = col.ColumnName.Remove(0, 1);
                                if (FieldExists(colName)) row[colName] = row[col.ColumnName];
                            }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        private void ColumnAltered(ControlCollection frm, string column)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    ColumnAltered(ctr.Controls, column);
                }
                else
                {
                    if (ctr is DBControl)
                        if (((DBControl) ctr).RelationDataControl != null)
                            if (((DBControl) ctr).RelationDataControl.NameControl() == NameControl())
                                if (column.ToLower() == ((DBControl) ctr).RelationParentDBField.ToLower())
                                    UpdateRelationDBControls(FindForm().Controls, true, null);
                }
        }


        public DBTextBox FindBDTextBoxByDBFieldName(ControlCollection frm, string fieldName)
        {
            if (frm == null) return null;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    DBTextBox find = FindBDTextBoxByDBFieldName(ctr.Controls, fieldName);
                    if (find != null)
                        return find;
                }
                else
                {
                    if (ctr is DBTextBox)
                        if (!string.IsNullOrEmpty(((DBTextBox) ctr).DBField))
                            if (((DBTextBox) ctr).DBField.ToLower() == fieldName.ToLower())
                                return (DBTextBox) ctr;
                }

            return null;
        }


        public DBControl FindBDControl(ControlCollection frm, string name)
        {
            if (frm == null) return null;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    DBControl find = FindBDControl(ctr.Controls, name);
                    if (find != null)
                        return find;
                }
                else
                {
                    if (ctr is DBControl)
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            if (ctr.Name + "".ToLower() == name.ToLower()) return (DBControl) ctr;
                        }
                        else
                        {
                            return (DBControl) ctr;
                        }
                    }
                }

            return null;
        }


        public void UpdateAsociatedDBFindTextBoxAndAsociatedCombo(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    UpdateAsociatedDBFindTextBoxAndAsociatedCombo(ctr.Controls);
                }
                else
                {
                    if (ctr is DBTextBox)
                    {
                        if (((DBTextBox) ctr).AsociatedDBFindTextBox != null)
                        {
                            if (((DBTextBox) ctr).AsociatedDBFindTextBox.DataControl != null)
                                if (((DBTextBox) ctr).AsociatedDBFindTextBox.DataControl.NameControl() == Name)
                                    if (((DBTextBox) ctr).AsociatedDBFindTextBox.DataControlList != null)
                                        try
                                        {
                                            ctr.Text =
                                                ((DBTextBox) ctr).AsociatedDBFindTextBox.DataControlList.Find(
                                                    ((DBTextBox) ctr).AsociatedDBFindTextBox.DBFieldData,
                                                    ((DBTextBox) ctr).AsociatedDBFindTextBox.Text,
                                                    ((DBTextBox) ctr).DBField);
                                        }
                                        catch (Exception e)
                                        {
                                            throw new ExceptionUtil(e);
                                        }
                        }
                        else
                        {
                            if (((DBTextBox) ctr).AsociatedCombo != null)
                                if (((DBTextBox) ctr).AsociatedCombo.DataControlList != null)
                                    if (((DBTextBox) ctr).AsociatedCombo.DataControlList.NameControl() == Name)
                                        try
                                        {
                                            ctr.Text =
                                                ((DBTextBox) ctr).AsociatedCombo.DataControlList.Find(
                                                    ((DBTextBox) ctr).AsociatedCombo.DBFieldData,
                                                    Convert.ToString(((DBTextBox) ctr).AsociatedCombo.SelectedValue),
                                                    ((DBTextBox) ctr).DBField);
                                        }
                                        catch (Exception e)
                                        {
                                            throw new ExceptionUtil(e);
                                        }
                        }
                    }
                }
        }


        public void ShowList()
        {
            if (!Connected) return;

            var frmR = new frmListView();
            var dbc = new DBControl();
            dbc.Parent = frmR;
            dbc.Selection = Selection;

            frmR.DataControl = dbc;
            frmR.Show();
        }


        public void ShowReport()
        {
            ReportDocument report = null;

            report = new ReportDocument();

            report.Title = Name;
            report.SubTitleLeft = Convert.ToString(DateTime.Now);
            report.SubTitleRight = "Informe";
            report.FooterLeft = Global.ProjectName;
            report.Font = new Font("Arial", 10);

            report.AutoDiscover = true;
            report.DataSource = DataTable;


            var dlg = new PrintPreviewDialog();
            dlg.Document = report;
            dlg.WindowState = FormWindowState.Maximized;
            dlg.ShowDialog();
        }


        public void ShowRecord()
        {
            if (!Connected) return;

            var frmR = new frmRecord();
            var dbc = new DBControl();
            dbc.Parent = frmR;
            dbc.Selection = Selection;

            frmR.DataControl = dbc;
            frmR.Show();
        }


        public void ShowFilter()
        {
            var frm = new frmFilter();

            frm.DataControl = this;
            frm.ShowDialog();

            if (frm.Filter == "")
                DeleteFilter();
            else
                try
                {
                    Filter = frm.Filter;
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil("Filtro incorrecto: " + frm.Filter, e);
                }

            frm.Close();
        }


        public void ShowFind()
        {
            var frm = new frmFilter();

            frm.DataControl = this;
            frm.ShowDialog();

            if (frm.Filter == "")
            {
                m_FindPosition = 0;
                m_FindRows = null;
            }
            else
            {
                try
                {
                    m_FindRows = DataTable.Select(frm.Filter);

                    if ((m_FindRows != null) & (m_FindRows.Length > 0))
                    {
                        Go(m_FindRows[0]);
                        m_FindPosition = 0;
                    }
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil("Filtro de b?squeda incorrecto: " + frm.Filter, e);
                }
            }

            frm.Close();
        }


        public void Go(DataRow Row)
        {
            var booIguales = true;
            DataRow LineaDatos = null;
            DataRow LineaBusqueda = null;
            var intLinea = 0;
            var intColumna = 0;

            do
            {
                booIguales = true;

                LineaDatos = DataTable.Rows[intLinea];
                LineaBusqueda = Row;

                booIguales = true;
                for (intColumna = 0; intColumna <= DataTable.Columns.Count - 1; intColumna++)
                    try
                    {
                        if ("" + LineaDatos[intColumna] != "" + LineaBusqueda[intColumna])
                        {
                            booIguales = false;

                            break;
                        }
                    }
                    catch
                    {
                        booIguales = false;
                        break;
                    }

                if (booIguales)
                {
                    Go(intLinea);
                    break;
                }

                intLinea += 1;
            } while (intLinea != DataTable.Rows.Count);
        }


        public void FindNext()
        {
            if (m_FindRows == null) return;

            if (m_FindRows.Length == 0) return;

            if (m_FindPosition < m_FindRows.GetUpperBound(0))
            {
                if (m_FindRows != null) m_FindPosition += 1;
            }
            else
            {
                m_FindPosition = 0;
            }

            Go(m_FindRows[m_FindPosition]);
        }


        public void HideColumn(string columname)
        {
            DataTable.Columns[columname].ColumnMapping = MappingType.Hidden;
        }


        public void HideColumn(int column)
        {
            DataTable.Columns[column].ColumnMapping = MappingType.Hidden;
        }


        public void HideColumn(DataColumn column)
        {
            column.ColumnMapping = MappingType.Hidden;
        }


        public void ShowColumn(string columname)
        {
            DataTable.Columns[columname].ColumnMapping = MappingType.Element;
        }


        public void ShowColumn(int column)
        {
            DataTable.Columns[column].ColumnMapping = MappingType.Element;
        }


        public void ShowColumn(DataColumn column)
        {
            column.ColumnMapping = MappingType.Element;
        }


        public void DeleteFilter()
        {
            if (DataTable == null) return;
            Filter = "";
        }

        public void SetError(Control ctr, string msg)
        {
            ErrorProvider1.SetError(ctr, msg);
        }

        private string ReadTableFromSelect(string selec)
        {
            var s = selec.ToLower();
            if (s == "") return string.Empty;
            var p = s.IndexOf(" from ");
            if (p == -1) return string.Empty;
            p += 6;
            var l = s.IndexOf(" ", p);
            string se;
            if (l != -1)
                se = s.Substring(p, l - p);
            else
                se = s.Substring(p);

            return se;
        }
        
        public string DataTableMaxText(DataTable dataTable, string field)
        {
            var maxLengthText = "";

            if (string.IsNullOrEmpty(field))
                throw new ExceptionUtil("El campo no puede ser null, ni vacío.");

            if (dataTable == null)
                throw new ExceptionUtil("DataTable no puede ser null.");

            foreach (DataRow row in dataTable.Rows)
                if (maxLengthText.Length < row[field].ToString().Length)
                    maxLengthText = row[field].ToString();
            return maxLengthText;
        }

        #region Nested type: DBColumn

        public class DBColumn : Component
        {
            public string m_Caption;
            public string m_FieldDB;

            public DBColumn(string fieldDB, string caption)
            {
                m_FieldDB = fieldDB;
                m_Caption = caption;
            }

            public string FieldDB
            {
                get { return m_FieldDB; }
                set { m_FieldDB = value; }
            }

            public string Caption
            {
                get { return m_Caption; }
                set { m_Caption = value; }
            }
        }

        #endregion

        #region Nested type: TableList

        public class TableList : StringConverter
        {
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(Tables);
            }


            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }


            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }
        }

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        internal ErrorProvider ErrorProvider1;
        private Label Label1;


        ~DBControl()
        {
        }

        public DBControl()
        {
            InitializeComponent();

            Connected = false;

            if (ColumnMapping == null)
                ColumnMapping = new DBColumnCollection();

            if (Parameters == null)
                Parameters = new DBParamCollection();

            Visible = false;
            TabStop = false;
        }

        public DBControl(object value) : this()
        {
            if (value is ArrayList)
                Connect((ArrayList)value);
            if (value is DataSet)
                Connect((DataSet)value);
            if (value is DataTable)
                Connect((DataTable)value);
            if (value is DataView)
                Connect((DataView)value);
            if (value is string || value is String)
            {
                Selection = (string)value;
                Connect((string)value);
            }
        }

        public DBControl(string sql) : this()
        {
            Selection = sql;
            Connect();
        }

        public DBControl(DataView dataView) : this()
        {
            Connect(dataView);
        }

        public DBControl(DataTable dataTable) : this()
        {
            Connect(dataTable);
        }

        public DBControl(DataSet dataSet) : this()
        {
            Connect(dataSet);
        }

        public DBControl(ArrayList arrayList) : this()
        {
            Connect(arrayList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();

            if (disposing)
                if (LOCK != null)
                {
                    BdUtils db = new BdUtils(Global.ConnectionString);
                    LOCK.UnLock(TableName, GetField(db.PrimaryKeyName(TableName)).ToString());
                }

            base.Dispose(disposing);
        }


        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            Label1 = new Label();
            ErrorProvider1 = new ErrorProvider(components);
            ((ISupportInitialize) ErrorProvider1).BeginInit();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.BackColor = Color.FromArgb(255, 255, 192);
            Label1.BorderStyle = BorderStyle.FixedSingle;
            Label1.Dock = DockStyle.Fill;
            Label1.Location = new Point(0, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(150, 133);
            Label1.TabIndex = 0;
            Label1.Text = "DBControl";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ErrorProvider1
            // 
            ErrorProvider1.ContainerControl = this;
            // 
            // DBControl
            // 
            Controls.Add(Label1);
            Name = "DBControl";
            Size = new Size(150, 133);
            ((ISupportInitialize) ErrorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}