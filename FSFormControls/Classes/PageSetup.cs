#region

using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class PageSetup
    {
        public static PageSettings PageSettings { get; set; } = new PageSettings();

        public static void Setup()
        {
            try
            {
                var psDlg = new PageSetupDialog();

                psDlg.PageSettings = PageSettings;
                psDlg.ShowDialog();

                PageSettings = psDlg.PageSettings;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("An error occurred.", ex);
            }
        }
    }
}