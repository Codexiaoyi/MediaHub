using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.IRepository
{
    /// <summary>
    /// 泛型仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Add添加操作
        Task<int> AddAsync(TEntity model);
        #endregion
        #region Update更新操作
        Task<int> UpdateAsync(TEntity model);
        #endregion
        #region Delete删除操作
        Task<int> DeleteAsync(TEntity model);
        //Task<bool> DeleteById(object id);
        //Task<bool> DeleteByIds(object[] ids);
        #endregion
        #region Find查询操作
        Task<TEntity> QueryByIdAsync(Guid id);
        #endregion
    }
}
