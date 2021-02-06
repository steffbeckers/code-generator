using CodeGen.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.Services
{
    public interface IConfigService
    {
        Task Load();
        CodeGenConfig CodeGenConfig { get; }
    }

    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _configuration;
        
        private CodeGenConfig _codeGenConfig;
        public CodeGenConfig CodeGenConfig => _codeGenConfig;

        public ConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task Load()
        {
            _codeGenConfig = _configuration.GetSection("CodeGenConfig").Get<CodeGenConfig>();
            _codeGenConfig.Models = _codeGenConfig.Models.OrderBy(x => x.Name).ToList();
            return Task.CompletedTask;
        }
    }
}
