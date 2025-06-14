#region

using FSException;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Printing;
using DateTimeUtil = FSLibrary.DateTimeUtil;

#endregion

namespace FSFormControls
{
    public class ReportDocument : System.Drawing.Printing.PrintDocument
    {
        #region '" GetField "' 

        private string GetField(object obj, string FieldName)
        {
            if (obj is DataRowView)
                return ((DataRowView) obj)[FieldName].ToString();
            if (obj is ValueType && obj.GetType().IsPrimitive)
                return obj.ToString();
            if (obj is string)
                return Convert.ToString(obj);
            try
            {
                var sourcetype = obj.GetType();

                var prop = sourcetype.GetProperty(FieldName);

                if (prop == null || !prop.CanRead)
                {
                    var field = sourcetype.GetField(FieldName);

                    if (field == null)
                        return "No such value " + FieldName;
                    return field.GetValue(obj).ToString();
                }

                return prop.GetValue(obj, null).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region '" Event declarations "' 

        #region Delegates

        public delegate void PrintPageBeginEventHandler(object sender, ReportPageEventArgs e);

        public delegate void PrintPageBodyEndEventHandler(object sender, ReportPageEventArgs e);

        public delegate void PrintPageBodyStartEventHandler(object sender, ReportPageEventArgs e);

        public delegate void PrintPageEndEventHandler(object sender, ReportPageEventArgs e);

        public delegate void ReportBeginEventHandler(object sender, ReportPageEventArgs e);

        public delegate void ReportEndEventHandler(object sender, ReportPageEventArgs e);

        #endregion

        public event ReportBeginEventHandler ReportBegin;

        public event PrintPageBeginEventHandler PrintPageBegin;

        public event PrintPageBodyStartEventHandler PrintPageBodyStart;

        public event PrintPageBodyEndEventHandler PrintPageBodyEnd;

        public event PrintPageEndEventHandler PrintPageEnd;

        public event ReportEndEventHandler ReportEnd;

        #endregion

        #region '" Report Properties and Settings "' 

        private bool mAutoDiscover;
        private IBindingList mBindingList;
        private string mDataMember;
        private object mDataSource;
        private int mPageNumber;

        public ReportDocument()
        {
            Font = new Font("Courier New", 10);
            Brush = Brushes.Black;
            FooterLines = 2;
        }

        public ReportDocument(Font font, Brush brush)
        {
            Font = font;
            Brush = brush;
            FooterLines = 2;
        }

        public Font Font { get; set; }

        public Brush Brush { get; set; }

        public bool SupressDefaultHeader { get; set; }

        public bool SupressDefaultFooter { get; set; }

        public int FooterLines { get; set; }

        public ReportColumnCollection Columns { get; } = new ReportColumnCollection();

        public string Title { get; set; }

        public string SubTitleLeft { get; set; }

        public string SubTitleRight { get; set; }

        public string FooterLeft { get; set; }

        public string FooterRight { get; set; }

        #endregion

        #region '" DataSource/DataMember "' 

        [Category("Data")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter," + "System.Design")]
        public object DataSource
        {
            get { return mDataSource; }
            set
            {
                mDataSource = value;
                SetSource();
                if (mAutoDiscover) DoAutoDiscover();
                mRow = 0;
            }
        }

        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor," + "System.Design", typeof(UITypeEditor))]
        public string DataMember
        {
            get { return mDataMember; }
            set
            {
                mDataMember = value;
                SetSource();
                if (mAutoDiscover) DoAutoDiscover();
                mRow = 0;
            }
        }

        private void SetSource()
        {
            var InnerSource = InnerDataSource();

            if (InnerSource is IBindingList)
                mBindingList = (IBindingList) InnerSource;
            else
                mBindingList = null;
        }


        private IList InnerDataSource()
        {
            if (mDataSource is DataSet)
            {
                if (mDataMember.Length > 0)
                    return ((IListSource) ((DataSet) mDataSource).Tables[mDataMember]).GetList();
                return ((IListSource) ((DataSet) mDataSource).Tables[0]).GetList();
            }

            if (mDataSource is IListSource)
                return ((IListSource) mDataSource).GetList();
            return (IList) mDataSource;
        }

        #endregion

        #region '" AutoDiscover "' 

        [Category("Data")]
        public bool AutoDiscover
        {
            get { return mAutoDiscover; }
            set
            {
                if (mAutoDiscover == false && value)
                {
                    mAutoDiscover = value;
                    DoAutoDiscover();
                }
                else
                {
                    mAutoDiscover = value;
                    if (mAutoDiscover == false) Columns.Clear();
                }
            }
        }

        private void DoAutoDiscover()
        {
            var InnerSource = InnerDataSource();
            Columns.Clear();

            if (InnerSource == null) return;

            if (InnerSource is DataView)
                DoAutoDiscover((DataView) InnerSource);
            else
                DoAutoDiscover(InnerSource);
        }


        private void DoAutoDiscover(DataView ds)
        {
            var field = 0;
            ReportColumn col = null;

            for (field = 0; field <= ds.Table.Columns.Count - 1; field++)
            {
                col = new ReportColumn();
                col.Name = ds.Table.Columns[field].Caption;
                col.Field = ds.Table.Columns[field].ColumnName;
                Columns.Add(col);
            }

            Columns.SetEvenSpacing(650);
        }


        private void DoAutoDiscover(IList ds)
        {
            if (ds.Count > 0)
            {
                var obj = ds[0];

                if (obj is ValueType && obj.GetType().IsPrimitive)
                {
                    ReportColumn col = null;
                    col = new ReportColumn();
                    col.Name = "Value";
                    Columns.Add(col);
                }
                else if (obj is string)
                {
                    ReportColumn col = null;
                    col = new ReportColumn();
                    col.Name = "Text";
                    Columns.Add(col);
                }
                else
                {
                    var SourceType = obj.GetType();
                    var column = 0;

                    var props = SourceType.GetProperties();
                    Array transTemp0 = props;
                    if (transTemp0.GetUpperBound(0) >= 0)
                    {
                        Array transTemp1 = props;
                        for (column = 0; column <= transTemp1.GetUpperBound(0); column++)
                            Columns.Add(props[column].Name);
                    }

                    var fields = SourceType.GetFields();
                    Array transTemp2 = fields;
                    if (transTemp2.GetUpperBound(0) >= 0)
                    {
                        Array transTemp3 = fields;
                        for (column = 0; column <= transTemp3.GetUpperBound(0); column++)
                            Columns.Add(fields[column].Name);
                    }

                    Columns.SetEvenSpacing(650);
                }
            }
        }

        #endregion

        #region '" Do printing "' 

        public int mRow;

        private void ReportDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            mPageNumber = 0;
            mRow = 0;
        }


        private void ReportDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            mPageNumber += 1;

            var page = new ReportPageEventArgs(e, mPageNumber, Font, Brush, FooterLines);

            if (mPageNumber == 1)
                if (null != ReportBegin)
                    ReportBegin(this, page);

            if (Columns.Count > 0)
            {
                var space = Convert.ToInt32(e.MarginBounds.Width / Columns.Count);
                var index = 0;
                for (index = 0; index <= Columns.Count - 1; index++) Columns[index].Width = space;
            }

            GeneratePage(page);

            if (!page.HasMorePages)
                if (null != ReportEnd)
                    ReportEnd(this, page);

            e.Cancel = page.Cancel;
            e.HasMorePages = page.HasMorePages;
        }


        private void GeneratePage(ReportPageEventArgs e)
        {
            var InnerSource = InnerDataSource();
            var Field = 0;

            if (null != PrintPageBegin) PrintPageBegin(this, e);

            if (!SupressDefaultHeader) PrintHeader(e);

            if (null != PrintPageBodyStart) PrintPageBodyStart(this, e);

            if (!(mDataSource == null) && Columns.Count > 0)
            {
                while (!e.EndOfPage && mRow < InnerSource.Count)
                {
                    for (Field = 0; Field <= Columns.Count - 1; Field++)
                        e.WriteColumn(GetField(InnerSource[mRow], Columns[Field].Field), Columns[Field]);
                    e.WriteLine();
                    mRow += 1;
                }

                e.HasMorePages = mRow < InnerSource.Count;
            }

            if (null != PrintPageBodyEnd) PrintPageBodyEnd(this, e);

            if (!SupressDefaultFooter) PrintFooter(e);

            if (null != PrintPageEnd) PrintPageEnd(this, e);
        }


        private void PrintHeader(ReportPageEventArgs e)
        {
            var field = 0;

            var transTemp4 = e;
            transTemp4.WriteLine(Title, ReportLineJustification.Centered);
            transTemp4.Write(SubTitleLeft);
            transTemp4.WriteLine(SubTitleRight, ReportLineJustification.Right);

            if (Columns.Count > 0)
            {
                for (field = 0; field <= Columns.Count - 1; field++)
                    transTemp4.WriteColumn(Columns[field].Name, Columns[field]);
                transTemp4.WriteLine();
            }

            transTemp4.HorizontalRule();
        }


        private void PrintFooter(ReportPageEventArgs e)
        {
            var transTemp5 = e;
            transTemp5.CurrentY = e.PageBottom;

            transTemp5.HorizontalRule();

            if (FooterLeft.Length > 0)
                transTemp5.Write(FooterLeft);
            else
                transTemp5.Write(DateTimeUtil.ShortDate(System.DateTime.Now));

            if (FooterRight.Length > 0)
                transTemp5.WriteLine(FooterRight);
            else
                transTemp5.WriteLine("Page " + e.PageNumber, ReportLineJustification.Right);
        }

        #endregion

        // events handled by ReportDocument_BeginPrint
        /* TRANSERROR: move EventHandler assignment(s) to appropriate scope */
        // base.BeginPrint += new System.Drawing.Printing.PrintEventHandler( ReportDocument_BeginPrint ); 
        // events handled by ReportDocument_PrintPage
        /* TRANSERROR: move EventHandler assignment(s) to appropriate scope */
        // base.PrintPage += new System.Drawing.Printing.PrintPageEventHandler( ReportDocument_PrintPage ); 
    }


