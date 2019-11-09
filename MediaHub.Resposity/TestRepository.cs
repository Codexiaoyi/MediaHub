using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using MediaHub.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaHub.Repository
{
    public class TestRepository : BaseRepository<TestModel>, ITestRepository
    {
        private readonly MyContext _context;

        public TestRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TestModel>> Query()
        {
            return await _context.TestModels.ToListAsync();
        }

        public async Task<TestModel> QueryById(Guid id)
        {
            return await _context.TestModels.Where(x => x.Id == id).FirstAsync();
        }

    }
}
