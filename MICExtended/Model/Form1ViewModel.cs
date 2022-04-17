using MICExtended.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Model
{
    public class Form1ViewModel : IPersistable
    {
        public string SrcDir { get; set; } = string.Empty;
        public string DstDir { get; set; } = string.Empty;

        public List<FileModel> SrcFiles { get; set; } = new List<FileModel>();
        public List<FileModel> DstFiles { get; set; } = new List<FileModel>();

        public ProgressReport ProgressReport { get; set; } = new ProgressReport();

        public CompressionCondition Compression { get; set; } = new CompressionCondition();
        public SelectionCondition Selection { get; set; } = new SelectionCondition();
    }
}
