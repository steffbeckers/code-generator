using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API
{
    public partial class StartupTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public StartupTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
