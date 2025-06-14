using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FSFormControls
{
    public partial class DBGridViewPrintOptions : Form
    {
        public DBGridViewPrintOptions()
        {
            InitializeComponent();
        }

        public DBGridViewPrintOptions(List<string> availableFields)
        {
            InitializeComponent();

            foreach (var field in availableFields)
                chklst.Items.Add(field, true);
        }

        public string PrintTitle => txtTitle.Text;

        public bool PrintAllRows => rdoAllRows.Checked;

        public bool FitToPageWidth => chkFitToPageWidth.Checked;

        private void PrintOtions_Load(object sender, EventArgs e)
        {
            // Initialize some controls
            rdoAllRows.Checked = true;
            chkFitToPageWidth.Checked = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public List<string> GetSelectedColumns()
        {
            var lst = new List<string>();
            foreach (var item in chklst.CheckedItems)
                lst.Add(item.ToString());
            return lst;
        }
    }
}