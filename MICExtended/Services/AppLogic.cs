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

        public IEnumerable<FileViewModel> GetCompressedFilePreview(string srcPath, string dstPath, List<FileViewModel> sourceFiles, SelectionConditionModel selectionCondition) {
            var result = sourceFiles.Select(a => new FileViewModel {
                FilePath = Path.Combine(dstPath, selectionCondition.ConvertToJpg 
                    ? Path.ChangeExtension(a.RelativePath, ".jpg") 
                    : a.RelativePath),
                Size = null
            });

            return result;
        }

        public IEnumerable<FileViewModel> GetFileViewModels(string path) {
            var result = GetFileInfos(path).OrderByAlphaNumeric(a => a.FullName)
                    .Select(a => new FileViewModel {
                        RootPath = path,
                        FilePath = a.FullName,
                        Extension = a.Extension,
                        Size = a.Length,
                    });

            return result;
        }

        private IEnumerable<FileInfo> GetFileInfos(string path) {
            var filePaths = _io.GetAllFiles(path);
            var result = filePaths.Select(a => new FileInfo(a))
                .ToList();

            return result;
        }
    }
}
