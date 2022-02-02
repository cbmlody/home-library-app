using Contracts.Repositories;

using System.Threading.Tasks;

namespace Contracts
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
