//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.DAL {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Models;
    using System;
    
    
    public partial class ApplicationDbContextTemplate : ApplicationDbContextTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(@"using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 foreach (CodeGenModel model in _config.Models.List) { 
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("        public DbSet<");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( model.Name ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("> ");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(" { get; set; }\r\n");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("\r\n        protected override void OnModelCreating(ModelBuilder builder)\r\n        " +
                    "{\r\n            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAsse" +
                    "mbly());\r\n\r\n            base.OnModelCreating(builder);\r\n        }\r\n");
            
            #line default
            #line hidden
            
            #line 32 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 CodeGenModelProperty auditDeletedProperty = _config.Models.DefaultAuditDeletedProperty(); 
            
            #line default
            #line hidden
            
            #line 33 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 CodeGenModelProperty auditDateCreatedProperty = _config.Models.DefaultAuditDateCreatedProperty(); 
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 CodeGenModelProperty auditDateModifiedProperty = _config.Models.DefaultAuditDateModifiedProperty(); 
            
            #line default
            #line hidden
            
            #line 35 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 if (auditDeletedProperty != null || auditDateCreatedProperty != null || auditDateModifiedProperty != null) { 
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("\r\n        public override Task<int> SaveChangesAsync(CancellationToken cancellati" +
                    "onToken = new CancellationToken())\r\n        {\r\n            foreach (var entry in" +
                    " ChangeTracker.Entries<Auditable>())\r\n            {\r\n");
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 if (auditDeletedProperty != null) { 
            
            #line default
            #line hidden
            
            #line 42 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("                // Soft delete\r\n                if (entry.State == EntityState.De" +
                    "leted)\r\n                {\r\n                    entry.State = EntityState.Modifie" +
                    "d;\r\n                    entry.CurrentValues[\"");
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( auditDeletedProperty.Name ));
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("\"] = ");
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( auditDeletedProperty.Type == "bool" ? "true" : auditDeletedProperty.Type == "DateTime?" ? "DateTime.Now" : "" ));
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(";\r\n                }\r\n");
            
            #line default
            #line hidden
            
            #line 48 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 49 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 if (auditDateCreatedProperty != null || auditDateModifiedProperty != null) { 
            
            #line default
            #line hidden
            
            #line 50 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("\r\n                switch (entry.State)\r\n                {\r\n");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 if (auditDateCreatedProperty != null) { 
            
            #line default
            #line hidden
            
            #line 54 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("                    case EntityState.Added:\r\n                        entry.Entity" +
                    ".");
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( auditDateCreatedProperty.Name ));
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(" = DateTime.Now;\r\n                        break;\r\n");
            
            #line default
            #line hidden
            
            #line 57 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 58 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 if (auditDateModifiedProperty != null) { 
            
            #line default
            #line hidden
            
            #line 59 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("                    case EntityState.Modified:\r\n                        entry.Ent" +
                    "ity.");
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( auditDateModifiedProperty.Name ));
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write(" = DateTime.Now;\r\n                        break;\r\n");
            
            #line default
            #line hidden
            
            #line 62 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 63 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("                }\r\n");
            
            #line default
            #line hidden
            
            #line 64 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 65 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("            }\r\n\r\n            return base.SaveChangesAsync(cancellationToken);\r\n  " +
                    "      }\r\n");
            
            #line default
            #line hidden
            
            #line 69 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 70 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\ApplicationDbContextTemplate.tt"
            this.Write("    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ApplicationDbContextTemplateBase {
        
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
