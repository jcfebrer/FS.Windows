using FSException;
using FSLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace FSCompress
{
    public class Gz
    {
        public static event EventHandler<string> OnProgress;

        public static void CompressFile(string fileName)
        {
            FileInfo fileToBeGZipped = new FileInfo(fileName);
            FileInfo gzipFileName = new FileInfo(string.Concat(fileToBeGZipped.FullName, ".gz"));

            if (OnProgress != null)
                OnProgress(null, fileName);

            using (FileStream fileToBeZippedAsStream = fileToBeGZipped.OpenRead())
            {
                using (FileStream gzipTargetAsStream = gzipFileName.Create())
                {
                    using (GZipStream gzipStream = new GZipStream(gzipTargetAsStream, CompressionMode.Compress))
                    {
                        try
                        {
                            StreamUtil.CopyTo(fileToBeZippedAsStream, gzipStream);
                        }
                        catch (Exception ex)
                        {
                            throw new ExceptionUtil(ex);
                        }
                    }
                }
            }
        }

        public static void DecompressFile(string fileName, string decompressedFileName)
        {
            FileInfo gzipFileName = new FileInfo(fileName);

            if (OnProgress != null)
                OnProgress(null, fileName);

            using (FileStream fileToDecompressAsStream = gzipFileName.OpenRead())
            {
                using (FileStream decompressedStream = File.Create(decompressedFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(fileToDecompressAsStream, CompressionMode.Decompress))
                    {
                        try
                        {
                            StreamUtil.CopyTo(decompressionStream, decompressedStream);
                        }
                        catch (Exception ex)
                        {
                            throw new ExceptionUtil(ex);
                        }
                    }
                }
            }
        }


        public static byte[] Compress(byte[] data)
        {
            try
            {
                using (var compressedStream = new MemoryStream())
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    zipStream.Write(data, 0, data.Length);
                    zipStream.Close();
                    return compressedStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            try
            {
                using (var compressedStream = new MemoryStream(data))
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                using (var resultStream = new MemoryStream())
                {
                    StreamUtil.CopyTo(zipStream, resultStream);
                    return resultStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private static void CompressFile(string sDir, string sRelativePath, GZipStream zipStream)
        {
            //Compress file name
            char[] chars = sRelativePath.ToCharArray();
            zipStream.Write(BitConverter.GetBytes(chars.Length), 0, sizeof(int));
            foreach (char c in chars)
                zipStream.Write(BitConverter.GetBytes(c), 0, sizeof(char));

            //Compress file content
            try
            {
                //using (FileStream sourceFile = new FileStream(Path.Combine(sDir, sRelativePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //{
                //    byte[] src = new byte[4096];
                //    int count = sourceFile.Read(src, 0, 4096);
                //    while (count != 0)
                //    {
                //        zipStream.Write(src, 0, count);
                //        count = sourceFile.Read(src, 0, 4096);
                //    }
                //}

                //Compress file content
                //byte[] bytes = File.ReadAllBytes(Path.Combine(sDir, sRelativePath));
                byte[] bytes = FSLibrary.NumberUtils.ReadAllBytes(Path.Combine(sDir, sRelativePath));
                zipStream.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
                zipStream.Write(bytes, 0, bytes.Length);
            }
            catch(Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        private static bool DecompressFile(string folder, GZipStream zipStream)
        {
            try
            {
                //Decompress file name
                byte[] bytes = new byte[sizeof(int)];
                int Readed = zipStream.Read(bytes, 0, sizeof(int));
                if (Readed < sizeof(int))
                    return false;

                int iNameLen = BitConverter.ToInt32(bytes, 0);
                bytes = new byte[sizeof(char)];
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < iNameLen; i++)
                {
                    zipStream.Read(bytes, 0, sizeof(char));
                    char c = BitConverter.ToChar(bytes, 0);
                    sb.Append(c);
                }

                string fileName = sb.ToString();

                if (OnProgress != null)
                    OnProgress(null, fileName);

                //Decompress file content
                bytes = new byte[sizeof(int)];
                zipStream.Read(bytes, 0, sizeof(int));
                int iFileLen = BitConverter.ToInt32(bytes, 0);

                bytes = new byte[iFileLen];
                zipStream.Read(bytes, 0, bytes.Length);

                string sFilePath = Path.Combine(folder, fileName);
                string sFinalDir = Path.GetDirectoryName(sFilePath);
                if (!Directory.Exists(sFinalDir))
                    Directory.CreateDirectory(sFinalDir);

                using (FileStream outFile = new FileStream(sFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    outFile.Write(bytes, 0, iFileLen);

                return true;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        public static void CompressDirectory(string sourceFolder, string targetFile, bool overWrite, bool copyHidden)
        {
            try
            {
                string file = targetFile;
                sourceFolder = Path.GetFullPath(sourceFolder);
                string[] sFiles = Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories);
                int iDirLen = sourceFolder[sourceFolder.Length - 1] == Path.DirectorySeparatorChar ? sourceFolder.Length : sourceFolder.Length + 1;

                if (!overWrite)
                {
                    int f = 1;
                    string extension = Path.GetExtension(targetFile);
                    while (File.Exists(file))
                    {
                        file = targetFile.Replace(extension, " (" + f + ")" + extension);
                        f++;
                    }
                };

                using (FileStream outFile = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
                using (GZipStream str = new GZipStream(outFile, CompressionMode.Compress))
                    foreach (string sFilePath in sFiles)
                    {
                        string sRelativePath = sFilePath.Substring(iDirLen);
                        if (OnProgress != null)
                            OnProgress(null, sRelativePath);

                        if (sRelativePath.Contains("\\."))
                        {
                            if (copyHidden)
                                CompressFile(sourceFolder, sRelativePath, str);
                        }
                        else
                            CompressFile(sourceFolder, sRelativePath, str);
                    }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        public static void DecompressToDirectory(string fileName, string targetFolder)
        {
            try
            {
                using (FileStream inFile = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                using (GZipStream zipStream = new GZipStream(inFile, CompressionMode.Decompress, true))
                    while (DecompressFile(targetFolder, zipStream)) ;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }
    }
}
