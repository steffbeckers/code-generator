using CodeGen.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.Services
{
    public interface IConfigService
    {
        CodeGenConfig CodeGenConfig { get; }
        Task LoadFromConfigFile();
        Task LoadFromRequest(CodeGenConfig codeGenConfig);
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

        public Task LoadFromConfigFile()
        {
            _codeGenConfig = _configuration.GetSection("CodeGenConfig").Get<CodeGenConfig>();
            _codeGenConfig.Models.List = _codeGenConfig.Models.List.OrderBy(x => x.Name).ToList();
            return Task.CompletedTask;
        }

        public Task LoadFromRequest(CodeGenConfig codeGenConfig)
        {
            _codeGenConfig = codeGenConfig;
            _codeGenConfig.Models.List = _codeGenConfig.Models.List.OrderBy(x => x.Name).ToList();
            return Task.CompletedTask;
        }
    }
}
