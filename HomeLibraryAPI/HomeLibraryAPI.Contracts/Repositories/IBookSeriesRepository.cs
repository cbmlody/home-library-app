using HomeLibraryAPI.EF.Models;

namespace HomeLibraryAPI.Contracts.Repositories
{
    public interface IBookSeriesRepository : IRepositoryBase<BookSeries>, IRepositoryBaseAsync<BookSeries>
    {
    }
}
