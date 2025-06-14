#region

using System.Drawing;

#endregion

namespace FSFormControls
{
    public static class Global
    {
        public enum AccessMode
        {
            ReadMode,
            WriteMode,
            ProtectedMode
        }

        public const int MAX_COLUMN_WIDTH = 500;
        public const int MAX_TEXT_LENGTH = 32767;
        public const int DATE_LENGTH = 10;
        public const string DATE_FORMAT = "dd/MM/yyyy";
        public const string TIME_FORMAT = "HH:mm";
        public const int CARACTER_SIZE = 6;
        public const string SINDEFINIR = "";

        public const string CrLf = "\n";
        public const string Cr = "\t";
        public const string Lf = "\r";
        public const string Tab = " ";

        public static Color ObligatoryBackColor = Color.LightYellow;
        public static Color NormalBackColor = Color.AntiqueWhite;
        public static Color DisableBackColor = Color.DarkGray;
        public static Color ProtectedBackColor = Color.Red;
        public static Color WriteBackColor = Color.LightBlue;

        public static string ProjectName = "FSFormControls";
        public static string ApplicationTittle = "FSFormControls - Febrer Software 2014";
        public static string UserName = "";

        public static DBControl.DbActionTypes Action = DBControl.DbActionTypes.None;
        public static string ConnectionString;
        //public static DbConnection DBconnection;
        //public static FSDatabase.Utils.TypeBd typeDb = FSDatabase.Utils.TypeBd.Oledb;
        public static bool SilentError;
        public static bool ShowCalc = true;

        public static string MailServer = "mail.febrersoftware.com";
        public static string MailUserName = "juancarlos@febrersoftware.com";
        public static string MailPassword = "*******";
        public static int MailPort = 25;
        public static bool MailEnableSSL = true;

        //Application.OpenForms
        public static FormsCollection Forms = new FormsCollection();
        public static ErrorCollection Errors = new ErrorCollection();
    }
}