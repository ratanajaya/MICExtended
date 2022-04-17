using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Abstraction
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
}
