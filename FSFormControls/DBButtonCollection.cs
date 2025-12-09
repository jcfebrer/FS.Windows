using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using FSLibrary;

namespace FSFormControls
{
    public class DBButtonCollection : CollectionBase, IBindingList
    {
        private ListChangedEventHandler onListChanged;

        public Button this[int index]
        {
            get { return (Button) List[index]; }
            set { List[index] = value; }
        }

        public Button this[string key] => get_Find(key);

        public bool AllowEdit => false;

        public bool AllowNew => false;

        public bool AllowRemove => false;

        public bool IsSorted => false;

        public ListSortDirection SortDirection => 0;

        public PropertyDescriptor SortProperty => null;

        public bool SupportsChangeNotification => false;

        public bool SupportsSearching => false;

        public bool SupportsSorting => false;

        public Button get_Find(string key)
        {
            foreach (Button dbbutton in List)
                if (dbbutton.Name.ToLower() == key.ToLower())
                    return dbbutton;
            return null;
        }


        public int get_GetColumnOrdinal(string name)
        {
            var f = 0;

            if (name == "") return -1;

            if (name.Substring(0, 1) == "_") name = TextUtil.Replace(name, "_", "");

            foreach (Button dbcol in List)
            {
                if (dbcol.Name.ToLower() == name.ToLower()) return f;
                f = f + 1;
            }

            return -1;
        }

        public Button Add(Button Value)
        {
            List.Add(Value);

            return Value;
        }

        public Button Add(DBButton Value)
        {
            List.Add(Value);

            return Value;
        }

        public Button Add(DBButtonEx Value)
        {
            List.Add(Value);

            return Value.Button;
        }

        public void AddRange(Button[] Values)
        {
            var f = 0;
            for (f = 0; f <= Values.Length - 1; f++) List.Add(Values[f]);
        }


        public void Remove(Button Value)
        {
            List.Remove(Value);
        }


        public void Insert(int index, Button Value)
        {
            List.Insert(index, Value);
        }


        public bool Contains(Button Value)
        {
            return List.Contains(Value);
        }


        public int IndexOf(Button Value)
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