#region

using System.Collections;
using System.ComponentModel;

#endregion

namespace FSFormControls
{
    public abstract class DBHtmlNode
    {
        protected DBHtmlElement mParent;

        protected DBHtmlNode()
        {
            mParent = null;
        }


        [Category("Navigation")]
        [Description("The parent node of this one")]
        public DBHtmlElement Parent => mParent;

        [Category("Navigation")]
        [Description("The next sibling node")]
        public DBHtmlNode Next
        {
            get
            {
                if (Index == -1) return null;

                if (Parent.Nodes.Count > Index + 1)
                    return Parent.Nodes[Index + 1];
                return null;
            }
        }

        [Category("Navigation")]
        [Description("The previous sibling node")]
        public DBHtmlNode Previous
        {
            get
            {
                if (Index == -1) return null;

                if (Index > 0)
                    return Parent.Nodes[Index - 1];
                return null;
            }
        }

        [Category("Navigation")]
        [Description("The first child of this node")]
        public DBHtmlNode FirstChild
        {
            get
            {
                if (this is DBHtmlElement)
                {
                    if (((DBHtmlElement) this).Nodes.Count == 0)
                        return null;
                    return ((DBHtmlElement) this).Nodes[0];
                }

                return null;
            }
        }

        [Category("Navigation")]
        [Description("The last child of this node")]
        public DBHtmlNode LastChild
        {
            get
            {
                if (this is DBHtmlElement)
                {
                    if (((DBHtmlElement) this).Nodes.Count == 0)
                        return null;
                    return ((DBHtmlElement) this).Nodes[((DBHtmlElement) this).Nodes.Count - 1];
                }

                return null;
            }
        }

        [Category("Navigation")]
        [Description("The zero-based index of this node in the parent's nodes collection")]
        public int Index
        {
            get
            {
                if (mParent == null)
                    return -1;
                return mParent.Nodes.IndexOf(this);
            }
        }

        [Category("Navigation")]
        [Description("Is this node a root node?")]
        public bool IsRoot => mParent == null;

        [Category("Navigation")]
        [Description("Is this node a child of another?")]
        public bool IsChild => mParent != null;

        [Category("Navigation")]
        [Description("Does this node have any children?")]
        public bool IsParent
        {
            get
            {
                if (this is DBHtmlElement)
                    return ((DBHtmlElement) this).Nodes.Count > 0;
                return false;
            }
        }

        [Category("Output")]
        [Description("The HTML that represents this node and all the children")]
        public abstract string HTML { get; }

        [Category("Output")]
        [Description("The XHTML that represents this node and all the children")]
        public abstract string XHTML { get; }

        [Category("Relationships")]
        public bool IsDescendentOf(DBHtmlNode node)
        {
            DBHtmlNode parent = mParent;
            while (parent != null)
            {
                if (parent == node) return true;
                parent = parent.Parent;
            }

            return false;
        }


        [Category("Relationships")]
        public bool IsAncestorOf(DBHtmlNode node)
        {
            return node.IsDescendentOf(this);
        }


        [Category("Relationships")]
        public DBHtmlNode GetCommonAncestor(DBHtmlNode node)
        {
            var thisParent = this;
            while (thisParent != null)
            {
                var thatParent = node;
                while (thatParent != null)
                {
                    if (thisParent == thatParent) return thisParent;
                    thatParent = thatParent.Parent;
                }

                thisParent = thisParent.Parent;
            }

            return null;
        }


        [Category("General")]
        public void Remove()
        {
            if (mParent != null) mParent.Nodes.RemoveAt(Index);
        }


        internal void SetParent(DBHtmlElement parentNode)
        {
            mParent = parentNode;
        }


        [Category("General")]
        [Description("This is true if this is a text node")]
        public bool IsText()
        {
            return this is DBHtmlText;
        }


        [Category("General")]
        [Description("This is true if this is an element node")]
        public bool IsElement()
        {
            return this is DBHtmlElement;
        }
    }


    public class DBHtmlNodeCollection : CollectionBase
    {
        private readonly DBHtmlElement mParent;

        public DBHtmlNodeCollection()
        {
            mParent = null;
        }

        internal DBHtmlNodeCollection(DBHtmlElement parent)
        {
            mParent = parent;
        }

