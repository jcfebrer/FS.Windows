#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;

#endregion

namespace FSFormControls
{
    public class frmTaskBarNotifier : Form
    {
        private readonly TaskBarNotifier taskbarNotifier1;
        private readonly TaskBarNotifier taskbarNotifier2;
        private readonly TaskBarNotifier taskbarNotifier3;


        private void Notifier_CloseButtonClick(object sender, EventArgs e)
        {
            var taskbarSender = (TaskBarNotifier) sender;

            if (taskbarSender.Equals(taskbarNotifier1)) MessageBox.Show("TaskBarNotifier 1: CloseButton was clicked");

            if (taskbarSender.Equals(taskbarNotifier2)) MessageBox.Show("TaskBarNotifier 2: CloseButton was clicked");

            if (taskbarSender.Equals(taskbarNotifier3)) MessageBox.Show("TaskBarNotifier 3: CloseButton was clicked");
        }


        private void Notifier_TitleClick(object sender, EventArgs e)
        {
            var taskbarSender = (TaskBarNotifier) sender;

            if (taskbarSender.Equals(taskbarNotifier1)) MessageBox.Show("TaskBarNotifier 1: Title was clicked");

            if (taskbarSender.Equals(taskbarNotifier2)) MessageBox.Show("TaskBarNotifier 2: Title was clicked");

            if (taskbarSender.Equals(taskbarNotifier3)) MessageBox.Show("TaskBarNotifier 3: Title was clicked");
        }


        private void Notifier_TextClick(object sender, EventArgs e)
        {
            var taskbarSender = (TaskBarNotifier) sender;

            if (taskbarSender.Equals(taskbarNotifier1)) MessageBox.Show("TaskBarNotifier 1: TextZone was clicked");

            if (taskbarSender.Equals(taskbarNotifier2)) MessageBox.Show("TaskBarNotifier 2: TextZone was clicked");

            if (taskbarSender.Equals(taskbarNotifier3)) MessageBox.Show("TaskBarNotifier 3: TextZone was clicked");
        }


        private void ButtonShowPopup1_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text.Length == 0 || textBoxContent.Text.Length == 0)
            {
                MessageBox.Show("Enter a title and a content Text");
                return;
            }

            if (!NumberUtils.IsNumeric(textBoxDelayShowing.Text) ||
                !NumberUtils.IsNumeric(textBoxDelayStaying.Text) ||
                !NumberUtils.IsNumeric(textBoxDelayHiding.Text))
            {
                MessageBox.Show("Enter valid Delays (integers)");
                return;
            }

