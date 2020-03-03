using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.Models
{
    public partial class UserModelTemplate
    {
        private CodeGenConfig config;

        public UserModelTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
