//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.3
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
    
    
    public partial class UpdateTemplate : UpdateTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
 CodeGenModelProperty defaultKey = _config.Models.DefaultKey(_model); 
            
            #line default
            #line hidden
            
            #line 8 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(@"using AutoMapper;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.Validation;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("\r\n{\r\n    public class Update");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(" : IRequest<Response>\r\n    {\r\n        public ");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("UpdateVM ");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("UpdateVM { get; set; }\r\n    }\r\n\r\n    public class Update");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("Handler : IRequestHandler<Update");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(", Response>\r\n    {\r\n        private readonly IUnitOfWork _unitOfWork;\r\n        pr" +
                    "ivate readonly IMapper _mapper;\r\n\r\n        public Update");
            
            #line default
            #line hidden
            
            #line 30 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 30 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("Handler(IUnitOfWork unitOfWork, IMapper mapper)\r\n        {\r\n            _unitOfWo" +
                    "rk = unitOfWork;\r\n            _mapper = mapper;\r\n        }\r\n\r\n        public asy" +
                    "nc Task<Response> Handle(Update");
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(" request, CancellationToken cancellationToken)\r\n        {\r\n            IRepositor" +
                    "y<");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("> repository = _unitOfWork.GetRepository<");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(">();\r\n\r\n            ");
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(" = await repository.GetByIdAsync(request.");
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("UpdateVM.Id);\r\n            _mapper.Map(request.");
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("UpdateVM, ");
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(");\r\n\r\n            ValidationResult validationResult = await Validators.");
            
            #line default
            #line hidden
            
            #line 43 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 43 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("Validator.ValidateAsync(");
            
            #line default
            #line hidden
            
            #line 43 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 43 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(", cancellationToken);\r\n            if (!validationResult.IsValid)\r\n            {\r" +
                    "\n                return new Response()\r\n                {\r\n                    S" +
                    "uccess = false,\r\n                    Code = \"");
            
            #line default
            #line hidden
            
            #line 49 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToUpper() ));
            
            #line default
            #line hidden
            
            #line 49 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("_INVALID\",\r\n                    Message = \"");
            
            #line default
            #line hidden
            
            #line 50 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 50 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(" data invalid\",\r\n                    Data = validationResult.Errors\r\n            " +
                    "    };\r\n            }\r\n\r\n            ");
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(" = await repository.UpdateAsync(");
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(");\r\n            await _unitOfWork.Commit();\r\n\r\n            return new Response()\r" +
                    "\n            {\r\n                Code = \"");
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToUpper() ));
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("_UPDATED\",\r\n                Message = \"");
            
            #line default
            #line hidden
            
            #line 61 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 61 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(" updated\",\r\n                Data = _mapper.Map<");
            
            #line default
            #line hidden
            
            #line 62 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 62 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write("VM>(");
            
            #line default
            #line hidden
            
            #line 62 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 62 "Templates\Projects\WebAPI\CodeGenOutput.API\Requests\UpdateTemplate.tt"
            this.Write(")\r\n            };\r\n        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class UpdateTemplateBase {
        
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
