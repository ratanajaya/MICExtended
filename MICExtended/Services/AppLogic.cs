using MICExtended.Helpers;
using MICExtended.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Services
{
    public class AppLogic
    {
        private IIoWapper _io;

        public AppLogic(IIoWapper io) {
            _io = io;
        }

        public List<FileInfo> GetFileInfos(string path) {
            var filePaths = _io.GetAllFiles(path);
            var result = filePaths.OrderByAlphaNumeric(a => a)
                .Select(a => new FileInfo(a))
                .ToList();

            return result;
        }
    }
}
