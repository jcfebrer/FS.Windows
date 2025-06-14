#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSDatabase;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    internal class frmFilter : Form
    {
        public DBControl DataControl { get; set; }

        public string Filter { get; set; } = "";

        private void frmFiltrosBusquedas_Load(object sender, EventArgs e)
        {
            var f = 0;

            if (DataControl == null) throw new ExceptionUtil("Propiedad DataControl, no especificada.");

            FunctionsForms.Center(this);

            if (DataControl.ColumnMapping.Count == 0)
                for (f = 0; f <= Convert.ToInt32(DataControl.FieldsCount() - 1); f++)
                    DataControl.ColumnMapping.Add(DataControl.FieldName(f),
                        TextUtil.PCase(DataControl.FieldName(f)));

            for (f = 0; f <= DataControl.ColumnMapping.Count - 1; f++)
            {
                cboCampo0.Items.Add(DataControl.ColumnMapping[f].HeaderCaption);
                cboCampo1.Items.Add(DataControl.ColumnMapping[f].HeaderCaption);
                cboCampo2.Items.Add(DataControl.ColumnMapping[f].HeaderCaption);
                cboCampo3.Items.Add(DataControl.ColumnMapping[f].HeaderCaption);
                cboCampo4.Items.Add(DataControl.ColumnMapping[f].HeaderCaption);
            }

            cboComparacion0.Items.Clear();
            cboComparacion1.Items.Clear();
            cboComparacion2.Items.Clear();
            cboComparacion3.Items.Clear();
            cboComparacion4.Items.Clear();

            cboUnion1.Items.Clear();
            cboUnion2.Items.Clear();
            cboUnion3.Items.Clear();
            cboUnion4.Items.Clear();

            ComboBox cbo = null;
            for (f = 1; f <= 4; f++)
            {
                cbo = (ComboBox) FunctionsForms.GetControlByName(FindForm().Controls, "cboUnion" + f);
                cbo.Items.Clear();
                cbo.Items.Add("Y");
                cbo.Items.Add("?");
                cbo.Items.Add("NO");
            }

            lblFilter.Text = DataControl.Filter;
        }


        private void CondNumeric(ComboBox cbo)
        {
            cbo.Items.Clear();
            cbo.Items.Add("Igual");
            cbo.Items.Add("Mayor/igual que");
            cbo.Items.Add("Menor/igual que");
            cbo.Items.Add("Mayor que");
            cbo.Items.Add("Menor que");
            cbo.Items.Add("Diferente");
        }


        private void CondDateTime(ComboBox cbo)
        {
            cbo.Items.Clear();
            cbo.Items.Add("Igual");
            cbo.Items.Add("Mayor/igual que");
            cbo.Items.Add("Menor/igual que");
            cbo.Items.Add("Mayor que");
            cbo.Items.Add("Menor que");
            cbo.Items.Add("Diferente");
            cbo.Items.Add("Es Vacio");
            cbo.Items.Add("No es Vacio");
        }


        private void CondText(ComboBox cbo)
        {
            cbo.Items.Clear();
            cbo.Items.Add("Igual");
            cbo.Items.Add("Mayor/igual que");
            cbo.Items.Add("Menor/igual que");
            cbo.Items.Add("Mayor que");
            cbo.Items.Add("Menor que");
            cbo.Items.Add("Diferente");
            cbo.Items.Add("Empieza por");
            cbo.Items.Add("Termina por");
            cbo.Items.Add("Contiene");
            cbo.Items.Add("Es Vacio");
            cbo.Items.Add("No es Vacio");
        }


        private void CondBoolean(ComboBox cbo)
        {
            cbo.Items.Clear();
            cbo.Items.Add("Igual");
            cbo.Items.Add("Diferente");
        }


        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Filter = "";
            Close();
        }


        private void cmdAceptar_Click(object sender, EventArgs e)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            var f = 0;
            var cond = new string[5];
            var field = new string[5];
            var comp = new string[5];
            var union = new string[5];
            var sqlWhere = new string[5];
            var sql = "";
            var fielddb = "";
            var typ = "";

            for (f = 0; f <= 4; f++)
            {
                field[f] = ((ComboBox) FunctionsForms.GetControlByName(FindForm().Controls, "cboCampo" + f)).Text;

                if (!(DataControl.ColumnMapping.FindByHeaderCaption(field[f]) == null))
                    fielddb = DataControl.ColumnMapping.FindByHeaderCaption(field[f]).FieldDB;
                else
                    fielddb = "";

                field[f] = fielddb;
                cond[f] =
                    ReplaceCondSign(
                        ((ComboBox) FunctionsForms.GetControlByName(FindForm().Controls, "cboComparacion" + f)).Text);
                comp[f] =
                    ((TextBox) FunctionsForms.GetControlByName(FindForm().Controls, "txtTextoComparado" + f)).Text;

                if (comp[f] != "")
                {
                    if (cond[f].StartsWith("like"))
                    {
                        comp[f] = TextUtil.Replace(cond[f], "#", comp[f]);
                        cond[f] = " ";
                    }
                    else
                    {
                        switch (db.GetField(field[f], DataControl.TableName).Tipo.ToString().ToLower())
                        {
                            case "system.int16":
                            case "system.int32":
                            case "system.int64":
                            case "system.double":
                            case "system.single":
                            case "system.byte":
                            case "system.decimal":
                                if (!NumberUtils.IsNumeric(comp[f])) comp[f] = Convert.ToString(0);
                                comp[f] = TextUtil.Replace(comp[f], ",", ".");
                                break;
                            case "system.datetime":
                                comp[f] = "'" + comp[f] + "'";
                                break;
                            case "system.char":
                            case "system.string":
                                comp[f] = "'" + comp[f] + "'";
                                break;
                            case "system.boolean":
                                if (!((comp[f] == "1") | (comp[f].ToLower() == "true"))) comp[f] = "false";
                                comp[f] = comp[f];
                                break;
                        }
                    }
                }

                if (cond[f].IndexOf("@") != -1)
                {
                    typ = TextUtil.Replace(db.GetField(field[f], DataControl.TableName).Tipo.ToString().ToLower(),
                        "system.",
                        "");
                    switch (typ)
                    {
                        case "int16":
                        case "int32":
                        case "int64":
                        case "double":
                        case "single":
                        case "byte":
                        case "decimal":
                            cond[f] = TextUtil.Replace(cond[f], "@", "-1");
                            break;
                        case "datetime":
                            cond[f] = TextUtil.Replace(cond[f], "@", "'null'");
                            break;
                        case "char":
                        case "string":
                            cond[f] = TextUtil.Replace(cond[f], "@", "'null'");
                            break;
                        case "boolean":
                            cond[f] = TextUtil.Replace(cond[f], "@", "-1");
                            break;
                    }
                }

                if (f > 0)
                    union[f] =
                        ReplaceCompSign(
                            ((ComboBox) FunctionsForms.GetControlByName(FindForm().Controls, "cboUnion" + f)).Text);

                if (cond[f].StartsWith("isnull"))
                {
                    if ((field[f] != "") & (cond[f] != ""))
                    {
                        cond[f] = TextUtil.Replace(cond[f], "#", field[f]);
                        sqlWhere[f] = sqlWhere[f] + " " + cond[f];
                    }
                }
                else
                {
                    if ((field[f] != "") & (cond[f] != "") & (comp[f] != ""))
                        sqlWhere[f] = sqlWhere[f] + " " + field[f] + " " + cond[f] + " " + comp[f];
                }
            }

            sql = sqlWhere[0];

            if ((union[1] != "") & (sqlWhere[1] != "")) sql = sqlWhere[0] + " " + union[1] + " " + sqlWhere[1];
            if ((union[2] != "") & (sqlWhere[2] != "")) sql = sql + " " + union[2] + " " + sqlWhere[2];
            if ((union[3] != "") & (sqlWhere[3] != "")) sql = sql + " " + union[3] + " " + sqlWhere[3];
            if ((union[4] != "") & (sqlWhere[4] != "")) sql = sql + " " + union[4] + " " + sqlWhere[4];

            Filter = TextUtil.Replace(sql, "  ", " ");
            Close();
        }


        private string ReplaceCondSign(string cond)
        {
            if (string.IsNullOrEmpty(cond)) return string.Empty;

            switch (cond)
            {
                case "Igual":
                    return "=";
                case "Menor/igual que":
                    return "<=";
                case "Mayor/igual que":
                    return ">=";
                case "Menor que":
                    return "<";
                case "Mayor que":
                    return ">";
                case "Diferente":
                    return "<>";
                case "Empieza por":
                    return "like '#%'";
                case "Termina por":
                    return "like '%#'";
                case "Contiene":
                    return "like '%#%'";
                case "Es Vacio":
                    return "isnull (#,@)=@";
                case "No es Vacio":
                    return "isnull (#,@)<>@";
                default:
                    return string.Empty;
            }
        }


        private string ReplaceCompSign(string comp)
        {
            if (string.IsNullOrEmpty(comp)) return string.Empty;

            switch (comp)
            {
                case "?":
                    return "or";
                case "Y":
                    return "and";
                case "NO":
                    return "not";
                default:
                    return string.Empty;
            }
        }


        private void cboCampo0_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboComparacion0.Enabled = true;
            cboComparacion0.BackColor = Color.White;

            FillCond(cboCampo0, cboComparacion0);
        }

        private void cboCampo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboComparacion1.Enabled = true;
            cboComparacion1.BackColor = Color.White;

            FillCond(cboCampo1, cboComparacion1);
        }

        private void cboCampo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboComparacion2.Enabled = true;
            cboComparacion2.BackColor = Color.White;

            FillCond(cboCampo2, cboComparacion2);
        }

        private void cboCampo3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboComparacion3.Enabled = true;
            cboComparacion3.BackColor = Color.White;

            FillCond(cboCampo3, cboComparacion3);
        }

        private void cboCampo4_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboComparacion4.Enabled = true;
            cboComparacion4.BackColor = Color.White;

            FillCond(cboCampo4, cboComparacion4);
        }


        private void cboComparacion0_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTextoComparado0.Enabled = true;
            txtTextoComparado0.BackColor = Color.White;
        }


        private void cboComparacion1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTextoComparado1.Enabled = true;
            txtTextoComparado1.BackColor = Color.White;
        }


        private void cboComparacion2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTextoComparado2.Enabled = true;
            txtTextoComparado2.BackColor = Color.White;
        }


        private void cboComparacion3_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTextoComparado3.Enabled = true;
            txtTextoComparado3.BackColor = Color.White;
        }


        private void cboComparacion4_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTextoComparado4.Enabled = true;
            txtTextoComparado4.BackColor = Color.White;
        }


        private void txtTextoComparado0_TextChanged(object sender, EventArgs e)
        {
            cboUnion1.Enabled = true;
            cboUnion1.BackColor = Color.White;
        }


        private void txtTextoComparado1_TextChanged(object sender, EventArgs e)
        {
            cboUnion2.Enabled = true;
            cboUnion2.BackColor = Color.White;
        }


        private void txtTextoComparado2_TextChanged(object sender, EventArgs e)
        {
            cboUnion3.Enabled = true;
            cboUnion3.BackColor = Color.White;
        }


        private void txtTextoComparado3_TextChanged(object sender, EventArgs e)
        {
            cboUnion4.Enabled = true;
            cboUnion4.BackColor = Color.White;
        }


        private void cboUnion1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCampo1.Enabled = true;
            cboCampo1.BackColor = Color.White;
        }


        private void cboUnion2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCampo2.Enabled = true;
            cboCampo2.BackColor = Color.White;
        }


        private void cboUnion3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCampo3.Enabled = true;
            cboCampo3.BackColor = Color.White;
        }


        private void cboUnion4_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCampo4.Enabled = true;
            cboCampo4.BackColor = Color.White;
        }


        private void FillCond(ComboBox cboField, ComboBox cboCond)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            string fielddb = null;

            if (!(DataControl.ColumnMapping.FindByHeaderCaption(cboField.SelectedItem + "") == null))
                fielddb = DataControl.ColumnMapping.FindByHeaderCaption(cboField.SelectedItem + "").FieldDB;
            else
                fielddb = "";

            var tipo = db.GetField(fielddb, DataControl.TableName).Tipo.ToString().ToLower();
            var typ = TextUtil.Replace(tipo, "system.", "");
            switch (typ)
            {
                case "int16":
                case "int32":
                case "int64":
                case "double":
                case "single":
                case "byte":
                case "decimal":
                    CondNumeric(cboCond);
                    break;
                case "datetime":
                    CondDateTime(cboCond);
                    break;
                case "char":
                case "string":
                    CondText(cboCond);
                    break;
                case "boolean":
                    CondBoolean(cboCond);
                    break;
                default:
                    CondText(cboCond);
                    break;
            }
        }

        #region '"Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        public Label _Label1_0;
        public Label _Label1_1;

        public Label _Label1_2;

        //public System.Windows.Forms.ToolTip ToolTip1; 
        public Label _Line1_0;
        public Label _Line1_1;
        public ComboBox cboCampo0;
        public ComboBox cboCampo1;
        public ComboBox cboCampo2;
        public ComboBox cboCampo3;
        public ComboBox cboCampo4;
        public ComboBox cboComparacion0;
        public ComboBox cboComparacion1;
        public ComboBox cboComparacion2;
        public ComboBox cboComparacion3;
        public ComboBox cboComparacion4;
        public ComboBox cboUnion1;
        public ComboBox cboUnion2;
        public ComboBox cboUnion3;
        public ComboBox cboUnion4;
        public Button cmdAceptar;
        public Button cmdCancelar;
        internal Label lblFilter;
        public TextBox txtTextoComparado0;
        public TextBox txtTextoComparado1;
        public TextBox txtTextoComparado2;
        public TextBox txtTextoComparado3;
        public TextBox txtTextoComparado4;

        public frmFilter()
        {
            InitializeComponent();

            cboUnion4.SelectedIndexChanged += cboUnion4_SelectedIndexChanged;
            cboComparacion4.SelectedIndexChanged += cboComparacion4_SelectedIndexChanged;
            cboCampo4.SelectedIndexChanged += cboCampo4_SelectedIndexChanged;
            cboUnion3.SelectedIndexChanged += cboUnion3_SelectedIndexChanged;
            txtTextoComparado3.TextChanged += txtTextoComparado3_TextChanged;
            cboComparacion3.SelectedIndexChanged += cboComparacion3_SelectedIndexChanged;
            cboCampo3.SelectedIndexChanged += cboCampo3_SelectedIndexChanged;
            cboUnion2.SelectedIndexChanged += cboUnion2_SelectedIndexChanged;
            txtTextoComparado2.TextChanged += txtTextoComparado2_TextChanged;
            cboComparacion2.SelectedIndexChanged += cboComparacion2_SelectedIndexChanged;
            cboCampo2.SelectedIndexChanged += cboCampo2_SelectedIndexChanged;
            cboUnion1.SelectedIndexChanged += cboUnion1_SelectedIndexChanged;
            txtTextoComparado1.TextChanged += txtTextoComparado1_TextChanged;
            txtTextoComparado0.TextChanged += txtTextoComparado0_TextChanged;
            cboComparacion1.SelectedIndexChanged += cboComparacion1_SelectedIndexChanged;
            cboComparacion0.SelectedIndexChanged += cboComparacion0_SelectedIndexChanged;
            cboCampo1.SelectedIndexChanged += cboCampo1_SelectedIndexChanged;
            cboCampo0.SelectedIndexChanged += cboCampo0_SelectedIndexChanged;
            cmdAceptar.Click += cmdAceptar_Click;
            cmdCancelar.Click += cmdCancelar_Click;
        }

        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
                if (!(components == null))
                    components.Dispose();
            base.Dispose(Disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            cboUnion4 = new ComboBox();
            txtTextoComparado4 = new TextBox();
            cboComparacion4 = new ComboBox();
            cboCampo4 = new ComboBox();
            cboUnion3 = new ComboBox();
            txtTextoComparado3 = new TextBox();
            cboComparacion3 = new ComboBox();
            cboCampo3 = new ComboBox();
            cboUnion2 = new ComboBox();
            txtTextoComparado2 = new TextBox();
            cboComparacion2 = new ComboBox();
            cboCampo2 = new ComboBox();
            cboUnion1 = new ComboBox();
            txtTextoComparado1 = new TextBox();
            txtTextoComparado0 = new TextBox();
            cboComparacion1 = new ComboBox();
            cboComparacion0 = new ComboBox();
            cboCampo1 = new ComboBox();
            cboCampo0 = new ComboBox();
            cmdAceptar = new Button();
            cmdCancelar = new Button();
            _Line1_0 = new Label();
            _Line1_1 = new Label();
            _Label1_2 = new Label();
            _Label1_1 = new Label();
            _Label1_0 = new Label();
            lblFilter = new Label();
            SuspendLayout();
            // 
            // cboUnion4
            // 
            cboUnion4.BackColor = SystemColors.InactiveCaption;
            cboUnion4.Cursor = Cursors.Default;
            cboUnion4.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUnion4.Enabled = false;
            cboUnion4.Items.AddRange(new object[]
            {
                "Y",
                "?"
            });
            cboUnion4.Location = new Point(8, 140);
            cboUnion4.Name = "cboUnion4";
            cboUnion4.RightToLeft = RightToLeft.No;
            cboUnion4.Size = new Size(41, 21);
            cboUnion4.TabIndex = 15;
            // 
            // txtTextoComparado4
            // 
            txtTextoComparado4.AcceptsReturn = true;
            txtTextoComparado4.BackColor = SystemColors.InactiveCaption;
            txtTextoComparado4.Cursor = Cursors.IBeam;
            txtTextoComparado4.Enabled = false;
            txtTextoComparado4.Location = new Point(348, 140);
            txtTextoComparado4.MaxLength = 0;
            txtTextoComparado4.Name = "txtTextoComparado4";
            txtTextoComparado4.RightToLeft = RightToLeft.No;
            txtTextoComparado4.Size = new Size(141, 21);
            txtTextoComparado4.TabIndex = 18;
            // 
            // cboComparacion4
            // 
            cboComparacion4.BackColor = SystemColors.InactiveCaption;
            cboComparacion4.Cursor = Cursors.Default;
            cboComparacion4.DropDownStyle = ComboBoxStyle.DropDownList;
            cboComparacion4.Enabled = false;
            cboComparacion4.Items.AddRange(new object[]
            {
                "Igual",
                "Diferente",
                "Mayor que",
                "Menor que",
                "Como"
            });
            cboComparacion4.Location = new Point(236, 140);
            cboComparacion4.Name = "cboComparacion4";
            cboComparacion4.RightToLeft = RightToLeft.No;
            cboComparacion4.Size = new Size(105, 21);
            cboComparacion4.TabIndex = 17;
            // 
            // cboCampo4
            // 
            cboCampo4.BackColor = SystemColors.InactiveCaption;
            cboCampo4.Cursor = Cursors.Default;
            cboCampo4.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo4.Enabled = false;
            cboCampo4.Location = new Point(56, 140);
            cboCampo4.Name = "cboCampo4";
            cboCampo4.RightToLeft = RightToLeft.No;
            cboCampo4.Size = new Size(173, 21);
            cboCampo4.TabIndex = 16;
            // 
            // cboUnion3
            // 
            cboUnion3.BackColor = SystemColors.InactiveCaption;
            cboUnion3.Cursor = Cursors.Default;
            cboUnion3.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUnion3.Enabled = false;
            cboUnion3.Items.AddRange(new object[]
            {
                "Y",
                "?"
            });
            cboUnion3.Location = new Point(8, 112);
            cboUnion3.Name = "cboUnion3";
            cboUnion3.RightToLeft = RightToLeft.No;
            cboUnion3.Size = new Size(41, 21);
            cboUnion3.TabIndex = 11;
            // 
            // txtTextoComparado3
            // 
            txtTextoComparado3.AcceptsReturn = true;
            txtTextoComparado3.BackColor = SystemColors.InactiveCaption;
            txtTextoComparado3.Cursor = Cursors.IBeam;
            txtTextoComparado3.Enabled = false;
            txtTextoComparado3.Location = new Point(348, 112);
            txtTextoComparado3.MaxLength = 0;
            txtTextoComparado3.Name = "txtTextoComparado3";
            txtTextoComparado3.RightToLeft = RightToLeft.No;
            txtTextoComparado3.Size = new Size(141, 21);
            txtTextoComparado3.TabIndex = 14;
            // 
            // cboComparacion3
            // 
            cboComparacion3.BackColor = SystemColors.InactiveCaption;
            cboComparacion3.Cursor = Cursors.Default;
            cboComparacion3.DropDownStyle = ComboBoxStyle.DropDownList;
            cboComparacion3.Enabled = false;
            cboComparacion3.Items.AddRange(new object[]
            {
                "Igual",
                "Diferente",
                "Mayor que",
                "Menor que",
                "Como"
            });
            cboComparacion3.Location = new Point(236, 112);
            cboComparacion3.Name = "cboComparacion3";
            cboComparacion3.RightToLeft = RightToLeft.No;
            cboComparacion3.Size = new Size(105, 21);
            cboComparacion3.TabIndex = 13;
            // 
            // cboCampo3
            // 
            cboCampo3.BackColor = SystemColors.InactiveCaption;
            cboCampo3.Cursor = Cursors.Default;
            cboCampo3.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo3.Enabled = false;
            cboCampo3.Location = new Point(56, 112);
            cboCampo3.Name = "cboCampo3";
            cboCampo3.RightToLeft = RightToLeft.No;
            cboCampo3.Size = new Size(173, 21);
            cboCampo3.TabIndex = 12;
            // 
            // cboUnion2
            // 
            cboUnion2.BackColor = SystemColors.InactiveCaption;
            cboUnion2.Cursor = Cursors.Default;
            cboUnion2.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUnion2.Enabled = false;
            cboUnion2.Items.AddRange(new object[]
            {
                "Y",
                "?"
            });
            cboUnion2.Location = new Point(8, 84);
            cboUnion2.Name = "cboUnion2";
            cboUnion2.RightToLeft = RightToLeft.No;
            cboUnion2.Size = new Size(41, 21);
            cboUnion2.TabIndex = 7;
            // 
            // txtTextoComparado2
            // 
            txtTextoComparado2.AcceptsReturn = true;
            txtTextoComparado2.BackColor = SystemColors.InactiveCaption;
            txtTextoComparado2.Cursor = Cursors.IBeam;
            txtTextoComparado2.Enabled = false;
            txtTextoComparado2.Location = new Point(348, 84);
            txtTextoComparado2.MaxLength = 0;
            txtTextoComparado2.Name = "txtTextoComparado2";
            txtTextoComparado2.RightToLeft = RightToLeft.No;
            txtTextoComparado2.Size = new Size(141, 21);
            txtTextoComparado2.TabIndex = 10;
            // 
            // cboComparacion2
            // 
            cboComparacion2.BackColor = SystemColors.InactiveCaption;
            cboComparacion2.Cursor = Cursors.Default;
            cboComparacion2.DropDownStyle = ComboBoxStyle.DropDownList;
            cboComparacion2.Enabled = false;
            cboComparacion2.Items.AddRange(new object[]
            {
                "Igual",
                "Diferente",
                "Mayor que",
                "Menor que",
                "Como"
            });
            cboComparacion2.Location = new Point(236, 84);
            cboComparacion2.Name = "cboComparacion2";
            cboComparacion2.RightToLeft = RightToLeft.No;
            cboComparacion2.Size = new Size(105, 21);
            cboComparacion2.TabIndex = 9;
            // 
            // cboCampo2
            // 
            cboCampo2.BackColor = SystemColors.InactiveCaption;
            cboCampo2.Cursor = Cursors.Default;
            cboCampo2.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo2.Enabled = false;
            cboCampo2.Location = new Point(56, 84);
            cboCampo2.Name = "cboCampo2";
            cboCampo2.RightToLeft = RightToLeft.No;
            cboCampo2.Size = new Size(173, 21);
            cboCampo2.TabIndex = 8;
            // 
            // cboUnion1
            // 
            cboUnion1.BackColor = SystemColors.InactiveCaption;
            cboUnion1.Cursor = Cursors.Default;
            cboUnion1.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUnion1.Enabled = false;
            cboUnion1.Items.AddRange(new object[]
            {
                "Y",
                "?"
            });
            cboUnion1.Location = new Point(8, 56);
            cboUnion1.Name = "cboUnion1";
            cboUnion1.RightToLeft = RightToLeft.No;
            cboUnion1.Size = new Size(41, 21);
            cboUnion1.TabIndex = 3;
            // 
            // txtTextoComparado1
            // 
            txtTextoComparado1.AcceptsReturn = true;
            txtTextoComparado1.BackColor = SystemColors.InactiveCaption;
            txtTextoComparado1.Cursor = Cursors.IBeam;
            txtTextoComparado1.Enabled = false;
            txtTextoComparado1.Location = new Point(348, 56);
            txtTextoComparado1.MaxLength = 0;
            txtTextoComparado1.Name = "txtTextoComparado1";
            txtTextoComparado1.RightToLeft = RightToLeft.No;
            txtTextoComparado1.Size = new Size(141, 21);
            txtTextoComparado1.TabIndex = 6;
            // 
            // txtTextoComparado0
            // 
            txtTextoComparado0.AcceptsReturn = true;
            txtTextoComparado0.BackColor = SystemColors.InactiveCaption;
            txtTextoComparado0.Cursor = Cursors.IBeam;
            txtTextoComparado0.Enabled = false;
            txtTextoComparado0.Location = new Point(348, 28);
            txtTextoComparado0.MaxLength = 0;
            txtTextoComparado0.Name = "txtTextoComparado0";
            txtTextoComparado0.RightToLeft = RightToLeft.No;
            txtTextoComparado0.Size = new Size(141, 21);
            txtTextoComparado0.TabIndex = 2;
            // 
            // cboComparacion1
            // 
            cboComparacion1.BackColor = SystemColors.InactiveCaption;
            cboComparacion1.Cursor = Cursors.Default;
            cboComparacion1.DropDownStyle = ComboBoxStyle.DropDownList;
            cboComparacion1.Enabled = false;
            cboComparacion1.Items.AddRange(new object[]
            {
                "Igual",
                "Diferente",
                "Mayor que",
                "Menor que",
                "Como"
            });
            cboComparacion1.Location = new Point(236, 56);
            cboComparacion1.Name = "cboComparacion1";
            cboComparacion1.RightToLeft = RightToLeft.No;
            cboComparacion1.Size = new Size(105, 21);
            cboComparacion1.TabIndex = 5;
            // 
            // cboComparacion0
            // 
            cboComparacion0.BackColor = SystemColors.InactiveCaption;
            cboComparacion0.Cursor = Cursors.Default;
            cboComparacion0.DropDownStyle = ComboBoxStyle.DropDownList;
            cboComparacion0.Enabled = false;
            cboComparacion0.Items.AddRange(new object[]
            {
                "Igual",
                "Diferente",
                "Mayor que",
                "Menor que",
                "Como"
            });
            cboComparacion0.Location = new Point(236, 28);
            cboComparacion0.Name = "cboComparacion0";
            cboComparacion0.RightToLeft = RightToLeft.No;
            cboComparacion0.Size = new Size(105, 21);
            cboComparacion0.TabIndex = 1;
            // 
            // cboCampo1
            // 
            cboCampo1.BackColor = SystemColors.InactiveCaption;
            cboCampo1.Cursor = Cursors.Default;
            cboCampo1.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo1.Enabled = false;
            cboCampo1.Location = new Point(56, 56);
            cboCampo1.Name = "cboCampo1";
            cboCampo1.RightToLeft = RightToLeft.No;
            cboCampo1.Size = new Size(173, 21);
            cboCampo1.TabIndex = 4;
            // 
            // cboCampo0
            // 
            cboCampo0.BackColor = SystemColors.Window;
            cboCampo0.Cursor = Cursors.Default;
            cboCampo0.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo0.ForeColor = SystemColors.WindowText;
            cboCampo0.Location = new Point(56, 28);
            cboCampo0.Name = "cboCampo0";
            cboCampo0.RightToLeft = RightToLeft.No;
            cboCampo0.Size = new Size(173, 21);
            cboCampo0.TabIndex = 0;
            // 
            // cmdAceptar
            // 
            cmdAceptar.BackColor = SystemColors.Control;
            cmdAceptar.Cursor = Cursors.Default;
            cmdAceptar.ForeColor = SystemColors.ControlText;
            cmdAceptar.Location = new Point(404, 180);
            cmdAceptar.Name = "cmdAceptar";
            cmdAceptar.RightToLeft = RightToLeft.No;
            cmdAceptar.Size = new Size(85, 24);
            cmdAceptar.TabIndex = 21;
            cmdAceptar.Text = "&Aceptar";
            cmdAceptar.UseVisualStyleBackColor = false;
            // 
            // cmdCancelar
            // 
            cmdCancelar.BackColor = SystemColors.Control;
            cmdCancelar.Cursor = Cursors.Default;
            cmdCancelar.DialogResult = DialogResult.Cancel;
            cmdCancelar.ForeColor = SystemColors.ControlText;
            cmdCancelar.Location = new Point(304, 180);
            cmdCancelar.Name = "cmdCancelar";
            cmdCancelar.RightToLeft = RightToLeft.No;
            cmdCancelar.Size = new Size(85, 24);
            cmdCancelar.TabIndex = 20;
            cmdCancelar.Text = "&Quitar Filtro";
            cmdCancelar.UseVisualStyleBackColor = false;
            // 
            // _Line1_0
            // 
            _Line1_0.BackColor = Color.FromArgb(128, 128, 128);
            _Line1_0.Location = new Point(8, 168);
            _Line1_0.Name = "_Line1_0";
            _Line1_0.Size = new Size(482, 1);
            _Line1_0.TabIndex = 22;
            _Line1_0.Tag = "h;1";
            // 
            // _Line1_1
            // 
            _Line1_1.BackColor = Color.White;
            _Line1_1.Location = new Point(8, 169);
            _Line1_1.Name = "_Line1_1";
            _Line1_1.Size = new Size(482, 1);
            _Line1_1.TabIndex = 23;
            _Line1_1.Tag = "h;1";
            // 
            // _Label1_2
            // 
            _Label1_2.BackColor = SystemColors.Control;
            _Label1_2.Cursor = Cursors.Default;
            _Label1_2.ForeColor = SystemColors.ControlText;
            _Label1_2.Location = new Point(348, 12);
            _Label1_2.Name = "_Label1_2";
            _Label1_2.RightToLeft = RightToLeft.No;
            _Label1_2.Size = new Size(112, 17);
            _Label1_2.TabIndex = 24;
            _Label1_2.Text = "Comparado con:";
            // 
            // _Label1_1
            // 
            _Label1_1.BackColor = SystemColors.Control;
            _Label1_1.Cursor = Cursors.Default;
            _Label1_1.ForeColor = SystemColors.ControlText;
            _Label1_1.Location = new Point(236, 12);
            _Label1_1.Name = "_Label1_1";
            _Label1_1.RightToLeft = RightToLeft.No;
            _Label1_1.Size = new Size(76, 17);
            _Label1_1.TabIndex = 23;
            _Label1_1.Text = "Comparación:";
            // 
            // _Label1_0
            // 
            _Label1_0.BackColor = SystemColors.Control;
            _Label1_0.Cursor = Cursors.Default;
            _Label1_0.ForeColor = SystemColors.ControlText;
            _Label1_0.Location = new Point(56, 12);
            _Label1_0.Name = "_Label1_0";
            _Label1_0.RightToLeft = RightToLeft.No;
            _Label1_0.Size = new Size(53, 17);
            _Label1_0.TabIndex = 22;
            _Label1_0.Text = "Campo:";
            // 
            // lblFilter
            // 
            lblFilter.Location = new Point(8, 176);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(288, 32);
            lblFilter.TabIndex = 25;
            lblFilter.Text = "Filtro:";
            // 
            // frmFilter
            // 
            AcceptButton = cmdAceptar;
            AutoScaleBaseSize = new Size(5, 13);
            CancelButton = cmdCancelar;
            ClientSize = new Size(500, 215);
            Controls.Add(lblFilter);
            Controls.Add(cboUnion4);
            Controls.Add(txtTextoComparado4);
            Controls.Add(cboComparacion4);
            Controls.Add(cboCampo4);
            Controls.Add(cboUnion3);
            Controls.Add(txtTextoComparado3);
            Controls.Add(cboComparacion3);
            Controls.Add(cboCampo3);
            Controls.Add(cboUnion2);
            Controls.Add(txtTextoComparado2);
            Controls.Add(cboComparacion2);
            Controls.Add(cboCampo2);
            Controls.Add(cboUnion1);
            Controls.Add(txtTextoComparado1);
            Controls.Add(txtTextoComparado0);
            Controls.Add(cboComparacion1);
            Controls.Add(cboComparacion0);
            Controls.Add(cboCampo1);
            Controls.Add(cboCampo0);
            Controls.Add(cmdAceptar);
            Controls.Add(cmdCancelar);
            Controls.Add(_Line1_0);
            Controls.Add(_Line1_1);
            Controls.Add(_Label1_2);
            Controls.Add(_Label1_1);
            Controls.Add(_Label1_0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Location = new Point(152, 244);
            Name = "frmFilter";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Filtro";
            ResumeLayout(false);
        }

        #endregion
    }
}