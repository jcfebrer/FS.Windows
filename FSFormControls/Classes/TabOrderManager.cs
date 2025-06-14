#region

using FSException;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class TabOrderManager
    {
        #region TabScheme enum

        public enum TabScheme
        {
            None,
            AcrossFirst,
            DownFirst
        }

        #endregion

        private readonly Control container;

        private readonly Hashtable schemeOverrides;

        private int curTabIndex;


        public TabOrderManager(Control container)
        {
            this.container = container;
            curTabIndex = 0;
            schemeOverrides = new Hashtable();
        }

        private TabOrderManager(Control container, int curTabIndex, Hashtable schemeOverrides)
        {
            this.container = container;
            this.curTabIndex = curTabIndex;
            this.schemeOverrides = schemeOverrides;
        }

        public void SetSchemeForControl(Control c, TabScheme scheme)
        {
            schemeOverrides[c] = scheme;
        }


        public int SetTabOrder(TabScheme scheme)
        {
            try
            {
                var controlArraySorted = new ArrayList();
                controlArraySorted.AddRange(container.Controls);
                controlArraySorted.Sort(new TabSchemeComparer(scheme));

                foreach (Control c in controlArraySorted)
                {
                    c.TabIndex = curTabIndex;
                    curTabIndex = curTabIndex + 1;

                    if (c.Controls.Count > 0)
                    {
                        var childScheme = scheme;
                        if (schemeOverrides.Contains(c)) childScheme = (TabScheme) schemeOverrides[c];
                        curTabIndex = new TabOrderManager(c, curTabIndex, schemeOverrides).SetTabOrder(childScheme);
                    }
                }

                return curTabIndex;
            }
            catch (Exception e)
            {
                Debug.Assert(false, "Exception in TabOrderManager.SetTabOrder:  " + e.Message);
                return 0;
            }
        }

        #region Nested type: TabSchemeComparer

        private class TabSchemeComparer : IComparer


        {
            private readonly TabScheme comparisonScheme;

            public TabSchemeComparer(TabScheme scheme)
            {
                comparisonScheme = scheme;
            }

            #region IComparer Members

            int IComparer.Compare(object x, object y)
            {
                return Compare(x, y);
            }

            #endregion

            public virtual int Compare(object x, object y)
            {
                var control1 = (Control) x;
                var control2 = (Control) y;

                object transTemp0 = control1;
                object transTemp1 = control2;
                if ((transTemp0 == null) | (transTemp1 == null))
                {
                    Debug.Assert(false, "Attempting to compare a non-control");
                    return 0;
                }

                if (comparisonScheme == TabScheme.AcrossFirst)
                {
                    if (control1.Top < control2.Top)
                        return -1;
                    if (control1.Top > control2.Top)
                        return 1;
                    return control1.Left.CompareTo(control2.Left);
                }

                if (control1.Left < control2.Left)
                    return -1;
                if (control1.Left > control2.Left)
                    return 1;
                return control1.Top.CompareTo(control2.Top);
            }
        }

        #endregion
    }
}