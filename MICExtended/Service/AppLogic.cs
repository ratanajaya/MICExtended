using MICExtended.Common;
using MICExtended.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MICExtended.Service
{
    public class AppLogic
    {
        private IIoWapper _io;
        private ImageCompressor _ic;
        private string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        private Dictionary<string, List<FileViewModel>> _fileCache = new Dictionary<string, List<FileViewModel>>();

        public AppLogic(IIoWapper io, ImageCompressor ic) {
            _io = io;
            _ic = ic;
        }

        #region Configuration
        public async Task<ConfigurationModel> LoadConfiguration() {
            var defaultConf = new ConfigurationModel {
                Selection = new SelectionCondition {
                    FileTypes = new List<string> { Constant.Extension.JPEG, Constant.Extension.JPG, Constant.Extension.PNG }
                }
            };

            try {
                if(!_io.FileExist(_configPath)) return defaultConf;

                var configTxt = await _io.ReadAllText(_configPath);
                return JsonSerializer.Deserialize<ConfigurationModel>(configTxt) ?? defaultConf;
            }
            catch(Exception ex) {
                return defaultConf;
            }
        }

        public async Task SaveConfiguration(MainFormViewModel viewModel) {
            try {
                var config = new ConfigurationModel {
                    Compression = viewModel.Compression,
                    Selection = viewModel.Selection,
                };

                var configJson = JsonSerializer.Serialize(config);
                await _io.WriteAllText(_configPath, configJson);
            }
            catch(Exception ex) { 
                
            }
        }
        #endregion

        public IEnumerable<FileViewModel> GetCompressedFilePreview(string srcPath, string dstPath, List<FileViewModel> sourceFiles, CompressionCondition selectionCondition) {
            var result = sourceFiles.Select(a => new FileViewModel {
                FilePath = Path.Combine(dstPath, 
                    selectionCondition.ConvertTo == SupportedMimeType.JPEG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.JPG) : 
                    selectionCondition.ConvertTo == SupportedMimeType.PNG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.PNG) :
                    a.RelativePath),
                Size = null
            });

            return result;
        }

        public IEnumerable<FileViewModel> GetFileViewModels(string path, SelectionCondition selection) {
            var allData = new Func<List<FileViewModel>>(() => {
                if(_fileCache.ContainsKey(path)) return _fileCache[path];

                var filePaths = GetSuitableFilePaths(path);
                var fileVms = filePaths.Select(a => GetFileViewModel(a, path)).ToList();

                _fileCache.Add(path, fileVms);

                return _fileCache[path];
            })();

            var selectedData = allData.Where(a => 
                selection.FileTypes.Any(b => b.Equals(a.Extension, StringComparison.OrdinalIgnoreCase)) &&
                (!selection.UseMinB100 || a.BytesPer100Pixel >= selection.MinB100) &&
                (!selection.UseMinSize || a.Size >= selection.MinSize * 1024)
            );

            return selectedData;
        }

        private IEnumerable<string> GetSuitableFilePaths(string path) {
            string[] subDirs = _io.GetDirectories(path);
            var filePathsFromSubdir = subDirs.SelectMany(s => GetSuitableFilePaths(s));

            var filePaths = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                .Where(f => Constant.Extension.ALLOWED.Any(a => a.Equals(Path.GetExtension(f), StringComparison.OrdinalIgnoreCase)));

            //var filePaths = Constant.Extension.ALLOWED.AsParallel()
            //    .SelectMany(a => Directory.EnumerateFiles(path, a, SearchOption.TopDirectoryOnly)).AsEnumerable();

            //.ToList();

            //var fileNames = filePaths.Select(f => Path.GetFileName(f));
            //var sortedFileNames = fileNames.OrderByAlphaNumeric(f => f);
            //var sortedFilePaths = fileNames.Select(f => Path.Combine(path, f));
            //var combinedFilePaths = filePathsFromSubdir.Concat(sortedFilePaths).ToList();

            return filePaths;
        }

        private FileViewModel GetFileViewModel(string path, string rootPath) {
            //var fi = new FileInfo(path);
            using(var fileStream = _io.GetStream(path)) {
                using(var img = Image.FromStream(fileStream, false, false)) {
                    return new FileViewModel {
                        RootPath = rootPath,
                        FilePath = path,
                        Extension = Path.GetExtension(path),
                        Size = fileStream.Length,
                        Height = img.Height,
                        Width = img.Width,
                    };
                }
            }
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

        public IEnumerable<FileViewModel> LoadFileDetail(List<FileViewModel> files) {
            return files.Select(a => GetFileViewModel(a.FilePath, a.RootPath));
        }

        public void ClearCache() {
            _fileCache.Clear();
        }
    }
}
