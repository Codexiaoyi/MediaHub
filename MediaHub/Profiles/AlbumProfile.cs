using AutoMapper;
using MediaHub.Model;
using MediaHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, AlbumViewModel>();
            CreateMap<AlbumViewModel, Album>();
        }
    }
}
