using HomeLibraryAPI.EF.Models;

namespace HomeLibraryAPI.Contracts.Repositories
{
    public interface IBookshelveRepository : IRepositoryBase<Bookshelve>, IRepositoryBaseAsync<Bookshelve>
    {
    }
}
