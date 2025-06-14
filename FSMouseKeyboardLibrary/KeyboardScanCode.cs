using System;
using System.Collections.Generic;
using System.Text;

namespace FSMouseKeyboardLibrary
{
    public class KeyboardScanCode
    {
        private int[] _virtualKeyScanCodes = new int[255];

        public KeyboardScanCode()
        {
            Initialize();
        }

        public int VirtualKeyToScanCode(int virtualKey)
        {
            return _virtualKeyScanCodes[virtualKey];
        }
        private void Initialize()
        {
            IntPtr keybHandle = FSLibrary.Win32API.GetKeyboardLayout(0);

            // Scroll through the Scan Code (SC) values and get the Virtual Key (VK)
            // values in it. Then, store the SC in each valid VK so it can act as both a 
            // flag that the VK is valid, and it can store the SC value.
            for (int scanCode = 0x01; scanCode <= 0xff; scanCode++)
            {
                int virtualKeyCode = FSLibrary.Win32API.MapVirtualKeyEx(
                    scanCode,
                    (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VSC_TO_VK,
                    keybHandle);
                if (virtualKeyCode != 0)
                {
                    this._virtualKeyScanCodes[virtualKeyCode] = scanCode;
                }
            }

            // Add the special keys that do not get added from the code above
            for (int ke = FSLibrary.Win32APIEnums.VK_NUMPAD0; ke <= FSLibrary.Win32APIEnums.VK_NUMPAD9; ke++)
            {
                this._virtualKeyScanCodes[ke] = FSLibrary.Win32API.MapVirtualKeyEx(
                    ke,
                    (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC,
                    keybHandle);
            }

            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_DECIMAL] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_DECIMAL, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_DIVIDE] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_DIVIDE, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_TAB] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_TAB, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_CANCEL] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_CANCEL, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_SNAPSHOT] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_SNAPSHOT, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_APPS] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_APPS, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_OEM_102] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_OEM_102, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_LSHIFT] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_LSHIFT, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_RSHIFT] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_RSHIFT, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_LCONTROL] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_LCONTROL, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_RCONTROL] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_RCONTROL, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_LMENU] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_LMENU, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_RMENU] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_RMENU, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_LWIN] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_LWIN, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_RWIN] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_RWIN, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_PAUSE] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_PAUSE, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_VOLUME_UP] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_VOLUME_UP, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_VOLUME_DOWN] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_VOLUME_DOWN, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_VOLUME_MUTE] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_VOLUME_MUTE, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_MEDIA_NEXT_TRACK] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_MEDIA_NEXT_TRACK, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_MEDIA_PREV_TRACK] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_MEDIA_PREV_TRACK, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_MEDIA_PLAY_PAUSE] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_MEDIA_PLAY_PAUSE, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibrary.Win32APIEnums.VK_MEDIA_STOP] =
                FSLibrary.Win32API.MapVirtualKeyEx(
                    FSLibrary.Win32APIEnums.VK_MEDIA_STOP, (int)FSLibrary.Win32APIEnums.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
        }
    }
}
