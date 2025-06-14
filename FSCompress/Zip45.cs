using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FSCompress
{
    public static class Zip45
    {
        public static int BytesRead = 0;
        public static int BytesWrite = 0;
        public static double TotalBytes = 0;

        public static event EventHandler<int> OnBytesRead;
        public static event EventHandler<int> OnBytesWrite;
        public static event EventHandler<string> OnProgress;

        public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, bool overWrite, bool copyHidden)
        {
            string file = destinationArchiveFileName;
            bool copy = true;

            sourceDirectoryName = Path.GetFullPath(sourceDirectoryName);

            FileInfo[] sourceFiles =
                new DirectoryInfo(sourceDirectoryName).GetFiles("*", SearchOption.AllDirectories);

            TotalBytes = sourceFiles.Sum(f => f.Length);

            if(!overWrite)
            {
                int f = 1;
                string extension = Path.GetExtension(destinationArchiveFileName);
                while (File.Exists(file))
                {
                    file = destinationArchiveFileName.Replace(extension, " (" + f + ")" + extension);
                    f++;
                }
            }

            using (ZipArchive archive = ZipFile.Open(file, ZipArchiveMode.Create))
            {
                foreach (FileInfo fileInfo in sourceFiles)
                {
                    // NOTE: naive method to get sub-path from file name, relative to
                    // input directory. Production code should be more robust than this.
                    // Either use Path class or similar to parse directory separators and
                    // reconstruct output file name, or change this entire method to be
                    // recursive so that it can follow the sub-directories and include them
                    // in the entry name as they are processed.
                    string entryName = fileInfo.FullName.Substring(sourceDirectoryName.Length + 1);

                    copy = true;
                    if (fileInfo.DirectoryName.Contains("\\."))
                    {
                        if (!copyHidden)
                        {
                            copy = false;
                        }
                    }

                    if (copy)
                    {
                        if (OnProgress != null)
                            OnProgress(null, entryName);

                        ZipArchiveEntry entry = archive.CreateEntry(entryName);

                        entry.LastWriteTime = fileInfo.LastWriteTime;

                        using (Stream inputStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (Stream outputStream = entry.Open())
                        {
                            StreamWithProgress progressStream = new StreamWithProgress(inputStream);
                            progressStream.OnBytesRead += ProgressStream_OnBytesRead;

                            progressStream.CopyTo(outputStream);
                        }
                    }
                }
            }
        }

        private static void ProgressStream_OnBytesRead(object sender, int e)
        {
            BytesRead = e;

            if (OnBytesRead != null)
                OnBytesRead(null, BytesRead);
        }

        public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            using (ZipArchive archive = ZipFile.OpenRead(sourceArchiveFileName))
            {
                TotalBytes = archive.Entries.Sum(e => e.Length);

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string fileName = Path.Combine(destinationDirectoryName, entry.FullName);

                    if (OnProgress != null)
                        OnProgress(null, fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                    using (Stream inputStream = entry.Open())
                    using (Stream outputStream = File.OpenWrite(fileName))
                    {
                        StreamWithProgress progressStream = new StreamWithProgress(outputStream);
                        progressStream.OnBytesWrite += ProgressStream_OnBytesWrite;

                        inputStream.CopyTo(progressStream);
                    }

                    File.SetLastWriteTime(fileName, entry.LastWriteTime.LocalDateTime);
                }
            }
        }

        private static void ProgressStream_OnBytesWrite(object sender, int e)
        {
            BytesWrite = e;

            if (OnBytesWrite != null)
                OnBytesWrite(null, BytesWrite);
        }
    }
}