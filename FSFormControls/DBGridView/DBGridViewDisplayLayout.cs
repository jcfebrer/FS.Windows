using System.Windows.Forms;

namespace FSFormControls
{
    /// <summary>
    /// Clase para aplicar stilos al DBGridView utilizada por Infragistics. Será eliminada en un futuro.
    /// </summary>
    public class DBGridViewDisplayLayout
    {
        public DBGridViewDisplayLayout()
        {
        }

        public enum DBCellClickAction
        {
            Default,
            Edit,
            RowSelect,
            CellSelect,
            EditAndSelectText
        }
        public enum DBHeaderClickAction
        {
            Default,
            Select,
            SortSingle,
            SortMulti,
            ExternalSortSingle,
            ExternalSortMulti
        }
        //public enum DBElementBorderStyle
        //{
        //    Default,
        //    None,
        //    FixedSingle,
        //    Fixed3D,
        //    Dotted,
        //    Dashed,
        //    Solid,
        //    Inset,
        //    Raised,
        //    InsetSoft,
        //    RaisedSoft,
        //    Etched,
        //    Rounded1,
        //    Rounded1Etched,
        //    Rounded4,
        //    Rounded4Thick,
        //    TwoColor,
        //    WindowsVista,
        //    Rounded3
        //}
        public enum DBHeaderStyle
        {
            Default,
            Standard,
            WindowsXPCommand,
            XPThemed,
            WindowsVista
        }
        public enum DBScrollBounds
        {
            ScrollToFill,
            ScrollToLastItem
        }
        public enum DBScrollStyle
        {
            Deferred,
            Immediate
        }
        public enum DBViewStyleBand
        {
            Vertical,
            Horizontal,
            OutlookGroupBy
        }
        public enum DBRowSizing
        {
            Default,
            Fixed,
            Free,
            Sychronized,
            AutoFixed,
            AutoFree
        }
        public enum DBRowSelectorNumberStyle
        {
            Default,
            None,
            ListIndex,
            RowIndex,
            VisibleIndex
        }
        public enum DBRowSelectorHeaderStyle
        {
            Default,
            None,
            ExtendFirstColumn,
            SeparateElement,
            ColumnChooserButton,
            ColumnChooserButtonFixedSize
        }
        public enum SelectType
        {
            Default,
            None,
            Single,
            Extended,
            ExtendedAutoDrag,
            SingleAutoDrag
        }
        public enum DBAllowColMoving
        {
            Default,
            NotAllowed,
            WithinGroup,
            WithinBand
        }
        public enum DBAllowColSwapping
        {
            Default,
            NotAllowed,
            WithinGroup,
            WithinBand
        }
        public enum DBTabNavigation
        {
            NextCell,
            NextControl,
            NextControlOnLastCell
        }
        public enum DBLoadStyle
        {             
            Default,
            LoadOnDemand,
            LoadOnDemandWithFullExpand
        }

        public DBAppearance Appearance { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public DBGridViewDisplayLayout GroupByBox { get; set; }
        public DBGridViewDisplayLayout Override { get; set; }
        public bool CaptionVisible { get; set; }
        public bool Hidden { get; set; }
        public bool RowSelectors { get; set; }
        public int MinRowHeight { get; set; }
        public int CellPadding { get; set; }
        public int MaxColScrollRegions { get; set; }
        public int MaxRowScrollRegions { get; set; }

        public DBAppearance ActiveRowAppearance { get; set; }
        public DBAppearance ActiveCellAppearance { get; set; }
        public DBAppearance BandLabelAppearance { get; set; }
        public DBAppearance CardAreaAppearance { get; set; }
        public DBAppearance CellAppearance { get; set; }
        public DBAppearance GroupByRowAppearance { get; set; }
        public DBAppearance HeaderAppearance { get; set; }
        public DBAppearance PromptAppearance { get;set; }
        public DBAppearance RowAppearance { get; set; }
        public DBAppearance TemplateAddRowAppearance { get; set; }

        public DBScrollBounds ScrollBounds { get; set; }
        public DBScrollStyle ScrollStyle { get; set; }
        public DBViewStyleBand ViewStyleBand { get; set; }
        public DBHeaderStyle HeaderStyle { get; set; }
        public DBHeaderClickAction HeaderClickAction { get; set; }
        public DBCellClickAction CellClickAction { get; set; }
        public BorderStyle BorderStyleCell { get; set; }
        public BorderStyle BorderStyleRow { get; set; }
        public DBLoadStyle LoadStyle { get; set; }
        public DBRowSizing RowSizing { get; set; }
        public SelectType SelectTypeRow { get; set; }
        public DBTabNavigation TabNavigation { get; set; }
        public DBHeaderStyle RowSelectorStyle { get; set; }
        public DBRowSelectorNumberStyle RowSelectorNumberStyle { get; set; }
        public DBRowSelectorHeaderStyle RowSelectorHeaderStyle { get; set; }
        public SelectType SelectTypeCol { get; set; }
        public DBAllowColMoving AllowColMoving { get; set; }
        public DBAllowColSwapping AllowColSwapping { get; set; }
        public bool AllowDelete { get; set; }
    }
}