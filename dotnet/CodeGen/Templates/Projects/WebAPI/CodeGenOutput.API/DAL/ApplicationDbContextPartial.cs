using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.DAL
{
    public partial class ApplicationDbContext
    {
        private readonly CodeGenConfig _config;

        public ApplicationDbContext(CodeGenConfig config)
        {
            _config = config;
        }
    }
}
