#region

using System;
using System.Collections;

#endregion

namespace FSFormControls
{
    public class ErrorCollection : IEnumerable
    {
        private readonly ArrayList colError = new ArrayList();

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Add(Exception e)
        {
            colError.Add(e);
        }

        public void Remove(Exception e)
        {
            var itemCount = 0;
            for (itemCount = 1; itemCount <= colError.Count; itemCount++)
                if (e == colError[itemCount])
                {
                    colError.Remove(itemCount);
                    break;
                }
        }

        public void Clear()
        {
            var itemCount = 0;
            for (itemCount = 1; itemCount <= colError.Count; itemCount++) colError.Remove(itemCount);
        }

        public Exception get_Item(int index)
        {
            return (Exception) colError[index];
        }


        public int Count()
        {
            return colError.Count;
        }


        public virtual IEnumerator GetEnumerator()
        {
            return colError.GetEnumerator();
        }

        // interface methods implemented by GetEnumerator
    }
}