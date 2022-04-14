using MICExtended.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Models
{
    public class ConfigurationModel
    {
        public List<string> FileTypes { get; set; } = new List<string>() {
            Constant.Extension.JPEG, Constant.Extension.JPG, Constant.Extension.PNG
        };
    }
}
