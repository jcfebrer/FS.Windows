using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using FSLibrary;

namespace FSFormControls
{
    public class DBComboExValues : CollectionBase, IBindingList
    {
        private readonly ComboBox m_combobox;
        private ListChangedEventHandler onListChanged;

        public DBComboExValues(ComboBox combobox)
        {
            m_combobox = combobox;
        }

        public DBComboExBoxItem this[int index]
        {
            get { return (DBComboExBoxItem) List[index]; }
            set { List[index] = value; }
        }

        public DBComboExBoxItem this[string name] => Find(name);

        public IList ValueList => List;

        public bool AllowEdit => false;

        public bool AllowNew => false;

        public bool AllowRemove => false;

        public bool IsSorted => false;

        public ListSortDirection SortDirection => 0;

        public PropertyDescriptor SortProperty => null;

        public bool SupportsChangeNotification => false;

        public bool SupportsSearching => false;

        public bool SupportsSorting => false;

        public object AddNew()
        {
            throw new NotImplementedException();
        }

        public void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotImplementedException();
        }

        public int Find(PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void RemoveSort()
        {
            throw new NotImplementedException();
        }

        public DBComboExBoxItem Find(string fieldName)
        {
            foreach (DBComboExBoxItem dbcol in List)
                if (dbcol.Text.ToLower() == fieldName.ToLower())
                    return dbcol;
            return null;
        }


        public int GetColumnOrdinal(string fieldName)
        {
            var f = 0;

            if (fieldName == "") return -1;

            if (fieldName.Substring(0, 1) == "_") fieldName = TextUtil.Replace(fieldName, "_", "");

            foreach (DBComboExBoxItem dbcol in List)
            {
                if (dbcol.Text.ToLower() == fieldName.ToLower()) return f;
                f = f + 1;
            }

            return -1;
        }


        public DBComboExBoxItem FindByValue(string value)
        {
            foreach (DBComboExBoxItem dbcol in List)
                if (Functions.Value(dbcol.Value).ToLower() == value.ToLower())
                    return dbcol;
            return null;
        }


        public void Add(string value, string text)
        {
            List.Add(new DBComboExBoxItem(value, text));
            m_combobox.Items.Add(new DBComboExBoxItem(value, text));
        }

        public void Add(int value, string text)
        {
            List.Add(new DBComboExBoxItem(value, text));
            m_combobox.Items.Add(new DBComboExBoxItem(value, text));
        }

        public void Add(decimal value, string text)
        {
            List.Add(new DBComboExBoxItem(value, text));
            m_combobox.Items.Add(new DBComboExBoxItem(value, text));
        }

        public DBComboExBoxItem Add(DBComboExBoxItem Value)
        {
            List.Add(Value);
            m_combobox.Items.Add(Value);

            return Value;
        }

        public DBRadioButton Add(DBRadioButton Value)
        {
            List.Add(Value);
            m_combobox.Items.Add(Value);

            return Value;
        }


        public void AddRange(DBComboExBoxItem[] Values)
        {
            var f = 0;
            for (f = 0; f <= Values.Length - 1; f++)
            {
                List.Add(Values[f]);
                m_combobox.Items.Add(Values[f]);
            }
        }


        public void Remove(DBComboExBoxItem Value)
        {
            List.Remove(Value);
            m_combobox.Items.Remove(Value);
        }


        public void Insert(int index, DBComboExBoxItem Value)
        {
            List.Insert(index, Value);
            m_combobox.Items.Insert(index, Value);
        }


        public bool Contains(DBComboExBoxItem Value)
        {
            return List.Contains(Value);
        }

        public bool Contains(string Value)
        {
            return List.Contains(Value);
        }

        public bool Contains(object Value)
        {
            return List.Contains(Value);
        }

        public int IndexOf(DBComboExBoxItem Value)
        {
            return List.IndexOf(Value);
        }

        #region IBindingList Members

        bool IBindingList.AllowEdit => AllowEdit;

        bool IBindingList.AllowNew => AllowNew;

        bool IBindingList.AllowRemove => AllowRemove;

        bool IBindingList.IsSorted => IsSorted;


        public event ListChangedEventHandler ListChanged
        {
            add { onListChanged += value; }
            remove { onListChanged -= value; }
        }

        ListSortDirection IBindingList.SortDirection => SortDirection;

        PropertyDescriptor IBindingList.SortProperty => SortProperty;

        bool IBindingList.SupportsChangeNotification => SupportsChangeNotification;

        bool IBindingList.SupportsSearching => SupportsSearching;

        bool IBindingList.SupportsSorting => SupportsSorting;

        #endregion
    }

    public class DBComboExBoxItem
    {
        public DBComboExBoxItem(string value, string text)
        {
            Text = text;
            Value = value;
        }

        public DBComboExBoxItem(int value, string text)
        {
            Text = text;
            Value = value.ToString();
        }

        public DBComboExBoxItem(decimal value, string text)
        {
            Text = text;
            Value = value.ToString();
        }

        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}