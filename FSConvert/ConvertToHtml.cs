#if NETFRAMEWORK

using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BorderStyle = System.Web.UI.WebControls.BorderStyle;
using Button = System.Web.UI.WebControls.Button;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using Control = System.Windows.Forms.Control;
using DataGrid = System.Web.UI.WebControls.DataGrid;
using Label = System.Web.UI.WebControls.Label;
using Panel = System.Web.UI.WebControls.Panel;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace FSConvert
{
    public class ConvertToHtml
    {
        public string GenerateHTML(Form frm)
        {
            var frame = new Label();

            frame.Controls.Add(GenerateHTMLControls(frm.Controls));

            var sb = new StringBuilder();
            var tw = new StringWriter(sb);
            var htmltw = new HtmlTextWriter(tw);
            frame.RenderControl(htmltw);

            return sb.ToString();
        }

        private WebControl GenerateHTMLControls(Control.ControlCollection cc)
        {
            var frame = new Label();
            object webctrl;

            var x = 0;
            var y = 0;
            var sx = 0;
            var sy = 0;

            foreach (Control winctrl in cc)
            {
                x = winctrl.Left;
                y = winctrl.Top;
                sx = winctrl.Width;
                sy = winctrl.Height;

                webctrl = null;

                switch (winctrl.GetType().ToString())
                {
                    case "FSFormControls.DBLabel":
                    case "System.Windows.Forms.Label":
                        webctrl = new Label();
                        ((Label) webctrl).Style["POSITION"] = "absolute";
                        ((Label) webctrl).Style["LEFT"] = x + "px";
                        ((Label) webctrl).Style["TOP"] = y + "px";
                        ((Label) webctrl).Style["WIDTH"] = sx + "px";
                        ((Label) webctrl).Style["HEIGHT"] = sy + "px";
                        ((Label) webctrl).Font.Name = winctrl.Font.Name;
                        ((Label) webctrl).Font.Size = FontUnit.Point(Convert.ToInt32(winctrl.Font.Size));
                        ((Label) webctrl).Text = HttpUtility.HtmlEncode(winctrl.Text);
                        break;
                    case "FSFormControls.DBCheckBox":
                    case "System.Windows.Forms.CheckBox":
                        webctrl = new CheckBox();
                        ((CheckBox) webctrl).Style["POSITION"] = "absolute";
                        ((CheckBox) webctrl).Style["LEFT"] = x + "px";
                        ((CheckBox) webctrl).Style["TOP"] = y + "px";
                        ((CheckBox) webctrl).Style["WIDTH"] = sx + "px";
                        ((CheckBox) webctrl).Style["HEIGHT"] = sy + "px";
                        ((CheckBox) webctrl).Font.Name = winctrl.Font.Name;
                        ((CheckBox) webctrl).Font.Size = FontUnit.Point(Convert.ToInt32(winctrl.Font.Size));
                        ((CheckBox) webctrl).Text = HttpUtility.HtmlEncode(winctrl.Text);
                        break;
                    case "FSFormControls.DBTextBox":
                    case "System.Windows.Forms.TextBox":
                        webctrl = new TextBox();
                        ((TextBox) webctrl).Style["POSITION"] = "absolute";
                        ((TextBox) webctrl).Style["LEFT"] = x + "px";
                        ((TextBox) webctrl).Style["TOP"] = y + "px";
                        ((TextBox) webctrl).Style["WIDTH"] = sx + "px";
                        ((TextBox) webctrl).Style["HEIGHT"] = sy + "px";
                        ((TextBox) webctrl).Font.Name = winctrl.Font.Name;
                        ((TextBox) webctrl).Font.Size = FontUnit.Point(Convert.ToInt32(winctrl.Font.Size));
                        ((TextBox) webctrl).Text = HttpUtility.HtmlEncode(winctrl.Text);
                        break;
                    case "FSFormControls.DBCombo":
                    case "System.Windows.Forms.ComboBox":
                        webctrl = new DropDownList();
                        ((DropDownList) webctrl).Style["POSITION"] = "absolute";
                        ((DropDownList) webctrl).Style["LEFT"] = x + "px";
                        ((DropDownList) webctrl).Style["TOP"] = y + "px";
                        ((DropDownList) webctrl).Style["WIDTH"] = sx + "px";
                        ((DropDownList) webctrl).Style["HEIGHT"] = sy + "px";
                        ((DropDownList) webctrl).Font.Name = winctrl.Font.Name;
                        ((DropDownList) webctrl).Font.Size = FontUnit.Point(Convert.ToInt32(winctrl.Font.Size));
                        if (winctrl.GetType().ToString() == "FSFormControls.DBCombo")
                        {
                            //((DropDownList) webctrl).DataSource = ((DBCombo) winctrl).DataControlList.DataTable;
                            //((DropDownList) webctrl).DataTextField = ((DBCombo) winctrl).DBFieldList;
                            //((DropDownList) webctrl).DataValueField = ((DBCombo) winctrl).DBFieldData;
                        }
                        else
                        {
                            ((DropDownList) webctrl).DataSource = ((ComboBox) winctrl).DataSource;
                            ((DropDownList) webctrl).DataTextField = ((ComboBox) winctrl).DisplayMember;
                            ((DropDownList) webctrl).DataValueField = ((ComboBox) winctrl).ValueMember;
                        }

                        ((DropDownList) webctrl).DataBind();
                        break;
                    case "FSFormControls.DBButton":
                    case "System.Windows.Forms.Button":
                        webctrl = new Button();
                        ((Button) webctrl).Style["POSITION"] = "absolute";
                        ((Button) webctrl).Style["LEFT"] = x + "px";
                        ((Button) webctrl).Style["TOP"] = y + "px";
                        ((Button) webctrl).Style["WIDTH"] = sx + "px";
                        ((Button) webctrl).Style["HEIGHT"] = sy + "px";
                        ((Button) webctrl).Font.Name = winctrl.Font.Name;
                        ((Button) webctrl).Font.Size = FontUnit.Point(Convert.ToInt32(winctrl.Font.Size));
                        ((Button) webctrl).Text = HttpUtility.HtmlEncode(winctrl.Text);
                        break;
                    case "FSFormControls.DBGrid":
                    case "FSFormControls.DBGridView":
                    case "System.Windows.Forms.DataGrid":
                        var rows = 0;
                        webctrl = new DataGrid();
                        ((DataGrid) webctrl).GridLines = GridLines.Both;
                        ((DataGrid) webctrl).BorderColor = Color.Teal;
                        ((DataGrid) webctrl).ShowHeader = true;
                        ((DataGrid) webctrl).HeaderStyle.BackColor = Color.LightGray;
                        ((DataGrid) webctrl).HeaderStyle.Height = Unit.Pixel(15);
                        ((DataGrid) webctrl).AllowPaging = true;
                        ((DataGrid) webctrl).AutoGenerateColumns = true;
                        ((DataGrid) webctrl).PagerStyle.Mode = PagerMode.NextPrev;
                        rows = Convert.ToInt32(sy / 17 - 2);
                        ((DataGrid) webctrl).PageSize = rows;

                        ((DataGrid) webctrl).Style["POSITION"] = "absolute";
                        ((DataGrid) webctrl).Style["LEFT"] = x + "px";
                        ((DataGrid) webctrl).Style["TOP"] = y + "px";
                        //((DataGrid) webctrl).Style["WIDTH"] = sx + "px";
                        //((DataGrid) webctrl).Style["HEIGHT"] = sy + "px";
                        ((DataGrid) webctrl).Font.Name = winctrl.Font.Name;
                        ((DataGrid) webctrl).Font.Size = FontUnit.Point(Convert.ToInt32(winctrl.Font.Size));

                        //if (winctrl is DBGridView)
                        //    ((DataGrid)webctrl).DataSource = ((DBGridView)winctrl).DataControl.DataTable;
                        //else if (winctrl is DBGrid)
                        //    ((DataGrid) webctrl).DataSource = ((DBGrid) winctrl).DataControl.DataTable;
                        //else
                            ((DataGrid) webctrl).DataSource = ((System.Windows.Forms.DataGrid) winctrl).DataSource;
                        ((DataGrid) webctrl).DataBind();
                        break;
                    case "FSFormControls.DBFrame":
                    case "FSFormControls.DBGroupBox":
                    case "System.Windows.Forms.Panel":
                    case "System.Windows.Forms.GroupBox":
                    case "System.Windows.Forms.TabControl":
                    case "System.Windows.Forms.DBTabControl":
                    case "FSFormControls.DBRecord":
                    case "System.Windows.Forms.TabPage":
                    case "System.Windows.Forms.DBTabPage":
                        webctrl = new Panel();
                        ((Panel) webctrl).BorderStyle = BorderStyle.Solid;
                        ((Panel) webctrl).BorderWidth = Unit.Point(1);
                        ((Panel) webctrl).Style["POSITION"] = "absolute";
                        ((Panel) webctrl).Style["LEFT"] = x + "px";
                        ((Panel) webctrl).Style["TOP"] = y + "px";
                        ((Panel) webctrl).Style["WIDTH"] = sx + "px";
                        ((Panel) webctrl).Style["HEIGHT"] = sy + "px";

                        var label = new Label();
                        label.Text = "&nbsp;" + HttpUtility.HtmlEncode(winctrl.Text) + "&nbsp;";
                        label.Style["POSITION"] = "absolute";
                        label.Style["LEFT"] = 15 + "px";
                        label.Style["TOP"] = -8 + "px";
                        label.BackColor = Color.White;
                        label.Font.Name = winctrl.Font.Name;
                        label.Font.Size = FontUnit.Point(Convert.ToInt32(winctrl.Font.Size));
                        ((Panel) webctrl).Controls.Add(label);
                        if (winctrl is TabControl)
                        {
                            var tp = ((TabControl) winctrl).SelectedTab;
                            ((Panel) webctrl).Controls.Add(GenerateHTMLControls(tp.Controls));
                        }
                        else
                        {
                            ((Panel)webctrl).Controls.Add(GenerateHTMLControls(winctrl.Controls));

                            //if (winctrl is DBTabControl)
                            //{
                            //    var tp = ((DBTabControl) winctrl).SelectedTab;
                            //    ((Panel) webctrl).Controls.Add(GenerateHTMLControls(tp.Controls));
                            //}
                            //else
                            //{
                            //    ((Panel) webctrl).Controls.Add(GenerateHTMLControls(winctrl.Controls));
                            //}
                        }

                        break;
                    case "FSFormControls.DBListView":
                    case "System.Windows.Forms.ListView":
                        webctrl = new DataGrid();
                        ((DataGrid) webctrl).Style["POSITION"] = "absolute";
                        ((DataGrid) webctrl).Style["LEFT"] = x + "px";
                        ((DataGrid) webctrl).Style["TOP"] = y + "px";
                        ((DataGrid) webctrl).Style["WIDTH"] = sx + "px";
                        ((DataGrid) webctrl).Style["HEIGHT"] = sy + "px";
                        ((DataGrid) webctrl).Font.Name = winctrl.Font.Name;
                        ((DataGrid) webctrl).Font.Size = FontUnit.Point(Convert.ToInt32(winctrl.Font.Size));
                        break;
                }


                if (webctrl != null) frame.Controls.Add((System.Web.UI.Control) webctrl);
            }

            return frame;
        }
    }
}

#endif