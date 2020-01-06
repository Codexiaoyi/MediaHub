using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaHub.Repository
{
    public class FileChunkRepository : BaseRepository<FileChunk>, IFileChunkRepository
    {
        private readonly MyContext _myContext;

        public FileChunkRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }
    }
}
