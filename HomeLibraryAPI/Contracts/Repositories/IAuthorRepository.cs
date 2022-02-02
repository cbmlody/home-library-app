using Entities.Models;

namespace Contracts.Repositories
{
    public interface IAuthorRepository : IRepositoryBase<Author>, IRepositoryBaseAsync<Author>
    {
    }
}
