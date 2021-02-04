using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.BLL
{
    public partial class BusinessLogicLayerTemplate : ITextTemplate
    {
        public CodeGenConfig _config;

        public BusinessLogicLayerTemplate(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
