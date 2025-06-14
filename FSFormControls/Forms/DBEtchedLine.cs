#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    /// <summary>
    ///     Summary description for UserControl1.
    /// </summary>
    public class DBEtchedLine : DBUserControl
    {
        #region DrawPosition enum

        public enum DrawPosition
        {
            Top,
            Middle,
            Bottom
        }

        #endregion

        #region DrawType enum

        public enum DrawType
        {
            Etched,
            Raised,
            White,
            Gray
        }

        #endregion

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public DBEtchedLine()
        {
            InitializeComponent();

            Paint += EtchedLine_Paint;
            SizeChanged += EtchedLine_SizeChanged;
        }

        [DefaultValue(DrawType.Etched)] public DrawType Sunken { get; set; } = DrawType.Etched;


        [DefaultValue(DrawPosition.Middle)] public DrawPosition LinePosition { get; set; } = DrawPosition.Middle;

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        private void EtchedLine_Paint(object sender, PaintEventArgs e)
        {
            var iStartHeight = 0;

            if (LinePosition == DrawPosition.Middle)
                iStartHeight = Height / 2;
            else if (LinePosition == DrawPosition.Bottom)
                iStartHeight = Height - 1;

            switch (Sunken)
            {
                case DrawType.Etched:
                    e.Graphics.DrawLine(Pens.White, new Point(0, iStartHeight - 1), new Point(Width, iStartHeight - 1));
                    e.Graphics.DrawLine(Pens.Gray, new Point(0, iStartHeight), new Point(Width, iStartHeight));
                    break;

                case DrawType.Raised:
                    e.Graphics.DrawLine(Pens.Gray, new Point(0, iStartHeight - 1), new Point(Width, iStartHeight - 1));
                    e.Graphics.DrawLine(Pens.White, new Point(0, iStartHeight), new Point(Width, iStartHeight));
                    break;

                case DrawType.White:
                    e.Graphics.DrawLine(Pens.White, new Point(0, iStartHeight), new Point(Width, iStartHeight));
                    break;

                case DrawType.Gray:
                    e.Graphics.DrawLine(Pens.Gray, new Point(0, iStartHeight), new Point(Width, iStartHeight));
                    break;
            }
        }

        private void EtchedLine_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region Component Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DBEtchedLine
            // 
            this.Name = "DBEtchedLine";
            this.Size = new System.Drawing.Size(198, 10);
            this.ResumeLayout(false);
        }

        #endregion
    }
}