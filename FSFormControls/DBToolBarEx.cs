#region

using FSException;
using FSLibrary;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBarEx : DBUserControl
    {
        private bool m_AllowAddNew = true;
        private bool m_AllowCancel = true;
        private bool m_AllowClose = true;
        private bool m_AllowDelete = true;
        private bool m_AllowEdit = true;
        private bool m_AllowFilter = true;
        private bool m_AllowList = true;
        private bool m_AllowNavigate = true;
        private bool m_AllowPrint = true;
        private bool m_AllowRecord = true;
        private bool m_AllowSave = true;
        private bool m_AllowSearch = true;

        private bool m_ShowAddNewButton = true;
        private bool m_ShowCancelButton = true;
        private bool m_ShowCloseButton = true;
        private bool m_ShowDeleteButton = true;
        private bool m_ShowEditButton = true;
        private bool m_ShowFilterButton = true;
        private bool m_ShowListButton = true;
        private bool m_ShowNavigateButton = true;
        private bool m_ShowPrintButton = true;
        private bool m_ShowRecordButton = true;
        private bool m_ShowSaveButton = true;
        private bool m_ShowScrollBar = true;
        private bool m_ShowSearchButton = true;
        private bool m_ShowText = true;

        internal ToolStripButton ToolBarButton77;
        internal ToolStripButton ToolBarButton78;
        internal ToolStripButton ToolBarButton84;
        private ToolStripButton toolBarButton87;

        public int Value
        {
            get { return HScroll1.Value; }
            set
            {
                if (DataControl == null) 
                    return;

                if (value != HScroll1.Value)
                {
                    if (value <= HScroll1.Maximum)
                    {
                        HScroll1.Value = value;
                    }
                    else
                    {
                        if (value - 1 == HScroll1.Maximum) HScroll1.Maximum = value;
                        HScroll1.Value = HScroll1.Maximum;
                    }
                }
            }
        }


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }

        public bool VisibleScroll
        {
            get { return HScroll1.Visible; }
            set { HScroll1.Visible = value; }
        }

        public bool VisibleTotalRecord
        {
            get { return lblReg.Visible; }
            set { lblReg.Visible = value; }
        }

        public bool ShowNavigateButton
        {
            get { return m_ShowNavigateButton; }
            set
            {
                m_ShowNavigateButton = value;
                ShowToolBar();
            }
        }

        public bool ShowSearchButton
        {
            get { return m_ShowSearchButton; }
            set
            {
                m_ShowSearchButton = value;
                ShowToolBar();
            }
        }

        public bool ShowCloseButton
        {
            get { return m_ShowCloseButton; }
            set
            {
                m_ShowCloseButton = value;
                ShowToolBar();
            }
        }

        public bool ShowCancelButton
        {
            get { return m_ShowCancelButton; }
            set
            {
                m_ShowCancelButton = value;
                ShowToolBar();
            }
        }

        public bool ShowSaveButton
        {
            get { return m_ShowSaveButton; }
            set
            {
                m_ShowSaveButton = value;
                ShowToolBar();
            }
        }

        public bool ShowScrollBar
        {
            get { return m_ShowScrollBar; }
            set
            {
                m_ShowScrollBar = value;
                ShowToolBar();
            }
        }

        public bool ShowText
        {
            get { return m_ShowText; }
            set
            {
                m_ShowText = value;
                ShowToolBar();
            }
        }

        public bool ShowAddNewButton
        {
            get { return m_ShowAddNewButton; }
            set
            {
                m_ShowAddNewButton = value;
                ShowToolBar();
            }
        }

        public bool ShowPrintButton
        {
            get { return m_ShowPrintButton; }
            set
            {
                m_ShowPrintButton = value;
                ShowToolBar();
            }
        }

        public bool ShowFilterButton
        {
            get { return m_ShowFilterButton; }
            set
            {
                m_ShowFilterButton = value;
                ShowToolBar();
            }
        }

        public bool ShowRecordButton
        {
            get { return m_ShowRecordButton; }
            set
            {
                m_ShowRecordButton = value;
                ShowToolBar();
            }
        }

        public bool ShowEditButton
        {
            get { return m_ShowEditButton; }
            set
            {
                m_ShowEditButton = value;
                ShowToolBar();
            }
        }

        public bool ShowDeleteButton
        {
            get { return m_ShowDeleteButton; }
            set
            {
                m_ShowDeleteButton = value;
                ShowToolBar();
            }
        }

        public bool ShowListButton
        {
            get { return m_ShowListButton; }
            set
            {
                m_ShowListButton = value;
                ShowToolBar();
            }
        }

        public bool AllowNavigate
        {
            get { return m_AllowNavigate; }
            set
            {
                m_AllowNavigate = value;
                ShowToolBar();
            }
        }

        public bool AllowSearch
        {
            get { return m_AllowSearch; }
            set
            {
                m_AllowSearch = value;
                ShowToolBar();
            }
        }

        public bool AllowCancel
        {
            get { return m_AllowCancel; }
            set
            {
                m_AllowCancel = value;
                ShowToolBar();
            }
        }

        public bool AllowSave
        {
            get { return m_AllowSave; }
            set
            {
                m_AllowSave = value;
                ShowToolBar();
            }
        }

        public bool AllowAddNew
        {
            get { return m_AllowAddNew; }
            set
            {
                m_AllowAddNew = value;
                ShowToolBar();
            }
        }

        public bool AllowPrint
        {
            get { return m_AllowPrint; }
            set
            {
                m_AllowPrint = value;
                ShowToolBar();
            }
        }

        public bool AllowFilter
        {
            get { return m_AllowFilter; }
            set
            {
                m_AllowFilter = value;
                ShowToolBar();
            }
        }

        public bool AllowRecord
        {
            get { return m_AllowRecord; }
            set
            {
                m_AllowRecord = value;
                ShowToolBar();
            }
        }

        public bool AllowEdit
        {
            get { return m_AllowEdit; }
            set
            {
                m_AllowEdit = value;
                ShowToolBar();
            }
        }

        public bool AllowDelete
        {
            get { return m_AllowDelete; }
            set
            {
                m_AllowDelete = value;
                ShowToolBar();
            }
        }

        public bool AllowList
        {
            get { return m_AllowList; }
            set
            {
                m_AllowList = value;
                ShowToolBar();
            }
        }

        public bool AllowClose
        {
            get { return m_AllowClose; }
            set
            {
                m_AllowClose = value;
                ShowToolBar();
            }
        }

        public event ChangeEventHandler Change;
        public event EventHandler ButtonClick;

        public void Initialize()
        {
            if (DataControl == null)
            {
                AllowAddNew = false;
                AllowDelete = false;
                AllowFilter = false;
                AllowList = false;
                AllowNavigate = false;
                AllowPrint = false;
                AllowRecord = false;
                ShowScrollBar = false;
                AllowSearch = false;

                //DBGlobal.Err.ErrorMessage( this.FindForm(), this, "DataControl no especificado.", "", MessageBoxIcon.Error, null, false ); 
            }
            else
            {
                DataControl.ChangeRecord += m_dbcontrol_ChangeRecord;

                HScroll1.SmallChange = 1;
                HScroll1.Minimum = 0;
                HScroll1.Maximum = DataControl.RecordCount() - 1 == -1 ? 0 : DataControl.RecordCount() - 1;
                HScroll1.LargeChange = 1;

                HScroll1.Value = DataControl.DBPosition;
                DataControl.Go(DataControl.DBPosition);

                ShowRecordLabel();
            }
        }

        private void tbrRegistrosBIG_ButtonClick(object sender, EventArgs e)
        {
            ToolbarOptions(sender, e);
        }

        private void ToolbarOptions(object sender, EventArgs e)
        {
            ToolStripItemClickedEventArgs button = (ToolStripItemClickedEventArgs)e;
            ToolStripItem tsi = button.ClickedItem;
            if (tsi != null && tsi.Tag != null)
                switch (tsi.Tag.ToString().ToUpper())
                {
                    case "IMPRIMIR":
                        if (DataControl != null) DataControl.ShowReport();
                        break;
                    case "BUSCAR":
                        if (DataControl != null) DataControl.ShowFind();
                        break;
                    case "BUSCARSIGUIENTE":
                        if (DataControl != null) DataControl.FindNext();
                        break;
                    case "ESTABLECERFILTROS":
                        if (DataControl != null) DataControl.ShowFilter();
                        break;
                    case "QUITARFILTROS":
                        if (DataControl != null) DataControl.DeleteFilter();
                        break;
                    case "MOVERPRIMERO":
                        if (DataControl != null) DataControl.MoveFirst();
                        break;
                    case "MOVERANTERIOR":
                        if (DataControl != null) DataControl.MovePrevious();
                        break;
                    case "MOVERSIGUIENTE":
                        if (DataControl != null) DataControl.MoveNext();
                        break;
                    case "MOVERULTIMO":
                        if (DataControl != null) DataControl.MoveLast();
                        break;
                    case "NUEVO":
                        if (DataControl != null) DataControl.AddNew();
                        if (DataControl != null) DataControl.Mode = Global.AccessMode.WriteMode;
                        break;
                    case "CANCELARALTA":
                        if (DataControl != null)
                        {
                            DataControl.CancelEdit();
                            DataControl.Mode = Global.AccessMode.ReadMode;
                        }
                        else
                        {
                            CancelAll();
                        }

                        break;
                    case "GUARDAR":
                        Save();
                        break;
                    case "REFRESCAR":
                        Refrescar();
                        break;
                    case "ELIMINAR":
                        if (DataControl != null)
                            DataControl.Delete();
                        break;
                    case "EDITAR":
                        if (DataControl != null)
                            DataControl.Mode = Global.AccessMode.WriteMode;
                        else
                            EditAll();
                        break;
                    case "GO":
                        if (DataControl != null)
                        {
                            var pos = 0;
                            var ibr = InputBox.ShowDialog("Ir a:", "", "", InputBox.Icon.Question, InputBox.Buttons.Ok, InputBox.Type.TextBox);
                            pos = Convert.ToInt32(InputBox.ResultValue);
                            if (pos != 0)
                                if ((pos >= HScroll1.Minimum) & (pos <= HScroll1.Maximum + 1))
                                    HScroll1.Value = pos - 1;
                        }

                        break;
                    case "LISTADO":
                        if (DataControl != null)
                            DataControl.ShowList();
                        break;
                    case "REGISTRO":
                        if (DataControl != null)
                            DataControl.ShowRecord();
                        break;
                    case "CERRAR":
                        var findForm = FindForm();
                        if (findForm != null)
                            findForm.Close();
                        break;
                }


            if (null != ButtonClick)
                ButtonClick(sender, e);
        }


        public void CancelAll()
        {
            var findForm = FindForm();
            if (findForm != null) CancelAllDbControls(findForm.Controls);
        }


        public void EditAll()
        {
            WriteModeAllDbControls(FindForm().Controls);
        }


        public void SaveAll()
        {
            var findForm = FindForm();
            if (findForm != null)
                SaveAllDbControls(findForm.Controls);
        }


        public void Save()
        {
            SaveAll();

            //if (DataControl != null)
            //{
            //    DataControl.Save();
            //    Form findForm = FindForm();
            //    if (findForm != null && !(DataControl.RelationSaveError(findForm.Controls)))
            //    {
            //        DataControl.Mode = Global.AccessMode.ReadMode;
            //    }
            //}
            //else
            //{
            //    SaveAll();
            //}
        }


        public void Refrescar()
        {
            if (DataControl == null)
                return;
            DataControl.ReConnect();
            DataControl.Go(0);
        }


        private bool CancelAllDbControls(ControlCollection frm)
        {
            if (frm == null) return false;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    CancelAllDbControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl)
                    {
                        ((DBControl) ctr).CancelEdit();
                        ((DBControl) ctr).Mode = Global.AccessMode.ReadMode;
                    }
                }

            return false;
        }


        private void WriteModeAllDbControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    WriteModeAllDbControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl) ((DBControl) ctr).Mode = Global.AccessMode.WriteMode;
                }
        }


        private void SaveAllDbControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    SaveAllDbControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl)
                    {
                        ((DBControl) ctr).Save();

                        if (!((DBControl) ctr).RelationSaveError(FindForm().Controls))
                            ((DBControl) ctr).Mode = Global.AccessMode.ReadMode;
                    }
                }
        }


        private void HScroll1_ValueChanged(object sender, EventArgs e)
        {
            if (DataControl == null) return;
            if (HScroll1.Value != -1)
            {
                DataControl.Go(HScroll1.Value);
                ShowRecordLabel();
            }

            if (null != Change) Change();
        }


        private void ShowRecordLabel()
        {
            lblReg.Text = (int)(HScroll1.Value + 1) + " / " + (int)(HScroll1.Maximum + 1);
        }


        private void ShowToolBar()
        {
            if (DataControl != null)
            {
                HScroll1.SmallChange = 1;
                HScroll1.Minimum = 0;
                HScroll1.Maximum = DataControl.RecordCount() - 1 == -1 ? 0 : DataControl.RecordCount() - 1;

                HScroll1.LargeChange = DataControl.RecordCount() - 1 < 10 ? 1 : 10;
                ShowRecordLabel();
            }


            Height = tbrRegistros.Height;
            Width = tbrRegistros.Width;

            HScroll1.Enabled = m_AllowNavigate;
            HScroll1.Visible = m_ShowScrollBar;

            tbrRegistros.Items[6].Enabled = m_AllowNavigate;
            tbrRegistros.Items[7].Enabled = m_AllowNavigate;
            tbrRegistros.Items[8].Enabled = m_AllowNavigate;
            tbrRegistros.Items[9].Enabled = m_AllowNavigate;
            
            tbrRegistros.Items[1].Enabled = m_AllowSearch;
            tbrRegistros.Items[2].Enabled = m_AllowSearch;
            tbrRegistros.Items[15].Enabled = m_AllowSave;
            tbrRegistros.Items[14].Enabled = m_AllowCancel;
            tbrRegistros.Items[11].Enabled = m_AllowAddNew;
            tbrRegistros.Items[24].Enabled = m_AllowPrint;
            tbrRegistros.Items[4].Enabled = m_AllowFilter;
            tbrRegistros.Items[22].Enabled = m_AllowRecord;
            tbrRegistros.Items[12].Enabled = m_AllowEdit;
            tbrRegistros.Items[18].Enabled = m_AllowDelete;
            tbrRegistros.Items[21].Enabled = m_AllowList;
            tbrRegistros.Items[26].Enabled = m_AllowClose;

            tbrRegistros.Items[6].Visible = m_ShowNavigateButton;
            tbrRegistros.Items[7].Visible = m_ShowNavigateButton;
            tbrRegistros.Items[8].Visible = m_ShowNavigateButton;
            tbrRegistros.Items[9].Visible = m_ShowNavigateButton;
            
            tbrRegistros.Items[1].Visible = m_ShowSearchButton;
            tbrRegistros.Items[2].Visible = m_ShowSearchButton;
            tbrRegistros.Items[15].Visible = m_ShowSaveButton;
            tbrRegistros.Items[14].Visible = m_ShowCancelButton;
            tbrRegistros.Items[11].Visible = m_ShowAddNewButton;
            tbrRegistros.Items[24].Visible = m_ShowPrintButton;
            tbrRegistros.Items[4].Visible = m_ShowFilterButton;
            tbrRegistros.Items[22].Visible = m_ShowRecordButton;
            tbrRegistros.Items[12].Visible = m_ShowEditButton;
            tbrRegistros.Items[18].Visible = m_ShowDeleteButton;
            tbrRegistros.Items[21].Visible = m_ShowListButton;
            tbrRegistros.Items[26].Visible = m_ShowCloseButton;

            //HideSeparatorDuplicates();
            UpdateButtons();
        }


        //private void HideSeparatorDuplicates()
        //{
        //    ToolStripButton lastVisible = null;
        //    var ant = false;

        //    foreach (ToolStripButton button in tbrRegistros.Items)
        //        if (button.Visible)
        //        {
        //            if (button.Style == ToolBarButtonStyle.Separator)
        //            {
        //                if (ant) 
        //                    button.Visible = false;

        //                ant = true;
        //            }
        //            else
        //            {
        //                ant = false;
        //            }

        //            lastVisible = button;
        //        }

        //    if (lastVisible != null)
        //        if (lastVisible.Style == ToolBarButtonStyle.Separator)
        //            lastVisible.Visible = false;
        //}

        private void UpdateButtons()
        {
            //if(m_ShowText)
            //    tbrRegistros.TextAlign = ToolBarTextAlign.Underneath;
            //else
            //    tbrRegistros.TextAlign = ToolBarTextAlign.Right;

            foreach (ToolStripButton button in tbrRegistros.Items)
            {
                if (m_ShowText)
                {
                    button.Text = button.ToolTipText;
                }
                else
                {
                    button.Text = "";
                }
            }
        }


        private void m_dbcontrol_ChangeRecord()
        {
            ShowRecordLabel();
        }

        #region Delegates

        public delegate void ButtonClickEventHandler(object sender, EventArgs e);

        public delegate void ChangeEventHandler();

        #endregion


        #region '" Código generado por el Diseñador de Windows Forms "' 

        private HScrollBar HScroll1;
        internal ToolStripButton Separador8;
        internal ToolStripButton ToolBarButton1;
        internal ToolStripButton ToolBarButton10;
        internal ToolStripButton ToolBarButton11;
        internal ToolStripButton ToolBarButton12;
        internal ToolStripButton ToolBarButton13;
        internal ToolStripButton ToolBarButton14;
        internal ToolStripButton ToolBarButton15;
        internal ToolStripButton ToolBarButton16;
        internal ToolStripButton ToolBarButton17;
        internal ToolStripButton ToolBarButton18;
        internal ToolStripButton ToolBarButton19;
        internal ToolStripButton ToolBarButton2;
        internal ToolStripButton ToolBarButton20;
        internal ToolStripButton ToolBarButton21;
        internal ToolStripButton ToolBarButton22;
        internal ToolStripButton ToolBarButton23;
        internal ToolStripButton ToolBarButton24;
        internal ToolStripButton ToolBarButton25;
        internal ToolStripButton ToolBarButton26;
        internal ToolStripButton ToolBarButton3;
        internal ToolStripButton ToolBarButton4;
        internal ToolStripButton ToolBarButton5;
        internal ToolStripButton ToolBarButton7;
        internal ToolStripButton ToolBarButton8;
        internal ToolStripButton ToolBarButton9;
        private IContainer components;
        internal ImageList imgStandard;
        internal ImageList imgXPToolBarBIG;
        internal ImageList imgXPToolbar;
        internal Label lblReg;
        internal ToolStrip tbrRegistros;

        public DBToolBarEx()
        {
            Init();
        }

        public DBToolBarEx(string name)
        {
            Name = name;
            Init();
        }

        private void Init()
        {
            InitializeComponent();

            TabStop = false;

            HScroll1.ValueChanged += HScroll1_ValueChanged;
            tbrRegistros.ItemClicked += tbrRegistrosBIG_ButtonClick;

            Resize += DBToolBarEx_Resize;
            Load += DBToolBarEx_Load;
            Paint += DBToolBarEx_Paint;
        }

        private void DBToolBarEx_Paint(object sender, PaintEventArgs e)
        {
            ShowToolBar();
        }

        private void DBToolBarEx_Load(object sender, EventArgs e)
        {
            ShowToolBar();
        }

        private void DBToolBarEx_Resize(object sender, EventArgs e)
        {
            ShowToolBar();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(DBToolBarEx));
            imgXPToolbar = new ImageList(components);
            Separador8 = new ToolStripButton();
            HScroll1 = new HScrollBar();
            tbrRegistros = new ToolStrip();
            ToolBarButton1 = new ToolStripButton();
            ToolBarButton2 = new ToolStripButton();
            ToolBarButton3 = new ToolStripButton();
            ToolBarButton4 = new ToolStripButton();
            ToolBarButton5 = new ToolStripButton();
            ToolBarButton7 = new ToolStripButton();
            ToolBarButton8 = new ToolStripButton();
            ToolBarButton9 = new ToolStripButton();
            ToolBarButton10 = new ToolStripButton();
            ToolBarButton11 = new ToolStripButton();
            ToolBarButton12 = new ToolStripButton();
            ToolBarButton13 = new ToolStripButton();
            ToolBarButton14 = new ToolStripButton();
            ToolBarButton15 = new ToolStripButton();
            ToolBarButton16 = new ToolStripButton();
            ToolBarButton17 = new ToolStripButton();
            ToolBarButton84 = new ToolStripButton();
            ToolBarButton18 = new ToolStripButton();
            ToolBarButton19 = new ToolStripButton();
            ToolBarButton20 = new ToolStripButton();
            ToolBarButton21 = new ToolStripButton();
            ToolBarButton22 = new ToolStripButton();
            ToolBarButton23 = new ToolStripButton();
            ToolBarButton24 = new ToolStripButton();
            ToolBarButton25 = new ToolStripButton();
            ToolBarButton26 = new ToolStripButton();
            ToolBarButton77 = new ToolStripButton();
            ToolBarButton78 = new ToolStripButton();
            imgXPToolBarBIG = new ImageList(components);
            lblReg = new Label();
            imgStandard = new ImageList(components);
            toolBarButton87 = new ToolStripButton();
            tbrRegistros.SuspendLayout();
            SuspendLayout();
            // 
            // imgXPToolbar
            // 
            imgXPToolbar.ColorDepth = ColorDepth.Depth16Bit;
            imgXPToolbar.ImageStream = (ImageListStreamer)resources.GetObject("imgXPToolbar.ImageStream");
            imgXPToolbar.TransparentColor = Color.Fuchsia;
            imgXPToolbar.Images.SetKeyName(0, "");
            imgXPToolbar.Images.SetKeyName(1, "");
            imgXPToolbar.Images.SetKeyName(2, "");
            imgXPToolbar.Images.SetKeyName(3, "");
            imgXPToolbar.Images.SetKeyName(4, "");
            imgXPToolbar.Images.SetKeyName(5, "");
            imgXPToolbar.Images.SetKeyName(6, "");
            imgXPToolbar.Images.SetKeyName(7, "");
            imgXPToolbar.Images.SetKeyName(8, "");
            imgXPToolbar.Images.SetKeyName(9, "");
            imgXPToolbar.Images.SetKeyName(10, "");
            imgXPToolbar.Images.SetKeyName(11, "");
            imgXPToolbar.Images.SetKeyName(12, "");
            imgXPToolbar.Images.SetKeyName(13, "");
            imgXPToolbar.Images.SetKeyName(14, "");
            // 
            // Separador8
            // 
            Separador8.Name = "Separador8";
            Separador8.Size = new Size(23, 23);
            // 
            // HScroll1
            // 
            HScroll1.Location = new Point(1034, 12);
            HScroll1.Name = "HScroll1";
            HScroll1.Size = new Size(104, 16);
            HScroll1.TabIndex = 10;
            // 
            // tbrRegistros
            // 
            tbrRegistros.Dock = DockStyle.None;
            tbrRegistros.ImageList = imgXPToolBarBIG;
            tbrRegistros.Items.AddRange(new ToolStripItem[] { ToolBarButton1, ToolBarButton2, ToolBarButton3, ToolBarButton4, ToolBarButton5, ToolBarButton7, ToolBarButton8, ToolBarButton9, ToolBarButton10, ToolBarButton11, ToolBarButton12, ToolBarButton13, ToolBarButton14, ToolBarButton15, ToolBarButton16, ToolBarButton17, ToolBarButton84, ToolBarButton18, ToolBarButton19, ToolBarButton20, ToolBarButton21, ToolBarButton22, ToolBarButton23, ToolBarButton24, ToolBarButton25, ToolBarButton26, ToolBarButton77, ToolBarButton78 });
            tbrRegistros.Location = new Point(0, 0);
            tbrRegistros.Name = "tbrRegistros";
            tbrRegistros.Size = new Size(1293, 25);
            tbrRegistros.TabIndex = 12;
            // 
            // ToolBarButton1
            // 
            ToolBarButton1.Name = "ToolBarButton1";
            ToolBarButton1.Size = new Size(23, 22);
            // 
            // ToolBarButton2
            // 
            ToolBarButton2.ImageIndex = 0;
            ToolBarButton2.Name = "ToolBarButton2";
            ToolBarButton2.Size = new Size(62, 22);
            ToolBarButton2.Tag = "BUSCAR";
            ToolBarButton2.Text = "Buscar";
            ToolBarButton2.ToolTipText = "Buscar";
            // 
            // ToolBarButton3
            // 
            ToolBarButton3.ImageIndex = 1;
            ToolBarButton3.Name = "ToolBarButton3";
            ToolBarButton3.Size = new Size(52, 22);
            ToolBarButton3.Tag = "BUSCARSIGUIENTE";
            ToolBarButton3.Text = "Sig...";
            ToolBarButton3.ToolTipText = "Buscar siguiente";
            // 
            // ToolBarButton4
            // 
            ToolBarButton4.Name = "ToolBarButton4";
            ToolBarButton4.Size = new Size(23, 22);
            // 
            // ToolBarButton5
            // 
            ToolBarButton5.ImageIndex = 2;
            ToolBarButton5.Name = "ToolBarButton5";
            ToolBarButton5.Size = new Size(54, 22);
            ToolBarButton5.Tag = "ESTABLECERFILTROS";
            ToolBarButton5.Text = "Filtro";
            ToolBarButton5.ToolTipText = "Establecer filtro";
            // 
            // ToolBarButton7
            // 
            ToolBarButton7.Name = "ToolBarButton7";
            ToolBarButton7.Size = new Size(23, 22);
            // 
            // ToolBarButton8
            // 
            ToolBarButton8.ImageIndex = 3;
            ToolBarButton8.Name = "ToolBarButton8";
            ToolBarButton8.Size = new Size(56, 22);
            ToolBarButton8.Tag = "MOVERPRIMERO";
            ToolBarButton8.Text = "Inicio";
            ToolBarButton8.ToolTipText = "Primero";
            // 
            // ToolBarButton9
            // 
            ToolBarButton9.ImageIndex = 4;
            ToolBarButton9.Name = "ToolBarButton9";
            ToolBarButton9.Size = new Size(54, 22);
            ToolBarButton9.Tag = "MOVERANTERIOR";
            ToolBarButton9.Text = "Atrás";
            ToolBarButton9.ToolTipText = "Anterior";
            // 
            // ToolBarButton10
            // 
            ToolBarButton10.ImageIndex = 5;
            ToolBarButton10.Name = "ToolBarButton10";
            ToolBarButton10.Size = new Size(52, 22);
            ToolBarButton10.Tag = "MOVERSIGUIENTE";
            ToolBarButton10.Text = "Sig...";
            ToolBarButton10.ToolTipText = "Siguiente";
            // 
            // ToolBarButton11
            // 
            ToolBarButton11.ImageIndex = 6;
            ToolBarButton11.Name = "ToolBarButton11";
            ToolBarButton11.Size = new Size(43, 22);
            ToolBarButton11.Tag = "MOVERULTIMO";
            ToolBarButton11.Text = "Fin";
            ToolBarButton11.ToolTipText = "Fin";
            // 
            // ToolBarButton12
            // 
            ToolBarButton12.Name = "ToolBarButton12";
            ToolBarButton12.Size = new Size(23, 22);
            // 
            // ToolBarButton13
            // 
            ToolBarButton13.ImageIndex = 7;
            ToolBarButton13.Name = "ToolBarButton13";
            ToolBarButton13.Size = new Size(62, 22);
            ToolBarButton13.Tag = "NUEVO";
            ToolBarButton13.Text = "Nuevo";
            ToolBarButton13.ToolTipText = "Nuevo";
            // 
            // ToolBarButton14
            // 
            ToolBarButton14.ImageIndex = 8;
            ToolBarButton14.Name = "ToolBarButton14";
            ToolBarButton14.Size = new Size(57, 22);
            ToolBarButton14.Tag = "EDITAR";
            ToolBarButton14.Text = "Editar";
            ToolBarButton14.ToolTipText = "Editar";
            // 
            // ToolBarButton15
            // 
            ToolBarButton15.Name = "ToolBarButton15";
            ToolBarButton15.Size = new Size(23, 22);
            // 
            // ToolBarButton16
            // 
            ToolBarButton16.ImageIndex = 9;
            ToolBarButton16.Name = "ToolBarButton16";
            ToolBarButton16.Size = new Size(63, 22);
            ToolBarButton16.Tag = "CANCELARALTA";
            ToolBarButton16.Text = "Canc...";
            ToolBarButton16.ToolTipText = "Canc...";
            // 
            // ToolBarButton17
            // 
            ToolBarButton17.ImageIndex = 10;
            ToolBarButton17.Name = "ToolBarButton17";
            ToolBarButton17.Size = new Size(58, 22);
            ToolBarButton17.Tag = "GUARDAR";
            ToolBarButton17.Text = "Salvar";
            ToolBarButton17.ToolTipText = "Salvar";
            // 
            // ToolBarButton84
            // 
            ToolBarButton84.ImageIndex = 11;
            ToolBarButton84.Name = "ToolBarButton84";
            ToolBarButton84.Size = new Size(75, 22);
            ToolBarButton84.Tag = "REFRESCAR";
            ToolBarButton84.Text = "Refrescar";
            ToolBarButton84.ToolTipText = "Refrescar";
            // 
            // ToolBarButton18
            // 
            ToolBarButton18.Name = "ToolBarButton18";
            ToolBarButton18.Size = new Size(23, 22);
            // 
            // ToolBarButton19
            // 
            ToolBarButton19.ImageIndex = 12;
            ToolBarButton19.Name = "ToolBarButton19";
            ToolBarButton19.Size = new Size(59, 22);
            ToolBarButton19.Tag = "ELIMINAR";
            ToolBarButton19.Text = "Borrar";
            ToolBarButton19.ToolTipText = "Borrar";
            // 
            // ToolBarButton20
            // 
            ToolBarButton20.Name = "ToolBarButton20";
            ToolBarButton20.Size = new Size(23, 22);
            // 
            // ToolBarButton21
            // 
            ToolBarButton21.ImageIndex = 13;
            ToolBarButton21.Name = "ToolBarButton21";
            ToolBarButton21.Size = new Size(43, 22);
            ToolBarButton21.Tag = "GO";
            ToolBarButton21.Text = "Ir a";
            ToolBarButton21.ToolTipText = "Ir a";
            ToolBarButton21.Visible = false;
            // 
            // ToolBarButton22
            // 
            ToolBarButton22.ImageIndex = 13;
            ToolBarButton22.Name = "ToolBarButton22";
            ToolBarButton22.Size = new Size(54, 22);
            ToolBarButton22.Tag = "LISTADO";
            ToolBarButton22.Text = "List...";
            ToolBarButton22.ToolTipText = "Listado";
            // 
            // ToolBarButton23
            // 
            ToolBarButton23.ImageIndex = 14;
            ToolBarButton23.Name = "ToolBarButton23";
            ToolBarButton23.Size = new Size(56, 22);
            ToolBarButton23.Tag = "REGISTRO";
            ToolBarButton23.Text = "Reg...";
            ToolBarButton23.ToolTipText = "Registro";
            // 
            // ToolBarButton24
            // 
            ToolBarButton24.Name = "ToolBarButton24";
            ToolBarButton24.Size = new Size(23, 22);
            // 
            // ToolBarButton25
            // 
            ToolBarButton25.ImageIndex = 18;
            ToolBarButton25.Name = "ToolBarButton25";
            ToolBarButton25.Size = new Size(61, 22);
            ToolBarButton25.Tag = "IMPRIMIR";
            ToolBarButton25.Text = "Impr...";
            ToolBarButton25.ToolTipText = "Imprimir";
            // 
            // ToolBarButton26
            // 
            ToolBarButton26.Name = "ToolBarButton26";
            ToolBarButton26.Size = new Size(23, 22);
            // 
            // ToolBarButton77
            // 
            ToolBarButton77.ImageIndex = 9;
            ToolBarButton77.Name = "ToolBarButton77";
            ToolBarButton77.Size = new Size(59, 22);
            ToolBarButton77.Tag = "CERRAR";
            ToolBarButton77.Text = "Cerrar";
            ToolBarButton77.ToolTipText = "Cerrar formulario";
            // 
            // ToolBarButton78
            // 
            ToolBarButton78.Name = "ToolBarButton78";
            ToolBarButton78.Size = new Size(23, 22);
            // 
            // imgXPToolBarBIG
            // 
            imgXPToolBarBIG.ColorDepth = ColorDepth.Depth16Bit;
            imgXPToolBarBIG.ImageStream = (ImageListStreamer)resources.GetObject("imgXPToolBarBIG.ImageStream");
            imgXPToolBarBIG.TransparentColor = Color.Fuchsia;
            imgXPToolBarBIG.Images.SetKeyName(0, "");
            imgXPToolBarBIG.Images.SetKeyName(1, "");
            imgXPToolBarBIG.Images.SetKeyName(2, "");
            imgXPToolBarBIG.Images.SetKeyName(3, "");
            imgXPToolBarBIG.Images.SetKeyName(4, "");
            imgXPToolBarBIG.Images.SetKeyName(5, "");
            imgXPToolBarBIG.Images.SetKeyName(6, "");
            imgXPToolBarBIG.Images.SetKeyName(7, "");
            imgXPToolBarBIG.Images.SetKeyName(8, "");
            imgXPToolBarBIG.Images.SetKeyName(9, "");
            imgXPToolBarBIG.Images.SetKeyName(10, "");
            imgXPToolBarBIG.Images.SetKeyName(11, "");
            imgXPToolBarBIG.Images.SetKeyName(12, "");
            imgXPToolBarBIG.Images.SetKeyName(13, "");
            imgXPToolBarBIG.Images.SetKeyName(14, "");
            imgXPToolBarBIG.Images.SetKeyName(15, "");
            imgXPToolBarBIG.Images.SetKeyName(16, "");
            imgXPToolBarBIG.Images.SetKeyName(17, "");
            imgXPToolBarBIG.Images.SetKeyName(18, "");
            // 
            // lblReg
            // 
            lblReg.AutoSize = true;
            lblReg.Location = new Point(1078, 28);
            lblReg.Name = "lblReg";
            lblReg.Size = new Size(24, 15);
            lblReg.TabIndex = 13;
            lblReg.Text = "0/0";
            // 
            // imgStandard
            // 
            imgStandard.ColorDepth = ColorDepth.Depth8Bit;
            imgStandard.ImageStream = (ImageListStreamer)resources.GetObject("imgStandard.ImageStream");
            imgStandard.TransparentColor = Color.Transparent;
            imgStandard.Images.SetKeyName(0, "");
            imgStandard.Images.SetKeyName(1, "");
            imgStandard.Images.SetKeyName(2, "");
            imgStandard.Images.SetKeyName(3, "");
            imgStandard.Images.SetKeyName(4, "");
            imgStandard.Images.SetKeyName(5, "");
            imgStandard.Images.SetKeyName(6, "");
            imgStandard.Images.SetKeyName(7, "");
            imgStandard.Images.SetKeyName(8, "");
            imgStandard.Images.SetKeyName(9, "");
            imgStandard.Images.SetKeyName(10, "");
            imgStandard.Images.SetKeyName(11, "");
            imgStandard.Images.SetKeyName(12, "");
            imgStandard.Images.SetKeyName(13, "");
            imgStandard.Images.SetKeyName(14, "");
            // 
            // toolBarButton87
            // 
            toolBarButton87.Name = "toolBarButton87";
            toolBarButton87.Size = new Size(23, 23);
            // 
            // DBToolBarEx
            // 
            Controls.Add(lblReg);
            Controls.Add(tbrRegistros);
            Controls.Add(HScroll1);
            Name = "DBToolBarEx";
            Size = new Size(1175, 65);
            tbrRegistros.ResumeLayout(false);
            tbrRegistros.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}