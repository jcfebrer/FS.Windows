#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FSDatabase;
using FSLibrary;
using FSException;
using FSSecurity;

#endregion

namespace FSFormControls
{
    public class frmAbout : Form
    {
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.febrersoftware.com");
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void frmAbout_Load(object sender, EventArgs e)
        {
            string[] fileNames = null;

            try
            {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                fileNames = Directory.GetFiles(assemblyPath);

                foreach (var fileName in fileNames)
                {
                    ListViewItem item = null;
                    item = lstAssemblies.Items.Add(fileName);

                    if (fileName.EndsWith(".dll"))
                        item.SubItems.Add(Assembly.LoadFrom(fileName).GetName().Version.ToString());
                }

                FillEnvironment();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void FillEnvironment()
        {
            ListView1.View = View.Details;
            ListView1.FullRowSelect = true;
            ListView1.GridLines = true;
            ListView1.LabelEdit = false;
            ListView1.Columns.Clear();
            ListView1.Columns.Add("Propiedad", 170, HorizontalAlignment.Left);
            ListView1.Columns.Add("Valor", 300, HorizontalAlignment.Left);


            ListViewItem lvItem = null;
            var i = 0;
            lvItem = ListView1.Items.Add("CommandLine");
            lvItem.SubItems.Add(Environment.CommandLine);
            lvItem = ListView1.Items.Add("CurrentDirectory");
            lvItem.SubItems.Add(Environment.CurrentDirectory);
            lvItem = ListView1.Items.Add("GetCommandLineArgs.Length");
            lvItem.SubItems.Add(Environment.GetCommandLineArgs().Length.ToString());
            for (i = 0; i <= Environment.GetCommandLineArgs().Length - 1; i++)
            {
                lvItem = ListView1.Items.Add("GetCommandLineArgs(" + i + ")");
                lvItem.SubItems.Add(Environment.GetCommandLineArgs()[i]);
            }

            lvItem = ListView1.Items.Add("GetEnvironmentVariables.App.Count");
            lvItem.SubItems.Add(Environment.GetEnvironmentVariables().Count.ToString());
            foreach (DictionaryEntry tDE in Environment.GetEnvironmentVariables())
            {
                lvItem = ListView1.Items.Add("...(" + tDE.Key + ")");
                lvItem.SubItems.Add(tDE.Value.ToString());
            }

            lvItem = ListView1.Items.Add("GetFolderPath(SpecialFolder. ...)");
            lvItem.SubItems.Add(" ");
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.ApplicationData + ")");
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.CommonProgramFiles);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Cookies);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.DesktopDirectory);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Favorites);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.History);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.History));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.InternetCache);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.LocalApplicationData);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Personal);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.ProgramFiles);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Programs);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Programs));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Recent);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Recent));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.SendTo);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.SendTo));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.StartMenu);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Startup);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.System);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.System));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Templates);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Templates));
            lvItem = ListView1.Items.Add("GetLogicalDrives.Length");
            lvItem.SubItems.Add(Environment.GetLogicalDrives().Length.ToString());
            for (i = 0; i <= Environment.GetLogicalDrives().Length - 1; i++)
            {
                lvItem = ListView1.Items.Add("GetLogicalDrives(" + i + ")");
                lvItem.SubItems.Add(Environment.GetLogicalDrives()[i]);
            }

            lvItem = ListView1.Items.Add("MachineName");
            lvItem.SubItems.Add(Environment.MachineName);

            lvItem = ListView1.Items.Add("OSVersion.Platform");
            lvItem.SubItems.Add(Environment.OSVersion.Platform.ToString());

            lvItem = ListView1.Items.Add("OSVersion.Version");
            lvItem.SubItems.Add(Environment.OSVersion.Version.ToString());

            lvItem = ListView1.Items.Add("SystemDirectory");
            lvItem.SubItems.Add(Environment.SystemDirectory);

            lvItem = ListView1.Items.Add("TickCount");
            lvItem.SubItems.Add(Environment.TickCount.ToString("###,###"));

            lvItem = ListView1.Items.Add("UserDomainName");
            lvItem.SubItems.Add(Environment.UserDomainName);

            lvItem = ListView1.Items.Add("UserName");
            lvItem.SubItems.Add(Environment.UserName);

            lvItem = ListView1.Items.Add("UserInteractive");
            lvItem.SubItems.Add(Environment.UserInteractive.ToString());

            lvItem = ListView1.Items.Add("CLR Version");
            lvItem.SubItems.Add(Environment.Version.ToString());

            lvItem = ListView1.Items.Add("WorkingSet (memoria asignada)");
            lvItem.SubItems.Add(Environment.WorkingSet.ToString("###,###"));

            if (Global.ConnectionString != null)
            {
                lvItem = ListView1.Items.Add("ConnectionString");
                lvItem.SubItems.Add(Global.ConnectionString);

                //lvItem = ListView1.Items.Add("ProviderName");
                //lvItem.SubItems.Add(Global.ConnectionString.ProviderName);
            }

            //lvItem = ListView1.Items.Add("MDAC Version");
            //lvItem.SubItems.Add(Utils.GetMdacVersion().ToString());

            lvItem = ListView1.Items.Add("Compilation Size");
            lvItem.SubItems.Add(NumberUtils.CompilationSize());

            //lvItem = ListView1.Items.Add("Permisos registro");
            //lvItem.SubItems.Add(Permission.Permissions());

            lvItem = ListView1.Items.Add("StackTrace...");
            foreach (var s in Environment.StackTrace.Split(char.Parse(Global.Lf)))
            {
                lvItem.SubItems.Add(s);
            }
        }


        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.febrersoftware.com");
        }

        private void cmdInfo_Click(object sender, EventArgs e)
        {
            Process.Start("msinfo32.exe");
        }


        private void cmdEntorno_Click(object sender, EventArgs e)
        {
            switch (cmdEntorno.Text)
            {
                case "Entorno":
                    cmdEntorno.Text = "Dll's";
                    ListView1.BringToFront();
                    break;
                case "Dll's":
                    cmdEntorno.Text = "Entorno";
                    lstAssemblies.BringToFront();
                    break;
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        internal ColumnHeader ColumnHeader1;
        internal ColumnHeader ColumnHeader2;

        internal Label Label1;
        internal Label Label2;
        internal Label Label3;
        internal Label Label4;
        internal Label Label5;
        internal LinkLabel LinkLabel1;
        internal ListView ListView1;
        internal PictureBox logoNET;
        internal PictureBox logoFebrerSoftware;
        internal Button cmdClose;
        internal Button cmdEntorno;
        internal Button cmdInfo;
        internal ColumnHeader colName;
        internal ColumnHeader colVersion;
        internal ListView lstAssemblies;

        public frmAbout()
        {
            InitializeComponent();

            LinkLabel1.TabStop = false;

            logoFebrerSoftware.Click += PictureBox2_Click;
            logoNET.DoubleClick += LogoNET_DoubleClick;
            LinkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            cmdClose.Click += cmdClose_Click;
            cmdInfo.Click += cmdInfo_Click;
            cmdEntorno.Click += cmdEntorno_Click;
            Load += frmAbout_Load;
        }

        private void LogoNET_DoubleClick(object sender, EventArgs e)
        {
            frmTest frm = new frmTest();
            frm.Show();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.logoFebrerSoftware = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lstAssemblies = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdInfo = new System.Windows.Forms.Button();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListView1 = new System.Windows.Forms.ListView();
            this.cmdEntorno = new System.Windows.Forms.Button();
            this.logoNET = new System.Windows.Forms.PictureBox();
            this.Label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoFebrerSoftware)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoNET)).BeginInit();
            this.SuspendLayout();
            // 
            // logoFebrerSoftware
            // 
            this.logoFebrerSoftware.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logoFebrerSoftware.ForeColor = System.Drawing.Color.Black;
            this.logoFebrerSoftware.Image = ((System.Drawing.Image)(resources.GetObject("logoFebrerSoftware.Image")));
            this.logoFebrerSoftware.Location = new System.Drawing.Point(16, 8);
            this.logoFebrerSoftware.Name = "logoFebrerSoftware";
            this.logoFebrerSoftware.Size = new System.Drawing.Size(240, 63);
            this.logoFebrerSoftware.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logoFebrerSoftware.TabIndex = 8;
            this.logoFebrerSoftware.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.White;
            this.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Label2.Location = new System.Drawing.Point(16, 452);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(156, 13);
            this.Label2.TabIndex = 11;
            this.Label2.Text = "juancarlos@febrersoftware.com";
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.BackColor = System.Drawing.Color.White;
            this.LinkLabel1.Location = new System.Drawing.Point(16, 436);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(155, 13);
            this.LinkLabel1.TabIndex = 9;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "http://www.febrersoftware.com";
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            this.Label1.Location = new System.Drawing.Point(459, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(98, 15);
            this.Label1.TabIndex = 14;
            this.Label1.Text = "Bº Marusas Nº5 Lonja";
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            this.Label4.Location = new System.Drawing.Point(451, 56);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(113, 15);
            this.Label4.TabIndex = 15;
            this.Label4.Text = "48610 - Urduliz (Vizcaya)";
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(474, 23);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(76, 15);
            this.Label5.TabIndex = 16;
            this.Label5.Text = "Tfno: 629237109";
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.BackColor = System.Drawing.SystemColors.Control;
            this.cmdClose.Location = new System.Drawing.Point(626, 436);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(71, 24);
            this.cmdClose.TabIndex = 17;
            this.cmdClose.Text = "Cerrar";
            this.cmdClose.UseVisualStyleBackColor = false;
            // 
            // lstAssemblies
            // 
            this.lstAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAssemblies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstAssemblies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colVersion});
            this.lstAssemblies.FullRowSelect = true;
            this.lstAssemblies.GridLines = true;
            this.lstAssemblies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstAssemblies.HideSelection = false;
            this.lstAssemblies.Location = new System.Drawing.Point(16, 88);
            this.lstAssemblies.MultiSelect = false;
            this.lstAssemblies.Name = "lstAssemblies";
            this.lstAssemblies.Size = new System.Drawing.Size(683, 292);
            this.lstAssemblies.TabIndex = 18;
            this.lstAssemblies.UseCompatibleStateImageBehavior = false;
            this.lstAssemblies.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Nombre";
            this.colName.Width = 369;
            // 
            // colVersion
            // 
            this.colVersion.Text = "Versión";
            this.colVersion.Width = 80;
            // 
            // cmdInfo
            // 
            this.cmdInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInfo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdInfo.Location = new System.Drawing.Point(474, 436);
            this.cmdInfo.Name = "cmdInfo";
            this.cmdInfo.Size = new System.Drawing.Size(143, 24);
            this.cmdInfo.TabIndex = 19;
            this.cmdInfo.Text = "Información del Sistema";
            this.cmdInfo.UseVisualStyleBackColor = false;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Nombre";
            this.ColumnHeader1.Width = 369;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Versión";
            this.ColumnHeader2.Width = 80;
            // 
            // ListView1
            // 
            this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2});
            this.ListView1.FullRowSelect = true;
            this.ListView1.GridLines = true;
            this.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView1.HideSelection = false;
            this.ListView1.Location = new System.Drawing.Point(16, 88);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(683, 292);
            this.ListView1.TabIndex = 20;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            // 
            // cmdEntorno
            // 
            this.cmdEntorno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEntorno.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEntorno.Location = new System.Drawing.Point(386, 436);
            this.cmdEntorno.Name = "cmdEntorno";
            this.cmdEntorno.Size = new System.Drawing.Size(80, 24);
            this.cmdEntorno.TabIndex = 21;
            this.cmdEntorno.Text = "Entorno";
            this.cmdEntorno.UseVisualStyleBackColor = false;
            // 
            // logoNET
            // 
            this.logoNET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logoNET.Image = ((System.Drawing.Image)(resources.GetObject("logoNET.Image")));
            this.logoNET.Location = new System.Drawing.Point(596, 16);
            this.logoNET.Name = "logoNET";
            this.logoNET.Size = new System.Drawing.Size(112, 60);
            this.logoNET.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logoNET.TabIndex = 22;
            this.logoNET.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label3.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(16, 380);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(683, 48);
            this.Label3.TabIndex = 23;
            this.Label3.Text = resources.GetString("Label3.Text");
            // 
            // frmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(717, 471);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.logoNET);
            this.Controls.Add(this.cmdEntorno);
            this.Controls.Add(this.cmdInfo);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.logoFebrerSoftware);
            this.Controls.Add(this.lstAssemblies);
            this.Controls.Add(this.ListView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(524, 332);
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Febrer Software";
            ((System.ComponentModel.ISupportInitialize)(this.logoFebrerSoftware)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoNET)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}