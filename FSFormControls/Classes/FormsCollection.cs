#region

using System.Collections;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class FormsCollection : CollectionBase
    {
        public DBForm this[int index]
        {
            get { return (DBForm)List[index]; }
            set { List[index] = value; }
        }

        public void Add(DBForm form)
        {
            List.Add(form);
        }

        public void Remove(DBForm form)
        {
            List.Remove(form);
        }

        public void CloseAll()
        {
            foreach (DBForm f in Global.Forms) 
                f.Close();
        }

        public void RemoveAll()
        {
            foreach (DBForm f in Global.Forms) List.Remove(f);
        }

        public DBForm Find(DBForm form)
        {
            foreach (DBForm entry in List)
                if (entry == form)
                    return entry;
            return null;
        }

        public DBForm Find(string name)
        {
            foreach (DBForm f in Global.Forms)
                if (f.Name.ToLower() == name.ToLower())
                    return f;
            return null;
        }

        public bool Exist(string name)
        {
            foreach (DBForm f in Global.Forms)
                if (f.Name == name)
                    return true;
            return false;
        }

        public bool Exist(DBForm dbform)
        {
            foreach (DBForm f in Global.Forms)
                if (f.Name == dbform.Name)
                    return true;
            return false;
        }
    }
}