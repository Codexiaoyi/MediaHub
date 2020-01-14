using AutoMapper;
using MediaHub.Model;
using MediaHub.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.Profiles
{
    public class FileModelProfile : Profile
    {
        public FileModelProfile()
        {
            CreateMap<FileModelViewModel, FileModel>()
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.TotalSize))
                .ForMember(dest => dest.ExtensionName, opt => opt.MapFrom(src => Path.GetExtension(src.FileName)));
        }
    }
}
