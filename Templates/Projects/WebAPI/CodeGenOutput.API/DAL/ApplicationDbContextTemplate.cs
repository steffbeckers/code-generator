using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.DAL
{
    public partial class ApplicationDbContextTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public ApplicationDbContextTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
