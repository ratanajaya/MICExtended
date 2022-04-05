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
        }
        public static class Extension
        {
            public const string JPG = ".jpg";
            public const string JPEG = ".jpeg";
            public const string PNG = ".png";

            public static readonly ReadOnlyCollection<string> ALLOWED = new ReadOnlyCollection<string> (
                new[] { 
                    ".JPG" , ".JPEG", ".PNG" , ".BMP" , ".RLE" , ".DIB" , ".GIF" , ".TIF" , ".TIFF", ".WMF" ,
                    ".3FR" , ".ARI" , ".ARW" , ".BAY" , ".CRW" , ".CR2" , ".CAP" , ".DCS" , ".DCR" , ".DNG" ,
                    ".DRF" , ".EIP" , ".ERF" , ".FFF" , ".IIQ" , ".K25" , ".KDC" , ".MDC" , ".MEF" , ".MOS" ,
                    ".MRW" , ".NEF" , ".NRW" , ".OBM" , ".ORF" , ".PEF" , ".PTX" , ".PXN" , ".R3D" , ".RAF" ,
                    ".RAW" , ".RWL" , ".RW2" , ".RWZ" , ".SR2" , ".SRF" , ".SRW" , ".X3F"
                });

            public static readonly ReadOnlyCollection<string> ALLOWED_RAW = new ReadOnlyCollection<string>(
                new[] {
                    ".3FR" , ".ARI" , ".ARW" , ".BAY" , ".CRW" , ".CR2" , ".CAP" , ".DCS" , ".DCR" , ".DNG" ,
                    ".DRF" , ".EIP" , ".ERF" , ".FFF" , ".IIQ" , ".K25" , ".KDC" , ".MDC" , ".MEF" , ".MOS" ,
                    ".MRW" , ".NEF" , ".NRW" , ".OBM" , ".ORF" , ".PEF" , ".PTX" , ".PXN" , ".R3D" , ".RAF" ,
                    ".RAW" , ".RWL" , ".RW2" , ".RWZ" , ".SR2" , ".SRF" , ".SRW" , ".X3F"
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
