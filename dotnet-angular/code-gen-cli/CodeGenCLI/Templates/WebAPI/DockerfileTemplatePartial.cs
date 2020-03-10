using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI
{
    public partial class DockerfileTemplate
    {
        private CodeGenConfig config;

        public DockerfileTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
