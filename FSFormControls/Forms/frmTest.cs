namespace FSFormControls
{
    public partial class frmTest : DBForm
    {
        public frmTest()
        {
            InitializeComponent();

            dbCombo1.Items.Add(1, "Valor 1");
            dbCombo1.Items.Add(2, "Valor 2");
            dbCombo1.Items.Add(3, "Valor 3");
            dbCombo1.Items.Add(4, "Valor 4");
            dbCombo1.Items.Add(5, "Valor 5");
            dbCombo1.Items.Add(6, "Valor 6");
            dbCombo1.Items.Add(7, "Valor 7");
            dbCombo1.Items.Add(8, "Valor 8");
        }

        private void dbButton18_Click(object sender, System.EventArgs e)
        {
            frmTestGauge ftmT = new frmTestGauge();
            ftmT.Show();
        }

        private void dbButton19_Click(object sender, System.EventArgs e)
        {
            frmTestVuMeter ftmT = new frmTestVuMeter();
            ftmT.Show();
        }
    }
}