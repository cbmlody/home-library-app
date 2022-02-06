using HomeLibraryAPI.Contracts.Repositories;
using HomeLibraryAPI.EF;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Linq.Expressions;

namespace HomeLibraryAPI.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected LibraryContext LibraryContext { get; set; }

        protected RepositoryBase(LibraryContext libraryContext)
        {
            LibraryContext = libraryContext;
        }

        public IQueryable<TEntity> FindAll()
        {
            return LibraryContext.Set<TEntity>()
                .AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return LibraryContext.Set<TEntity>()
                .Where(expression)
                .AsNoTracking();
        }

        public void Create(TEntity entity)
        {
            LibraryContext.Set<TEntity>()
                .Add(entity);
        }

        public void Update(TEntity entity)
        {
            LibraryContext.Set<TEntity>()
                .Update(entity);
        }

        public void Delete(TEntity entity)
        {
            LibraryContext.Set<TEntity>()
                .Remove(entity);
        }
    }
}
