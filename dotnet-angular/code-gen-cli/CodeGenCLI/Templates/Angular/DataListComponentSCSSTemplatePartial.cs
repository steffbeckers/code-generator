using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataListComponentSCSSTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataListComponentSCSSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
