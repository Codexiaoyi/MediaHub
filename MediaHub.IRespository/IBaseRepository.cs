using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.IRespository
{
    /// <summary>
    /// 泛型仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Query查询操作
        Task<List<TEntity>> Query();
        Task<TEntity> QueryById(object id);
        Task<List<TEntity>> QueryByIds(object[] ids);
        #endregion
        #region Add添加操作
        Task<bool> Add(TEntity model);
        #endregion
        #region Update更新操作
        Task<bool> Update(TEntity model);
        #endregion
        #region Delete删除操作
        Task<bool> Delete(TEntity model);
        Task<bool> DeleteById(object id);
        Task<bool> DeleteByIds(object[] ids);
        #endregion
    }
}
