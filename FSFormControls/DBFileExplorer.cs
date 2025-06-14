using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FSDisk;
using System.IO;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public partial class DBFileExplorer : DBUserControl
    {
        public event TreeNodeMouseClickEventHandler NodeMouseClick;
        public event TreeNodeMouseClickEventHandler NodeMouseDoubleClick;
        public const string GO_BACK = "back";

        public DBFileExplorer()
        {
            InitializeComponent();

            cmbDrives.DisplayMember = "Name";
            cmbDrives.DataSource = FileUtils.GetDrives();
            //cmbDrives.Items.AddRange(FileUtils.GetDrives());

            this.dbTreeView1.BeforeSelect += DbTreeView1_BeforeSelect;
            this.dbTreeView1.BeforeCollapse += DbTreeView1_BeforeCollapse;
            this.dbTreeView1.BeforeExpand += DbTreeView1_BeforeExpand;
        }

        private void DbTreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            DBTreeViewNode node = (DBTreeViewNode)e.Node;

            LoadFolders(node.Value.ToString(), node);
        }

        private void DbTreeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
        }

        private void DbTreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
        }

        public DriveInfo SelectedDrive
        {
            get { return (DriveInfo)cmbDrives.SelectedItem; }
            set { cmbDrives.SelectedItem = value; }
        }

        public TreeNode SelectedNode
        {
            get { return dbTreeView1.SelectedNode; }
            set { dbTreeView1.SelectedNode = value; }
        }

        public DBTreeViewNode SelectedDBNode
        {
            get { return dbTreeView1.SelectedDBNode; }
        }

        public void LoadFolders(string path)
        {
            LoadFolders(path, null);
        }

        public void LoadFolders(string path, DBTreeViewNode node)
        {
            DBTreeViewNode nodeBack = new DBTreeViewNode();
            nodeBack.Text = "..";
            nodeBack.Value = GO_BACK;

            if (node == null)
            {
                this.dbTreeView1.Nodes.Clear();
                dbTreeView1.Nodes.Add(nodeBack);
            }
            else
            {
                node.Nodes.Clear();
                node.Nodes.Add(nodeBack);
            }


            Folders folders = FolderUtils.GetFolders(path, false);

            foreach (Folder folder in folders)
            {
                DBTreeViewNode newNode = new DBTreeViewNode();
                newNode.Text = folder.Nombre;
                newNode.Value = folder.Path;

                if (folder.HasSubfolder)
                {
                    //to show '+' symbol
                    newNode.Nodes.Add("dummy");
                }

                if (node == null)
                    dbTreeView1.Nodes.Add(newNode);
                else
                    node.Nodes.Add(newNode);
            }

            return;
        }

        private void cmbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFolders(((System.IO.DriveInfo)cmbDrives.SelectedItem).RootDirectory.FullName, null);
            //LoadFiles(((System.IO.DriveInfo)cmbDrives.SelectedItem).RootDirectory.FullName);
        }

        private void dbTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (NodeMouseClick != null)
                NodeMouseClick(sender, e);
        }

        private void dbTreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (NodeMouseDoubleClick != null)
                NodeMouseDoubleClick(sender, e);
        }
    }
}
