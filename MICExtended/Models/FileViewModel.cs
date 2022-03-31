﻿using MICExtended.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MICExtended.Models
{
    public class FileViewModel
    {
        public string RootPath { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Extension { get; set;} = string.Empty;
        public long? Size { get; set; }


        public string Name { 
            get {
                return Path.GetFileName(FilePath);
            } 
        }

        public string NameWithoutExt {
            get {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        public string RelativePath {
            get {
                return Path.GetRelativePath(RootPath, FilePath);
            }
        }

        public string SizeDisplay { 
            get {
                return Size.HasValue ? Function.BytesToString(Size.Value) : string.Empty;
            } 
        }
    }
}