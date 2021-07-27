using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepositoryLibrary
{
    public interface IPageableRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetPage(int pageNumber, int pageSize);
        Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> GetPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        Task<IEnumerable<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}
