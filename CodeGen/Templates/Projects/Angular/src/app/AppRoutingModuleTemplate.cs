using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app
{
    public partial class AppRoutingModuleTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public AppRoutingModuleTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
