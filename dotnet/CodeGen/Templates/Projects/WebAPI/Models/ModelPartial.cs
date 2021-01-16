using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.Models
{
    public partial class Model
    {
        private readonly CodeGenConfig _config;
        private readonly CodeGenModel _model;

        public Model(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
