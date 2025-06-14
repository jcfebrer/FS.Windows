using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBGridViewMultipleComboBoxCell : DataGridViewTextBoxCell
    {
        public DBGridViewMultipleComboBoxCell()
        {
            Style.ForeColor = Color.RoyalBlue;
        }

        public override Type EditType => typeof(MultipleComboboxEditingControl);

        public override Type ValueType => typeof(string);

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            var ctl = DataGridView.EditingControl as MultipleComboboxEditingControl;

            ctl.Text = Convert.ToString(Value);
        }

        private class MultipleComboboxEditingControl : ComboBox, IDataGridViewEditingControl
        {
            public object EditingControlFormattedValue
            {
                get { return Text; }
                set
                {
                    if (value is string) Text = (string) value;
                }
            }

            public object GetEditingControlFormattedValue(
                DataGridViewDataErrorContexts context)
            {
                return EditingControlFormattedValue;
            }

            public void ApplyCellStyleToEditingControl(
                DataGridViewCellStyle dataGridViewCellStyle)
            {
                Font = dataGridViewCellStyle.Font;
            }

            public int EditingControlRowIndex { get; set; }

            public bool EditingControlWantsInputKey(
                Keys key, bool dataGridViewWantsInputKey)
            {
                switch (key & Keys.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Right:
                    case Keys.Home:
                    case Keys.End:
                    case Keys.PageDown:
                    case Keys.PageUp:
                        return true;
                    default:
                        return false;
                }
            }

            public void PrepareEditingControlForEdit(bool selectAll)
            {
            }

            public bool RepositionEditingControlOnValueChange => false;

            public DataGridView EditingControlDataGridView { get; set; }

            public bool EditingControlValueChanged { get; set; }


            public Cursor EditingPanelCursor => base.Cursor;

            protected override void OnLeave(EventArgs e)
            {
                EditingControlValueChanged = true;
                EditingControlDataGridView.NotifyCurrentCellDirty(true);
                base.OnTextChanged(e);
            }

            protected override void OnClick(EventArgs e)
            {
            }

            protected override void OnDoubleClick(EventArgs e)
            {
            }

            protected override void OnDisplayMemberChanged(EventArgs e)
            {
            }

            protected override void OnDataSourceChanged(EventArgs e)
            {
            }

            protected override void OnMouseClick(MouseEventArgs e)
            {
            }


            protected override void OnMouseDoubleClick(MouseEventArgs e)
            {
            }

            protected override void OnEnter(EventArgs e)
            {
            }

            protected override void OnGotFocus(EventArgs e)
            {
            }

            protected override void OnSelectedItemChanged(EventArgs e)
            {
            }

            protected override void OnSelectedValueChanged(EventArgs e)
            {
            }

            protected override void OnSelectionChangeCommitted(EventArgs e)
            {
            }
        }
    }
}