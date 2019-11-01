﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGenCLI.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGenCLI.CodeGenClasses;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class ViewModelTemplate : ViewModelTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.ComponentModel.Dat" +
                    "aAnnotations;\r\n\r\nnamespace ");
            
            #line 11 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".ViewModels\r\n{\r\n\t/// <summary>\r\n    /// ");
            
            #line 14 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(" view model\r\n");
            
            #line 15 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (!string.IsNullOrEmpty(model.Description)) { 
            
            #line default
            #line hidden
            this.Write("    /// ");
            
            #line 16 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Description));
            
            #line default
            #line hidden
            this.Write(".\r\n");
            
            #line 17 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    /// </summary>\r\n    public class ");
            
            #line 19 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write("VM\r\n    {\r\n\t\tpublic ");
            
            #line 21 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write("VM()\r\n        {\r\n");
            
            #line 23 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (model.Relations.ToList().Count > 0) { 
            
            #line default
            #line hidden
            this.Write("            // Relations\r\n");
            
            #line 25 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (model.Relations.Where(r => r.Type == "one-to-many").ToList().Count > 0) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t//// One-to-many\r\n");
            
            #line 28 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "one-to-many")) { 
            
            #line default
            #line hidden
            this.Write("\t\t\tthis.");
            
            #line 29 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" = new List<");
            
            #line 29 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("VM>();\r\n");
            
            #line 30 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 31 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 32 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (model.Relations.Where(r => r.Type == "many-to-many").ToList().Count > 0) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t//// Many-to-many\r\n");
            
            #line 35 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "many-to-many")) { 
            
            #line default
            #line hidden
            this.Write("\t\t\tthis.");
            
            #line 36 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" = new List<");
            
            #line 36 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("VM>();\r\n");
            
            #line 37 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 38 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 39 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        }\r\n\r\n\t\t// Properties\r\n\r\n\t\t/// <summary>\r\n        /// The identifier of ");
            
            #line 45 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(".\r\n        /// </summary>\r\n\t\tpublic Guid Id { get; set; }\r\n");
            
            #line 48 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 foreach (CodeGenModelProperty property in model.Properties) { 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 50 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (!string.IsNullOrEmpty(property.Description)) { 
            
            #line default
            #line hidden
            this.Write("\t\t/// <summary>\r\n        /// ");
            
            #line 52 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Description));
            
            #line default
            #line hidden
            this.Write(".\r\n        /// </summary>\r\n");
            
            #line 54 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("\t\t/// <summary>\r\n        /// The ");
            
            #line 56 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(" property of ");
            
            #line 56 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(".\r\n        /// </summary>\r\n");
            
            #line 58 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 59 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (property.Required) { 
            
            #line default
            #line hidden
            this.Write("        [Required]\r\n");
            
            #line 61 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\tpublic ");
            
            #line 62 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Type));
            
            #line default
            #line hidden
            
            #line 62 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((property.Required ? "" : (property.Type == "Guid" || property.Type == "int" ? "?" : ""))));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 62 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 63 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 65 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (model.Relations.ToList().Count > 0) { 
            
            #line default
            #line hidden
            this.Write("\t\t// Relations\r\n");
            
            #line 67 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (model.Relations.Where(r => r.Type == "many-to-one").ToList().Count > 0) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t\t//// Many-to-one\r\n\r\n");
            
            #line 71 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "many-to-one")) { 
            
            #line default
            #line hidden
            this.Write("\t    /// <summary>\r\n        /// The related foreign key ");
            
            #line 73 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write("Id for ");
            
            #line 73 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" of ");
            
            #line 73 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(".\r\n        /// </summary>\r\n\t\tpublic Guid");
            
            #line 75 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((relation.Required ? "" : "?")));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 75 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("Id { get; set; }\r\n\r\n\t\t/// <summary>\r\n        /// The related ");
            
            #line 78 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" of ");
            
            #line 78 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(".\r\n        /// </summary>\r\n\t\tpublic ");
            
            #line 80 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("VM ");
            
            #line 80 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n\r\n");
            
            #line 82 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 83 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 84 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (model.Relations.Where(r => r.Type == "one-to-many").ToList().Count > 0) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t\t//// One-to-many\r\n\r\n");
            
            #line 88 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "one-to-many")) { 
            
            #line default
            #line hidden
            this.Write("\t\t/// <summary>\r\n        /// The related ");
            
            #line 90 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" of ");
            
            #line 90 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(".\r\n        /// </summary>\r\n\t\tpublic IList<");
            
            #line 92 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("VM> ");
            
            #line 92 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 93 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 94 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 95 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 if (model.Relations.Where(r => r.Type == "many-to-many").ToList().Count > 0) { 
            
            #line default
            #line hidden
            this.Write("\r\n\t\t//// Many-to-many\r\n\r\n");
            
            #line 99 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "many-to-many")) { 
            
            #line default
            #line hidden
            this.Write("\t\t/// <summary>\r\n        /// The related ");
            
            #line 101 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" of ");
            
            #line 101 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(".\r\n        /// </summary>\r\n\t\tpublic IList<");
            
            #line 103 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("VM> ");
            
            #line 103 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 104 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 105 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 106 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\ViewModelTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class ViewModelTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
