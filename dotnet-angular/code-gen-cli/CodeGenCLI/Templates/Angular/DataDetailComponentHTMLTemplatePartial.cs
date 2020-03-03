using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataDetailComponentHTMLTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataDetailComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
