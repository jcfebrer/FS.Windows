#region

using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using FSDatabase;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class Export
    {
        public static void ExportDataset(DataSet ds)
        {
            var objSaveFileDialog = new SaveFileDialog();
            objSaveFileDialog.Filter = "Fichero Xml (*.xml)|*.xml|Fichero Html (*.htm)|*.htm|Todos (*.*)|*.*";
            objSaveFileDialog.Title = "Exportación de datos";
            if (objSaveFileDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    var file = objSaveFileDialog.FileName;
                    if (file.Substring(file.Length - 3).ToLower() == "xml")
                        Utils.ExportXml(objSaveFileDialog.FileName, ds);
                    else
                        Utils.ExportHtml(objSaveFileDialog.FileName, ds);
                }
                catch (Exception ex)
                {
                    throw new ExceptionUtil("Error en la exportación del fichero.", ex);
                }

            objSaveFileDialog.Dispose();
            objSaveFileDialog = null;
            GC.Collect();
        }


        public static void SendEMailDataSet(string FromAddress, string ToAddress, DataSet ds)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(FromAddress);
                mail.Subject = "Informe";
                mail.Body = "Prueba";

                var fileName = Path.GetTempPath() + "default.htm";

                Utils.ExportHtml(fileName, ds);

                var ma = new Attachment(fileName);
                mail.Attachments.Add(ma);

                var smtp = new SmtpClient();
                smtp.Send(mail);
                File.Delete(fileName);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("Error en el envio del correo.", ex);
            }
        }
    }
}