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
    
    #line 1 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class DbContextTemplate : DbContextTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using Microsoft.EntityFrameworkCore;\r\nusing Microsoft.Extensions.Configuration;\r\n" +
                    "using System;\r\nusing System.IO;\r\nusing System.Threading;\r\nusing System.Threading" +
                    ".Tasks;\r\nusing ");
            
            #line 13 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".Models;\r\n\r\nnamespace ");
            
            #line 15 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(config.WebAPI.NamespaceRoot) ? config.WebAPI.NamespaceRoot : config.Name));
            
            #line default
            #line hidden
            this.Write(".DAL\r\n{\r\n    public class ");
            
            #line 17 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(config.Name));
            
            #line default
            #line hidden
            this.Write("Context : DbContext\r\n\t{\r\n\t\tpublic ");
            
            #line 19 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(config.Name));
            
            #line default
            #line hidden
            this.Write("Context(DbContextOptions<");
            
            #line 19 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(config.Name));
            
            #line default
            #line hidden
            this.Write("Context> options) : base(options)\r\n        {\r\n        }\r\n\r\n");
            
            #line 23 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 foreach (CodeGenModel model in config.Models) { 
            
            #line default
            #line hidden
            this.Write("\t\tpublic DbSet<");
            
            #line 24 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 24 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(model.NamePlural) ? model.NamePlural : model.Name + "s"));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 25 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(""appsettings.json"", optional: true, reloadOnChange: true)
                    .AddJsonFile(""appsettings.Development.json"", optional: true, reloadOnChange: true)
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString(""");
            
            #line 36 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(config.Name));
            
            #line default
            #line hidden
            this.Write("Context\"));\r\n            }\r\n        }\r\n\r\n\t\tprotected override void OnModelCreatin" +
                    "g(ModelBuilder modelBuilder)\r\n        {\r\n");
            
            #line 42 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 CodeGenModel lastModel = config.Models.Last(); 
            
            #line default
            #line hidden
            
            #line 43 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 foreach (CodeGenModel model in config.Models) { 
            
            #line default
            #line hidden
            this.Write("\t\t\t#region ");
            
            #line 44 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(model.NamePlural) ? model.NamePlural : model.Name + "s"));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n            // Soft delete query filter\r\n            modelBuilder.Entity<");
            
            #line 47 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(">().HasQueryFilter(e => e.DeletedOn == null);\r\n\r\n            // Table\r\n          " +
                    "  modelBuilder.Entity<");
            
            #line 50 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(">().ToTable(\"");
            
            #line 50 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(!string.IsNullOrEmpty(model.NamePlural) ? model.NamePlural : model.Name + "s"));
            
            #line default
            #line hidden
            this.Write("\");\r\n\r\n            // Required properties\r\n");
            
            #line 53 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 foreach (CodeGenModelProperty property in model.Properties.Where(p => p.Required)) { 
            
            #line default
            #line hidden
            this.Write("            modelBuilder.Entity<");
            
            #line 54 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(">().Property(e => e.");
            
            #line 54 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(").IsRequired();\r\n");
            
            #line 55 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n            #endregion\r\n");
            
            #line 58 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 if (!model.Equals(lastModel)) { 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 60 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 61 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"		}

		public override int SaveChanges()
        {
            SoftDeleteLogic();
            TimeStampsLogic();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SoftDeleteLogic();
            TimeStampsLogic();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SoftDeleteLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (
");
            
            #line 86 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 foreach (CodeGenModel model in config.Models) { 
            
            #line default
            #line hidden
            
            #line 87 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 if (!model.Equals(lastModel)) { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\tentry.Entity.GetType() == typeof(");
            
            #line 88 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(") ||\r\n");
            
            #line 89 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\tentry.Entity.GetType() == typeof(");
            
            #line 90 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 91 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 92 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"				)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues[""DeletedOn""] = null;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues[""DeletedOn""] = DateTime.Now;
                            break;
                    }
                }
            }
        }

        private void TimeStampsLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (
");
            
            #line 115 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 foreach (CodeGenModel model in config.Models) { 
            
            #line default
            #line hidden
            
            #line 116 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 if (!model.Equals(lastModel)) { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\tentry.Entity.GetType() == typeof(");
            
            #line 117 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(") ||\r\n");
            
            #line 118 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\tentry.Entity.GetType() == typeof(");
            
            #line 119 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(model.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 120 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 121 "C:\dev\steffbeckers\code-generator\dotnet-angular\code-gen-cli\CodeGenCLI\Templates\DbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"				)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues[""CreatedOn""] = DateTime.Now;
                            entry.CurrentValues[""ModifiedOn""] = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues[""ModifiedOn""] = DateTime.Now;
                            break;
                    }
                }
            }
        }
	}
}
");
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
    public class DbContextTemplateBase
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