    public enum ReportLineJustification
    {
        Left = 0,
        Centered = 1,
        Right = 2
    }


    public class ReportPageEventArgs : PrintPageEventArgs
    {
        private readonly Brush mBrush;
        private readonly Font mFont;
        private readonly int mFooterLines;
        private readonly int mLineHeight;
        private readonly int mPageBottom;
        private int mX;

        internal ReportPageEventArgs(PrintPageEventArgs e, int pageNumber, Font font, Brush brush, int footerLines)
            : base(e.Graphics, e.MarginBounds, e.PageBounds, e.PageSettings)
        {
            PageNumber = pageNumber;
            mFont = font;
            mBrush = brush;
            PositionToStart();
            mFooterLines = footerLines;

            mLineHeight = Convert.ToInt32(mFont.GetHeight(Graphics));
            mPageBottom = MarginBounds.Bottom - mFooterLines * mLineHeight - mLineHeight;
        }

        public int CurrentX
        {
            get { return mX; }
            set { CurrentY = value; }
        }

        public int CurrentY { get; set; }

        public int PageBottom => mPageBottom + mLineHeight;

        public bool EndOfPage => CurrentY >= mPageBottom;

        public int PageNumber { get; }

        public void Write(string Text)
        {
            Graphics.DrawString(Text, mFont, mBrush, mX, CurrentY);
            mX += Convert.ToInt32(Graphics.MeasureString(Text, mFont).Width);
        }


