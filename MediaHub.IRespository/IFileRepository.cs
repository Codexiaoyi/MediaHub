using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.IRepository
{
    public interface IFileRepository : IBaseRepository<FileModel>
    {
        /// <summary>
        /// 获取所有文件信息
        /// </summary>
        /// <returns></returns>
        Task<List<FileModel>> QueryAllFileAsync();

        Task<FileModel> QueryByIdAsync(Guid id);
    }
}
