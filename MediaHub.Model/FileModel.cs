using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MediaHub.Model
{
    public class FileModel : BaseEntity
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string ExtensionName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }

        public Guid AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
