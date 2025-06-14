using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
//using System.Windows.Forms;

namespace FSGraphics
{
    public class ImageProcess
    {
        /// <summary>
        ///     this method creates a black and white copy of the image
        /// </summary>
        public void DetectEdges(Bitmap original, ref Bitmap modified, Color c, int noise)
        {
            original = ConvertIndexedImage(original);

            var g = Graphics.FromImage(original);
            for (var x = 0; x < original.Width; x++)
            for (var y = 0; y < original.Height; y++)
                if (x > 2 && y > 2)
                    try
                    {
                        var current = modified.GetPixel(x, y);
                        var right = modified.GetPixel(x + 1, y);
                        var left = modified.GetPixel(x - 1, y);

                        var totalCurrent = current.R + current.G + current.B;
                        var totalRight = right.R + right.G + right.B;
                        var totalLeft = left.R + left.G + left.B;

                        if (totalCurrent > totalLeft + noise || totalCurrent > totalRight + noise)
                            modified.SetPixel(x - 1, y, Color.White); //white
                        else if (totalCurrent > totalRight + noise)
                            modified.SetPixel(x + 1, y, Color.White); //white
                        else
                            modified.SetPixel(x, y, Color.Black); //black

                        var upOne = modified.GetPixel(x, y - 1);
                        var downOne = modified.GetPixel(x, y + 1);

                        var totalUpOne = upOne.R + upOne.G + upOne.B;
                        var totalDownOne = downOne.R + downOne.G + downOne.B;

                        if (totalUpOne > totalCurrent + noise)
                            modified.SetPixel(x, y - 1, Color.White);
                        else if (totalDownOne > totalCurrent + noise)
                            modified.SetPixel(x, y + 1, Color.White);
                        else
                            modified.SetPixel(x, y, Color.Black);


                        var upLeft = modified.GetPixel(x - 1, y - 1);
                        var downRight = modified.GetPixel(x + 1, y + 1);

                        var totalUpLeft = upLeft.R + upLeft.G + upLeft.B;
                        var totalDownRight = downRight.R + downRight.G + downRight.B;
                        if (totalUpLeft > totalCurrent + noise)
                            modified.SetPixel(x - 1, y - 1, Color.White);
                        else if (totalDownRight > totalCurrent + noise)
                            modified.SetPixel(x + 1, y + 1, Color.White);
                        else
                            modified.SetPixel(x, y, Color.Black);

                        var upRight = modified.GetPixel(x + 1, y - 1);
                        var downLeft = modified.GetPixel(x - 1, y + 1);

                        var totalupRight = upRight.R + upRight.G + upRight.B;
                        var totalDownLeft = downLeft.R + downLeft.G + downLeft.B;
                        if (totalupRight > totalCurrent + noise)
                            modified.SetPixel(x + 1, y - 1, Color.White);
                        else if (totalDownRight > totalCurrent + noise)
                            modified.SetPixel(x - 1, y + 1, Color.White);
                        else
                            modified.SetPixel(x, y, Color.Black);
                    }
                    catch (ArgumentException)
                    {
                    }
        }

