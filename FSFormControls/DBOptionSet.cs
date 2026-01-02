using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public partial class DBOptionSet : DBUserControl, ISupportInitialize
    {
        public DBOptionSet()
        {
            InitializeComponent();

            if (DesignMode)
                return;

            this.Load += DBOptionSet_Load;
        }

        private void DBOptionSet_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            dbGroupBox1.Controls.Clear();

            FlowLayoutPanel verticalPanel = new FlowLayoutPanel();
            verticalPanel.FlowDirection = FlowDirection.TopDown;
            verticalPanel.AutoScroll = true;
            verticalPanel.WrapContents = false;
            verticalPanel.Dock = DockStyle.Fill;

            int pos = 0;
            foreach (DBRadioButton item in Items)
            {
                item.AutoSize = true;
                item.Margin = new Padding(5);

                if(pos == 0)
                    item.Checked = true;

                verticalPanel.Controls.Add(item);

                pos++;
            }

            dbGroupBox1.Controls.Add(verticalPanel);
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
            get { return dbGroupBox1.SelectedIndex; }
            set { dbGroupBox1.SelectedIndex = value; }
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}