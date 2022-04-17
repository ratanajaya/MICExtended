using MICExtended.Abstraction;
using MICExtended.Common;
using MICExtended.Model;
using Serilog;
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
        private ILogger _log;
        private string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        private Dictionary<string, List<FileModel>> _fileCache = new Dictionary<string, List<FileModel>>();

        public AppLogic(IIoWapper io, ImageCompressor ic, ILogger log) {
            _io = io;
            _ic = ic;
            _log = log;
        }

        #region Persistent State
        public async Task<PersistentState> LoadState() {
            var defaultConf = new PersistentState {
                Selection = new SelectionCondition {
                    FileTypes = new List<string> { Constant.Extension.JPEG, Constant.Extension.JPG, Constant.Extension.PNG },
                    CheckAllFile = false,
                    UseMinB100 = false,
                    UseMinSize = false,
                },
                Compression = new CompressionCondition {
                    ConvertTo = SupportedMimeType.ORIGINAL,
                    Dimension = Dimension.NewDimensionInPct,
                    DimensionInPct = 100,
                    DimensionFixedWidth = 0,
                    Quality = 90,
                }
            };

            try {
                if(!_io.FileExist(_configPath)) return defaultConf;

                var configTxt = await _io.ReadAllText(_configPath);
                return JsonSerializer.Deserialize<PersistentState>(configTxt) ?? defaultConf;
            }
            catch(Exception ex) {
                _log.Error($"LoadState | {_configPath} | {ex.Message}");
                return defaultConf;
            }
        }

        public async Task SaveState(Form1ViewModel viewModel) {
            try {
                var config = new PersistentState {
                    Compression = viewModel.Compression,
                    Selection = viewModel.Selection,
                };

                var configJson = JsonSerializer.Serialize(config);
                await _io.WriteAllText(_configPath, configJson);
            }
            catch(Exception ex) {
                _log.Error($"SaveState | {_configPath} | {ex.Message}");
            }
        }
        #endregion

        public List<FileModel> GetCompressedFilePreview(string srcPath, string dstPath, List<FileModel> sourceFiles, CompressionCondition selectionCondition) {
            var result = sourceFiles.AsParallel().Select(a => new FileModel {
                FilePath = Path.Combine(dstPath,
                    selectionCondition.ConvertTo == SupportedMimeType.JPEG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.JPG) :
                    selectionCondition.ConvertTo == SupportedMimeType.PNG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.PNG) :
                    a.RelativePath),
                Size = null
            }).ToList();

            return result;
        }

        public List<FileModel> GetFileViewModels(string path, SelectionCondition selection, IProgress<ProgressReport> progress) {
            int taskCount = 2;
            progress.Report(new ProgressReport {
                CurrentTask = $"Lorem ipsum.",
                Step = 0,
                TaskCount = taskCount,
            });

            var allData = new Func<IProgress<ProgressReport>, List<FileModel>>((lProgress) => {
                if(_fileCache.ContainsKey(path)) return _fileCache[path];

                lProgress.Report(new ProgressReport {
                    CurrentTask = $"Looking up image files...",
                    Step = 0,
                    TaskCount = taskCount,
                });

                var filePaths = GetSuitableFilePaths(path).ToList();

                lProgress.Report(new ProgressReport {
                    CurrentTask = $"Found {filePaths.Count} images. Loading metadata...",
                    Step = 1,
                    TaskCount = taskCount,
                });

                var fileVms = filePaths.AsParallel().Select(a => GetFileViewModel(a, path)).ToList();

                _fileCache.Add(path, fileVms);

                return _fileCache[path];
            })(progress);

            var selectedData = allData.Where(a => 
                selection.FileTypes.Any(b => b.Equals(a.Extension, StringComparison.OrdinalIgnoreCase)) &&
                (!selection.UseMinB100 || a.BytesPer100Pixel >= selection.MinB100) &&
                (!selection.UseMinSize || a.Size >= selection.MinSize * 1024)
            ).ToList();

            progress.Report(new ProgressReport {
                CurrentTask = $"Loading finished. {selectedData.Count()} images found",
                TaskEndMessage = $"Loading finished. {selectedData.Count()} images found",
                TaskEnd = true,
                Step = taskCount,
                TaskCount = taskCount,
            });

            return selectedData;
        }

        private IEnumerable<string> GetSuitableFilePaths(string path) {
            string[] subDirs = _io.GetDirectories(path);
            var filePathsFromSubdir = subDirs.SelectMany(s => GetSuitableFilePaths(s));

            var filePaths = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                .Where(f => Constant.Extension.ALLOWED.Any(a => a.Equals(Path.GetExtension(f), StringComparison.OrdinalIgnoreCase)));

            #region LEGACY with sorting
            //var filePaths = Constant.Extension.ALLOWED.AsParallel()
            //    .SelectMany(a => Directory.EnumerateFiles(path, a, SearchOption.TopDirectoryOnly)).AsEnumerable();

            //.ToList();

            //var fileNames = filePaths.Select(f => Path.GetFileName(f));
            //var sortedFileNames = fileNames.OrderByAlphaNumeric(f => f);
            //var sortedFilePaths = fileNames.Select(f => Path.Combine(path, f));
            //var combinedFilePaths = filePathsFromSubdir.Concat(sortedFilePaths).ToList();
            #endregion

            return filePaths;
        }

        private FileModel GetFileViewModel(string path, string rootPath) {
            using(var fileStream = _io.GetStream(path)) {
                using(var img = Image.FromStream(fileStream, false, false)) {
                    return new FileModel {
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

        public void CompressFiles(List<FileModel> srcFiles, List<FileModel> dstFiles, CompressionCondition compressionCondition, IProgress<ProgressReport> progress) {
            if(srcFiles.Count != dstFiles.Count) { throw new InvalidDataException("srcFiles and dstFiles have different length"); }

            var dirNames = dstFiles.Select(a => Path.GetDirectoryName(a.FilePath)).Distinct().ToList();
            dirNames.ForEach(dir => {
                _io.CreateDirectory(dir);
            });

            var taskCount = srcFiles.Count + 1;
            var parallelReport = new ProgressReport {
                CurrentTask = "Compressing files..",
                Step = 0,
                TaskCount = taskCount,
            };
            progress.Report(parallelReport);

            Parallel.For(0, srcFiles.Count, (i, state) => {
                //DO NOT perform operation that may target toward the same filesystem entity as it can cause race condition
                //Performing _io.CreateDirectory() here can cause race condition
                var src = srcFiles[i];
                var dst = dstFiles[i];
                try {
                    _ic.CompressImage(src.FilePath, dst.FilePath, compressionCondition.Quality, null, compressionCondition.ConvertTo);
                }
                catch(Exception ex) {
                    _log.Error($"CompressFiles | {src.FilePath} | {dst.FilePath} | {ex.Message}");
                }

                lock(parallelReport) {
                    parallelReport.CurrentTask = $"Compressing files... {src.Name} compressed";
                    parallelReport.Step += 1;
                    progress.Report(parallelReport);
                }
            });

            progress.Report(new ProgressReport {
                CurrentTask = $"Finished compressing",
                Step = taskCount,
                TaskCount = taskCount,
                TaskEnd = true,
                ShowPopup = true,
                TaskEndMessage = $"{taskCount} images has been compressed"
            });
        }

        public IEnumerable<FileModel> LoadFileDetail(List<FileModel> files) {
            return files.Select(a => GetFileViewModel(a.FilePath, a.RootPath));
        }

        public void ClearCache() {
            _fileCache.Clear();
        }
    }
}
