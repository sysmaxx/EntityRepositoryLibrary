using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepositoryLibrary
{
    /// <summary>
    /// Base Repository  
    /// </summary>
    /// <typeparam name="TEntity">The type of the elements of source entity model</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        /// <summary>
        /// Create an instance of repository
        /// </summary>
        /// <param name="context">Entity DbContext</param>
        public Repository(DbContext context)
        {
            _context = context;
        }

        #region Synchronous
        public TContext Context<TContext>() where TContext : DbContext => _context as TContext;

        /// <summary>
        /// Find an Entity with given Primary Key
        /// </summary>
        /// <param name="id">Primary Key Object</param>
        /// <returns>The entity found or null</returns>
        public TEntity Get(object id)
            => _context.Set<TEntity>().Find(id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>All entites</returns>
        public IEnumerable<TEntity> GetAll()
            => _context.Set<TEntity>().ToList();

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>All entites based on the predicate</returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().Where(predicate).ToList();

        /// <summary>
        ///  Returns the only element of a sequence that satisfies a specified condition,
        ///  and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>Single element that match the predicate function</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().Single(predicate);

        /// <summary>
        ///  Returns the only element of a sequence that satisfies a specified condition,
        ///  and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>Single element that match the predicate function or default(TSource) if no such element is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().SingleOrDefault(predicate);


        /// <summary>
        ///  Returns the first element of a sequence that satisfies a specified condition,
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>Single element that match the predicate function</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().First(predicate);


        /// <summary>
        ///  Returns the first element of a sequence that satisfies a specified condition
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>First element that match the predicate function or default(TSource) if no such element is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().FirstOrDefault(predicate);

        /// <summary>
        /// Add an entity to Entitys tracking, it will be inserted to Database if DbContext.SaveChanges is called 
        /// </summary>
        /// <param name="entity">Entity to add to</param>
        public void Add(TEntity entity)
            => _context.Set<TEntity>().Add(entity);

        /// <summary>
        /// Add a sequenz of entities to Entitys tracking, they will be inserted to Database if DbContext.SaveChanges is called 
        /// </summary>
        /// <param name="entities">Entities to add</param>
        public void AddRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().AddRange(entities);

        /// <summary>
        /// Remove an entity from Entitys tracking, it will be deleted if DbContext.SaveChanges is called
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public void Remove(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        /// <summary>
        /// Remove a sequenz entities from Entitys tracking, it will be deleted if DbContext.SaveChanges is called
        /// </summary>
        /// <param name="entity">Entities to remove</param>
        public void RemoveRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().RemoveRange(entities);

        /// <summary>
        /// Get a paged sequenz of entities
        /// </summary>
        /// <param name="pageNumber">Current Pagenumber</param>
        /// <param name="pageSize">Pagesize</param>
        /// <returns>Paged sequenz</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> GetPage(int pageNumber, int pageSize)
            => _context.Set<TEntity>().Skip(pageNumber * pageSize).Take(pageSize).ToList();

        /// <summary>
        /// Get a paged sequence that satisfies a specified condition
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="pageNumber">Current Pagenumber</param>
        /// <param name="pageSize">Pagesize</param>
        /// <returns>Paged, filtered sequenz</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> GetPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
            => _context.Set<TEntity>()
            .Where(predicate)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToList();

        #endregion

        #region Asynchronous
        /// <summary>
        /// Find an Entity with given Primary Key
        /// </summary>
        /// <param name="id">Primary Key Object</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The entity found or null</returns>
        public async Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>All entites</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().ToListAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>All entites based on the predicate</returns>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken).ConfigureAwait(false);


        /// <summary>
        ///  Returns the only element of a sequence that satisfies a specified condition,
        ///  and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Single element that match the predicate function</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().SingleAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);

        /// <summary>
        ///  Returns the only element of a sequence that satisfies a specified condition,
        ///  and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Single element that match the predicate function or default(TSource) if no such element is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);

        /// <summary>
        ///  Returns the first element of a sequence that satisfies a specified condition,
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Single element that match the predicate function</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FirstAsync(predicate, cancellationToken);

        /// <summary>
        ///  Returns the first element of a sequence that satisfies a specified condition
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// 
        /// <returns>First element that match the predicate function or default(TSource) if no such element is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Add an entity to Entitys tracking, it will be inserted to Database if DbContext.SaveChanges is called 
        /// </summary>
        /// <param name="entity">Entity to add to</param>
        /// <param name="cancellationToken"></param>
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Add a sequenz of entities to Entitys tracking, they will be inserted to Database if DbContext.SaveChanges is called 
        /// </summary>
        /// <param name="entities">Entities to add</param>
        /// <param name="cancellationToken"></param>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Remove an entity from Entitys tracking, it will be deleted if DbContext.SaveChanges is called
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        /// <param name="cancellationToken"></param>
        public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default) => await Task.Run(()
            => _context.Set<TEntity>().Remove(entity), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Remove a sequenz entities from Entitys tracking, it will be deleted if DbContext.SaveChanges is called
        /// </summary>
        /// <param name="entity">Entities to remove</param>
        /// <param name="cancellationToken"></param>
        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => await Task.Run(() => _context.Set<TEntity>().RemoveRange(entities), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Get a paged sequenz of entities
        /// </summary>
        /// <param name="pageNumber">Current Pagenumber</param>
        /// <param name="pageSize">Pagesize</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Paged sequenz</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().Skip(pageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Get a paged sequence that satisfies a specified condition
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <param name="pageNumber">Current Pagenumber</param>
        /// <param name="pageSize">Pagesize</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Paged, filtered sequenz</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>()
            .Where(predicate)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        #endregion
    }
}
