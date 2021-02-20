using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.Mappers
{
    public partial class AutoMappingTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public AutoMappingTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
