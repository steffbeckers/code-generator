//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.DAL.Configurations {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Models;
    using System;
    
    
    public partial class ConfigurationTemplate : ConfigurationTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 CodeGenModelProperty defaultKey = _config.Models.DefaultKey(_model); 
            
            #line default
            #line hidden
            
            #line 8 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("using CodeGenOutput.API.Models;\r\nusing Microsoft.EntityFrameworkCore;\r\nusing Micr" +
                    "osoft.EntityFrameworkCore.Metadata.Builders;\r\n\r\nnamespace CodeGenOutput.API.DAL." +
                    "Configurations\r\n{\r\n    public class ");
            
            #line default
            #line hidden
            
            #line 14 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 14 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("Configuration : IEntityTypeConfiguration<");
            
            #line default
            #line hidden
            
            #line 14 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 14 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(">\r\n    {\r\n        public void Configure(EntityTypeBuilder<");
            
            #line default
            #line hidden
            
            #line 16 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 16 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("> builder)\r\n        {\r\n");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 CodeGenModelProperty auditDeletedProperty = _config.Models.DefaultAuditDeletedProperty(); 
            
            #line default
            #line hidden
            
            #line 19 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 if (auditDeletedProperty != null) { 
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("            builder.HasQueryFilter(x => !x.");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( auditDeletedProperty.Name ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(");\r\n\r\n");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("            builder.HasKey(x => x.");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(");\r\n");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 foreach (CodeGenModelProperty property in _model.Properties) { 
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("\r\n            builder.Property(x => x.");
            
            #line default
            #line hidden
            
            #line 26 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( property.Name ));
            
            #line default
            #line hidden
            
            #line 26 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(")\r\n");
            
            #line default
            #line hidden
            
            #line 27 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 if (property.Required) { 
            
            #line default
            #line hidden
            
            #line 28 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("                .IsRequired()\r\n");
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 30 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 if (property.Type.ToLower() == "string") { 
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 if (property.MaxLength != null) { 
            
            #line default
            #line hidden
            
            #line 32 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("                .HasMaxLength(");
            
            #line default
            #line hidden
            
            #line 32 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( property.MaxLength ));
            
            #line default
            #line hidden
            
            #line 32 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(")\r\n");
            
            #line default
            #line hidden
            
            #line 33 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("                .HasMaxLength(");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _config.Models.StringPropertyMaxLength ?? 100 ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(")\r\n");
            
            #line default
            #line hidden
            
            #line 35 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 37 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 if (property.DefaultValue != null) { 
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("                .HasDefaultValue(");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( property.DefaultValue ));
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(")\r\n");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 if (!string.IsNullOrEmpty(property.ColumnType)) { 
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("                .HasColumnType(\"");
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( property.ColumnType ));
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("\")\r\n");
            
            #line default
            #line hidden
            
            #line 42 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 43 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("            ;\r\n");
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\Configurations\ConfigurationTemplate.tt"
            this.Write("        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ConfigurationTemplateBase {
        
        private global::System.Text.StringBuilder builder;
        
        private global::System.Collections.Generic.IDictionary<string, object> session;
        
        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;
        
        private string currentIndent = string.Empty;
        
        private global::System.Collections.Generic.Stack<int> indents;
        
        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();
        
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session {
            get {
                return this.session;
            }
            set {
                this.session = value;
            }
        }
        
        public global::System.Text.StringBuilder GenerationEnvironment {
            get {
                if ((this.builder == null)) {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set {
                this.builder = value;
            }
        }
        
        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors {
            get {
                if ((this.errors == null)) {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }
        
        public string CurrentIndent {
            get {
                return this.currentIndent;
            }
        }
        
        private global::System.Collections.Generic.Stack<int> Indents {
            get {
                if ((this.indents == null)) {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }
        
        public ToStringInstanceHelper ToStringHelper {
            get {
                return this._toStringHelper;
            }
        }
        
        public void Error(string message) {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }
        
        public void Warning(string message) {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }
        
        public string PopIndent() {
            if ((this.Indents.Count == 0)) {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }
        
        public void PushIndent(string indent) {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }
        
        public void ClearIndent() {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }
        
        public void Write(string textToAppend) {
            this.GenerationEnvironment.Append(textToAppend);
        }
        
        public void Write(string format, params object[] args) {
            this.GenerationEnvironment.AppendFormat(format, args);
        }
        
        public void WriteLine(string textToAppend) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }
        
        public void WriteLine(string format, params object[] args) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }
        
        public class ToStringInstanceHelper {
            
            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;
            
            public global::System.IFormatProvider FormatProvider {
                get {
                    return this.formatProvider;
                }
                set {
                    if ((value != null)) {
                        this.formatProvider = value;
                    }
                }
            }
            
            public string ToStringWithCulture(object objectToConvert) {
                if ((objectToConvert == null)) {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type)) {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                            iConvertibleType});
                if ((methInfo != null)) {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                                this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}
