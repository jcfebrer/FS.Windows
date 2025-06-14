#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBPicture.bmp")]
    [ToolboxItem(true)]
    public class DBPicture : DBUserControl
    {
        #region t_PictureType enum

        public enum t_PictureType
        {
            Line,
            Square
        }

        #endregion

        public int m_BorderSize = 1;
        public t_PictureType m_PictureType = t_PictureType.Line;

        public t_PictureType PictureType
        {
            get { return m_PictureType; }
            set
            {
                m_PictureType = value;
                DBPicture_Resize(this, new EventArgs());
            }
        }

        public new BorderStyle BorderStyle
        {
            get { return Label1.BorderStyle; }
            set
            {
                Label1.BorderStyle = value;
                DBPicture_Resize(this, new EventArgs());
            }
        }

        public Color Color
        {
            get { return Label1.BackColor; }
            set { Label1.BackColor = value; }
        }

        public int BorderSize
        {
            get { return m_BorderSize; }
            set
            {
                m_BorderSize = value;
                DBPicture_Resize(this, new EventArgs());
            }
        }

        private void DBPicture_Resize(object sender, EventArgs e)
        {
            switch (m_PictureType)
            {
                case t_PictureType.Line:
                    Label1.Height = m_BorderSize;
                    Label1.Left = 0;
                    Label1.Top = 0;
                    Label1.Width = Width;
                    Height = m_BorderSize;
                    break;
                case t_PictureType.Square:
                    Label1.Height = Height;
                    Label1.Left = 0;
                    Label1.Top = 0;
                    Label1.Width = Width;
                    break;
            }
        }


        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void Label1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal Label Label1;

        public DBPicture()
        {
            InitializeComponent();

            TabStop = false;

            Resize += DBPicture_Resize;
            Label1.MouseDown += Label1_MouseDown;
            Label1.MouseUp += Label1_MouseUp;
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
            Label1 = new Label();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.BorderStyle = BorderStyle.Fixed3D;
            Label1.Location = new Point(56, 40);
            Label1.Name = "Label1";
            Label1.Size = new Size(40, 3);
            Label1.TabIndex = 0;
            // 
            // DBPicture
            // 
            Controls.Add(Label1);
            Name = "DBPicture";
            Size = new Size(154, 143);
            ResumeLayout(false);
        }

        #endregion
    }
}