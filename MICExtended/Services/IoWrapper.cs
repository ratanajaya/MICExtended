using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Services
{
    /// <summary>
    /// Very thin abstraction of System.IO for Unit testing purposes
    /// </summary>
    public interface IIoWapper
    {
        IEnumerable<string> GetAllFiles(string path);
        void CreateDirectory(string path);
    }

    public class IoWrapper : IIoWapper
    {
        public IEnumerable<string> GetAllFiles(string path) {
            return Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);
        }

        public void CreateDirectory(string path) { 
            Directory.CreateDirectory(path);
        }
    }
}
