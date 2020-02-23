using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace MediaHub.Model
{
    public class Album : BaseEntity
    {
        public Album()
        {
            Files = new List<FileModel>();
        }
        /// <summary>
        /// 封面留言
        /// </summary>
        public string CoverMessage { get; set; }
        /// <summary>
        /// 封面地址
        /// </summary>
        public string CoverUrl { get; set; }
        /// <summary>
        /// 外键,关联到某个用户
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 属于某个用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 对应多个文件（暂时包含图片和视频）
        /// </summary>
        public List<FileModel> Files { get; set; }
    }
}
