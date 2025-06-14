using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace FSFormControls
{
    partial class DBPlannerObject : System.Windows.Forms.UserControl 
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
            this.SuspendLayout();
            // 
            // DBPlannerObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "DBPlannerObject";
            this.Size = new System.Drawing.Size(398, 48);
            this.ResumeLayout(false);

        } 
        
        
    } 
    
    
} 
