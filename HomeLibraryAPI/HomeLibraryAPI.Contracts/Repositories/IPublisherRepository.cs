using HomeLibraryAPI.EF.Models;

namespace HomeLibraryAPI.Contracts.Repositories
{
    public interface IPublisherRepository : IRepositoryBase<Publisher>, IRepositoryBaseAsync<Publisher>
    {
    }
}
