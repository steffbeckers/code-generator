using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataRoutingModuleTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataRoutingModuleTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
