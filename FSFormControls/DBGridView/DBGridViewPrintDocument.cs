#region

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxItem(false)]
    public class DBGridViewPrintDocument : System.Drawing.Printing.PrintDocument
    {
        private readonly FSFormControls.DBGridView dataGridToPrint;
        private readonly DataView objView;
        public int CurCol;
        public int CurRow;
        private Font headerFont;
        public int NextFirstCol;
        private int npp;
        private Font printFont;
        private CurrencyManager privCM;

        public DBGridViewPrintDocument(FSFormControls.DBGridView dataGridToPrint, DataView objView, DataTable objTable,
            CurrencyManager privCM,
            int NP, string Question)
        {
            this.dataGridToPrint = dataGridToPrint;
            if (objView == null)
                this.objView = objTable.DefaultView;
            else
                this.objView = objView;
            DataTable = objTable;
            this.privCM = privCM;
            NumberOfPagesPreviewed = NP;
            QuestionAfterNPreviewedPages = Question;
        }

        public DataTable DataTable { get; }

        public int NumberOfPagesPreviewed { get; set; }

        public string QuestionAfterNPreviewedPages { get; set; }

        protected override void OnBeginPrint(PrintEventArgs ev)
        {
            base.OnBeginPrint(ev);
            CurRow = 0;
            CurCol = 0;
            NextFirstCol = 0;
            npp = 0;
            headerFont = dataGridToPrint.DefaultHeaderFont;
            printFont = dataGridToPrint.Font;
        }


        protected override void OnPrintPage(PrintPageEventArgs ev)
        {
            base.OnPrintPage(ev);

            NextFirstCol = 0;
            npp += 1;

            float lpp = 0;
            //float yPos = 0; 
            //int count = 0; 
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            var strField = "";

            lpp = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

            //DataGridTableStyle tblStyle = dataGridToPrint.TableStyles[objTable.TableName];

            var rh = dataGridToPrint.DefaultHeaderFont.Height + 5;
            var headerHeight = 0;
            headerHeight = rh;

            var nrperpage = ev.MarginBounds.Height / rh - headerHeight / rh;
            int i = 0, j = 0, w = 0, l = 0;
            l = Convert.ToInt32(topMargin);
            w = Convert.ToInt32(leftMargin);
            var divPen = new Pen(dataGridToPrint.ForeColor);
            var hfpen = new Pen(dataGridToPrint.ForeColor);
            var hbBrush = new SolidBrush(dataGridToPrint.BackColor);
            var hfBrush = new SolidBrush(dataGridToPrint.ForeColor);
            for (j = CurCol; j <= dataGridToPrint.Columns.Count - 1; j++)
            {
                if ((w + dataGridToPrint.Columns[j].Width >= ev.MarginBounds.Right) & (j != CurCol))
                {
                    NextFirstCol = j;
                    break;
                }

                if (dataGridToPrint.Columns[j].Width != 0)
                {
                    var r = new Rectangle(w, l, dataGridToPrint.Columns[j].Width, headerHeight);
                    var rf = new RectangleF(w, l, r.Width, r.Height);
                    ev.Graphics.FillRectangle(hbBrush, r);

                    ev.Graphics.DrawString(dataGridToPrint.Columns[j].HeaderCaption, headerFont, hfBrush, rf);

                    w += dataGridToPrint.Columns[j].Width;
                }
            }

            l += headerHeight;
            for (i = CurRow; i <= nrperpage + CurRow - 1; i++)
            {
                if (i >= objView.Count) break;
                w = Convert.ToInt32(leftMargin);
                for (j = CurCol; j <= dataGridToPrint.Columns.Count - 1; j++)
                {
                    if ((w + dataGridToPrint.Columns[j].Width >= ev.MarginBounds.Right) & (j != CurCol)) break;
                    if (dataGridToPrint.Columns[j].Width != 0)
                    {
                        var r = new Rectangle(w, l, dataGridToPrint.Columns[j].Width, rh);
                        var rf = new RectangleF(w, l + 2, r.Width, r.Height);
                        ev.Graphics.DrawRectangle(divPen, r);
                        strField = dataGridToPrint.RowValue(j, i) + "";
                        ev.Graphics.DrawString(strField, printFont, Brushes.Black, rf);
                        w += dataGridToPrint.Columns[j].Width;
                    }
                }

                l += rh;
            }

            l = Convert.ToInt32(topMargin);
            w = Convert.ToInt32(leftMargin);
            for (j = CurCol; j <= dataGridToPrint.Columns.Count - 1; j++)
            {
                if ((w + dataGridToPrint.Columns[j].Width >= ev.MarginBounds.Right) & (j != CurCol)) break;
                if (dataGridToPrint.Columns[j].Width != 0)
                {
                    var r = new Rectangle(w, l, dataGridToPrint.Columns[j].Width, headerHeight);
                    ev.Graphics.DrawRectangle(Pens.Black, r);

                    w += dataGridToPrint.Columns[j].Width;
                }
            }

            CurRow = i;

            if (NumberOfPagesPreviewed != -1)
                if (npp >= NumberOfPagesPreviewed)
                {
                    if (MessageBox.Show(QuestionAfterNPreviewedPages, "", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) ==
                        DialogResult.No)
                    {
                        ev.HasMorePages = false;
                        return;
                    }

                    npp -= NumberOfPagesPreviewed;
                }

            if (CurRow < objView.Count)
            {
                ev.HasMorePages = true;
            }
            else
            {
                if (NextFirstCol == 0)
                {
                    ev.HasMorePages = false;
                }
                else
                {
                    CurRow = 0;
                    CurCol = NextFirstCol;
                    ev.HasMorePages = true;
                }
            }

            divPen.Dispose();
            hfpen.Dispose();
            hbBrush.Dispose();
            hfBrush.Dispose();
        }
    }
}