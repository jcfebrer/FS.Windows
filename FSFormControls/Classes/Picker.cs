using FSDisk;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FSFormControls
{
    public class Picker
    {
        public static Font FontPicker(Font oFont)
        {
            var fd = new FontDialog();

            fd.Font = oFont;


            if (fd.ShowDialog() != DialogResult.Cancel) oFont = fd.Font;

            return oFont;
        }

        public static Font FontPicker()
        {
            return FontPicker("Arial", 8);
        }

        public static Font FontPicker(string cFontName, long nFontSize)
        {
            var f = new Font(cFontName, nFontSize);

            return FontPicker(f);
        }

        public static Color ColorPicker(Color oColor)
        {
            var cd = new ColorDialog();

            cd.ShowHelp = true;

            cd.Color = oColor;

            cd.ShowDialog();
            return cd.Color;
        }


        public static Color ColorPicker()
        {
            var oColor = new Color();

            return ColorPicker(oColor);
        }


        public static string DirectoryPicker()
        {
            return DirectoryPicker("");
        }

        public static string DirectoryPicker(string tcTitle)
        {
            var db = new GetDirBrowser();
            return db.ShowIt(tcTitle);
        }


        public static string OpenFilePicker(string cFileName)
        {
            var lcFile = "";

            try
            {
                if (System.IO.File.Exists(cFileName))
                    lcFile = FileUtils.FullPath(cFileName);
                else if (System.IO.File.Exists(FileUtils.AddBackSlash(System.IO.Directory.GetCurrentDirectory()) + cFileName))
                    lcFile = FileUtils.AddBackSlash(System.IO.Directory.GetCurrentDirectory()) + cFileName;
                else
                    lcFile = GetFile("", 0, "");
            }
            catch
            {
                MessageBox.Show("An error occured while locating the file.", "Error: OpenFilePicker()",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return lcFile;
        }


        private static string GetFile(string tcFilter, int tnFilterIndex, string tcTitle)
        {
            var lcFile = "";
            var lcFilter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            var lnFilterIndex = 2;
            var lcTitle = "Open";

            if (tcFilter.Length > 0)
            {
                lcFilter += GetExtenstion(tcFilter);
                lnFilterIndex = tnFilterIndex;
            }


            if (tcTitle.Length > 0) lcTitle = tcTitle;


            var ofd = new OpenFileDialog();

            ofd.Filter = lcFilter;

            ofd.FilterIndex = lnFilterIndex;

            ofd.Title = lcTitle;

            ofd.RestoreDirectory = true;
            ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            if (ofd.ShowDialog() != DialogResult.Cancel) lcFile = ofd.FileName;

            return lcFile;
        }


        public static string SaveFilePicker(string tcTitle, string tcFileName, string tcExtension)
        {
            var lcFileName = "";
            var lcFilter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            var sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            sfd.FileName = tcFileName;

            if (tcExtension.Trim().Length == 0)
            {
                sfd.Filter = lcFilter;
                sfd.FilterIndex = 2;
            }
            else
            {
                try
                {
                    sfd.Filter = lcFilter + GetExtenstion(tcExtension);
                    sfd.FilterIndex = 3;
                }
                catch
                {
                    sfd.Filter = lcFilter;
                }
            }

            if (tcTitle.Trim().Length > 0) sfd.Title = tcTitle;

            if (sfd.ShowDialog() != DialogResult.Cancel) lcFileName = sfd.FileName;

            return lcFileName;
        }


        public static string SaveFilePicker()
        {
            return SaveFilePicker("", "", "");
        }


        public static string SaveFilePicker(string tcTitle)
        {
            return SaveFilePicker(tcTitle, "", "");
        }


        public static string SaveFilePicker(string tcTitle, string tcFileName)
        {
            return SaveFilePicker(tcTitle, tcFileName, "");
        }


        private static string GetExtenstion(string lcExtension)
        {
            char[] laSeperators = {';', '|'};

            var laExtensions = lcExtension.Split(laSeperators);
            var sb = new StringBuilder();
            string lcCurrent = null;
            var lcPrevious = "";
            var i = 0;
            for (i = 0; i <= laExtensions.Length - 1; i += i + 1)
            {
                lcCurrent = laExtensions[i].Trim();

                if ((lcCurrent.IndexOf("(") >= 0) & (lcPrevious.Length == 0))
                {
                    lcPrevious = lcCurrent;
                }
                else
                {
                    if (lcCurrent.IndexOf(".") < 0) lcCurrent = "*." + lcCurrent;

                    if (lcPrevious.Length == 0)
                    {
                        sb.Append("|" + lcCurrent);
                        sb.Append("|" + lcCurrent);
                    }
                    else
                    {
                        sb.Append("|" + lcPrevious);
                        sb.Append("|" + lcCurrent);
                    }

                    lcPrevious = "";
                }
            }

            return sb.ToString();
        }

        private class GetDirBrowser : FolderNameEditor
        {
            private readonly FolderBrowser fBrowser;

            public GetDirBrowser()
            {
                fBrowser = new FolderBrowser();
            }

            public string ShowIt(string textdescription)
            {
                fBrowser.Description = textdescription;
                fBrowser.ShowDialog();
                return fBrowser.DirectoryPath;
            }
        }
    }
}