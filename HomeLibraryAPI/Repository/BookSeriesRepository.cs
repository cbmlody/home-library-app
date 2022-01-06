using Entities.Models;
using Contracts.Repositories;
using Entities;

namespace Repository
{
    public class BookSeriesRepository : RepositoryBase<BookSeries>, IBookSeriesRepository
    {
        public BookSeriesRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }
    }
}
