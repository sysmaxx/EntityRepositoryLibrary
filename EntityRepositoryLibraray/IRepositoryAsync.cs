using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepositoryLibrary
{
    /// <summary>
    /// Represents async base functions for repositories 
    /// </summary>
    /// <typeparam name="TEntity">Modeltype</typeparam>
    public interface IRepositoryAsync<TEntity> where TEntity : class
    {

        /// <summary>
        /// Find an Entity with given Primary Key
        /// </summary>
        /// <param name="id">Primary Key Object</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The entity found or null</returns>
        Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>All entites</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>All entites based on the predicate</returns>
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns the only element of a sequence that satisfies a specified condition,
        ///  and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Single element that match the predicate function</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns the only element of a sequence that satisfies a specified condition,
        ///  and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Single element that match the predicate function or default(TSource) if no such element is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns the first element of a sequence that satisfies a specified condition,
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Single element that match the predicate function</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns the first element of a sequence that satisfies a specified condition
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>First element that match the predicate function or default(TSource) if no such element is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add an entity to Entitys tracking, it will be inserted to Database if DbContext.SaveChanges is called 
        /// </summary>
        /// <param name="entity">Entity to add to</param>
        /// <param name="cancellationToken"></param>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a sequenz of entities to Entitys tracking, they will be inserted to Database if DbContext.SaveChanges is called 
        /// </summary>
        /// <param name="entities">Entities to add</param>
        /// <param name="cancellationToken"></param>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove an entity from Entitys tracking, it will be deleted if DbContext.SaveChanges is called
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        /// <param name="cancellationToken"></param>
        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove a sequenz entities from Entitys tracking, it will be deleted if DbContext.SaveChanges is called
        /// </summary>
        /// <param name="entities">Entities to remove</param>
        /// <param name="cancellationToken"></param>
        Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    }
}
