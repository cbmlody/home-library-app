using Contracts.Repositories;

using Entities;
using Entities.Models;

namespace Repository
{
    public class PublisherReposiory : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherReposiory(LibraryContext libraryContext) : base(libraryContext)
        {
        }
    }
}
