using Contracts.Repositories;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IAuthorRepository Author { get; }
        IBookRepository Book { get ;}

        void Save();
    }
}
