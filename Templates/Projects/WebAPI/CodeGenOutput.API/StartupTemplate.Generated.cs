//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using CodeGen.Models;
    using System;
    
    
    public partial class StartupTemplate : StartupTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\WebAPI\CodeGenOutput.API\StartupTemplate.tt"
            this.Write("using CodeGenOutput.API.BLL;\r\nusing CodeGenOutput.API.DAL;\r\nusing CodeGenOutput.A" +
                    "PI.Filters;\r\nusing CodeGenOutput.API.Validation;\r\nusing FluentValidation;\r\nusing" +
                    " MediatR;\r\nusing Microsoft.AspNetCore.Builder;\r\nusing Microsoft.AspNetCore.Hosti" +
                    "ng;\r\nusing Microsoft.EntityFrameworkCore;\r\nusing Microsoft.Extensions.Configurat" +
                    "ion;\r\nusing Microsoft.Extensions.DependencyInjection;\r\nusing Microsoft.Extension" +
                    "s.Hosting;\r\nusing Microsoft.OpenApi.Models;\r\n\r\nnamespace CodeGenOutput.API\r\n{\r\n " +
                    "   public class Startup\r\n    {\r\n        private readonly IConfiguration _configu" +
                    "ration;\r\n\r\n        public Startup(IConfiguration configuration)\r\n        {\r\n    " +
                    "        _configuration = configuration;\r\n        }\r\n\r\n        public void Config" +
                    "ureServices(IServiceCollection services)\r\n        {\r\n            services.AddDbC" +
                    "ontext<ApplicationDbContext>(options =>\r\n                    options.UseSqlServe" +
                    "r(_configuration.GetConnectionString(\"ApplicationDbContext\")));\r\n\r\n            s" +
                    "ervices.AddScoped<IUnitOfWork, UnitOfWork>();\r\n\r\n            services.AddScoped<" +
                    "IBusinessLogicLayer, BusinessLogicLayer>();\r\n\r\n            services.AddAutoMappe" +
                    "r(typeof(Startup));\r\n\r\n            services.AddValidatorsFromAssembly(typeof(Sta" +
                    "rtup).Assembly);\r\n\r\n            services.AddMediatR(typeof(Startup));\r\n\r\n       " +
                    "     services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavi" +
                    "or<,>));\r\n\r\n            services.AddControllers(options => options.Filters.Add(n" +
                    "ew ApiExceptionFilter()))\r\n                .AddNewtonsoftJson(options =>\r\n      " +
                    "              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json" +
                    ".ReferenceLoopHandling.Ignore\r\n                );\r\n\r\n            services.AddSwa" +
                    "ggerGen(options =>\r\n            {\r\n                options.SwaggerDoc(\"v1\", new " +
                    "OpenApiInfo { Title = \"CodeGenOutput.API\", Version = \"v1\" });\r\n            });\r\n" +
                    "        }\r\n\r\n        // This method gets called by the runtime. Use this method " +
                    "to configure the HTTP request pipeline.\r\n        public void Configure(IApplicat" +
                    "ionBuilder app, IWebHostEnvironment env)\r\n        {\r\n            if (env.IsDevel" +
                    "opment())\r\n            {\r\n                app.UseDeveloperExceptionPage();\r\n\r\n  " +
                    "              app.UseSwagger();\r\n                app.UseSwaggerUI(options =>\r\n  " +
                    "              {\r\n                    options.SwaggerEndpoint(\"swagger/v1/swagger" +
                    ".json\", \"CodeGenOutput.API v1\");\r\n                    options.RoutePrefix = stri" +
                    "ng.Empty;\r\n                });\r\n            }\r\n\r\n            app.UseHttpsRedirec" +
                    "tion();\r\n\r\n            app.UseRouting();\r\n\r\n            app.UseAuthorization();\r" +
                    "\n\r\n            app.UseEndpoints(endpoints =>\r\n            {\r\n                end" +
                    "points.MapControllers();\r\n            });\r\n        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class StartupTemplateBase {
        
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
