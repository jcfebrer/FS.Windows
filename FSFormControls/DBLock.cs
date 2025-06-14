#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FSDatabase;
using FSLibrary;
using DateTime = System.DateTime;
using StringConverter = System.ComponentModel.StringConverter;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public class DBLock : DBUserControl
    {
        //private string m_Table;

        public string User { get; set; }


        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenara el identificador del usuario.")]
        public string LOCK_UserField { get; set; }

        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenara la fecha de la operación.")]
        public string LOCK_DateTimeField { get; set; }

        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenara el recurso accedido.")]
        public string LOCK_RecurseField { get; set; }

        [TypeConverter(typeof(FieldList))]
        [DefaultValueAttribute("")]
        [Description("Campo de la base de datos que almacenara el código de recurso accedido.")]
        public string LOCK_RecurseCodeField { get; set; }


        public void Lock(string tableName, string registerCode)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            string ssql;

            if ((tableName == "") | (registerCode == "")) return;

            ssql = "INSERT INTO " + DataControl.TableName + " (" + LOCK_DateTimeField + "," + LOCK_RecurseCodeField + "," +
                   LOCK_RecurseField + "," + LOCK_UserField + ") VALUES ('" + DateTime.Now + "','" + registerCode +
                   "','" + tableName + "','" + User + "')";

            try
            {
                db.ExecuteScalar(ssql);
            }
            catch
            {
                throw new ExceptionUtil("Error almacenando información de bloqueo. SQL: " + ssql);
            }
        }


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }


        public bool IsLock(string tableName, string registerCode)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            string ssql;

            if ((tableName == "") | (registerCode == "")) return false;

            ssql = "SELECT count(*) from " + DataControl.TableName + " WHERE " + LOCK_RecurseCodeField + "='" +
                   registerCode +
                   "' AND " + LOCK_RecurseField + "='" + tableName + "'";

            try
            {
                var drc = Convert.ToInt32(db.ExecuteScalar(ssql));
                if (drc == 0)
                    return false;
                return true;
            }
            catch
            {
                throw new ExceptionUtil("Error almacenando información de bloqueo. SQL: " + ssql);
            }
        }


        public string LockUser(string tableName, string registerCode)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            string ssql;

            if ((tableName == "") | (registerCode == "")) return string.Empty;

            ssql = "SELECT * from " + DataControl.TableName + " WHERE " + LOCK_RecurseCodeField + "='" + registerCode +
                   "' AND " + LOCK_RecurseField + "='" + tableName + "'";

            try
            {
                var user = db.ExecuteReaderSingleRow(ssql, LOCK_UserField);
                return user;
            }
            catch
            {
                throw new ExceptionUtil("Error almacenando información de bloqueo. SQL: " + ssql);
            }
        }


        public DateTime LockDate(string tableName, string registerCode)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            string ssql;

            if ((tableName == "") | (registerCode == "")) return DateTime.Parse("");

            ssql = "SELECT * from " + DataControl.TableName + " WHERE " + LOCK_RecurseCodeField + "='" + registerCode +
                   "' AND " + LOCK_RecurseField + "='" + tableName + "'";

            try
            {
                var da = DateTime.Parse(db.ExecuteReaderSingleRow(ssql, LOCK_DateTimeField));
                return da;
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("Error almacenando información de bloqueo. SQL: " + ssql, ex);
            }
        }


        public void UnLock(string tableName, string registerCode)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            string ssql;

            if ((tableName == "") | (registerCode == "")) return;

            ssql = "DELETE FROM " + DataControl.TableName + " WHERE " + LOCK_RecurseCodeField + "='" + registerCode +
                   "' AND " + LOCK_RecurseField + "='" + tableName + "' AND " + LOCK_UserField + "='" + User + "'";

            try
            {
                db.ExecuteScalar(ssql);
            }
            catch
            {
                throw new ExceptionUtil("Error eliminando información de bloqueo. SQL: " + ssql);
            }
        }


        public void ClearUser(string tableName)
        {
            BdUtils db = new BdUtils(Global.ConnectionString);
            string ssql;

            if (User == "") return;

            ssql = "DELETE FROM " + DataControl.TableName + " WHERE " + LOCK_UserField + "='" + User + "' AND " +
                   LOCK_RecurseField + "='" + tableName + "'";

            try
            {
                db.ExecuteScalar(ssql);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("Error eliminando información de bloqueo por usuario. SQL: " + ssql, ex);
            }
        }

        #region Nested type: FieldList

        public class FieldList : StringConverter
        {
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(DBControl.Fields);
            }


            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }


            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }
        }

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal Label Label1;

        public DBLock()
        {
            InitializeComponent();

            Visible = false;
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
            Label1.BackColor = Color.FromArgb(255, 224, 192);
            Label1.BorderStyle = BorderStyle.FixedSingle;
            Label1.Dock = DockStyle.Fill;
            Label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(0, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(131, 38);
            Label1.TabIndex = 0;
            Label1.Text = "Control de bloqueos";
            Label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DBLock
            // 
            Controls.Add(Label1);
            Name = "DBLock";
            Size = new Size(131, 38);
            ResumeLayout(false);
        }

        #endregion
    }
}