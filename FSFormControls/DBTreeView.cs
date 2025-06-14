#region

using FSException;
using FSLibrary;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBTreeView.bmp")]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    public class DBTreeView : DBUserControl, ISupportInitialize
    {
        private const int MAPSIZE = 128;

        private StringBuilder m_NewNodeMap = new StringBuilder(MAPSIZE);
        private string m_NodeMap = "";

        public DBTreeView()
        {
            InitializeComponent();

            ContextMenu1.Items[0].Visible = AllowSaveXML;
            ContextMenu1.Items[1].Visible = AllowLoadXML;


            TreeView1.DoubleClick += TreeView1_DoubleClick;
            TreeView1.Click += TreeView1_Click;
            MenuItem1.Click += MenuItem1_Click;
            MenuItem2.Click += MenuItem2_Click;
            TreeView1.MouseDown += TreeView1_MouseDown;
            TreeView1.MouseUp += TreeView1_MouseUp;
            TreeView1.AfterSelect += TreeView1_AfterSelect;
            TreeView1.AfterCollapse += TreeView1_AfterCollapse;
            TreeView1.AfterExpand += TreeView1_AfterExpand;
            TreeView1.BeforeSelect += TreeView1_BeforeSelect;
            TreeView1.BeforeCollapse += TreeView1_BeforeCollapse;
            TreeView1.BeforeExpand += TreeView1_BeforeExpand;
            TreeView1.DragEnter += TreeView1_DragEnter;
            TreeView1.DragOver += TreeView1_DragOver;
            TreeView1.ItemDrag += TreeView1_ItemDrag;
            TreeView1.DragDrop += TreeView1_DragDrop;
            TreeView1.NodeMouseClick += TreeView1_NodeMouseClick;
            TreeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            TreeView1.NodeMouseHover += TreeView1_NodeMouseHover;

            TreeView1.ShowNodeToolTips = true;
        }

        #region Delegates

        public delegate void AfterSelectEventHandler(object sender, TreeViewEventArgs e);

        public delegate void BeforeSelectEventHandler(object sender, TreeViewCancelEventArgs e);

        public delegate void AfterCollapseEventHandler(object sender, TreeViewEventArgs e);

        public delegate void BeforeCollapseEventHandler(object sender, TreeViewCancelEventArgs e);

        public delegate void AfterExpandEventHandler(object sender, TreeViewEventArgs e);

        public delegate void BeforeExpandEventHandler(object sender, TreeViewCancelEventArgs e);

        public delegate void ClickEventHandler(object sender, EventArgs e);

        public delegate void DoubleClickEventHandler(object sender, EventArgs e);

        public delegate void TreeNodeMouseClickEventHandler(object sender, TreeNodeMouseClickEventArgs e);

        public delegate void TreeNodeMouseHoverEventHandler(object sender, TreeNodeMouseHoverEventArgs e);

        public delegate void TreeNodeMouseDoubleClickEventHandler(object sender, TreeNodeMouseClickEventArgs e);

        #endregion

        #region Events

        public new event DoubleClickEventHandler DoubleClick;
        public new event ClickEventHandler Click;
        public event AfterSelectEventHandler AfterSelect;
        public event BeforeSelectEventHandler BeforeSelect;
        public event AfterCollapseEventHandler AfterCollapse;
        public event BeforeCollapseEventHandler BeforeCollapse;
        public event AfterExpandEventHandler AfterExpand;
        public event BeforeExpandEventHandler BeforeExpand;
        public event TreeNodeMouseClickEventHandler NodeMouseClick;
        public event TreeNodeMouseHoverEventHandler NodeMouseHover;
        public event TreeNodeMouseDoubleClickEventHandler NodeMouseDoubleClick;

        #endregion


        public int Level { get; set; }

        public bool EnableReArrange { get; set; }

        public bool AllowLoadXML { get; set; } = false;

        public bool AllowSaveXML { get; set; } = false;


        public TreeNodeCollection Nodes
        {
            get { return TreeView1.Nodes; }
        }

        public bool HotTracking
        {
            get { return TreeView1.HotTracking; }
            set { TreeView1.HotTracking = value; }
        }

        public bool HideSelection
        {
            get { return TreeView1.HideSelection; }
            set { TreeView1.HideSelection = value; }
        }

        public override ContextMenuStrip ContextMenuStrip
        {
            get { return TreeView1.ContextMenuStrip; }
            set { TreeView1.ContextMenuStrip = value; }
        }

        public bool ShowLines
        {
            get { return TreeView1.ShowLines; }
            set { TreeView1.ShowLines = value; }
        }

        public bool ShowRootLines
        {
            get { return TreeView1.ShowRootLines; }
            set { TreeView1.ShowRootLines = value; }
        }

        [Description("Imagenes del DBTreeView. 0 - Carpeta cerrada, 1 - Carpeta abierta, 2 - Elemento.")]
        public ImageList ImageList
        {
            get { return TreeView1.ImageList; }
            set { TreeView1.ImageList = value; }
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


        public DBTreeViewNode SelectedDBNode => TreeView1.SelectedNode as DBTreeViewNode;

        public TreeNode SelectedNode
        {
            get { return TreeView1.SelectedNode; }
            set { TreeView1.SelectedNode = value; }
        }

        public TreeNode ActiveNode {
            get { return TreeView1.SelectedNode; }
            set { TreeView1.SelectedNode = value; }
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }


        public void Clear()
        {
            TreeView1.BeginUpdate();
            try
            {
                TreeView1.SelectedNode = null;
                TreeView1.Nodes.Clear();
            }
            finally
            {
                TreeView1.EndUpdate();
            }

            Level = 0;
        }


        private void trvDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FSFormControls.DBTreeView+DBTreeViewNode", false) && !(m_NodeMap == ""))
            {
                var MovingNode = (DBTreeViewNode) e.Data.GetData("FSFormControls.DBTreeView+DBTreeViewNode");
                var NodeIndexes = m_NodeMap.Split('|');
                var InsertCollection = TreeView1.Nodes;
                var i = 0;
                while (i < NodeIndexes.Length - 1)
                {
                    InsertCollection = InsertCollection[int.Parse(NodeIndexes[i])].Nodes;
                    Math.Min(Interlocked.Increment(ref i), i - 1);
                }

                if (!(InsertCollection == null))
                {
                    InsertCollection.Insert(int.Parse(NodeIndexes[NodeIndexes.Length - 1]),
                        (TreeNode) MovingNode.Clone());
                    TreeView1.SelectedNode = InsertCollection[int.Parse(NodeIndexes[NodeIndexes.Length - 1])];
                    MovingNode.Remove();
                }
            }
        }

        private void trvDragOver(object sender, DragEventArgs e)
        {
            var NodeOver = TreeView1.GetNodeAt(TreeView1.PointToClient(Cursor.Position));
            var NodeMoving = (DBTreeViewNode) e.Data.GetData("FSFormControls.DBTreeView+DBTreeViewNode");
            if (!(NodeOver == null) &&
                (!(NodeOver == NodeMoving) ||
                 !(NodeOver.Parent == null) && NodeOver.Index == NodeOver.Parent.Nodes.Count - 1))
            {
                var OffsetY = TreeView1.PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top;
                var NodeOverImageWidth = TreeView1.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
                var g = TreeView1.CreateGraphics();
                if (NodeOver.ImageIndex == 1)
                {
                    if (OffsetY < NodeOver.Bounds.Height / 2)
                    {
                        var tnParadox = NodeOver;
                        while (!(tnParadox.Parent == null))
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                m_NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }

                        SetNewNodeMap(NodeOver, false);
                        if (SetMapsEqual()) return;
                        Refresh();
                        DrawLeafTopPlaceholders(NodeOver);
                    }
                    else
                    {
                        var tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                m_NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }

                        TreeNode ParentDragDrop = null;
                        if (NodeOver.Parent != null && NodeOver.Index == NodeOver.Parent.Nodes.Count - 1)
                        {
                            var XPos = TreeView1.PointToClient(Cursor.Position).X;
                            if (XPos < NodeOver.Bounds.Left)
                            {
                                ParentDragDrop = NodeOver.Parent;
                                if (XPos <
                                    ParentDragDrop.Bounds.Left -
                                    TreeView1.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width)
                                    if (!(ParentDragDrop.Parent == null))
                                        ParentDragDrop = ParentDragDrop.Parent;
                            }
                        }

                        SetNewNodeMap(ParentDragDrop != null ? ParentDragDrop : NodeOver, true);
                        if (SetMapsEqual()) return;
                        Refresh();
                        DrawLeafBottomPlaceholders(NodeOver, ParentDragDrop);
                    }
                }
                else
                {
                    if (OffsetY < NodeOver.Bounds.Height / 3)
                    {
                        var tnParadox = NodeOver;
                        while (!(tnParadox.Parent == null))
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                m_NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }

                        SetNewNodeMap(NodeOver, false);
                        if (SetMapsEqual()) return;
                        Refresh();
                        DrawFolderTopPlaceholders(NodeOver);
                    }
                    else
                    {
                        if (NodeOver.Parent != null && NodeOver.Index == 0 &&
                            OffsetY > NodeOver.Bounds.Height - NodeOver.Bounds.Height / 3)
                        {
                            var tnParadox = NodeOver;
                            while (tnParadox.Parent != null)
                            {
                                if (tnParadox.Parent == NodeMoving)
                                {
                                    m_NodeMap = "";
                                    return;
                                }

                                tnParadox = tnParadox.Parent;
                            }

                            SetNewNodeMap(NodeOver, true);
                            if (SetMapsEqual()) return;
                            Refresh();
                            DrawFolderTopPlaceholders(NodeOver);
                        }
                        else
                        {
                            if (NodeOver.Nodes.Count > 0)
                            {
                                NodeOver.Expand();
                            }
                            else
                            {
                                if (NodeMoving == NodeOver) return;
                                var tnParadox = NodeOver;
                                while (tnParadox.Parent != null)
                                {
                                    if (tnParadox.Parent == NodeMoving)
                                    {
                                        m_NodeMap = "";
                                        return;
                                    }

                                    tnParadox = tnParadox.Parent;
                                }

                                SetNewNodeMap(NodeOver, false);
                                m_NewNodeMap = m_NewNodeMap.Insert(m_NewNodeMap.Length, "|0");
                                if (SetMapsEqual()) return;
                                Refresh();
                                DrawAddToFolderPlaceholder(NodeOver);
                            }
                        }
                    }
                }
            }
        }

        private void DrawLeafTopPlaceholders(TreeNode NodeOver)
        {
            var g = TreeView1.CreateGraphics();
            var NodeOverImageWidth = TreeView1.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            var LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            var RightPos = TreeView1.Width - 4;
            var LeftTriangle = new Point[5]
            {
                new Point(LeftPos, NodeOver.Bounds.Top - 4),
                new Point(LeftPos, NodeOver.Bounds.Top + 4),
                new Point(LeftPos + 4, NodeOver.Bounds.Y),
                new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                new Point(LeftPos, NodeOver.Bounds.Top - 5)
            };
            var RightTriangle = new Point[5]
            {
                new Point(RightPos, NodeOver.Bounds.Top - 4),
                new Point(RightPos, NodeOver.Bounds.Top + 4),
                new Point(RightPos - 4, NodeOver.Bounds.Y),
                new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                new Point(RightPos, NodeOver.Bounds.Top - 5)
            };
            g.FillPolygon(Brushes.Black, LeftTriangle);
            g.FillPolygon(Brushes.Black, RightTriangle);
            g.DrawLine(new Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top),
                new Point(RightPos, NodeOver.Bounds.Top));
        }

        private void DrawLeafBottomPlaceholders(TreeNode NodeOver, TreeNode ParentDragDrop)
        {
            var g = TreeView1.CreateGraphics();
            var NodeOverImageWidth = TreeView1.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            var LeftPos = 0;
            var RightPos = 0;
            if (!(ParentDragDrop == null))
                LeftPos = ParentDragDrop.Bounds.Left -
                          (TreeView1.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width + 8);
            else
                LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = TreeView1.Width - 4;
            var LeftTriangle = new Point[5]
            {
                new Point(LeftPos, NodeOver.Bounds.Bottom - 4),
                new Point(LeftPos, NodeOver.Bounds.Bottom + 4),
                new Point(LeftPos + 4, NodeOver.Bounds.Bottom),
                new Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
                new Point(LeftPos, NodeOver.Bounds.Bottom - 5)
            };
            var RightTriangle = new Point[5]
            {
                new Point(RightPos, NodeOver.Bounds.Bottom - 4),
                new Point(RightPos, NodeOver.Bounds.Bottom + 4),
                new Point(RightPos - 4, NodeOver.Bounds.Bottom),
                new Point(RightPos - 4, NodeOver.Bounds.Bottom - 1),
                new Point(RightPos, NodeOver.Bounds.Bottom - 5)
            };
            g.FillPolygon(Brushes.Black, LeftTriangle);
            g.FillPolygon(Brushes.Black, RightTriangle);
            g.DrawLine(new Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Bottom),
                new Point(RightPos, NodeOver.Bounds.Bottom));
        }

        private void DrawFolderTopPlaceholders(TreeNode NodeOver)
        {
            var g = TreeView1.CreateGraphics();
            var NodeOverImageWidth = TreeView1.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            var LeftPos = 0;
            var RightPos = 0;
            LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = TreeView1.Width - 4;
            var LeftTriangle = new Point[5]
            {
                new Point(LeftPos, NodeOver.Bounds.Top - 4),
                new Point(LeftPos, NodeOver.Bounds.Top + 4),
                new Point(LeftPos + 4, NodeOver.Bounds.Y),
                new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                new Point(LeftPos, NodeOver.Bounds.Top - 5)
            };
            var RightTriangle = new Point[5]
            {
                new Point(RightPos, NodeOver.Bounds.Top - 4),
                new Point(RightPos, NodeOver.Bounds.Top + 4),
                new Point(RightPos - 4, NodeOver.Bounds.Y),
                new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                new Point(RightPos, NodeOver.Bounds.Top - 5)
            };
            g.FillPolygon(Brushes.Black, LeftTriangle);
            g.FillPolygon(Brushes.Black, RightTriangle);
            g.DrawLine(new Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top),
                new Point(RightPos, NodeOver.Bounds.Top));
        }

        private void DrawAddToFolderPlaceholder(TreeNode NodeOver)
        {
            var g = TreeView1.CreateGraphics();
            var RightPos = NodeOver.Bounds.Right + 6;
            var RightTriangle = new Point[5]
            {
                new Point(RightPos,
                    Convert.ToInt32(NodeOver.Bounds.Y + NodeOver.Bounds.Height / 2 + 4)),
                new Point(RightPos,
                    Convert.ToInt32(NodeOver.Bounds.Y + NodeOver.Bounds.Height / 2 + 4)),
                new Point(RightPos - 4,
                    Convert.ToInt32(NodeOver.Bounds.Y + NodeOver.Bounds.Height / 2)),
                new Point(RightPos - 4,
                    Convert.ToInt32(NodeOver.Bounds.Y + NodeOver.Bounds.Height / 2 - 1)),
                new Point(RightPos, Convert.ToInt32(
                    NodeOver.Bounds.Y + NodeOver.Bounds.Height / 2 - 5))
            };
            Refresh();
            g.FillPolygon(Brushes.Black, RightTriangle);
        }

        private void SetNewNodeMap(TreeNode tnNode, bool boolBelowNode)
        {
            m_NewNodeMap.Length = 0;
            if (boolBelowNode)
                m_NewNodeMap.Insert(0, Convert.ToSByte(tnNode.Index + 1));
            else
                m_NewNodeMap.Insert(0, Convert.ToSByte(tnNode.Index));
            var tnCurNode = tnNode;
            while (!(tnCurNode.Parent == null))
            {
                tnCurNode = tnCurNode.Parent;
                if (m_NewNodeMap.Length == 0 && boolBelowNode)
                    m_NewNodeMap.Insert(0, Convert.ToString(tnCurNode.Index + 1 + double.Parse("|")));
                else
                    m_NewNodeMap.Insert(0, Convert.ToString(tnCurNode.Index + double.Parse("|")));
            }
        }

        private bool SetMapsEqual()
        {
            if (m_NewNodeMap.ToString() == m_NodeMap) return true;

            m_NodeMap = m_NewNodeMap.ToString();
            return false;
        }


        public void AddCommonLevel(string levelName, int level, string value)
        {
            DBTreeViewNode newNode = null;

            TreeView1.BeginUpdate();

            foreach (DBTreeViewNode node in TreeView1.Nodes)
                if (node.Level == level - 1)
                {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;

                    newNode = new DBTreeViewNode();
                    newNode.Value = value;
                    newNode.Text = levelName;
                    newNode.Level = level;
                    newNode.ImageIndex = 2;
                    newNode.SelectedImageIndex = 2;
                    node.Nodes.Add(newNode);
                }

            TreeView1.EndUpdate();
        }


        public void AddLevel(DBControl DBC, string DBField, string DBFieldValue, string DBFieldData)
        {
            DBTreeViewNode newNode = null;

            if (DBC.Connected == false) DBC.Connect();

            TreeView1.BeginUpdate();
            foreach (DataRow row in DBC.DataTable.Rows)
                if (Level == 0)
                {
                    newNode = new DBTreeViewNode();
                    newNode.Value = row[DBFieldData];
                    newNode.Text = row[DBField] + "";
                    newNode.Level = Level;
                    newNode.ImageIndex = 0;
                    newNode.SelectedImageIndex = 1;
                    TreeView1.Nodes.Add(newNode);
                }
                else
                {
                    AddNodeLevel(TreeView1.Nodes, row, DBField, DBFieldValue, DBFieldData, -1);
                }

            Level = Level + 1;

            TreeView1.EndUpdate();
        }


        public void AddLevel(string title, string value, int level)
        {
            DBTreeViewNode newNode = null;

            TreeView1.BeginUpdate();

            newNode = new DBTreeViewNode();
            newNode.Value = value;
            newNode.Text = title;
            newNode.Level = level;
            newNode.ImageIndex = 0;
            newNode.SelectedImageIndex = 1;
            TreeView1.Nodes.Add(newNode);

            TreeView1.EndUpdate();
        }


        public void AddLevelBd(DBControl DBC, string DBField, string DBFieldData, int level)
        {
            if (DBC.Connected == false) 
                DBC.Connect();

            TreeView1.BeginUpdate();
            foreach (DataRow row in DBC.DataTable.Rows)
            {
                if (!AddNodeLevel(TreeView1.Nodes, row, DBField, DBFieldData, DBFieldData, level))
                {
                    throw new ExceptionUtil("Nivel " + level + " no añadido.");
                }
            }

            TreeView1.EndUpdate();
        }


        public void AddLevelBd(string DBField, string DBFieldData, int level)
        {
            if (DataControl is null)
                throw new ExceptionUtil("Datacontrol es nulo");

            if (DataControl.Connected == false)
                DataControl.Connect();

            TreeView1.BeginUpdate();
            foreach (DataRow row in DataControl.DataTable.Rows)
            {
                if(!AddNodeLevel(TreeView1.Nodes, row, DBField, DBFieldData, DBFieldData, level))
                {
                    throw new ExceptionUtil("Nivel " + level + " no añadido.");
                }
            }

            TreeView1.EndUpdate();
        }


        private bool AddNodeLevel(TreeNodeCollection nodes, DataRow row, string DBField, string DBFieldValue,
            string DBFieldData, int level)
        {
            if (String.IsNullOrEmpty(DBFieldValue))
                DBFieldValue = DBField;

            if (String.IsNullOrEmpty(DBFieldData))
                DBFieldData = DBField;

            DBTreeViewNode newNode = null;
            var lev = 0;

            if (level != -1)
                lev = level;
            else
                lev = Level - 1;

            foreach (DBTreeViewNode node in nodes)
                if (node.Level == lev)
                {
                    if (DBFieldValue != "")
                        if (node.Value == row[DBFieldValue])
                        {
                            node.ImageIndex = 0;
                            node.SelectedImageIndex = 1;

                            newNode = new DBTreeViewNode();
                            newNode.Value = row[DBFieldData];
                            newNode.Text = row[DBField] + "";
                            newNode.Level = Level;
                            newNode.ImageIndex = 2;
                            newNode.SelectedImageIndex = 2;
                            node.Nodes.Add(newNode);
                            return true;
                        }
                }
                else
                {
                    if (node.Nodes.Count > 0)
                        if (AddNodeLevel(node.Nodes, row, DBField, DBFieldValue, DBFieldData, -1))
                            return true;
                }

            return false;
        }


        private bool AddNodeLevel(TreeNodeCollection nodes, DataRow row, string DBField, string DBFieldValue,
            string DBFieldData)
        {
            return AddNodeLevel(nodes, row, DBField, DBFieldValue, DBFieldData, -1);
        }


        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            if (null != DoubleClick) DoubleClick(this, e);
        }


        private void TreeView1_Click(object sender, EventArgs e)
        {
            if (null != Click) Click(this, e);
        }


        public void LoadXML(string filename)
        {
            try
            {
                TreeView1.Nodes.Clear();
                var tmpxmldoc = new XmlDocument();
                tmpxmldoc.Load(filename);
                FillTreeXML(tmpxmldoc.DocumentElement, TreeView1.Nodes);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }



        private void FillTreeXML(XmlNode node, TreeNodeCollection parentnode)
        {
            if ((node == null) | (node.NodeType == XmlNodeType.Text) | (node.NodeType == XmlNodeType.CDATA)) 
                return;

            var tmptreenodecollection = AddNodeToTree(node, parentnode);

            foreach (XmlNode tmpchildnode in node.ChildNodes) 
                FillTreeXML(tmpchildnode, tmptreenodecollection);
        }


        private TreeNodeCollection AddNodeToTree(XmlNode node, TreeNodeCollection parentnode)
        {
            var newchildnode = CreateTreeNodeFromXmlNode(node);

            if (newchildnode == null) return parentnode;

            if (parentnode != null) parentnode.Add(newchildnode);

            return newchildnode.Nodes;
        }


        private TreeNode CreateTreeNodeFromXmlNode(XmlNode node)
        {
            var tmptreenode = new TreeNode();

            if (node.HasChildNodes & !(node.FirstChild.Value == Convert.ToString(DBNull.Value)))
            {
                tmptreenode = new TreeNode(node.Name);
                var tmptreenode2 = new TreeNode(node.FirstChild.Value);
                tmptreenode.Nodes.Add(tmptreenode2);
            }
            else if (node.NodeType != XmlNodeType.CDATA)
            {
                tmptreenode = new TreeNode(node.Name);
            }

            return tmptreenode;
        }


        public void SaveXML(string fileName)
        {
            var sr = new StreamWriter(fileName, false, Encoding.UTF8);
            sr.WriteLine("<?xml version='1.0' encoding='utf-8' ?>");

            var ie = TreeView1.Nodes.GetEnumerator();
            if (ie.MoveNext())
            {
                var tn = (TreeNode) ie.Current;
                sr.WriteLine("<" + tn.Text + ">");
                ParseNode(tn, sr);
            }

            sr.Close();
        }


        private void ParseNode(TreeNode tn, StreamWriter sr)
        {
            var ie = tn.Nodes.GetEnumerator();

            var parentnode = "";

            parentnode = tn.Text;

            TreeNode ctn = null;

            while (ie.MoveNext())
            {
                ctn = (TreeNode) ie.Current;

                if (ctn.GetNodeCount(true) == 0)
                    sr.Write(ctn.Text);
                else
                    sr.Write("<" + ctn.Text + ">");
                if (ctn.GetNodeCount(true) > 0) ParseNode(ctn, sr);
            }

            sr.Write("</" + parentnode + ">");
            sr.WriteLine("");
        }

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog1.ShowDialog();

            try
            {
                SaveXML(SaveFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void MenuItem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.ShowDialog();

            try
            {
                LoadXML(OpenFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void TreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (EnableReArrange) TreeView1.SelectedNode = TreeView1.GetNodeAt(e.X, e.Y);

            base.OnMouseDown(e);
        }


        private void TreeView1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }


        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (null != AfterSelect)
                AfterSelect(this, e);
        }

        private void TreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (null != BeforeSelect)
                BeforeSelect(this, e);
        }


        private void TreeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (null != AfterCollapse)
                AfterCollapse(this, e);
        }

        private void TreeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (null != BeforeCollapse)
                BeforeCollapse(this, e);
        }

        private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (null != AfterExpand)
                AfterExpand(this, e);
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (null != BeforeExpand)
                BeforeExpand(this, e);
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView1.SelectedNode = e.Node;

            if (null != NodeMouseDoubleClick)
                NodeMouseDoubleClick(this, e);
        }

        private void TreeView1_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            //TreeView1.SelectedNode = e.Node;

            if (null != NodeMouseHover)
                NodeMouseHover(this, e);
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView1.SelectedNode = e.Node;

            if (null != NodeMouseClick)
                NodeMouseClick(this, e);
        }


        public DBTreeViewNode FindNode(string value, TreeNodeCollection nodes)
        {
            DBTreeViewNode nd;

            if (nodes == null) nodes = Nodes;

            foreach (DBTreeViewNode n in nodes)
            {
                if (Convert.ToString(n.Value) == value) return n;
                if (n.Nodes.Count > 0)
                {
                    nd = FindNode(value, n.Nodes);
                    if (!(nd == null)) return nd;
                }
            }

            return null;
        }

        public DBTreeViewNode FindNode(string value)
        {
            return FindNode(value, null);
        }

        public DBTreeViewNode FindNodeByXPath(string xpath)
        {
            return FindNodeByXPath(xpath, null);
        }

        public DBTreeViewNode FindNodeByXPath(string xpath, TreeNodeCollection nodes)
        {
            if (nodes == null) nodes = Nodes;

            foreach (DBTreeViewNode node in nodes)
            {
                if (node.GetXPath() == xpath)
                    return node;

                if (node.Nodes.Count > 0)
                {
                    DBTreeViewNode tn = FindNodeByXPath(xpath, node.Nodes);
                    if (tn != null)
                        return tn;
                }
            }

            return null;
        }


        private void TreeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (EnableReArrange) e.Effect = DragDropEffects.Move;
        }


        private void TreeView1_DragOver(object sender, DragEventArgs e)
        {
            if (EnableReArrange) trvDragOver(sender, e);
        }


        private void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (EnableReArrange) DoDragDrop(e.Item, DragDropEffects.Move);
        }


        private void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            if (EnableReArrange) trvDragDrop(sender, e);
        }

        #region '" Windows Form Designer generated code "' 

        internal ContextMenuStrip ContextMenu1;
        internal ImageList ImageList1;
        internal ToolStripMenuItem MenuItem1;
        internal ToolStripMenuItem MenuItem2;
        internal OpenFileDialog OpenFileDialog1;
        internal SaveFileDialog SaveFileDialog1;
        internal TreeView TreeView1;
        private IContainer components;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBTreeView));
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.ContextMenu1 = new System.Windows.Forms.ContextMenuStrip();
            this.MenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // TreeView1
            // 
            this.TreeView1.AllowDrop = true;
            this.TreeView1.ContextMenuStrip = this.ContextMenu1;
            this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView1.ImageIndex = 0;
            this.TreeView1.ImageList = this.ImageList1;
            this.TreeView1.Location = new System.Drawing.Point(0, 0);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.SelectedImageIndex = 0;
            this.TreeView1.Size = new System.Drawing.Size(149, 126);
            this.TreeView1.TabIndex = 0;
            // 
            // ContextMenu1
            // 
            this.ContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.MenuItem1,
            this.MenuItem2});
            // 
            // MenuItem1
            // 
            this.MenuItem1.ImageIndex = 0;
            this.MenuItem1.Text = "Guardar XML";
            // 
            // MenuItem2
            // 
            this.MenuItem2.ImageIndex = 1;
            this.MenuItem2.Text = "Cargar XML";
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.White;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            this.ImageList1.Images.SetKeyName(2, "");
            // 
            // DBTreeView
            // 
            this.ContextMenuStrip = this.ContextMenu1;
            this.Controls.Add(this.TreeView1);
            this.Name = "DBTreeView";
            this.Size = new System.Drawing.Size(149, 126);
            this.ResumeLayout(false);

        }

        #endregion
    }
}