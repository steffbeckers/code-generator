using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataUpdateComponentTSTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataUpdateComponentTSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
