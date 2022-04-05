﻿using MICExtended.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Models
{
    public class CompressionCondition
    {
        public int Quality { get; set; } = 90;
        public Dimension Dimension { get; set; } = Dimension.NewDimensionInPct;
        public int DimensionFixedWidth { get; set; } = 1920;
        public int DimensionInPct { get; set; } = 100;

        public SupportedMimeType ConvertTo { get; set; } = SupportedMimeType.ORIGINAL;
    }
}
