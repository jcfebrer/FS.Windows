#region

using System.Collections;

#endregion

namespace FSFormControls
{
    public class PageCollection : CollectionBase
    {
        public int ind;


        public DBPage this[int index]
        {
            get { return (DBPage) List[index]; }
            set { List[index] = value; }
        }

        public void Add(DBPage page)
        {
            page.Index = ind;
            ind += 1;
            List.Add(page);
        }

        public DBPage Find(string pageName)
        {
            foreach (DBPage pg in List)
                if (pg.Name.ToLower() == pageName.ToLower())
                    return pg;
            return null;
        }
    }
}