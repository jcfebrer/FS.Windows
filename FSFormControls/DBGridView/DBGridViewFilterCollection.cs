using System.Collections;
using System.ComponentModel;
using FSLibrary;

namespace FSFormControls
{
    public class DBGridViewFilterCollection : CollectionBase, IBindingList
    {
        private ListChangedEventHandler onListChanged;

        public DBGridViewFilter this[int index]
        {
            get { return (DBGridViewFilter) List[index]; }
            set { List[index] = value; }
        }

        public DBGridViewFilter this[string name] => get_Find(name);

        public bool AllowEdit => false;

        public bool AllowNew => false;

        public bool AllowRemove => false;

        public bool IsSorted => false;

        public ListSortDirection SortDirection => 0;

        public PropertyDescriptor SortProperty => null;

        public bool SupportsChangeNotification => false;

        public bool SupportsSearching => false;

        public bool SupportsSorting => false;

        public DBGridViewFilter get_Find(string name)
        {
            foreach (DBGridViewFilter DBGridViewFilter in List)
                if (DBGridViewFilter.Name.ToLower() == name.ToLower())
                    return DBGridViewFilter;
            return null;
        }

        public int get_GetColumnOrdinal(string name)
        {
            var f = 0;

            if (name == "") return -1;

            if (name.Substring(0, 1) == "_") name = TextUtil.Replace(name, "_", "");

            foreach (DBGridViewFilter dbcol in List)
            {
                if (dbcol.Name.ToLower() == name.ToLower()) return f;
                f = f + 1;
            }

            return -1;
        }

        public DBGridViewFilter Add(DBGridViewFilter Value)
        {
            List.Add(Value);

            return Value;
        }

        public void AddRange(DBGridViewFilter[] Values)
        {
            var f = 0;
            for (f = 0; f <= Values.Length - 1; f++) List.Add(Values[f]);
        }


        public void Remove(DBGridViewFilter Value)
        {
            List.Remove(Value);
        }


        public void Insert(int index, DBGridViewFilter Value)
        {
            List.Insert(index, Value);
        }


        public bool Contains(DBGridViewFilter Value)
        {
            return List.Contains(Value);
        }


        public int IndexOf(DBGridViewFilter Value)
        {
            return List.IndexOf(Value);
        }


        public void AddIndex(PropertyDescriptor property)
        {
        }

        // interface methods implemented by AddIndex


        public object AddNew()
        {
            return null;
        }

        // interface methods implemented by AddNew


        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
        }

        // interface methods implemented by ApplySort


        public int Find1(PropertyDescriptor property, object key)
        {
            return 0;
        }

        // interface methods implemented by Find1


        public void RemoveIndex(PropertyDescriptor property)
        {
        }

        // interface methods implemented by RemoveIndex


        public void RemoveSort()
        {
        }

        public void ExpandAll(bool v)
        {
        }

        #region IBindingList Members

        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            AddIndex(property);
        }

        object IBindingList.AddNew()
        {
            return AddNew();
        }

        bool IBindingList.AllowEdit => AllowEdit;

        bool IBindingList.AllowNew => AllowNew;

        bool IBindingList.AllowRemove => AllowRemove;

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            ApplySort(property, direction);
        }

        int IBindingList.Find(PropertyDescriptor property, object key)
        {
            return Find1(property, key);
        }

        bool IBindingList.IsSorted => IsSorted;


        public event ListChangedEventHandler ListChanged
        {
            add { onListChanged += value; }
            remove { onListChanged -= value; }
        }

        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            RemoveIndex(property);
        }

        void IBindingList.RemoveSort()
        {
            RemoveSort();
        }

        ListSortDirection IBindingList.SortDirection => SortDirection;

        PropertyDescriptor IBindingList.SortProperty => SortProperty;

        bool IBindingList.SupportsChangeNotification => SupportsChangeNotification;

        bool IBindingList.SupportsSearching => SupportsSearching;

        bool IBindingList.SupportsSorting => SupportsSorting;

        public DBColumnCollection ColumnFilters { get; set; }

        #endregion

        // interface methods implemented by RemoveSort
    }
}