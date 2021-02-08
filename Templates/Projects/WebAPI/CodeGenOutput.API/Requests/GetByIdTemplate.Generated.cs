//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.Requests {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Models;
    using System;
    
    
    public partial class GetByIdTemplate : GetByIdTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("using AutoMapper;\r\nusing CodeGenOutput.API.BLL;\r\nusing CodeGenOutput.API.Models;\r" +
                    "\nusing CodeGenOutput.API.ViewModels;\r\nusing MediatR;\r\nusing System;\r\nusing Syste" +
                    "m.Threading;\r\nusing System.Threading.Tasks;\r\n\r\nnamespace CodeGenOutput.API.Reque" +
                    "sts.");
            
            #line default
            #line hidden
            
            #line 16 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 16 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("\r\n{\r\n    public class Get");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("ById : IRequest<Response<");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("VM>>\r\n    {\r\n        public Guid Id { get; set; }\r\n    }\r\n\r\n    public class Get");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("ByIdHandler : IRequestHandler<Get");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("ById, Response<");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("VM>>\r\n    {\r\n        private readonly I");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("BLL _bll;\r\n        private readonly IMapper _mapper;\r\n\r\n        public Get");
            
            #line default
            #line hidden
            
            #line 28 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 28 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("ByIdHandler(IBusinessLogicLayer bll, IMapper mapper)\r\n        {\r\n            _bll" +
                    " = bll;\r\n            _mapper = mapper;\r\n        }\r\n\r\n        public async Task<R" +
                    "esponse<");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("VM>> Handle(Get");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("ById request, CancellationToken cancellationToken)\r\n        {\r\n            Respon" +
                    "se<");
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("VM> response = new Response<");
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("VM>();\r\n\r\n            ");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(" = await _bll.Get");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("ByIdAsync(request.Id);\r\n            response.Data = _mapper.Map<");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write("VM>(");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\GetByIdTemplate.tt"
            this.Write(");\r\n\r\n            return response;\r\n        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class GetByIdTemplateBase {
        
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
