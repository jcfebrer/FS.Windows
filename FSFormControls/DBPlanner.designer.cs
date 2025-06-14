using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace FSFormControls
{
    partial class DBPlanner : DBUserControl 
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
            this.DbPanel1 = new FSFormControls.DBPanel();
            this.DbPanel2 = new FSFormControls.DBPanel();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DbPanel1
            // 
            this.DbPanel1.AutoScroll = true;
            this.DbPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbPanel1.Location = new System.Drawing.Point(0, 0);
            this.DbPanel1.Name = "DbPanel1";
            this.DbPanel1.Size = new System.Drawing.Size(230, 323);
            this.DbPanel1.TabIndex = 0;
            // 
            // DbPanel2
            // 
            this.DbPanel2.AutoScroll = true;
            this.DbPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DbPanel2.Location = new System.Drawing.Point(0, 0);
            this.DbPanel2.Name = "DbPanel2";
            this.DbPanel2.Size = new System.Drawing.Size(446, 323);
            this.DbPanel2.TabIndex = 1;
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.DbPanel1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.DbPanel2);
            this.SplitContainer1.Size = new System.Drawing.Size(680, 323);
            this.SplitContainer1.SplitterDistance = 230;
            this.SplitContainer1.TabIndex = 2;
            // 
            // DBPlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer1);
            this.Name = "DBPlanner";
            this.Size = new System.Drawing.Size(680, 323);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        } 
        
        internal FSFormControls.DBPanel DbPanel1; 
        internal FSFormControls.DBPanel DbPanel2; 
        internal System.Windows.Forms.SplitContainer SplitContainer1; 
        
    } 
    
    
} 
