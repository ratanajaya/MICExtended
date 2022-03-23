using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Models
{
    public class MainFormViewModel
    {
        public string SrcDir { get; set; }
        public string DstDir { get; set; }

        public List<FileInfo> SrcFileInfos { get; set; }
    }
}
