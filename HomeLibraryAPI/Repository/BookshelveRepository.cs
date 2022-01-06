using Contracts.Repositories;

using Entities;
using Entities.Models;

namespace Repository
{
    public class BookshelveRepository : RepositoryBase<Bookshelve>, IBookshelveRepository
    {
        public BookshelveRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }
    }
}
