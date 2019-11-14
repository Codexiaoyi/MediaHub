using MediaHub.Data;
using MediaHub.IRepository;
using MediaHub.Model;
using MediaHub.Respository;

namespace MediaHub.Repository
{
    public class FileRepository : BaseRepository<FileModel>, IFileRepository
    {
        private readonly MyContext _myContext;

        public FileRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }
    }
}
