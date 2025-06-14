using FSLibrary;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using static FSLibrary.Win32APIEnums;

namespace FSGraphics
{
    public class IconUtil
    {
        public static Icon ExtractAssociatedIcon(string fileName)
        {
            Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
            if (icon == null)
            {
                short uicon;
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = Win32API.ExtractAssociatedIcon(IntPtr.Zero, strB, out uicon);
                icon = Icon.FromHandle(handle);
            }

            return icon;
        }

        public static Icon ExtractIcon(string fileName, int iconIndex = 0)
        {
            Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
            if (icon == null)
            {
                IntPtr handle = Win32API.ExtractIcon(IntPtr.Zero, fileName, iconIndex);
                icon = Icon.FromHandle(handle);
            }

            return icon;
    }

        /// <summary>
        /// Returns an icon representation of the specified file.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        /// <param name="size">The desired size of the icon.</param>
        /// <returns>An icon that represents the file.</returns>
        public static Icon GetIconForFile(string filename, ShellIconSize size)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            Win32API.SHGetFileInfo(filename, 0, ref shinfo, Marshal.SizeOf(shinfo), size);

            Icon icon = null;

            if (shinfo.hIcon.ToInt32() != 0)
            {
                // create the icon from the native handle and make a managed copy of it
                icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();

                // release the native handle
                Win32API.DestroyIcon(shinfo.hIcon);
            }

            return icon;
        }

        /// <summary>
        /// Returns the default icon representation for files with the specified extension.
        /// </summary>
        /// <param name="extension">File extension (including the leading period).</param>
        /// <param name="size">The desired size of the icon.</param>
        /// <returns>The default icon for files with the specified extension.</returns>
        public static Icon GetIconForExtension(string extension, ShellIconSize size)
        {
            // repeat the process used for files, but instruct the API not to access the file
            size |= (ShellIconSize)SHGFI_USEFILEATTRIBUTES;
            return GetIconForFile(extension, size);
        }
}
}