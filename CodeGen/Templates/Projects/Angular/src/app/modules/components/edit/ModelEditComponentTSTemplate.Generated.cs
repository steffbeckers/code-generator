//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.edit {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Framework.Extensions;
    using CodeGen.Models;
    using System;
    
    
    public partial class ModelEditComponentTSTemplate : ModelEditComponentTSTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 8 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
 CodeGenModelProperty defaultKey = _config.Models.DefaultKey(_model); 
            
            #line default
            #line hidden
            
            #line 9 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("import { Component, OnDestroy, OnInit } from \'@angular/core\';\r\nimport { FormBuild" +
                    "er, Validators } from \'@angular/forms\';\r\nimport { Router, ActivatedRoute } from " +
                    "\'@angular/router\';\r\nimport { BehaviorSubject, Subscription } from \'rxjs\';\r\nimpor" +
                    "t { ");
            
            #line default
            #line hidden
            
            #line 13 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 13 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(" } from \'src/app/shared/models/");
            
            #line default
            #line hidden
            
            #line 13 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 13 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(".model\';\r\nimport { Response } from \'src/app/shared/models/response.model\';\r\nimpor" +
                    "t { ");
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("Service } from \'../");
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(".service\';\r\n\r\n@Component({\r\n  selector: \'app-");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("-edit\',\r\n  templateUrl: \'./edit.component.html\',\r\n  styleUrls: [\'./edit.component" +
                    ".scss\'],\r\n})\r\nexport class ");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("EditComponent implements OnInit, OnDestroy {\r\n  private subs: Subscription[] = []" +
                    ";\r\n\r\n  ");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("$: BehaviorSubject<");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("> = new BehaviorSubject<");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(">(null);\r\n  saving: boolean;\r\n  close: boolean;\r\n  form = this.fb.group({\r\n");
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
 foreach (CodeGenModelProperty property in _model.Properties.Where(x => x.Name != defaultKey.Name).ToList()) { 
            
            #line default
            #line hidden
            
            #line 30 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
 if (property.Required) { 
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("    ");
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( property.Name.ToCamelCase() ));
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(": [null, [Validators.required]],\r\n");
            
            #line default
            #line hidden
            
            #line 32 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            
            #line 33 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("    ");
            
            #line default
            #line hidden
            
            #line 33 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( property.Name.ToCamelCase() ));
            
            #line default
            #line hidden
            
            #line 33 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(": [null],\r\n");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 35 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("  });\r\n\r\n  constructor(\r\n    private ");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("Service: ");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(@"Service,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.subs.push(
      this.route.paramMap.subscribe((paramMap: any) => {
        const id: string = paramMap.params.id;

        this.subs.push(
          this.");
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("Service\r\n            .get");
            
            #line default
            #line hidden
            
            #line 52 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 52 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("ById(id)\r\n            .subscribe((response: Response) => {\r\n              if (!re" +
                    "sponse.success) {\r\n                // TODO: Check code\r\n                this.rou" +
                    "ter.navigateByUrl(\'/");
            
            #line default
            #line hidden
            
            #line 56 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 56 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("\');\r\n                return;\r\n              }\r\n\r\n              this.");
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("$.next(response.data);\r\n            })\r\n        );\r\n      })\r\n    );\r\n\r\n    this." +
                    "subs.push(\r\n      this.");
            
            #line default
            #line hidden
            
            #line 67 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 67 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("$.subscribe((");
            
            #line default
            #line hidden
            
            #line 67 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 67 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(": ");
            
            #line default
            #line hidden
            
            #line 67 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 67 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(") => {\r\n        this.form.patchValue(");
            
            #line default
            #line hidden
            
            #line 68 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 68 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(@");
        this.form.markAsPristine();
      })
    );
  }

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }

  save(): void {
    if (this.saving || this.form.invalid) {
      return;
    }

    if (this.form.pristine) {
      if (this.close) {
        this.router.navigateByUrl(`/");
            
            #line default
            #line hidden
            
            #line 87 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 87 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("/${this.");
            
            #line default
            #line hidden
            
            #line 87 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 87 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("$.value.id}`);\r\n        return;\r\n      }\r\n      return;\r\n    }\r\n\r\n    const ");
            
            #line default
            #line hidden
            
            #line 93 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 93 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(": ");
            
            #line default
            #line hidden
            
            #line 93 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 93 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(" = {\r\n      ...this.");
            
            #line default
            #line hidden
            
            #line 94 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 94 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("$.value,\r\n      ...this.form.getRawValue(),\r\n    };\r\n\r\n    this.saving = true;\r\n " +
                    "   this.subs.push(\r\n      this.");
            
            #line default
            #line hidden
            
            #line 100 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 100 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("Service.update");
            
            #line default
            #line hidden
            
            #line 100 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 100 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("(");
            
            #line default
            #line hidden
            
            #line 100 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 100 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(").subscribe(\r\n        (response: Response) => {\r\n          this.saving = false;\r\n" +
                    "\r\n          if (!response.success) {\r\n            return;\r\n          }\r\n\r\n      " +
                    "    if (this.close) {\r\n            this.router.navigateByUrl(`/");
            
            #line default
            #line hidden
            
            #line 109 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 109 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("/${");
            
            #line default
            #line hidden
            
            #line 109 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 109 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 109 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name.ToCamelCase() ));
            
            #line default
            #line hidden
            
            #line 109 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("}`);\r\n            return;\r\n          }\r\n\r\n          this.");
            
            #line default
            #line hidden
            
            #line 113 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 113 "Templates\Projects\Angular\src\app\modules\components\edit\ModelEditComponentTSTemplate.tt"
            this.Write("$.next(response.data);\r\n        },\r\n        (error: any) => {\r\n          this.sav" +
                    "ing = false;\r\n        }\r\n      )\r\n    );\r\n  }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ModelEditComponentTSTemplateBase {
        
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