        /// <summary>
        ///     outlines the edges of an image
        /// </summary>
        public void OutLineEdges(Bitmap original, ref Bitmap modified, Color c, int noise)
        {
            original = ConvertIndexedImage(original);

            var g = Graphics.FromImage(original);
            for (var x = 0; x < original.Width; x++)
            for (var y = 0; y < original.Height; y++)
                if (x > 2 && y > 2)
                    try
                    {
                        var current = modified.GetPixel(x, y);
                        var right = modified.GetPixel(x + 1, y);
                        var left = modified.GetPixel(x - 1, y);

                        var totalCurrent = current.R + current.G + current.B;
                        var totalRight = right.R + right.G + right.B;
                        var totalLeft = left.R + left.G + left.B;

                        if (totalCurrent > totalLeft + noise || totalCurrent > totalRight + noise)
                            modified.SetPixel(x - 1, y, c);
                        else if (totalCurrent > totalRight + noise) modified.SetPixel(x + 1, y, c);

                        var upOne = modified.GetPixel(x, y - 1);
                        var downOne = modified.GetPixel(x, y + 1);

                        var totalUpOne = upOne.R + upOne.G + upOne.B;
                        var totalDownOne = downOne.R + downOne.G + downOne.B;

                        if (totalUpOne > totalCurrent + noise) //|| totalDownOne > (totalCurrent + 75))
                            modified.SetPixel(x, y - 1, c);
                        else if (totalDownOne > totalCurrent + noise) modified.SetPixel(x, y + 1, c);

                        var upLeft = modified.GetPixel(x - 1, y - 1);
                        var downRight = modified.GetPixel(x + 1, y + 1);

                        var totalUpLeft = upLeft.R + upLeft.G + upLeft.B;
                        var totalDownRight = downRight.R + downRight.G + downRight.B;
                        if (totalUpLeft > totalCurrent + noise)
                            modified.SetPixel(x - 1, y - 1, c);
                        else if (totalDownRight > totalCurrent + noise) modified.SetPixel(x + 1, y + 1, c);

                        var upRight = modified.GetPixel(x + 1, y - 1);
                        var downLeft = modified.GetPixel(x - 1, y + 1);

                        var totalupRight = upRight.R + upRight.G + upRight.B;
                        var totalDownLeft = downLeft.R + downLeft.G + downLeft.B;
                        if (totalupRight > totalCurrent + noise)
                            modified.SetPixel(x + 1, y - 1, c);
                        else if (totalDownRight > totalCurrent + noise) modified.SetPixel(x - 1, y + 1, c);
                    }
                    catch (ArgumentException)
                    {
                    }
        }


        /// <summary>
        ///     Devuelve el porcentaje de "carne" que hay en una imagen
        /// </summary>
        /// <param name="original"></param>
        /// <param name="modified"></param>
        public int DetectSkinPercent(Bitmap image)
        {
            image = ConvertIndexedImage(image);

            var g = Graphics.FromImage(image);
            var points = new ArrayList();

            long totalSize = image.Width * image.Height;
            long totalSkin = 0;

            for (var x = 0; x < image.Width; x++)
            for (var y = 0; y < image.Height; y++)
            {
                var c = image.GetPixel(x, y);


                /* convert RGB color space to IRgBy color space using this formula:
                    I= [L(R) + L(B) + L(G)] / 3
                    Rg = L(R) - L(G)
                    By = L(B) - [L(G) +L(R)] / 2
					
                    to calculate the hue:
                    hue = atan2(Rg,By) * (180 / 3.141592654f)
                    */
                var I = (Math.Log(c.R) + Math.Log(c.B) + Math.Log(c.G)) / 3;
                var Rg = Math.Log(c.R) - Math.Log(c.G);
                var By = Math.Log(c.B) - (Math.Log(c.G) + Math.Log(c.R)) / 2;
                var hue = Math.Atan2(Rg, By) * (180 / Math.PI);


                //si es "piel"
                if (I <= 5 && hue >= 4 && hue <= 255)
                    //r = 255;
                    //points.Add(new Point(x, y));
                    totalSkin++;
            }

            var totalPercent = (int) (totalSkin * 100 / totalSize);

            return totalPercent;
        }

        /// <summary>
        ///     Convierte una imagen "Indexed" a no indexada
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Bitmap ConvertIndexedImage(Bitmap image)
        {
            if (image.PixelFormat == PixelFormat.Format8bppIndexed || image.PixelFormat == PixelFormat.Format4bppIndexed
                                                                   || image.PixelFormat == PixelFormat.Indexed ||
                                                                   image.PixelFormat == PixelFormat.Format1bppIndexed)
            {
                var tmp = new Bitmap(image.Width, image.Height);
                var grPhoto = Graphics.FromImage(tmp);
                grPhoto.DrawImage(image, new Rectangle(0, 0, tmp.Width, tmp.Height), 0, 0, tmp.Width, tmp.Height,
                    GraphicsUnit.Pixel);
                grPhoto.Dispose();

                image = tmp;
            }

            return image;
        }


