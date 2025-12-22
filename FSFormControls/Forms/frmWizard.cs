#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using FSSystemInfo;

#endregion

namespace FSFormControls
{
    public class frmWizard : DBForm
    {
        protected bool m_bAllowBack = true;

        protected PageIndexChangedDlgt m_dPageIndexChanged;

        public frmWizard()
        {
            InitializeComponent();

            m_dPageIndexChanged = DisplayCurrentPage;
            LinkLabel1.TabStop = false;

            PictureBox1.Click += PictureBox1_Click;
            cmdFinish.Click += cmdFinish_Click;
            cmdNext.Click += cmdNext_Click;
            cmdBack.Click += cmdBack_Click;
            cmdCancel.Click += cmdCancel_Click;
            LinkLabel1.LinkClicked += LinkLabel1_LinkClicked;
        }

        public PageIndexChangedDlgt PageIndexChangedDelegate => m_dPageIndexChanged;

        public int CurrentPage { get; private set; }

        public event ValidatePageEventHandler ValidatePage;

        protected void EnablePrevNextButton(int piPageIndex)
        {
            if (piPageIndex == 0)
            {
                cmdBack.Enabled = false;
            }
            else
            {
                if (m_bAllowBack)
                    cmdBack.Enabled = true;
                else
                    cmdBack.Enabled = false;
                if (piPageIndex == TabControl1.TabCount - 1)
                    cmdNext.Enabled = false;
                else
                    cmdNext.Enabled = true;
            }
        }


        protected void DisplayCurrentPage(int piPageIndex)
        {
            CurrentPage = piPageIndex;

            EnablePrevNextButton(CurrentPage);

            if (TabControl1.TabPages.Count > piPageIndex)
            {
                TabControl1.SelectedTab = TabControl1.TabPages[CurrentPage];

                title.Text = TabControl1.SelectedTab.Text;
                if (TabControl1.SelectedTab.Tag != null)
                    stepDescription.Text = TabControl1.SelectedTab.Tag.ToString();
                else
                    stepDescription.Text = "";
            }
        }


        private void cmdNext_Click(object sender, EventArgs e)
        {
            var cancel = false;
            if (null != ValidatePage) ValidatePage(CurrentPage, ref cancel);
            if (!cancel) PageIndexChangedDelegate.Invoke(ForwardOffset(CurrentPage));
        }


        private void cmdBack_Click(object sender, EventArgs e)
        {
            PageIndexChangedDelegate.Invoke(PreviousOffset(CurrentPage));
        }


        public int ForwardOffset(int piCurrentPage)
        {
            return piCurrentPage + 1;
        }


        public int PreviousOffset(int piCurrentPage)
        {
            return piCurrentPage - 1;
        }


        private void cmdFinish_Click(object sender, EventArgs e)
        {
            var i = 0;
            var cancel = false;
            for (i = CurrentPage; i <= TabControl1.TabCount; i++)
            {
                if (null != ValidatePage) ValidatePage(i, ref cancel);
                if (cancel)
                    return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = cmdCancel.DialogResult;
            Close();
        }


        public DialogResult ShowDialog(IWin32Window poOwner, int piPageNumber)
        {
            CurrentPage = piPageNumber;
            return base.ShowDialog(poOwner);
        }


        private void PictureBox1_Click(object sender, EventArgs e)
        {
            OpenPage();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenPage();
        }

        private static void OpenPage()
        {
            ProcessUtil.OpenDocument("http://www.febrersoftware.com");
        }

        public void AllowBack(bool pbAllowBack)
        {
            m_bAllowBack = pbAllowBack;
        }

        private void frmWizard_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                DisplayCurrentPage(0);
        }

        #region Delegates

        public delegate void ActivatePageEventHandler(int piPageNumber);

        public delegate void PageIndexChangedDlgt(int piPageIndex);

        public delegate void ValidatePageEventHandler(int piPageNumber, ref bool cancel);

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        public LinkLabel LinkLabel1;
        public PictureBox PictureBox1;

