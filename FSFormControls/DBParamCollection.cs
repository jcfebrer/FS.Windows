using System.Collections;
using System.ComponentModel;
using FSLibrary;

namespace FSFormControls
{
    public class DBParamCollection : CollectionBase, IBindingList
    {
        private ListChangedEventHandler onListChanged;

        public DBParam this[int index]
        {
            get { return (DBParam) List[index]; }
            set { List[index] = value; }
        }

        public DBParam this[string name] => Find(name);

        public DBParam Find(string paramName)
        {
            foreach (DBParam dbcol in List)
                if (dbcol.Name.ToLower() == paramName.ToLower())
                    return dbcol;
            return null;
        }

        public bool AllowEdit => false;

        public bool AllowNew => false;

        public bool AllowRemove => false;

        public bool IsSorted => false;

        public ListSortDirection SortDirection => 0;

        public PropertyDescriptor SortProperty => null;

        public bool SupportsChangeNotification => false;

        public bool SupportsSearching => false;

        public bool SupportsSorting => false;


        public int GetParamOrdinal(string paramName)
        {
            var f = 0;

            if (string.IsNullOrEmpty(paramName)) 
                return -1;

            foreach (DBParam dbcol in List)
            {
                if (dbcol.Name.ToLower() == paramName.ToLower())
                    return f;
                f = f + 1;
            }

            return -1;
        }


        public void Add(string name, object value)
        {
            int pos = GetParamOrdinal(name);
            if (pos != -1)
                ((DBParam)List[pos]).Value = value;
            else
                List.Add(new DBParam(name, value));
        }

        public void Set(string name, object value)
        {
            int pos = GetParamOrdinal(name);
            if (pos != -1)
                ((DBParam)List[pos]).Value = value;
            else
                throw new System.Exception("Parámetro inexistente. Debes añadirlo con Add.");
        }

        //public DBParam Add(DBParam param)
        //{
        //    int pos = GetParamOrdinal(param.Name);
        //    if (pos != -1)
        //        List[pos] = param.Value;
        //    else
        //        List.Add(param);

        //    return param;
        //}


        public void AddRange(DBParam[] parameters)
        {
            var f = 0;
            for (f = 0; f <= parameters.Length - 1; f++) 
                List.Add(parameters[f]);
        }


        public void Remove(DBParam param)
        {
            List.Remove(param);
        }


        public void Insert(int index, DBParam Value)
        {
            List.Insert(index, Value);
        }


        public bool Contains(DBParam Value)
        {
            return List.Contains(Value);
        }


        public int IndexOf(DBParam Value)
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

        public bool Exists(string name)
        {
            foreach (DBParam dbcol in List)
                if (dbcol.Name.ToLower() == name.ToLower())
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