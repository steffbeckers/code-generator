using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataLinkComponentHTMLTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataLinkComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
