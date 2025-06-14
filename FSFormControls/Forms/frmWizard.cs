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
            var resources = new ComponentResourceManager(typeof(frmWizard));
            panelStep = new Panel();
            dbEtchedLine1 = new DBEtchedLine();
            TabControl1 = new TabControl();
            sidePanel = new Panel();
            picWizard = new PictureBox();
            wizardTop = new Panel();
            PictureBox1 = new PictureBox();
            stepDescription = new Label();
            title = new Label();
            bottomPanel = new Panel();
            cmdFinish = new Button();
            cmdNext = new Button();
            cmdBack = new Button();
            cmdCancel = new Button();
            LinkLabel1 = new LinkLabel();
            panelStep.SuspendLayout();
            sidePanel.SuspendLayout();
            ((ISupportInitialize) picWizard).BeginInit();
            wizardTop.SuspendLayout();
            ((ISupportInitialize) PictureBox1).BeginInit();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mnuForm
            // 
            //mnuForm.OwnerDraw = true;
            mnuForm.Visible = false;
            // 
            // panelStep
            // 
            panelStep.Controls.Add(dbEtchedLine1);
            panelStep.Controls.Add(TabControl1);
            panelStep.Dock = DockStyle.Fill;
            panelStep.Location = new Point(164, 64);
            panelStep.Name = "panelStep";
            panelStep.Padding = new System.Windows.Forms.Padding(8);
            panelStep.Size = new Size(527, 85);
            panelStep.TabIndex = 20;
            // 
            // dbEtchedLine1
            // 
            dbEtchedLine1.BackColor = Color.Transparent;
            dbEtchedLine1.Dock = DockStyle.Top;
            dbEtchedLine1.LinePosition = DBEtchedLine.DrawPosition.Bottom;
            dbEtchedLine1.Location = new Point(8, 8);
            dbEtchedLine1.Name = "dbEtchedLine1";
            dbEtchedLine1.Size = new Size(511, 21);
            dbEtchedLine1.Sunken = DBEtchedLine.DrawType.White;
            dbEtchedLine1.TabIndex = 9;
            // 
            // TabControl1
            // 
            TabControl1.Dock = DockStyle.Fill;
            TabControl1.Location = new Point(8, 8);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(511, 69);
            TabControl1.TabIndex = 0;
            // 
            // sidePanel
            // 
            sidePanel.BackColor = Color.White;
            sidePanel.Controls.Add(picWizard);
            sidePanel.Dock = DockStyle.Left;
            sidePanel.Location = new Point(0, 64);
            sidePanel.Name = "sidePanel";
            sidePanel.Size = new Size(164, 85);
            sidePanel.TabIndex = 19;
            // 
            // picWizard
            // 
            picWizard.Dock = DockStyle.Fill;
            picWizard.Image = (Image) resources.GetObject("picWizard.Image");
            picWizard.Location = new Point(0, 0);
            picWizard.Name = "picWizard";
            picWizard.Size = new Size(164, 85);
            picWizard.SizeMode = PictureBoxSizeMode.StretchImage;
            picWizard.TabIndex = 0;
            picWizard.TabStop = false;
            // 
            // wizardTop
            // 
            wizardTop.BackColor = Color.White;
            wizardTop.Controls.Add(PictureBox1);
            wizardTop.Controls.Add(stepDescription);
            wizardTop.Controls.Add(title);
            wizardTop.Dock = DockStyle.Top;
            wizardTop.Location = new Point(0, 0);
            wizardTop.Name = "wizardTop";
            wizardTop.Size = new Size(691, 64);
            wizardTop.TabIndex = 18;
            // 
            // PictureBox1
            // 
            PictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PictureBox1.Cursor = Cursors.Hand;
            PictureBox1.Image = (Image) resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(627, 0);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(57, 55);
            PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox1.TabIndex = 5;
            PictureBox1.TabStop = false;
            // 
            // stepDescription
            // 
            stepDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                      | AnchorStyles.Left
                                                      | AnchorStyles.Right;
            stepDescription.AutoSize = true;
            stepDescription.Location = new Point(16, 24);
            stepDescription.Name = "stepDescription";
            stepDescription.Size = new Size(312, 13);
            stepDescription.TabIndex = 4;
            stepDescription.Text = "Utiliza la propiedad \'Tag\' del TabPage para mostrar aquí el texto.";
            // 
            // title
            // 
            title.Anchor = AnchorStyles.Top | AnchorStyles.Left
                                            | AnchorStyles.Right;
            title.AutoSize = true;
            title.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title.Location = new Point(8, 8);
            title.Name = "title";
            title.Size = new Size(381, 13);
            title.TabIndex = 3;
            title.Text = "Utiliza la propiedad \'Text\' del TabPage para mostrar aquí el texto.";
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(cmdFinish);
            bottomPanel.Controls.Add(cmdNext);
            bottomPanel.Controls.Add(cmdBack);
            bottomPanel.Controls.Add(cmdCancel);
            bottomPanel.Controls.Add(LinkLabel1);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 149);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(691, 40);
            bottomPanel.TabIndex = 17;
            // 
            // cmdFinish
            // 
            cmdFinish.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdFinish.FlatStyle = FlatStyle.System;
            cmdFinish.Location = new Point(595, 8);
            cmdFinish.Name = "cmdFinish";
            cmdFinish.Size = new Size(88, 23);
            cmdFinish.TabIndex = 8;
            cmdFinish.Text = "Fín";
            // 
            // cmdNext
            // 
            cmdNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdNext.FlatStyle = FlatStyle.System;
            cmdNext.Location = new Point(499, 8);
            cmdNext.Name = "cmdNext";
            cmdNext.Size = new Size(88, 23);
            cmdNext.TabIndex = 5;
            cmdNext.Text = "Adelante >";
            // 
            // cmdBack
            // 
            cmdBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdBack.FlatStyle = FlatStyle.System;
            cmdBack.Location = new Point(403, 8);
            cmdBack.Name = "cmdBack";
            cmdBack.Size = new Size(88, 23);
            cmdBack.TabIndex = 4;
            cmdBack.Text = "< Atras";
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.FlatStyle = FlatStyle.System;
            cmdCancel.Location = new Point(307, 8);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(88, 23);
            cmdCancel.TabIndex = 3;
            cmdCancel.Text = "Cancelar";
            // 
            // LinkLabel1
            // 
            LinkLabel1.AutoSize = true;
            LinkLabel1.Cursor = Cursors.Hand;
            LinkLabel1.LinkColor = Color.Blue;
            LinkLabel1.Location = new Point(16, 16);
            LinkLabel1.Name = "LinkLabel1";
            LinkLabel1.Size = new Size(121, 13);
            LinkLabel1.TabIndex = 6;
            LinkLabel1.TabStop = true;
            LinkLabel1.Text = "Febrer Software 2004(c)";
            // 
            // frmWizard
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(691, 189);
            Controls.Add(panelStep);
            Controls.Add(sidePanel);
            Controls.Add(wizardTop);
            Controls.Add(bottomPanel);
            Name = "frmWizard";
            ShowMenu = false;
            //ShowStatusBar = false;
            ShowToolBar = false;
            Text = "frmWizard";
            Controls.SetChildIndex(bottomPanel, 0);
            Controls.SetChildIndex(wizardTop, 0);
            Controls.SetChildIndex(sidePanel, 0);
            Controls.SetChildIndex(panelStep, 0);
            panelStep.ResumeLayout(false);
            sidePanel.ResumeLayout(false);
            ((ISupportInitialize) picWizard).EndInit();
            wizardTop.ResumeLayout(false);
            wizardTop.PerformLayout();
            ((ISupportInitialize) PictureBox1).EndInit();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}