#region

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using FSException;
using FSFormControls;

#endregion

namespace FSReport
{
    public class Report : DBUserControl
    {
        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal CrystalReportViewer CrystalReportViewer1;

        public Report()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.CrystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CrystalReportViewer1
            // 
            this.CrystalReportViewer1.ActiveViewIndex = -1;
            this.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CrystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.CrystalReportViewer1.Name = "CrystalReportViewer1";
            this.CrystalReportViewer1.Size = new System.Drawing.Size(574, 456);
            this.CrystalReportViewer1.TabIndex = 0;
            // 
            // DBReport
            // 
            this.Controls.Add(this.CrystalReportViewer1);
            this.Name = "FSReport";
            this.Size = new System.Drawing.Size(574, 456);
            this.ResumeLayout(false);

        }

        #endregion

        public ParameterValues currValue;
        public DataSet m_DataSet;
        public string m_Database = "";
        public string m_Parameters = "";
        public string m_Password = "";
        public string m_ReportFile = "";
        public string m_Selection = "";
        public string m_Server = "";
        public string m_SubReportSelection = "";
        public string m_UserName = "";

        public ParameterDiscreteValue paraValue = new ParameterDiscreteValue();
        public string[] strParValPair;
        public string[] strVal;

        public string ReportFile
        {
            get { return m_ReportFile; }
            set { m_ReportFile = value; }
        }

        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        public string Parameters
        {
            get { return m_Parameters; }
            set { m_Parameters = value; }
        }

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        public string Server
        {
            get { return m_Server; }
            set { m_Server = value; }
        }

        public string Database
        {
            get { return m_Database; }
            set { m_Database = value; }
        }

        public string Selection
        {
            get { return m_Selection; }
            set { m_Selection = value; }
        }

        public string SubReportSelection
        {
            get { return m_SubReportSelection; }
            set { m_SubReportSelection = value; }
        }

        public DataSet DataSet
        {
            get { return m_DataSet; }
            set { m_DataSet = value; }
        }

        public bool DisplayToolbar
        {
            get { return CrystalReportViewer1.DisplayToolbar; }
            set { CrystalReportViewer1.DisplayToolbar = value; }
        }

        public bool ShowCloseButton
        {
            get { return CrystalReportViewer1.ShowCloseButton; }
            set { CrystalReportViewer1.ShowCloseButton = value; }
        }

        public bool ShowExportButton
        {
            get { return CrystalReportViewer1.ShowExportButton; }
            set { CrystalReportViewer1.ShowExportButton = value; }
        }

        public bool ShowGotoPageButton
        {
            get { return CrystalReportViewer1.ShowGotoPageButton; }
            set { CrystalReportViewer1.ShowGotoPageButton = value; }
        }

        public bool ShowGroupTreeButton
        {
            get { return CrystalReportViewer1.ShowGroupTreeButton; }
            set { CrystalReportViewer1.ShowGroupTreeButton = value; }
        }

        public void Connect()
        {
            try
            {
                if (m_ReportFile == "" || m_ReportFile == null)
                {
                    throw new ExceptionUtil("Debes indicar el nombre del informe en ReportFile.");
                }

                if (m_Database == "" || m_Database == null)
                {
					throw new ExceptionUtil("Debes indicar el nombre de la base de datos en DataBase. En Access, debes indicar el nombre y path del fichero .MDB");
                }

                Cursor.Current = Cursors.WaitCursor;

                CrystalDecisions.CrystalReports.Engine.ReportDocument crReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                crReportDocument.Load(m_ReportFile);

                int intCounter = 0;
                int index = 0;
                intCounter = crReportDocument.DataDefinition.ParameterFields.Count;

                if (intCounter == 1)
                {
                    string ch = crReportDocument.DataDefinition.ParameterFields[0].ParameterFieldName;

					if (FSLibrary.TextUtil.IndexOf(ch, ".") > 0)
                    {
                        intCounter = 0;
                    }
                }

                if (intCounter > 0 & !String.IsNullOrEmpty(m_Parameters.Trim()))
                {
                    strParValPair = m_Parameters.Split(char.Parse("&"));

                    Array arrParValPair = strParValPair;
                    for (index = 0; index <= arrParValPair.GetUpperBound(0); index++)
                    {
                        if (strParValPair[index].IndexOf("=") + 1 > 0)
                        {
                            strVal = strParValPair[index].Split(char.Parse("="));
                            paraValue.Value = strVal[1];
                            currValue = crReportDocument.DataDefinition.ParameterFields[strVal[0]].CurrentValues;
                            currValue.Add(paraValue);
                            crReportDocument.DataDefinition.ParameterFields[strVal[0]].ApplyCurrentValues(currValue);
                        }
                    }
                }

                Cursor.Current = Cursors.Default;

                if (!String.IsNullOrEmpty(m_Server))
                {
                    if (String.IsNullOrEmpty(m_UserName))
                    {
						throw new ExceptionUtil("Debes indicar el nombre del usuario en UserName.");
                    }
                }
                else
                {
                    m_Server = m_Database;
                    m_Database = "";
                }

                Functions.ChangeLoginInfo(crReportDocument.Database.Tables, m_Server, m_Database, m_UserName, m_Password);
                Functions.SetSubreportSelection(crReportDocument, m_SubReportSelection, m_Server, m_Database, m_UserName, m_Password);


                if (DataSet != null)
                {
                    crReportDocument.SetDataSource(DataSet);
                }

                if (!String.IsNullOrEmpty(m_Selection))
                {
                    CrystalReportViewer1.SelectionFormula = m_Selection;
                }

                CrystalReportViewer1.ReportSource = null;
                CrystalReportViewer1.ReportSource = crReportDocument;

                CrystalReportViewer1.Show();
            }
            catch (System.Exception e)
            {
				throw new ExceptionUtil(e);
            }
        }
    }
}