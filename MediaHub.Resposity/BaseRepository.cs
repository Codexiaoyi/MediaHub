using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MediaHub.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new
        ()
    {
        private readonly MyContext _context;

        public BaseRepository(MyContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 添加实体到数据库中
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>影响行数</returns>
        public async Task<int> AddAsync(TEntity model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 删除数据库中的某个实体
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>影响行数</returns>
        public async Task<int> DeleteAsync(TEntity model)
        {
            _context.Remove(model);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 查询数据库中的某个实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>实体</returns>
        public async Task<TEntity> QueryByIdAsync(Guid id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>影响行数</returns>
        public async Task<int> UpdateAsync(TEntity model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }
    }
}
