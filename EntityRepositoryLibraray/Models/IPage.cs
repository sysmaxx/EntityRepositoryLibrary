using System.Collections.Generic;

namespace EntityRepositoryLibrary.Models
{
    /// <summary>
    /// Response of pageable-request
    /// </summary>
    /// <typeparam name="TEntity">Modeltype</typeparam>
    public interface IPage<TEntity> where TEntity : class
    {
        /// <summary>
        /// Selected items in this page
        /// </summary>
        public IEnumerable<TEntity> Items { get; init; }
        /// <summary>
        /// Item count in page
        /// </summary>
        public int ItemsInPage { get; init; }
        /// <summary>
        /// Total count of all items 
        /// </summary>
        /// 
        public int TotalItems { get; init; }
        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; init; }
        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; init; }
        /// <summary>
        /// Selected page size
        /// </summary>
        public int PageSize { get; init; }
    }
}
