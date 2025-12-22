using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    [ToolboxItem(true)]
    public delegate void DBEditorButtonEventHandler(object sender, DBEditorButtonEventArgs e);

    public class DBEditorButtonEventArgs : EventArgs
    {
        public Button Button;
        public Element Element;

        public Item ClickedItem { get; set; }

        public DBEditorButtonEventArgs(DBButtonEx button)
        {
            this.Button = button.Button;
            this.Element = new Element() { Rect = button.Bounds };
            this.ClickedItem = new Item() { Key = button.Key };
        }

        public DBEditorButtonEventArgs(DBButton button)
        {
            this.Button = button;
            this.Element = new Element() { Rect = button.Bounds };
            this.ClickedItem = new Item() { Key = button.Key };
        }

        public DBEditorButtonEventArgs(Button button)
        {
            this.Button = button;
            this.Element = new Element() { Rect = button.Bounds };
            this.ClickedItem = new Item() { Key = button.Name };
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