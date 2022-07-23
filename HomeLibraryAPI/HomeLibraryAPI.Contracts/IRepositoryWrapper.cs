using HomeLibraryAPI.Contracts.Repositories;

using System.Threading.Tasks;

namespace HomeLibraryAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        IAuthorRepository Author { get; }
        IBookRepository Book { get; }
        IBookSeriesRepository BookSeries { get; }
        IBookshelveRepository Bookshelve { get; }
        IPublisherRepository Publisher { get; }

        Task SaveAsync();
    }
}
