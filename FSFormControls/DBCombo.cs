using FSLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBCombo.bmp")]
    [ToolboxItem(true)]
    [Serializable]
    public class DBCombo : System.Windows.Forms.ComboBox, ISupportInitialize
    {
        /* INICIO COMPATIBILIDAD INFRAGISTICS */

        public enum SortStyleEnum
        {
            Ascending,
            AscendingByValue,
            Descending,
            DescendingByValue,
            None
        }

        public delegate void MouseEnterElementEventHandler(object sender, DBEditorButtonEventArgs e);

        public event EventHandler AfterEnterEditMode;
        public event EventHandler AfterExitEditMode;
        public event EventHandler SelectionChanged;
        public event EventHandler ValueChanged;
        public event DBEditorButtonEventHandler EditorButtonClick;
        public event MouseEnterElementEventHandler MouseEnterElement;

        public DBAppearance Appearance { get; set; }
        public bool BlankSelection { get; set; }
        public object DataControl { get; set; }
        public object DataControlList { get; set; }
        public string DBField { get; set; }
        public string DBFieldData { get; set; }
        public string DBFieldList { get; set; }
        public bool Editable { get; set; }
        public bool GridMode { get; set; }
        public bool IsInEditMode { get; set; }
        public string OrderBy { get; set; }
        public bool ShowCode { get; set; }
        public bool Sort { get; set; } = true;
        public SortStyleEnum SortStyle { get; set; }
        public object About { get; set; }

        private bool doTextChanged = true;

        /* FIN COMPATIBILIDAD INFRAGISTICS */

        public DBCombo()
        {
            Appearance = new DBAppearance();

            this.SelectedValueChanged += DBCombo_SelectedValueChanged;

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            if (ButtonsRight != null && ButtonsRight.Count > 0)
            {
                foreach (DBButton button in ButtonsRight)
                {
                    button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    button.Width = 16;
                    button.Height = 16;
                    button.Visible = true;
                    button.Top = 0;
                    button.Click += Button_Click;
                    button.ToolTip = button.Text;
                    button.MouseEnter += Button_MouseEnter;

                    button.BringToFront();
                }
            }

            if (ButtonsLeft != null && ButtonsLeft.Count > 0)
            {
                foreach (DBButton button in ButtonsLeft)
                {
                    button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    button.Width = 16;
                    button.Height = 16;
                    button.Visible = true;
                    button.Top = 0;
                    button.Click += Button_Click;
                    button.ToolTip = button.Text;
                    button.MouseEnter += Button_MouseEnter;

                    button.BringToFront();
                }
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (null != MouseEnterElement)
                MouseEnterElement(this, new DBEditorButtonEventArgs());
        }

        private void DBCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (doTextChanged)
            {
                if (SelectionChanged != null)
                    SelectionChanged(this, e);

                if (ValueChanged != null)
                    ValueChanged(this, e);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (DBButton)sender;

            if (EditorButtonClick != null)
                EditorButtonClick(sender, new DBEditorButtonEventArgs());
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object Value
        {
            get { return this.SelectedValue; }
            set
            {
                if (value == null)
                    return;

                DBComboExBoxItem dbitem = FindByValue(value.ToString());
                if (dbitem != null)
                    this.Text = dbitem.Text;
            }
        }

        private DBComboExValues m_Items;
        public new DBComboExValues Items
        {
            get
            {
                if (m_Items == null)
                    m_Items = new DBComboExValues(this);

                return m_Items;
            }
            set
            {
                m_Items = value;
                if (value != null)
                    foreach (DBComboExBoxItem v in value)
                        this.Items.Add(v);
            }
        }

        public DBComboExBoxItem FindByValue(string value)
        {
            if (Items != null)
            {
                foreach (DBComboExBoxItem dbcol in Items)
                    if (Functions.Value(dbcol.Value).ToLower() == value.ToLower())
                        return dbcol;
            }

            return null;
        }

        private Global.AccessMode m_Mode = Global.AccessMode.ReadMode;
        public Global.AccessMode Mode
        {
            get
            {
                return m_Mode;
            }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case Global.AccessMode.ReadMode:
                        this.Enabled = false;
                        this.BackColor = Global.NormalBackColor;
                        //cmdEdit.Visible = false;
                        break;
                    case Global.AccessMode.WriteMode:
                        this.Enabled = true;
                        this.BackColor = Global.WriteBackColor;
                        //cmdEdit.Visible = true;
                        //Combo1.BringToFront();
                        break;
                    case Global.AccessMode.ProtectedMode:
                        this.Enabled = true;
                        this.BackColor = Global.ObligatoryBackColor;
                        //cmdEdit.Visible = true;
                        //Combo1.BringToFront();
                        break;
                }
            }
        }

        private bool m_Obligatory = false;
        public bool Obligatory
        {
            get { return m_Obligatory; }
            set
            {
                m_Obligatory = value;
                if (value) this.BackColor = Global.ObligatoryBackColor;
            }
        }

        [Description("Modo lectura")]
        public bool ReadOnly
        {
            get { return m_Mode == Global.AccessMode.ReadMode; }
            set { Mode = value ? Global.AccessMode.ReadMode : Global.AccessMode.WriteMode; }
        }

        private object m_SelectedOption = null;
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public object SelectedOption
        {
            get { return m_SelectedOption; }
            set
            {
                m_SelectedOption = value;
                this.SelectedValue = value;
            }
        }

        private bool m_ShowEdit = false;
        public bool ShowEdit
        {
            get { return m_ShowEdit; }
            set
            {
                m_ShowEdit = value;
                //cmdEdit.Visible = m_ShowEdit;
            }
        }

        public void SetDataBinding(ArrayList dataSource, string valueMember)
        {
            doTextChanged = false;
            if (!String.IsNullOrEmpty(valueMember))
                this.ValueMember = valueMember;

            this.DataSource = dataSource;
            doTextChanged = true;
        }

        public void SetDataBinding(ArrayList dataSource, string valueMember, string displayMember)
        {
            doTextChanged = false;
            if (!String.IsNullOrEmpty(valueMember))
                this.ValueMember = valueMember;

            if (!String.IsNullOrEmpty(displayMember))
                this.DisplayMember = displayMember;

            this.DataSource = dataSource;
            doTextChanged = true;
        }

        public bool IsItemInList()
        {
            if (this.Items.Contains(this.SelectedText))
                return true;
            return false;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public DBButtonCollection ButtonsRight { get; set; } = new DBButtonCollection();

        public DBButtonCollection ButtonsLeft { get; set; } = new DBButtonCollection();

        public DBButtonCollection ClickedItemsLeft
        {
            get { return ButtonsLeft; }
            set { ButtonsLeft = value; }
        }

        public DBButtonCollection ClickedItemsRight
        {
            get { return ButtonsRight; }
            set { ButtonsRight = value; }
        }
    }
}
