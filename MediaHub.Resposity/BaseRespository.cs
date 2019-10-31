using MediaHub.Data;
using MediaHub.IRespository;
using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.Respository
{
    public class BaseRespository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new
        ()
    {
        private readonly MyContext _context;

        public BaseRespository(MyContext context)
        {
            _context = context;
        }
        public Task<bool> Add(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIds(object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> QueryById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryByIds(object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
