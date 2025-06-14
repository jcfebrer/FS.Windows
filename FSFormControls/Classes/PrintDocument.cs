#region

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using FSDatabase;

#endregion

namespace FSFormControls
{
    [ToolboxItem(false)]
    public class PrintDocument : System.Drawing.Printing.PrintDocument
    {
        private readonly DBControl DBControlToPrint;
        private readonly DataView objView;
        public int CurCol;
        public int CurRow;
        private Font headerFont;
        public int NextFirstCol;
        private int npp;

        private Font printFont;

        //private CurrencyManager privCM; 

        public PrintDocument(DBControl DBControlToPrint, DataView objView, DataTable objTable, int NP, string Question)
        {
            this.DBControlToPrint = DBControlToPrint;
            if (objView == null)
                this.objView = objTable.DefaultView;
            else
                this.objView = objView;
            DataTable = objTable;
            //this.privCM = privCM; 
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
            headerFont = new Font("Arial", 8, FontStyle.Regular);
            printFont = new Font("Arial", 8, FontStyle.Regular);
        }


        protected override void OnPrintPage(PrintPageEventArgs ev)
        {
            base.OnPrintPage(ev);

            var db = new BdUtils(Global.ConnectionString);
            NextFirstCol = 0;
            npp += 1;

            float lpp = 0;
            //float yPos = 0; 
            //int count = 0; 
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            var strField = "";

            lpp = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);


            var rh = 15;
            var headerHeight = 0;
            headerHeight = rh;

            var nrperpage = ev.MarginBounds.Height / rh - headerHeight / rh;
            int i = 0, j = 0, w = 0, l = 0;
            l = Convert.ToInt32(topMargin);
            w = Convert.ToInt32(leftMargin);
            var divPen = new Pen(Color.Black);
            var hfpen = new Pen(Color.Black);
            var hbBrush = new SolidBrush(Color.Gray);
            var hfBrush = new SolidBrush(Color.White);
            for (j = CurCol; j <= Convert.ToInt32(DBControlToPrint.FieldsCount() - 1); j++)
            {
                var tamano = db.GetField(DBControlToPrint.FieldName(j), DBControlToPrint.TableName).Tamano;
                if ((w + tamano * 8 >= ev.MarginBounds.Right) & (j != CurCol))
                {
                    NextFirstCol = j;
                    break;
                }

                if (tamano * 8 != 0)
                {
                    var r = new Rectangle(w, l, Convert.ToInt32(tamano * 8), headerHeight);
                    var rf = new RectangleF(w, l, r.Width, r.Height);
                    ev.Graphics.FillRectangle(hbBrush, r);

                    ev.Graphics.DrawString(DBControlToPrint.FieldName(j), headerFont, hfBrush, rf);

                    w += Convert.ToInt32(tamano * 8);
                }
            }

            l += headerHeight;
            for (i = CurRow; i <= nrperpage + CurRow - 1; i++)
            {
                if (i >= objView.Count) break;
                w = Convert.ToInt32(leftMargin);
                for (j = CurCol; j <= Convert.ToInt32(DBControlToPrint.FieldsCount() - 1); j++)
                {
                    var tamano = db.GetField(DBControlToPrint.FieldName(j), DBControlToPrint.TableName).Tamano;
                    if ((w + tamano * 8 >= ev.MarginBounds.Right) & (j != CurCol)) break;
                    if (tamano * 8 != 0)
                    {
                        var r = new Rectangle(w, l, Convert.ToInt32(tamano * 8), rh);
                        var rf = new RectangleF(w, l, r.Width, r.Height);
                        ev.Graphics.DrawRectangle(divPen, r);
                        strField = DBControlToPrint.DataTable.Rows[i][j] + "";
                        ev.Graphics.DrawString(strField, printFont, Brushes.Black, rf);
                        w += Convert.ToInt32(tamano * 8);
                    }
                }

                l += rh;
            }

            l = Convert.ToInt32(topMargin);
            w = Convert.ToInt32(leftMargin);
            for (j = CurCol; j <= Convert.ToInt32(DBControlToPrint.FieldsCount() - 1); j++)
            {
                var tamano = db.GetField(DBControlToPrint.FieldName(j), DBControlToPrint.TableName).Tamano;
                if ((w + tamano * 8 >= ev.MarginBounds.Right) & (j != CurCol)) break;
                if (tamano * 8 != 0)
                {
                    var r = new Rectangle(w, l, Convert.ToInt32(tamano * 8), headerHeight);
                    ev.Graphics.DrawRectangle(Pens.Black, r);

                    w += Convert.ToInt32(tamano * 8);
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