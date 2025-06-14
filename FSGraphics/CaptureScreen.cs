using FSException;
using FSLibrary;
using FSTrace;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FSGraphics
{
    /// <summary>
    ///     This class encapsulates the API functions necessary to get the
    ///     desktop image and form a bitmap from it.
    /// </summary>
    public sealed class CaptureScreen
    {
        /// <summary>
        ///     Captures the windows desktop and returns it as a Bitmap
        /// </summary>
        /// <returns></returns>
        public static Bitmap CaptureScreenDesktop()
        {
            try
            {
                //Get a pointer to the desktop window
                var desktopWindow = Win32API.GetDesktopWindow();

                //Get a device context from the desktop window
                var desktopDC = Win32API.GetDC(desktopWindow);

                var desktopImage = GetBitmap(desktopDC);

                Win32API.ReleaseDC(desktopDC);

                return desktopImage;
            }
            catch (Exception e)
            {
                Log.TraceError(e);
                return null;
            }
        }

        internal static Bitmap GetBitmap(IntPtr imagePtr)
        {
            //Get a GDI handle to the image
            var hwnd = Win32API.GetCurrentObject(imagePtr, 7);

            //This call takes as an argument the handle to a GDI image
            var desktopImage = Image.FromHbitmap(hwnd);

            return desktopImage;
        }

        internal static Bitmap GetForegroundWindowBitmap()
        {
            var handle = Win32API.GetForegroundWindow();

            //Get a device context from the desktop window
            var windowDC = Win32API.GetWindowDC(handle);

            //Get a GDI handle to the image
            var hwnd = Win32API.GetCurrentObject(windowDC, 0);

            //This call takes as an argument the handle to a GDI image
            var desktopImage = Image.FromHbitmap(hwnd);

            Win32API.ReleaseDC(windowDC);

            return desktopImage;
        }

        /// <summary>
        ///     Creates a screen capture in a bitmap format. The currently used method in EncodedRectangleFactory.
        /// </summary>
        /// <param name="rectangle">The rectangle from the screen that we should take a screenshot from.</param>
        /// <returns>A bitmap containing the image data of our screenshot. The return value is null only if a problem occured.</returns>
        public static Bitmap CreateScreenCapture(Rectangle rectangle, bool showCursor)
        {
            return CreateScreenCapture(rectangle, showCursor, PixelFormat.Format32bppArgb);
        }

        public static Bitmap CreateScreenCapture(Rectangle rectangle, bool showCursor, PixelFormat pixelFormat)
        {
            try
            {
                //LogUtil.TraceInfo("Screen capture method API32 start");
                //Bitmap b = CaptureScreenDesktop();
                //if (b != null) return b;

                Log.TraceInfo("Capura con pixelformat: " + pixelFormat);
                //Stopwatch t = Stopwatch.StartNew();
                var width = rectangle.Width;
                var height = rectangle.Height;
                var bitmap = new Bitmap(width, height, pixelFormat); //System.Drawing.Imaging.PixelFormat.Format48bppRgb
                var g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, new Size(width, height));

                if (showCursor)
                    Cursors.Default.Draw(g,
                        new Rectangle(new Point(Cursor.Position.X - 10, Cursor.Position.Y - 10), new Size(32, 32)));

                //t.Stop();
                //LogUtil.TraceInfo("Screen capture method 1 done in: " + t.ElapsedMilliseconds + "ms. Size: " + bitmap.Size.ToString());
                return bitmap;
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
                Thread.Sleep(200);
                try
                {
                    //LogUtil.TraceInfo("Screen capture method 2 start");
                    var width = rectangle.Width;
                    var height = rectangle.Height;
                    var bitmap = new Bitmap(width, height, pixelFormat);
                    var g = Graphics.FromImage(bitmap);
                    g.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, new Size(width, height));

                    if (showCursor)
                        Cursors.Default.Draw(g,
                            new Rectangle(new Point(Cursor.Position.X - 32, Cursor.Position.Y - 32), new Size(32, 32)));

                    //LogUtil.TraceInfo("Screen capture method 2 done");
                    return bitmap;
                }
                catch (Exception)
                {
                    Log.TraceError(ex);
                    return null;
                }
            }
        }

        /// <summary>
        ///     An alternate method of creating a screenshot.
        /// </summary>
        /// <param name="x">The X coordinate of the Rectangle of our screenshot</param>
        /// <param name="y">The Y coordinate of the Rectangle of our screenshot</param>
        /// <param name="w">The width of the Rectangle of our screenshot</param>
        /// <param name="h">The height of the Rectangle of our screenshot</param>
        /// <returns></returns>
        public static Bitmap CreateScreenCapture(int x, int y, int w, int h, bool showCursor)
        {
            var r = new Rectangle(x, y, w, h);
            return CreateScreenCapture(r, showCursor);
        }



        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns></returns>
        public Image CaptureWindow()
        {
            return CaptureWindow(Win32API.GetDesktopWindow());
        }
        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = Win32API.GetWindowDC(handle);
            // get the size
            Rectangle windowRect = new Rectangle();
            Win32API.GetWindowRect(handle, ref windowRect);
            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;
            // create a device context we can copy to
            IntPtr hdcDest = Win32API.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = Win32API.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = Win32API.SelectObject(hdcDest, hBitmap);
            // bitblt over
            Win32API.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Win32APIEnums.SRCCOPY);
            // restore selection
            Win32API.SelectObject(hdcDest, hOld);
            // clean up 
            Win32API.DeleteDC(hdcDest);
            Win32API.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            Win32API.DeleteObject(hBitmap);
            return img;
        }
        /// <summary>
        /// Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public Image CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);

            return img;
        }
        /// <summary>
        /// Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public Image CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureWindow();
            img.Save(filename, format);

            return img;
        }


        public Image CaptureScreenToFile(string filename)
        {
            using (Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot))
                {
                    try
                    {
                        gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

                        ImageCodecInfo codec = GetEncoderInfo("image/jpeg");

                        System.Drawing.Imaging.Encoder qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameter ratio = new EncoderParameter(qualityEncoder, 10L); //calidad muy baja para jpg

                        EncoderParameters codecParams = new EncoderParameters(1);
                        codecParams.Param[0] = ratio;

                        if (File.Exists(filename)) File.Delete(filename);

                        using (FileStream fs = new FileStream(filename, FileMode.Create))
                        {
                            bmpScreenshot.Save(fs, codec, codecParams);
                            fs.Close();
                        }

                        return (Image)bmpScreenshot;
                    }
                    catch (Exception e)
                    {
                        throw new ExceptionUtil("Error al realizar la captura", e);
                    }
                }
            }
        }


        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}