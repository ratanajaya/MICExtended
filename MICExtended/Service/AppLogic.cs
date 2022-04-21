using MICExtended.Abstraction;
using MICExtended.Common;
using MICExtended.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public List<FileModel> GetCompressedFilePreview(string dstPath, List<FileModel> sourceFiles, CompressionCondition selectionCondition) {
            var result = sourceFiles.AsParallel().Select(a => new FileModel {
                FilePath = Path.Combine(dstPath,
                    selectionCondition.ConvertTo == SupportedMimeType.JPEG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.JPG) :
                    selectionCondition.ConvertTo == SupportedMimeType.PNG ? Path.ChangeExtension(a.RelativePath, Constant.Extension.PNG) :
                    a.RelativePath),
                Size = null
            }).ToList();

            return result;
        }

        public async Task<List<FileModel>> GetFileViewModels(string path, SelectionCondition selection, IProgress<ProgressReport> progress) {
            int taskCount = 2;

            var sw = new Stopwatch();
            sw.Start();

            (List<FileModel> allData, bool isReload) = await Task.Run(() => {
                if(_fileCache.ContainsKey(path)) return (_fileCache[path], false);

                progress.Report(new ProgressReport {
                    CurrentTask = $"Looking up image files...",
                    Step = 0,
                    TaskCount = taskCount,
                });

                var filePaths = GetSuitableFilePaths(path).ToList();
                taskCount += filePaths.Count;

                FileModel[] result = new FileModel[filePaths.Count];

                progress.Report(new ProgressReport {
                    CurrentTask = $"Found {filePaths.Count} images. Loading metadata...",
                    Step = 1,
                    TaskCount = taskCount,
                });

                int parallelStep = 1;
                long lastTick = DateTime.Now.Ticks;

                var pathWithSlash = path + Path.DirectorySeparatorChar;
                Parallel.For(0, filePaths.Count, new ParallelOptions { MaxDegreeOfParallelism = 8 }, (i, state) => {
                    var filePath = filePaths[i];
                    try {
                        result[i] = GetFileViewModel(filePath, path);
                    }
                    catch(Exception ex) {
                        #pragma warning disable CS8625
                        result[i] = null;
                        #pragma warning restore CS8625
                        _log.Error($"GetFileViewModels | Parallel.For | {filePath} | {ex.Message}");
                    }

                    var currentStep = Interlocked.Increment(ref parallelStep);
                    long currentTick = DateTime.Now.Ticks;
                    if(currentTick - lastTick > 2000000) { //Updating too fast will cause winForm label to fail
                        Interlocked.Exchange(ref lastTick, currentTick);
                        progress.Report(new ProgressReport {
                            CurrentTask = $"Loading {filePath.Replace(pathWithSlash, string.Empty)}...",
                            Step = currentStep,
                            TaskCount = taskCount,
                        });
                    }
                });

                _fileCache.Add(path, result.Where(a => a != null).ToList());

                return (_fileCache[path], true);
            });

            var selectedData = allData.Where(a => 
                selection.FileTypes.Any(b => b.Equals(a.Extension, StringComparison.OrdinalIgnoreCase)) &&
                (!selection.UseMinB100 || a.BytesPer100Pixel >= selection.MinB100) &&
                (!selection.UseMinSize || a.Size >= selection.MinSize * 1024)
            ).ToList();

            var elapsed = sw.Elapsed;
            var reloadMessage = isReload ? $"Loaded {allData.Count} images in {elapsed.ToReadableString()}. ": "";
            var taskMessage = $"{reloadMessage}Filtered {selectedData.Count} of {allData.Count} images";

            progress.Report(new ProgressReport {
                CurrentTask = taskMessage,
                TaskEndMessage = taskMessage,
                TaskEnd = true,
                Step = taskCount,
                TaskCount = taskCount,
            });

            return selectedData;
        }

        private IEnumerable<string> GetSuitableFilePaths(string path) {
            var subDirs = _io.GetDirectories(path).Where(a => !a.EndsWith(Constant.Pathing.SCOMPRESSED));
            var filePathsFromSubdir = subDirs.SelectMany(s => GetSuitableFilePaths(s));
            var filePathsFromRoot = Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);

            var result = filePathsFromRoot.Concat(filePathsFromSubdir)
                         .Where(f => Constant.Extension.ALLOWED.Any(a => a.Equals(Path.GetExtension(f), StringComparison.OrdinalIgnoreCase)));

            return result;

            #region LEGACY without dir exclusion
            //return Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
            //    .Where(f => //!f.Contains(Constant.Pathing.SCOMPRESSED) && 
            //    Constant.Extension.ALLOWED.Any(a => a.Equals(Path.GetExtension(f), StringComparison.OrdinalIgnoreCase)));
            #endregion

            #region LEGACY with sorting
            //string[] subDirs = _io.GetDirectories(path);
            //var filePathsFromSubdir = subDirs.SelectMany(s => GetSuitableFilePaths(s));

            //var filePaths = Constant.Extension.ALLOWED.AsParallel()
            //    .SelectMany(a => Directory.EnumerateFiles(path, a, SearchOption.TopDirectoryOnly)).AsEnumerable();

            //.ToList();

            //var fileNames = filePaths.Select(f => Path.GetFileName(f));
            //var sortedFileNames = fileNames.OrderByAlphaNumeric(f => f);
            //var sortedFilePaths = fileNames.Select(f => Path.Combine(path, f));
            //var combinedFilePaths = filePathsFromSubdir.Concat(sortedFilePaths).ToList();

            //return filePaths;
            #endregion
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
            if(!srcFiles.Any()) {
                progress.Report(new ProgressReport {
                    CurrentTask = $"Operation invalid",
                    Step = 0,
                    TaskCount = 1,
                    TaskEnd = true,
                    ShowPopup = true,
                    TaskEndMessage = $"There is no file to compress"
                });

                return;
            }

            var sw = new Stopwatch();
            sw.Start();

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
                    parallelReport.CurrentTask = $"Compressed {src.RelativePath}...";
                    parallelReport.Step++;
                    progress.Report(parallelReport);
                }
            });

            progress.Report(new ProgressReport {
                CurrentTask = $"Finished compressing",
                Step = taskCount,
                TaskCount = taskCount,
                TaskEnd = true,
                ShowPopup = true,
                TaskEndMessage = $"{srcFiles.Count} images has been compressed in {sw.Elapsed.ToReadableString()}"
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
