using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.ViewModel
{
    public class FileModelViewModel
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public long TotalSize { get; set; }

        [Required]
        public Guid AlbumId { get; set; }

        public string Type { get; set; }
    }
}
