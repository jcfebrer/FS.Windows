using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

#if NET35_OR_GREATER || NETCOREAPP
    using System.Linq;
#endif

using System.Collections.Generic;
using System.Security.AccessControl;
using FSSecurity;

namespace FSFormControls
{
    /// <summary>
    /// Summary description for ExplorerTree.
    /// </summary>
    /// 
    [ToolboxBitmapAttribute(typeof(FSFormControls.DBExplorerTree), "tree.gif"), DefaultEvent("PathChanged")]
    public class DBExplorerTree : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.TreeView tvwMain;
        private System.ComponentModel.IContainer components;

        //private bool goflag = false;
        private bool showMyDocuments = true;
        private bool showMyFavorites = true;
        private bool showMyNetwork = true;

        private bool showAddressbar = true;
        private bool showToolbar = true;

        public bool ShowAddressbar
        {
            get
            {
                return showAddressbar;
            }
            set
            {
                showAddressbar = value;
            }
        }

        public bool ShowToolbar
        {
            get
            {
                return showToolbar;
            }
            set
            {
                showToolbar = value;
            }
        }

        public bool ShowMyDocuments
        {
            get
            {
                return showMyDocuments;
            }
            set
            {
                showMyDocuments = value;
                this.Refresh();
            }
        }

        public bool ShowMyFavorites
        {
            get
            {
                return showMyFavorites;
            }
            set
            {
                showMyFavorites = value;
                this.Refresh();
            }
        }

        public bool ShowMyNetwork
        {
            get
            {
                return showMyNetwork;
            }
            set
            {
                showMyNetwork = value;
                this.Refresh();
            }
        }

        public string Pattern { get; set; } = "*.*";


        TreeNode node;
        TreeNode TreeNodeMyComputer;
        TreeNode TreeNodeRootNode;


        //ListViewItem comunalItem;
        private System.Windows.Forms.Button btnGo;

        //SHFILEINFO [] iconList = new SHFILEINFO[1];	//used icons
        public delegate void PathChangedEventHandler(object sender, EventArgs e);
        private PathChangedEventHandler PathChangedEvent;
        public event PathChangedEventHandler PathChanged
        {
            add
            {
                PathChangedEvent = (PathChangedEventHandler)System.Delegate.Combine(PathChangedEvent, value);
            }
            remove
            {
                PathChangedEvent = (PathChangedEventHandler)System.Delegate.Remove(PathChangedEvent, value);
            }
        }
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip cMShortcut;
        private System.Windows.Forms.ToolStripMenuItem mnuShortcut;
        private System.Windows.Forms.TextBox txtPath;
        private ToolStrip toolStrip;
        private ToolStripButton toolStripAdd;
        private ToolStripButton toolStripBack;
        private ToolStripButton toolStripNext;
        private ToolStripButton toolStripUp;
        private ToolStripButton toolStripRefresh;
        private ToolStripButton toolStripHome;
        private ToolStripButton toolStripInfo;
        private Panel panel;
        private string selectedPath = "home";
        private string[] selectedPathFiles;
        private string[] selectedPathFolders;
        private List<NavigationPath> navigationPaths = new List<NavigationPath>();

        private class NavigationPath
        {
            public NavigationPath(string path, bool selected)
            {
                Path = path;
                Selected = selected;
            }

            public string Path { get; set; }
            public bool Selected { get; set; }
        }

        [
        Category("Appearance"),
        Description("Selected Path files")
        ]
        public string SelectedPath
        {
            get
            {
                return this.selectedPath;
            }
            set
            {
                this.selectedPath = value;

                GetFiles(selectedPath);
                GetFolders(selectedPath);

                this.Invalidate();
            }
        }

        [
        Category("Appearance"),
        Description("Selected files of path")
        ]
        public string[] SelectedPathFiles
        {
            get
            {
                return this.selectedPathFiles;
            }
            set
            {
                this.selectedPathFiles = value;
                this.Invalidate();
            }
        }

        [
        Category("Appearance"),
        Description("Selected path folders")
        ]
        public string[] SelectedPathFolders
        {
            get
            {
                return this.selectedPathFolders;
            }
            set
            {
                this.selectedPathFolders = value;
                this.Invalidate();
            }
        }


