using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    [Serializable]
    public partial class DBOptionSet : DBUserControl, ISupportInitialize
    {
        public DBOptionSet()
        {
            InitializeComponent();

            this.Load += DBOptionSet_Load;
        }

        private void DBOptionSet_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            listBox1.Items.Clear();
            foreach (DBRadioButton item in Items)
            {
                listBox1.Items.Add(item.DisplayText);
            }
        }

        DBRadioButtonCollection _Items = new DBRadioButtonCollection();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DBRadioButtonCollection Items {
            get { return _Items; }
            set { _Items = value; }
        }

        public new BorderStyle BorderStyle
        {
            get { return dbGroupBox1.BorderStyle; }
            set { dbGroupBox1.BorderStyle = value; }
        }

        public int ItemSpacingVertical { get; set; }

        public int TextIndentation { get; set; }

        public Point ItemOrigin { get; set; }

        public int CheckedIndex {
            get { return listBox1.SelectedIndex; }
            set { 
                if(listBox1.Items.Count > 0)
                    listBox1.SelectedIndex = value; 
            }
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}