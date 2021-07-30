using EntityRepositoryLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepositoryLibrary
{
    /// <inheritdoc/>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        /// <summary>
        /// Create an instance of repository
        /// </summary>
        /// <param name="context">Entity DbContext</param>
        public Repository(DbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public TContext Context<TContext>() where TContext : DbContext => _context as TContext;

        #region Synchronous

        /// <inheritdoc/>
        public TEntity Get(object id)
            => _context.Set<TEntity>().Find(id);

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAll()
            => _context.Set<TEntity>().ToList();

        /// <inheritdoc/>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().Where(predicate).ToList();

        /// <inheritdoc/>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().Single(predicate);

        /// <inheritdoc/>
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().SingleOrDefault(predicate);


        /// <inheritdoc/>
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().First(predicate);


        /// <inheritdoc/>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().FirstOrDefault(predicate);

        /// <inheritdoc/>
        public void Add(TEntity entity)
            => _context.Set<TEntity>().Add(entity);

        /// <inheritdoc/>
        public void AddRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().AddRange(entities);

        /// <inheritdoc/>
        public void Remove(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        /// <inheritdoc/>
        public void RemoveRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().RemoveRange(entities);



        #endregion

        #region Asynchronous
        /// <inheritdoc/>
        public async Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().ToListAsync(cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().SingleAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FirstAsync(predicate, cancellationToken);

        /// <inheritdoc/>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default) => await Task.Run(()
            => _context.Set<TEntity>().Remove(entity), cancellationToken).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => await Task.Run(() => _context.Set<TEntity>().RemoveRange(entities), cancellationToken).ConfigureAwait(false);


        #endregion

        #region Pageable

        /// <inheritdoc/>
        public IPage<TEntity> GetPage(int pageNumber, int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentException($"{nameof(pageSize)} can not be less or equals then 0", nameof(pageSize));

            if (pageNumber < 0)
                throw new ArgumentException($"{nameof(pageNumber)} can not be less then 0", nameof(pageNumber));

            var totalEntires = _context.Set<TEntity>().Count();
            var totalPages = totalEntires / pageSize;

            if (totalPages > pageNumber)
                pageNumber = totalPages;

            var items = _context.Set<TEntity>()
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToList();

            return new Page<TEntity>
            {
                Items = items,
                ItemsInPage = items.Count,
                TotalItems = totalEntires,
                PageSize = pageSize,
                TotalPages = totalEntires / pageSize,
                CurrentPage = pageNumber
            };
        }

        /// <inheritdoc/>
        public IPage<TEntity> GetPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentException($"{nameof(pageSize)} can not be less or equals then 0", nameof(pageSize));

            if (pageNumber < 0)
                throw new ArgumentException($"{nameof(pageNumber)} can not be less then 0", nameof(pageNumber));

            var totalEntires = _context.Set<TEntity>().Count(predicate);
            var totalPages = totalEntires / pageSize;

            if (totalPages > pageNumber)
                pageNumber = totalPages;

            var items = _context.Set<TEntity>()
                .Where(predicate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToList();

            return new Page<TEntity>
            {
                Items = items,
                ItemsInPage = items.Count,
                TotalItems = totalEntires,
                PageSize = pageSize,
                TotalPages = totalEntires / pageSize,
                CurrentPage = pageNumber
            };
        }

        /// <inheritdoc/>
        public async Task<IPage<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageSize <= 0)
                throw new ArgumentException($"{nameof(pageSize)} can not be less or equals then 0", nameof(pageSize));

            if (pageNumber < 0)
                throw new ArgumentException($"{nameof(pageNumber)} can not be less then 0", nameof(pageNumber));

            var totalEntires = await _context.Set<TEntity>().CountAsync(cancellationToken).ConfigureAwait(false);
            var totalPages = totalEntires / pageSize;

            if (totalPages > pageNumber)
                pageNumber = totalPages;

            var items = await _context.Set<TEntity>()
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new Page<TEntity>
            {
                Items = items,
                ItemsInPage = items.Count,
                TotalItems = totalEntires,
                PageSize = pageSize,
                TotalPages = totalEntires / pageSize,
                CurrentPage = pageNumber
            };
        }

        /// <inheritdoc/>
        public async Task<IPage<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageSize <= 0)
                throw new ArgumentException($"{nameof(pageSize)} can not be less or equals then 0", nameof(pageSize));

            if (pageNumber < 0)
                throw new ArgumentException($"{nameof(pageNumber)} can not be less then 0", nameof(pageNumber));

            var totalEntires = await _context.Set<TEntity>().CountAsync(predicate, cancellationToken).ConfigureAwait(false);
            var totalPages = totalEntires / pageSize;

            if (totalPages > pageNumber)
                pageNumber = totalPages;

            var items = await _context.Set<TEntity>()
                .Where(predicate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new Page<TEntity>
            {
                Items = items,
                ItemsInPage = items.Count,
                TotalItems = totalEntires,
                PageSize = pageSize,
                TotalPages = totalEntires / pageSize,
                CurrentPage = pageNumber
            };
        }

        #endregion
    }
}
