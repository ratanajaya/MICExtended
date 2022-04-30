using MICExtended.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Service
{
    public class IoWrapper : IIoWapper
    {
        public IEnumerable<string> GetAllFiles(string path) {
            return Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption) {
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
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

        public void DeleteFile(string path) { 
            File.Delete(path);
        }
    }
}
