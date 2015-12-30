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
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// Implementation of an <see cref="IAsyncRepository{TEntity,TId}"/> for the NHibernate,
    /// exposing only async operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The entity id type</typeparam>
    public abstract class NHibernateAsyncRepository<TEntity, TKey> : IAsyncRepository<TEntity, TKey>
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
        /// <exception cref="ArgumentNullException"/>
        protected NHibernateAsyncRepository([NotNull] ISession session, [NotNull] Func<TEntity, TKey, bool> filterById)
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
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        public virtual Task<TEntity> GetByIdAsync(TKey id)
        {
            return GetByIdAsync(id, CancellationToken.None);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        public virtual Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => GetById(id), ct);
        }

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity)
        {
            return AddAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => Add(entity), ct);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            return AddAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => Add(entities), ct);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return UpdateAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => Update(entity), ct);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return UpdateAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => Update(entities), ct);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return DeleteAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => Delete(entity), ct);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            return DeleteAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => Delete(entities), ct);
        }

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        public virtual Task<long> TotalAsync()
        {
            return TotalAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        public virtual Task<long> TotalAsync(CancellationToken ct)
        {
            return Task.Factory.StartNew(() => Total(), ct);
        }

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey id)
        {
            return ExistsAsync(id, CancellationToken.None);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey id, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => Exists(id), ct);
        }

        #endregion

        #region Private

        private TEntity GetById(TKey id)
        {
            return Session.Get<TEntity>(id);
        }

        private TEntity Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Save(entity);
            return entity;
        }

        private IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Add).ToArray();
        }

        private TEntity Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Update(entity);
            return entity;
        }

        private IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Update).ToArray();
        }

        private TEntity Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Delete(entity);
            return entity;
        }

        private IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Delete).ToArray();
        }

        private long Total()
        {
            return Session.QueryOver<TEntity>().RowCountInt64();
        }

        private bool Exists(TKey id)
        {
            return QueryById(id).Any();
        }

        #endregion
    }
}
