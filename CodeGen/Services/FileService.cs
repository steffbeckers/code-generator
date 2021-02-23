using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.Services
{
    public interface IFileService
    {
        Task Create(string path, string text);
        Task<string> Read(string path);
        Task<List<string>> GetSubdirectories(string path);
        Task<List<string>> TraverseDirectory(string path);
        bool DirectoryExists(string projectsOutputFolderPath);
        Task DeleteDirectory(string path);
    }

    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
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
            if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
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

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public Task DeleteDirectory(string path)
        {
            if (DirectoryExists(path))
            {
                Directory.Delete(path, true);
            }

            return Task.CompletedTask;
        }
    }
}
