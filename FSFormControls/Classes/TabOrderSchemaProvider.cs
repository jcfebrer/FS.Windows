#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ProvideProperty("TabScheme", typeof(Control))]
    [Description("Wrap the TabOrderManager class and supply extendee controls with a custom tab scheme")]
    public class TabOrderSchemaProvider : Component, IExtenderProvider

        #region '"MEMBER VARIABLES"' 

    {
        public TabOrderSchemaProvider()
        {
            InitializeComponent();
        }

        #region IExtenderProvider Members

        bool IExtenderProvider.CanExtend(object extendee)
        {
            return CanExtend(extendee);
        }

        #endregion

        private void InitializeComponent()
        {
        }


        [DefaultValue(TabOrderManager.TabScheme.None)]
        public TabOrderManager.TabScheme GetTabScheme(Control c)
        {
            if (!extendees.Contains(c)) return TabOrderManager.TabScheme.None;

            return (TabOrderManager.TabScheme) extendees[c];
        }


        private void HookFormLoad()
        {
            if (!(topLevelForm == null)) topLevelForm.Load += TopLevelForm_Load;
        }


        private void UnhookFormLoad()
        {
            if (!(topLevelForm == null)) topLevelForm.Load -= TopLevelForm_Load;
        }


        private void HookParentChangedEvents(Control c)
        {
            while (!(c == null))
            {
                c.ParentChanged += Extendee_ParentChanged;
                c = c.Parent;
            }
        }


        public void SetTabScheme(Control c, TabOrderManager.TabScheme val)
        {
            if (val != TabOrderManager.TabScheme.None)
            {
                extendees[c] = val;

                if (topLevelForm == null)
                {
                    if (!(c.TopLevelControl == null))
                    {
                        topLevelForm = (Form) c.TopLevelControl;
                        HookFormLoad();
                    }
                    else
                    {
                        HookParentChangedEvents(c);
                    }
                }
            }
            else if (extendees.Contains(c))
            {
                extendees.Remove(c);

                if (extendees.Count == 0) UnhookFormLoad();
            }
        }

        public bool CanExtend(object extendee)
        {
            return extendee is Form | extendee is DBPanel | extendee is Panel | extendee is GroupBox |
                   extendee is UserControl;
        }

        // interface methods implemented by CanExtend

        public void TopLevelForm_Load(object sender, EventArgs e)
        {
            var f = (Form) sender;
            var tom = new TabOrderManager(f);

            var formScheme = TabOrderManager.TabScheme.None;
            var extendeeEnumerator = extendees.GetEnumerator();
            while (extendeeEnumerator.MoveNext())
            {
                var c = (Control) extendeeEnumerator.Key;
                var scheme = (TabOrderManager.TabScheme) extendeeEnumerator.Value;

                if (c == f)
                    formScheme = scheme;
                else
                    tom.SetSchemeForControl(c, scheme);
            }

            tom.SetTabOrder(formScheme);
        }


        private void Extendee_ParentChanged(object sender, EventArgs e)
        {
            if (!(topLevelForm == null)) return;

            var c = (Control) sender;

            if (!(c.TopLevelControl == null) & c.TopLevelControl is Form)
            {
                topLevelForm = (Form) c.TopLevelControl;
                HookFormLoad();
            }
        }

        private readonly Hashtable extendees = new Hashtable();

        private Form topLevelForm;

        #endregion
    }
}