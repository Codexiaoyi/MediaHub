using MediaHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.IRepository
{
    public interface IUserRepository : IBaseRepository<MediaHubUser>
    {
        Task<MediaHubUser> QuaryUserById(Guid id);
        Task<MediaHubUser> QuaryUserByName(string userName);
    }
}
