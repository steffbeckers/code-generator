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
        Task Copy(string fromPath, string toPath);
        Task<bool> Exists(string path);
        Task<List<string>> GetSubdirectories(string path);
        Task<List<string>> TraverseDirectory(string path);
        Task CreateDirectory(string path);
        bool DirectoryExists(string path);
        Task DeleteDirectory(string path);
    }

    public class FileService : IFileService
    {
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

            return File.WriteAllTextAsync(path, text);
        }

        public Task Copy(string fromPath, string toPath)
        {
            File.Copy(fromPath, toPath);

            return Task.CompletedTask;
        }

        public Task<bool> Exists(string path)
        {
            return Task.FromResult(File.Exists(path));
        }

        public Task<List<string>> GetSubdirectories(string path)
        {
            return Task.FromResult(Directory.GetDirectories(path).ToList());
        }

        public Task<List<string>> TraverseDirectory(string path)
        {
            return Task.FromResult(Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList());
        }

        public Task CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);

            return Task.CompletedTask;
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
