//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.Angular.src.app.modules {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Models;
    using System;
    
    
    public partial class ModelModuleTemplate : ModelModuleTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("import { NgModule } from \'@angular/core\';\r\nimport { SharedModule } from \'../../sh" +
                    "ared/shared.module\';\r\n\r\nimport { ");
            
            #line default
            #line hidden
            
            #line 10 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 10 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("RoutingModule } from \'./");
            
            #line default
            #line hidden
            
            #line 10 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 10 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("-routing.module\';\r\nimport { ");
            
            #line default
            #line hidden
            
            #line 11 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 11 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("Component } from \'./");
            
            #line default
            #line hidden
            
            #line 11 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 11 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(".component\';\r\nimport { ");
            
            #line default
            #line hidden
            
            #line 12 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 12 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("Service } from \'./");
            
            #line default
            #line hidden
            
            #line 12 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 12 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(".service\';\r\nimport { ");
            
            #line default
            #line hidden
            
            #line 13 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 13 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("ListComponent } from \'./list/list.component\';\r\nimport { ");
            
            #line default
            #line hidden
            
            #line 14 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 14 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("DetailComponent } from \'./detail/detail.component\';\r\nimport { ");
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("CreateComponent } from \'./create/create.component\';\r\nimport { ");
            
            #line default
            #line hidden
            
            #line 16 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 16 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("EditComponent } from \'./edit/edit.component\';\r\n\r\n@NgModule({\r\n  declarations: [\r\n" +
                    "    ");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("Component,\r\n    ");
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("ListComponent,\r\n    ");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("DetailComponent,\r\n    ");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("CreateComponent,\r\n    ");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("EditComponent,\r\n  ],\r\n  imports: [SharedModule, ");
            
            #line default
            #line hidden
            
            #line 26 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 26 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("RoutingModule],\r\n  providers: [");
            
            #line default
            #line hidden
            
            #line 27 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 27 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("Service],\r\n})\r\nexport class ");
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\Angular\src\app\modules\ModelModuleTemplate.tt"
            this.Write("Module {}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ModelModuleTemplateBase {
        
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
