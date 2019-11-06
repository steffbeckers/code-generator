using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataCreateComponentTSTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataCreateComponentTSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
