using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MediaHub.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly MyContext _myContext;

        public UserRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }

        //public async Task<MediaHubUser> QuaryUserById(Guid id)
        //{
        //    return await _myContext.MediaHubUsers
        //        .FirstOrDefaultAsync(f => f.Id == id);
        //}

        public async Task<User> QueryUserByAccount(string userAccount)
        {
            return await _myContext.Users
                .FirstOrDefaultAsync(f => f.UserAccount == userAccount);
        }
    }
}
