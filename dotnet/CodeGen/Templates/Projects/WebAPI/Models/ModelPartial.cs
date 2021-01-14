using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.Models
{
    public partial class Model
    {
        private readonly CodeGenModel _model;

        public Model(CodeGenModel model)
        {
            _model = model;
        }
    }
}
