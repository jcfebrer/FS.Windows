using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace FSFormControls
{
    partial class frmFind : System.Windows.Forms.Form 
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
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.WholeWord = new System.Windows.Forms.CheckBox();
            this.MatchCase = new System.Windows.Forms.CheckBox();
            this.ReplaceTextBox = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.FindTextBox = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdFind = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnReplaceAll.Location = new System.Drawing.Point(152, 131);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(105, 24);
            this.btnReplaceAll.TabIndex = 15;
            this.btnReplaceAll.Text = "Reemplazar todo";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 24);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(263, 131);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(86, 24);
            this.btnReplace.TabIndex = 13;
            this.btnReplace.Text = "Reemplazar";
            this.btnReplace.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.WholeWord);
            this.GroupBox1.Controls.Add(this.MatchCase);
            this.GroupBox1.Location = new System.Drawing.Point(17, 79);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(129, 79);
            this.GroupBox1.TabIndex = 12;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Opciones";
            // 
            // WholeWord
            // 
            this.WholeWord.AutoSize = true;
            this.WholeWord.Location = new System.Drawing.Point(14, 52);
            this.WholeWord.Name = "WholeWord";
            this.WholeWord.Size = new System.Drawing.Size(108, 17);
            this.WholeWord.TabIndex = 4;
            this.WholeWord.Text = "Palabra completa";
            this.WholeWord.UseVisualStyleBackColor = true;
            // 
            // MatchCase
            // 
            this.MatchCase.AutoSize = true;
            this.MatchCase.Location = new System.Drawing.Point(14, 29);
            this.MatchCase.Name = "MatchCase";
            this.MatchCase.Size = new System.Drawing.Size(96, 17);
            this.MatchCase.TabIndex = 3;
            this.MatchCase.Text = "Mayús / Minús";
            this.MatchCase.UseVisualStyleBackColor = true;
            // 
            // ReplaceTextBox
            // 
            this.ReplaceTextBox.Location = new System.Drawing.Point(92, 41);
            this.ReplaceTextBox.Name = "ReplaceTextBox";
            this.ReplaceTextBox.Size = new System.Drawing.Size(349, 20);
            this.ReplaceTextBox.TabIndex = 10;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(14, 44);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(66, 13);
            this.Label2.TabIndex = 11;
            this.Label2.Text = "Reemplazar:";
            // 
            // FindTextBox
            // 
            this.FindTextBox.Location = new System.Drawing.Point(92, 12);
            this.FindTextBox.Name = "FindTextBox";
            this.FindTextBox.Size = new System.Drawing.Size(349, 20);
            this.FindTextBox.TabIndex = 8;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(14, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(43, 13);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Buscar:";
            // 
            // cmdFind
            // 
            this.cmdFind.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdFind.Location = new System.Drawing.Point(152, 101);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(86, 24);
            this.cmdFind.TabIndex = 16;
            this.cmdFind.Text = "Buscar";
            this.cmdFind.UseVisualStyleBackColor = true;
            // 
            // frmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 170);
            this.Controls.Add(this.cmdFind);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.ReplaceTextBox);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.FindTextBox);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmFind";
            this.Text = "Buscar y reemplazar";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        } 
        
        internal System.Windows.Forms.Button btnReplaceAll; 
        internal System.Windows.Forms.Button btnCancel; 
        internal System.Windows.Forms.Button btnReplace; 
        internal System.Windows.Forms.GroupBox GroupBox1; 
        internal System.Windows.Forms.CheckBox WholeWord; 
        internal System.Windows.Forms.CheckBox MatchCase; 
        internal System.Windows.Forms.TextBox ReplaceTextBox; 
        internal System.Windows.Forms.Label Label2; 
        internal System.Windows.Forms.TextBox FindTextBox; 
        internal System.Windows.Forms.Label Label1; 
        internal System.Windows.Forms.Button cmdFind; 
    } 
    
    
} 
