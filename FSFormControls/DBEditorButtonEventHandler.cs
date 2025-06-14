using System;
using System.Drawing;

namespace FSFormControls
{
    public delegate void DBEditorButtonEventHandler(object sender, DBEditorButtonEventArgs e);

    public class DBEditorButtonEventArgs : EventArgs
    {
        public DBButton Button;
        public Element Element;

        public Item ClickedItem { get; set; }

        public DBEditorButtonEventArgs(DBButton button)
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