//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.detail {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Models;
    using System;
    
    
    public partial class ModelDetailComponentTSTemplate : ModelDetailComponentTSTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("import { Component, OnDestroy, OnInit } from \'@angular/core\';\r\nimport { Activated" +
                    "Route, Router } from \'@angular/router\';\r\nimport { BehaviorSubject, Subscription " +
                    "} from \'rxjs\';\r\nimport { ");
            
            #line default
            #line hidden
            
            #line 10 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 10 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(" } from \'src/app/shared/models/");
            
            #line default
            #line hidden
            
            #line 10 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 10 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(".model\';\r\nimport { Response } from \'src/app/shared/models/response.model\';\r\nimpor" +
                    "t { ");
            
            #line default
            #line hidden
            
            #line 12 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 12 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("Service } from \'../");
            
            #line default
            #line hidden
            
            #line 12 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 12 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(".service\';\r\n\r\n@Component({\r\n  selector: \'app-");
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("-detail\',\r\n  templateUrl: \'./detail.component.html\',\r\n  styleUrls: [\'./detail.com" +
                    "ponent.scss\'],\r\n})\r\nexport class ");
            
            #line default
            #line hidden
            
            #line 19 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 19 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("DetailComponent implements OnInit, OnDestroy {\r\n  private subs: Subscription[] = " +
                    "[];\r\n\r\n  ");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("$: BehaviorSubject<");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("> = new BehaviorSubject<");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(">(null);\r\n\r\n  constructor(\r\n    private ");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("Service: ");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(@"Service,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.subs.push(
      this.route.paramMap.subscribe((paramMap: any) => {
        const id: string = paramMap.params.id;

        this.subs.push(
          this.");
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("Service\r\n            .get");
            
            #line default
            #line hidden
            
            #line 37 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 37 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("ById(id)\r\n            .subscribe((response: Response) => {\r\n              if (!re" +
                    "sponse.success) {\r\n                // TODO: Check code\r\n                this.rou" +
                    "ter.navigateByUrl(\'/");
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("\');\r\n                return;\r\n              }\r\n\r\n              this.");
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("$.next(response.data);\r\n            })\r\n        );\r\n      })\r\n    );\r\n  }\r\n\r\n  ng" +
                    "OnDestroy(): void {\r\n    for (const sub of this.subs) {\r\n      sub.unsubscribe()" +
                    ";\r\n    }\r\n  }\r\n\r\n  delete(): void {\r\n    if (confirm(\'Are you sure?\')) {\r\n      " +
                    "const ");
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(": ");
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(" = this.");
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("$.value;\r\n      this.subs.push(\r\n        this.");
            
            #line default
            #line hidden
            
            #line 62 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 62 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("Service\r\n          .delete");
            
            #line default
            #line hidden
            
            #line 63 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 63 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("(");
            
            #line default
            #line hidden
            
            #line 63 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 63 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(")\r\n          .subscribe((response: Response) => {\r\n            if (response.succe" +
                    "ss) {\r\n              this.router.navigateByUrl(\'/");
            
            #line default
            #line hidden
            
            #line 66 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 66 "Templates\Projects\Angular\src\app\modules\components\detail\ModelDetailComponentTSTemplate.tt"
            this.Write("\');\r\n            }\r\n          })\r\n      );\r\n    }\r\n  }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ModelDetailComponentTSTemplateBase {
        
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
