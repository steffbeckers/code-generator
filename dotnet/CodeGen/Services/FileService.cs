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

        public Task Create(string path, string text)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), path);

            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            return File.WriteAllTextAsync(path, text);
        }

        public Task DeleteDirectory(string path)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), path);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            return Task.CompletedTask;
        }
    }
}
