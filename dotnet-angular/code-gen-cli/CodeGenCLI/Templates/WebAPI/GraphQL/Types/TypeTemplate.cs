﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGenCLI.Templates.WebAPI.GraphQL.Types
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGenCLI.CodeGenClasses;
    using CodeGenCLI.Extensions;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class TypeTemplate : TypeTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using GraphQL.Types;\r\nusing System;\r\nusing ");
            
            #line 10 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".DAL.Repositories;\r\nusing ");
            
            #line 11 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Models;\r\n\r\nnamespace ");
            
            #line 13 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".GraphQL.Types\r\n{\r\n    public class ");
            
            #line 15 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write("Type : ObjectGraphType<");
            
            #line 15 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(">\r\n    {\r\n        public ");
            
            #line 17 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write("Type(\r\n");
            
            #line 18 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "one-to-many" || r.Type == "many-to-one")) { 
            
            #line default
            #line hidden
            
            #line 19 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 if (relation.Model != model.Name) { 
            
            #line default
            #line hidden
            this.Write("            ");
            
            #line 20 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("Repository ");
            
            #line 20 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Repository,\r\n");
            
            #line 21 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 22 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t\t");
            
            #line 23 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write("Repository ");
            
            #line 23 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Repository");
            
            #line 23 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((model.Relations.Where(r => r.Type == "many-to-many" && !string.IsNullOrEmpty(r.Through)).ToList().Count() > 0 ? "," : "")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 24 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "many-to-many" && !string.IsNullOrEmpty(r.Through))) { 
            
            #line default
            #line hidden
            
            #line 25 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 CodeGenModelRelation lastModelManyToManyRelation = model.Relations.Where(r => r.Type == "many-to-many" && !string.IsNullOrEmpty(r.Through)).Last(); 
            
            #line default
            #line hidden
            
            #line 26 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 if (relation.Model != model.Name) { 
            
            #line default
            #line hidden
            this.Write("            ");
            
            #line 27 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("Repository ");
            
            #line 27 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Repository,\r\n");
            
            #line 28 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t\t");
            
            #line 29 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Through));
            
            #line default
            #line hidden
            this.Write("Repository ");
            
            #line 29 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Through.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Repository");
            
            #line 29 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((!relation.Equals(lastModelManyToManyRelation) ? "," : "")));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 30 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        )\r\n        {\r\n            Field(x => x.Id, type: typeof(IdGraphType));\r\n");
            
            #line 34 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 foreach (CodeGenModelProperty property in model.Properties) { 
            
            #line default
            #line hidden
            this.Write("            Field(x => x.");
            
            #line 35 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            
            #line 35 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!property.Required ? ", nullable: true" : ""));
            
            #line default
            #line hidden
            this.Write(")");
            
            #line 35 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(property.Description) ? ".Description(" + property.Description + ")" : ""));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 36 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 38 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "many-to-one")) { 
            
            #line default
            #line hidden
            this.Write("            Field<");
            
            #line 39 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("Type>(\r\n                \"");
            
            #line 40 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("\",\r\n                resolve: context =>\r\n                {\r\n                    i" +
                    "f (context.Source.");
            
            #line 43 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("Id != null)\r\n                        return ");
            
            #line 44 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Repository.GetById((Guid)context.Source.");
            
            #line 44 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("Id);\r\n                    return null;\r\n                }\r\n            );\r\n");
            
            #line 48 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 49 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "one-to-many")) { 
            
            #line default
            #line hidden
            
            #line 50 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 CodeGenModel relationModel = config.Models.Single(m => m.Name == relation.Model); 
            
            #line default
            #line hidden
            this.Write("            Field<ListGraphType<");
            
            #line 51 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model));
            
            #line default
            #line hidden
            this.Write("Type>>(\r\n                \"");
            
            #line 52 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((!string.IsNullOrEmpty(relationModel.NamePlural) ? relationModel.NamePlural : relationModel.Name + "s").ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("\",\r\n                resolve: context => ");
            
            #line 53 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Model.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Repository.GetBy");
            
            #line 53 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write("Id(context.Source.Id)\r\n            );\r\n\r\n");
            
            #line 56 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 57 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 foreach (CodeGenModelRelation relation in model.Relations.Where(r => r.Type == "many-to-many")) { 
            
            #line default
            #line hidden
            
            #line 58 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 CodeGenModel relationModel = config.Models.Single(m => m.Name == relation.Model); 
            
            #line default
            #line hidden
            this.Write("            Field<ListGraphType<");
            
            #line 59 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relationModel.Name));
            
            #line default
            #line hidden
            this.Write("Type>>(\r\n                \"");
            
            #line 60 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((!string.IsNullOrEmpty(relationModel.NamePlural) ? relationModel.NamePlural : relationModel.Name + "s").ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("\",\r\n                resolve: context => ");
            
            #line 61 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("Repository.Get");
            
            #line 61 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(relationModel.NamePlural) ? relationModel.NamePlural : relationModel.Name + "s"));
            
            #line default
            #line hidden
            this.Write("Of");
            
            #line 61 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write("ById(context.Source.Id)\r\n            );\r\n\r\n");
            
            #line 64 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\WebAPI\GraphQL\Types\TypeTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        }\r\n    }\r\n}\r\n");
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
    public class TypeTemplateBase
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
