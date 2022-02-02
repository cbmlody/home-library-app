using Contracts;
using Contracts.Repositories;

using Entities;

using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly LibraryContext _libraryContext;
        private IAuthorRepository _author;
        private IBookRepository _book;
        private IBookSeriesRepository _bookSeries;
        private IBookshelveRepository _bookshelve;
        private IPublisherRepository _publisher;

        public IAuthorRepository Author => _author ??= new AuthorRepository(_libraryContext);
        public IBookRepository Book => _book ??= new BookRepository(_libraryContext);
        public IBookSeriesRepository BookSeries => _bookSeries ??= new BookSeriesRepository(_libraryContext);
        public IBookshelveRepository Bookshelve => _bookshelve ??= new BookshelveRepository(_libraryContext);
        public IPublisherRepository Publisher => _publisher ??= new PublisherReposiory(_libraryContext);

        public RepositoryWrapper(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task SaveAsync()
        {
            await _libraryContext.SaveChangesAsync();
        }
    }
}
