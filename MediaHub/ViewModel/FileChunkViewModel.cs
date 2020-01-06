using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.ViewModel
{
    public class FileChunkViewModel
    {
        //[Required]
        //public long Id { get; set; }
        /// <summary>
        /// 当前文件块，1开始
        /// </summary>
        [Required]
        public int ChunkNumber { get; set; }
        /// <summary>
        /// 分块大小
        /// </summary>
        [Required]
        public long ChunkSize { get; set; }
        /// <summary>
        /// 当前分块大小
        /// </summary>
        [Required]
        public long CurrentChunkSize { get; set; }
        /// <summary>
        /// 总大小
        /// </summary>
        [Required]
        public long TotalSize { get; set; }
        /// <summary>
        /// 文件标识
        /// </summary>
        [Required]
        public string Identifier { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        public string Filename { get; set; }
        /// <summary>
        /// 相对路径
        /// </summary>
        [Required]
        public string RelativePath { get; set; }
        /// <summary>
        /// 总块数
        /// </summary>
        [Required]
        public int TotalChunks { get; set; }

        public IFormFile file { get; set; }
    }
}
