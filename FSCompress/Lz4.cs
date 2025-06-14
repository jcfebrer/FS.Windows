using FSException;
using FSLibrary;
using LZ4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FSCompress
{
    public class Lz4
    {
        public static void Compress(string fileName)
        {
            FileInfo fileToBeGZipped = new FileInfo(fileName);
            FileInfo lz4FileName = new FileInfo(string.Concat(fileToBeGZipped.FullName, ".lz4"));

            using (FileStream fileToBeZippedAsStream = fileToBeGZipped.OpenRead())
            {
                using (FileStream lz4TargetAsStream = lz4FileName.Create())
                {
                    using (LZ4Stream lz4Stream = new LZ4Stream(lz4TargetAsStream, LZ4StreamMode.Compress, LZ4StreamFlags.HighCompression, 4096))
                    {
                        try
                        {
                            StreamUtil.CopyTo(fileToBeZippedAsStream, lz4Stream);
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
            FileInfo lz4FileName = new FileInfo(fileName);
            using (FileStream fileToDecompressAsStream = lz4FileName.OpenRead())
            {
                using (FileStream decompressedStream = File.Create(decompressedFileName))
                {
                    using (LZ4Stream decompressionStream = new LZ4Stream(fileToDecompressAsStream, LZ4StreamMode.Decompress, LZ4StreamFlags.HighCompression, 4096))
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
            using (LZ4Stream lz4Stream = new LZ4Stream(compressedStream, LZ4StreamMode.Compress, LZ4StreamFlags.HighCompression, 4096))
            {
                lz4Stream.Write(data, 0, data.Length);
                lz4Stream.Close();
                return compressedStream.ToArray();
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (MemoryStream compressedStream = new MemoryStream(data))
            using (LZ4Stream lz4Stream = new LZ4Stream(compressedStream, LZ4StreamMode.Decompress, LZ4StreamFlags.HighCompression, 4096))
            using (MemoryStream resultStream = new MemoryStream())
            {
                StreamUtil.CopyTo(lz4Stream, resultStream);
                return resultStream.ToArray();
            }
        }
    }
}
