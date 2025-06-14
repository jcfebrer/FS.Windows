using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace FSReport
{
    public class Functions
    {
        public static void SetSubreportSelection(CrystalDecisions.CrystalReports.Engine.ReportDocument crReportDocument, string subRerportSelection, string server, string database, string userName, string password)
        {
            ReportObjects crReportObjects = null;

            crReportObjects = crReportDocument.ReportDefinition.ReportObjects;

            int f = 0;
            ReportObject crReportObject = null;
            SubreportObject crSubreportObject = null;
            CrystalDecisions.CrystalReports.Engine.ReportDocument crSubreport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            for (f = 0; f <= crReportObjects.Count - 1; f++)
            {
                crReportObject = crReportObjects[f];


                if (crReportObject.Kind == ReportObjectKind.SubreportObject)
                {
                    crSubreportObject = ((SubreportObject)(crReportObject));
                    crSubreport = crReportDocument.OpenSubreport(crSubreportObject.SubreportName);

                    ChangeLoginInfo(crSubreport.Database.Tables, server, database, userName, password);

                    if (subRerportSelection != "" & crSubreport.RecordSelectionFormula != "")
                    {
                        crSubreport.RecordSelectionFormula = subRerportSelection;
                    }
                }
            }
        }


        public static void ChangeLoginInfo(Tables crystalReportTables, string server, string database, string userName, string password)
        {
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();


            crConnectionInfo.ServerName = server;
            crConnectionInfo.DatabaseName = database;
            crConnectionInfo.UserID = userName;
            if (password != null) crConnectionInfo.Password = password;


            foreach (Table crystalReportTable in crystalReportTables)
            {
                crtableLogoninfo = crystalReportTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                crystalReportTable.ApplyLogOnInfo(crtableLogoninfo);
            }

            foreach (Table CrTable in crystalReportTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;

                crConnectionInfo.UserID = userName;
                if (password != null) crConnectionInfo.Password = password;

                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
        }

        /// <summary>
        /// Asigna una imagen a una tabla de un DataSet
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public static DataSet SetImageToDataSet(string imagePath, string tableName, string fieldName, DataSet dataSet)
        {
            FileStream FilStr = new FileStream(imagePath, FileMode.Open);
            BinaryReader BinRed = new BinaryReader(FilStr);
            DataRow dr = dataSet.Tables[tableName].NewRow();

            dr[fieldName] = BinRed.ReadBytes((int)BinRed.BaseStream.Length);

            dataSet.Tables[tableName].Rows.Add(dr);

            FilStr.Close();
            BinRed.Close();

            return dataSet;
        }
    }
}
