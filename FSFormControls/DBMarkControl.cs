#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public class DBMarkControl : DBUserControl
    {
        public bool IsMoveMark;
        public int m_SizeX = 9;
        public int m_SizeY = 9;

        public Point Center =>
            new Point(Convert.ToInt32(Location.X + (m_SizeX - 1) / 2),
                Convert.ToInt32(Location.Y + (m_SizeY - 1) / 2));

        public int X => Location.X;

        public int Y => Location.Y;

        public int SizeX
        {
            get { return m_SizeX; }
            set
            {
                m_SizeX = value;
                Width = m_SizeX;
            }
        }

        public int SizeY
        {
            get { return m_SizeY; }
            set
            {
                m_SizeY = value;
                Height = m_SizeY;
            }
        }

        private void MarkControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMoveMark)
                Cursor = Cursors.SizeAll;
            else
                Cursor = Cursors.SizeNWSE;
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        public DBMarkControl(int sizeX, int sizeY)
        {
            InitializeComponent();

            m_SizeX = sizeX;
            m_SizeY = sizeY;
            base.BackColor = Color.Black;
            Height = m_SizeY;
            Width = m_SizeX;

            MouseMove += MarkControl_MouseMove;
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
            this.SuspendLayout();
            // 
            // DBMarkControl
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Name = "DBMarkControl";
            this.Size = new System.Drawing.Size(34, 26);
            this.ResumeLayout(false);

        }

        #endregion
    }
}