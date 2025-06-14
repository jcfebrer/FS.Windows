using System.Collections;
using System.ComponentModel;
using FSLibrary;

namespace FSFormControls
{
    public class DBSummarieCollection : CollectionBase
    {
        private ListChangedEventHandler onListChanged;

        public DBSummarie this[int index]
        {
            get { return (DBSummarie) List[index]; }
            set { List[index] = value; }
        }

        public DBSummarie Add(string name, DBSummarie.SummarieType type, DBColumn dBColumn)
        {
            DBSummarie summarie = new DBSummarie(name, type, dBColumn);
            
            return Add(summarie);
        }

        public DBSummarie Add(DBSummarie Value)
        {
            List.Add(Value);

            return Value;
        }

        public void AddRange(DBSummarie[] Values)
        {
            for (int f = 0; f <= Values.Length - 1; f++) 
                List.Add(Values[f]);
        }


        public void Remove(DBSummarie Value)
        {
            List.Remove(Value);
        }


        public void Insert(int index, DBSummarie Value)
        {
            List.Insert(index, Value);
        }


        public bool Contains(DBSummarie Value)
        {
            return List.Contains(Value);
        }


        public int IndexOf(DBSummarie Value)
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
            foreach (DBSummarie dbcol in List)
                if (dbcol.Name.ToLower() == fieldName.ToLower())
                    return true;
            return false;
        }
    }
}