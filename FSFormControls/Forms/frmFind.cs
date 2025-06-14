#region

using System;
using System.ComponentModel;

#endregion

namespace FSFormControls
{
    public partial class frmFind
    {
        public DBRichTextBox m_DBRichTextBox;

        public frmFind()
        {
            InitializeComponent();

            btnReplaceAll.Click += btnReplaceAll_Click;
            btnReplace.Click += btnReplace_Click;
            cmdFind.Click += cmdFind_Click;
        }

        public DBRichTextBox DBRichTextBox
        {
            get { return m_DBRichTextBox; }
            set { m_DBRichTextBox = value; }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            if (MatchCase.Checked)
            {
                if (WholeWord.Checked)
                    m_DBRichTextBox.FindAndReplace(FindTextBox.Text, ReplaceTextBox.Text, true, true, true);
                else
                    m_DBRichTextBox.FindAndReplace(FindTextBox.Text, ReplaceTextBox.Text, true, true, false);
            }
            else
            {
                if (WholeWord.Checked)
                    m_DBRichTextBox.FindAndReplace(FindTextBox.Text, ReplaceTextBox.Text, true, false, true);
                else
                    m_DBRichTextBox.FindAndReplace(FindTextBox.Text, ReplaceTextBox.Text, true, false, false);
            }
        }


        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (MatchCase.Checked)
            {
                if (WholeWord.Checked)
                    m_DBRichTextBox.FindAndReplace(FindTextBox.Text, ReplaceTextBox.Text, false, true, true);
                else
                    m_DBRichTextBox.FindAndReplace(FindTextBox.Text, ReplaceTextBox.Text, false, true, false);
            }
            else
            {
                if (WholeWord.Checked)
                    m_DBRichTextBox.FindAndReplace(FindTextBox.Text, ReplaceTextBox.Text, false, false, true);
                else
                    m_DBRichTextBox.FindAndReplace(FindTextBox.Text, ReplaceTextBox.Text, false, false, false);
            }
        }


        private void cmdFind_Click(object sender, EventArgs e)
        {
            if (MatchCase.Checked)
            {
                if (WholeWord.Checked)
                    m_DBRichTextBox.Find(FindTextBox.Text, true, true);
                else
                    m_DBRichTextBox.Find(FindTextBox.Text, true, false);
            }
            else
            {
                if (WholeWord.Checked)
                    m_DBRichTextBox.Find(FindTextBox.Text, false, true);
                else
                    m_DBRichTextBox.Find(FindTextBox.Text, false, false);
            }
        }
    }
}