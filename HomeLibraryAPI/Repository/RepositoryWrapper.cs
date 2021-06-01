using Contracts;
using Contracts.Repositories;

using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly LibraryContext _libraryContext;
        private IAuthorRepository _author;
        private IBookRepository _book;

        public IAuthorRepository Author
        {
            get
            {
                if (_author == null)
                    _author = new AuthorRepository(_libraryContext);

                return _author;
            }
        }
        public IBookRepository Book
        {
            get
            {
                if (_book == null)
                    _book = new BookRepository(_libraryContext);


                        return _book;
            }
        }

        public RepositoryWrapper(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public void Save()
        {
            _libraryContext.SaveChanges();
        }
    }
}
