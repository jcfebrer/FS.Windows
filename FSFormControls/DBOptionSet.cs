using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public partial class DBOptionSet : DBUserControl, ISupportInitialize
    {
        public DBOptionSet()
        {
            InitializeComponent();
        }

        public DBRadioButtonCollection Items { get; set; }

        public new BorderStyle BorderStyle
        {
            get { return dbGroupBox1.BorderStyle; }
            set { dbGroupBox1.BorderStyle = value; }
        }

        public int ItemSpacingVertical { get; set; }

        public int TextIndentation { get; set; }

        public Point ItemOrigin { get; set; }

        public int CheckedIndex { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}