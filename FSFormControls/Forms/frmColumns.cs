#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmColumns : Form
    {
        public void LoadProperties()
        {
            cmdSelect.Enabled = false;
            cmdUnSelect.Enabled = false;
        }


        private void cmdSelect_Click(object sender, EventArgs e)
        {
            var lSelItems = new ArrayList();
            foreach (var colName in lboxNSelected.SelectedItems)
            {
                lboxSelected.Items.Add(colName);
                lSelItems.Add(colName);
            }

            foreach (var item in lSelItems) lboxNSelected.Items.Remove(item);
        }


        private void cmdUnSelect_Click(object sender, EventArgs e)
        {
            var lSelItems = new ArrayList();
            foreach (var colName in lboxSelected.SelectedItems)
            {
                lboxNSelected.Items.Add(colName);
                lSelItems.Add(colName);
            }

            foreach (var item in lSelItems) lboxSelected.Items.Remove(item);
            lSelItems = null;
        }


        private void cmdMoveUp_Click(object sender, EventArgs e)
        {
            MoveUp();
        }

        private void cmdMoveDown_Click(object sender, EventArgs e)
        {
            MoveDown();
        }


        private void MoveUp()
        {
            var sIndex = 0;

            if (lboxSelected.SelectedIndices.Count == 1)
            {
                sIndex = lboxSelected.SelectedIndex;
                if (sIndex != 0)
                {
                    var item1 = lboxSelected.SelectedItem;
                    lboxSelected.Items.Remove(lboxSelected.SelectedItem);
                    lboxSelected.Items.Insert(sIndex - 1, item1);
                    lboxSelected.SelectedIndex = sIndex - 1;
                }
            }
        }


        private void MoveDown()
        {
            var sIndex = 0;

            if (lboxSelected.SelectedIndices.Count == 1)
            {
                sIndex = lboxSelected.SelectedIndex;
                if (sIndex + 1 < lboxSelected.Items.Count)
                {
                    var item1 = lboxSelected.SelectedItem;
                    lboxSelected.Items.Remove(lboxSelected.SelectedItem);
                    lboxSelected.Items.Insert(sIndex + 1, item1);
                    lboxSelected.SelectedIndex = sIndex + 1;
                }
            }
        }

        #region '" Windows Form Designer generated code "' 

        private readonly IContainer components = null;

        internal Button cmdCancel;
        internal Button cmdMoveDown;
        internal Button cmdMoveUp;
        internal Button cmdSave;
        internal Button cmdSelect;
        internal Button cmdUnSelect;
        internal ListBox lboxNSelected;
        internal ListBox lboxSelected;

        public frmColumns()
        {
            InitializeComponent();

            cmdSelect.Click += cmdSelect_Click;
            lboxNSelected.DoubleClick += cmdSelect_Click;
            cmdUnSelect.Click += cmdUnSelect_Click;
            lboxSelected.DoubleClick += cmdUnSelect_Click;
            cmdMoveUp.Click += cmdMoveUp_Click;
            cmdMoveDown.Click += cmdMoveDown_Click;
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
            lboxNSelected = new ListBox();
            lboxSelected = new ListBox();
            cmdSelect = new Button();
            cmdUnSelect = new Button();
            cmdSave = new Button();
            cmdCancel = new Button();
            cmdMoveUp = new Button();
            cmdMoveDown = new Button();
            SuspendLayout();
            // 
            // lboxNSelected
            // 
            lboxNSelected.Location = new Point(13, 14);
            lboxNSelected.Name = "lboxNSelected";
            lboxNSelected.Size = new Size(174, 160);
            lboxNSelected.TabIndex = 7;
            // 
            // lboxSelected
            // 
            lboxSelected.Location = new Point(273, 14);
            lboxSelected.Name = "lboxSelected";
            lboxSelected.Size = new Size(174, 160);
            lboxSelected.TabIndex = 6;
            // 
            // cmdSelect
            // 
            cmdSelect.Location = new Point(193, 14);
            cmdSelect.Name = "cmdSelect";
            cmdSelect.Size = new Size(75, 23);
            cmdSelect.TabIndex = 2;
            cmdSelect.Text = "--->";
            // 
            // cmdUnSelect
            // 
            cmdUnSelect.Location = new Point(193, 42);
            cmdUnSelect.Name = "cmdUnSelect";
            cmdUnSelect.Size = new Size(75, 23);
            cmdUnSelect.TabIndex = 3;
            cmdUnSelect.Text = "<---";
            // 
            // cmdSave
            // 
            cmdSave.DialogResult = DialogResult.OK;
            cmdSave.Location = new Point(293, 184);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new Size(75, 24);
            cmdSave.TabIndex = 0;
            cmdSave.Text = "Guardar";
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Location = new Point(373, 184);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(75, 24);
            cmdCancel.TabIndex = 1;
            cmdCancel.Text = "Cancelar";
            // 
            // cmdMoveUp
            // 
            cmdMoveUp.Location = new Point(192, 104);
            cmdMoveUp.Name = "cmdMoveUp";
            cmdMoveUp.Size = new Size(75, 23);
            cmdMoveUp.TabIndex = 4;
            cmdMoveUp.Text = "Arriba";
            // 
            // cmdMoveDown
            // 
            cmdMoveDown.Location = new Point(192, 132);
            cmdMoveDown.Name = "cmdMoveDown";
            cmdMoveDown.Size = new Size(75, 22);
            cmdMoveDown.TabIndex = 5;
            cmdMoveDown.Text = "Abajo";
            // 
            // frmColumns
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(460, 218);
            Controls.Add(cmdMoveDown);
            Controls.Add(cmdMoveUp);
            Controls.Add(cmdCancel);
            Controls.Add(cmdSave);
            Controls.Add(cmdUnSelect);
            Controls.Add(cmdSelect);
            Controls.Add(lboxSelected);
            Controls.Add(lboxNSelected);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmColumns";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Orden y posición de las columnas";
            ResumeLayout(false);
        }

        #endregion
    }
}