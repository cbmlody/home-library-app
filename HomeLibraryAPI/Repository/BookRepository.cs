using Contracts.Repositories;

using Entities;
using Entities.Models;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }
    }
}
