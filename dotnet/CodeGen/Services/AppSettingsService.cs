using CodeGen.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Services
{
    public interface IAppSettingsService
    {
        Task Load();
        CodeGenConfig CodeGenConfig { get; }
    }

    public class AppSettingsService : IAppSettingsService
    {
        private readonly IConfiguration _configuration;
        
        private CodeGenConfig _codeGenConfig;
        public CodeGenConfig CodeGenConfig => _codeGenConfig;

        public AppSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task Load()
        {
            _codeGenConfig = _configuration.GetSection("CodeGenConfig").Get<CodeGenConfig>();
            return Task.CompletedTask;
        }
    }
}
