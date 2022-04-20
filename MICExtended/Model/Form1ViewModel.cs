using MICExtended.Abstraction;
using MICExtended.Common;
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
        public string DefaultDstDir { 
            get {
                if(string.IsNullOrEmpty(SrcDir)) return string.Empty;
                if(Compression.ReplaceOriginal) return SrcDir;
                return Path.Combine(SrcDir, Constant.Pathing.COMPRESSED);
            } 
        }

        public List<FileModel> SrcFiles { get; set; } = new List<FileModel>();
        public List<FileModel> DstFiles { get; set; } = new List<FileModel>();

        public ProgressReport ProgressReport { get; set; } = new ProgressReport();

        public CompressionCondition Compression { get; set; } = new CompressionCondition();
        public SelectionCondition Selection { get; set; } = new SelectionCondition();
    }
}
