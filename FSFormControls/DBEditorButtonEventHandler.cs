using System;
using System.ComponentModel;
using System.Drawing;

namespace FSFormControls
{
    [ToolboxItem(true)]
    [Serializable]
    public delegate void DBEditorButtonEventHandler(object sender, DBEditorButtonEventArgs e);

    public class DBEditorButtonEventArgs : EventArgs
    {
        public DBButtonEx Button;
        public Element Element;

        public Item ClickedItem { get; set; }

        public DBEditorButtonEventArgs(DBButtonEx button)
        {
        }

        public DBEditorButtonEventArgs()
        {
        }
    }

    public class Element
    {
        public Rectangle Rect;
    }

    public class Item
    {
        public string Key;
    }
}