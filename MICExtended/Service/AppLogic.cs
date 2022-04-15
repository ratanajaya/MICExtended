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

        private Dictionary<string, IEnumerable<FileViewModel>> _fileCache = new Dictionary<string, IEnumerable<FileViewModel>>();

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
                    selectionCondition.ConvertTo == SupportedMimeType.JPEG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.JPG.ToLower()) : 
                    selectionCondition.ConvertTo == SupportedMimeType.PNG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.PNG.ToLower()) :
                    a.RelativePath),
                Size = null
            });

            return result;
        }

        public IEnumerable<FileViewModel> GetFileViewModels(string path, SelectionCondition selection) {
            var allData = new Func<IEnumerable<FileViewModel>>(() => {
                if(_fileCache.ContainsKey(path)) return _fileCache[path];

                var filePaths = GetSuitableFilePaths(path);
                var fileVms = filePaths.Select(a => {
                    var fi = new FileInfo(a);
                    var img = Image.FromFile(a);
                    return new FileViewModel {
                        RootPath = path,
                        FilePath = fi.FullName,
                        Extension = fi.Extension,
                        Size = fi.Length,
                        Height = img.Height,
                        Width = img.Width,
                    };
                });

                _fileCache.Add(path, fileVms);

                return _fileCache[path];
            })();

            var selectedData = allData.Where(a => 
                selection.FileTypes.Any(b => b.Equals(a.Extension, StringComparison.OrdinalIgnoreCase)) &&
                (!selection.UseMinB100 || a.BytesPer100Pixel >= selection.MinB100) &&
                (!selection.UseMinSize || a.Size >= selection.MinSize)
            );

            return selectedData;
        }

        private IEnumerable<string> GetSuitableFilePaths(string path) {
            string[] subDirs = _io.GetDirectories(path);
            var filePathsFromSubdir = subDirs.SelectMany(s => GetSuitableFilePaths(s));

            var filePaths = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => Constant.Extension.ALLOWED.Any(a => a.Equals(Path.GetExtension(f), StringComparison.OrdinalIgnoreCase)))
                .ToList();

            var fileNames = filePaths.Select(f => Path.GetFileName(f));
            var sortedFileNames = fileNames.OrderByAlphaNumeric(f => f);
            var sortedFilePaths = fileNames.Select(f => Path.Combine(path, f));
            var combinedFilePaths = filePathsFromSubdir.Concat(sortedFilePaths).ToList();

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

        public void ClearCache() {
            _fileCache.Clear();
        }
    }
}
