using System.Collections.Generic;
using System.ComponentModel;

namespace FSFormControls
{
    public class DBGridViewFilter
    {
        public List<DBGridViewFilter> FilterConditions { get; set; } = new List<DBGridViewFilter>();

        public string CompareValue { get; set; }
        public string Name { get; set; }

        public enum FilterComparisionOperator
        {
            StartsWith,
            EndWith,
            Contains
        }

        public enum FilterLogicalOperator
        {
            And,
            Or,
            Not
        }

        public FilterComparisionOperator ComparisionOperator { get; set; }
        public FilterLogicalOperator LogicalOperator { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DBGridViewFilterCollection DBGridViewFilters { get; set; }
    }
}