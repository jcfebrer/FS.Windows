#region

using System.Collections;
using System.Windows.Forms;

#endregion

namespace FSMouseKeyboardLibrary
{
    public class MouseActionEntry
    {
        public enum EventType
        {
            MouseMove,
            MouseDown,
            MouseUp,
            MouseWheel,
            KeyDown,
            KeyUp,
            KeyPress
        }

        int x;
        int y;
        int interval;
        EventType type;
        string process;
        System.Windows.Forms.MouseButtons button;
        Keys keyCode;

        public MouseActionEntry()
        {
        }
        public MouseActionEntry(int x, int y, int interval, EventType type, string process, System.Windows.Forms.MouseButtons button, Keys keyCode) //, System.EventArgs eventArgs)
        {
            this.x = x;
            this.y = y;
            this.interval = interval;
            this.type = type;
            this.process = process;
            this.button = button;
            this.keyCode = keyCode;
        }

        public int X
        {
            set { x = value; }
            get { return x; }
        }
        public int Y
        {
            set { y = value; }
            get { return y; }
        }

        public int Interval
        {
            set { interval = value; }
            get { return interval; }
        }
        public EventType Type
        {
            set { type = value; }
            get { return type; }
        }

        public string Process
        {
            set { process = value; }
            get { return process; }
        }

        public System.Windows.Forms.MouseButtons Button
        {
            set { button = value; }
            get { return button; }
        }

        public Keys KeyCode
        {
            set { keyCode = value; }
            get { return keyCode; }
        }
    }
    public class MouseActionsEntry : CollectionBase
    {
        public MouseActionsEntry()
        {
        }

        public MouseActionEntry this[int index]
        {
            get { return (MouseActionEntry)List[index]; }
            set { List[index] = value; }
        }

        public void Add(MouseActionEntry entry)
        {
            List.Add(entry);
        }

        public void Remove(MouseActionEntry entry)
        {
            List.Remove(entry);
        }

        public MouseActionEntry Find(MouseActionEntry entry)
        {
            foreach (MouseActionEntry action in List)
                if (entry == action)
                    return action;
            return null;
        }

        public MouseActionEntry Find(string name)
        {
            foreach (MouseActionEntry f in List)
                if (f.Process.ToLower() == name.ToLower())
                    return f;
            return null;
        }

        public bool Exist(string name)
        {
            foreach (MouseActionEntry f in List)
                if (f.Process == name)
                    return true;
            return false;
        }
    }
}