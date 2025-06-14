using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using System.Threading;

namespace FSMouseKeyboardLibrary
{

    /// <summary>
    /// Mouse buttons that can be pressed
    /// </summary>
    public enum MouseButton
    {
        Left = 0x2,
        Right = 0x8,
        Middle = 0x20
    }

    /// <summary>
    /// Operations that simulate mouse events
    /// </summary>
    public static class MouseSimulator
    {
        #region Properties

        /// <summary>
        /// Gets or sets a structure that represents both X and Y mouse coordinates
        /// </summary>
        public static Point Position
        {
            get
            {
                return new Point(Cursor.Position.X, Cursor.Position.Y);
            }
            set
            {
                Cursor.Position = value;
            }
        }

        /// <summary>
        /// Gets or sets only the mouse's x coordinate
        /// </summary>
        public static int X
        {
            get
            {
                return Cursor.Position.X;
            }
            set
            {
                Cursor.Position = new Point(value, Y);
            }
        }

        /// <summary>
        /// Gets or sets only the mouse's y coordinate
        /// </summary>
        public static int Y
        {
            get
            {
                return Cursor.Position.Y;
            }
            set
            {
                Cursor.Position = new Point(X, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Press a mouse button down
        /// </summary>
        /// <param name="button"></param>
        public static void MouseDown(MouseButton button)
        {
            Win32API.mouse_event(((int)button), 0, 0, 0, 0);
        }

        /// <summary>
        /// Press a mouse button down
        /// </summary>
        /// <param name="button"></param>
        public static void MouseDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    MouseDown(MouseButton.Left);
                    break;
                case MouseButtons.Middle:
                    MouseDown(MouseButton.Middle);
                    break;
                case MouseButtons.Right:
                    MouseDown(MouseButton.Right);
                    break;
            }
        }

        /// <summary>
        /// Let a mouse button up
        /// </summary>
        /// <param name="button"></param>
        public static void MouseUp(MouseButton button)
        {
            Win32API.mouse_event(((int)button) * 2, 0, 0, 0, 0);
        }

        /// <summary>
        /// Let a mouse button up
        /// </summary>
        /// <param name="button"></param>
        public static void MouseUp(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    MouseUp(MouseButton.Left);
                    break;
                case MouseButtons.Middle:
                    MouseUp(MouseButton.Middle);
                    break;
                case MouseButtons.Right:
                    MouseUp(MouseButton.Right);
                    break;
            }
        }

        /// <summary>
        /// Click a mouse button (down then up)
        /// </summary>
        /// <param name="button"></param>
        public static void Click(MouseButton button)
        {
            MouseDown(button);
            MouseUp(button);
        }

        /// <summary>
        /// Click a mouse button (down then up)
        /// </summary>
        /// <param name="button"></param>
        public static void Click(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    Click(MouseButton.Left);
                    break;
                case MouseButtons.Middle:
                    Click(MouseButton.Middle);
                    break;
                case MouseButtons.Right:
                    Click(MouseButton.Right);
                    break;
            }
        }

        /// <summary>
        /// Double click a mouse button (down then up twice)
        /// </summary>
        /// <param name="button"></param>
        public static void DoubleClick(MouseButton button)
        {
            Click(button);
            Click(button);
        }

        /// <summary>
        /// Double click a mouse button (down then up twice)
        /// </summary>
        /// <param name="button"></param>
        public static void DoubleClick(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    DoubleClick(MouseButton.Left);
                    break;
                case MouseButtons.Middle:
                    DoubleClick(MouseButton.Middle);
                    break;
                case MouseButtons.Right:
                    DoubleClick(MouseButton.Right);
                    break;
            }
        }

        /// <summary>
        /// Roll the mouse wheel. Delta of 120 wheels up once normally, -120 wheels down once normally
        /// </summary>
        /// <param name="delta"></param>
        public static void MouseWheel(int delta)
        {

            Win32API.mouse_event(Win32APIEnums.MOUSEEVENTF_WHEEL, 0, 0, delta, 0);

        }

        /// <summary>
        /// Show a hidden current on currently application
        /// </summary>
        public static void Show()
        {
            Win32API.ShowCursor(true);
        }

        /// <summary>
        /// Hide mouse cursor only on current application's forms
        /// </summary>
        public static void Hide()
        {
            Win32API.ShowCursor(false);
        }

        #endregion


        #region Mover el ratón entre 2 puntos

        public static void MouseMove(int x, int y)
        {
            Position = new Point(x, y);
        }

        public static void MouseMove(Point point)
        {
            Position = point;
        }

        public static void MouseMove(Point pointA, Point pointB, int pointCount = 15, int sleep = 50)
        {
            MouseMove(pointA.X, pointA.Y, pointB.X, pointB.Y, pointCount, sleep);
        }

        public static void MouseMove(int fromX, int fromY, int toX, int toY, int pointCount = 15, int sleep = 50)
        {
            int diffX = toX - fromX;
            int diffY = toY - fromY;

            int intervalX = diffX / pointCount;
            int intervalY = diffY / pointCount;

            for (int i = 0; i < pointCount; i++)
            {
                Position = new Point(fromX + intervalX * i, fromY + intervalY * i);
                Thread.Sleep(sleep);

                Application.DoEvents();
            }
        }

        public static void MouseMove2(Point a, Point b, int pointCount = 15, int sleep = 50)
        {
            Double d = Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y)) / pointCount;
            Double fi = Math.Atan2(b.Y - a.Y, b.X - a.X);

            for (int i = 0; i < pointCount; i++)
            {
                Position = new Point((int)(a.X + i * d * Math.Cos(fi)), (int)(a.Y + i * d * Math.Sin(fi)));
                Thread.Sleep(sleep);

                Application.DoEvents();
            }
        }

        #endregion
    }

}
