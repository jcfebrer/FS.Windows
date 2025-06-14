using System;
using System.Drawing;
using System.Windows.Forms;
using FSDatabase;
using FSLibrary;
using FSException;

namespace FSFormControls
{
    public class FunctionsGrid
    {
        public static void ColumnsSetSize(DBColumnCollection columns, DBControl datacontrol, bool force)
        {
            for (var f = 0; f <= columns.Count - 1; f++)
            {
                var dbc = columns[f];
                if (dbc.HeaderCaption == "") dbc.HeaderCaption = TextUtil.PCase(dbc.FieldDB);

                if ((dbc.Width == 0) | force)
                {
                    var db = new BdUtils(Global.ConnectionString);
                    dbc.Width = db.GetField(dbc.FieldDB, datacontrol.TableName).Tamano * Global.CARACTER_SIZE;
                    if (dbc.ColumnType == DBColumn.ColumnTypes.DescriptionColumn)
                    {
                        if (dbc.ColumnDBControl == null)
                        {
                            if ((dbc.AsociatedButtonColumn == -1) & (dbc.AsociatedComboColumn == -1))
                                throw new ExceptionUtil("[" + dbc.FieldDB +
                                                        "] DBColumn sin ColumnDBControl asociado, ó DBColumn sin AsociatedButtonColumn/AsociatedComboColumn definido.");

                            if (dbc.AsociatedButtonColumn != -1)
                                dbc.ColumnDBControl = columns[dbc.AsociatedButtonColumn].ColumnDBControl;
                            else
                                dbc.ColumnDBControl = columns[dbc.AsociatedComboColumn].ColumnDBControl;
                        }

                        dbc.Width = db.GetField(dbc.FieldDB, dbc.ColumnDBControl.TableName).Tamano *
                                    Global.CARACTER_SIZE;
                    }

                    if (dbc.ColumnType == DBColumn.ColumnTypes.ComboColumn)
                    {
                        if (dbc.ColumnDBControl == null)
                            throw new ExceptionUtil("[" + dbc.FieldDB + "] DBColumn sin ColumnDBControl asociado.");
                        dbc.Width = db.GetField(dbc.ComboListField, dbc.ColumnDBControl.TableName).Tamano *
                                    Global.CARACTER_SIZE;
                    }

                    if (dbc.Width < dbc.HeaderCaption.Length * Global.CARACTER_SIZE)
                        dbc.Width = dbc.HeaderCaption.Length * Global.CARACTER_SIZE;
                    if (dbc.Width > Global.MAX_COLUMN_WIDTH) dbc.Width = Global.MAX_COLUMN_WIDTH;
                }
            }
        }


        public static void ColumnsSetSize(DBColumnCollection columns, DBControl datacontrol)
        {
            ColumnsSetSize(columns, datacontrol, false);
        }


        public static void AutoSizeColumnsToContent(DBControl dc, DBColumnCollection col, Graphics g, Font fon)
        {
            var nRowsToScan = 0;

            try
            {
                nRowsToScan = dc.DataTable.Rows.Count;

                var iWidth = 0;
                var iW = 0;
                var iCurrCol = 0;
                string sField = null;

                for (iCurrCol = 0; iCurrCol <= col.Count - 1; iCurrCol++)
                    if (col[iCurrCol].Width == 0)
                    {
                        if (col[iCurrCol].ColumnType == DBColumn.ColumnTypes.FormulaColumn)
                            sField = "_" + col[iCurrCol].FieldDB;
                        else
                            sField = col[iCurrCol].FieldDB;


                        var dataColumn = dc.DataTable.Columns[sField];

                        iWidth = Convert.ToInt32(g.MeasureString(col[iCurrCol].HeaderCaption, fon).Width);

                        if (col[iCurrCol].ColumnType == DBColumn.ColumnTypes.ComboColumn)
                        {
                            if (col[iCurrCol].ColumnDBControl.Connected == false)
                                col[iCurrCol].ColumnDBControl
                                    .Connect(); //throw new ExceptionUtil("DBControl no conectado.");

                            iW =
                                Convert.ToInt32(
                                    g.MeasureString(
                                        dc.DataTableMaxText(col[iCurrCol].ColumnDBControl.DataTable,
                                            col[iCurrCol].ComboListField), fon).Width);
                            if (iW > iWidth) iWidth = iW;
                        }


                        string dataValue = null;
                        object objectData = null;
                        var iRow = 0;
                        for (iRow = 0; iRow <= nRowsToScan - 1; iRow++)
                            if (!string.IsNullOrEmpty(sField))
                            {
                                if (Utils.FieldExists(dc.DataTable, sField))
                                    objectData = dc.DataTable.Rows[iRow][sField];

                                if (Convert.IsDBNull(objectData) | (objectData == null)) objectData = "";

                                if (!(objectData is Array))
                                {
                                    dataValue = Convert.ToString(objectData);
                                    dataValue = dataValue + "0000";

                                    var iColWidth = Convert.ToInt32(g.MeasureString(dataValue, fon).Width);
                                    iWidth = Math.Max(iWidth, iColWidth);
                                }
                            }

                        col[iCurrCol].Width = iWidth + 1;
                    }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
            finally
            {
                g.Dispose();
            }
        }

        public static void GenerateColumns(DBControl dataControl, DBColumnCollection columns, int decimals,
            bool autoSize, Graphics graphics, Font font)
        {
            var f = 0;
            DBColumn column = null;

            if(dataControl == null)
            {
                throw new ExceptionUtil("DBControl es null.");
            }

            if (columns.Count == 0)
            {
                if (dataControl.ColumnMapping.Count == 0)
                {
                    for (f = 0; f <= Convert.ToInt32(dataControl.FieldsCount() - 1); f++)
                    {
                        if (dataControl.TypeDB == DBControl.DbType.Odbc ||
                            dataControl.TypeDB == DBControl.DbType.OleDB ||
                            dataControl.TypeDB == DBControl.DbType.SQLServer)
                        {
                            var db = new BdUtils(Global.ConnectionString);
                            column = new DBColumn(dataControl.FieldName(f), TextUtil.PCase(dataControl.FieldName(f)),
                                FunctionsForms.ConvertFieldTypeToColumnType(db.GetField(dataControl.FieldName(f),
                                    dataControl.TableName).Tipo));

                            if (column.FieldDB.ToLower() == db.PrimaryKeyName(dataControl.TableName).ToLower())
                                column.ReadColumn = true;
                        }
                        else
                        {
                            column = new DBColumn(dataControl.FieldName(f), TextUtil.PCase(dataControl.FieldName(f)));
                        }

                        column.Decimals = decimals;
                        columns.Add(column);
                    }
                }
                else
                {
                    columns.Clear();
                    for (f = 0; f <= dataControl.ColumnMapping.Count - 1; f++)
                        columns.Add(dataControl.ColumnMapping[f].FieldDB, dataControl.ColumnMapping[f].HeaderCaption);
                }
            }

            if (autoSize)
                AutoSizeColumnsToContent(dataControl, columns, graphics, font);
            else
                ColumnsSetSize(columns, dataControl, false);
        }
    }
}