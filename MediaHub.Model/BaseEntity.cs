using System;
using System.ComponentModel.DataAnnotations;

namespace MediaHub.Model
{
    /// <summary>
    /// 作为所有实体的基类
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string CreateDate { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");//精确到秒
        }
    }
}
