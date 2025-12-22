#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using DateTime = System.DateTime;
using FSException;

#endregion

#region '"FTP client class"' 

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public partial class DBFtp : DBUserControl
    {
        public event UploadingEventHandler Uploading;

        public event DownloadingEventHandler Downloading;

        #region Delegates

        public delegate void DownloadingEventHandler(FtpWebRequest fileInfo, int size);

        public delegate void UploadingEventHandler(long uploadSize, long totalSize, ref bool cancel);

        #endregion

        #region '"CONSTRUCTORS"' 

        public DBFtp()
        {
            _hostname = "";
            InitializeComponent();
        }

        public DBFtp(string Hostname)
        {
            _hostname = Hostname;

            InitializeComponent();
        }

        public DBFtp(string Hostname, string Username, string Password, bool EnableSSL = false)
        {
            _hostname = Hostname;
            _username = Username;
            this.Password = Password;
            this.EnableSSL = EnableSSL;

            InitializeComponent();
        }

        #endregion

        #region '"Directory functions"' 

        public List<string> ListDirectory(string directory)
        {
            var ftp = GetRequest(GetDirectory(directory));
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;

            var str = GetStringResponse(ftp);
            str = str.Replace("\r\n", Global.Cr).TrimEnd(Convert.ToChar(13));
            var result = new List<string>();
            result.AddRange(str.Split(Convert.ToChar(13)));
            return result;
        }


        public List<string> ListDirectory()
        {
            return ListDirectory("");
        }


        public DBFtpDirectory ListDirectoryDetail(string directory)
        {
            var ftp = GetRequest(GetDirectory(directory));
            ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            var str = GetStringResponse(ftp);
            str = str.Replace("\r\n", Global.Cr).TrimEnd(Convert.ToChar(13));
            return new DBFtpDirectory(str, _lastDirectory);
        }


        public DBFtpDirectory ListDirectoryDetail()
        {
            return ListDirectoryDetail("");
        }

        #endregion

        #region '"Upload: File transfer TO ftp server"' 

        public bool Upload(string localFilename, string targetFilename)
        {
            if (!File.Exists(localFilename)) throw new ApplicationException("File " + localFilename + " not found");
            var fi = new FileInfo(localFilename);
            return Upload(fi, targetFilename);
        }


        public bool Upload(string localFilename)
        {
            return Upload(localFilename, "");
        }


        public bool Upload(FileInfo fi, string targetFilename)
        {
            var bErr = false;

            string target = null;
            if (targetFilename.Trim() == "")
                target = CurrentDirectory + fi.Name;
            else if (targetFilename.Contains("/"))
                target = AdjustDir(targetFilename);
            else
                target = CurrentDirectory + targetFilename;

            var URI = Hostname + target;
            var ftp = GetRequest(URI);

            ftp.UsePassive = Passive;

            ftp.KeepAlive = KeepAlive;

            ftp.Credentials = new NetworkCredential(Username, Password);

            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;

            ftp.ContentLength = fi.Length;

            const int BufferSize = 2048;
            var content = new byte[BufferSize - 1 + 1];
            var dataRead = 0;

            long fileUp = 0;
            var cancel = false;
            using (var fs = fi.OpenRead())
            {
                try
                {
                    using (var rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);

                            fileUp = fileUp + dataRead;

                            if (null != Uploading)
                                Uploading(fileUp, fi.Length, ref cancel);

                            if (cancel)
                            {
                                bErr = true;
                                break;
                            }
                        } while (!(dataRead < BufferSize));

                        rs.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new ExceptionUtil(ex);
                }
                finally
                {
                    fs.Close();
                }
            }


            ftp = null;
            return !bErr;
        }


        public bool Upload(FileInfo fi)
        {
            return Upload(fi, "");
        }

        #endregion

        #region '"Download: File transfer FROM ftp server"' 

        public bool Download(string sourceFilename, string localFilename, bool PermitOverwrite)
        {
            var fi = new FileInfo(localFilename);
            return Download(sourceFilename, fi, PermitOverwrite);
        }


        public bool Download(string sourceFilename, string localFilename)
        {
            return Download(sourceFilename, localFilename, false);
        }


        public bool Download(DBFtpFileInfo file, string localFilename, bool PermitOverwrite)
        {
            return Download(file.FullName, localFilename, PermitOverwrite);
        }


        public bool Download(DBFtpFileInfo file, string localFilename)
        {
            return Download(file, localFilename, false);
        }


        public bool Download(DBFtpFileInfo file, FileInfo localFI, bool PermitOverwrite)
        {
            return Download(file.FullName, localFI, PermitOverwrite);
        }


        public bool Download(DBFtpFileInfo file, FileInfo localFI)
        {
            return Download(file, localFI, false);
        }


        public bool Download(string sourceFilename, FileInfo targetFI, bool PermitOverwrite)
        {
            if (targetFI.Exists & !PermitOverwrite) throw new ApplicationException("Target file already exists");

            string target = null;
            if (sourceFilename.Trim() == "")
                throw new ApplicationException("File not specified");
            if (sourceFilename.Contains("/"))
                target = AdjustDir(sourceFilename);
            else
                target = CurrentDirectory + sourceFilename;

            var URI = Hostname + target;

            var ftp = GetRequest(URI);

            ftp.UsePassive = Passive;

            ftp.KeepAlive = KeepAlive;

            ftp.Method = WebRequestMethods.Ftp.DownloadFile;
            ftp.UseBinary = true;

            using (var response = (FtpWebResponse) ftp.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var fs = targetFI.OpenWrite())
                    {
                        try
                        {
                            var buffer = new byte[2048];
                            var read = 0;
                            do
                            {
                                read = responseStream.Read(buffer, 0, buffer.Length);
                                fs.Write(buffer, 0, read);

                                if (null != Downloading) Downloading(ftp, read);
                            } while (!(read == 0));

                            responseStream.Close();
                            fs.Flush();
                            fs.Close();
                        }
                        catch (Exception)
                        {
                            fs.Close();
                            targetFI.Delete();
                            throw;
                        }
                    }

                    responseStream.Close();
                }

                response.Close();
            }


            return true;
        }


        public bool Download(string sourceFilename, FileInfo targetFI)
        {
            return Download(sourceFilename, targetFI, false);
        }

        #endregion

        #region '"Other functions: Delete rename etc."' 

        public bool FtpDelete(string filename)
        {
            var URI = Hostname + GetFullPath(filename);

            var ftp = GetRequest(URI);
            ftp.Method = WebRequestMethods.Ftp.DeleteFile;
            try
            {
                var str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool FtpFileExists(string filename)
        {
            try
            {
                var size = GetFileSize(filename);
                return true;
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    if (ex.Message.Contains("550"))
                        return false;
                    throw;
                }

                throw;
            }
        }


        public long GetFileSize(string filename)
        {
            string path = null;
            if (filename.Contains("/"))
                path = AdjustDir(filename);
            else
                path = CurrentDirectory + filename;
            var URI = Hostname + path;
            var ftp = GetRequest(URI);
            ftp.Method = WebRequestMethods.Ftp.GetFileSize;
            var tmp = GetStringResponse(ftp);
            return GetSize(ftp);
        }


        public bool FtpRename(string sourceFilename, string newName)
        {
            var source = GetFullPath(sourceFilename);
            if (!FtpFileExists(source)) throw new FileNotFoundException("File " + source + " not found");

            var target = GetFullPath(newName);
            if (target == source)
                throw new ApplicationException("Source and target are the same");
            if (FtpFileExists(target)) throw new ApplicationException("Target file " + target + " already exists");

            var URI = Hostname + source;

            var ftp = GetRequest(URI);
            ftp.Method = WebRequestMethods.Ftp.Rename;
            ftp.RenameTo = target;
            try
            {
                var str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool FtpCreateDirectory(string dirpath)
        {
            var URI = Hostname + AdjustDir(dirpath);
            var ftp = GetRequest(URI);
            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                var str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool FtpDeleteDirectory(string dirpath)
        {
            var URI = Hostname + AdjustDir(dirpath);
            var ftp = GetRequest(URI);
            ftp.Method = WebRequestMethods.Ftp.RemoveDirectory;
            try
            {
                var str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public DateTime GetDateTimestamp(string fileName)
        {
            var source = GetFullPath(fileName);
            if (!FtpFileExists(source)) throw new FileNotFoundException("File " + source + " not found");

            var URI = Hostname + source;
            var ftp = GetRequest(URI);
            ftp.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            return response.LastModified;
        }

        #endregion

        #region '"private supporting fns"' 

        private string _lastDirectory = "";

        private FtpWebRequest GetRequest(string URI)
        {
            var result = (FtpWebRequest) WebRequest.Create(URI);
            result.Credentials = GetCredentials();
            result.KeepAlive = false;
            result.EnableSsl = EnableSSL;
            return result;
        }


        private ICredentials GetCredentials()
        {
            return new NetworkCredential(Username, Password);
        }


        private string GetFullPath(string file)
        {
            if (file.Contains("/"))
                return AdjustDir(file);
            return CurrentDirectory + file;
        }


        private string AdjustDir(string path)
        {
            return (string) (path.StartsWith("/") ? "" : "/") + path;
        }


        private string GetDirectory(string directory)
        {
            string URI = null;
            if (directory == "")
            {
                URI = Hostname + CurrentDirectory;
                _lastDirectory = CurrentDirectory;
            }
            else
            {
                if (!directory.StartsWith("/")) throw new ApplicationException("Directory should start with /");
                URI = Hostname + directory;
                _lastDirectory = directory;
            }

            return URI;
        }


        private string GetDirectory()
        {
            return GetDirectory("");
        }


        private string GetStringResponse(FtpWebRequest ftp)
        {
            var result = "";
            using (var response = (FtpWebResponse) ftp.GetResponse())
            {
                using (var datastream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(datastream))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                    }

                    datastream.Close();
                }

                response.Close();
            }

            return result;
        }


        private long GetSize(FtpWebRequest ftp)
        {
            long size = 0;
            using (var response = (FtpWebResponse) ftp.GetResponse())
            {
                size = response.ContentLength;
                response.Close();
            }

            return size;
        }

        #endregion

        #region '"Properties"' 

        private string _currentDirectory = "/";
        private string _hostname;

        private string _username;

        public string Hostname
        {
            get
            {
                if (_hostname.StartsWith("ftp://"))
                    return _hostname;
                return "ftp://" + _hostname;
            }
            set { _hostname = value; }
        }

        public string Username
        {
            get { return _username == "" ? "anonymous" : _username; }
            set { _username = value; }
        }

        public string Password { get; set; }

        public bool EnableSSL { get; set; }

        public bool Passive { get; set; }

        public bool KeepAlive { get; set; }

        public string CurrentDirectory
        {
            get
            {
                return _currentDirectory + (string) (_currentDirectory.EndsWith("/") ? "" : "/");
            }
            set
            {
                if (!value.StartsWith("/")) throw new ApplicationException("Directory should start with /");
                _currentDirectory = value;
            }
        }

        #endregion
    }

    #endregion

    #region '"FTP file info class"' 

    public class DBFtpFileInfo
    {
        #region DirectoryEntryTypes enum

        public enum DirectoryEntryTypes
        {
            File,
            Directory
        }

        #endregion

        #region '"Regular expressions for parsing LIST results"' 

        private static readonly string[] _ParseFormats =
        {
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\w+\s+\w+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{4})\s+(?<name>.+)",
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\d+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{4})\s+(?<name>.+)",
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\d+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<name>.+)",
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\w+\s+\w+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<name>.+)",
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})(\s+)(?<size>(\d+))(\s+)(?<ctbit>(\w+\s\w+))(\s+)(?<size2>(\d+))\s+(?<timestamp>\w+\s+\d+\s+\d{2}:\d{2})\s+(?<name>.+)",
            @"(?<timestamp>\d{2}\-\d{2}\-\d{2}\s+\d{2}:\d{2}[Aa|Pp][mM])\s+(?<dir>\<\w+\>){0,1}(?<size>\d+){0,1}\s+(?<name>.+)"
        };

        #endregion

        public DBFtpFileInfo(string line, string path)
        {
            var m = GetMatchingRegex(line);
            if (m == null) throw new ApplicationException("Unable to parse line: " + line);

            Filename = m.Groups["name"].Value;
            Path = path;
            Size = long.Parse(m.Groups["size"].Value);
            Permission = m.Groups["permission"].Value;
            var _dir = m.Groups["dir"].Value;
            if (!string.IsNullOrEmpty(_dir) && _dir != "-")
                FileType = DirectoryEntryTypes.Directory;
            else
                FileType = DirectoryEntryTypes.File;

            try
            {
                FileDateTime = DateTime.Parse(m.Groups["timestamp"].Value);
            }
            catch (Exception)
            {
                FileDateTime = Convert.ToDateTime(null);
            }
        }

        private Match GetMatchingRegex(string line)
        {
            Regex rx = null;
            Match m = null;
            for (var i = 0; i <= _ParseFormats.Length - 1; i++)
            {
                rx = new Regex(_ParseFormats[i]);
                m = rx.Match(line);
                if (m.Success) return m;
            }

            return null;
        }

        #region '"Properties"' 

        public string FullName => Path + Filename;

        public string Filename { get; }

        public string Path { get; }

        public DirectoryEntryTypes FileType { get; }

        public long Size { get; }

        public DateTime FileDateTime { get; }

        public string Permission { get; }

        public string Extension
        {
            get
            {
                var i = Filename.LastIndexOf(".");
                if ((i >= 0) & (i < Filename.Length - 1))
                    return Filename.Substring(i + 1);
                return string.Empty;
            }
        }

        public string NameOnly
        {
            get
            {
                var i = Filename.LastIndexOf(".");
                if (i > 0)
                    return Filename.Substring(0, i);
                return Filename;
            }
        }

        #endregion
    }

    #endregion

    #region '"FTP Directory class"' 

    public class DBFtpDirectory : List<DBFtpFileInfo>
    {
        private const char slash = '/';

        public DBFtpDirectory()
        {
        }

        public DBFtpDirectory(string dir, string path)
        {
            foreach (var line in dir.Replace(Global.Lf, "").Split(char.Parse(Global.Cr)))
                if (!string.IsNullOrEmpty(line))
                    Add(new DBFtpFileInfo(line, path));
        }

        public DBFtpDirectory GetFiles(string ext)
        {
            return GetFileOrDir(DBFtpFileInfo.DirectoryEntryTypes.File, ext);
        }


        public DBFtpDirectory GetFiles()
        {
            return GetFiles("");
        }


        public DBFtpDirectory GetDirectories()
        {
            return GetFileOrDir(DBFtpFileInfo.DirectoryEntryTypes.Directory, "");
        }


        private DBFtpDirectory GetFileOrDir(DBFtpFileInfo.DirectoryEntryTypes type, string ext)
        {
            var result = new DBFtpDirectory();
            foreach (var fi in this)
                if (fi.FileType == type)
                {
                    if (ext == "")
                        result.Add(fi);
                    else if (ext == fi.Extension) result.Add(fi);
                }

            return result;
        }


        private DBFtpDirectory GetFileOrDir(DBFtpFileInfo.DirectoryEntryTypes type)
        {
            return GetFileOrDir(type, "");
        }


        public bool FileExists(string filename)
        {
            foreach (var ftpfile in this)
                if (ftpfile.Filename == filename)
                    return true;
            return false;
        }


        public static string GetParentDirectory(string dir)
        {
            var tmp = dir.TrimEnd(slash);
            var i = tmp.LastIndexOf(slash);
            if (i > 0)
                return tmp.Substring(0, i - 1);
            throw new ApplicationException("No parent for root");
        }
    }

    #endregion
}