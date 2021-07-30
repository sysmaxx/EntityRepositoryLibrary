using Microsoft.EntityFrameworkCore;

namespace EntityRepositoryLibrary
{
    /// <summary>
    /// Represents the base-repository
    /// </summary>
    /// <typeparam name="TEntity">Databasemodel</typeparam>
    public interface IRepository<TEntity> : 
        IRepositorySync<TEntity>, 
        IRepositoryAsync<TEntity>,
        IPageableRepository<TEntity>
        where TEntity : class
    {

        /// <summary>
        /// Return the DbContaxt
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns>Casted DbContext</returns>
        TContext Context<TContext>() where TContext : DbContext;
    }
}
