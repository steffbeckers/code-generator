using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.Validation
{
    public partial class ValidatorsTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public ValidatorsTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
