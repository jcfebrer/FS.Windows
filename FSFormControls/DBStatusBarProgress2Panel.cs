using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace FSFormControls
{
    /// <summary>
    ///     Status Bar Panel that Displays Progress
    /// </summary>
    public class DBStatusBarProgress2Panel : ToolStripStatusLabel
    {
        #region Component Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion

        #region Step

        /// <summary>
        ///     Promotes the progress bar by one step
        /// </summary>
        public void Step()
        {
            if (!_drawEventRegistered)
            {
                //Parent.DrawItem += Parent_DrawItem;
                _drawEventRegistered = true;
            }

            if (IsAnimated)
            {
                if (_increasing)
                {
                    ProgressPosition += StepSize;

                    if (ProgressPosition >= EndPoint) _increasing = false;
                }
                else
                {
                    ProgressPosition -= StepSize;

                    if (ProgressPosition <= StartPoint) _increasing = true;
                }
            }
            else if (ProgressPosition < EndPoint)
            {
                ProgressPosition += StepSize;
            }

            Parent.Invoke(_refreshDelegate);
        }

        #endregion

        #region Refresh

        /// <summary>
        ///     Refreshes the progress bar
        /// </summary>
        public void Refresh()
        {
            Parent.Refresh();
        }

        #endregion

        #region Reset

        /// <summary>
        ///     Reinitializes the progress bar
        /// </summary>
        public void Reset()
        {
            StopAnimation();
            ProgressPosition = StartPoint;

            Parent.Invoke(_refreshDelegate);
        }

        #endregion

        #region Owner-Draw

        ///// <summary>
        /////     Owner-Draw Event
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="sbdevent"></param>
        //private void Parent_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
        //{
        //    if (sbdevent.Panel == this)
        //    {
        //        sbdevent.DrawBackground();

        //        if (ProgressPosition != StartPoint)
        //            if (ProgressPosition <= EndPoint || AnimationStyle == ProgressDisplayStyle.Infinite)
        //            {
        //                var bounds = sbdevent.Bounds;
        //                var percent = ProgressPosition / (EndPoint - (float) StartPoint);

        //                switch (AnimationStyle)
        //                {
        //                    case ProgressDisplayStyle.LeftToRight:
        //                    {
        //                        bounds.Width = (int) (percent * sbdevent.Bounds.Width);
        //                        break;
        //                    }
        //                    case ProgressDisplayStyle.RightToLeft:
        //                    {
        //                        bounds.Width = (int) (percent * sbdevent.Bounds.Width);
        //                        bounds.X += sbdevent.Bounds.Width - bounds.Width;
        //                        break;
        //                    }
        //                    case ProgressDisplayStyle.BottomToTop:
        //                    {
        //                        bounds.Height = (int) (percent * sbdevent.Bounds.Height);
        //                        bounds.Y += sbdevent.Bounds.Height - bounds.Height;
        //                        break;
        //                    }
        //                    case ProgressDisplayStyle.TopToBottom:
        //                    {
        //                        bounds.Height = (int) (percent * sbdevent.Bounds.Height);
        //                        break;
        //                    }
        //                    case ProgressDisplayStyle.Infinite:
        //                    {
        //                        bounds.Height = (int) (percent * sbdevent.Bounds.Height);
        //                        bounds.Y += (sbdevent.Bounds.Height - bounds.Height) / 2;
        //                        bounds.Width = (int) (percent * sbdevent.Bounds.Width);
        //                        bounds.X += (sbdevent.Bounds.Width - bounds.Width) / 2;
        //                        break;
        //                    }
        //                }

        //                // draw the progress bar
        //                sbdevent.Graphics.FillRectangle(ProgressDrawStyle, bounds);

        //                if (ShowText)
        //                {
        //                    var sf = new StringFormat();
        //                    sf.LineAlignment = StringAlignment.Center;
        //                    sf.Alignment = StringAlignment.Center;

        //                    // draw the text on top of the progress bar
        //                    sbdevent.Graphics.DrawString(Convert.ToInt32(percent * 100) + "%", TextFont, TextDrawStyle,
        //                        sbdevent.Bounds, sf);
        //                }
        //            }
        //    }
        //}

        #endregion

        #region Delegates

        private delegate void RefreshDelegate();

        #endregion

        #region Member Variables

        private readonly RefreshDelegate _refreshDelegate;

        private bool _drawEventRegistered;

        private Thread _animationThread;

        /// <summary>
        ///     Flag used by Infiniate Style
        /// </summary>
        private bool _increasing;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private Container components;

        #endregion

        #region Construction / Destruction

        /// <summary>
        ///     Creates a new StatusBarProgressPanel
        /// </summary>
        public DBStatusBarProgress2Panel()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            _drawEventRegistered = false;

            AnimationStyle = ProgressDisplayStyle.Infinite;

            ProgressPosition = 0;
            StepSize = 10;
            StartPoint = 0;
            EndPoint = 100;

            ShowText = true;
            TextFont = new Font("Arial", 8);
            TextDrawStyle = SystemBrushes.ControlText;

            ProgressDrawStyle = SystemBrushes.Highlight;

            _increasing = true;

            AnimationTick = TimeSpan.FromSeconds(0.5);
            InitializeAnimationThread();

            _refreshDelegate = Refresh;
        }

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

        #endregion

        #region Properties

        /// <summary>
        ///     The method used when drawing the progress bar
        /// </summary>
        [Category("Animation")]
        public ProgressDisplayStyle AnimationStyle { get; set; }

        /// <summary>
        ///     Timespan between infinate progress animation changes
        /// </summary>
        [Category("Animation")]
        public TimeSpan AnimationTick { get; set; }

        /// <summary>
        ///     Ammount to move on each progress step
        /// </summary>
        [Category("Measurement")]
        public long StepSize { get; set; }

        /// <summary>
        ///     Start point of progress
        /// </summary>
        [Category("Measurement")]
        public long StartPoint { get; set; }

        /// <summary>
        ///     Point of progress completion
        /// </summary>
        [Category("Measurement")]
        public long EndPoint { get; set; }

        /// <summary>
        ///     Current Position of the Progress Indicator
        /// </summary>
        [Category("Measurement")]
        public long ProgressPosition { get; set; }

        /// <summary>
        ///     Brush style of the progress indicator
        /// </summary>
        [Category("Style")]
        public Brush ProgressDrawStyle { get; set; }

        /// <summary>
        ///     Brush style of the Text when it is drawn
        /// </summary>
        [Category("Style")]
        public Brush TextDrawStyle { get; set; }

        /// <summary>
        ///     Font style of the Text when it is drawn
        /// </summary>
        [Category("Style")]
        public Font TextFont { get; set; }

        /// <summary>
        ///     Optionally Display Text value of the Indicator
        /// </summary>
        [Category("Style")]
        public bool ShowText { get; set; }

        /// <summary>
        ///     Value indicating the current status of the animation thread
        /// </summary>
        [Category("Animation")]
        public bool IsAnimated => _animationThread.IsAlive;

        #endregion

        #region Animation

        /// <summary>
        ///     Spawn the progress animation thread
        /// </summary>
        public void StartAnimation()
        {
            StopAnimation();

            ProgressPosition = 0;

            InitializeAnimationThread();

            _animationThread.Start();
        }

        /// <summary>
        ///     Stop the progress animation thread
        /// </summary>
        public void StopAnimation()
        {
            if (_animationThread.IsAlive) _animationThread.Interrupt();
        }

        /// <summary>
        ///     ThreadStart Delegate Handler for infinate progress animation
        /// </summary>
        private void AnimationThreadStartCallback()
        {
            while (true)
            {
                Step();
                Thread.Sleep(AnimationTick);
            }
        }

        public string Key { get; set; }

        public enum SizingModeEnum
        {
            Default,
            Spring
        }

        public SizingModeEnum SizingMode { get; set; }

        public new bool Visible { get; set; }

        private void InitializeAnimationThread()
        {
            _animationThread = new Thread(AnimationThreadStartCallback);
            _animationThread.IsBackground = true;
            _animationThread.Name = "Progress Bar Animation Thread";
        }

        #endregion
    }

    #region ProgressDisplayStyle

    /// <summary>
    ///     Statusbar Progress Display Styles
    /// </summary>
    public enum ProgressDisplayStyle
    {
        /// <summary>
        ///     A continually moving animation
        /// </summary>
        Infinite,

        /// <summary>
        ///     A progress bar that fills from left to right
        /// </summary>
        LeftToRight,

        /// <summary>
        ///     A progress bar that fills from right to left
        /// </summary>
        RightToLeft,

        /// <summary>
        ///     A progress bar that fills from bottom to top
        /// </summary>
        BottomToTop,

        /// <summary>
        ///     A progress bar that fills from top to bottom
        /// </summary>
        TopToBottom
    }

    #endregion
}