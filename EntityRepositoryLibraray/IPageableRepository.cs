using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityRepositoryLibrary
{
    public interface IPageableRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetPage(int pageNumber, int pageSize);
        Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize);
    }
}
