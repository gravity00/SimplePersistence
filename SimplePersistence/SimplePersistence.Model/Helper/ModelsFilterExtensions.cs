#region License
// The MIT License (MIT)
// Copyright (c) 2015 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion
namespace SimplePersistence.Model.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods that can be used to filter models
    /// </summary>
    public static class ModelsFilterExtensions
    {
        #region With Id

        /// <summary>
        /// Filters a given <see cref="IQueryable{T}"/> entities using the updatedAfter Id
        /// </summary>
        /// <param name="entities">The entities to be filtered</param>
        /// <param name="id">The updatedAfter to be searched for</param>
        /// <typeparam name="TEntity">The updatedAfter type</typeparam>
        /// <typeparam name="TId">The updatedAfter type</typeparam>
        /// <returns>The entities with the filter applied</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryable<TEntity> WithId<TEntity, TId>(this IQueryable<TEntity> entities, TId id)
            where TEntity : IEntity<TId> 
            where TId : IEquatable<TId>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Where(e => e.Id.Equals(id));
        }

        /// <summary>
        /// Filters a given <see cref="IEnumerable{T}"/> entities using the updatedAfter Id
        /// </summary>
        /// <param name="entities">The entities to be filtered</param>
        /// <param name="id">The updatedAfter to be searched for</param>
        /// <typeparam name="TEntity">The updatedAfter type</typeparam>
        /// <typeparam name="TId">The updatedAfter type</typeparam>
        /// <returns>The entities with the filter applied</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<TEntity> WithId<TEntity, TId>(this IEnumerable<TEntity> entities, TId id)
            where TEntity : IEntity<TId>
            where TId : IEquatable<TId>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Where(e => e.Id.Equals(id));
        }

        #endregion

        #region Created After

        /// <summary>
        /// Filters a given collection of <see cref="IHaveCreatedMeta{TCreatedBy}"/> that where
        /// created after a given date
        /// </summary>
        /// <param name="entities">The collection to be filtered</param>
        /// <param name="createdAfter">The date to be compared</param>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <typeparam name="TCreatedBy">The created by type</typeparam>
        /// <returns>The filtered collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryable<TEntity> CreatedAfter<TEntity, TCreatedBy>(this IQueryable<TEntity> entities, DateTimeOffset createdAfter)
            where TEntity : IHaveCreatedMeta<TCreatedBy>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Where(e => e.CreatedOn > createdAfter);
        }

        /// <summary>
        /// Filters a given collection of <see cref="IHaveCreatedMeta{TCreatedBy}"/> that where
        /// created after a given date
        /// </summary>
        /// <param name="entities">The collection to be filtered</param>
        /// <param name="createdAfter">The date to be compared</param>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <typeparam name="TCreatedBy">The created by type</typeparam>
        /// <returns>The filtered collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<TEntity> CreatedAfter<TEntity, TCreatedBy>(this IEnumerable<TEntity> entities, DateTimeOffset createdAfter)
            where TEntity : IHaveCreatedMeta<TCreatedBy>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Where(e => e.CreatedOn > createdAfter);
        }

        #endregion

        #region Updated After

        /// <summary>
        /// Filters a given collection of <see cref="IHaveUpdatedMeta{TUpdatedBy}"/> that where
        /// updated after a given date
        /// </summary>
        /// <param name="entities">The collection to be filtered</param>
        /// <param name="updatedAfter">The date to be compared</param>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <typeparam name="TUpdatedBy">The updated by type</typeparam>
        /// <returns>The filtered collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryable<TEntity> UpdatedAfter<TEntity, TUpdatedBy>(this IQueryable<TEntity> entities, DateTimeOffset updatedAfter)
            where TEntity : IHaveUpdatedMeta<TUpdatedBy>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Where(e => e.UpdatedOn > updatedAfter);
        }

        /// <summary>
        /// Filters a given collection of <see cref="IHaveUpdatedMeta{TUpdatedBy}"/> that where
        /// updated after a given date
        /// </summary>
        /// <param name="entities">The collection to be filtered</param>
        /// <param name="updatedAfter">The date to be compared</param>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <typeparam name="TCreatedBy">The created by type</typeparam>
        /// <returns>The filtered collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<TEntity> UpdatedAfter<TEntity, TCreatedBy>(this IEnumerable<TEntity> entities, DateTimeOffset updatedAfter)
            where TEntity : IHaveUpdatedMeta<TCreatedBy>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Where(e => e.UpdatedOn > updatedAfter);
        }

        #endregion

        #region Deleted

        /// <summary>
        /// Filters a given collection of <see cref="IHaveDeletedMeta{TDeletedBy}"/> by their deleted state
        /// </summary>
        /// <param name="entities">The collection to be filtered</param>
        /// <param name="deleted">If the entities must be deleted. By default it is false.</param>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <typeparam name="TDeletedBy">The deleted by type</typeparam>
        /// <returns>The filtered collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryable<TEntity> Deleted<TEntity, TDeletedBy>(this IQueryable<TEntity> entities, bool deleted = false)
            where TEntity : IHaveDeletedMeta<TDeletedBy>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return deleted ? entities.Where(e => e.DeletedOn != null) : entities.Where(e => e.DeletedOn == null);
        }

        /// <summary>
        /// Filters a given collection of <see cref="IHaveDeletedMeta{TDeletedBy}"/> by their deleted state
        /// </summary>
        /// <param name="entities">The collection to be filtered</param>
        /// <param name="deleted">If the entities must be deleted. By default it is false.</param>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <typeparam name="TDeletedBy">The deleted by type</typeparam>
        /// <returns>The filtered collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<TEntity> Deleted<TEntity, TDeletedBy>(this IEnumerable<TEntity> entities, bool deleted = false)
            where TEntity : IHaveDeletedMeta<TDeletedBy>
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return deleted ? entities.Where(e => e.DeletedOn != null) : entities.Where(e => e.DeletedOn == null);
        }

        /// <summary>
        /// Filters a given collection of <see cref="IHaveSoftDelete"/> by their deleted state
        /// </summary>
        /// <param name="entities">The collection to be filtered</param>
        /// <param name="deleted">If the entities must be deleted. By default it is false.</param>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>The filtered collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryable<TEntity> Deleted<TEntity>(this IQueryable<TEntity> entities, bool deleted = false)
            where TEntity : IHaveSoftDelete
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Where(e => e.Deleted == deleted);
        }

        /// <summary>
        /// Filters a given collection of <see cref="IHaveSoftDelete"/> by their deleted state
        /// </summary>
        /// <param name="entities">The collection to be filtered</param>
        /// <param name="deleted">If the entities must be deleted. By default it is false.</param>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>The filtered collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<TEntity> Deleted<TEntity>(this IEnumerable<TEntity> entities, bool deleted = false)
            where TEntity : IHaveSoftDelete
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Where(e => e.Deleted == deleted);
        }

        #endregion
    }
}
