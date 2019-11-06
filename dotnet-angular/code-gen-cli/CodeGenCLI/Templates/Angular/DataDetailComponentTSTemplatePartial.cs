using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataDetailComponentTSTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataDetailComponentTSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
