using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.Controllers
{
    public partial class Controller
    {
        private readonly CodeGenConfig _config;
        private readonly CodeGenModel _model;

        public Controller(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
