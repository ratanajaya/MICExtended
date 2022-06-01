using MICExtended.Model;
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
        }
        public static class Message
        {
            public const string ImageComment = "MICExtended compressed this image";
        }

        public const int COMMENT_PROPID = 0x9286;
        public static readonly DateTime MIN_DATE = new DateTime(1970, 01, 01);
        public static readonly DateTime MAX_DATE = new DateTime(9998, 12, 31);
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
