using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.layouts.@default
{
    public partial class DefaultComponentHTMLTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public DefaultComponentHTMLTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
