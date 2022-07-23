using HomeLibraryAPI.EF.Models;

namespace HomeLibraryAPI.Contracts.Repositories
{
    public interface IAuthorRepository : IRepositoryBase<Author>, IRepositoryBaseAsync<Author>
    {
    }
}
