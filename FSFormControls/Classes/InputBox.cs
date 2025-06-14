using FSFormControls.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FSFormControls
{
    public static class InputBox
    {
        private static System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
        public static string ResultValue;
        private static DialogResult DialogRes;
        private static string[] buttonTextArray = new string[4];

        public enum Icon
        {
            Error,
            Exclamation,
            Information,
            Question,
            Nothing
        }

        public enum Type
        {
            ComboBox,
            TextBox,
            Nothing
        }

        public enum Buttons
        {
            Ok,
            OkCancel,
            YesNo,
            YesNoCancel
        }

        public enum Language
        {
            Czech,
            English,
            German,
            Slovakian,
            Spanish
        }

        /// <summary>
        /// This form is like a MessageBox, but you can select type of controls on it. This form returning a DialogResult value.
        /// 
        /// Set buttons language Czech/English/German/Slovakian/Spanish (default English)
        /// InputBox.SetLanguage(InputBox.Language.English);
        /// Save the DialogResult as res
        ///    DialogResult res = InputBox.ShowDialog("Select some item from ComboBox below:", "Combo InputBox",   //Text message (mandatory), Title (optional)
        ///        InputBox.Icon.Question,                                                                         //Set icon type Error/Exclamation/Question/Warning (default info)
        ///        InputBox.Buttons.OkCancel,                                                                      //Set buttons set OK/OKcancel/YesNo/YesNoCancel (default ok)
        ///        InputBox.Type.ComboBox,                                                                         //Set type ComboBox/TextBox/Nothing (default nothing)
        ///        new string[] { "Item1", "Item2", "Item3" },                                                        //Set string field as ComboBox items (default null)
        ///        true,                                                                                           //Set visible in taskbar (default false)
        ///        new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold));                        //Set font (default by system)
        ///    //Check InputBox result
        ///    if (res == System.Windows.Forms.DialogResult.OK || res == System.Windows.Forms.DialogResult.Yes)
        ///        listView1.Items.Add(InputBox.ResultValue);  
        ///
        /// </summary>
        /// <param name="Message">Message in dialog(as System.String)</param>
        /// <param name="Title">Title of dialog (as System.String)</param>
        /// <param name="icon">Select icon (as InputBox.Icon)</param>
        /// <param name="buttons">Select icon (as InputBox.Buttons)</param>
        /// <param name="type">Type of control in Input box (as InputBox.Type)</param>
        /// <param name="ListItems">Array of ComboBox items (as System.String[])</param>
        /// <param name="FormFont">Font in form (as System.Drawing.Font)</param>
        /// <returns></returns>
        /// 
        public static DialogResult ShowDialog(string Message, string Title = "", string DefaulText = "", Icon icon = Icon.Information, Buttons buttons = Buttons.Ok, Type type = Type.Nothing, string[] ListItems = null, bool ShowInTaskBar = false, Font FormFont = null, bool password = false)
        {
            if (String.IsNullOrEmpty(buttonTextArray[0]))
                SetLanguage(Language.Spanish);

            frm.Controls.Clear();
            ResultValue = "";

            //Form definition
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            frm.Size = new System.Drawing.Size(350, 170);
            frm.Text = Title;
            frm.ShowIcon = false;
            frm.ShowInTaskbar = ShowInTaskBar;
            frm.StartPosition = FormStartPosition.CenterParent;

            //Panel definiton
            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(0, 0);
            panel.Size = new System.Drawing.Size(340, 97);
            panel.BackColor = System.Drawing.Color.White;
            frm.Controls.Add(panel);

            //Add icon in to panel
            panel.Controls.Add(CreatePicture(icon));

            //Label definition (message)
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            label.Text = Message;
            label.Size = new System.Drawing.Size(245, 60);
            label.Location = new System.Drawing.Point(90, 10);
            label.TextAlign = ContentAlignment.MiddleLeft;
            panel.Controls.Add(label);

            //Add buttons to the form
            foreach (Button btn in CreateButtons(buttons))
                frm.Controls.Add(btn);

            //Add ComboBox or TextBox to the form
            Control ctrl = CreateControl(type, DefaulText, ListItems, password);
            panel.Controls.Add(ctrl);

            //Get automaticly cursor to the TextBox
            if (ctrl.Name == "textBox")
                frm.ActiveControl = ctrl;

            //Set label font
            if (FormFont != null)
                label.Font = FormFont;

            frm.ShowDialog();

            //Return text value
            switch (type)
            {
                case Type.Nothing:
                    break;
                default:
                    if (DialogRes == DialogResult.OK || DialogRes == DialogResult.Yes)
                    { ResultValue = ctrl.Text; }
                    else ResultValue = "";
                    break;
            }

            return DialogRes;
        }

        private static void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch (button.Name)
            {
                case "Yes":
                    DialogRes = DialogResult.Yes;
                    break;
                case "No":
                    DialogRes = DialogResult.No;
                    break;
                case "Cancel":
                    DialogRes = DialogResult.Cancel;
                    break;
                default:
                    DialogRes = DialogResult.OK;
                    break;
            }

            frm.Close();
        }

        private static void textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogRes = DialogResult.OK;
                frm.Close();
            }
        }

        private static PictureBox CreatePicture(Icon icon)
        {
            System.Windows.Forms.PictureBox picture = new System.Windows.Forms.PictureBox();

            //Set icon
            switch (icon)
            {
                case Icon.Error:
                    picture.Image = Resources.FSInputBoxError;
                    break;
                case Icon.Exclamation:
                    picture.Image = Resources.FSInputBoxExclamation;
                    break;
                case Icon.Information:
                    picture.Image = Resources.FSInputBoxInformation;
                    break;
                case Icon.Question:
                    picture.Image = Resources.FSInputBoxQuestion;
                    break;
                case Icon.Nothing:
                    picture.Image = Resources.FSInputBoxNic80x80;
                    break;
            }
            picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picture.Size = new System.Drawing.Size(60, 60);
            picture.Location = new System.Drawing.Point(10, 10);
            return picture;
        }

        private static Button[] CreateButtons(Buttons button, Language lang = Language.English)
        {
            //Buttons field for return
            System.Windows.Forms.Button[] returnButtons = new Button[3];

            //Buttons instances
            System.Windows.Forms.Button OkButton = new System.Windows.Forms.Button();
            System.Windows.Forms.Button CancelButton = new System.Windows.Forms.Button();
            System.Windows.Forms.Button YesButton = new System.Windows.Forms.Button();
            System.Windows.Forms.Button NoButton = new System.Windows.Forms.Button();

            //Set buttons names and text
            OkButton.Text = buttonTextArray[0];
            OkButton.Name = "OK";

            YesButton.Text = buttonTextArray[1];
            YesButton.Name = "Yes";

            NoButton.Text = buttonTextArray[2];
            NoButton.Name = "No";

            CancelButton.Text = buttonTextArray[3];
            CancelButton.Name = "Cancel";

            //Set buttons position
            switch (button)
            {
                case Buttons.Ok:
                    OkButton.Location = new System.Drawing.Point(250, 101);
                    returnButtons[0] = OkButton;
                    break;

                case Buttons.OkCancel:
                    OkButton.Location = new System.Drawing.Point(170, 101);
                    returnButtons[0] = OkButton;

                    CancelButton.Location = new System.Drawing.Point(250, 101);
                    returnButtons[1] = CancelButton;
                    break;

                case Buttons.YesNo:
                    YesButton.Location = new System.Drawing.Point(170, 101);
                    returnButtons[0] = YesButton;

                    NoButton.Location = new System.Drawing.Point(250, 101);
                    returnButtons[1] = NoButton;
                    break;

                case Buttons.YesNoCancel:
                    YesButton.Location = new System.Drawing.Point(90, 101);
                    returnButtons[0] = YesButton;

                    NoButton.Location = new System.Drawing.Point(170, 101);
                    returnButtons[1] = NoButton;

                    CancelButton.Location = new System.Drawing.Point(250, 101);
                    returnButtons[2] = CancelButton;
                    break;
            }

            //Set size and event for all used buttons
            foreach (Button btn in returnButtons)
            {
                if (btn != null)
                {
                    btn.Size = new System.Drawing.Size(75, 23);
                    btn.Click += new System.EventHandler(button_Click);
                }
            }

            return returnButtons;
        }

        private static Control CreateControl(Type type, string defaultText, string[] ListItems, bool password)
        {
            //ComboBox
            System.Windows.Forms.ComboBox comboBox = new System.Windows.Forms.ComboBox();
            comboBox.Size = new System.Drawing.Size(180, 22);
            comboBox.Location = new System.Drawing.Point(90, 70);
            comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox.Name = "comboBox";
            if (ListItems != null)
            {
                foreach (string item in ListItems)
                    comboBox.Items.Add(item);
                comboBox.SelectedIndex = 0;
            }

            //Textbox
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
            textBox.Size = new System.Drawing.Size(180, 23);
            textBox.Location = new System.Drawing.Point(90, 70);
            textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
            textBox.Name = "textBox";
            textBox.Text = defaultText;

            if (password)
                textBox.PasswordChar = '*';

            //Set returned Control
            Control returnControl = new Control();

            switch (type)
            {
                case Type.ComboBox:
                    returnControl = comboBox;
                    break;
                case Type.TextBox:
                    returnControl = textBox;
                    break;
            }

            return returnControl;
        }

        public static void SetLanguage(Language lang)
        {
            switch (lang)
            {
                case Language.Czech:
                    buttonTextArray = "OK,Ano,Ne,Storno".Split(',');
                    break;
                case Language.German:
                    buttonTextArray = "OK,Ja,Nein,Stornieren".Split(',');
                    break;
                case Language.Spanish:
                    buttonTextArray = "OK,Sí,No,Cancelar".Split(',');
                    break;
                case Language.Slovakian:
                    buttonTextArray = "OK,Áno,Nie,Zrušiť".Split(',');
                    break;
                default:
                    buttonTextArray = "OK,Yes,No,Cancel".Split(',');
                    break;
            }
        }

        private static void SetControlPosition(Form frm, Label text, TextBox textBox, int posX, int posY)
        {
            // Retrieve the working rectangle from the Screen class
            // using the PrimaryScreen and the WorkingArea properties.
            var workingRectangle = Screen.PrimaryScreen.WorkingArea;

            if (posX >= 0 && posX < workingRectangle.Width - 100 && posY >= 0 &&
                posY < workingRectangle.Height - 100)
            {
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(posX, posY);
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
            }


            var PrompText = text.Text;

            var n = 0;
            var Index = 0;
            while (PrompText.IndexOf("\n", Index) > -1)
            {
                Index = PrompText.IndexOf("\n", Index) + 1;
                n++;
            }

            if (n == 0)
                n = 1;

            var Txt = textBox.Location;
            Txt.Y = Txt.Y + n * 4;
            textBox.Location = Txt;
            var form = frm.Size;
            form.Height = form.Height + n * 4;
            frm.Size = form;

            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
            textBox.Focus();
        }
    }
}