        public DBExplorerTree()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            // TODO: Add any initialization after the InitializeComponent call

        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBExplorerTree));
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.tvwMain = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cMShortcut = new System.Windows.Forms.ContextMenuStrip();
            this.mnuShortcut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripHome = new System.Windows.Forms.ToolStripButton();
            this.toolStripBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripAdd = new System.Windows.Forms.ToolStripButton();
            this.panel = new System.Windows.Forms.Panel();
            this.toolStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(8, 3);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(438, 20);
            this.txtPath.TabIndex = 61;
            this.toolTip.SetToolTip(this.txtPath, "Current directory");
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            this.txtPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPath_KeyPress);
            this.txtPath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyUp);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.ForeColor = System.Drawing.Color.White;
            this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
            this.btnGo.Location = new System.Drawing.Point(443, 1);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(24, 22);
            this.btnGo.TabIndex = 60;
            this.toolTip.SetToolTip(this.btnGo, "Go to the directory");
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // tvwMain
            // 
            this.tvwMain.AllowDrop = true;
            this.tvwMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwMain.BackColor = System.Drawing.Color.White;
            this.tvwMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwMain.ImageIndex = 0;
            this.tvwMain.ImageList = this.imageList;
            this.tvwMain.Location = new System.Drawing.Point(3, 29);
            this.tvwMain.Name = "tvwMain";
            this.tvwMain.SelectedImageIndex = 2;
            this.tvwMain.ShowLines = false;
            this.tvwMain.ShowRootLines = false;
            this.tvwMain.Size = new System.Drawing.Size(464, 346);
            this.tvwMain.TabIndex = 59;
            this.tvwMain.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvwMain_AfterExpand);
            this.tvwMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwMain_AfterSelect);
            this.tvwMain.DoubleClick += new System.EventHandler(this.tvwMain_DoubleClick);
            this.tvwMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwMain_MouseUp);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            this.imageList.Images.SetKeyName(10, "");
            this.imageList.Images.SetKeyName(11, "");
            this.imageList.Images.SetKeyName(12, "");
            this.imageList.Images.SetKeyName(13, "");
            this.imageList.Images.SetKeyName(14, "");
            this.imageList.Images.SetKeyName(15, "");
            this.imageList.Images.SetKeyName(16, "");
            this.imageList.Images.SetKeyName(17, "");
            this.imageList.Images.SetKeyName(18, "");
            this.imageList.Images.SetKeyName(19, "");
            this.imageList.Images.SetKeyName(20, "");
            this.imageList.Images.SetKeyName(21, "");
            this.imageList.Images.SetKeyName(22, "");
            this.imageList.Images.SetKeyName(23, "");
            this.imageList.Images.SetKeyName(24, "");
            this.imageList.Images.SetKeyName(25, "");
            this.imageList.Images.SetKeyName(26, "");
            this.imageList.Images.SetKeyName(27, "");
            this.imageList.Images.SetKeyName(28, "");
            // 
            // cMShortcut
            // 
            this.cMShortcut.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuShortcut});
            // 
            // mnuShortcut
            // 
            //this.mnuShortcut.Index = 0;
            this.mnuShortcut.Text = "Remove Shortcut";
            this.mnuShortcut.Click += new System.EventHandler(this.mnuShortcut_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripHome,
            this.toolStripBack,
            this.toolStripNext,
            this.toolStripUp,
            this.toolStripRefresh,
            this.toolStripInfo,
            this.toolStripAdd});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(471, 25);
            this.toolStrip.TabIndex = 72;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripHome
            // 
            this.toolStripHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripHome.Image = ((System.Drawing.Image)(resources.GetObject("toolStripHome.Image")));
            this.toolStripHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHome.Name = "toolStripHome";
            this.toolStripHome.Size = new System.Drawing.Size(23, 22);
            this.toolStripHome.Text = "Casa";
            this.toolStripHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // toolStripBack
            // 
            this.toolStripBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBack.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBack.Image")));
            this.toolStripBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBack.Name = "toolStripBack";
            this.toolStripBack.Size = new System.Drawing.Size(23, 22);
            this.toolStripBack.Text = "Anterior";
            this.toolStripBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // toolStripNext
            // 
            this.toolStripNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripNext.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNext.Image")));
            this.toolStripNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNext.Name = "toolStripNext";
            this.toolStripNext.Size = new System.Drawing.Size(23, 22);
            this.toolStripNext.Text = "Siguiente";
            this.toolStripNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // toolStripUp
            // 
            this.toolStripUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripUp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripUp.Image")));
            this.toolStripUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripUp.Name = "toolStripUp";
            this.toolStripUp.Size = new System.Drawing.Size(23, 22);
            this.toolStripUp.Text = "Nivel superior";
            this.toolStripUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // toolStripRefresh
            // 
            this.toolStripRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRefresh.Image")));
            this.toolStripRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRefresh.Name = "toolStripRefresh";
            this.toolStripRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripRefresh.Text = "Actualizar";
            this.toolStripRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripInfo
            // 
            this.toolStripInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripInfo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripInfo.Image")));
            this.toolStripInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripInfo.Name = "toolStripInfo";
            this.toolStripInfo.Size = new System.Drawing.Size(23, 22);
            this.toolStripInfo.Text = "Información";
            this.toolStripInfo.Visible = false;
            this.toolStripInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // toolStripAdd
            // 
            this.toolStripAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAdd.Image")));
            this.toolStripAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAdd.Name = "toolStripAdd";
            this.toolStripAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripAdd.Text = "Acceso directo";
            this.toolStripAdd.Visible = false;
            this.toolStripAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.txtPath);
            this.panel.Controls.Add(this.btnGo);
            this.panel.Controls.Add(this.tvwMain);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 25);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(471, 378);
            this.panel.TabIndex = 73;
            // 
            // DBExplorerTree
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolStrip);
            this.Name = "DBExplorerTree";
            this.Size = new System.Drawing.Size(471, 403);
            this.Load += new System.EventHandler(this.ExplorerTree_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void ExplorerTree_Load(object sender, System.EventArgs e)
        {
            GetDirectory();

            if (Directory.Exists(selectedPath))
            {
                SetCurrentPath(selectedPath);
            }
            else
            {
                SetCurrentPath("home");
            }

            Go(txtPath.Text);

            RefreshView();

        }
        public void RefreshView()
        {
            if ((!showAddressbar) && (!showToolbar))
            {
                tvwMain.Top = 0;
                txtPath.Visible = false;
                btnGo.Visible = false;
                toolStrip.Visible = false;
                tvwMain.Height = this.Height;
            }
            else
            {
                if (showToolbar && (!showAddressbar))
                {
                    tvwMain.Top = 20;
                    txtPath.Visible = false;
                    btnGo.Visible = false;
                    tvwMain.Height = this.Height - 20;
                    toolStrip.Visible = true;
                }
                else if (showAddressbar && (!showToolbar))
                {
                    tvwMain.Top = 20;
                    txtPath.Top = 1;
                    btnGo.Top = -2;
                    txtPath.Visible = true;
                    btnGo.Visible = true;
                    tvwMain.Height = this.Height - 20;
                    toolStrip.Visible = false;
                }
                else
                {
                    tvwMain.Top = 40;
                    txtPath.Visible = true;
                    btnGo.Visible = true;
                    txtPath.Top = 19;
                    btnGo.Top = 16;
                    toolStrip.Visible = true;
                    tvwMain.Height = this.Height - 40;
                }
            }
        }

        public void GetDirectory()
        {
            tvwMain.Nodes.Clear();


            string[] drives = Environment.GetLogicalDrives();
            TreeNode nodeD;

            TreeNode nodemd;
            TreeNode nodemf;
            TreeNode nodemyC;
            TreeNode nodemNc;

            TreeNode nodemyN;

            TreeNode nodeEN;
            TreeNode nodeNN;

            nodeD = new TreeNode();
            nodeD.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            nodeD.Text = "Desktop";
            nodeD.ImageIndex = 10;
            nodeD.SelectedImageIndex = 10;

            tvwMain.Nodes.Add(nodeD);
            TreeNodeRootNode = nodeD;


            if (ShowMyDocuments)
            {
                nodemd = new TreeNode();
                nodemd.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                nodemd.Text = "My Documents";
                nodemd.ImageIndex = 9;
                nodemd.SelectedImageIndex = 9;
                nodeD.Nodes.Add(nodemd);
                GetDirectories(nodemd);
            }

            nodemyC = new TreeNode();
            nodemyC.Tag = "My Computer";
            nodemyC.Text = "My Computer";
            nodemyC.ImageIndex = 12;
            nodemyC.SelectedImageIndex = 12;
            nodeD.Nodes.Add(nodemyC);
            nodemyC.EnsureVisible();

            TreeNodeMyComputer = nodemyC;

            nodemNc = new TreeNode();
            nodemNc.Tag = "my Node";
            nodemNc.Text = "my Node";
            nodemNc.ImageIndex = 12;
            nodemNc.SelectedImageIndex = 12;
            nodemyC.Nodes.Add(nodemNc);



            if (ShowMyNetwork)
            {

                nodemyN = new TreeNode();
                nodemyN.Tag = "My Network Places";
                nodemyN.Text = "My Network Places";
                nodemyN.ImageIndex = 13;
                nodemyN.SelectedImageIndex = 13;
                nodeD.Nodes.Add(nodemyN);
                nodemyN.EnsureVisible();

                nodeEN = new TreeNode();
                nodeEN.Tag = "Entire Network";
                nodeEN.Text = "Entire Network";
                nodeEN.ImageIndex = 14;
                nodeEN.SelectedImageIndex = 14;
                nodemyN.Nodes.Add(nodeEN);

                nodeNN = new TreeNode();
                nodeNN.Tag = "Network Node";
                nodeNN.Text = "Network Node";
                nodeNN.ImageIndex = 15;
                nodeNN.SelectedImageIndex = 15;
                nodeEN.Nodes.Add(nodeNN);

                nodeEN.EnsureVisible();
            }

            if (ShowMyFavorites)
            {
                nodemf = new TreeNode();
                nodemf.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
                nodemf.Text = "My Favorites";
                nodemf.ImageIndex = 26;
                nodemf.SelectedImageIndex = 26;
                nodeD.Nodes.Add(nodemf);
                GetDirectories(nodemf);
            }
            ExploreTreeNode(nodeD);

        }
        private void ExploreTreeNode(TreeNode n)
        {
            Cursor.Current = Cursors.WaitCursor;

            GetDirectories(n);

            foreach (TreeNode node in n.Nodes)
            {
                if (String.Compare(n.Text, "Desktop") == 0)
                {
                    if (!(
                        (String.Compare(node.Text, "My Documents") == 0) ||
                        (String.Compare(node.Text, "My Computer") == 0) ||
                        (String.Compare(node.Text, "Microsoft Windows Network") == 0) ||
                        (String.Compare(node.Text, "My Network Places") == 0)
                        ))
                    {
                        GetDirectories(node);
                    }
                }
                else
                {
                    GetDirectories(node);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void GetDirectories(TreeNode parentNode)
        {
            string[] dirList;

            if (!Security.HasAccessFolder(parentNode.Tag.ToString(), FileSystemRights.ListDirectory))
                return;

            dirList = Directory.GetDirectories(parentNode.Tag.ToString());
            Array.Sort(dirList);

            if (dirList.Length == parentNode.Nodes.Count)
                return;

            for (int i = 0; i < dirList.Length; i++)
            {
                node = new TreeNode();
                node.Tag = dirList[i];
                node.Text = dirList[i].Substring(dirList[i].LastIndexOf(@"\") + 1);
                node.ImageIndex = 1;
                parentNode.Nodes.Add(node);
            }
        }

#if NET35_OR_GREATER || NETCOREAPP
        private void GetFiles(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists && Security.HasAccessFolder(path, FileSystemRights.ListDirectory))
                selectedPathFiles = di.GetFiles(Pattern).Select(o => o.Name).ToArray();
            else
                selectedPathFiles = null;
        }

        private void GetFolders(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists && Security.HasAccessFolder(path, FileSystemRights.ListDirectory))
                selectedPathFolders = di.GetDirectories(Pattern).Select(o => o.Name).ToArray();
            else
                selectedPathFolders = null;
        }
#else
        private void GetFiles(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists && Security.HasAccessFolder(path, FileSystemRights.ListDirectory))
            {
                FileInfo[] files = di.GetFiles(Pattern);
                selectedPathFiles = new string[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    selectedPathFiles[i] = files[i].Name;
                }
            }
            else
            {
                selectedPathFiles = null;
            }
        }

        private void GetFolders(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists && Security.HasAccessFolder(path, FileSystemRights.ListDirectory))
            {
                DirectoryInfo[] folders = di.GetDirectories(Pattern);
                selectedPathFolders = new string[folders.Length];

                for (int i = 0; i < folders.Length; i++)
                {
                    selectedPathFolders[i] = folders[i].Name;
                }
            }
            else
            {
                selectedPathFolders = null;
            }
        }
#endif

        public void SetCurrentPath(string strPath)
        {
            if (String.Compare(strPath, "home") == 0)
            {
                txtPath.Text = Application.StartupPath;
            }
            else
            {
                DirectoryInfo inf = new DirectoryInfo(strPath);
                if (inf.Exists)
                {
                    txtPath.Text = strPath;
                }
                else
                    txtPath.Text = Application.StartupPath;
            }

            SelectedTreeNode(tvwMain.Nodes, txtPath.Text);

            SelectedPath = txtPath.Text;
        }

        private bool SelectedTreeNode(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag.ToString() == path)
                {
                    node.TreeView.SelectedNode = node;
                    return true;
                }

                if (node.Nodes.Count > 0)
                {
                    if (SelectedTreeNode(node.Nodes, path))
                        return true;
                }
            }

            return false;
        }

        private void tvwMain_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            string[] drives = Environment.GetLogicalDrives();

            Cursor.Current = Cursors.WaitCursor;
            TreeNode n;
            TreeNode nodeNN;
            TreeNode nodemN;
            TreeNode nodemyC;
            TreeNode nodeNNode;
            TreeNode nodeDrive;
            nodemyC = e.Node;

            n = e.Node;

            if (n.Text.IndexOf(":", 1) > 0)
            {
                ExploreTreeNode(n);
            }
            else
            {
                if ((String.Compare(n.Text, "Desktop") == 0) || (String.Compare(n.Text, "Microsoft Windows Network") == 0) || (String.Compare(n.Text, "My Computer") == 0) || (String.Compare(n.Text, "My Network Places") == 0) || (String.Compare(n.Text, "Entire Network") == 0) || ((n.Parent != null) && (String.Compare(n.Parent.Text, "Microsoft Windows Network") == 0)))
                {
                    if ((String.Compare(n.Text, "My Computer") == 0) && (nodemyC.GetNodeCount(true) < 2))
                    {
                        nodemyC.FirstNode.Remove();

                        foreach (string drive in drives)
                        {

                            nodeDrive = new TreeNode();
                            nodeDrive.Tag = drive;

                            nodeDrive.Text = drive;

                            //Determine icon to display by drive
                            switch (Win32.GetDriveType(drive))
                            {
                                case 2:
                                    nodeDrive.ImageIndex = 17;
                                    nodeDrive.SelectedImageIndex = 17;
                                    break;
                                case 3:
                                    nodeDrive.ImageIndex = 0;
                                    nodeDrive.SelectedImageIndex = 0;
                                    break;
                                case 4:
                                    nodeDrive.ImageIndex = 8;
                                    nodeDrive.SelectedImageIndex = 8;
                                    break;
                                case 5:
                                    nodeDrive.ImageIndex = 7;
                                    nodeDrive.SelectedImageIndex = 7;
                                    break;
                                default:
                                    nodeDrive.ImageIndex = 0;
                                    nodeDrive.SelectedImageIndex = 0;
                                    break;
                            }

                            nodemyC.Nodes.Add(nodeDrive);
                            nodeDrive.EnsureVisible();
                            tvwMain.Refresh();

                            //add dirs under drive
                            if (Directory.Exists(drive))
                            {
                                foreach (string dir in Directory.GetDirectories(drive))
                                {
                                    node = new TreeNode();
                                    node.Tag = dir;
                                    node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
                                    node.ImageIndex = 1;
                                    nodeDrive.Nodes.Add(node);
                                }
                            }

                            nodemyC.Expand();
                        }

                    }
                    if ((String.Compare(n.Text, "Entire Network") == 0))
                    {
                        if (n.FirstNode.Text == "Network Node")
                        {
                            n.FirstNode.Remove();

                            ServerEnum servers = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET, ResourceType.RESOURCETYPE_DISK, ResourceUsage.RESOURCEUSAGE_ALL, ResourceDisplayType.RESOURCEDISPLAYTYPE_NETWORK, "");

                            foreach (string s1 in servers)
                            {
                                string s2 = "";
                                s2 = s1.Substring(0, s1.IndexOf("|", 1));

                                if (s1.IndexOf("NETWORK", 1) > 0)
                                {
                                    nodeNN = new TreeNode();
                                    nodeNN.Tag = s2;
                                    nodeNN.Text = s2;
                                    nodeNN.ImageIndex = 15;
                                    nodeNN.SelectedImageIndex = 15;
                                    n.Nodes.Add(nodeNN);
                                }
                                else
                                {
                                    TreeNode nodemNc;
                                    nodemN = new TreeNode();
                                    nodemN.Tag = s2;
                                    nodemN.Text = s2;
                                    nodemN.ImageIndex = 16;
                                    nodemN.SelectedImageIndex = 16;
                                    n.LastNode.Nodes.Add(nodemN);

                                    nodemNc = new TreeNode();
                                    nodemNc.Tag = "my netNode";
                                    nodemNc.Text = "my netNode";
                                    nodemNc.ImageIndex = 12;
                                    nodemNc.SelectedImageIndex = 12;
                                    nodemN.Nodes.Add(nodemNc);
                                }
                            }
                        }
                    }
                    if ((n.Parent != null) && (String.Compare(n.Parent.Text, "Microsoft Windows Network") == 0))
                    {
                        if (n.FirstNode.Text == "my netNode")
                        {
                            n.FirstNode.Remove();

                            string pS = n.Text;

                            ServerEnum servers = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
                                ResourceType.RESOURCETYPE_DISK,
                                ResourceUsage.RESOURCEUSAGE_ALL,
                                ResourceDisplayType.RESOURCEDISPLAYTYPE_SERVER, pS);

                            foreach (string s1 in servers)
                            {
                                string s2 = "";


                                if ((s1.Length < 6) || (String.Compare(s1.Substring(s1.Length - 6, 6), "-share") != 0))
                                {
                                    s2 = s1;
                                    nodeNN = new TreeNode();
                                    nodeNN.Tag = s2;
                                    nodeNN.Text = s2.Substring(2);
                                    nodeNN.ImageIndex = 12;
                                    nodeNN.SelectedImageIndex = 12;
                                    n.Nodes.Add(nodeNN);
                                    foreach (string s1node in servers)
                                    {
                                        if (s1node.Length > 6)
                                        {
                                            if (String.Compare(s1node.Substring(s1node.Length - 6, 6), "-share") == 0)
                                            {
                                                if (s2.Length <= s1node.Length)
                                                {
                                                    if (String.Compare(s1node.Substring(0, s2.Length + 1), s2 + @"\") == 0)
                                                    {
                                                        nodeNNode = new TreeNode();
                                                        nodeNNode.Tag = s1node.Substring(0, s1node.Length - 6);
                                                        nodeNNode.Text = s1node.Substring(s2.Length + 1, s1node.Length - s2.Length - 7);
                                                        nodeNNode.ImageIndex = 28;
                                                        nodeNNode.SelectedImageIndex = 28;
                                                        nodeNN.Nodes.Add(nodeNNode);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    ExploreTreeNode(n);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void tvwMain_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            TreeNode n;
            n = e.Node;

            if (String.Compare(n.Text, "My Computer") != 0 && String.Compare(n.Text, "My Network Places") != 0 && String.Compare(n.Text, "Entire Network") != 0)
            {
                SetCurrentPath(n.Tag.ToString());
            }
        }

        private void tvwMain_DoubleClick(object sender, System.EventArgs e)
        {

            TreeNode n;
            n = tvwMain.SelectedNode;

            if (!tvwMain.SelectedNode.IsExpanded)
                tvwMain.SelectedNode.Collapse();
            else
            {
                ExploreTreeNode(n);
            }
        }

        public void RefreshFolders()
        {
            navigationPaths.Clear();
            tvwMain.Nodes.Clear();
            SetCurrentPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            GetDirectory();
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            RefreshView();

            RefreshFolders();

            SetCurrentPath("home");
            Cursor.Current = Cursors.Default;
            ExploreMyComputer();
        }

        private void ExploreMyComputer()
        {

            string[] drives = Environment.GetLogicalDrives();
            string dir2 = "";

            Cursor.Current = Cursors.WaitCursor;
            TreeNode nodeDrive;

            if (TreeNodeMyComputer.GetNodeCount(true) < 2)
            {
                TreeNodeMyComputer.FirstNode.Remove();

                foreach (string drive in drives)
                {
                    nodeDrive = new TreeNode();
                    nodeDrive.Tag = drive;

                    nodeDrive.Text = drive;

                    switch (Win32.GetDriveType(drive))
                    {
                        case 2:
                            nodeDrive.ImageIndex = 17;
                            nodeDrive.SelectedImageIndex = 17;
                            break;
                        case 3:
                            nodeDrive.ImageIndex = 0;
                            nodeDrive.SelectedImageIndex = 0;
                            break;
                        case 4:
                            nodeDrive.ImageIndex = 8;
                            nodeDrive.SelectedImageIndex = 8;
                            break;
                        case 5:
                            nodeDrive.ImageIndex = 7;
                            nodeDrive.SelectedImageIndex = 7;
                            break;
                        default:
                            nodeDrive.ImageIndex = 0;
                            nodeDrive.SelectedImageIndex = 0;
                            break;
                    }

                    TreeNodeMyComputer.Nodes.Add(nodeDrive);

                    //add dirs under drive
                    if (Directory.Exists(drive))
                    {
                        foreach (string dir in Directory.GetDirectories(drive))
                        {
                            dir2 = dir;
                            node = new TreeNode();
                            node.Tag = dir;
                            node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
                            node.ImageIndex = 1;
                            nodeDrive.Nodes.Add(node);
                        }
                    }
                }
            }

            TreeNodeMyComputer.Expand();
        }

        private void UpdateListAddCurrent()
        {
            for (int i = 0; i < navigationPaths.Count - 1; i++)
            {
                if (navigationPaths[i].Selected)
                {
                    //navigationPaths.RemoveAt(i);

                    //for (int j = navigationPaths.Count - 1; j > i + 1; j--)
                    //	navigationPaths.RemoveAt(j);
                    //break;
                }

            }
        }
        private void UpdateListGoBack()
        {
            if ((navigationPaths.Count > 0) && (navigationPaths[0].Selected))
                return;

            for (int i = 0; i < navigationPaths.Count; i++)
            {
                if (navigationPaths[i].Selected)
                {
                    if (i != 0)
                    {
                        navigationPaths[i - 1].Selected = true;
                        SetCurrentPath(navigationPaths[i - 1].Path);
                    }
                }
                if (i != 0)
                {
                    navigationPaths[i].Selected = false;
                }
            }
        }
        private void UpdateListGoFwd()
        {
            if ((navigationPaths.Count > 0) && (navigationPaths[navigationPaths.Count - 1].Selected))
                return;

            for (int i = navigationPaths.Count - 1; i >= 0; i--)
            {
                if (navigationPaths[i].Selected)
                {
                    if (i != navigationPaths.Count)
                    {
                        navigationPaths[i + 1].Selected = true;
                        SetCurrentPath(navigationPaths[i + 1].Path);
                    }
                }

                if (i != navigationPaths.Count - 1)
                    navigationPaths[i].Selected = false;
            }
        }
        private void UpdateList(string path)
        {
            UpdateListAddCurrent();

            //if (navigationPaths.Count > 0)
            //{
            //	if (navigationPaths[navigationPaths.Count - 1].Selected)
            //	{
            //		return;
            //	}
            //}

            for (int i = 0; i < navigationPaths.Count; i++)
            {
                navigationPaths[i].Selected = false;
            }

            if (navigationPaths.Exists(p => p.Path == path))
                navigationPaths.Find(p => p.Path == path).Selected = true;
            else
                navigationPaths.Add(new NavigationPath(path, true));
        }

        private void btnGo_Click(object sender, System.EventArgs e)
        {
            Go(txtPath.Text);
        }

        public void Go(string path)
        {
            Cursor.Current = Cursors.WaitCursor;

            ExploreMyComputer();
            SetCurrentPath(path);

            Cursor.Current = Cursors.Default;

            //         Cursor.Current = Cursors.WaitCursor;
            //         ExploreMyComputer();
            //         TreeNode myComputerNode = TreeNodeMyComputer;

            //do
            //{
            //	foreach (TreeNode node in myComputerNode.Nodes)
            //	{
            //		string mypath = node.Tag.ToString().ToLower();

            //		string mypathf = mypath;
            //		if (!mypath.EndsWith(@"\"))
            //		{
            //			if (path.ToLower().Length > mypathf.Length)
            //				mypathf = mypath + @"\";
            //		}

            //		if (path.ToLower().StartsWith(mypathf))
            //		{
            //			node.TreeView.Focus();
            //			node.TreeView.SelectedNode = node;
            //			node.EnsureVisible();
            //			node.Expand();
            //			if (node.Nodes.Count >= 1)
            //			{
            //				node.Expand();
            //				myComputerNode = node;
            //			}
            //			else
            //			{
            //				if (String.Compare(path.ToLower(), mypath) == 0)
            //				{
            //					break;
            //				}
            //				else
            //				{
            //					continue;
            //				}
            //			}

            //			if (mypathf.StartsWith(path.ToLower()))
            //			{
            //				break;
            //			}
            //		}
            //	}

            //	myComputerNode = myComputerNode.NextNode;

            //} while (myComputerNode != null);

            //         Cursor.Current = Cursors.Default;
        }

        private void btnHome_Click(object sender, System.EventArgs e)
        {
            SetCurrentPath("home");
            ExploreMyComputer();

            Go(txtPath.Text);
        }


        private void btnNext_Click(object sender, System.EventArgs e)
        {
            string cpath = txtPath.Text;
            UpdateListGoFwd();

            if (String.Compare(cpath, txtPath.Text) != 0)
            {
                Go(txtPath.Text);
            }
        }

        private void btnBack_Click(object sender, System.EventArgs e)
        {
            string cpath = txtPath.Text;
            UpdateListGoBack();

            if (String.Compare(cpath, txtPath.Text) != 0)
            {
                Go(txtPath.Text);
            }
        }

        private void tvwMain_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            UpdateList(txtPath.Text);
            if (tvwMain.SelectedNode != null)
            {

                if ((tvwMain.SelectedNode.ImageIndex == 18) && (e.Button == MouseButtons.Right))
                    cMShortcut.Show(tvwMain, new Point(e.X, e.Y));
            }
        }

        private void btnUp_Click(object sender, System.EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(txtPath.Text);

            if (directoryInfo.Parent == null)
                return;

            if (directoryInfo.Parent.Exists)
                SetCurrentPath(directoryInfo.Parent.FullName);
            UpdateList(txtPath.Text);

            Go(txtPath.Text);
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            string myname = "";
            string mypath = "";

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Add Folder in Explorer Tree";
            dialog.ShowNewFolderButton = true;
            dialog.SelectedPath = txtPath.Text;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mypath = dialog.SelectedPath;
                myname = mypath.Substring(mypath.LastIndexOf("\\") + 1);

                AddFolderNode(myname, mypath);

            }
        }
        private void AddFolderNode(string name, string path)
        {
            TreeNode nodemyC = new TreeNode();

            nodemyC.Tag = path;
            nodemyC.Text = name;

            nodemyC.ImageIndex = 18;
            nodemyC.SelectedImageIndex = 18;

            TreeNodeRootNode.Nodes.Add(nodemyC);

            //add dirs under drive
            if (Directory.Exists(path))
            {
                foreach (string dir in Directory.GetDirectories(path))
                {
                    TreeNode node = new TreeNode();
                    node.Tag = dir;
                    node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
                    node.ImageIndex = 1;
                    nodemyC.Nodes.Add(node);
                }
            }
        }

        private void mnuShortcut_Click(object sender, System.EventArgs e)
        {
            if (tvwMain.SelectedNode.ImageIndex == 18)
                tvwMain.SelectedNode.Remove();
        }

        private void txtPath_TextChanged(object sender, System.EventArgs e)
        {
            if (Directory.Exists(txtPath.Text))
            {
                SelectedPath = txtPath.Text;

                if (PathChangedEvent != null)
                    PathChangedEvent(this, EventArgs.Empty);
            }
        }

        public void AboutExplorerTree()
        {
            frmDBExplorerTreeOptions form = new frmDBExplorerTreeOptions(showMyDocuments, showMyFavorites, showMyNetwork, showAddressbar, showToolbar);
            if (form.ShowDialog() == DialogResult.OK)
            {
                showMyDocuments = form.myDocument;
                showMyNetwork = form.myNetwork;
                ShowMyFavorites = form.myFavorite;
                ShowAddressbar = form.myAddressbar;
                ShowToolbar = form.myToolbar;

                btnRefresh_Click(this, null);
            }
        }

        private void txtPath_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.ToString());   
        }

        private void txtPath_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Go(txtPath.Text);
                txtPath.Focus();
            }
        }

        private void btnInfo_Click(object sender, System.EventArgs e)
        {
            AboutExplorerTree();

        }

        private void grptoolbar_Enter(object sender, System.EventArgs e)
        {
        }
    }

	//Shell functions
	public class Win32
	{
		public const uint SHGFI_ICON = 0x100;
		//public const uint SHGFI_LARGEICON = 0x0;    // 'Large icon
		public const uint SHGFI_SMALLICON = 0x1;    // 'Small icon

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SHQUERYRBINFO
    {
        public uint cbSize;
        public ulong i64Size;
        public ulong i64NumItems;
    };

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbSizeFileInfo,
            uint uFlags);

        [DllImport("kernel32")]
        public static extern uint GetDriveType(
            string lpRootPathName);

        [DllImport("shell32.dll")]
        public static extern bool SHGetDiskFreeSpaceEx(
            string pszVolume,
            ref ulong pqwFreeCaller,
            ref ulong pqwTot,
            ref ulong pqwFree);

        [DllImport("shell32.Dll")]
        public static extern int SHQueryRecycleBin(
            string pszRootPath,
            ref SHQUERYRBINFO pSHQueryRBInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };



        [StructLayout(LayoutKind.Sequential)]
        public class BITMAPINFO
        {
            public Int32 biSize;
            public Int32 biWidth;
            public Int32 biHeight;
            public Int16 biPlanes;
            public Int16 biBitCount;
            public Int32 biCompression;
            public Int32 biSizeImage;
            public Int32 biXPelsPerMeter;
            public Int32 biYPelsPerMeter;
            public Int32 biClrUsed;
            public Int32 biClrImportant;
            public Int32 colors;
        };
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Add(IntPtr hImageList, IntPtr hBitmap, IntPtr hMask);
        [DllImport("kernel32.dll")]
        private static extern bool RtlMoveMemory(IntPtr dest, IntPtr source, int dwcount);
        [DllImport("shell32.dll")]
        public static extern IntPtr DestroyIcon(IntPtr hIcon);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, [In, MarshalAs(UnmanagedType.LPStruct)] BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);
    }

    public enum ResourceScope
    {
        RESOURCE_CONNECTED = 1,
        RESOURCE_GLOBALNET,
        RESOURCE_REMEMBERED,
        RESOURCE_RECENT,
        RESOURCE_CONTEXT
    };

    public enum ResourceType
    {
        RESOURCETYPE_ANY,
        RESOURCETYPE_DISK,
        RESOURCETYPE_PRINT,
        RESOURCETYPE_RESERVED
    };

    public enum ResourceUsage
    {
        RESOURCEUSAGE_CONNECTABLE = 0x00000001,
        RESOURCEUSAGE_CONTAINER = 0x00000002,
        RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
        RESOURCEUSAGE_SIBLING = 0x00000008,
        RESOURCEUSAGE_ATTACHED = 0x00000010,
        RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
    };

    public enum ResourceDisplayType
    {
        RESOURCEDISPLAYTYPE_GENERIC,
        RESOURCEDISPLAYTYPE_DOMAIN,
        RESOURCEDISPLAYTYPE_SERVER,
        RESOURCEDISPLAYTYPE_SHARE,
        RESOURCEDISPLAYTYPE_FILE,
        RESOURCEDISPLAYTYPE_GROUP,
        RESOURCEDISPLAYTYPE_NETWORK,
        RESOURCEDISPLAYTYPE_ROOT,
        RESOURCEDISPLAYTYPE_SHAREADMIN,
        RESOURCEDISPLAYTYPE_DIRECTORY,
        RESOURCEDISPLAYTYPE_TREE,
        RESOURCEDISPLAYTYPE_NDSCONTAINER
    };

    public class ServerEnum : IEnumerable
    {
        enum ErrorCodes
        {
            NO_ERROR = 0,
            ERROR_NO_MORE_ITEMS = 259
        };

        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public ResourceScope dwScope = 0;
            public ResourceType dwType = 0;
            public ResourceDisplayType dwDisplayType = 0;
            public ResourceUsage dwUsage = 0;
            public string lpLocalName = null;
            public string lpRemoteName = null;
            public string lpComment = null;
            public string lpProvider = null;
        };


        private ArrayList aData = new ArrayList();


        public int Count
        {
            get { return aData.Count; }
        }

        [DllImport("Mpr.dll", EntryPoint = "WNetOpenEnumA", CallingConvention = CallingConvention.Winapi)]
        private static extern ErrorCodes WNetOpenEnum(ResourceScope dwScope, ResourceType dwType, ResourceUsage dwUsage, NETRESOURCE p, out IntPtr lphEnum);

        [DllImport("Mpr.dll", EntryPoint = "WNetCloseEnum", CallingConvention = CallingConvention.Winapi)]
        private static extern ErrorCodes WNetCloseEnum(IntPtr hEnum);

        [DllImport("Mpr.dll", EntryPoint = "WNetEnumResourceA", CallingConvention = CallingConvention.Winapi)]
        private static extern ErrorCodes WNetEnumResource(IntPtr hEnum, ref uint lpcCount, IntPtr buffer, ref uint lpBufferSize);


        private void EnumerateServers(NETRESOURCE pRsrc, ResourceScope scope, ResourceType type, ResourceUsage usage, ResourceDisplayType displayType, string kPath)
        {
            uint bufferSize = 16384;
            IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
            IntPtr handle = IntPtr.Zero;
            ErrorCodes result;
            uint cEntries = 1;
            bool serverenum = false;

            result = WNetOpenEnum(scope, type, usage, pRsrc, out handle);

            if (result == ErrorCodes.NO_ERROR)
            {
                do
                {
                    result = WNetEnumResource(handle, ref cEntries, buffer, ref bufferSize);

                    if ((result == ErrorCodes.NO_ERROR))
                    {
                        Marshal.PtrToStructure(buffer, pRsrc);

                        if (String.Compare(kPath, "") == 0)
                        {
                            if ((pRsrc.dwDisplayType == displayType) || (pRsrc.dwDisplayType == ResourceDisplayType.RESOURCEDISPLAYTYPE_DOMAIN))
                                aData.Add(pRsrc.lpRemoteName + "|" + pRsrc.dwDisplayType);

                            if ((pRsrc.dwUsage & ResourceUsage.RESOURCEUSAGE_CONTAINER) == ResourceUsage.RESOURCEUSAGE_CONTAINER)
                            {
                                if ((pRsrc.dwDisplayType == displayType))
                                {
                                    EnumerateServers(pRsrc, scope, type, usage, displayType, kPath);

                                }

                            }
                        }
                        else
                        {
                            if (pRsrc.dwDisplayType == displayType)
                            {
                                aData.Add(pRsrc.lpRemoteName);
                                EnumerateServers(pRsrc, scope, type, usage, displayType, kPath);
                                //return;
                                serverenum = true;
                            }
                            if (!serverenum)
                            {
                                if (pRsrc.dwDisplayType == ResourceDisplayType.RESOURCEDISPLAYTYPE_SHARE)
                                {
                                    aData.Add(pRsrc.lpRemoteName + "-share");
                                }
                            }
                            else
                            {
                                serverenum = false;
                            }
                            if ((kPath.IndexOf(pRsrc.lpRemoteName) >= 0) || (String.Compare(pRsrc.lpRemoteName, "Microsoft Windows Network") == 0))
                            {
                                EnumerateServers(pRsrc, scope, type, usage, displayType, kPath);
                                //return;

                            }
                            //}
                        }

                    }
                    else if (result != ErrorCodes.ERROR_NO_MORE_ITEMS)
                        break;
                } while (result != ErrorCodes.ERROR_NO_MORE_ITEMS);

                WNetCloseEnum(handle);
            }

            Marshal.FreeHGlobal((IntPtr)buffer);
        }

        public ServerEnum(ResourceScope scope, ResourceType type, ResourceUsage usage, ResourceDisplayType displayType, string kPath)
        {

            NETRESOURCE netRoot = new NETRESOURCE();
            EnumerateServers(netRoot, scope, type, usage, displayType, kPath);

        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return aData.GetEnumerator();
        }

        #endregion
    }
}