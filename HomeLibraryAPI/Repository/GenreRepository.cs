using Contracts.Repositories;

using Entities;
using Entities.Models;

namespace Repository
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }
    }
}
