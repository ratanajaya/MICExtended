using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Models
{
    public class ProgressReport
    {
        public int TaskCount { get; set; }
        public int Step { get; set; }
        public int StepPct { 
            get { 
                return (int)((float)Step/TaskCount * 100); 
            }
        }
        public string CurrentTask { get; set; } = "";
    }
}
