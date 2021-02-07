//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.Controllers {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Models;
    using System;
    
    
    public partial class ControllerTemplate : ControllerTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("using CodeGenOutput.API.Requests;\r\nusing CodeGenOutput.API.Requests.");
            
            #line default
            #line hidden
            
            #line 8 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 8 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(";\r\nusing CodeGenOutput.ViewModels;\r\nusing MediatR;\r\nusing Microsoft.AspNetCore.Mv" +
                    "c;\r\nusing System;\r\nusing System.Collections.Generic;\r\nusing System.Threading.Tas" +
                    "ks;\r\n\r\nnamespace CodeGenOutput.API.Controllers\r\n{\r\n    [Route(\"api/");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("\")]\r\n    [ApiController]\r\n    public class ");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("Controller : ControllerBase\r\n    {\r\n        private readonly IMediator _mediator;" +
                    "\r\n\r\n        public ");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("Controller(IMediator mediator)\r\n        {\r\n            _mediator = mediator;\r\n   " +
                    "     }\r\n\r\n        // GET: api/");
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("\r\n        [HttpGet]\r\n        public async Task<ActionResult<Response<List<");
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("ListVM>>>> Get");
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("([FromQuery] int skip = 0, [FromQuery] int take = 20)\r\n        {\r\n            ret" +
                    "urn Ok(await _mediator.Send(new Get");
            
            #line default
            #line hidden
            
            #line 33 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural ));
            
            #line default
            #line hidden
            
            #line 33 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("() { Skip = skip, Take = take }));\r\n        }\r\n\r\n        // GET: api/");
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("/{id}\r\n        [HttpGet(\"{id}\")]\r\n        public async Task<ActionResult<Response" +
                    "<");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM>>> Get");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 38 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("ById([FromRoute] Guid id)\r\n        {\r\n            return Ok(await _mediator.Send(" +
                    "new Get");
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 40 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("ById() { Id = id }));\r\n        }\r\n\r\n        // POST: api/");
            
            #line default
            #line hidden
            
            #line 43 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 43 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("\r\n        [HttpPost]\r\n        public async Task<ActionResult<Response<");
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM>>> Create");
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("([FromBody] ");
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("CreateVM ");
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 45 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("CreateVM)\r\n        {\r\n            Response<");
            
            #line default
            #line hidden
            
            #line 47 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 47 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM> response = await _mediator.Send(new Create");
            
            #line default
            #line hidden
            
            #line 47 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 47 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("() { ");
            
            #line default
            #line hidden
            
            #line 47 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 47 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("CreateVM = ");
            
            #line default
            #line hidden
            
            #line 47 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 47 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("CreateVM });\r\n            return CreatedAtAction(\"Get");
            
            #line default
            #line hidden
            
            #line 48 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 48 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("ById\", new { id = response.Data.Id }, response);\r\n        }\r\n\r\n        // PUT: ap" +
                    "i/");
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("/{id}\r\n        [HttpPut(\"{id}\")]\r\n        public async Task<ActionResult<Response" +
                    "<");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM>>> Update");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("([FromRoute] Guid id, [FromBody] ");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM ");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM)\r\n        {\r\n            if (id != ");
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM.Id) { return BadRequest(); }\r\n            return Ok(await _mediator.Send(new U" +
                    "pdate");
            
            #line default
            #line hidden
            
            #line 56 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 56 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("() { ");
            
            #line default
            #line hidden
            
            #line 56 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 56 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM = ");
            
            #line default
            #line hidden
            
            #line 56 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 56 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("VM }));\r\n        }\r\n\r\n        // DELETE: api/");
            
            #line default
            #line hidden
            
            #line 59 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.NamePlural.ToLower() ));
            
            #line default
            #line hidden
            
            #line 59 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("/{id}\r\n        [HttpDelete(\"{id}\")]\r\n        public async Task<ActionResult<Respo" +
                    "nse>> Delete");
            
            #line default
            #line hidden
            
            #line 61 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 61 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("([FromRoute] Guid id)\r\n        {\r\n            return Ok(await _mediator.Send(new " +
                    "Delete");
            
            #line default
            #line hidden
            
            #line 63 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 63 "Templates\Projects\WebAPI\CodeGenOutput.API\Controllers\ControllerTemplate.tt"
            this.Write("() { Id = id }));\r\n        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ControllerTemplateBase {
        
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
