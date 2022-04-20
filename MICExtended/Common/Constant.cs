using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Common
{
    public static class Constant
    {
        public static class Pathing
        {
            public const string COMPRESSED = "_compressed";
            public const string SCOMPRESSED = "\\_compressed";
        }
        public static class Extension
        {
            public const string JPG = ".jpg";
            public const string JPEG = ".jpeg";
            public const string PNG = ".png";

            public static readonly ReadOnlyCollection<string> ALLOWED = new ReadOnlyCollection<string> (
                new[] {
                    JPG, JPEG, PNG , ".bmp" , ".rle" , ".dib" , ".gif" , ".tif" , ".tiff", ".wmf" ,
                    ".3fr" , ".ari" , ".arw" , ".bay" , ".crw" , ".cr2" , ".cap" , ".dcs" , ".dcr" , ".dng" ,
                    ".drf" , ".eip" , ".erf" , ".fff" , ".iiq" , ".k25" , ".kdc" , ".mdc" , ".mef" , ".mos" ,
                    ".mrw" , ".nef" , ".nrw" , ".obm" , ".orf" , ".pef" , ".ptx" , ".pxn" , ".r3d" , ".raf" ,
                    ".raw" , ".rwl" , ".rw2" , ".rwz" , ".sr2" , ".srf" , ".srw" , ".x3f"
                });

            public static readonly ReadOnlyCollection<string> ALLOWED_RAW = new ReadOnlyCollection<string>(
                new[] {
                    ".3fr" , ".ari" , ".arw" , ".bay" , ".crw" , ".cr2" , ".cap" , ".dcs" , ".dcr" , ".dng" ,
                    ".drf" , ".eip" , ".erf" , ".fff" , ".iiq" , ".k25" , ".kdc" , ".mdc" , ".mef" , ".mos" ,
                    ".mrw" , ".nef" , ".nrw" , ".obm" , ".orf" , ".pef" , ".ptx" , ".pxn" , ".r3d" , ".raf" ,
                    ".raw" , ".rwl" , ".rw2" , ".rwz" , ".sr2" , ".srf" , ".srw" , ".x3f"
                });
        }
    }

    public enum SupportedMimeType
    {
        JPEG,
        PNG,
        ORIGINAL
    }

    public enum Dimension
    {
        FixedWidth,
        NewDimensionInPct
    }
}
