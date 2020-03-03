using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataLinkComponentTSTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataLinkComponentTSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
