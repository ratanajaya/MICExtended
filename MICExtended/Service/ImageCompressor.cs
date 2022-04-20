#define MULTITHREADING

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

namespace MICExtended.Service
{
    /// <summary>
    /// Taken from yugee's Mass Image Compressor
    /// https://sourceforge.net/p/icompress/code/
    /// </summary>
    public class ImageCompressor
    {
        private ILogger _log;

        public ImageCompressor(ILogger log) {
            _log = log;
        }

        public void CompressImage(string filePath, string savePath, int quality, Size? size, SupportedMimeType type) {
            Bitmap img = Helper.GetBitmap(filePath);

            var nSize = size ?? img.Size;

            this.CompressImage(
                filePath,
                img,
                savePath,
                quality,
                nSize,
                type
                    );
            img.Dispose();
        }

        private void CompressImage(string filePath, Bitmap img, string savePath, int quality, Size size, SupportedMimeType type) {
            try {
                byte[] originalFile = null;
                if(Helper.SavingAsSameMimeType(filePath, type)) {
                    originalFile = File.ReadAllBytes(filePath);
                }

                ImageCodecInfo imageCodecInfo;

                if(type == SupportedMimeType.JPEG || Helper.IsRaw(filePath))
                    imageCodecInfo = Helper.GetEncoderInfo("image/jpeg");
                else if(type == SupportedMimeType.PNG)
                    imageCodecInfo = Helper.GetEncoderInfo("image/png");
                else
                    imageCodecInfo = Helper.GetEncoderInfoFromOriginalFile(filePath);

                if(imageCodecInfo == null)
                    return;

                EncoderParameters encoderParameters;

                bool keepOriginalSize = false;
                long OriginalFileSize = Helper.GetFileSize(filePath);
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

                SetImageComments(filePath, imgCompressed, quality);

                encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] =
                    new EncoderParameter(Encoder.Quality, quality);

                string fileSavePath = Helper.ChangeExensionToMimeType(savePath, type);
                imgCompressed.Save(fileSavePath, imageCodecInfo, encoderParameters);

                imgCompressed.Dispose();

                if(fileSavePath.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase)) {
                    if(quality < 100)
                        Helper.CompressPNG(fileSavePath, quality, true);
                    Helper.OptimizePNG(fileSavePath, true);
                }
                else if(fileSavePath.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase) || fileSavePath.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase)) {
                    Helper.MakeJpegProgressive(fileSavePath, true);
                }

                if(keepOriginalSize && Helper.SavingAsSameMimeType(filePath, type) && Helper.GetFileSize(fileSavePath) > OriginalFileSize) {
                    File.WriteAllBytes(fileSavePath, originalFile);
                }
            }
            catch(Exception ex) {
                _log.Error($"CompressImage | {filePath} | {ex.Message}");
            }
        }

        public void SetImageComments(string filePath, Bitmap bmp, int quality) {
            string newVal = "Mass Image Compressor Compressed this image. https://sourceforge.net/projects/icompress/ with Quality:" + quality;
            try {
                PropertyItem propItem;

                if(bmp.PropertyIdList.Contains(0x9286))
                    propItem = bmp.GetPropertyItem(0x9286);
                else {
                    propItem = (PropertyItem)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(PropertyItem));
                    propItem.Id = 0x9286;
                }

                propItem.Len = newVal.Length + 1;

                byte[] newValb = System.Text.Encoding.UTF8.GetBytes(newVal + "\0");

                propItem.Value = newValb;
                propItem.Type = 2;
                bmp.SetPropertyItem(propItem);
            }
            catch(Exception ex) {
                _log.Error($"SetImageComments | {filePath} | {ex.Message}");
            }
        }

        public int GetQualityIfCompressed(string filePath, Bitmap bmp) {
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
    }
}
