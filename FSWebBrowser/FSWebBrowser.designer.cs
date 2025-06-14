using FSFormControls;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FSWebBrowser
{
    partial class Browser : DBUserControl 
    { 
        
        [ System.Diagnostics.DebuggerNonUserCode() ]
        protected override void Dispose( bool disposing ) 
        { 
            if ( disposing && components != null ) 
            { 
                components.Dispose(); 
            } 
            base.Dispose( disposing ); 
        } 
        
        
        private System.ComponentModel.IContainer components = null; 
        
        [ System.Diagnostics.DebuggerStepThrough() ]
        private void InitializeComponent() 
        {
            this.WebBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // WebBrowser1
            // 
            this.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser1.Name = "WebBrowser1";
            this.WebBrowser1.Size = new System.Drawing.Size(155, 146);
            this.WebBrowser1.TabIndex = 0;
            // 
            // DBWebBrowser
            // 
            this.Controls.Add(this.WebBrowser1);
            this.Name = "DBWebBrowser";
            this.Size = new System.Drawing.Size(155, 146);
            this.ResumeLayout(false);

        } 
        
        internal System.Windows.Forms.WebBrowser WebBrowser1;       
    } 
    
    
} 
