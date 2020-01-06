using System;
using System.Collections.Generic;
using System.Text;

namespace MediaHub.Model
{
    public class FileInfo : BaseEntity
    {
        public string FileName { get; set; }

        public string Identifier { get; set; }

        public long TotalSize { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }
    }
}