        public TabControl TabControl1;
        internal Panel bottomPanel;
        private Button cmdBack;
        private Button cmdCancel;
        private Button cmdFinish;
        private Button cmdNext;
        private DBEtchedLine dbEtchedLine1;
        protected Panel panelStep;
        public PictureBox picWizard;
        internal Panel sidePanel;
        private Label stepDescription;
        private Label title;
        protected Panel wizardTop;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWizard));
            this.panelStep = new System.Windows.Forms.Panel();
            this.dbEtchedLine1 = new FSFormControls.DBEtchedLine();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.picWizard = new System.Windows.Forms.PictureBox();
            this.wizardTop = new System.Windows.Forms.Panel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.stepDescription = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.cmdFinish = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdBack = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panelStep.SuspendLayout();
            this.sidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWizard)).BeginInit();
            this.wizardTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStep
            // 
            this.panelStep.Controls.Add(this.dbEtchedLine1);
            this.panelStep.Controls.Add(this.TabControl1);
            this.panelStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStep.Location = new System.Drawing.Point(164, 64);
            this.panelStep.Name = "panelStep";
            this.panelStep.Padding = new System.Windows.Forms.Padding(8);
            this.panelStep.Size = new System.Drawing.Size(610, 399);
            this.panelStep.TabIndex = 20;
            // 
            // dbEtchedLine1
            // 
            this.dbEtchedLine1.About = "";
            this.dbEtchedLine1.BackColor = System.Drawing.Color.Transparent;
            this.dbEtchedLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dbEtchedLine1.LinePosition = FSFormControls.DBEtchedLine.DrawPosition.Bottom;
            this.dbEtchedLine1.Location = new System.Drawing.Point(8, 8);
            this.dbEtchedLine1.Name = "dbEtchedLine1";
            this.dbEtchedLine1.Size = new System.Drawing.Size(594, 21);
            this.dbEtchedLine1.Sunken = FSFormControls.DBEtchedLine.DrawType.White;
            this.dbEtchedLine1.TabIndex = 9;
            // 
            // TabControl1
            // 
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Location = new System.Drawing.Point(8, 8);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(594, 383);
            this.TabControl1.TabIndex = 0;
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.White;
            this.sidePanel.Controls.Add(this.picWizard);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 64);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(164, 399);
            this.sidePanel.TabIndex = 19;
            // 
            // picWizard
            // 
            this.picWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWizard.Image = ((System.Drawing.Image)(resources.GetObject("picWizard.Image")));
            this.picWizard.Location = new System.Drawing.Point(0, 0);
            this.picWizard.Name = "picWizard";
            this.picWizard.Size = new System.Drawing.Size(164, 399);
            this.picWizard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWizard.TabIndex = 0;
            this.picWizard.TabStop = false;
            // 
            // wizardTop
            // 
            this.wizardTop.BackColor = System.Drawing.Color.White;
            this.wizardTop.Controls.Add(this.PictureBox1);
            this.wizardTop.Controls.Add(this.stepDescription);
            this.wizardTop.Controls.Add(this.title);
            this.wizardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardTop.Location = new System.Drawing.Point(0, 0);
            this.wizardTop.Name = "wizardTop";
            this.wizardTop.Size = new System.Drawing.Size(774, 64);
            this.wizardTop.TabIndex = 18;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(710, 0);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(57, 55);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBox1.TabIndex = 5;
            this.PictureBox1.TabStop = false;
            // 
            // stepDescription
            // 
            this.stepDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stepDescription.AutoSize = true;
            this.stepDescription.Location = new System.Drawing.Point(16, 24);
            this.stepDescription.Name = "stepDescription";
            this.stepDescription.Size = new System.Drawing.Size(312, 13);
            this.stepDescription.TabIndex = 4;
            this.stepDescription.Text = "Utiliza la propiedad \'Tag\' del TabPage para mostrar aquí el texto.";
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(8, 8);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(381, 13);
            this.title.TabIndex = 3;
            this.title.Text = "Utiliza la propiedad \'Text\' del TabPage para mostrar aquí el texto.";
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.cmdFinish);
            this.bottomPanel.Controls.Add(this.cmdNext);
            this.bottomPanel.Controls.Add(this.cmdBack);
            this.bottomPanel.Controls.Add(this.cmdCancel);
            this.bottomPanel.Controls.Add(this.LinkLabel1);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 463);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(774, 40);
            this.bottomPanel.TabIndex = 17;
            // 
            // cmdFinish
            // 
            this.cmdFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFinish.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdFinish.Location = new System.Drawing.Point(678, 8);
            this.cmdFinish.Name = "cmdFinish";
            this.cmdFinish.Size = new System.Drawing.Size(88, 23);
            this.cmdFinish.TabIndex = 8;
            this.cmdFinish.Text = "Fín";
            // 
            // cmdNext
            // 
            this.cmdNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdNext.Location = new System.Drawing.Point(582, 8);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(88, 23);
            this.cmdNext.TabIndex = 5;
            this.cmdNext.Text = "Adelante >";
            // 
            // cmdBack
            // 
            this.cmdBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdBack.Location = new System.Drawing.Point(486, 8);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(88, 23);
            this.cmdBack.TabIndex = 4;
            this.cmdBack.Text = "< Atras";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancel.Location = new System.Drawing.Point(390, 8);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(88, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancelar";
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LinkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.LinkLabel1.Location = new System.Drawing.Point(16, 16);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(121, 13);
            this.LinkLabel1.TabIndex = 6;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "Febrer Software 2004(c)";
            // 
            // frmWizard
            // 
            this.ClientSize = new System.Drawing.Size(774, 525);
            this.Controls.Add(this.panelStep);
            this.Controls.Add(this.sidePanel);
            this.Controls.Add(this.wizardTop);
            this.Controls.Add(this.bottomPanel);
            this.Name = "frmWizard";
            this.ShowMenu = false;
            this.ShowToolBar = false;
            this.Text = "frmWizard";
            this.Controls.SetChildIndex(this.DbToolBar1, 0);
            this.Controls.SetChildIndex(this.bottomPanel, 0);
            this.Controls.SetChildIndex(this.wizardTop, 0);
            this.Controls.SetChildIndex(this.sidePanel, 0);
            this.Controls.SetChildIndex(this.panelStep, 0);
            this.panelStep.ResumeLayout(false);
            this.sidePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWizard)).EndInit();
            this.wizardTop.ResumeLayout(false);
            this.wizardTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}