using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.Repository
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        private readonly MyContext _myContext;

        public AlbumRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }

        /// <summary>
        /// 返回所有相册数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<Album>> QueryAlbumsByUserIdAsync(Guid userId)
        {
            return await _myContext.Albums.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
