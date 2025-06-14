using System.Collections;
using System.ComponentModel;
using FSLibrary;

namespace FSFormControls
{
    public class DBColumnCollection : CollectionBase, IBindingList
    {
        private ListChangedEventHandler onListChanged;

        public DBColumn this[int index]
        {
            get { 
                if(List.Count > 0)
                    return (DBColumn) List[index];
                else
                    return null;
            }
            set { List[index] = value; }
        }

        public DBColumn this[string name] => Find(name);

        public bool AllowEdit => false;

        public bool AllowNew => false;

        public bool AllowRemove => false;

        public bool IsSorted => false;

        public ListSortDirection SortDirection => 0;

        public PropertyDescriptor SortProperty => null;

        public bool SupportsChangeNotification => false;

        public bool SupportsSearching => false;

        public bool SupportsSorting => false;

        public DBColumn Find(string fieldName)
        {
            foreach (DBColumn dbcol in List)
                if (dbcol.FieldDB.ToLower() == fieldName.ToLower())
                    return dbcol;
            return null;
        }


        public int GetColumnOrdinal(string fieldName)
        {
            var f = 0;

            if (string.IsNullOrEmpty(fieldName)) 
                return -1;

            if (fieldName.Substring(0, 1) == "_") fieldName = TextUtil.Replace(fieldName, "_", "");

            foreach (DBColumn dbcol in List)
            {
                if (dbcol.FieldDB.ToLower() == fieldName.ToLower())
                    return f;
                f = f + 1;
            }

            return -1;
        }


        public DBColumn FindByHeaderCaption(string headerCaption)
        {
            foreach (DBColumn dbcol in List)
                if (dbcol.HeaderCaption == headerCaption)
                    return dbcol;
            return null;
        }


        public void Add(string strFieldDB, string strHeaderCaption)
        {
            List.Add(new DBColumn(strFieldDB, strHeaderCaption));
        }

        public void Add(DBColumn column, bool descendent)
        {
            //TODO: Permitir ordenar columnas
            column.SortIndicator =
                descendent ? DBColumn.SortIndicatorEnum.Descending : DBColumn.SortIndicatorEnum.Ascending;
            List.Add(column);
        }

        public void Add(string fieldName, bool descendent)
        {
            var column = Find(fieldName);
            //TODO: Permitir ordenar columnas
            column.SortIndicator =
                descendent ? DBColumn.SortIndicatorEnum.Descending : DBColumn.SortIndicatorEnum.Ascending;
            List.Add(column);
        }


        public void Add(string strFieldDB, string strHeaderCaption, bool bolHidden)
        {
            List.Add(new DBColumn(strFieldDB, strHeaderCaption, bolHidden));
        }


        public void Add(string strFieldDB, string strHeaderCaption, DBControl dbcColumnDBControl)
        {
            List.Add(new DBColumn(strFieldDB, strHeaderCaption, dbcColumnDBControl));
        }


        public void Add(string strFieldDB, string strHeaderCaption, DBColumn.ColumnTypes tColumnType)
        {
            List.Add(new DBColumn(strFieldDB, strHeaderCaption, tColumnType));
        }


        public DBColumn Add(DBColumn Value)
        {
            List.Add(Value);

            return Value;
        }


        public void AddRange(DBColumn[] Values)
        {
            var f = 0;
            for (f = 0; f <= Values.Length - 1; f++) List.Add(Values[f]);
        }


        public void Remove(DBColumn Value)
        {
            List.Remove(Value);
        }


        public void Insert(int index, DBColumn Value)
        {
            List.Insert(index, Value);
        }


        public bool Contains(DBColumn Value)
        {
            return List.Contains(Value);
        }


        public int IndexOf(DBColumn Value)
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

        public bool Exists(string fieldName)
        {
            foreach (DBColumn dbcol in List)
                if (dbcol.FieldDB.ToLower() == fieldName.ToLower())
                    return true;
            return false;
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

        #endregion

        // interface methods implemented by RemoveSort
    }
}