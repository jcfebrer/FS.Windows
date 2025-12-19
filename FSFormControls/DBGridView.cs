using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBGrid.bmp")]
    [ToolboxItem(true)]
    [Serializable]
    public class DBGridView : DataGridView
    {
        public DBGridView()
        {
            if (!DesignMode)
            {
                //Filas alternativas con diferente color
                this.RowsDefaultCellStyle.BackColor = Color.White;
                this.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;

                // Estilo de cabecera (quitar fondo azul al seleccionar una columna)
                this.EnableHeadersVisualStyles = false;
                this.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Control;

                // Eventos
                this.KeyDown += DBGridView_KeyDown;
                this.RowPostPaint += DataGridView1_RowPostPaint;
                this.CellDoubleClick += DBGridView_CellDoubleClick;

                if (SortedColumns == null)
                    SortedColumns = new DataGridViewColumnCollection(this);
            }
        }

        private void DBGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(DoubleClickRow != null)
                DoubleClickRow(this, e);
        }

        private void DBGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.AllowUserToDeleteRows && AllowDelete && this.DataSource is DataTable)
                {
                    //this.Rows.Remove(this.CurrentRow);
                    // Eliminamos el datarow actual del DataTable
                    DataTable data = this.DataSource as DataTable;
                    if (this.CurrentRow != null && this.CurrentRow.Index > data.Rows.Count)
                    {
                        data.Rows.RemoveAt(this.CurrentRow.Index);
                    }
                }
            }
        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DrawCounterOnRowHeader(this, e);
        }

        /// <summary>
        /// Ponemos un numero de row en la parte izquierda del DataGridView
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="e"></param>
        private void DrawCounterOnRowHeader(DataGridView grid, DataGridViewRowPostPaintEventArgs e)
        {
            var rowIdx = (e.RowIndex + 1).ToString();

            var font = new Font(FontFamily.GenericMonospace, 6);

            var centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Far;
            centerFormat.LineAlignment = StringAlignment.Center;
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth,
                e.RowBounds.Height - grid.Rows[e.RowIndex].DividerHeight);
            e.Graphics.DrawString(rowIdx, font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        /*
        * COMPATIBILIDAD CON INFRAGISTICS
        */
        private DBGridView m_childView;

        //EVENTS
        public event DoubleClickRowEventHandler DoubleClickRow;
        public delegate void DoubleClickRowEventHandler(object sender, DataGridViewCellEventArgs e);

        //private DBGridViewBandCollection m_Bands = new DBGridViewBandCollection();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        public enum UpdateModeEnum
        {
            OnCellChange,
            OnRowChange,
            OnRowLeave,
            OnRowValidated,
            OnRowEnter,
            OnRowStateChanged
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
        public DBGridViewDisplayLayout DisplayLayout { get; set; } = new DBGridViewDisplayLayout();
        public bool AllowAddNew { get; set; } = true;
        public bool AllowDelete { get; set; } = true;
        public bool Editable { get; set; } = true;
        public int DefaultDecimals { get; set; } = 2;
        public object AllowRowSummaries { get; set; }

        public bool AllowUpdate
        {
            get { return !this.ReadOnly; }
            set { this.ReadOnly = !value; }
        }

        public DataGridViewRow ActiveRow
        {
            get
            {
                return this.CurrentRow;
            }
            set
            {
                if (value != null)
                {
                    if (value.DataGridView != null)
                        value.Selected = true;
                }
                else
                {
                    if(this.CurrentRow != null)
                        this.CurrentRow.Selected = false;
                }
            }
        }

        public void UpdateData()
        {
            this.Update();
        }

        public new DataGridViewColumn SortedColumn
        {
            get { return this.SortedColumn; }
        }

        public DataGridViewColumnCollection SortedColumns { get; set; }

/*
* HASTA AQUI COMPATIBILIDAD CON INFRAGISTICS
*/

        public new void Select()
        {
            this.Select();
        }


        public void Select(int row)
        {
            this.Rows[row].Selected = true;
        }

        public void SetDataBinding(DataTable dataSource, string dataMember)
        {
            this.DataSource = dataSource;
            this.DataMember = dataMember;
        }

        public void SetDataBinding(ArrayList dataSource, string dataMember)
        {
            this.DataSource = dataSource;
            this.DataMember = dataMember;
        }
    }
}
