using FSException;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace FSMouseKeyboardLibrary
{
    public class ProcessActions
    {
        private static bool cancel = false;
        public delegate void ActionEntryEventHandler(MouseActionEntry action, int position);
        public static event ActionEntryEventHandler OnEntryProcess;

        public static void Do(MouseActionsEntry actions, bool repeat, decimal repeatCount, bool makePointsIntermediate)
        {
            cancel = false;

            if (!repeat)
            {
                repeatCount = 1;
            }

            if (repeatCount > 0)
            {
            for (int f = 0; f < repeatCount; f++)
            { 
                if (cancel)
                    break;

                ProcessActions.Do(actions, makePointsIntermediate);
                }
            }
            else
            {
                do
                {
                    if (cancel)
                        break;

                    ProcessActions.Do(actions, makePointsIntermediate);
                } while (true);
            }
        }

        public static void Do(MouseActionsEntry actions, bool makePointsIntermediate)
        {
            int f = 0;
            Point lastPoint = new Point(0, 0);

            foreach (MouseActionEntry action in actions)
            {
                if (cancel)
                    break;

                OnEntryProcess(action, f++);


                if (makePointsIntermediate)
                {
                    Point currentPoint = new Point(action.X, action.Y);
                    MouseSimulator.MouseMove(lastPoint, currentPoint);
                    lastPoint = currentPoint;
                }
                else
                {
                    Thread.Sleep(action.Interval);
                    MouseSimulator.X = action.X;
                    MouseSimulator.Y = action.Y;
                }

                switch (action.Type)
                {
                    case MouseActionEntry.EventType.MouseMove:
                        {
                            //MouseSimulator.X = action.X;
                            //MouseSimulator.Y = action.Y;
                        }
                        break;
                    case MouseActionEntry.EventType.MouseDown:
                        {
                            MouseSimulator.Click(action.Button);
                        }
                        break;
                    case MouseActionEntry.EventType.MouseUp:
                        {
                            MouseSimulator.MouseUp(action.Button);
                        }
                        break;
                    case MouseActionEntry.EventType.KeyDown:
                        {
                            KeyboardSimulator.KeyDown(action.KeyCode);
                        }
                        break;
                    case MouseActionEntry.EventType.KeyUp:
                        {
                            KeyboardSimulator.KeyUp(action.KeyCode);
                        }
                        break;
                    case MouseActionEntry.EventType.KeyPress:
                        {
                            KeyboardSimulator.KeyPress(action.KeyCode);
                        }
                        break;
                    default:
                        break;
                }

                Application.DoEvents();
            }
        }

        public static void Cancel()
        {
            cancel = true;
        }

        public static MouseActionsEntry OpenFileXml(string file)
        {
            //Get data from XML file
            XmlSerializer ser = new XmlSerializer(typeof(MouseActionsEntry));
            using (FileStream fs = System.IO.File.Open(file, FileMode.Open))
            {
                try
                {
                    MouseActionsEntry entries = (MouseActionsEntry)ser.Deserialize(fs);
                    return entries;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Abrir XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public static bool SaveFileXml(string file, MouseActionsEntry actionsEntry)
        {
            XmlSerializer ser = new XmlSerializer(typeof(MouseActionsEntry));

            using (XmlWriter writer = XmlWriter.Create(file))
            {
                try
                {
                    ser.Serialize(writer, actionsEntry);
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Abrir XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
