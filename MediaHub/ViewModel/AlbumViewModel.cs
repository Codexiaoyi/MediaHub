using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.ViewModel
{
    public class AlbumViewModel
    {
        /// <summary>
        /// 封面留言
        /// </summary>
        public string CoverMessage { get; set; }
        /// <summary>
        /// 外键,关联到某个用户
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 封面的图片
        /// </summary>
        public IFormFile CoverImage { get; set; }
    }
}
