using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.DAL
{
    public partial class GenericRepositoryTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public GenericRepositoryTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
