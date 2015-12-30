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
namespace SimplePersistence.UoW.NH
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// Implementation of an <see cref="IAsyncRepository{TEntity,TId}"/> for the NHibernate,
    /// exposing only sync operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The key type</typeparam>
    public abstract class NHibernateSyncRepository<TEntity, TKey> : ISyncRepository<TEntity, TKey>
        where TEntity : class 
        where TKey : IEquatable<TKey>
    {
        private readonly Func<TEntity, TKey, bool> _filterById;
        private readonly ISession _session;

        /// <summary>
        /// The database session
        /// </summary>
        protected ISession Session
        {
            get { return _session; }
        }

        /// <summary>
        /// Creates a new repository
        /// </summary>
        /// <param name="session">The database session</param>
        /// <param name="filterById">The filter by the entity if expression</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected NHibernateSyncRepository([NotNull] ISession session, [NotNull] Func<TEntity, TKey, bool> filterById)
        {
            if (session == null) throw new ArgumentNullException("session");
            if (filterById == null) throw new ArgumentNullException("filterById");

            _session = session;
            _filterById = filterById;
        }

        #region Query

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public virtual IQueryable<TEntity> Query()
        {
            return Session.Query<TEntity>();
        }

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public IQueryable<TEntity> QueryById(TKey id)
        {
            return Query().Where(e => _filterById(e, id));
        }

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public virtual IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch)
        {
            return propertiesToFetch.Aggregate(Query(), (current, expression) => current.Fetch(expression));
        }

        #endregion

        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The entity or null if not found</returns>
        public virtual TEntity GetById(TKey id)
        {
            return Session.Get<TEntity>(id);
        }

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Save(entity);
            return entity;
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Add).ToArray();
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Update(entity);
            return entity;
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        public virtual IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Update).ToArray();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        public virtual TEntity Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Delete(entity);
            return entity;
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        public virtual IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Delete).ToArray();
        }

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        public virtual long Total()
        {
            return Session.QueryOver<TEntity>().RowCountInt64();
        }

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>True if entity exists</returns>
        public bool Exists(TKey id)
        {
            return QueryById(id).Any();
        }

        #endregion
    }
}
