﻿#define MULTITHREADING

using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Linq;
using System.Threading;
using MICExtended.Common;
using Serilog;
using System.Reflection;
using MICExtended.Model;
using System.Text;

namespace MICExtended.Service
{
    /// <summary>
    /// Taken from yugee's Mass Image Compressor
    /// https://sourceforge.net/p/icompress/code/
    /// </summary>
    public class ImageProcessor
    {
        //TODO refactor class to use IoWrapper
        private AppSettingJson _appSetting;
        private ILogger _log;

        public ImageProcessor(ILogger log, AppSettingJson appSetting) {
            _log = log;
            _appSetting = appSetting;
        }

        #region From ImageCompressor.cs
        public bool CompressImage(string filePath, string savePath, int quality, Size size, SupportedMimeType type) {
            try {
                Bitmap img = GetBitmap(filePath);
                byte[] originalFile = null;
                if(SavingAsSameMimeType(filePath, type)) {
                    originalFile = File.ReadAllBytes(filePath);
                }

                ImageCodecInfo imageCodecInfo;

                if(type == SupportedMimeType.JPEG || IsRaw(filePath))
                    imageCodecInfo = GetEncoderInfo("image/jpeg");
                else if(type == SupportedMimeType.PNG)
                    imageCodecInfo = GetEncoderInfo("image/png");
                else
                    imageCodecInfo = GetEncoderInfoFromOriginalFile(filePath);

                if(imageCodecInfo == null)
                    return false;

                EncoderParameters encoderParameters;

                bool keepOriginalSize = false;
                long OriginalFileSize = GetFileSize(filePath);
                if(img.Size.Height <= size.Height || img.Size.Width <= size.Width) {
                    size = img.Size;
                    keepOriginalSize = true;
                }

                Bitmap imgCompressed = new Bitmap(size.Width, size.Height);
                using(Graphics gr = Graphics.FromImage(imgCompressed)) {
                    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    gr.DrawImage(img, new Rectangle(0, 0, size.Width, size.Height));
                }

                foreach(var id in img.PropertyIdList) {
                    imgCompressed.SetPropertyItem(img.GetPropertyItem(id));
                }
                img.Dispose();


                if(quality > GetQualityIfCompressed(filePath, imgCompressed))
                    quality = GetQualityIfCompressed(filePath, imgCompressed); //don't save higher qulaity than required.

                //SetImageComments(filePath, imgCompressed, quality);

                encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] =
                    new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                string fileSavePath = ChangeExensionToMimeType(savePath, type);
                imgCompressed.Save(fileSavePath, imageCodecInfo, encoderParameters);

                imgCompressed.Dispose();

                if(fileSavePath.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase)) {
                    if(quality < 100)
                        CompressPNG(fileSavePath, quality, true);
                    OptimizePNG(fileSavePath, true);
                }
                else if(fileSavePath.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase) || fileSavePath.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase)) {
                    MakeJpegProgressive(fileSavePath, true);
                }

                if(keepOriginalSize && SavingAsSameMimeType(filePath, type) && GetFileSize(fileSavePath) > OriginalFileSize) {
                    File.WriteAllBytes(fileSavePath, originalFile);
                }

                return true;
            }
            catch(Exception ex) {
                _log.Error($"CompressImage | {filePath} | {ex.Message}");
                return false;
            }
        }

        public void SetComment(string filePath) {
            try {
                var ext = Path.GetExtension(filePath);

                if(ext == Constant.Extension.JPG)
                    SetJpgComment(filePath);
                else if(ext == Constant.Extension.PNG) {
                    //TODO
                }
            }
            catch(Exception ex) {
                _log.Error($"SetComment | {filePath} | {ex.Message}");
            }
        }

        public string GetComment(string filePath, Image img) {
            try {
                var ext = Path.GetExtension(filePath);

                if(ext == Constant.Extension.JPG) {
                    //var commentProp = img.PropertyItems.FirstOrDefault(a => a.Id == Constant.COMMENT_PROPID);
                    //if(commentProp != null)
                    //    return Encoding.UTF8.GetString(commentProp?.Value).Replace("\0", string.Empty);
                    //TODO
                }
                else if(ext == Constant.Extension.PNG) {
                    //TODO
                }
                return string.Empty;
            }
            catch(Exception ex) {
                _log.Error($"LoadComment | {filePath} | {ex.Message}");
                return string.Empty;
            }
        }

        void SetJpgComment(string filePath) {
            //Bitmap bmp = GetBitmap(filePath);
            //string comment = Constant.Message.ImageComment;
            //try {
            //    PropertyItem propItem;

            //    if(bmp.PropertyIdList.Contains(Constant.COMMENT_PROPID))
            //        propItem = bmp.GetPropertyItem(Constant.COMMENT_PROPID);
            //    else {
            //        propItem = (PropertyItem)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(PropertyItem));
            //        propItem.Id = Constant.COMMENT_PROPID;
            //    }

            //    propItem.Len = comment.Length + 1;

            //    byte[] newValb = System.Text.Encoding.UTF8.GetBytes(comment + "\0");

            //    propItem.Value = newValb;
            //    propItem.Type = 2;
            //    bmp.SetPropertyItem(propItem);
            //}
            //catch(Exception ex) {
            //    _log.Error($"SetImageComments | {filePath} | {ex.Message}");
            //}
        }
        #endregion

        #region From Helper.cs
        int GetQualityIfCompressed(string filePath, Bitmap bmp) {
            try {
                PropertyItem propItem;

                if(bmp.PropertyIdList.Contains(0x9286))
                    propItem = bmp.GetPropertyItem(0x9286);
                else
                    return 100;

                string comment = System.Text.Encoding.UTF8.GetString(propItem.Value);

                if(comment.Contains('\0'))
                    comment = string.Join("", comment.Split('\0'));

                string[] splits = comment.Split(':');
                if(splits.Length > 1) {
                    return int.Parse(splits[2]);
                }
                else
                    return 100;
            }
            catch(Exception ex) {
                _log.Error($"GetQualityIfCompressed | {filePath} | {ex.Message}");
                return 100; //default to 100 in case of error.
            }
        }

        bool IsSupportedImage(string filePath) {
            return _appSetting.AllowedExtensions.Contains(Path.GetExtension(filePath).ToUpper());
        }

        bool IsRaw(string filePath) {
            return _appSetting.AllowedRawExtensions.Contains(Path.GetExtension(filePath).ToUpper());
        }

        Bitmap GetBitmap(string filepath) {
            if(IsRaw(filepath))
                return GetRawImageData(filepath, GetFullPath(@"Exec\dcraw.exe"));
            else
                return new System.Drawing.Bitmap(filepath);
        }

        Bitmap GetRawImageData(string filePath, string execDcrawExe) {
            var f = new FileInfo(execDcrawExe);
            var args = "-c -T";

            var startInfo = new System.Diagnostics.ProcessStartInfo(f.FullName) {
                Arguments = args + " \"" + filePath + "\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = System.Diagnostics.Process.Start(startInfo);

            if(process == null) return null;

            try {
                return (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(process.StandardOutput.BaseStream, true, true);
            }
            catch(Exception) {
                // ignored
            }

            return null;
        }

        string GetFullPath(string relativePath) {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), relativePath);
        }

        bool SavingAsSameMimeType(string filePath, SupportedMimeType type) {
            string ext = Path.GetExtension(filePath);

            if(type == SupportedMimeType.ORIGINAL)
                return true;

            if((ext.Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase) || ext.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase))
                && (type == SupportedMimeType.JPEG))
                return true;
            if(ext.Equals(".png", StringComparison.InvariantCultureIgnoreCase) && (type == SupportedMimeType.PNG))
                return true;
            return false;
        }

        ImageCodecInfo GetEncoderInfo(String mimeType) {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for(j = 0; j < encoders.Length; ++j) {
                if(encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        ImageCodecInfo GetEncoderInfoFromOriginalFile(String filePath) {
            string inputfileExt = "*" + Path.GetExtension(filePath).ToUpper();

            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for(j = 0; j < encoders.Length; ++j) {
                if(encoders[j].FilenameExtension.Contains(inputfileExt))
                    return encoders[j];
            }

            return null;
        }

        string ChangeExensionToMimeType(string fullFilePath, SupportedMimeType type) {
            string dirPath = RemoveFileName(fullFilePath);

            if(type == SupportedMimeType.JPEG || IsRaw(fullFilePath))
                return
                    AddDirectorySeparatorAtEnd(dirPath)
                    + Path.GetFileNameWithoutExtension(fullFilePath)
                    + Constant.Extension.JPG;
            else if(type == SupportedMimeType.PNG)
                return
                    AddDirectorySeparatorAtEnd(dirPath)
                    + Path.GetFileNameWithoutExtension(fullFilePath)
                    + Constant.Extension.PNG;
            else
                return fullFilePath;
        }

        string RemoveFileName(string pathWithFileName) {
            string dirPath = "";

            if(string.IsNullOrEmpty(Path.GetFileName(pathWithFileName)))
                return "";

            string[] DirsAndFile = pathWithFileName.Split(Path.DirectorySeparatorChar);
            if(DirsAndFile == null) return "";

            for(int i = 0; i < DirsAndFile.Length - 1; i++)
                dirPath += DirsAndFile[i] + Path.DirectorySeparatorChar;
            return dirPath;
        }

        string AddDirectorySeparatorAtEnd(string path) {
            if(!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                return path + Path.DirectorySeparatorChar.ToString();
            return path;
        }

        void CompressPNG(string filePath, int qualityLevel, bool waitForProcessToEnd) {
            var exeFilePath = GetFullPath(@"Exec\pngquant.exe");
            var args = " --quality=0-" + qualityLevel + " -f --ext .png ";

            RunExternalOperationOnImage(filePath, exeFilePath, args, waitForProcessToEnd);
        }


        void OptimizePNG(string filePath, bool waitForProcessToEnd) {
            var exeFilePath = GetFullPath(@"Exec\optipng.exe");
            var args = "-o2 -strip all";

            RunExternalOperationOnImage(filePath, exeFilePath, args, waitForProcessToEnd);
        }

        void RunExternalOperationOnImage(string filePath, string exeFilePath, string args, bool waitForProcessToEnd) {
            var f = new FileInfo(exeFilePath);
            var startInfo = new System.Diagnostics.ProcessStartInfo(f.FullName) {
                Arguments = args + " \"" + filePath + "\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = System.Diagnostics.Process.Start(startInfo);

            if(waitForProcessToEnd)
                process.WaitForExit();
        }

        long GetFileSize(string filePath) {
            return new FileInfo(filePath).Length;
        }

        void MakeJpegProgressive(string filePath, bool waitForProcessToEnd) {
            var exeFilePath = GetFullPath(@"Exec\jpegtran.exe");
            var args = "-copy all -optimize -progressive -outfile " + " \"" + filePath + "\"";
            RunExternalOperationOnImage(filePath, exeFilePath, args, waitForProcessToEnd);
        }
        #endregion
    }
}
