using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepositoryLibrary
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public TContext Context<TContext>() where TContext : DbContext => _context as TContext;

        public TEntity Get(object id)
            => _context.Set<TEntity>().Find(id);
        public IEnumerable<TEntity> GetAll()
            => _context.Set<TEntity>().ToList();
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().Where(predicate).ToList();

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().Single(predicate);
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().SingleOrDefault(predicate);
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().FirstOrDefault(predicate);

        public void Add(TEntity entity)
            => _context.Set<TEntity>().Add(entity);
        public void AddRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().AddRange(entities);

        public void Remove(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().RemoveRange(entities);


        public async Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().ToListAsync(cancellationToken).ConfigureAwait(false);
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken).ConfigureAwait(false);

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().SingleAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken).ConfigureAwait(false);

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

        public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default) => await Task.Run(()
            => _context.Set<TEntity>().Remove(entity), cancellationToken).ConfigureAwait(false);
        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => await Task.Run(() => _context.Set<TEntity>().RemoveRange(entities), cancellationToken).ConfigureAwait(false);


        public IEnumerable<TEntity> GetPage(int pageNumber, int pageSize)
            => _context.Set<TEntity>().Skip(pageNumber * pageSize).Take(pageSize).ToList();
        public async Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize) 
            => await _context.Set<TEntity>().Skip(pageNumber * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);


    }
}
