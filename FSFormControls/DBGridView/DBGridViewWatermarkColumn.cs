using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBGridViewWatermarkColumn : DataGridViewTextBoxColumn
    {
        public DBGridViewWatermarkColumn()
        {
            CellTemplate = new DataGridViewWatermarkCell();
        }

        public string WatermarkText
        {
            get
            {
                if ((DataGridViewWatermarkCell) CellTemplate == null)
                    throw new InvalidOperationException("cell template required");

                return ((DataGridViewWatermarkCell) CellTemplate).WatermarkText;
            }
            set
            {
                if (WatermarkText != value)
                {
                    ((DataGridViewWatermarkCell) CellTemplate).WatermarkText = value;

                    if (DataGridView != null)
                    {
                        var dataGridViewRows =
                            DataGridView.Rows;

                        var rowCount = dataGridViewRows.Count;
                        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                        {
                            var dataGridViewRow =
                                dataGridViewRows.SharedRow(rowIndex);

                            var cell =
                                dataGridViewRow.Cells[Index]
                                    as DataGridViewWatermarkCell;

                            if (cell != null) cell.WatermarkText = value;
                        }
                    }
                }
            }
        }
    }

    public class DataGridViewWatermarkCell : DataGridViewTextBoxCell
    {
        public string WatermarkText { get; set; }

        public override object Clone()
        {
            var cell = (DataGridViewWatermarkCell) base.Clone();
            cell.WatermarkText = WatermarkText;

            return cell;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
            Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
            object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)

        {
            if ((OwningColumn.Site == null || !OwningColumn.Site.DesignMode) &&
                (RowIndex < 0 || !IsInEditMode) && !string.IsNullOrEmpty(WatermarkText) &&
                (GetValue(rowIndex) == null || GetValue(rowIndex) == DBNull.Value))
            {
                cellStyle.Font = new Font(cellStyle.Font, FontStyle.Italic);
                cellStyle.ForeColor = Color.Gray;
                formattedValue = WatermarkText;
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                cellState, value, formattedValue, errorText,
                cellStyle, advancedBorderStyle, paintParts);
        }
    }
}