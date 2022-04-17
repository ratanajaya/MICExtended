using MICExtended.Abstraction;
using MICExtended.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Model
{
    public class PersistentState : IPersistable
    {
        public CompressionCondition Compression { get; set; } = new CompressionCondition();
        public SelectionCondition Selection { get; set; } = new SelectionCondition();
    }
}
