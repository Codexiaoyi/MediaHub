using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaHub.Data.SeedData
{
    public static class TemporaryData
    {
        private static List<User> Users = new List<User>() { new User { UserName = "张老二", Password = "zhangzhiersb" }, new User { UserName = "王弟弟", Password = "wangdidisb" } };

        public static User GetUser(string userName)
        {
            return Users.FirstOrDefault(m => m.UserName.Equals(userName));
        }
    }
}
