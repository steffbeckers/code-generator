using CodeGen.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Services
{
    public interface IFileService
    {
        Task Create(string path, string text);
        Task<string> Read(string path);
        Task<List<string>> GetSubdirectories(string path);
        Task<List<string>> TraverseDirectory(string path);
        Task DeleteDirectory(string path);
    }

    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly CodeGenConfig _codeGenConfig;

        public FileService(
            ILogger<FileService> logger,
            IOptions<CodeGenConfig> codeGenConfigOptions
        )
        {
            _logger = logger;
            _codeGenConfig = codeGenConfigOptions.Value;
        }

        public Task<string> Read(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllTextAsync(path);
            }

            return Task.FromResult(string.Empty);
        }

        public Task Create(string path, string text)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            _logger.LogInformation($"Create file: " + path);

            return File.WriteAllTextAsync(path, text);
        }

        public Task<List<string>> GetSubdirectories(string path)
        {
            return Task.FromResult(Directory.GetDirectories(path).ToList());
        }

        public Task<List<string>> TraverseDirectory(string path)
        {
            return Task.FromResult(Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList());
        }

        public Task DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            return Task.CompletedTask;
        }
    }
}
