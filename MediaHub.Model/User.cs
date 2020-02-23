using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MediaHub.Model
{
    public class User : BaseEntity
    {
        public User()
        {
            Albums = new List<Album>();
        }

        /// <summary>
        /// 账号(唯一)
        /// </summary>
        [Required]
        public string UserAccount { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        public List<Album> Albums { get; set; }
    }
}
