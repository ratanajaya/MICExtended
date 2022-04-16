using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Service
{
    /// <summary>
    /// Very thin abstraction of System.IO for Unit testing purposes
    /// </summary>
    public interface IIoWapper
    {
        IEnumerable<string> GetAllFiles(string path);
        void CreateDirectory(string path);
        string[] GetDirectories(string path);
        bool FileExist(string path);
        Task<string> ReadAllText(string path);
        Task WriteAllText(string path, string text);
        Stream GetStream(string path);
    }

    public class IoWrapper : IIoWapper
    {
        public IEnumerable<string> GetAllFiles(string path) {
            return Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);
        }

        public string[] GetDirectories(string path) {
            return Directory.GetDirectories(path);
        }

        public void CreateDirectory(string path) { 
            Directory.CreateDirectory(path);
        }

        public bool FileExist(string path) { 
            return File.Exists(path);
        }

        public Task<string> ReadAllText(string path) {
            return File.ReadAllTextAsync(path);
        }

        public Task WriteAllText(string path, string text) { 
            return File.WriteAllTextAsync(path, text);
        }

        public Stream GetStream(string path) {
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}
