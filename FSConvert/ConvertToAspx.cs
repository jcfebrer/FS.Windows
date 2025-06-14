using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace FSConvert
{
	/// <summary>
	/// Class that converts a Windows Forms form or control to an ASP.NET Web Forms page or control.
	/// </summary>
	public class ConvertToAspx
	{
		/// <summary>
		/// Enum desgnating the desired output type.
		/// </summary>
		public enum AspxTypes
		{
			/// <summary>
			/// Página
			/// </summary>
			Page,
			/// <summary>
			/// Control de usuario
			/// </summary>
			UserControl,
		}

		/// <summary>
		/// Enum designating the desired output language.
		/// </summary>
		public enum SourceLanguages
		{
			/// <summary>
			/// C_Sharp
			/// </summary>
			C_Sharp,
			/// <summary>
			/// VbNet
			/// </summary>
			VbNet,
		}

		/// <summary>
		/// The z-index CSS style declaration.
		/// </summary>
		private int _ZIndex;
		/// <summary>
		/// The _Extension depends on the SourceLanguage cs / vb
		/// </summary>
		private string _Extension;
		/// <summary>
		/// The _CodeLanguage is used in the aspx header.
		/// </summary>
		private string _CodeLanguage;
		/// <summary>
		/// The _TypeExtension is aspx for a Page and ascx for a UserControl
		/// </summary>
		private string _TypeExtension;
		/// <summary>
		/// The _AspxHeaderType is Page / Control.
		/// </summary>
		private string _AspxHeaderType;
		/// <summary>
		/// The _FullName is automatic created from the rootControl that should
		/// be converted, if it is not assigned before the convert method is called. 
		/// </summary>
		private string _FullName;
		/// <summary>
		/// The _RootName is the name of the control or the usercontrol.
		/// </summary>
		private string _RootName;
		/// <summary>
		/// The namespace for the aspx Page / ascx Control.
		/// </summary>
		private string _Namespace;
		/// <summary>
		/// The ArrayList _WebControls store all WebControls.
		/// </summary>
		private System.Collections.ArrayList _WebControls;
		private AspxTypes _AspxType;
		private SourceLanguages _SourceLanguage;

		/// <summary>
		/// Nombre completo
		/// </summary>
		public string FullName
		{
			get { return this._FullName; ; }
			set { this._FullName = value; }
		}

		/// <summary>
		/// Nombre raíz
		/// </summary>
		public string RootName
		{
			get { return this._RootName; ; }
			set { this._RootName = value; }
		}

		/// <summary>
		/// Espacio de nombres
		/// </summary>
		public string Namespace
		{
			get { return this._Namespace; ; }
			set { this._Namespace = value; }
		}

		/// <summary>
		/// Tipo de aspx. Página o Control de Usuario.
		/// </summary>
		public AspxTypes AspxType
		{
			get { return this._AspxType; ; }
			set
			{
				this._AspxType = value;
				switch (this._AspxType)
				{
					case AspxTypes.Page:
						this._TypeExtension = "aspx";
						this._AspxHeaderType = "Page";
						break;
					case AspxTypes.UserControl:
						this._TypeExtension = "ascx";
						this._AspxHeaderType = "Control";
						break;
				}
			}
		}

		/// <summary>
		/// Lenguage origen
		/// </summary>
		public SourceLanguages SourceLanguage
		{
			get { return this._SourceLanguage; ; }
			set
			{
				this._SourceLanguage = value;
				switch (this._SourceLanguage)
				{
					case SourceLanguages.C_Sharp:
						this._CodeLanguage = "C#";
						this._Extension = "cs";
						break;
					case SourceLanguages.VbNet:
						this._CodeLanguage = "VB";
						this._Extension = "vb";
						break;
				}
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public ConvertToAspx()
		{
			this.AspxType = AspxTypes.Page;
			this.SourceLanguage = SourceLanguages.C_Sharp;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="aspxType"></param>
		public ConvertToAspx(AspxTypes aspxType)
		{
			this.AspxType = aspxType;
			this.SourceLanguage = SourceLanguages.C_Sharp;
		}

		/// <summary>
		/// The method converts the rootControl to an aspx page / usercontrol.
		/// </summary>
		/// <param name="rootControl">
		/// The rootControl that should be converted.
		/// </param>
		/// <param name="path">
		/// The path the aspx source should be written to.
		/// </param>
		public void Convert(System.Windows.Forms.Control rootControl, string path)
		{
			string tempFileName;
			System.IO.StreamWriter streamWriter;
			System.Text.StringBuilder stringBuilder;

			if (rootControl == null)
			{
				return;
			}

			this.CheckNames(rootControl);
			this._WebControls = new System.Collections.ArrayList();
			this._ZIndex = 100;
			stringBuilder = new System.Text.StringBuilder();
			stringBuilder.AppendFormat("<%@ {0} language=\"{1}\" Codebehind=\"{2}.{3}.{4}\" AutoEventWireup=\"false\" Inherits=\"{5}\" %>{6}", this._AspxHeaderType, this._CodeLanguage, this._RootName, this._TypeExtension, this._Extension, this._FullName, System.Environment.NewLine);

			if (this._AspxType == AspxTypes.Page)
			{
				stringBuilder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\" >" + System.Environment.NewLine);
				stringBuilder.Append("<HTML>" + System.Environment.NewLine);
				stringBuilder.Append("<HEAD>" + System.Environment.NewLine);
				stringBuilder.AppendFormat("<title>{0}</title>{1}", rootControl.Text, System.Environment.NewLine);
				stringBuilder.Append("<meta name=\"GENERATOR\" Content=\"form.suite4.net\">" + System.Environment.NewLine);
				stringBuilder.AppendFormat("<meta name=\"CODE_LANGUAGE\" Content=\"{0}\">{1}", this._CodeLanguage, System.Environment.NewLine);
				stringBuilder.Append("<meta name=\"vs_defaultClientScript\" content=\"JavaScript\">" + System.Environment.NewLine);
				stringBuilder.Append("<meta name=\"vs_targetSchema\" content=\"http://schemas.microsoft.com/intellisense/ie5\">" + System.Environment.NewLine);
				stringBuilder.Append("</HEAD>" + System.Environment.NewLine);
				stringBuilder.Append("<body MS_POSITIONING=\"GridLayout\">" + System.Environment.NewLine);
				stringBuilder.AppendFormat("<form id=\"{0}\" method=\"post\" runat=\"server\">{1}", this._RootName, System.Environment.NewLine);
			}

			this.ConvertControls(rootControl, stringBuilder);

			if (this._AspxType == AspxTypes.Page)
			{
				stringBuilder.Append("</form>" + System.Environment.NewLine);
				stringBuilder.Append("</body>" + System.Environment.NewLine);
				stringBuilder.Append("</HTML>" + System.Environment.NewLine);
			}

			tempFileName = path + string.Format("\\{0}.{1}", this._RootName, this._TypeExtension);
			streamWriter = new System.IO.StreamWriter(tempFileName, false, System.Text.Encoding.Default);
			streamWriter.Write(stringBuilder.ToString());
			streamWriter.Flush();
			streamWriter.Close();
			streamWriter = null;
			stringBuilder = null;
			tempFileName = path + string.Format("\\{0}.{1}.{2}", this._RootName, this._TypeExtension, this._Extension);
			streamWriter = new System.IO.StreamWriter(tempFileName, false, System.Text.Encoding.Default);
			streamWriter.Write(this.BuildCodeBehind());
			streamWriter.Flush();
			streamWriter.Close();
			streamWriter = null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rootControl"></param>
		/// <param name="stringBuilder"></param>
		private void ConvertControls(System.Windows.Forms.Control rootControl, System.Text.StringBuilder stringBuilder)
		{
#if NETFRAMEWORK
			System.Web.UI.WebControls.Label webLabel;
			System.Web.UI.WebControls.TextBox webTextBox;
			System.Web.UI.HtmlControls.HtmlGenericControl webGroupBox;

			foreach (System.Windows.Forms.Control control in rootControl.Controls)
			{
				if (control is System.Windows.Forms.Label || control.GetType().ToString() == "FSFormControls.DBLabel")
				{
					webLabel = new System.Web.UI.WebControls.Label();
					webLabel.ID = control.Name;
					this._WebControls.Add(webLabel);
					stringBuilder.Append("<asp:Label");
					this.AddProperties(control, stringBuilder);
					stringBuilder.AppendFormat(">{0}</asp:Label>{1}", control.Text, System.Environment.NewLine);
				}
				else if (control is System.Windows.Forms.TextBox || control.GetType().ToString() == "FSFormControls.DBTextBox")
				{
					webTextBox = new System.Web.UI.WebControls.TextBox();
					webTextBox.ID = control.Name;
					this._WebControls.Add(webTextBox);
					stringBuilder.Append("<asp:TextBox");
					this.AddProperties(control, stringBuilder);
					stringBuilder.AppendFormat(">{0}</asp:TextBox>{1}", control.Text, System.Environment.NewLine);
				}
				else if (control.HasChildren)
				{
					stringBuilder.Append("<fieldset");
					stringBuilder.AppendFormat(" ID=\"{0}\" runat=\"server\"", control.Name);
					stringBuilder.AppendFormat(" style=\"POSITION: absolute; left: {0}px; top: {1}px; width:{2}px; height: {3}\"", control.Left, control.Top, control.Width, control.Height);
					stringBuilder.AppendFormat(">{0}", System.Environment.NewLine);
					stringBuilder.Append("<legend");
					stringBuilder.AppendFormat(" style=\"Z-INDEX: {0}; color:black; font-family:'{1}'; font-size:{2}pt; width=\"", this._ZIndex++, control.Font.FontFamily.Name, (int)control.Font.Size);
					stringBuilder.AppendFormat(">{0}</legend>{1}", control.Text, System.Environment.NewLine);
					this.ConvertControls(control, stringBuilder);
					stringBuilder.AppendFormat("</fieldset>{0}", System.Environment.NewLine);
					webGroupBox = new System.Web.UI.HtmlControls.HtmlGenericControl("fieldset");
					webGroupBox.ID = control.Name;
					this._WebControls.Add(webGroupBox);
				}
			}
#endif
        }

        /// <summary>
        /// The method assigns values to the _FullName, _RootName and _Namespace 
        /// private variables based on the rootControl' type properties.
        /// </summary>
        /// <param name="rootControl"></param>
        private void CheckNames(System.Windows.Forms.Control rootControl)
		{
			if (this._FullName == null)
			{
				this._FullName = rootControl.GetType().FullName;
			}
			if (this._RootName == null)
			{
				this._RootName = rootControl.GetType().Name;
			}
			if (this._Namespace == null)
			{
				this._Namespace = rootControl.GetType().Namespace;
			}
		}

		/// <summary>
		/// Add the properies from the Windows Control to aspx stringBuilder.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="stringBuilder"></param>
		private void AddProperties(System.Windows.Forms.Control control, System.Text.StringBuilder stringBuilder)
		{
			stringBuilder.AppendFormat(" id=\"{0}\"", control.Name);
			stringBuilder.AppendFormat(" style=\"z-index:{0}; left:{1}px; top:{2}px; font-family:'{3}'; font-size:{4}pt; position:absolute;\"", this._ZIndex++, control.Left, control.Top, control.Font.FontFamily.Name, (int)control.Font.Size);
			stringBuilder.Append(" runat=\"server\"");
			stringBuilder.AppendFormat(" Width=\"{0}\"", control.Width);
			stringBuilder.AppendFormat(" Height=\"{0}\"", control.Height);

			if (control is System.Windows.Forms.TextBox)
			{
				stringBuilder.AppendFormat(" TabIndex=\"{0}\"", control.TabIndex);
			}
		}

		/// <summary>
		/// The method builds the CodeDom and returns a string with the source.
		/// </summary>
		/// <returns></returns>
		private string BuildCodeBehind()
		{
			string resultString;
			System.IO.StringWriter stringWriter;
			System.CodeDom.CodeNamespace codeUsing;
			System.CodeDom.CodeNamespace codeNamespace;
			System.CodeDom.CodeCompileUnit codeCompileUnit;
			System.CodeDom.CodeTypeDeclaration codeTypeDeclaration;
			CodeDomProvider codeDomProvider = null;
			System.CodeDom.Compiler.CodeGeneratorOptions codeGeneratorOptions;
			// Initialize Header with namespace and using declarations
			codeCompileUnit = new System.CodeDom.CodeCompileUnit();
			codeUsing = new System.CodeDom.CodeNamespace();
			codeUsing.Imports.Add(new System.CodeDom.CodeNamespaceImport("System"));
			codeNamespace = new System.CodeDom.CodeNamespace(this._Namespace);
			// Initialize TypeDeclaration private declaration
			codeTypeDeclaration = new System.CodeDom.CodeTypeDeclaration(this._RootName);

#if NETFRAMEWORK
            switch (this._AspxType)
			{
				case AspxTypes.Page:
					codeTypeDeclaration.BaseTypes.Add(typeof(System.Web.UI.Page));
					break;
				case AspxTypes.UserControl:
					codeTypeDeclaration.BaseTypes.Add(typeof(System.Web.UI.UserControl));
					break;
			}
#endif

			this.GenerateFields(codeTypeDeclaration);
			this.BuildOnInitMethod(codeTypeDeclaration);
			this.BuildPageLoadMethod(codeTypeDeclaration);
			this.BuildInitializeComponentMethod(codeTypeDeclaration);
			codeNamespace.Types.Add(codeTypeDeclaration);
			codeCompileUnit.Namespaces.Add(codeUsing);
			codeCompileUnit.Namespaces.Add(codeNamespace);

			switch (this._SourceLanguage)
			{
				case SourceLanguages.C_Sharp:
					if (CodeDomProvider.IsDefinedLanguage("CSharp"))
					{
						codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
					}
					else
						throw new Exception("Imposible crear el proveedor para: CSharp.");
					break;
				case SourceLanguages.VbNet:
					if (CodeDomProvider.IsDefinedLanguage("VB"))
					{
						codeDomProvider = CodeDomProvider.CreateProvider("VB");
					}
					else
						throw new Exception("Imposible crear el proveedor para: VB.NET.");
					break;
			}

			if (codeDomProvider != null)
			{
				// init codeGeneratorOptions
				codeGeneratorOptions = new System.CodeDom.Compiler.CodeGeneratorOptions();
				codeGeneratorOptions.BlankLinesBetweenMembers = false;
				codeGeneratorOptions.BracingStyle = "C";
				codeGeneratorOptions.ElseOnClosing = true;
				codeGeneratorOptions.IndentString = "\t";
				stringWriter = new System.IO.StringWriter();

				codeDomProvider.GenerateCodeFromCompileUnit(codeCompileUnit, stringWriter, codeGeneratorOptions);
				resultString = stringWriter.ToString();
				stringWriter.Close();
				return resultString;
			}

			return null;
		}

		/// <summary>
		/// Generate the declaration for all WebControls that are stored in the Arraylist _WebControls.
		/// </summary>
		/// <param name="typeDeclaration"></param>
		private void GenerateFields(System.CodeDom.CodeTypeDeclaration typeDeclaration)
		{
			System.CodeDom.CodeMemberField memberField;

#if NETFRAMEWORK
            foreach (System.Web.UI.Control webControl in this._WebControls)
			{
				memberField = new System.CodeDom.CodeMemberField(webControl.GetType(), webControl.ID);
				memberField.Attributes = System.CodeDom.MemberAttributes.Family;
				typeDeclaration.Members.Add(memberField);
			}
#endif

			// The following placeholder declaration is required by the Web Form Designer.
			// it is only necessary for Vb.Net
			if (this._SourceLanguage == SourceLanguages.VbNet)
			{
				memberField = new System.CodeDom.CodeMemberField(typeof(System.Object), "designerPlaceholderDeclaration");
				memberField.Attributes = System.CodeDom.MemberAttributes.Family;
				typeDeclaration.Members.Add(memberField);
			}
		}

		/// <summary>
		/// Build the code-behind Page_Load method.
		/// </summary>
		private void BuildPageLoadMethod(System.CodeDom.CodeTypeDeclaration typeDeclaration)
		{
			System.CodeDom.CodeMemberMethod codeMethodPageLoad;
			System.CodeDom.CodeParameterDeclarationExpression codeParameterExpression;
			// Add Page_Load method
			codeMethodPageLoad = new System.CodeDom.CodeMemberMethod();
			codeMethodPageLoad.Name = "Page_Load";
			// Add sender parameter
			codeParameterExpression = new System.CodeDom.CodeParameterDeclarationExpression(typeof(object), "sender");
			codeMethodPageLoad.Parameters.Add(codeParameterExpression);
			// Add eventargs parameter
			codeParameterExpression = new System.CodeDom.CodeParameterDeclarationExpression(typeof(System.EventArgs), "e");
			codeMethodPageLoad.Parameters.Add(codeParameterExpression);
			typeDeclaration.Members.Add(codeMethodPageLoad);
		}

		/// <summary>
		/// Build the code-behind OnInit method.
		/// </summary>
		/// <param name="typeDeclaration"></param>
		private void BuildOnInitMethod(System.CodeDom.CodeTypeDeclaration typeDeclaration)
		{
			System.CodeDom.CodeMemberMethod codeMethodOnInit;
			System.CodeDom.CodeMethodInvokeExpression codeMethodInvoke;
			System.CodeDom.CodeParameterDeclarationExpression codeParameterExpression;
			codeMethodOnInit = new System.CodeDom.CodeMemberMethod();
			codeMethodOnInit.Name = "OnInit";
			codeMethodOnInit.Attributes = System.CodeDom.MemberAttributes.Family | System.CodeDom.MemberAttributes.Override;
			codeParameterExpression = new System.CodeDom.CodeParameterDeclarationExpression(typeof(System.EventArgs), "e");
			codeMethodOnInit.Parameters.Add(codeParameterExpression);
			codeMethodInvoke = new System.CodeDom.CodeMethodInvokeExpression(new System.CodeDom.CodeThisReferenceExpression(), "InitializeComponent");
			codeMethodOnInit.Statements.Add(codeMethodInvoke);
			codeMethodInvoke = new System.CodeDom.CodeMethodInvokeExpression(new System.CodeDom.CodeBaseReferenceExpression(), "OnInit", new System.CodeDom.CodeExpression[] { new System.CodeDom.CodeFieldReferenceExpression(null, "e") });
			codeMethodOnInit.Statements.Add(codeMethodInvoke);
			typeDeclaration.Members.Add(codeMethodOnInit);
		}

		/// <summary>
		/// Build the code-behind InitializeComponent method.
		/// </summary>
		/// <param name="typeDeclaration"></param>
		private void BuildInitializeComponentMethod(System.CodeDom.CodeTypeDeclaration typeDeclaration)
		{
			System.CodeDom.CodeMemberMethod codeMethodInitializeComponent;
			System.CodeDom.CodeAttachEventStatement attachEventStatement;
			codeMethodInitializeComponent = new System.CodeDom.CodeMemberMethod();
			codeMethodInitializeComponent.Name = "InitializeComponent";
			attachEventStatement = new System.CodeDom.CodeAttachEventStatement();
			attachEventStatement.Event = new System.CodeDom.CodeEventReferenceExpression(new System.CodeDom.CodeThisReferenceExpression(), "Load");
			attachEventStatement.Listener = new System.CodeDom.CodeDelegateCreateExpression(new System.CodeDom.CodeTypeReference(typeof(System.EventHandler)), new System.CodeDom.CodeThisReferenceExpression(), "Page_Load");
			codeMethodInitializeComponent.Statements.Add(attachEventStatement);
			typeDeclaration.Members.Add(codeMethodInitializeComponent);
		}
	}
}