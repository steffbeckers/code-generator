//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.list {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Framework.Extensions;
    using CodeGen.Models;
    using System;
    
    
    public partial class ModelListComponentHTMLTemplate : ModelListComponentHTMLTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 8 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
 CodeGenModelProperty defaultKey = _config.Models.DefaultKey(_model); 
            
            #line default
            #line hidden
            
            #line 9 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("<div fxLayout=\"column\" fxLayoutGap=\"16px\">\r\n  <header fxLayout=\"row\" fxLayoutAlig" +
                    "n=\"space-between\" fxLayoutGap=\"32px\">\r\n    <h2 class=\"title\">");
            
            #line default
            #line hidden
            
            #line 11 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 11 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("</h2>\r\n    <div fxLayout=\"row\" fxLayoutGap=\"16px\">\r\n      <button fxFlex=\"100px\" " +
                    "routerLink=\"new\">Create new</button>\r\n    </div>\r\n  </header>\r\n  <main fxLayout=" +
                    "\"column\" fxLayoutGap=\"16px\">\r\n    <ng-container *ngIf=\"(");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("$ | async) === null\">\r\n      <p>Loading...</p>\r\n    </ng-container>\r\n    <ng-cont" +
                    "ainer *ngIf=\"");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("$ | async as ");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("\">\r\n      <table *ngIf=\"");
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(".length > 0\">\r\n        <thead>\r\n");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
 foreach (CodeGenModelProperty property in _model.Properties.Where(x => x.Name != defaultKey.Name).ToList()) { 
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("          <th>");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( property.DisplayName ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("</th>\r\n");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 26 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("        </thead>\r\n        <tbody>\r\n          <tr\r\n            class=\"clickable\"\r\n" +
                    "            *ngFor=\"let ");
            
            #line default
            #line hidden
            
            #line 30 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 30 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(" of ");
            
            #line default
            #line hidden
            
            #line 30 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 30 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("\"\r\n            [routerLink]=\"");
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name.ToCamelCase() ));
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("\"\r\n          >\r\n");
            
            #line default
            #line hidden
            
            #line 33 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
 foreach (CodeGenModelProperty property in _model.Properties.Where(x => x.Name != defaultKey.Name).ToList()) { 
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("            <td>{{ ");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( property.Name.ToCamelCase() ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(" }}</td>\r\n");
            
            #line default
            #line hidden
            
            #line 35 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write("          </tr>\r\n        </tbody>\r\n      </table>\r\n      <p *ngIf=\"");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(".length === 0\">No ");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\Angular\src\app\modules\components\list\ModelListComponentHTMLTemplate.tt"
            this.Write(" found.</p>\r\n    </ng-container>\r\n  </main>\r\n</div>\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ModelListComponentHTMLTemplateBase {
        
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
