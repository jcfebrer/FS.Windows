using FSCompress.Zlib;
using FSException;
using FSLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FSCompress.Zlib
{
    public class ZlibNET
    {
        public static void Compress(string fileName)
        {
            FileInfo fileToBeZipped = new FileInfo(fileName);
            FileInfo zipFileName = new FileInfo(string.Concat(fileToBeZipped.FullName, ".zip"));

            using (FileStream fileToBeZippedAsStream = fileToBeZipped.OpenRead())
            {
                using (FileStream zipTargetAsStream = zipFileName.Create())
                {
                    using (ZOutputStream zipStream = new ZOutputStream(zipTargetAsStream))
                    {
                        try
                        {
                            StreamUtil.CopyTo(fileToBeZippedAsStream, zipStream);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        public static void Decompress(string fileName, string decompressedFileName)
        {
            FileInfo gzipFileName = new FileInfo(fileName);
            using (FileStream fileToDecompressAsStream = gzipFileName.OpenRead())
            {
                using (FileStream decompressedStream = File.Create(decompressedFileName))
                {
                    using (ZOutputStream decompressionStream = new ZOutputStream(fileToDecompressAsStream))
                    {
                        try
                        {
                            StreamUtil.CopyTo(decompressionStream, decompressedStream);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }


        public static byte[] Compress(byte[] data)
        {
            using (MemoryStream compressedStream = new MemoryStream())
            using (ZOutputStream zipStream = new ZOutputStream(compressedStream))
            {
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (MemoryStream compressedStream = new MemoryStream(data))
            using (ZOutputStream zipStream = new ZOutputStream(compressedStream))
            using (MemoryStream resultStream = new MemoryStream())
            {
                StreamUtil.CopyTo(zipStream, resultStream);
                return resultStream.ToArray();
            }
        }
    }
}
