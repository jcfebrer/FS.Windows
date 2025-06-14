using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;

namespace FSFormControls {

    public partial class frmPreferences : Form {

        Config config = new Config();
        string nodeToEdit = "";

        public frmPreferences() {
            InitializeComponent();
        }

        private List<KeyValuePair<string, string>> m_DescriptionList = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> DescriptionList
        {
            get { return m_DescriptionList; }
            set { m_DescriptionList = value; }
        }

        /// <summary>
        /// Lista de secciones a utilizar
        /// </summary>
        private List<string> m_Sections = new List<string>();
        public List<string> Sections {
            get { return m_Sections; }
            set { m_Sections = value; } 
        }

        private void LoadTree(string section) {
            try {
                config.Section = section;

                tvNodes.Nodes.Clear();
                tvNodes.ShowNodeToolTips = true;

                TreeNode root = new TreeNode("Configuración");
                tvNodes.Nodes.Add(root);

                FSSettingsCollection propertiesList = config.ReadProperties();

                foreach (FSSettingsElement keys in propertiesList) {
                    TreeNode eltNode = new TreeNode(keys.Key);

                    string tooltipText = DescriptionList.Find(x => x.Key == keys.Key).Value;
                    if (string.IsNullOrEmpty(tooltipText))
                        tooltipText = keys.Desc;

                    eltNode.ToolTipText = tooltipText;

                    root.Nodes.Add(eltNode);
                }
                tvNodes.ExpandAll();
            } catch { }
        }

        private void tvNodes_AfterSelect(object sender, TreeViewEventArgs e) {

            //Si es el primer nodo, no lo mostramos
            if (e.Node.Level == 0)
            {
                nodeToEdit = "";
                txtValue.Text = "";
                btnUpdate.Enabled = false;
                return;
            }

            btnUpdate.Enabled = true;

            nodeToEdit = e.Node.Text;

            txtValue.Text = config.ValueProperty(nodeToEdit);
        }

        private void btnUpdate_Click(object sender, EventArgs e) {

            config.SaveConfigFile();

            MessageBox.Show("Configuración guardada.", config.Section, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            //Rellenamos el combo con las diferentes secciones del app.config
            comboSecciones.Items.Clear();
            comboSecciones.DropDownStyle = ComboBoxStyle.DropDownList;
            if (Sections.Count == 0)
            {
                Sections = config.GetSections();
            }

            foreach (string section in Sections)
            {
                comboSecciones.Items.Add(section);
            }
            comboSecciones.Items.Add("appSettings");
            comboSecciones.SelectedIndex = 0;

            LoadTree(comboSecciones.Items[0].ToString());
        }

        private void comboSecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            nodeToEdit = "";
            txtValue.Text = "";
            LoadTree(comboSecciones.SelectedItem.ToString());
        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            if (nodeToEdit != "")
                config.SetProperty(nodeToEdit, txtValue.Text);
        }
    }
}