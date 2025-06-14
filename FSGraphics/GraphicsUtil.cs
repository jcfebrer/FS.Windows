using FSException;
using FSLibrary;
using FSTrace;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FSGraphics
{
    /// <summary>
    ///     Description of Graphics.
    /// </summary>
    public class GraphicsUtil
    {
        public static int OrigX = 0;
        public static int OrigY = 0;

        //static int resX = 0;
        //static int resY = 0;

        //static int maxSizeH = 0;
        //static int maxSizeW = 0;

        //public static void SetResolution(int width, int height)
        //{
        //    resX = width;
        //    resY = height;
        //}

        //public static void SetMaxSize(int width, int height)
        //{
        //    maxSizeW = width;
        //    maxSizeH = height;
        //}
        public static void DrawRoundedRectangle(Graphics objGraphics, Brush br, Rectangle rect, int diameter)
        {
            DrawSolidRoundedRectangle(objGraphics, br, rect, diameter);
        }


        public static void DrawRoundedRectangle(Graphics objGraphics, Pen pen, Rectangle rect, int diameter)
        {
            DrawRoundedRectangle(objGraphics, pen, rect.X + OrigX, rect.Y + OrigY, rect.Width + OrigX,
                rect.Height + OrigY, diameter);
        }


        public static void DrawRoundedRectangle(Graphics objGraphics, Brush br, int m_intxAxis, int m_intyAxis,
            int m_intWidth,
            int m_intHeight, int m_diameter)
        {
            m_intxAxis += OrigX;
            m_intyAxis += OrigY;
            m_intHeight += OrigY;
            m_intWidth += OrigX;

            DrawSolidRoundedRectangle(objGraphics, m_intxAxis, m_intyAxis, m_intWidth, m_intHeight, m_diameter, br);
        }


        public static void DrawRoundedRectangle(Graphics objGraphics, Pen pen, int m_intxAxis, int m_intyAxis,
            int m_intWidth,
            int m_intHeight, int m_diameter)
        {
            m_intxAxis += OrigX;
            m_intyAxis += OrigY;
            m_intHeight += OrigY;
            m_intWidth += OrigX;

            var BaseRect = new RectangleF(m_intxAxis, m_intyAxis, m_intWidth, m_intHeight);
            var ArcRect = new RectangleF(BaseRect.Location, new SizeF(m_diameter, m_diameter));
            objGraphics.DrawArc(pen, ArcRect, 180, 90);
            objGraphics.DrawLine(pen, m_intxAxis + Convert.ToInt32(m_diameter / 2), m_intyAxis,
                m_intxAxis + m_intWidth - Convert.ToInt32(m_diameter / 2), m_intyAxis);

            ArcRect.X = BaseRect.Right - m_diameter;
            objGraphics.DrawArc(pen, ArcRect, 270, 90);
            objGraphics.DrawLine(pen, m_intxAxis + m_intWidth, m_intyAxis + Convert.ToInt32(m_diameter / 2),
                m_intxAxis + m_intWidth, m_intyAxis + m_intHeight - Convert.ToInt32(m_diameter / 2));

            ArcRect.Y = BaseRect.Bottom - m_diameter;
            objGraphics.DrawArc(pen, ArcRect, 0, 90);
            objGraphics.DrawLine(pen, m_intxAxis + Convert.ToInt32(m_diameter / 2), m_intyAxis + m_intHeight,
                m_intxAxis + m_intWidth - Convert.ToInt32(m_diameter / 2), m_intyAxis + m_intHeight);

            ArcRect.X = BaseRect.Left;
            objGraphics.DrawArc(pen, ArcRect, 90, 90);
            objGraphics.DrawLine(pen, m_intxAxis, m_intyAxis + Convert.ToInt32(m_diameter / 2), m_intxAxis,
                m_intyAxis + m_intHeight - Convert.ToInt32(m_diameter / 2));
        }

        public static void DrawGradient(Form frmForm, Color color1, Color color2, LinearGradientMode mode)
        {
            var a = new LinearGradientBrush(new RectangleF(0, 0, frmForm.Width, frmForm.Height), color1,
                color2, mode);
            var g = frmForm.CreateGraphics();
            g.FillRectangle(a, new RectangleF(0, 0, frmForm.Width, frmForm.Height));
            g.Dispose();
        }


        public static void DrawGradientString(Form frmForm, string text, Color color1, Color color2,
            LinearGradientMode mode)
        {
            var a = new LinearGradientBrush(new RectangleF(0, 0, 100, 19), color1, color2, mode);
            var g = frmForm.CreateGraphics();
            Font f = null;
            f = new Font("arial", 20, FontStyle.Bold, GraphicsUnit.Pixel);
            g.DrawString(text, f, a, 0, 0);
            g.Dispose();
        }

        public static void DrawSolidRoundedRectangle(Graphics objGraphics, Brush br, Rectangle rect, int diameter)
        {
            DrawSolidRoundedRectangle(objGraphics, rect.X + OrigX, rect.Y + OrigY, rect.Width + OrigX,
                rect.Height + OrigY, diameter, br);
        }


        public static HorizontalAlignment ConvertContentAlignToHorizontalAlign(ContentAlignment ca)
        {
            HorizontalAlignment ha = 0;
            switch (ca)
            {
                case ContentAlignment.BottomLeft:
                    ha = HorizontalAlignment.Left;
                    break;
                case ContentAlignment.TopLeft:
                    ha = HorizontalAlignment.Left;
                    break;
                case ContentAlignment.MiddleLeft:
                    ha = HorizontalAlignment.Left;
                    break;
                case ContentAlignment.BottomCenter:
                    ha = HorizontalAlignment.Center;
                    break;
                case ContentAlignment.TopCenter:
                    ha = HorizontalAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    ha = HorizontalAlignment.Center;
                    break;
                case ContentAlignment.BottomRight:
                    ha = HorizontalAlignment.Right;
                    break;
                case ContentAlignment.TopRight:
                    ha = HorizontalAlignment.Right;
                    break;
                case ContentAlignment.MiddleRight:
                    ha = HorizontalAlignment.Right;
                    break;
            }

            return ha;
        }

        public static ContentAlignment ConvertHorizontalAlignToContentAlign(HorizontalAlignment ha)
        {
            ContentAlignment ca = 0;
            switch (ha)
            {
                case HorizontalAlignment.Center:
                    ca = ContentAlignment.TopCenter;
                    break;
                case HorizontalAlignment.Left:
                    ca = ContentAlignment.TopLeft;
                    break;
                case HorizontalAlignment.Right:
                    ca = ContentAlignment.TopRight;
                    break;
            }

            return ca;
        }


        public static void DrawSolidRoundedRectangle(Graphics objGraphics, int m_intxAxis, int m_intyAxis,
            int m_intWidth,
            int m_intHeight, int m_diameter, Brush m_Brush)
        {
            m_intxAxis += OrigX;
            m_intyAxis += OrigY;
            m_intHeight += OrigY;
            m_intWidth += OrigX;

            var path = new GraphicsPath();
            var BaseRect = new RectangleF(m_intxAxis, m_intyAxis, m_intWidth, m_intHeight);
            var ArcRect = new RectangleF(BaseRect.Location, new SizeF(m_diameter, m_diameter));
            path.AddArc(ArcRect, 180, 90);
            path.AddLine(m_intxAxis + Convert.ToInt32(m_diameter / 2), m_intyAxis,
                m_intxAxis + m_intWidth - Convert.ToInt32(m_diameter / 2), m_intyAxis);
            ArcRect.X = BaseRect.Right - m_diameter;
            path.AddArc(ArcRect, 270, 90);
            path.AddLine(m_intxAxis + m_intWidth, m_intyAxis + Convert.ToInt32(m_diameter / 2), m_intxAxis + m_intWidth,
                m_intyAxis + m_intHeight - Convert.ToInt32(m_diameter / 2));
            ArcRect.Y = BaseRect.Bottom - m_diameter;
            path.AddArc(ArcRect, 0, 90);
            path.AddLine(m_intxAxis + Convert.ToInt32(m_diameter / 2), m_intyAxis + m_intHeight,
                m_intxAxis + m_intWidth - Convert.ToInt32(m_diameter / 2), m_intyAxis + m_intHeight);
            ArcRect.X = BaseRect.Left;
            path.AddArc(ArcRect, 90, 90);
            path.AddLine(m_intxAxis, m_intyAxis + Convert.ToInt32(m_diameter / 2), m_intxAxis,
                m_intyAxis + m_intHeight - Convert.ToInt32(m_diameter / 2));

            objGraphics.FillPath(m_Brush, path);
        }


        public static Color Color(int valor)
        {
            var c = new Color[16];
            c[0] = System.Drawing.Color.Red;
            c[1] = System.Drawing.Color.Green;
            c[2] = System.Drawing.Color.Chocolate;
            c[3] = System.Drawing.Color.Blue;
            c[4] = System.Drawing.Color.Yellow;
            c[5] = System.Drawing.Color.Olive;
            c[6] = System.Drawing.Color.LightBlue;
            c[7] = System.Drawing.Color.LightGreen;
            c[8] = System.Drawing.Color.LightYellow;
            c[9] = System.Drawing.Color.Coral;
            c[10] = System.Drawing.Color.DarkBlue;
            c[11] = System.Drawing.Color.Gray;
            c[12] = System.Drawing.Color.Lime;
            c[13] = System.Drawing.Color.Wheat;
            c[14] = System.Drawing.Color.Fuchsia;
            c[15] = System.Drawing.Color.DarkRed;

            return c[valor % 15];
        }


        public static Color GetDarkColor(Color c, byte d)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if (c.R > d) r = Convert.ToByte(c.R - d);
            if (c.G > d) g = Convert.ToByte(c.G - d);
            if (c.B > d) b = Convert.ToByte(c.B - d);

            var c1 = System.Drawing.Color.FromArgb(r, g, b);
            return c1;
        }

        public static Color GetLightColor(Color c, byte d)
        {
            byte r = 255;
            byte g = 255;
            byte b = 255;

            if (Convert.ToInt32(c.R) + Convert.ToInt32(d) <= 255) r = Convert.ToByte(c.R + d);
            if (Convert.ToInt32(c.G) + Convert.ToInt32(d) <= 255) g = Convert.ToByte(c.G + d);
            if (Convert.ToInt32(c.B) + Convert.ToInt32(d) <= 255) b = Convert.ToByte(c.B + d);

            var c2 = System.Drawing.Color.FromArgb(r, g, b);
            return c2;
        }

        public static void DrawPoint(Graphics objGraphics, int x, int y, Color color)
        {
            ////cambiamos la resolucion
            //x = (maxSizeW * x) / resX;
            //y = (maxSizeH * y) / resY;

            objGraphics.FillRectangle(new SolidBrush(color), x + OrigX, y + OrigY, 1, 1);
        }

        public static void DrawLine(Graphics objGraphics, int x1, int y1, int x2, int y2, Color color)
        {
            ////cambiamos la resolucion
            //x1 = (maxSizeW * x1) / resX;
            //y1 = (maxSizeH * y1) / resY;

            //x2 = (maxSizeW * x2) / resX;
            //y2 = (maxSizeH * y2) / resY;

            objGraphics.DrawLine(new Pen(color), x1 + OrigX, y1 + OrigY, x2 + OrigX, y2 + OrigY);
        }

        public static void DrawCircle(Graphics objGraphics, Color color,
            PointF center, float radius)
        {
            objGraphics.FillEllipse(new SolidBrush(color), center.X + OrigX - radius, center.Y + OrigY - radius,
                radius + radius + OrigX, radius + radius + OrigY);
        }

        public static void DrawCircle(Graphics objGraphics, Color color,
            int x, int y, float radius)
        {
            objGraphics.FillEllipse(new SolidBrush(color), x + OrigX - radius, y + OrigY - radius,
                radius + radius + OrigX, radius + radius + OrigY);
        }

        public static void DrawBinaryBitmap(Graphics objGraphics, int x, int y, byte[] pixels, int pos, int width,
            int height, Color c)
        {
            int x1 = 0, y1 = 0;
            int w1 = 0, h1 = 0;

            w1 = width; //cada byte son 8 bits
            h1 = height;

            ////cambiamos la resolucion
            //w1 = (maxSizeW * w1) / resX;
            //h1 = (maxSizeH * h1) / resY;
            var bmp = new DirectBitmap(w1, h1);

            //int line = 0;

            for (var i = pos; i < pixels.Length; i++)
            for (var j = 0; j < 8; j++) //recorremos cada bit del byte y si ....
                if ((MathUtil.RotateLeft(pixels[i], j) & 128) == 128) //si el valor es un 1, pintamos
                {
                    x1 = (i - pos) % (w1 / 8) * 8 + j; //((i % (w1 / 8)) * 8) + (7 - j);
                    y1 = (i - pos) / (w1 / 8); //line;

                    ////cambiamos la resolucion
                    //x1 = (maxSizeW * x1) / resX;
                    //y1 = (maxSizeH * y1) / resY;

                    if (x1 < w1 && y1 < h1)
                        bmp.SetPixel(x1, y1, c);
                }

            //if (((i - pos) % (w1 / 8)) == 1) line++;

            ////cambiamos la resolucion
            //x = (maxSizeW * x) / resX;
            //y = (maxSizeH * y) / resY;


            //SolidBrush brush = new SolidBrush(c);
            //using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            //{
            //    FillCircle(g, brush, new Point(x, y), 5);
            //}
            objGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            objGraphics.DrawImage(bmp.Bitmap, x + OrigX, y + OrigY);
        }


        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;
            return null;
        }

        public static byte[] CreateJpegBytes(Bitmap image, long quality)
        {
            using (var ms = new MemoryStream())
            {
                var encoder_params = new EncoderParameters(1);
                encoder_params.Param[0] = new EncoderParameter(
                    Encoder.Quality, quality);

                var jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                ms.SetLength(0);
                image.Save(ms, jgpEncoder, encoder_params);
                return ms.ToArray();
            }
        }

        public static unsafe Bitmap GetDifferenceImage(Bitmap image1, Bitmap image2, byte alpha)
        {
            if (image1.Width != image2.Width || image1.Height != image2.Height)
                throw new ExceptionUtil("Deben tener los mismos tamaños.");

            BitmapData bmData1 = null;
            BitmapData bmData2 = null;
            BitmapData bmDataRes = null;

            var bmpRes = new Bitmap(image1.Width, image1.Height, image1.PixelFormat);

            try
            {
                bmData1 = image1.LockBits(new Rectangle(0, 0, image1.Width, image1.Height), ImageLockMode.ReadOnly,
                    image1.PixelFormat);
                bmData2 = image2.LockBits(new Rectangle(0, 0, image2.Width, image2.Height), ImageLockMode.ReadOnly,
                    image2.PixelFormat);
                bmDataRes = bmpRes.LockBits(new Rectangle(0, 0, bmpRes.Width, bmpRes.Height), ImageLockMode.ReadWrite,
                    image1.PixelFormat);

                var scan01 = bmData1.Scan0;
                var scan02 = bmData2.Scan0;
                var scan0Res = bmDataRes.Scan0;

                var stride1 = bmData1.Stride;
                var stride2 = bmData2.Stride;
                var strideRes = bmDataRes.Stride;

                var nWidth = image1.Width;
                var nHeight = image1.Height;

                //System.Threading.Tasks.Parallel.For(0, nHeight, y =>
                for (var y = 0; y < nHeight; y++)
                {
                    //define the pointers inside the first loop for parallelizing
                    var p1 = (byte*) scan01.ToPointer();
                    p1 += y * stride1;
                    var p2 = (byte*) scan02.ToPointer();
                    p2 += y * stride2;
                    var pRes = (byte*) scan0Res.ToPointer();
                    pRes += y * strideRes;

                    for (var x = 0; x < nWidth; x++)
                    {
                        //always get the complete pixel when differences are found
                        if (p1[0] != p2[0] || p1[1] != p2[1] || p1[2] != p2[2])
                        {
                            pRes[0] = p2[0];
                            pRes[1] = p2[1];
                            pRes[2] = p2[2];

                            ////alpha (opacity)
                            //if (alpha != 0)
                            //    pRes[3] = alpha;
                            //else
                            //    pRes[3] = p2[3];

                            pRes[3] = 255; //indicampos que este pixel ha variado
                        }

                        p1 += 4;
                        p2 += 4;
                        pRes += 4;
                    }
                }
                //);

                image1.UnlockBits(bmData1);
                image2.UnlockBits(bmData2);
                bmpRes.UnlockBits(bmDataRes);
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
            }

            return bmpRes;
        }


        public static unsafe Bitmap GetDifferenceImage(Bitmap image1, Bitmap image2, Color matchColor)
        {
            if ((image1 == null) | (image2 == null))
                throw new ExceptionUtil("No tiene que ser null la imagen.");

            if (image1.Height != image2.Height || image1.Width != image2.Width)
                throw new ExceptionUtil("Deben tener los mismos tamaños.");

            var diffImage = image2.Clone() as Bitmap;

            var height = image1.Height;
            var width = image1.Width;

            var data1 = image1.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, image1.PixelFormat);
            var data2 = image2.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, image2.PixelFormat);
            var diffData = diffImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, image1.PixelFormat);

            var data1Ptr = (byte*) data1.Scan0;
            var data2Ptr = (byte*) data2.Scan0;
            var diffPtr = (byte*) diffData.Scan0;

            var swapColor = new byte[3];
            swapColor[0] = matchColor.B;
            swapColor[1] = matchColor.G;
            swapColor[2] = matchColor.R;

            var rowPadding = data1.Stride - image1.Width * 3;

            // iterate over height (rows)
            for (var i = 0; i < height; i++)
            {
                // iterate over width (columns)
                for (var j = 0; j < width; j++)
                {
                    var same = 0;

                    var tmp = new byte[3];

                    // compare pixels and copy new values into temporary array
                    for (var x = 0; x < 3; x++)
                    {
                        tmp[x] = data2Ptr[0];
                        if (data1Ptr[0] == data2Ptr[0]) same++;
                        data1Ptr++; // advance image1 ptr
                        data2Ptr++; // advance image2 ptr
                    }

                    // swap color or add new values
                    for (var x = 0; x < 3; x++)
                    {
                        diffPtr[0] = same == 3 ? swapColor[x] : tmp[x];
                        diffPtr++; // advance diff image ptr
                    }
                }

                // at the end of each column, skip extra padding
                if (rowPadding > 0)
                {
                    data1Ptr += rowPadding;
                    data2Ptr += rowPadding;
                    diffPtr += rowPadding;
                }
            }

            image1.UnlockBits(data1);
            image2.UnlockBits(data2);
            diffImage.UnlockBits(diffData);

            return diffImage;
        }


        public static unsafe Bitmap Combine2Image(Bitmap image1, Bitmap image2, byte alpha)
        {
            if (image1.Width != image2.Width || image1.Height != image2.Height)
                throw new ExceptionUtil("Deben tener los mismos tamaños.");


            var bmpRes = new Bitmap(image1.Width, image1.Height, image1.PixelFormat);

            try
            {
                var bmData1 = image1.LockBits(new Rectangle(0, 0, image1.Width, image1.Height), ImageLockMode.ReadOnly,
                    image1.PixelFormat);
                var bmData2 = image2.LockBits(new Rectangle(0, 0, image2.Width, image2.Height), ImageLockMode.ReadOnly,
                    image2.PixelFormat);
                var bmDataRes = bmpRes.LockBits(new Rectangle(0, 0, bmpRes.Width, bmpRes.Height),
                    ImageLockMode.ReadWrite, image1.PixelFormat);

                var scan01 = bmData1.Scan0;
                var scan02 = bmData2.Scan0;
                var scan0Res = bmDataRes.Scan0;

                var stride1 = bmData1.Stride;
                var stride2 = bmData2.Stride;
                var strideRes = bmDataRes.Stride;

                var nWidth = image1.Width;
                var nHeight = image1.Height;

                //System.Threading.Tasks.Parallel.For(0, nHeight, y =>
                for (var y = 0; y < nHeight; y++)
                {
                    //define the pointers inside the first loop for parallelizing
                    var p1 = (byte*) scan01.ToPointer();
                    p1 += y * stride1;
                    var p2 = (byte*) scan02.ToPointer();
                    p2 += y * stride2;
                    var pRes = (byte*) scan0Res.ToPointer();
                    pRes += y * strideRes;

                    for (var x = 0; x < nWidth; x++)
                    {
                        if (p2[3] == 255
                        ) //(p1[0] != p2[0] || p1[1] != p2[1] || p1[2] != p2[2]) //(p2[3] == 255) // (p2[0] != 0 || p2[1] != 0 || p2[2] != 0)
                        {
                            pRes[0] = p2[0];
                            pRes[1] = p2[1];
                            pRes[2] = p2[2];

                            //alpha (opacity)
                            pRes[3] = p2[3];
                        }
                        else
                        {
                            pRes[0] = p1[0];
                            pRes[1] = p1[1];
                            pRes[2] = p1[2];

                            //alpha (opacity)
                            pRes[3] = p1[3];
                        }

                        p1 += 4;
                        p2 += 4;
                        pRes += 4;
                    }
                }
                //);

                image1.UnlockBits(bmData1);
                image2.UnlockBits(bmData2);
                bmpRes.UnlockBits(bmDataRes);
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
            }

            return bmpRes;
        }

        ///// <summary>
        ///// Demasiado lento al utilizar las funciones GetPixel / SetPixel
        ///// </summary>
        ///// <param name="image1"></param>
        ///// <param name="image2"></param>
        ///// <param name="diffColor"></param>
        ///// <returns></returns>
        //public static Bitmap GetDifferenceBitmap(Bitmap image1, Bitmap image2, Color diffColor)
        //{
        //    Size s1 = image1.Size;
        //    Size s2 = image2.Size;
        //    if (s1 != s2) return null;

        //    Bitmap bmp3 = new Bitmap(s1.Width, s1.Height);

        //    for (int y = 0; y < s1.Height; y++)
        //        for (int x = 0; x < s1.Width; x++)
        //        {
        //            Color c1 = image1.GetPixel(x, y);
        //            Color c2 = image2.GetPixel(x, y);

        //            if (c1 == c2)
        //                bmp3.SetPixel(x, y, c1);
        //            else
        //                bmp3.SetPixel(x, y, diffColor);
        //        }

        //    return bmp3;
        //}

        public static Image CombineImage(Image image1, Image image2)
        {
            using (var grfx = Graphics.FromImage(image1))
            {
                grfx.DrawImage(image2, Point.Empty);
                return image1;
            }
        }

        public static byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[]) converter.ConvertTo(img, typeof(byte[]));
        }

        public static byte[] BitmapToByte(Bitmap bmp, ImageFormat imageFormat)
        {
            using (var stream = new MemoryStream())
            {
                stream.SetLength(0);
                bmp.Save(stream, imageFormat);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Convierte el array de Bytes a bitmap.
        /// </summary>
        /// <param name="bytes">El array de bytes.</param>
        /// <returns>El objeto bitmap</returns>
        public static Bitmap ByteToBitmap(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var img = Image.FromStream(stream);
                return new Bitmap(img);
            }
        }

        public static Bitmap GetBitmapFromBytes(byte[] pixelValues, int width, int height)
        {
            //Create an image that will hold the image data
            var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            //Get a reference to the images pixel data
            var dimension = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var picData = bmp.LockBits(dimension, ImageLockMode.ReadWrite, bmp.PixelFormat);
            var pixelStartAddress = picData.Scan0;

            //Copy the pixel data into the bitmap structure
            Marshal.Copy(pixelValues, 0, pixelStartAddress, pixelValues.Length);

            bmp.UnlockBits(picData);
            return bmp;
        }

        public static byte[] GetBytesFromBitmap(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException("bmp");
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
                throw new InvalidOperationException("Image format not supported.");

            BitmapData data;
            byte[] raw;

            //Lock the bitmap so we can access the raw pixel data
            data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, bmp.PixelFormat);

            //Accessing pointers must be in an unsafe context
            unsafe
            {
                int r,
                    c,
                    w = bmp.Width,
                    h = bmp.Height,
                    bmpStride = data.Stride,
                    rawStride = 3 * w;
                var bmpPtr = (byte*) data.Scan0.ToPointer();

                //Allocate the raw byte buffer
                raw = new byte[3 * w * h];
                fixed (byte* rawPtr = raw)
                {
                    //Scan through the image and copy each pixel
                    for (r = 0; r < h; ++r)
                    for (c = 0; c < rawStride; c += 3)
                    {
                        rawPtr[r * rawStride + c] = bmpPtr[r * bmpStride + c];
                        rawPtr[r * rawStride + c + 1] = bmpPtr[r * bmpStride + c + 2];
                        rawPtr[r * rawStride + c + 2] = bmpPtr[r * bmpStride + c + 2];
                    }
                }
            }

            bmp.UnlockBits(data);

            return raw;
        }

        public static Bitmap ConvertJpgToPng(Bitmap jpgBitmap)
        {
            var pngBitmap = new Bitmap(jpgBitmap);
            pngBitmap.MakeTransparent(System.Drawing.Color.FromArgb(255, 0, 0, 0));

            return pngBitmap;

            //using (MemoryStream stream = new MemoryStream())
            //{
            //    pngBitmap.Save(stream, ImageFormat.Png);

            //    return (Bitmap)Image.FromStream(stream);
            //}
        }


        public static Bitmap ConvertTo1bpp(Image img)
        {
            var ms = new MemoryStream();
            img.Save(ms, ImageFormat.Gif);
            ms.Position = 0;
            return new Bitmap(ms);
        }

        public static byte[] GetThumbnail2(Bitmap image, int width, int height)
        {
            using (var thumbnail = (Bitmap) image.GetThumbnailImage(640, 480, () => false, IntPtr.Zero))
            {
                using (var ms = new MemoryStream())
                {
                    thumbnail.Save(ms, ImageFormat.Png);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }

        public static byte[] GetThumbnail(Bitmap image, int width, int height)
        {
            try
            {
                using (Image thumbnail = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                {
                    using (var g = Graphics.FromImage(thumbnail))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image, 0, 0, width, height);
                    }

                    using (var ms = new MemoryStream())
                    {
                        thumbnail.Save(ms, ImageFormat.Png);
                        ms.Position = 0;
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
                return null;
            }
        }

        public static bool Invert(Bitmap b)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            var bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                ImageLockMode.ReadWrite, b.PixelFormat);
            var stride = bmData.Stride;
            var Scan0 = bmData.Scan0;
            unsafe
            {
                var p = (byte*) (void*) Scan0;
                var nOffset = stride - b.Width * 3;
                var nWidth = b.Width * 3;
                for (var y = 0; y < b.Height; ++y)
                {
                    for (var x = 0; x < nWidth; ++x)
                    {
                        p[0] = (byte) (255 - p[0]);
                        ++p;
                    }

                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        /// <summary>
        /// Converts an integer value in twips to the corresponding integer value
        /// in pixels on the x-axis.
        /// </summary>
        /// <param name="source">The Graphics context to use</param>
        /// <param name="inTwips">The number of twips to be converted</param>
        /// <returns>The number of pixels in that many twips</returns>
        public static int ConvertTwipsToXPixels(Graphics source, int twips)
        {
            return (int)(((double)twips) * (1.0 / 1440.0) * source.DpiX);
        }

        /// <summary>
        /// Converts an integer value in twips to the corresponding integer value
        /// in pixels on the y-axis.
        /// </summary>
        /// <param name="source">The Graphics context to use</param>
        /// <param name="inTwips">The number of twips to be converted</param>
        /// <returns>The number of pixels in that many twips</returns>
        public static int ConvertTwipsToYPixels(Graphics source, int twips)
        {
            return (int)(((double)twips) * (1.0 / 1440.0) * source.DpiY);
        }
    }


    public class DirectBitmap : IDisposable
    {
        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb,
                BitsHandle.AddrOfPinnedObject());
        }

        public Bitmap Bitmap { get; }
        public int[] Bits { get; }
        public bool Disposed { get; private set; }
        public int Height { get; }
        public int Width { get; }

        protected GCHandle BitsHandle { get; }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }

        public void SetPixel(int x, int y, Color colour)
        {
            var index = x + y * Width;
            var col = colour.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            var index = x + y * Width;
            var col = Bits[index];
            var result = Color.FromArgb(col);

            return result;
        }
    }

    public class LockBitmap
    {
        private BitmapData bitmapData;
        private IntPtr Iptr = IntPtr.Zero;
        private readonly Bitmap source;

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        ///     Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                var PixelCount = Width * Height;

                // Create rectangle to lock
                var rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = Image.GetPixelFormatSize(source.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                    source.PixelFormat);

                // create byte array to copy pixel values
                var step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            var clr = Color.Empty;

            // Get color components count
            var cCount = Depth / 8;

            // Get start index of the specified pixel
            var i = (y * Width + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                var b = Pixels[i];
                var g = Pixels[i + 1];
                var r = Pixels[i + 2];
                var a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }

            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                var b = Pixels[i];
                var g = Pixels[i + 1];
                var r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }

            if (Depth == 8)
                // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                var c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }

            return clr;
        }

        /// <summary>
        ///     Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            var cCount = Depth / 8;

            // Get start index of the specified pixel
            var i = (y * Width + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }

            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }

            if (Depth == 8)
                // For 8 bpp set color value (Red, Green and Blue values are the same)
                Pixels[i] = color.B;
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);

            using (Graphics graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        /// <summary>
        /// Devuelve un icono con el texto en la mitad
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Icon ModifyIconWithText(string text, Icon icon)
        {
            Bitmap bitmap = new Bitmap(32, 32);
            System.Drawing.Font drawFont = new System.Drawing.Font("Calibri", 16, FontStyle.Bold);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            graphics.DrawIcon(icon, 0, 0);
            graphics.DrawString(text, drawFont, drawBrush, 1, 2);
            Icon createdIcon = Icon.FromHandle(bitmap.GetHicon());
            drawFont.Dispose();
            drawBrush.Dispose();
            graphics.Dispose();
            bitmap.Dispose();
            return createdIcon;
        }
    }
}