        /// <summary>
        ///     detects skin. loops through the pixels in the image, if the pixel is skin then it leaves that pixel alone,
        ///     or else it will color that pixel black.
        /// </summary>
        public void DetectSkin(Bitmap original, ref Bitmap modified)
        {
            original = ConvertIndexedImage(original);

            var g = Graphics.FromImage(original);
            var points = new ArrayList();
            for (var x = 0; x < original.Width; x++)
            for (var y = 0; y < original.Height; y++)
            {
                var c = modified.GetPixel(x, y);


                /* convert RGB color space to IRgBy color space using this formula:
                    I= [L(R) + L(B) + L(G)] / 3
                    Rg = L(R) - L(G)
                    By = L(B) - [L(G) +L(R)] / 2
					
                    to calculate the hue:
                    hue = atan2(Rg,By) * (180 / 3.141592654f)
                    */
                var I = (Math.Log(c.R) + Math.Log(c.B) + Math.Log(c.G)) / 3;
                var Rg = Math.Log(c.R) - Math.Log(c.G);
                var By = Math.Log(c.B) - (Math.Log(c.G) + Math.Log(c.R)) / 2;
                var hue = Math.Atan2(Rg, By) * (180 / Math.PI);


                if (I <= 5 && hue >= 4 && hue <= 255)
                    //r = 255;
                    points.Add(new Point(x, y));
                else
                    modified.SetPixel(x, y, Color.Black);
            }

            //SortPoints(ref points);
            //PlotLines(Graphics.FromImage(modified), (Point)points[0], (Point)points[points.Count - 1]);
        }

        private void PlotLines(Graphics g, Point p1, Point p2)
        {
            g.DrawLine(Pens.White, p1, p2);
        }

        private void SortPoints(ref ArrayList pts)
        {
            //int x = 4;
            for (var i = 1; i < pts.Count; i++)
            {
                var thisPoint = (Point) pts[i];
                var lastPoint = (Point) pts[i - 1];
                if (thisPoint.X < lastPoint.X && thisPoint.Y < lastPoint.Y)
                {
                    //thisPoint is closer to 0,0
                }
                else
                {
                    //lastPoint is closer to 0,0
                    //swap thisPoint and lastPoint
                    swap(ref pts, i - 1, i);
                }
            }

            //--x;
            //if(!(x == 0)) SortPoints(ref pts);
        }

        private void swap(ref ArrayList pts, int a, int b)
        {
            Point temp;
            var pA = (Point) pts[a];
            var pB = (Point) pts[b];

            temp = pA;
            pA = pB;
            pB = temp;
        }

        private int max(int r, int g, int b)
        {
            if (r > g && r > b)
                return r;
            if (g > r && g > b)
                return g;
            return b;
        }

        private static int min(int r, int g, int b)
        {
            if (r < g && r < b)
                return r;
            if (g < r && g < b)
                return g;
            return b;
        }

        public string MakeImageSrcData(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return "data:image/png;base64," +
                   Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
        }

        public Image ConvertBase64ToImage(string base64String)
        {
            if (base64String.StartsWith("data:"))
            {
                base64String = base64String.Replace("data:image/jpg;base64,", "");
                base64String = base64String.Replace("data:image/png;base64,", "");
                base64String = base64String.Replace("data:image/gif;base64,", "");
                base64String = base64String.Replace("data:image/ico;base64,", "");
                base64String = base64String.Replace("data:image/bmp;base64,", "");
                base64String = base64String.Replace("data:image/jpeg;base64,", "");
            }

            // Convert Base64 String to byte[]
            var imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0,
                imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);
            return image;
        }

        public string ConvertImageToBase64(Image img, ImageFormat imageFormat)
        {
            using (var objMemoryStream = new MemoryStream())
            {
                img.Save(objMemoryStream, imageFormat);

                var objImageBytes = objMemoryStream.ToArray();
                // Convert byte[] to Base64 String
                var base64String = Convert.ToBase64String(objImageBytes);
                return base64String;
            }
        }
    }
}