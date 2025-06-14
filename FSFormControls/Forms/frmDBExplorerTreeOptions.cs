using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FSFormControls
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	public class frmDBExplorerTreeOptions : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.CheckBox chkMyD;
		private System.Windows.Forms.CheckBox chkMyF;
		private System.Windows.Forms.CheckBox chkMyN;
		private System.Windows.Forms.Label label1;
		
		public bool myDocument =false;
		public bool myFavorite =false;
		public bool myNetwork =false;
		
		public bool myAddressbar =false;
		public bool myToolbar =false;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox chkMyA;
		private System.Windows.Forms.CheckBox chkMyT;
		private System.ComponentModel.IContainer components;

		public frmDBExplorerTreeOptions(bool myD, bool myF, bool myN, bool myA, bool myT)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			myDocument = myD ;
			myFavorite = myF ;
			myNetwork = myN ;
			myAddressbar = myA;
			myToolbar = myT;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmDBExplorerTreeOptions));
            pictureBox1 = new PictureBox();
            okButton = new Button();
            chkMyD = new CheckBox();
            chkMyF = new CheckBox();
            chkMyN = new CheckBox();
            label1 = new Label();
            toolTip1 = new ToolTip(components);
            chkMyT = new CheckBox();
            chkMyA = new CheckBox();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(10, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(67, 59);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.FlatStyle = FlatStyle.System;
            okButton.Location = new Point(150, 146);
            okButton.Name = "okButton";
            okButton.Size = new Size(90, 28);
            okButton.TabIndex = 20;
            okButton.Text = "Ok";
            // 
            // chkMyD
            // 
            chkMyD.Location = new Point(115, 30);
            chkMyD.Name = "chkMyD";
            chkMyD.Size = new Size(125, 29);
            chkMyD.TabIndex = 21;
            chkMyD.Text = "My Documents";
            chkMyD.CheckedChanged += chkMyD_CheckedChanged;
            // 
            // chkMyF
            // 
            chkMyF.Location = new Point(115, 59);
            chkMyF.Name = "chkMyF";
            chkMyF.Size = new Size(125, 30);
            chkMyF.TabIndex = 22;
            chkMyF.Text = "My Favorites";
            chkMyF.CheckedChanged += chkMyF_CheckedChanged;
            // 
            // chkMyN
            // 
            chkMyN.Location = new Point(115, 89);
            chkMyN.Name = "chkMyN";
            chkMyN.Size = new Size(125, 29);
            chkMyN.TabIndex = 23;
            chkMyN.Text = "My Networks ";
            chkMyN.CheckedChanged += chkMyN_CheckedChanged;
            // 
            // label1
            // 
            label1.Location = new Point(86, 10);
            label1.Name = "label1";
            label1.Size = new Size(173, 20);
            label1.TabIndex = 24;
            label1.Text = "Customize Explorer Tree";
            // 
            // chkMyT
            // 
            chkMyT.Location = new Point(240, 59);
            chkMyT.Name = "chkMyT";
            chkMyT.Size = new Size(125, 30);
            chkMyT.TabIndex = 30;
            chkMyT.Text = "Toolbar ";
            chkMyT.CheckedChanged += chkMyT_CheckedChanged;
            // 
            // chkMyA
            // 
            chkMyA.Location = new Point(240, 30);
            chkMyA.Name = "chkMyA";
            chkMyA.Size = new Size(125, 29);
            chkMyA.TabIndex = 29;
            chkMyA.Text = "Addressbar ";
            chkMyA.CheckedChanged += chkMyA_CheckedChanged;
            // 
            // frmDBExplorerTreeOptions
            // 
            AutoScaleBaseSize = new Size(6, 16);
            ClientSize = new Size(373, 198);
            Controls.Add(chkMyT);
            Controls.Add(chkMyA);
            Controls.Add(label1);
            Controls.Add(chkMyN);
            Controls.Add(chkMyF);
            Controls.Add(chkMyD);
            Controls.Add(okButton);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDBExplorerTreeOptions";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Customize Windows Explorer";
            Load += frmOptions_Load;
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private void btnOK_Click(object sender, System.EventArgs e)
		{
			
		}

		private void frmOptions_Load(object sender, System.EventArgs e)
		{
			chkMyD.Checked = myDocument;
			chkMyF.Checked = myFavorite ;
			chkMyN.Checked = myNetwork;
			chkMyA.Checked = myAddressbar;
			chkMyT.Checked = myToolbar;  
		}

		private void chkMyD_CheckedChanged(object sender, System.EventArgs e)
		{
			myDocument =chkMyD.Checked ;
		}

		private void chkMyF_CheckedChanged(object sender, System.EventArgs e)
		{
			myFavorite =chkMyF.Checked ;
		}

		private void chkMyN_CheckedChanged(object sender, System.EventArgs e)
		{
			myNetwork = chkMyN.Checked;   
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			 
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents=false;
			proc.StartInfo.FileName="iexplore";
			proc.StartInfo.Arguments= @"http://www.codeproject.com/script/Articles/list_articles.asp?userid=81898";
			proc.Start();
		}

		private void chkMyA_CheckedChanged(object sender, System.EventArgs e)
		{
			myAddressbar = chkMyA.Checked;  
		}

		private void chkMyT_CheckedChanged(object sender, System.EventArgs e)
		{
//			
//			if (!chkMyT.Checked)
//			{	
//				DialogResult = MessageBox.Show(" You won't be able to customize the settings, once you make the toolbar invisible. Do you want to continue?","Information ExplorerTree",MessageBoxButtons.YesNo ,MessageBoxIcon.Information );
//				if (DialogResult == DialogResult.Yes)  
//					chkMyT.Checked = true;
//				else
//					chkMyT.Checked = false;
//
//			}
			myToolbar = chkMyT.Checked; 



		}
	}
}
