using MICExtended.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;

namespace MICExtended.Model
{
    public class FileModel
    {
        public string RootPath { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Extension { get; set;} = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public long? Size { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public PropertyItem[] PropertyItems { get; set; }

        public string Name { 
            get {
                return Path.GetFileName(FilePath);
            } 
        }

        public string NameWithoutExt {
            get {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        public string RelativePath {
            get {
                return Path.GetRelativePath(RootPath, FilePath);
            }
        }

        public string SizeDisplay { 
            get {
                return Size.HasValue ? Function.BytesToString(Size.Value) : string.Empty;
            } 
        }

        public string DimensionDisplay {
            get {
                return $"{Width} x {Height}";
            }
        }

        public long Area {
            get {
                return Height * Width;
            }
        }

        public decimal BytesPerPixel {
            get {
                if((Area == 0) || Size.GetValueOrDefault() == 0) return 0;

                return (decimal)Size.GetValueOrDefault() / (decimal)Area;
            }
        }

        public int BytesPer100Pixel {
            get {
                if((Area == 0) || Size.GetValueOrDefault() == 0) return 0;

                return (int)(100 * (decimal)Size.GetValueOrDefault() / (decimal)Area);
            }
        }

        public string BytesPer100PixelDisplay {
            get {
                return BytesPer100Pixel.ToString();
            }
        }
    }
}
