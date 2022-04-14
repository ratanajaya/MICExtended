using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Models
{
    public class MainFormViewModel
    {
        public string SrcDir { get; set; } = string.Empty;
        public string DstDir { get; set; } = string.Empty;

        public List<FileViewModel> SrcFiles { get; set; } = new List<FileViewModel>();
        public List<FileViewModel> DstFiles { get; set; } = new List<FileViewModel>();

        public CompressionCondition Compression { get; set; } = new CompressionCondition();

        public ProgressReport ProgressReport { get; set; } = new ProgressReport();

        public bool CheckAllFile { get; set; }
        public List<string> FileTypes { get; set; } = new List<string>();
    }
}
