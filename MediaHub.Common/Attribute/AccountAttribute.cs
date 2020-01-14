using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace MediaHub.Common.Attribute
{
    /// <summary>
    /// 是否为MediaHub的用户账号
    /// </summary>
    public class AccountAttribute : ValidationAttribute
    {
        public AccountAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            Regex regex = new Regex("^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
            return regex.IsMatch(value.ToString()) ? true : false;
        }
    }
}