        public DBHtmlNode this[int index]
        {
            get { return (DBHtmlNode) InnerList[index]; }
            set
            {
                if (mParent != null) value.SetParent(mParent);
                InnerList[index] = value;
            }
        }

        public DBHtmlNode this[string name]
        {
            get
            {
                var results = FindByName(name, false);
                if (results.Count > 0)
                    return results[0];
                return null;
            }
        }

        public int Add(DBHtmlNode node)
        {
            if (mParent != null) node.SetParent(mParent);
            return List.Add(node);
        }


        public int IndexOf(DBHtmlNode node)
        {
            return List.IndexOf(node);
        }


        public void Insert(int index, DBHtmlNode node)
        {
            if (mParent != null) node.SetParent(mParent);
            InnerList.Insert(index, node);
        }


        public DBHtmlNodeCollection FindByName(string name)
        {
            return FindByName(name, true);
        }


        public DBHtmlNodeCollection FindByText(string text)
        {
            return FindByText(text, true);
        }


        public DBHtmlNodeCollection FindByName(string name, bool searchChildren)
        {
            var results = new DBHtmlNodeCollection(null);
            foreach (DBHtmlNode node in List)
                if (node is DBHtmlElement)
                {
                    if (((DBHtmlElement) node).Name.ToLower().Equals(name.ToLower())) results.Add(node);
                    if (searchChildren)
                        foreach (
                            DBHtmlNode matchedChild in ((DBHtmlElement) node).Nodes.FindByName(name, searchChildren))
                            results.Add(matchedChild);
                }

            return results;
        }


        public DBHtmlNodeCollection FindByText(string text, bool searchChildren)
        {
            var results = new DBHtmlNodeCollection(null);
            foreach (DBHtmlNode node in List)
                if (node is DBHtmlElement)
                {
                    var nodeText = ((DBHtmlElement) node).Text.ToLower();
                    if (nodeText.IndexOf(text.ToLower()) + 1 > 0) results.Add(node);
                    if (searchChildren)
                        foreach (
                            DBHtmlNode matchedChild in ((DBHtmlElement) node).Nodes.FindByText(text, searchChildren))
                            results.Add(matchedChild);
                }

            return results;
        }


        public DBHtmlNodeCollection FindByAttributeName(string attributeName, bool searchChildren)
        {
            var results = new DBHtmlNodeCollection(null);
            foreach (DBHtmlNode node in List)
                if (node is DBHtmlElement)
                {
                    foreach (DBHtmlAttribute attribute in ((DBHtmlElement) node).Attributes)
                        if (attribute.Name.ToLower().Equals(attributeName.ToLower()))
                        {
                            results.Add(node);
                            break;
                        }

                    if (searchChildren)
                        foreach (
                            DBHtmlNode matchedChild in
                            ((DBHtmlElement) node).Nodes.FindByAttributeName(attributeName, searchChildren))
                            results.Add(matchedChild);
                }

            return results;
        }

        public DBHtmlNodeCollection FindByAttributeNameValue(string attributeName, string attributeValue,
            bool searchChildren)

        {
            return FindByAttributeNameValue(attributeName, attributeValue, false,
                searchChildren);
        }

        public DBHtmlNodeCollection FindByAttributeNameValue(string attributeName, string attributeValue,
            bool exactValue,
            bool searchChildren)
        {
            var results = new DBHtmlNodeCollection(null);
            foreach (DBHtmlNode node in List)
                if (node is DBHtmlElement)
                {
                    foreach (DBHtmlAttribute attribute in ((DBHtmlElement) node).Attributes)
                        if (attribute.Name.ToLower().Equals(attributeName.ToLower()))
                        {
                            var attrib = attribute.Value.ToLower();
                            var attribValue = attributeValue.ToLower();

                            if (exactValue)
                            {
                                if (attrib.Equals(attribValue))
                                    results.Add(node);
                            }
                            else
                            {
                                if (attrib.IndexOf(attribValue) + 1 > 0)
                                    results.Add(node);
                            }

                            break;
                        }

                    if (searchChildren)
                        foreach (
                            DBHtmlNode matchedChild in
                            ((DBHtmlElement) node).Nodes.FindByAttributeNameValue(attributeName, attributeValue,
                                exactValue,
                                searchChildren))
                            results.Add(matchedChild);
                }

            return results;
        }
    }
}