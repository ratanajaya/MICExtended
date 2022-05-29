using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Model
{
    public class SelectionCondition
    {
        public bool CheckAllFile { get; set; }
        public List<string> FileTypes { get; set; } = new List<string>();

        public bool UseMinSize { get; set; }
        public bool UseMinB100 { get; set; }
        public int MinSize { get; set; }
        public int MinB100 { get; set; }
        public bool SkipCompressed { get; set; }

        public bool UseModifiedDateFrom { get; set; }
        public bool UseModifiedDateTo { get; set; }
        public DateTime ModifiedDateFrom { get; set; }
        public DateTime ModifiedDateTo { get; set; }
    }
}
