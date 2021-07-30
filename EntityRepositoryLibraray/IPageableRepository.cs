using EntityRepositoryLibrary.Models;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepositoryLibrary
{
    /// <summary>
    /// Provides pageable requests
    /// </summary>
    /// <typeparam name="TEntity">Databasemodel</typeparam>
    public interface IPageableRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get a paged sequenz of entities
        /// </summary>
        /// <param name="pageNumber">Current Pagenumber</param>
        /// <param name="pageSize">Pagesize</param>
        /// <returns>Paged sequenz</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IPage<TEntity> GetPage(int pageNumber, int pageSize);

        /// <summary>
        /// Get a paged sequenz of entities async
        /// </summary>
        /// <param name="pageNumber">Current Pagenumber</param>
        /// <param name="pageSize">Pagesize</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Paged sequenz</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Task<IPage<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a paged sequence that satisfies a specified condition
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="pageNumber">Current Pagenumber</param>
        /// <param name="pageSize">Pagesize</param>
        /// <returns>Paged, filtered sequenz</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IPage<TEntity> GetPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);

        /// <summary>
        /// Get a paged sequence async that satisfies a specified condition
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="pageNumber">Current Pagenumber</param>
        /// <param name="pageSize">Pagesize</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Paged, filtered sequenz</returns>
        /// <exception cref="ArgumentNullException"></exception>
        ///         /// <exception cref="ArgumentException"></exception>
        Task<IPage<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}
