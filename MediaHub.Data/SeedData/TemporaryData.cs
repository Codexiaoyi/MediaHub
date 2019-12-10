using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaHub.Data.SeedData
{
    public static class TemporaryData
    {
        private static List<MediaHubUser> Users = new List<MediaHubUser>() { new MediaHubUser { UserName = "张老二", Password = "zhangzhiersb" }, new MediaHubUser { UserName = "王弟弟", Password = "wangdidisb" } };

        public static MediaHubUser GetUser(string userName)
        {
            return Users.FirstOrDefault(m => m.UserName.Equals(userName));
        }
    }
}
