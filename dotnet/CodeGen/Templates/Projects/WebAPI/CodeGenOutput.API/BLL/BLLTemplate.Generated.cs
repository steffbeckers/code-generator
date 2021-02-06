//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.BLL {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Models;
    using System;
    
    
    public partial class BLLTemplate : BLLTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("using CodeGenOutput.API.DAL;\r\nusing CodeGenOutput.Models;\r\nusing System;\r\nusing S" +
                    "ystem.Collections.Generic;\r\nusing System.Threading.Tasks;\r\n\r\nnamespace CodeGenOu" +
                    "tput.API.BLL\r\n{\r\n    public interface I");
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 15 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("BLL\r\n    {\r\n        Task<IEnumerable<");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(">> Get");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("sAsync();\r\n        Task<");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("> Get");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 18 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("ByIdAsync(Guid id);\r\n        Task<IEnumerable<");
            
            #line default
            #line hidden
            
            #line 19 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 19 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(">> Search");
            
            #line default
            #line hidden
            
            #line 19 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 19 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Async(string term);\r\n        Task<");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("> Create");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(");\r\n        Task<");
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("> Update");
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 21 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(");\r\n        Task Delete");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(");\r\n    }\r\n\r\n    public partial class BusinessLogicLayer : I");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("BLL\r\n    {\r\n        private readonly IRepository<");
            
            #line default
            #line hidden
            
            #line 27 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 27 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("> _");
            
            #line default
            #line hidden
            
            #line 27 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 27 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Repository;\r\n\r\n        public async Task<IEnumerable<");
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(">> Get");
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 29 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("sAsync()\r\n        {\r\n            return await _");
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 31 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Repository.GetAsync();\r\n        }\r\n\r\n        public async Task<");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("> Get");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("ByIdAsync(Guid id)\r\n        {\r\n            return await _");
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 36 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Repository.GetByIdAsync(id);\r\n        }\r\n\r\n        public async Task<IEnumerable<" +
                    "");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(">> Search");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Async(string term)\r\n        {\r\n            return await _");
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Repository.Search");
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 41 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("(term);\r\n        }\r\n\r\n        public async Task<");
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("> Create");
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 44 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(")\r\n        {\r\n            ");
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" created");
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" = await _");
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Repository.CreateAsync(");
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 46 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(");\r\n            await _unitOfWork.Commit();\r\n            return created");
            
            #line default
            #line hidden
            
            #line 48 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 48 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(";\r\n        }\r\n\r\n        public async Task<");
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("> Update");
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 51 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(")\r\n        {\r\n            ");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" updated");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" = await _");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Repository.UpdateAsync(");
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 53 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(");\r\n            await _unitOfWork.Commit();\r\n            return updated");
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 55 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(";\r\n        }\r\n\r\n        public async Task Delete");
            
            #line default
            #line hidden
            
            #line 58 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 58 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 58 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name ));
            
            #line default
            #line hidden
            
            #line 58 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 58 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 58 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(")\r\n        {\r\n            await _");
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write("Repository.DeleteAsync(");
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _model.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 60 "Templates\Projects\WebAPI\CodeGenOutput.API\BLL\BLLTemplate.tt"
            this.Write(");\r\n            await _unitOfWork.Commit();\r\n        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class BLLTemplateBase {
        
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
