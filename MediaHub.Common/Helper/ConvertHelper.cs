using System;
using System.Collections.Generic;
using System.Text;

namespace MediaHub.Common
{
    /// <summary>
    /// 技术支持：博客园.老张的哲学
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// obj转int
        /// </summary>
        public static int ObjToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        /// <summary>
        /// obj转string
        /// </summary>
        public static string ObjToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }
    }
}
