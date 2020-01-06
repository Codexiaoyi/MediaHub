using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MediaHub.Repository
{
    public class UserRepository : BaseRepository<MediaHubUser>, IUserRepository
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

        public async Task<MediaHubUser> QueryUserByName(string userName)
        {
            return await _myContext.MediaHubUsers
                .FirstOrDefaultAsync(f => f.UserName == userName);
        }
    }
}
