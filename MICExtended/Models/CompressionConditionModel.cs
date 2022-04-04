using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Models
{
    public class CompressionConditionModel
    {
        public int Quality { get; set; } = 90;
        public Dimension Dimension { get; set; } = Dimension.NewDimensionInPct;
        public int DimensionFixedWidth { get; set; } = 1920;
        public int DimensionInPct { get; set; } = 100;

        public ConvertTo ConvertTo { get; set; } = ConvertTo.Original;
    }

    public enum Dimension
    {
        FixedWidth,
        NewDimensionInPct
    }

    public enum ConvertTo
    {
        Original,
        JPEG,
        PNG
    }
}
