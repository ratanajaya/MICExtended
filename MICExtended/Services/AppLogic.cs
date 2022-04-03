using MICExtended.Common;
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
        private ImageCompressor _ic;

        public AppLogic(IIoWapper io, ImageCompressor ic) {
            _io = io;
            _ic = ic;
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

        public void CompressFiles(List<FileViewModel> srcFiles, List<FileViewModel> dstFiles) {
            if(srcFiles.Count != dstFiles.Count) throw new InvalidDataException("srcFiles and dstFiles have different length");

            for(int i = 0; i < srcFiles.Count; i++) { 
                var src = srcFiles[i];
                var dst = dstFiles[i];

                var dstDir = Path.GetDirectoryName(dst.FilePath);
                _io.CreateDirectory(dstDir);

                _ic.CompressImage(src.FilePath, dst.FilePath, 90, null, SupportedMimeType.ORIGINAL);
            }
        }
    }
}
