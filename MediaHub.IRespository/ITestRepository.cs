using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.IRepository
{
    public interface ITestRepository : IBaseRepository<TestModel>
    {
        Task<List<TestModel>> Query();
        Task<TestModel> QueryById(Guid id);
    }
}