            var transTemp0 = taskbarNotifier1;
            transTemp0.CloseButtonClickEnabled = checkBoxCloseClickable.Checked;
            transTemp0.TitleClickEnabled = checkBoxTitleClickable.Checked;
            transTemp0.TextClickEnabled = checkBoxContentClickable.Checked;
            transTemp0.DrawTextFocusRect = checkBoxSelectionRectangle.Checked;
            transTemp0.KeepVisibleOnMouseOver = checkBoxKeepVisibleOnMouseOver.Checked;
            transTemp0.ReShowOnMouseOver = checkBoxReShowOnMouseOver.Checked;
            transTemp0.Show(textBoxTitle.Text, textBoxContent.Text, int.Parse(textBoxDelayShowing.Text),
                int.Parse(textBoxDelayStaying.Text), int.Parse(textBoxDelayHiding.Text));
        }


        private void ButtonShowPopup2_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text.Length == 0 || textBoxContent.Text.Length == 0)
            {
                MessageBox.Show("Enter a title and a content Text");
                return;
            }

            if (!NumberUtils.IsNumeric(textBoxDelayShowing.Text) ||
                !NumberUtils.IsNumeric(textBoxDelayStaying.Text) ||
                !NumberUtils.IsNumeric(textBoxDelayHiding.Text))
            {
                MessageBox.Show("Enter valid Delays (integers)");
                return;
            }

            var transTemp1 = taskbarNotifier2;
            transTemp1.CloseButtonClickEnabled = checkBoxCloseClickable.Checked;
            transTemp1.TitleClickEnabled = checkBoxTitleClickable.Checked;
            transTemp1.TextClickEnabled = checkBoxContentClickable.Checked;
            transTemp1.DrawTextFocusRect = checkBoxSelectionRectangle.Checked;
            transTemp1.KeepVisibleOnMouseOver = checkBoxKeepVisibleOnMouseOver.Checked;
            transTemp1.ReShowOnMouseOver = checkBoxReShowOnMouseOver.Checked;
            transTemp1.Show(textBoxTitle.Text, textBoxContent.Text, int.Parse(textBoxDelayShowing.Text),
                int.Parse(textBoxDelayStaying.Text), int.Parse(textBoxDelayHiding.Text));
        }


        private void ButtonShowPopup3_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text.Length == 0 || textBoxContent.Text.Length == 0)
            {
                MessageBox.Show("Enter a title and a content Text");
                return;
            }

            if (!NumberUtils.IsNumeric(textBoxDelayShowing.Text) ||
                !NumberUtils.IsNumeric(textBoxDelayStaying.Text) ||
                !NumberUtils.IsNumeric(textBoxDelayHiding.Text))
            {
                MessageBox.Show("Enter valid Delays (integers)");
                return;
            }

            var transTemp2 = taskbarNotifier3;
            transTemp2.NormalTitleColor = Color.Black;
            transTemp2.HoverTitleColor = Color.Black;
            transTemp2.NormalContentColor = Color.Yellow;
            transTemp2.HoverContentColor = Color.White;
            transTemp2.CloseButtonClickEnabled = checkBoxCloseClickable.Checked;
            transTemp2.TitleClickEnabled = checkBoxTitleClickable.Checked;
            transTemp2.TextClickEnabled = checkBoxContentClickable.Checked;
            transTemp2.DrawTextFocusRect = checkBoxSelectionRectangle.Checked;
            transTemp2.KeepVisibleOnMouseOver = checkBoxKeepVisibleOnMouseOver.Checked;
            transTemp2.ReShowOnMouseOver = checkBoxReShowOnMouseOver.Checked;
            transTemp2.Show(textBoxTitle.Text, textBoxContent.Text, int.Parse(textBoxDelayShowing.Text),
                int.Parse(textBoxDelayStaying.Text), int.Parse(textBoxDelayHiding.Text));
        }

        #region '" Windows Form Designer generated code "' 

        private readonly IContainer components = null;
        internal Button ButtonShowPopup1;
        internal Button ButtonShowPopup2;
        internal Button ButtonShowPopup3;
        internal CheckBox checkBoxCloseClickable;
        internal CheckBox checkBoxContentClickable;
        internal CheckBox checkBoxKeepVisibleOnMouseOver;
        internal CheckBox checkBoxReShowOnMouseOver;
        internal CheckBox checkBoxSelectionRectangle;
        internal CheckBox checkBoxTitleClickable;

        internal GroupBox groupBox1;
        internal GroupBox groupBox2;
        internal GroupBox groupBox3;
        internal Label label1;
        internal Label label2;
        internal Label label3;
        internal Label label4;
        internal Label label5;
        internal TextBox textBoxContent;
        internal TextBox textBoxDelayHiding;
        internal TextBox textBoxDelayShowing;
        internal TextBox textBoxDelayStaying;
        internal TextBox textBoxTitle;

        public frmTaskBarNotifier()
        {
            InitializeComponent();

            textBoxContent.Text = "This is a sample content, it can spread on multiple lines";
            textBoxTitle.Text = "Title";
            textBoxDelayShowing.Text = "500";
            textBoxDelayStaying.Text = "3000";
            textBoxDelayHiding.Text = "500";
            checkBoxSelectionRectangle.Checked = true;
            checkBoxTitleClickable.Checked = false;
            checkBoxContentClickable.Checked = true;
            checkBoxCloseClickable.Checked = true;
            checkBoxKeepVisibleOnMouseOver.Checked = true;
            checkBoxReShowOnMouseOver.Checked = false;

            taskbarNotifier1 = new TaskBarNotifier();
            taskbarNotifier1.SetBackgroundBitmap(new Bitmap(GetType(), "skin.bmp"), Color.FromArgb(255, 0, 255));
            taskbarNotifier1.SetCloseBitmap(new Bitmap(GetType(), "close.bmp"), Color.FromArgb(255, 0, 255),
                new Point(127, 8));
            taskbarNotifier1.TitleRectangle = new Rectangle(40, 9, 70, 25);
            taskbarNotifier1.TextRectangle = new Rectangle(8, 41, 133, 68);

            taskbarNotifier2 = new TaskBarNotifier();
            taskbarNotifier2.SetBackgroundBitmap(new Bitmap(GetType(), "skin2.bmp"), Color.FromArgb(255, 0, 255));
            taskbarNotifier2.SetCloseBitmap(new Bitmap(GetType(), "close2.bmp"), Color.FromArgb(255, 0, 255),
                new Point(300, 74));
            taskbarNotifier2.TitleRectangle = new Rectangle(123, 80, 176, 16);
            taskbarNotifier2.TextRectangle = new Rectangle(116, 97, 197, 22);

            taskbarNotifier3 = new TaskBarNotifier();
            taskbarNotifier3.SetBackgroundBitmap(new Bitmap(GetType(), "skin3.bmp"), Color.FromArgb(255, 0, 255));
            taskbarNotifier3.SetCloseBitmap(new Bitmap(GetType(), "close.bmp"), Color.FromArgb(255, 0, 255),
                new Point(280, 57));
            taskbarNotifier3.TitleRectangle = new Rectangle(150, 57, 125, 28);
            taskbarNotifier3.TextRectangle = new Rectangle(75, 92, 215, 55);

            SubscriveEvents();
        }

        private void SubscriveEvents()
        {
            taskbarNotifier1.CloseButtonClick += Notifier_CloseButtonClick;
            taskbarNotifier2.CloseButtonClick += Notifier_CloseButtonClick;
            taskbarNotifier3.CloseButtonClick += Notifier_CloseButtonClick;

            taskbarNotifier1.TitleClick += Notifier_TitleClick;
            taskbarNotifier2.TitleClick += Notifier_TitleClick;
            taskbarNotifier3.TitleClick += Notifier_TitleClick;

            taskbarNotifier1.TextClick += Notifier_TextClick;
            taskbarNotifier2.TextClick += Notifier_TextClick;
            taskbarNotifier3.TextClick += Notifier_TextClick;

            ButtonShowPopup1.Click += ButtonShowPopup1_Click;

            ButtonShowPopup2.Click += ButtonShowPopup2_Click;

            ButtonShowPopup3.Click += ButtonShowPopup3_Click;
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
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            textBoxContent = new TextBox();
            textBoxTitle = new TextBox();
            groupBox2 = new GroupBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            textBoxDelayShowing = new TextBox();
            textBoxDelayStaying = new TextBox();
            textBoxDelayHiding = new TextBox();
            groupBox3 = new GroupBox();
            checkBoxReShowOnMouseOver = new CheckBox();
            checkBoxKeepVisibleOnMouseOver = new CheckBox();
            checkBoxCloseClickable = new CheckBox();
            checkBoxContentClickable = new CheckBox();
            checkBoxTitleClickable = new CheckBox();
            checkBoxSelectionRectangle = new CheckBox();
            ButtonShowPopup2 = new Button();
            ButtonShowPopup1 = new Button();
            ButtonShowPopup3 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBoxContent);
            groupBox1.Controls.Add(textBoxTitle);
            groupBox1.Location = new Point(4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(296, 88);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Text";
            // 
            // label2
            // 
            label2.Location = new Point(12, 52);
            label2.Name = "label2";
            label2.Size = new Size(48, 16);
            label2.TabIndex = 10;
            label2.Text = "Content";
            // 
            // label1
            // 
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(40, 16);
            label1.TabIndex = 9;
            label1.Text = "Title";
            // 
            // textBoxContent
            // 
            textBoxContent.Location = new Point(60, 52);
            textBoxContent.Name = "textBoxContent";
            textBoxContent.Size = new Size(224, 20);
            textBoxContent.TabIndex = 8;
            textBoxContent.Text = "textBoxContent";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(60, 20);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(224, 20);
            textBoxTitle.TabIndex = 7;
            textBoxTitle.Text = "textBoxTitle";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(textBoxDelayShowing);
            groupBox2.Controls.Add(textBoxDelayStaying);
            groupBox2.Controls.Add(textBoxDelayHiding);
            groupBox2.Location = new Point(4, 100);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(296, 88);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Animation Delays (ms)";
            // 
            // label5
            // 
            label5.Location = new Point(200, 28);
            label5.Name = "label5";
            label5.Size = new Size(80, 16);
            label5.TabIndex = 19;
            label5.Text = "Delay Hiding";
            // 
            // label4
            // 
            label4.Location = new Point(112, 28);
            label4.Name = "label4";
            label4.Size = new Size(80, 16);
            label4.TabIndex = 18;
            label4.Text = "Delay Staying";
            // 
            // label3
            // 
            label3.Location = new Point(16, 28);
            label3.Name = "label3";
            label3.Size = new Size(80, 16);
            label3.TabIndex = 17;
            label3.Text = "Delay Showing";
            // 
            // textBoxDelayShowing
            // 
            textBoxDelayShowing.Location = new Point(24, 52);
            textBoxDelayShowing.Name = "textBoxDelayShowing";
            textBoxDelayShowing.Size = new Size(56, 20);
            textBoxDelayShowing.TabIndex = 16;
            textBoxDelayShowing.Text = "textBoxDelayShowing";
            // 
            // textBoxDelayStaying
            // 
            textBoxDelayStaying.Location = new Point(120, 52);
            textBoxDelayStaying.Name = "textBoxDelayStaying";
            textBoxDelayStaying.Size = new Size(56, 20);
            textBoxDelayStaying.TabIndex = 15;
            textBoxDelayStaying.Text = "textBoxDelayStaying";
            // 
            // textBoxDelayHiding
            // 
            textBoxDelayHiding.Location = new Point(208, 52);
            textBoxDelayHiding.Name = "textBoxDelayHiding";
            textBoxDelayHiding.Size = new Size(56, 20);
            textBoxDelayHiding.TabIndex = 14;
            textBoxDelayHiding.Text = "textBoxDelayHiding";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(checkBoxReShowOnMouseOver);
            groupBox3.Controls.Add(checkBoxKeepVisibleOnMouseOver);
            groupBox3.Controls.Add(checkBoxCloseClickable);
            groupBox3.Controls.Add(checkBoxContentClickable);
            groupBox3.Controls.Add(checkBoxTitleClickable);
            groupBox3.Controls.Add(checkBoxSelectionRectangle);
            groupBox3.Location = new Point(4, 192);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(296, 116);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "Options";
            // 
            // checkBoxReShowOnMouseOver
            // 
            checkBoxReShowOnMouseOver.Location = new Point(16, 92);
            checkBoxReShowOnMouseOver.Name = "checkBoxReShowOnMouseOver";
            checkBoxReShowOnMouseOver.Size = new Size(272, 16);
            checkBoxReShowOnMouseOver.TabIndex = 5;
            checkBoxReShowOnMouseOver.Text = "Re-show when Mouse over window when hiding";
            // 
            // checkBoxKeepVisibleOnMouseOver
            // 
            checkBoxKeepVisibleOnMouseOver.Location = new Point(16, 72);
            checkBoxKeepVisibleOnMouseOver.Name = "checkBoxKeepVisibleOnMouseOver";
            checkBoxKeepVisibleOnMouseOver.Size = new Size(272, 16);
            checkBoxKeepVisibleOnMouseOver.TabIndex = 4;
            checkBoxKeepVisibleOnMouseOver.Text = "Keep Visible when Mouse over window";
            // 
            // checkBoxCloseClickable
            // 
            checkBoxCloseClickable.Location = new Point(16, 48);
            checkBoxCloseClickable.Name = "checkBoxCloseClickable";
            checkBoxCloseClickable.Size = new Size(104, 16);
            checkBoxCloseClickable.TabIndex = 3;
            checkBoxCloseClickable.Text = "Close Clickable";
            // 
            // checkBoxContentClickable
            // 
            checkBoxContentClickable.Location = new Point(128, 24);
            checkBoxContentClickable.Name = "checkBoxContentClickable";
            checkBoxContentClickable.Size = new Size(112, 16);
            checkBoxContentClickable.TabIndex = 1;
            checkBoxContentClickable.Text = "Content Clickable";
            // 
            // checkBoxTitleClickable
            // 
            checkBoxTitleClickable.Location = new Point(16, 24);
            checkBoxTitleClickable.Name = "checkBoxTitleClickable";
            checkBoxTitleClickable.Size = new Size(96, 16);
            checkBoxTitleClickable.TabIndex = 0;
            checkBoxTitleClickable.Text = "Title Clickable";
            // 
            // checkBoxSelectionRectangle
            // 
            checkBoxSelectionRectangle.Location = new Point(128, 48);
            checkBoxSelectionRectangle.Name = "checkBoxSelectionRectangle";
            checkBoxSelectionRectangle.Size = new Size(160, 16);
            checkBoxSelectionRectangle.TabIndex = 2;
            checkBoxSelectionRectangle.Text = "Show Selection Rectangle";
            // 
            // ButtonShowPopup2
            // 
            ButtonShowPopup2.Location = new Point(108, 316);
            ButtonShowPopup2.Name = "ButtonShowPopup2";
            ButtonShowPopup2.Size = new Size(88, 23);
            ButtonShowPopup2.TabIndex = 18;
            ButtonShowPopup2.Text = "Show popup 2";
            // 
            // ButtonShowPopup1
            // 
            ButtonShowPopup1.Location = new Point(8, 316);
            ButtonShowPopup1.Name = "ButtonShowPopup1";
            ButtonShowPopup1.Size = new Size(88, 23);
            ButtonShowPopup1.TabIndex = 17;
            ButtonShowPopup1.Text = "Show popup 1";
            // 
            // ButtonShowPopup3
            // 
            ButtonShowPopup3.Location = new Point(208, 316);
            ButtonShowPopup3.Name = "ButtonShowPopup3";
            ButtonShowPopup3.Size = new Size(88, 23);
            ButtonShowPopup3.TabIndex = 19;
            ButtonShowPopup3.Text = "Show popup 3";
            // 
            // frmTaskBarNotifier
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(305, 349);
            Controls.Add(ButtonShowPopup3);
            Controls.Add(ButtonShowPopup2);
            Controls.Add(ButtonShowPopup1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmTaskBarNotifier";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "C# .NET TaskBarNotifier Demo";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}