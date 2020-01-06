using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MediaHub.Repository
{
    public class FileRepository : BaseRepository<FileModel>, IFileRepository
    {
        private readonly MyContext _myContext;

        public FileRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }

        public async Task<List<FileModel>> QueryAllFileAsync()
        {
            return await _myContext.Files
                .ToListAsync();
        }

        //public async Task<FileInfo> QueryByIdAsync(Guid id)
        //{
        //    return await _myContext.Files
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}
