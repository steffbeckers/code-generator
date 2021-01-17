using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.Services
{
    public partial class EmailService
    {
        private readonly CodeGenConfig _config;

        public EmailService(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
