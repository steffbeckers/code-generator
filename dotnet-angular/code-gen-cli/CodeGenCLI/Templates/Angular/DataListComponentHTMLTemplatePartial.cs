using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataListComponentHTMLTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataListComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
