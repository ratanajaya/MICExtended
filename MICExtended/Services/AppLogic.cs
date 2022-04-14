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

        public IEnumerable<FileViewModel> GetCompressedFilePreview(string srcPath, string dstPath, List<FileViewModel> sourceFiles, CompressionCondition selectionCondition) {
            var result = sourceFiles.Select(a => new FileViewModel {
                FilePath = Path.Combine(dstPath, 
                    selectionCondition.ConvertTo == SupportedMimeType.JPEG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.JPG.ToLower()) : 
                    selectionCondition.ConvertTo == SupportedMimeType.PNG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.PNG.ToLower()) :
                    a.RelativePath),
                Size = null
            });

            return result;
        }

        public IEnumerable<FileViewModel> GetFileViewModels(string path, List<string> fileTypes) {
            var filePaths = GetSuitableFilePaths(path, fileTypes);
            var result = filePaths.Select(a => {
                var fi = new FileInfo(a);
                return new FileViewModel {
                    RootPath = path,
                    FilePath = fi.FullName,
                    Extension = fi.Extension,
                    Size = fi.Length,
                };
            });

            return result;
        }

        private IEnumerable<string> GetSuitableFilePaths(string path, List<string> fileTypes) {
            string[] subDirs = _io.GetDirectories(path);
            var filePathsFromSubdir = subDirs.SelectMany(s => GetSuitableFilePaths(s, fileTypes));

            var filePaths = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => fileTypes.Any(a => a.Equals(Path.GetExtension(f), StringComparison.OrdinalIgnoreCase)))
                .ToList();

            var fileNames = filePaths.Select(f => Path.GetFileName(f));
            var sortedFileNames = fileNames.OrderByAlphaNumeric(f => f);
            var sortedFilePaths = sortedFileNames.Select(f => Path.Combine(path, f));
            var combinedFilePaths = filePathsFromSubdir.Concat(sortedFilePaths);

            return combinedFilePaths;
        }

        public Task CompressFiles(List<FileViewModel> srcFiles, List<FileViewModel> dstFiles, CompressionCondition compressionCondition, IProgress<ProgressReport> progress) {
            if(srcFiles.Count != dstFiles.Count) throw new InvalidDataException("srcFiles and dstFiles have different length");

            return Task.Run(() => {
                var taskCount = srcFiles.Count;
                for(int i = 0; i < taskCount; i++) {
                    progress.Report(new ProgressReport {
                        CurrentTask = $"Compressing {srcFiles[i].Name}...",
                        Step = i,
                        TaskCount = taskCount,
                    });

                    var src = srcFiles[i];
                    var dst = dstFiles[i];

                    var dstDir = Path.GetDirectoryName(dst.FilePath);
                    _io.CreateDirectory(dstDir);

                    _ic.CompressImage(src.FilePath, dst.FilePath, compressionCondition.Quality, null, compressionCondition.ConvertTo);
                }

                progress.Report(new ProgressReport {
                    CurrentTask = $"Finished compressing",
                    Step = taskCount,
                    TaskCount = taskCount,
                    TaskEnd = true,
                    TaskEndMessage = $"{taskCount} images has been compressed"
                });
            });
        }
    }
}
