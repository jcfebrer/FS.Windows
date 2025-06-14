#region

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

internal class resfinder
{
}

namespace FSFormControls
{
    [Designer(typeof(DBControlDesigner))]
    [DesignTimeVisible(true)]
    [ToolboxItem(false)]
    public class DBUserControl : UserControl
    {
        public DBUserControl()
        {
        }


        [Editor(typeof(EditorAbout), typeof(UITypeEditor))]
        public string About { get; set; } = "";

         private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // DBUsercontrol
            // 
            Name = "DBUsercontrol";
            Size = new Size(151, 145);
            ResumeLayout(false);
        }
    }


    public class EditorAbout : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var about = new frmAbout();
            about.Show();
            return null;
        }


        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }


        public new virtual bool GetPaintValueSupported()
        {
            return true;
        }
    }


    public class FieldList : StringConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(DBControl.Fields);
        }


        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }


    internal class DBControlDesigner : ControlDesigner
    {
        public override DesignerVerbCollection Verbs
        {
            get
            {
                var v = new DesignerVerbCollection();

                v.Add(new DesignerVerb("&Acerca de ...", OnAcercaDe));

                return v;
            }
        }

        private void OnAcercaDe(object sender, EventArgs e)
        {
            var about = new frmAbout();
            about.Show();
        }
    }
}