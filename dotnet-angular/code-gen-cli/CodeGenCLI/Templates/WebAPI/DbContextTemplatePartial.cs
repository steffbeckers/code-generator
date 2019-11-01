using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI
{
    public partial class DbContextTemplate
    {
        private CodeGenConfig config;

        public DbContextTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
