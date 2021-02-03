using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.DAL
{
    public partial class ApplicationDbContextTemplate : ITextTemplate
    {
        private readonly CodeGenConfig _config;

        public ApplicationDbContextTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
