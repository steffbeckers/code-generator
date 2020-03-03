using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataUpdateComponentHTMLTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataUpdateComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
