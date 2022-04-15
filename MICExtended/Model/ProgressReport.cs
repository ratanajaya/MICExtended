using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Model
{
    public class ProgressReport
    {
        public int TaskCount { get; set; }
        public int Step { get; set; }
        public int StepPct { 
            get {
                if(TaskCount == 0) return 0;

                return (int)((float)Step/TaskCount * 1000); 
            }
        }
        public string CurrentTask { get; set; } = "";
        public bool TaskEnd { get; set; }
        public string TaskEndMessage { get; set; } = "";
    }
}
