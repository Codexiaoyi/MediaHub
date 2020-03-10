using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.IRepository
{
    public interface IAlbumRepository : IBaseRepository<Album>
    {
        /// <summary>
        /// 获取所有相册信息
        /// </summary>
        /// <returns></returns>
        Task<List<Album>> QueryAlbumsByUserIdAsync(Guid userId);
        /// <summary>
        /// 获取相册信息
        /// </summary>
        /// <param name="albumId">相册Id</param>
        /// <returns>相册</returns>
        Task<Album> QueryAlbumById(Guid albumId);
    }
}
