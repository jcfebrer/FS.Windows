using System.Windows.Forms;

namespace FSFormControls
{
    public class DBTreeViewNode : TreeNode
    {
        public DBTreeViewNode()
        {
        }

        public DBTreeViewNode(string text)
        {
            this.Text = text;
        }

        public object Value { get; set; }

        public new int Level { get; set; }


        public string GetXPath()
        {
            DBTreeViewNode treeNode = this;
            string xpath = "";
            while (treeNode != null)
            {
                int ind = GetXpathIndex(treeNode);
                if (ind > 1)
                    xpath = "/" + treeNode.Text.ToLower() + "[" + ind + "]" + xpath;
                else
                    xpath = "/" + treeNode.Text.ToLower() + xpath;

                treeNode = (DBTreeViewNode)treeNode.Parent;
            }
            return xpath;
        }

        private int GetXpathIndex(DBTreeViewNode treeNode)
        {
            if (treeNode.Parent == null) return 0;
            int ind = 0, indEle = 0;
            string tagName = treeNode.Text;
            TreeNodeCollection elecol = treeNode.Parent.Nodes;
            foreach (DBTreeViewNode it in elecol)
            {
                if (it.Text == tagName)
                {
                    ind++;
                    if (it.Value == treeNode.Value)
                        indEle = ind;
                }
            }
            if (ind > 1) return indEle;
            return 0;
        }
    }
}