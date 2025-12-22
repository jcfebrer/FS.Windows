#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public partial class DBPlanner : DBUserControl
    {
        public string[] m_names;

        public DBPlanner()
        {
            m_names = new string[4];
            InitializeComponent();
            // events handled by DBPanel1_Scroll

            DbPanel1.Scroll += DBPanel1_Scroll;
            // events handled by DBPanel2_Scroll

            DbPanel2.Scroll += DBPanel2_Scroll;
        }

        #region '"Event Handling"' 

        public void Initialize()
        {
            RenderLeftHandPane2();
            RenderRightHandPane2();
        }

        #endregion

        private void DBPanel1_Scroll(object sender)
        {
            DbPanel2.AutoScrollPosition = new Point(-DbPanel1.AutoScrollPosition.X, -DbPanel1.AutoScrollPosition.Y);
        }


        private void DBPanel2_Scroll(object sender)
        {
            DbPanel1.AutoScrollPosition = new Point(-DbPanel2.AutoScrollPosition.X, -DbPanel2.AutoScrollPosition.Y);
        }

        #region '"Helper Classes"' 

        public class DBPlannerQuarterHelper
        {
            public DBPlannerQuarterHelper()
            {
            }

            public DBPlannerQuarterHelper(int year, int quarter)
            {
                QuarterIndex = quarter;
                Year = year;
                Names = getQuarterNames();
                Days = getDaysInQuarter(Year, QuarterIndex);
            }

            public int Year { get; }

            public int QuarterIndex { get; }

            public string[] Names { get; } = new string[4];

            public int Days { get; }

            public string GetMonthName(int i)
            {
                return Names[i - 1];
            }


            public int TotalDays()
            {
                var retval = 0;
                switch (QuarterIndex)
                {
                    case 1:
                        retval = DateTime.DaysInMonth(Year, 1);
                        retval += DateTime.DaysInMonth(Year, 2);
                        retval += DateTime.DaysInMonth(Year, 3);
                        break;
                    case 2:
                        retval = DateTime.DaysInMonth(Year, 4);
                        retval += DateTime.DaysInMonth(Year, 5);
                        retval += DateTime.DaysInMonth(Year, 6);
                        break;
                    case 3:
                        retval = DateTime.DaysInMonth(Year, 7);
                        retval += DateTime.DaysInMonth(Year, 8);
                        retval += DateTime.DaysInMonth(Year, 9);
                        break;
                    case 4:
                        retval = DateTime.DaysInMonth(Year, 10);
                        retval += DateTime.DaysInMonth(Year, 11);
                        retval += DateTime.DaysInMonth(Year, 12);
                        break;
                }

                return retval;
            }


            public int TotalDaysInMonth(int i)
            {
                return DateTime.DaysInMonth(Year, i);
            }


            public string GetDayName(int month, int day)
            {
                var retval = string.Empty;
                var d = new DateTime(Year, month, day);
                switch (d.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        retval = "L";
                        break;
                    case DayOfWeek.Tuesday:
                        retval = "M";
                        break;
                    case DayOfWeek.Wednesday:
                        retval = "M";
                        break;
                    case DayOfWeek.Thursday:
                        retval = "J";
                        break;
                    case DayOfWeek.Friday:
                        retval = "V";
                        break;
                    case DayOfWeek.Saturday:
                        retval = "S";
                        break;
                    case DayOfWeek.Sunday:
                        retval = "D";
                        break;
                }

                return retval;
            }


            private string[] getQuarterNames()
            {
                var retval = new string[4];

                switch (QuarterIndex)
                {
                    case 1:
                        retval[0] = "Enero";
                        retval[1] = "Febrero";
                        retval[2] = "Marzo";
                        break;
                    case 2:
                        retval[0] = "Abril";
                        retval[1] = "Mayo";
                        retval[2] = "Junio";
                        break;
                    case 3:
                        retval[0] = "Julio";
                        retval[1] = "Agosto";
                        retval[2] = "Septiembre";
                        break;
                    case 4:
                        retval[0] = "Octubre";
                        retval[1] = "Noviembre";
                        retval[2] = "Diciembre";
                        break;
                }


                return retval;
            }


            public int getQuarter(int month)
            {
                switch (month)
                {
                    case 1:
                    case 2:
                    case 3:
                        return 1;
                    case 4:
                    case 5:
                    case 6:
                        return 2;
                    case 7:
                    case 8:
                    case 9:
                        return 3;
                    case 10:
                    case 11:
                    case 12:
                        return 4;
                }

                return 0;
            }


            private int getDaysInQuarter(int year, int quarter)
            {
                var dtS = DateTime.MinValue;
                var dtE = DateTime.MinValue;
                if (quarter < 4)
                {
                    dtS = new DateTime(year, 3 * quarter - 2, 1);
                    dtE = new DateTime(year, 3 * quarter - 2 + 3, 1);
                }
                else
                {
                    dtS = new DateTime(year, 3 * quarter - 2, 1);
                    dtE = new DateTime(year + 1, 1, 1);
                }

                var ts = new TimeSpan(dtE.Subtract(dtS).Ticks);
                return ts.Days;
            }


            public int getColumnIndex(string day)
            {
                var dt = DateTime.Parse(day);
                var offset = 0;
                var retval = 0;
                var i = 0;
                for (i = 1; i <= QuarterIndex - 1; i += i + 1) offset += getDaysInQuarter(Year, i);
                retval = dt.DayOfYear - 1 - offset;
                if (retval < 0) retval = 0;
                if (retval > getDaysInQuarter(Year, QuarterIndex)) retval = getDaysInQuarter(Year, QuarterIndex);
                return retval;
            }
        }

        #endregion

        #region Nested type: DBPlannerBlock

        public class DBPlannerBlock
        {
            public DBPlannerBlock(string name, Color color, DBPlannerDatesCollection dates)
            {
                Name = name;
                Color = color;
                Dates = dates;
            }

            public string Name { get; set; }

            public Color Color { get; set; }

            public DBPlannerDatesCollection Dates { get; set; }
        }

        #endregion

        #region Nested type: DBPlannerBlockCollection

        public class DBPlannerBlockCollection : CollectionBase
        {
            public void Add(string name, Color color, DBPlannerDatesCollection dates)
            {
                List.Add(new DBPlannerBlock(name, color, dates));
            }
        }

        #endregion

        #region Nested type: DBPlannerDates

        public class DBPlannerDates
        {
            public DBPlannerDates(DateTime startDate, DateTime endDate, string name)
            {
                this.startDate = startDate;
                this.endDate = endDate;
                Name = name;
            }

            public DateTime startDate { get; set; }

            public DateTime endDate { get; set; }

            public string Name { get; set; }
        }

        #endregion

        #region Nested type: DBPlannerDatesCollection

        public class DBPlannerDatesCollection : CollectionBase
        {
            public void Add(DateTime startDate, DateTime endDate, string name)
            {
                List.Add(new DBPlannerDates(startDate, endDate, name));
            }


            public DBPlannerDates get_Item(int index)
            {
                return (DBPlannerDates) List[index];
            }


            public void set_Item(int index, DBPlannerDates value)
            {
                List[index] = value;
            }
        }

        #endregion

        #region Nested type: DBPlannerGroup

        public class DBPlannerGroup
        {
            public DBPlannerGroup(string name, Color color, DBPlannerBlockCollection block)
            {
                Name = name;
                Color = color;
                Block = block;
            }

            public string Name { get; set; }

            public Color Color { get; set; }

            public DBPlannerBlockCollection Block { get; set; }
        }

        #endregion

        #region Nested type: DBPlannerGroupCollection

        public class DBPlannerGroupCollection : CollectionBase
        {
            public void Add(string name, Color color, DBPlannerBlockCollection block)
            {
                List.Add(new DBPlannerGroup(name, color, block));
            }
        }

        #endregion

        #region '"Public Properties"' 

        public DBPlannerGroupCollection Group { get; set; } = new DBPlannerGroupCollection();

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string XMLData { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(1)]
        public int Quarter { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public int Year { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("red")]
        public string BlockColor { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(15)]
        public int CellWidth { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(15)]
        public int CellHeight { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("#dcdcdc")]
        public string ToggleColor { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        #endregion

        #region '"Helper Functions"' 

        private void RenderLeftHandPane()
        {
            string blocktext = null;
            string grouptext = null;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(XMLData);

            var xmlRows = xmlDoc.SelectNodes("//group");
            var f = 0;
            foreach (XmlNode xmlRow in xmlRows)
            {
                grouptext = xmlRow.SelectSingleNode("name").InnerText;

                var dbpp = new DBPlannerObject();
                dbpp.BackColor = Color.Gainsboro;
                dbpp.Top = CellHeight * 3 + f * CellHeight;
                dbpp.Left = 0;
                dbpp.Width = DbPanel1.Width;
                dbpp.Height = CellHeight;
                dbpp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                dbpp.Blocks.Add("prin" + f, grouptext, 1, Color.White, new Font("Arial", 8), Color.Black,
                    HorizontalAlignment.Left);

                dbpp.Initialize();

                DbPanel1.Controls.Add(dbpp);

                var xmlBlocks = xmlRow.SelectNodes("block");

                f = f + 1;

                foreach (XmlNode xmlBlock in xmlBlocks)
                {
                    blocktext = xmlBlock.SelectSingleNode("name").InnerText;

                    var dbppb = new DBPlannerObject();
                    dbppb.BackColor = Color.White;
                    dbppb.Top = CellHeight * 3 + f * CellHeight;
                    dbppb.Width = DbPanel1.Width;
                    dbppb.Height = CellHeight;
                    dbppb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    dbppb.Blocks.Add("block" + f, "     -" + blocktext, 1, Color.White, new Font("Arial", 8),
                        Color.Blue,
                        HorizontalAlignment.Left);

                    dbppb.Initialize();

                    DbPanel1.Controls.Add(dbppb);

                    f = f + 1;
                }
            }
        }


        private void RenderRightHandPane()
        {
            string startdate = null;
            string enddate = null;
            string dayname = null;
            string href = null;
            string blocktext = null;
            string blockcolor = null;
            var startindex = 0;
            var endindex = 0;
            var imagewidth = 0;
            var month = 0;
            var week = false;
            var quarter = new DBPlannerQuarterHelper(Year, Quarter);
            var dbpDias = new DBPlannerObject();
            var dbpTitulo = new DBPlannerObject();

            dbpTitulo.Top = 0;
            dbpDias.Top = CellHeight;
            dbpTitulo.Height = CellHeight;
            dbpDias.Height = CellHeight;

            dbpDias.FixedSize = true;
            dbpTitulo.FixedSize = true;

            var i = 0;
            for (i = 1; i <= 3; i++)
            {
                month = i + 3 * (quarter.QuarterIndex - 1);
                dbpTitulo.Blocks.Add("mes" + i, quarter.GetMonthName(i), CellWidth * quarter.TotalDaysInMonth(month),
                    Color.White, new Font("Arial", 8), Color.Blue, HorizontalAlignment.Center);
            }

            for (i = 3 * quarter.QuarterIndex - 2; i <= 3 * quarter.QuarterIndex; i++)
            {
                var j = 0;
                for (j = 1; j <= quarter.TotalDaysInMonth(i); j++)
                {
                    dayname = quarter.GetDayName(i, j);
                    if (dayname == "L") week = !week;

                    if (week)
                        dbpDias.Blocks.Add("dia" + i + j, dayname, CellWidth, Color.White, new Font("Arial", 6),
                            Color.Red, HorizontalAlignment.Center);
                    else
                        dbpDias.Blocks.Add("dia" + i + j, dayname, CellWidth, Color.White, new Font("Arial", 6),
                            Color.Green, HorizontalAlignment.Center);
                }
            }

            dbpDias.Initialize();

            dbpTitulo.Initialize();

            DbPanel2.Controls.Add(dbpDias);
            DbPanel2.Controls.Add(dbpTitulo);


            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(XMLData);

            var xmlRows = xmlDoc.SelectNodes("//group");

            var f = 0;
            var g = 0;

            foreach (XmlNode xmlRow in xmlRows)
            {
                var node = xmlRow.SelectSingleNode("blockcolor");
                if (!(node == null))
                    blockcolor = node.InnerText;
                else
                    blockcolor = BlockColor;


                var xmlBlocks = xmlRow.SelectNodes("block");

                f = f + 1;

                foreach (XmlNode xmlBlock in xmlBlocks)
                {
                    var dbpp = new DBPlannerObject();
                    dbpp.FixedSize = true;
                    dbpp.Top = CellHeight * 3 + f * CellHeight;
                    dbpp.Left = 0;
                    dbpp.Height = CellHeight;
                    dbpp.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                    startdate = xmlBlock.SelectSingleNode("StartDate").InnerText;
                    enddate = xmlBlock.SelectSingleNode("EndDate").InnerText;
                    href = xmlBlock.SelectSingleNode("href").InnerText;
                    blocktext = xmlBlock.SelectSingleNode("name").InnerText;

                    startindex = quarter.getColumnIndex(startdate);
                    endindex = quarter.getColumnIndex(enddate);

                    dbpp.Blocks.Add("barspace" + f + g, CellWidth * startindex, Color.White);

                    if (!string.IsNullOrEmpty(href))
                    {
                        imagewidth = endindex - startindex + 1;

                        for (g = 1; g <= imagewidth; g++)
                            dbpp.Blocks.Add("bar" + f + g, CellWidth, Color.FromName(blockcolor));
                    }

                    dbpp.Blocks.Add("barend" + f + g, (quarter.Days - endindex - 1) * CellWidth, Color.White);

                    f = f + 1;
                    dbpp.Initialize();

                    DbPanel2.Controls.Add(dbpp);
                }
            }
        }


        private void RenderLeftHandPane2()
        {
            string blocktext = null;
            string grouptext = null;

            var f = 0;
            foreach (DBPlannerGroup grp in Group)
            {
                grouptext = grp.Name;

                var dbpp = new DBPlannerObject();
                dbpp.BackColor = Color.Gainsboro;
                dbpp.Top = CellHeight * 1 + f * CellHeight;
                dbpp.Left = 0;
                dbpp.Width = DbPanel1.Width;
                dbpp.Height = CellHeight;
                dbpp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                dbpp.Blocks.Add("prin" + f, grouptext, 1, Color.White, new Font("Arial", 8), Color.Black,
                    HorizontalAlignment.Left);

                dbpp.Initialize();

                DbPanel1.Controls.Add(dbpp);


                f = f + 1;

                foreach (DBPlannerBlock blk in grp.Block)
                {
                    blocktext = blk.Name;

                    var dbppb = new DBPlannerObject();
                    dbppb.BackColor = Color.White;
                    dbppb.Top = CellHeight * 1 + f * CellHeight;
                    dbppb.Width = DbPanel1.Width;
                    dbppb.Height = CellHeight;
                    dbppb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    dbppb.Blocks.Add("block" + f, "     -" + blocktext, 1, Color.White, new Font("Arial", 8),
                        Color.Blue,
                        HorizontalAlignment.Left);

                    dbppb.Initialize();

                    DbPanel1.Controls.Add(dbppb);

                    f = f + 1;
                }
            }
        }


        private void RenderRightHandPane2()
        {
            string dayname = null;
            string blocktext = null;
            var blockcolor = new Color();
            var startindex = 0;
            var endindex = 0;
            var imagewidth = 0;
            var iMonth = 0;
            var iYear = 0;
            var week = false;
            var dbpDias = new DBPlannerObject();
            var dbpTitulo = new DBPlannerObject();

            dbpTitulo.Top = 0;
            dbpDias.Top = CellHeight;
            dbpTitulo.Height = CellHeight;
            dbpDias.Height = CellHeight;

            dbpDias.FixedSize = true;
            dbpTitulo.FixedSize = true;

            var months = Convert.ToInt32(FSLibrary.DateTimeUtil.DateDiffMonth(InitialDate, EndDate) + 1);

            var i = 0;
            for (i = 1; i <= months; i++)
            {
                iMonth = InitialDate.Month + i - 1;
                iYear = Year;
                if (iMonth > 12)
                {
                    iMonth = iMonth - 12;
                    iYear = iYear + 1;
                }

                dbpTitulo.Blocks.Add("mes" + i, FSLibrary.DateTimeUtil.MonthName(iMonth),
                    CellWidth * DateTime.DaysInMonth(iYear, iMonth), Color.White, new Font("Arial", 8),
                    Color.Blue, HorizontalAlignment.Center);
            }

            for (i = 1; i <= months; i++)
            {
                iMonth = InitialDate.Month + i - 1;
                iYear = Year;
                if (iMonth > 12)
                {
                    iMonth = iMonth - 12;
                    iYear = iYear + 1;
                }

                var j = 0;
                for (j = 1; j <= DateTime.DaysInMonth(iYear, iMonth); j++)
                {
                    dayname = FSLibrary.DateTimeUtil.GetFirstDayName(iYear, iMonth, j);
                    if (dayname == "L") week = !week;

                    if (week)
                        dbpDias.Blocks.Add("dia" + i + j, Convert.ToString(j), CellWidth, Color.White,
                            new Font("Arial", 6), Color.Red, HorizontalAlignment.Center);
                    else
                        dbpDias.Blocks.Add("dia" + i + j, Convert.ToString(j), CellWidth, Color.White,
                            new Font("Arial", 6), Color.Green, HorizontalAlignment.Center);
                }
            }

            dbpDias.Initialize();

            dbpTitulo.Initialize();

            DbPanel2.Controls.Add(dbpDias);
            DbPanel2.Controls.Add(dbpTitulo);


            var f = 0;
            var g = 0;

            foreach (DBPlannerGroup grp in Group)
            {
                blockcolor = grp.Color;


                f = f + 1;

                foreach (DBPlannerBlock blk in grp.Block)
                {
                    var dbpp = new DBPlannerObject();
                    dbpp.BackColor = Color.White;
                    dbpp.FixedSize = true;
                    dbpp.Top = CellHeight * 1 + f * CellHeight;
                    dbpp.Left = 0;
                    dbpp.Height = CellHeight;
                    dbpp.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                    var blIn = 0;

                    foreach (DBPlannerDates d in blk.Dates)
                    {
                        blocktext = blk.Name;

                        if (d.startDate < InitialDate)
                            startindex = 0;
                        else
                            startindex = Convert.ToInt32(FSLibrary.DateTimeUtil.TotalDays(InitialDate, d.startDate));


                        if (d.endDate > EndDate)
                            endindex = Convert.ToInt32(FSLibrary.DateTimeUtil.TotalDays(InitialDate, EndDate));
                        else
                            endindex = Convert.ToInt32(FSLibrary.DateTimeUtil.TotalDays(InitialDate, d.endDate));

                        dbpp.Blocks.Add("barspace" + f + g + "(" + (startindex - blIn) + ")",
                            CellWidth * (startindex - blIn), Color.White);

                        imagewidth = endindex - startindex + 1;

                        dbpp.Blocks.Add(d.Name + f + g,
                            d.Name + "(" + imagewidth + ") - " + d.startDate.Day + "/" + d.startDate.Month +
                            "-" + d.endDate.Day + "/" + d.endDate.Month, imagewidth * CellWidth, blockcolor,
                            new Font("Arial", 6), Color.Black, HorizontalAlignment.Left, true);

                        g = g + 1;

                        blIn = blIn + imagewidth + (startindex - blIn);
                    }

                    dbpp.Blocks.Add("barend" + f + g,
                        (int) (FSLibrary.DateTimeUtil.TotalDays(EndDate, InitialDate) - blIn + 1) * CellWidth,
                        Color.White);

                    f = f + 1;
                    dbpp.Initialize();

                    DbPanel2.Controls.Add(dbpp);
                }
            }
        }

        #endregion
    }
}