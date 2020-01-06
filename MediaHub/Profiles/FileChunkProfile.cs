using AutoMapper;
using MediaHub.Model;
using MediaHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.Profiles
{
    public class FileChunkProfile : Profile
    {
        public FileChunkProfile()
        {
            CreateMap<FileChunk, FileChunkViewModel>();
            CreateMap<FileChunkViewModel, FileChunk>();
        }
    }
}
