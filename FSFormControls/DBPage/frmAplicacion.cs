#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmAplicacion : Form
    {
        public PageCollection m_Pages = new PageCollection();
        public int pos;

        public PageCollection Pages
        {
            get { return m_Pages; }
            set { m_Pages = value; }
        }

        public void Add(DBPage page)
        {
            Pages.Add(page);
        }


        public void Add(DBPage page, string title)
        {
            Pages.Add(page);
            page.Text = title;
        }


        private void ToolBar1_ButtonClick(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)e.ClickedItem;
            switch (Convert.ToString(tsb.Tag))
            {
                case "SIGUIENTE":
                    if (pos < Pages.Count - 1) Go(pos + 1);
                    break;
                case "ANTERIOR":
                    if (pos > 0) Go(pos - 1);
                    break;
                case "INICIO":
                    if(Pages.Count > 0) Go(0);
                    break;
                case "CERRAR":
                    Close();
                    break;
            }
        }


        public void Go(int page)
        {
            Panel1.Controls.Clear();
            pos = page;
            Panel1.Controls.Add(Pages[page]);
        }


        public void Go(string pageName)
        {
            DBPage pg = null;
            pg = Pages.Find(pageName);
            if (!(pg == null))
            {
                pos = pg.Index;
                Panel1.Controls.Clear();
                Panel1.Controls.Add(Pages[pg.Index]);
            }
        }


        public void Initialize()
        {
            AddContextMenuPages();

            AddProjectForms();
            AddProjectDBForms();
        }

        private void AddContextMenuPages()
        {
            var mnuPaginas = new ContextMenuStrip();

            foreach (DBPage pg in Pages) mnuPaginas.Items.Add(pg.Text, null, mnuClick);

            //ToolBar1.Items[4].DropDownMenu = mnuPaginas;
        }

        private void AddProjectForms()
        {
            TreeNode tn = TreeView1.Nodes.Add("Formularios");

            Type formType = typeof(Form);
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                if (formType.IsAssignableFrom(type))
                {
                    tn.Nodes.Add(type.Namespace + "." + type.Name, type.Name);
                }
        }

        private void AddProjectDBForms()
        {
            TreeNode tn = TreeView1.Nodes.Add("Formularios FS");

            Type formType = typeof(DBForm);
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                if (formType.IsAssignableFrom(type))
                {
                    tn.Nodes.Add(type.Namespace + "." + type.Name, type.Name);
                }
        }

        private void mnuClick(object sender, EventArgs e)
        {
            var m = (ToolStripMenuItem) sender;

            Go(m.ImageIndex);
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal Panel Panel1;
        internal Splitter Splitter1;
        public ToolStrip ToolBar1;
        internal ToolStripButton ToolBarButton1;
        internal ToolStripButton ToolBarButton2;
        internal ToolStripButton ToolBarButton3;
        internal ToolStripButton ToolBarButton4;
        internal ToolStripButton ToolBarButton5;
        internal TreeView TreeView1;

        public frmAplicacion()
        {
            InitializeComponent();

            ToolBar1.ItemClicked += ToolBar1_ButtonClick;

            this.Load += FrmAplicacion_Load;
        }

        private void FrmAplicacion_Load(object sender, EventArgs e)
        {
            Initialize();
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.ToolBar1 = new System.Windows.Forms.ToolStrip();
            this.ToolBarButton1 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton2 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton3 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton4 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton5 = new System.Windows.Forms.ToolStripButton();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(160, 50);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(352, 324);
            this.Panel1.TabIndex = 2;
            // 
            // ToolBar1
            // 
            this.ToolBar1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.ToolBarButton1,
            this.ToolBarButton2,
            this.ToolBarButton3,
            this.ToolBarButton4,
            this.ToolBarButton5});
            this.ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ToolBar1.Name = "ToolBar1";
            this.ToolBar1.ShowItemToolTips = true;
            this.ToolBar1.Size = new System.Drawing.Size(512, 50);
            this.ToolBar1.TabIndex = 4;
            // 
            // ToolBarButton1
            // 
            this.ToolBarButton1.Name = "ToolBarButton1";
            this.ToolBarButton1.Tag = "CERRAR";
            this.ToolBarButton1.Text = "Cerrar";
            this.ToolBarButton1.ToolTipText = "Cerrar";
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.Tag = "INICIO";
            this.ToolBarButton2.Text = "Inicio";
            this.ToolBarButton2.ToolTipText = "Inicio";
            // 
            // ToolBarButton3
            // 
            this.ToolBarButton3.Name = "ToolBarButton3";
            this.ToolBarButton3.Tag = "ANTERIOR";
            this.ToolBarButton3.Text = "Anterior";
            this.ToolBarButton3.ToolTipText = "Anterior";
            // 
            // ToolBarButton4
            // 
            this.ToolBarButton4.Name = "ToolBarButton4";
            this.ToolBarButton4.Tag = "SIGUIENTE";
            this.ToolBarButton4.Text = "Siguiente";
            this.ToolBarButton4.ToolTipText = "Siguiente";
            // 
            // ToolBarButton5
            // 
            this.ToolBarButton5.Name = "ToolBarButton5";
            this.ToolBarButton5.Tag = "PAGINAS";
            this.ToolBarButton5.Text = "Páginas";
            // 
            // Splitter1
            // 
            this.Splitter1.Location = new System.Drawing.Point(0, 50);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(160, 324);
            this.Splitter1.TabIndex = 5;
            this.Splitter1.TabStop = false;
            // 
            // TreeView1
            // 
            this.TreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeView1.Location = new System.Drawing.Point(8, 56);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(144, 306);
            this.TreeView1.TabIndex = 8;
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // frmAplicacion
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(512, 374);
            this.Controls.Add(this.TreeView1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.ToolBar1);
            this.Name = "frmAplicacion";
            this.Text = "Aplicación";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var formName = ((TreeNode)e.Node).Name;
            if (formName == "") return;

            var form = Activator.CreateInstance(Type.GetType(formName)) as Form;

            form.TopLevel = false;
            form.AutoScroll = true;
            Panel1.Controls.Clear();
            Panel1.Controls.Add(form);
            form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
        }
    }
}