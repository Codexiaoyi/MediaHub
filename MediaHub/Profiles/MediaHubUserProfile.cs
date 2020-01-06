using AutoMapper;
using MediaHub.Model;
using MediaHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.Profiles
{
    /// <summary>
    /// 用户模型与用户视图模型映射
    /// </summary>
    public class MediaHubUserProfile :Profile
    {
        public MediaHubUserProfile()
        {
            //这里里面类型如果是一样就会直接将源模型的值给目标模型
            CreateMap<MediaHubUser, MediaHubUserViewModel>();
            CreateMap<MediaHubUserViewModel, MediaHubUser>();
        }
    }
}