        public void Write(string Text, ReportLineJustification Justification)
        {
            switch (Justification)
            {
                case ReportLineJustification.Left:
                    mX = MarginBounds.Left;

                    break;
                case ReportLineJustification.Centered:
                    mX = MarginBounds.Left +
                         Convert.ToInt32(MarginBounds.Width / 2 - Graphics.MeasureString(Text, mFont).Width / 2);

                    break;
                case ReportLineJustification.Right:
                    mX = Convert.ToInt32(MarginBounds.Right - Graphics.MeasureString(Text, mFont).Width);

                    break;
            }

            Write(Text);
        }


        public void WriteColumn(string Text, ReportColumn column)
        {
            var x = MarginBounds.Left + column.Left;
            Graphics.FillRectangle(Brushes.White, new Rectangle(x - 5, CurrentY, column.Width + 5, mLineHeight));
            Graphics.DrawString(Text, mFont, mBrush, x, CurrentY);
        }


        public void WriteLine()
        {
            mX = MarginBounds.Left;
            CurrentY += mLineHeight;
        }


        public void WriteLine(string Text)
        {
            Graphics.DrawString(Text, mFont, mBrush, mX, CurrentY);
            WriteLine();
        }


        public void WriteLine(string Text, ReportLineJustification Justification)
        {
            switch (Justification)
            {
                case ReportLineJustification.Left:
                    mX = MarginBounds.Left;

                    break;
                case ReportLineJustification.Centered:
                    mX = MarginBounds.Left +
                         Convert.ToInt32(MarginBounds.Width / 2 - Graphics.MeasureString(Text, mFont).Width / 2);

                    break;
                case ReportLineJustification.Right:
                    mX = Convert.ToInt32(MarginBounds.Right - Graphics.MeasureString(Text, mFont).Width);

                    break;
            }

            Write(Text);
            WriteLine();
        }


        public void HorizontalRule()
        {
            var y = CurrentY + Convert.ToInt32(mLineHeight / 2);

            Graphics.DrawLine(Pens.Black, MarginBounds.Left, y, MarginBounds.Right, y);
            WriteLine();
        }


        public void PositionToStart()
        {
            mX = MarginBounds.Left;
            CurrentY = MarginBounds.Top;
        }
    }


    public class ReportColumn
    {
        public string Field;
        public int Left;
        public string Name;
        internal int Width;
    }


    public class ReportColumnCollection : CollectionBase
    {
        public ReportColumn this[int index] => (ReportColumn) List[index];

        public void Add(ReportColumn column)
        {
            List.Add(column);
        }


        public void Add(string Field)
        {
            var col = new ReportColumn();
            col.Name = Field;
            col.Field = Field;
            Add(col);
        }


        public void Add(string Field, int Left)
        {
            var col = new ReportColumn();
            col.Name = Field;
            col.Field = Field;
            col.Left = Left;
            Add(col);
        }


        public void Add(string Name, string Field, int Left)
        {
            var col = new ReportColumn();
            col.Name = Name;
            col.Field = Field;
            col.Left = Left;
            Add(col);
        }


        public void Remove(ReportColumn column)
        {
            List.Remove(column);
        }


        internal void SetEvenSpacing(int Width)
        {
            var space = Convert.ToInt32(Width / List.Count);
            var index = 0;

            for (index = 0; index <= List.Count - 1; index++)
            {
                var rc = (ReportColumn) List[index];
                rc.Left = space * index;
                rc.Width = space;
            }
        }
    }
}