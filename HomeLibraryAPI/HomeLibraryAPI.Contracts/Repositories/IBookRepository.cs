using HomeLibraryAPI.EF.Models;

namespace HomeLibraryAPI.Contracts.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book>, IRepositoryBaseAsync<Book>
    {
    }
}
