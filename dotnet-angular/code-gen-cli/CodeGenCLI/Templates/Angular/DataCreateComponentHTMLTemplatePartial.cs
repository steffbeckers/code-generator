using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataCreateComponentHTMLTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataCreateComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
