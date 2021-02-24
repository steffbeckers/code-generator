using CodeGen.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.Services
{
    public interface IConfigService
    {
        IConfiguration AppSettings { get; }
        CodeGenConfig CodeGenConfig { get; }
        Task LoadFromConfigFile();
        Task UpdateConfig(CodeGenConfig codeGenConfig);
    }

    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;
        private CodeGenConfig _codeGenConfig;

        public IConfiguration AppSettings => _configuration;
        public CodeGenConfig CodeGenConfig => _codeGenConfig;

        public ConfigService(
            IConfiguration configuration,
            IFileService fileService
        )
        {
            _configuration = configuration;
            _fileService = fileService;
        }

        public Task LoadFromConfigFile()
        {
            _codeGenConfig = _configuration.GetSection("CodeGenConfig").Get<CodeGenConfig>();
            _codeGenConfig.Models.List = _codeGenConfig.Models.List.OrderBy(x => x.Name).ToList();
            return Task.CompletedTask;
        }

        public async Task UpdateConfig(CodeGenConfig codeGenConfig)
        {
            codeGenConfig.Models.List = codeGenConfig.Models.List.OrderBy(x => x.Name).ToList();

            await _fileService.Create(
                "code-gen-config.json",
                JsonConvert.SerializeObject(
                    new
                    {
                        CodeGenConfig = codeGenConfig
                    },
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    }
                )
            );
        }
    }
}
