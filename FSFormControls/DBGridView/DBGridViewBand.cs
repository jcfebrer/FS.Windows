namespace FSFormControls
{
    public class DBGridViewBand
    {
        public string Name { get; set; }
        public DBColumnCollection Columns { get; set; }
        public DBColumnCollection SortedColumns { get; set; }
        public bool Hidden { get; set; }
    }
}