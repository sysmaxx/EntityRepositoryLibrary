using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityRepositoryLibrary
{
    /// <summary>
    /// Represents sync base functions for repositories 
    /// </summary>
    /// <typeparam name="TEntity">Modeltype</typeparam>
    public interface IRepositorySync<TEntity>  where TEntity : class
    {

        /// <summary>
        /// Find an Entity with given Primary Key
        /// </summary>
        /// <param name="id">Primary Key Object</param>
        /// <returns>The entity found or null</returns>
        TEntity Get(object id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>All entites</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>All entites based on the predicate</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///  Returns the only element of a sequence that satisfies a specified condition,
        ///  and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>Single element that match the predicate function</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///  Returns the only element of a sequence that satisfies a specified condition,
        ///  and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>Single element that match the predicate function or default(TSource) if no such element is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///  Returns the first element of a sequence that satisfies a specified condition,
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>Single element that match the predicate function</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        TEntity First(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///  Returns the first element of a sequence that satisfies a specified condition
        /// </summary>
        /// <param name="predicate">Predicate Function</param>
        /// <returns>First element that match the predicate function or default(TSource) if no such element is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Add an entity to Entitys tracking, it will be inserted to Database if DbContext.SaveChanges is called 
        /// </summary>
        /// <param name="entity">Entity to add to</param>
        void Add(TEntity entity);

        /// <summary>
        /// Add a sequenz of entities to Entitys tracking, they will be inserted to Database if DbContext.SaveChanges is called 
        /// </summary>
        /// <param name="entities">Entities to add</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove an entity from Entitys tracking, it will be deleted if DbContext.SaveChanges is called
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove a sequenz entities from Entitys tracking, it will be deleted if DbContext.SaveChanges is called
        /// </summary>
        /// <param name="entities">Entities to remove</param>
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
