using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.Models
{
    public partial class AuditableTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public AuditableTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
