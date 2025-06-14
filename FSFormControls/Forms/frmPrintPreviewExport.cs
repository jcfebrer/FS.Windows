#region

using FSFormControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxItem(false)]
    public class frmPrintPreviewExport : frmPrintPreview
    {
        public frmPrintPreviewExport()
        {
            InitializeComponent();

            var btn = new ToolStripButton();
            btn.ToolTipText = "Exportar datos";
            btn.Tag = 100;
            btn.ImageIndex = 0;
            var btn2 = new ToolStripButton();
            btn2.ToolTipText = "Enviar datos por e-Mail";
            btn2.Tag = 101;
            btn2.ImageIndex = 1;

            var btns = new ToolStripButton[2];
            btns[0] = btn;
            btns[1] = btn2;

            if (pToolBar.ImageList == null)
                pToolBar.ImageList = new ImageList();

            AddToolBarButtons(btns);

            AddedButtonsClick += TablePrintPreviewDialog_AddedButtonsClick;
        }

        public object TableDocument
        {
            get { return Document; }
            set { Document = (System.Drawing.Printing.PrintDocument) value; }
        }

        private void InitializeComponent()
        {
            var resources = new ResourceManager(typeof(frmPrintPreviewExport));
            AddedButtonsImageList.ImageStream =
                (ImageListStreamer) resources.GetObject("AddedButtonsImageList.ImageStream");
            ClientSize = new Size(672, 304);
            Name = "frmPrintPreviewExport";
        }

        private void TablePrintPreviewDialog_AddedButtonsClick(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;
            var TblDocument = TableDocument;
            DataTable tbl = null;

            if (TblDocument is PrintDocument) tbl = ((PrintDocument) TblDocument).DataTable;
            if (TblDocument is DBGridViewPrintDocument) tbl = ((DBGridViewPrintDocument) TblDocument).DataTable;

            var ds = tbl.DataSet;

            if (ds == null)
            {
                ds = new DataSet();
                ds.Tables.Add(tbl);
            }

            if (Convert.ToInt32(tsb.Tag) == 100)
            {
                Export.ExportDataset(ds);
            }
            else if (Convert.ToInt32(tsb.Tag) == 101)
            {
                var ibr = InputBox.ShowDialog("¿Dirección email de destino?", "", "", InputBox.Icon.Question, InputBox.Buttons.Ok, InputBox.Type.TextBox);
                Export.SendEMailDataSet(Global.MailUserName, InputBox.ResultValue, ds);
            }
        }
    }
}