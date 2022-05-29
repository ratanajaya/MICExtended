using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MICExtended.Model
{
    public class AppSettingJson
    {
        public string[] AllowedExtensions { get; set; } = new string[0];
        public string[] AllowedRawExtensions { get; set; } = new string[0];
    }
}
