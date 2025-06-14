#region

using FSLibrary;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace FSMouseKeyboardLibrary
{
    [ComVisible(false)]
    public class Keyboard
    {
        #region LedKeys enum

        public enum LedKeys
        {
            NumLock = Keys.NumLock,
            CapsLock = Keys.CapsLock,
            ScrollLock = Keys.Scroll
        }

        #endregion

        private const int KEYEVENTF_KEYUP = 0X2;

        public bool CapsLock
        {
            get { return IsCapsLock(); }
            set { SetKeyState(Convert.ToInt64(Keys.CapsLock), value); }
        }

        private void SetKeyState(long key, bool state)
        {
            var keys = new byte[257];
            Win32API.GetKeyboardState(keys);

            keys[Convert.ToInt32(key)] = Convert.ToByte(Math.Abs(Convert.ToInt32(state)));
            Win32API.SetKeyboardState(keys);
        }


        private bool GetKeyState2(long key)
        {
            var keys = new byte[257];
            Win32API.GetKeyboardState(keys);

            if (keys[Convert.ToInt32(key)] == 1)
                return true;
            return false;
        }

        public bool IsKeyPressed(Keys key)
        {
            short state = Win32API.GetKeyState((int)key);
            return ((state & 128) != 0);
        }

        public void TroggleLed(LedKeys key, bool state)
        {
            var keys = new byte[257];
            Win32API.GetKeyboardState(keys);

            if (keys[Convert.ToInt32(key)] != Convert.ToInt32(!state) + 1)
            {
                Win32API.keybd_event(Convert.ToByte(key), 0, 0, 0);
                Win32API.keybd_event(Convert.ToByte(key), 0, KEYEVENTF_KEYUP, 0);
            }
        }


        private bool IsCapsLock()
        {
            var state = Win32API.GetKeyState(Convert.ToInt32(Keys.CapsLock));

            if (state == 1)
                return true;
            return false;
        }
    }
}