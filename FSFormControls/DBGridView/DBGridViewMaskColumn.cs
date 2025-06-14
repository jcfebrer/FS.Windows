using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    //public class mskn
    //{
    //    static public string msk = "";
    //}

    public class DBGridViewMaskColumn : DataGridViewColumn
    {
        public DBGridViewMaskColumn(string mask)
            : base(new MaskedTextCell(mask))
        {
        }

        //public void maskA(string m)
        //{
        //    mskn.msk = m;
        //}

        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(MaskedTextCell)))
                    throw new InvalidCastException("Must be a MaskedTextCell");
                base.CellTemplate = value;
            }
        }
    }

    public class MaskedTextCell : DataGridViewTextBoxCell
    {
        private readonly string m_mask;

        public MaskedTextCell(string mask)
        {
            Style.ForeColor = Color.RoyalBlue;
            m_mask = mask;
        }

        public override Type EditType => typeof(MaskedTextEditingControl);

        public override Type ValueType => typeof(string);

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            var ctl = DataGridView.EditingControl as MaskedTextEditingControl;
            ctl.Text = (string) Value;
            ctl.Mask = m_mask;
        }

        private class MaskedTextEditingControl : MaskedTextBox, IDataGridViewEditingControl
        {
            public MaskedTextEditingControl(string mask)
            {
                Mask = mask;
            }

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
        }
    }
}