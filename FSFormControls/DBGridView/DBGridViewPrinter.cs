#region

using FSException;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class DBGridViewPrinter
    {
        private const int kVerticalCellLeeway = 10;
        private readonly DBColumnCollection m_Columns;
        private readonly DataGridView m_DataGrid;
        private readonly System.Drawing.Printing.PrintDocument m_PrintDocument;
        private readonly DataTable m_Table;
        public int BottomMargin;
        public int LeftMargin;
        public ArrayList Lines = new ArrayList();
        public int PageHeight;
        public int PageNumber = 1;
        public int PageWidth;
        public int RightMargin;
        public int RowCount;
        public int TopMargin;
        public int totalSize;


        public DBGridViewPrinter(DataGridView aGrid, System.Drawing.Printing.PrintDocument aPrintDocument,
            DataTable aTable, DBColumnCollection aColumns)
        {
            m_DataGrid = aGrid;
            m_PrintDocument = aPrintDocument;
            m_Table = aTable;
            m_Columns = aColumns;
        }

        public void DrawHeader(Graphics g)
        {
            var ForeBrush = new SolidBrush(m_DataGrid.ForeColor);
            var BackBrush = new SolidBrush(m_DataGrid.BackColor);
            var TheLinePen = new Pen(m_DataGrid.GridColor, 1);
            var cellformat = new StringFormat();
            cellformat.Trimming = StringTrimming.EllipsisCharacter;
            cellformat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;


            var initialRowCount = RowCount;

            float startxposition = m_DataGrid.Location.X + LeftMargin;
            var nextcellbounds = new RectangleF(0, 0, 0, 0);

            var HeaderBounds = new RectangleF(0, 0, 0, 0);

            HeaderBounds.X = m_DataGrid.Location.X + LeftMargin;
            HeaderBounds.Y = m_DataGrid.Location.Y + TopMargin +
                             (RowCount - initialRowCount) * (m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway);
            HeaderBounds.Height = m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway;
            HeaderBounds.Width = PageWidth;

            g.FillRectangle(BackBrush, HeaderBounds);

            var k = 0;
            for (k = 0; k <= m_Columns.Count - 1; k++)
                if (m_Columns[k].Hidden == false)
                {
                    var nextcolumn = m_Columns[k].HeaderCaption;
                    var columnwidth = Convert.ToSingle(PageWidth * m_Columns[k].Width / totalSize);
                    var cellbounds = new RectangleF(startxposition,
                        m_DataGrid.Location.Y + TopMargin +
                        (RowCount - initialRowCount) *
                        (m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway),
                        columnwidth,
                        m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway);
                    nextcellbounds = cellbounds;

                    switch (m_Columns[k].Alignment)
                    {
                        case HorizontalAlignment.Center:
                            cellformat.Alignment = StringAlignment.Center;
                            break;
                        case HorizontalAlignment.Left:
                            cellformat.Alignment = StringAlignment.Near;
                            break;
                        case HorizontalAlignment.Right:
                            cellformat.Alignment = StringAlignment.Far;
                            break;
                        default:
                            cellformat.Alignment = StringAlignment.Near;
                            break;
                    }


                    if (startxposition + columnwidth <= PageWidth + LeftMargin + RightMargin)
                        g.DrawString(nextcolumn, m_DataGrid.Font, ForeBrush, cellbounds, cellformat);
                    startxposition = startxposition + columnwidth;
                }

            if (m_DataGrid.CellBorderStyle != DataGridViewCellBorderStyle.None)
                g.DrawLine(TheLinePen, m_DataGrid.Location.X + LeftMargin, nextcellbounds.Bottom, PageWidth,
                    nextcellbounds.Bottom);
        }


        public bool DrawRows(Graphics g)
        {
            var lastRowBottom = TopMargin;

            try
            {
                var ForeBrush = new SolidBrush(m_DataGrid.ForeColor);
                var BackBrush = new SolidBrush(m_DataGrid.BackColor);
                var AlternatingBackBrush = new SolidBrush(m_DataGrid.AlternatingRowsDefaultCellStyle.ForeColor);
                var TheLinePen = new Pen(m_DataGrid.ForeColor, 1);
                var cellformat = new StringFormat();
                cellformat.Trimming = StringTrimming.EllipsisCharacter;
                cellformat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;

                var initialRowCount = RowCount;

                var RowBounds = new RectangleF(0, 0, 0, 0);


                var i = 0;
                var j = 0;
                for (i = initialRowCount; i <= m_Table.DefaultView.Count - 1; i++)
                {
                    var dr = m_Table.DefaultView[i].Row;
                    var startxposition = m_DataGrid.Location.X + LeftMargin;

                    RowBounds.X = m_DataGrid.Location.X + LeftMargin;
                    RowBounds.Y = m_DataGrid.Location.Y + TopMargin +
                                  (RowCount - initialRowCount + 1) *
                                  (m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway);
                    RowBounds.Height = m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway;
                    RowBounds.Width = PageWidth;
                    Lines.Add(RowBounds.Bottom);

                    if (i % 2 == 0)
                        g.FillRectangle(BackBrush, RowBounds);
                    else
                        g.FillRectangle(AlternatingBackBrush, RowBounds);

                    for (j = 0; j <= m_Columns.Count - 1; j++)
                        if (m_Columns[j].Hidden == false)
                        {
                            var columnwidth = Convert.ToSingle(PageWidth * m_Columns[j].Width / totalSize);
                            var cellbounds = new RectangleF(startxposition,
                                m_DataGrid.Location.Y + TopMargin +
                                (RowCount - initialRowCount + 1) *
                                (m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway),
                                columnwidth,
                                m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway);

                            if (startxposition + columnwidth <= PageWidth + LeftMargin + RightMargin)
                            {
                                switch (m_Columns[j].Alignment)
                                {
                                    case HorizontalAlignment.Center:
                                        cellformat.Alignment = StringAlignment.Center;
                                        break;
                                    case HorizontalAlignment.Left:
                                        cellformat.Alignment = StringAlignment.Near;
                                        break;
                                    case HorizontalAlignment.Right:
                                        cellformat.Alignment = StringAlignment.Far;
                                        break;
                                    default:
                                        cellformat.Alignment = StringAlignment.Near;
                                        break;
                                }


                                if (m_Columns[j].ColumnType == DBColumn.ColumnTypes.ComboColumn)
                                    g.DrawString(
                                        m_Columns[j].ColumnDBControl.Find(m_Columns[j].ColumnDBFieldData,
                                            Convert.ToString(dr[m_Columns[j].FieldDB]),
                                            m_Columns[j].ComboListField), m_DataGrid.Font,
                                        ForeBrush, cellbounds, cellformat);
                                else
                                    g.DrawString(dr[m_Columns[j].FieldDB].ToString(), m_DataGrid.Font, ForeBrush,
                                        cellbounds, cellformat);
                                lastRowBottom = Convert.ToInt32(cellbounds.Bottom);
                            }

                            startxposition = Convert.ToInt32(startxposition + columnwidth);
                        }

                    RowCount = RowCount + 1;

                    if (RowCount * (m_DataGrid.Font.SizeInPoints + kVerticalCellLeeway) >
                        PageHeight * PageNumber - (BottomMargin + TopMargin))
                    {
                        DrawHorizontalLines(g, Lines);
                        DrawVerticalGridLines(g, TheLinePen, lastRowBottom);
                        return true;
                    }
                }

                DrawHorizontalLines(g, Lines);
                DrawVerticalGridLines(g, TheLinePen, lastRowBottom);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        private void DrawHorizontalLines(Graphics g, ArrayList lines)
        {
            var TheLinePen = new Pen(m_DataGrid.ForeColor, 1);

            if (m_DataGrid.CellBorderStyle == DataGridViewCellBorderStyle.None) return;

            var i = 0;
            for (i = 0; i <= lines.Count - 1; i++)
                g.DrawLine(TheLinePen, m_DataGrid.Location.X + LeftMargin, Convert.ToInt64(lines[i]),
                    PageWidth + LeftMargin, Convert.ToInt64(lines[i]));
        }


        private void DrawVerticalGridLines(Graphics g, Pen TheLinePen, int bottom)
        {
            if (m_DataGrid.CellBorderStyle == DataGridViewCellBorderStyle.None) return;

            var k = 0;
            float posX = LeftMargin;
            for (k = 0; k <= m_Columns.Count - 1; k++)
                if (m_Columns[k].Hidden == false)
                {
                    var columnwidth = Convert.ToSingle(PageWidth * m_Columns[k].Width / totalSize);
                    g.DrawLine(TheLinePen, m_DataGrid.Location.X + posX, m_DataGrid.Location.Y + TopMargin,
                        m_DataGrid.Location.X + posX, bottom);
                    posX = posX + columnwidth;
                }

            g.DrawLine(TheLinePen, m_DataGrid.Location.X + posX, m_DataGrid.Location.Y + TopMargin,
                m_DataGrid.Location.X + posX, bottom);
        }


        private void DrawTopLabel(Graphics g)
        {
            if (m_DataGrid.Columns.Count > 0)
                g.DrawString(m_DataGrid.Columns[0].HeaderText, m_DataGrid.Font, new SolidBrush(m_DataGrid.ForeColor),
                    m_DataGrid.Location.X + LeftMargin, m_DataGrid.Location.Y + TopMargin - 20, new StringFormat());
        }


        public bool DrawDataGrid(Graphics g)
        {
            try
            {
                SetPage();
                CalcTotalSize();
                DrawTopLabel(g);
                DrawHeader(g);
                var bContinue = DrawRows(g);
                return bContinue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        private void CalcTotalSize()
        {
            var f = 0;
            totalSize = 0;
            for (f = 0; f <= m_Columns.Count - 1; f++)
                if (m_Columns[f].Hidden == false)
                    totalSize = Convert.ToInt32(totalSize + m_Columns[f].Width);
        }


        private void SetPage()
        {
            LeftMargin = m_PrintDocument.DefaultPageSettings.Margins.Left;
            RightMargin = m_PrintDocument.DefaultPageSettings.Margins.Right;

            if (m_PrintDocument.DefaultPageSettings.Landscape)
            {
                PageWidth = m_PrintDocument.DefaultPageSettings.PaperSize.Height;
                PageHeight = m_PrintDocument.DefaultPageSettings.PaperSize.Width - (RightMargin + LeftMargin);
            }
            else
            {
                PageWidth = m_PrintDocument.DefaultPageSettings.PaperSize.Width - (RightMargin + LeftMargin);
                PageHeight = m_PrintDocument.DefaultPageSettings.PaperSize.Height;
            }

            TopMargin = m_PrintDocument.DefaultPageSettings.Margins.Top;
            BottomMargin = m_PrintDocument.DefaultPageSettings.Margins.Bottom;
        }
    }
}