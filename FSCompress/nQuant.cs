using FSException;
using FSLibrary;
using FSTrace;
using nQuant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace FSCompress
{
    public class nQuant
    {
        public static byte[] CompressByte(Bitmap fileNameBitmap)
        {
            WuQuantizer quantizer = new WuQuantizer();

            try
            {
                using (Image quantized = quantizer.QuantizeImage(fileNameBitmap))
                {
                    return FSGraphics.GraphicsUtil.BitmapToByte((Bitmap)quantized, ImageFormat.Png);
                }
            }
            catch (QuantizationException q)
            {
                Log.TraceError(q);
                return null;
            }
        }
        public static Image Compress(Bitmap fileNameBitmap)
        {
            WuQuantizer quantizer = new WuQuantizer();

            try
            {
                using (Image quantized = quantizer.QuantizeImage(fileNameBitmap))
                {
                    return quantized;
                }
            }
            catch (QuantizationException q)
            {
                Log.TraceError(q);
                return null;
            }
        }
        public static Image Compress(string fileName)
        {
            WuQuantizer quantizer = new WuQuantizer();
            using (Bitmap bitmap = new Bitmap(fileName))
            {
                try
                {
                    using (Image quantized = quantizer.QuantizeImage(bitmap))
                    {
                        return quantized;
                    }
                }
                catch (QuantizationException q)
                {
                    Log.TraceError(q);
                    return null;
                }
            }
        }

        public static void Compress(string fileName, string destFileName)
        {
            WuQuantizer quantizer = new WuQuantizer();
            using (Bitmap bitmap = new Bitmap(fileName))
            {
                try
                {
                    using (Image quantized = quantizer.QuantizeImage(bitmap))
                    {
                        quantized.Save(destFileName, ImageFormat.Png);
                    }
                }
                catch (QuantizationException q)
                {
                    Log.TraceError(q);
                }
            }
        }
    }
}
