using MICExtended.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Abstraction
{
    public interface IPersistable
    {
        SelectionCondition Selection { get; set; }
        CompressionCondition Compression { get; set; }
    }
}
