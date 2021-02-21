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
    
    
    public partial class GenericRepositoryTemplate : GenericRepositoryTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
 CodeGenModelProperty defaultKey = _config.Models.DefaultKey(); 
            
            #line default
            #line hidden
            
            #line 8 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(@"using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> GetDbSet();
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string include = """",
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        );
        Task<TEntity> GetBy");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Type ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(");\r\n        Task<TEntity> CreateAsync(TEntity entity);\r\n        Task<TEntity> Upd" +
                    "ateAsync(TEntity entity);\r\n        Task DeleteAsync(");
            
            #line default
            #line hidden
            
            #line 28 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Type ));
            
            #line default
            #line hidden
            
            #line 28 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 28 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 28 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(@");
        Task DeleteAsync(TEntity entity);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<TEntity> GetDbSet()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string include = """",
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        )
        {
            IQueryable<TEntity> query = GetDbSet();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string property in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetBy");
            
            #line default
            #line hidden
            
            #line 72 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name ));
            
            #line default
            #line hidden
            
            #line 72 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 72 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Type ));
            
            #line default
            #line hidden
            
            #line 72 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 72 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 72 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(")\r\n        {\r\n            return await GetDbSet().FindAsync(");
            
            #line default
            #line hidden
            
            #line 74 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 74 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(@");
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(");
            
            #line default
            #line hidden
            
            #line 89 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Type ));
            
            #line default
            #line hidden
            
            #line 89 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 89 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 89 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(")\r\n        {\r\n            TEntity entity = await GetBy");
            
            #line default
            #line hidden
            
            #line 91 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name ));
            
            #line default
            #line hidden
            
            #line 91 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write("Async(");
            
            #line default
            #line hidden
            
            #line 91 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( defaultKey.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 91 "Templates\Projects\WebAPI\CodeGenOutput.API\DAL\GenericRepositoryTemplate.tt"
            this.Write(@");
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class GenericRepositoryTemplateBase {
        
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