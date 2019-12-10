using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using MediaHub.Respository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaHub.Repository
{
    public class UserRepository : BaseRepository<MediaHubUser>, IUserRepository
    {
        private readonly MyContext _myContext;

        public UserRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }

    }
}
