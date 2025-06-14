#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBListView.bmp")]
    [ToolboxItem(true)]
    public class DBListView : DBUserControl
    {
        private readonly DBListViewColumnSorter lvwColumnSorter;

        private bool m_CanDelete;
        private string m_XMLName;


        public DBListView()
        {
            InitializeComponent();

            m_CanDelete = true;

            ListView1.Clear();

            ListView1.KeyDown += ListView1_KeyDown;
            ListView1.Click += ListView1_Click;
            ListView1.ColumnClick += ListView1_ColumnClick;
            ListView1.DoubleClick += ListView1_DoubleClick;
            ListView1.MouseDown += ListView1_MouseDown;
            ListView1.MouseUp += ListView1_MouseUp;
            ListView1.ItemSelectionChanged += ListView1_ItemSelectionChanged;
            ListView1.ItemCheck += ListView1_ItemCheck;
            ListView1.ItemChecked += ListView1_ItemChecked;

            lvwColumnSorter = new DBListViewColumnSorter();
            ListView1.ListViewItemSorter = lvwColumnSorter;
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

        public bool CanDelete
        {
            get
            {
                var canDeleteReturn = false;
                canDeleteReturn = m_CanDelete;
                return canDeleteReturn;
            }
            set { m_CanDelete = value; }
        }

        public string XMLName
        {
            get
            {
                string xMLNameReturn = null;
                xMLNameReturn = m_XMLName;
                return xMLNameReturn;
            }
            set { m_XMLName = value; }
        }

        public bool CheckBoxes
        {
            get { return ListView1.CheckBoxes; }
            set { ListView1.CheckBoxes = value; }
        }

        public ListView.SelectedListViewItemCollection SelectedItems => ListView1.SelectedItems;

        public ListView.CheckedListViewItemCollection CheckedItems => ListView1.CheckedItems;

        public ListView.CheckedIndexCollection CheckedIndices => ListView1.CheckedIndices;

        public ListView.ListViewItemCollection Items => ListView1.Items;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        public event ItemCheckedEventHandler ItemChecked;

        public event ItemCheckEventHandler ItemCheck;

        public event ListViewItemSelectionChangedEventHandler ItemSelectionChanged;

        public new event ClickEventHandler Click;

        public new event DoubleClickEventHandler DoubleClick;

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                    if (!DataControl.isEOF)
                        if (m_CanDelete)
                            if (MessageBox.Show("¿Estás seguro?", "Eliminar", MessageBoxButtons.YesNo) ==
                                DialogResult.Yes)
                            {
                                DataControl.Delete();
                                if (!DataControl.DeleteError)
                                {
                                    DataControl.Go(0);
                                    Fill();
                                }
                            }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public void AddField(string fieldDB, string fieldName, int size, HorizontalAlignment alignment)
        {
            ListView1.Columns.Add(fieldName, size, alignment);
        }


        public void AddField(string fieldDB, string fieldName, int size)
        {
            AddField(fieldDB, fieldName, size, 0);
        }


        public void Fill()
        {
            var f = 0;
            //int r = 0;
            //int c = 0;
            //int t = 0;
            //string strField = null;

            try
            {
                var subitem = new ListViewItem();

                if (DataControl == null) throw new ExceptionUtil("[" + Name + "] - DataControl, no especificado.");
                if (!DataControl.Connected) return;

                if (DataControl.isEOF) return;

                ListView1.Columns.Clear();
                ListView1.Columns.Add(" ", 30, HorizontalAlignment.Center);

                ListView1.BeginUpdate();

                for (f = 0; f <= DataControl.DataTable.Columns.Count - 1; f++)
                {
                    var ha = HorizontalAlignment.Left;

                    if (FSDatabase.Utils.ToDataBaseType(DataControl.DataTable.Columns[f].DataType) == "int")
                        ha = HorizontalAlignment.Right;

                    AddField(DataControl.DataTable.Columns[f].ColumnName,
                        DataControl.DataTable.Columns[f].ColumnName, 100, ha);
                }

                ListView1.Items.Clear();
                DataControl.MoveFirst();

                var frm = FindForm();
                DBForm frmForm = null;
                if (frm is DBForm)
                {
                    frmForm = (DBForm) frm;

                    //frmForm.ProgressReset();
                    //frmForm.ProgressStartPoint = 0;
                    //frmForm.ProgressEndPoint = DataControl.RecordCount();
                }

                var t = 0;
                for (var r = 0; r <= Convert.ToInt32(DataControl.RecordCount() - 1); r++)
                {
                    subitem = new ListViewItem("", r + 1);

                    foreach (ColumnHeader col in ListView1.Columns)
                        if (col.Text.Trim() != "")
                        {
                            var strField = DataControl.GetField(col.Text).ToString();
                            subitem.SubItems.Add(strField);
                        }

                    ListView1.Items.Add(subitem);

                    DataControl.MoveNext();
                    t = t + 1;

                    //if (frmForm != null) 
                    //    frmForm.ProgressStep();
                }

                ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                ListView1.EndUpdate();

                DataControl.MoveFirst();

                DataControl.Go(0);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (null != ItemSelectionChanged) ItemSelectionChanged(this, e);
        }

        private void ListView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (null != ItemChecked) ItemChecked(this, e);
        }

        private void ListView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (null != ItemCheck) ItemCheck(this, e);
        }


        private void ListView1_Click(object sender, EventArgs e)
        {
            if (MousePosition.Y != 0)
                if (MousePosition.X == 0)
                    Fill();
            if (null != Click) Click(this, e);
        }


        public void Clear()
        {
            ListView1.Clear();
        }


        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                    lvwColumnSorter.Order = SortOrder.Descending;
                else
                    lvwColumnSorter.Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            ListView1.Sort();
        }


        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (null != DoubleClick) DoubleClick(this, e);
        }

        public string get_RowValue()
        {
            return get_RowValue(0);
        }

        public string get_RowValue(int column)
        {
            return ListView1.SelectedItems[0].SubItems[column].Text;
        }


        private void ListView1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void ListView1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region Delegates

        public delegate void ClickEventHandler(object sender, EventArgs e);

        public delegate void DoubleClickEventHandler(object sender, EventArgs e);

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        private ListView ListView1;


        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ListView1 = new ListView();
            SuspendLayout();
            // 
            // ListView1
            // 
            ListView1.AllowColumnReorder = true;
            ListView1.Dock = DockStyle.Fill;
            ListView1.FullRowSelect = true;
            ListView1.GridLines = true;
            ListView1.LabelEdit = true;
            ListView1.Location = new Point(0, 0);
            ListView1.MultiSelect = false;
            ListView1.Name = "ListView1";
            ListView1.Size = new Size(149, 153);
            ListView1.TabIndex = 0;
            ListView1.UseCompatibleStateImageBehavior = false;
            ListView1.View = View.Details;
            // 
            // DBListView
            // 
            Controls.Add(ListView1);
            Name = "DBListView";
            Size = new Size(149, 153);
            ResumeLayout(false);
        }

        #endregion
    }
}