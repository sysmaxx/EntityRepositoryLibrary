using System.Collections.Generic;

namespace EntityRepositoryLibrary.Models
{
    /// <inheritdoc/>
    public class Page<TEntity> : IPage<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public IEnumerable<TEntity> Items { get; init; }
        /// <inheritdoc/>
        public int ItemsInPage { get; init; }
        /// <inheritdoc/>
        public int TotalItems { get; init; }
        /// <inheritdoc/>
        public int CurrentPage { get; init; }
        /// <inheritdoc/>
        public int TotalPages { get; init; }
        /// <inheritdoc/>
        public int PageSize { get; init; }
    }
}
