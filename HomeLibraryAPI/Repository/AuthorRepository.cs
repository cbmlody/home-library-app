using Contracts.Repositories;

using Entities;
using Entities.Models;

namespace Repository
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }
    }
}
