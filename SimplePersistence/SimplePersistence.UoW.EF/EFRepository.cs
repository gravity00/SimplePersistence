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
namespace SimplePersistence.UoW.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IRepository{TEntity,TId}"/> for the Entity Framework,
    /// exposing both sync and async operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The key type</typeparam>
    public class EFRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        protected DbContext DbContext { get; private set; }

        /// <summary>
        /// The <see cref="IDbSet{TEntity}"/> of this repository entity
        /// </summary>
        protected DbSet<TEntity> DbSet { get; private set; }

        /// <summary>
        /// Creates a new repository
        /// </summary>
        /// <param name="dbContext">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EFRepository(DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");

            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        #region Query

        /// <summary>
        /// Gets an <see cref="System.Linq.IQueryable{T}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="System.Linq.IQueryable{T}"/> object</returns>
        public IQueryable<TEntity> Query()
        {
            return DbSet;
        }

        /// <summary>
        /// Gets an <see cref="System.Linq.IQueryable{T}"/> for this repository entities
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="System.Linq.IQueryable{T}"/> object</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch)
        {
            if (propertiesToFetch == null) throw new ArgumentNullException("propertiesToFetch");

            return
                propertiesToFetch
                    .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(
                        DbSet, (current, expression) => current.Include(expression));
        }

        #endregion

        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The entity or null if not found</returns>
        public TEntity GetById(TKey id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey id)
        {
            return DbSet.FindAsync(id);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct)
        {
            return DbSet.FindAsync(ct, id);
        }

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        public TEntity Add(TEntity entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
                return DbSet.Add(entity);

            dbEntityEntry.State = EntityState.Added;
            return dbEntityEntry.Entity;
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity)
        {
            return AddAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return Task.FromResult(Add(entity));
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return DbSet.AddRange(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            return AddAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return Task.FromResult(Add(entities));
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        public TEntity Update(TEntity entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
                DbSet.Attach(entity);
            if (dbEntityEntry.State != EntityState.Added && dbEntityEntry.State != EntityState.Deleted)
                dbEntityEntry.State = EntityState.Modified;

            return dbEntityEntry.Entity;
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return UpdateAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return Task.FromResult(Update(entity));
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Update).ToArray();
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return UpdateAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return Task.FromResult(Update(entities));
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        public TEntity Delete(TEntity entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
                return dbEntityEntry.Entity;
            }

            DbSet.Attach(entity);
            DbSet.Remove(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return DeleteAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return Task.FromResult(Delete(entity));
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Delete).ToArray();
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            return DeleteAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return Task.FromResult(Delete(entities));
        }

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        public long Total()
        {
            return DbSet.LongCount();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public async Task<long> TotalAsync()
        {
            return await DbSet.LongCountAsync();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public async Task<long> TotalAsync(CancellationToken ct)
        {
            return await DbSet.LongCountAsync(ct);
        }

        #endregion
    }
}