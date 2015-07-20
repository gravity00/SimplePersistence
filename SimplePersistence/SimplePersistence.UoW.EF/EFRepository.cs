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
        where TKey : IEquatable<TKey>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;
        private readonly EFRepository<TEntity> _repository;

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        protected DbContext DbContext
        {
            get { return _dbContext; }
        }

        /// <summary>
        /// The <see cref="IDbSet{TEntity}"/> of this repository entity
        /// </summary>
        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        /// <summary>
        /// Creates a new repository
        /// </summary>
        /// <param name="dbContext">The database context</param>
        /// <param name="filterById">The filter by the entity if expression</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EFRepository(DbContext dbContext, Func<TEntity, TKey, bool> filterById)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (filterById == null) throw new ArgumentNullException("filterById");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _repository =
                new EFRepository<TEntity>(
                    dbContext,
                    (e, o) => filterById(e, (TKey) o[0]));
        }

        #region Query

        /// <summary>
        /// Gets an <see cref="System.Linq.IQueryable{T}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="System.Linq.IQueryable{T}"/> object</returns>
        public IQueryable<TEntity> Query()
        {
            return _repository.Query();
        }

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public IQueryable<TEntity> QueryById(TKey id)
        {
            return _repository.QueryById(id);
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
            return _repository.QueryFetching(propertiesToFetch);
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
            return _repository.GetById(id);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey id)
        {
            return _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct)
        {
            return _repository.GetByIdAsync(ct, id);
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
            return _repository.Add(entity);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity)
        {
            return _repository.AddAsync(entity);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.AddAsync(entity, ct);
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            return _repository.Add(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            return _repository.AddAsync(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.AddAsync(entities, ct);
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
            return _repository.Update(entity);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.UpdateAsync(entity, ct);
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            return _repository.Update(entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return _repository.UpdateAsync(entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.UpdateAsync(entities, ct);
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
            return _repository.Delete(entity);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return _repository.DeleteAsync(entity);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.DeleteAsync(entity, ct);
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            return _repository.Delete(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            return _repository.DeleteAsync(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.DeleteAsync(entities, ct);
        }

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        public long Total()
        {
            return _repository.Total();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public Task<long> TotalAsync()
        {
            return _repository.TotalAsync();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public Task<long> TotalAsync(CancellationToken ct)
        {
            return _repository.TotalAsync(ct);
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
            return _repository.Exists(id);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey id)
        {
            return _repository.ExistsAsync(id);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey id, CancellationToken ct)
        {
            return _repository.ExistsAsync(ct, id);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of an <see cref="IRepository{TEntity,TId}"/> for the Entity Framework,
    /// exposing both sync and async operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    public class EFRepository<TEntity, TKey01, TKey02> : IRepository<TEntity, TKey01, TKey02>
        where TEntity : class
        where TKey01 : IEquatable<TKey01>
        where TKey02 : IEquatable<TKey02>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;
        private readonly EFRepository<TEntity> _repository;

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        protected DbContext DbContext
        {
            get { return _dbContext; }
        }

        /// <summary>
        /// The <see cref="IDbSet{TEntity}"/> of this repository entity
        /// </summary>
        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        /// <summary>
        /// Creates a new repository
        /// </summary>
        /// <param name="dbContext">The database context</param>
        /// <param name="filterById">The filter by the entity if expression</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EFRepository(DbContext dbContext, Func<TEntity, TKey01, TKey02, bool> filterById)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (filterById == null) throw new ArgumentNullException("filterById");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _repository =
                new EFRepository<TEntity>(
                    dbContext,
                    (e, o) => filterById(e, (TKey01) o[0], (TKey02) o[1]));
        }

        #region Query

        /// <summary>
        /// Gets an <see cref="System.Linq.IQueryable{T}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="System.Linq.IQueryable{T}"/> object</returns>
        public IQueryable<TEntity> Query()
        {
            return _repository.Query();
        }

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public IQueryable<TEntity> QueryById(TKey01 id01, TKey02 id02)
        {
            return _repository.QueryById(id01, id02);
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
            return _repository.QueryFetching(propertiesToFetch);
        }

        #endregion

        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>The entity or null if not found</returns>
        public TEntity GetById(TKey01 id01, TKey02 id02)
        {
            return _repository.GetById(id01, id02);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02)
        {
            return _repository.GetByIdAsync(id01, id02);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, CancellationToken ct)
        {
            return _repository.GetByIdAsync(ct, id01, id02);
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
            return _repository.Add(entity);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity)
        {
            return _repository.AddAsync(entity);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.AddAsync(entity, ct);
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            return _repository.Add(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            return _repository.AddAsync(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.AddAsync(entities, ct);
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
            return _repository.Update(entity);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.UpdateAsync(entity, ct);
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            return _repository.Update(entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return _repository.UpdateAsync(entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.UpdateAsync(entities, ct);
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
            return _repository.Delete(entity);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return _repository.DeleteAsync(entity);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.DeleteAsync(entity, ct);
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            return _repository.Delete(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            return _repository.DeleteAsync(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.DeleteAsync(entities, ct);
        }

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        public long Total()
        {
            return _repository.Total();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public Task<long> TotalAsync()
        {
            return _repository.TotalAsync();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public Task<long> TotalAsync(CancellationToken ct)
        {
            return _repository.TotalAsync(ct);
        }

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>True if entity exists</returns>
        public bool Exists(TKey01 id01, TKey02 id02)
        {
            return _repository.Exists(id01, id02);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02)
        {
            return _repository.ExistsAsync(id01, id02);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, CancellationToken ct)
        {
            return _repository.ExistsAsync(ct, id01, id02);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of an <see cref="IRepository{TEntity,TId}"/> for the Entity Framework,
    /// exposing both sync and async operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    /// <typeparam name="TKey03">The entity id third type</typeparam>
    public class EFRepository<TEntity, TKey01, TKey02, TKey03> : IRepository<TEntity, TKey01, TKey02, TKey03>
        where TEntity : class
        where TKey01 : IEquatable<TKey01>
        where TKey02 : IEquatable<TKey02>
        where TKey03 : IEquatable<TKey03>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;
        private readonly EFRepository<TEntity> _repository;

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        protected DbContext DbContext
        {
            get { return _dbContext; }
        }

        /// <summary>
        /// The <see cref="IDbSet{TEntity}"/> of this repository entity
        /// </summary>
        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        /// <summary>
        /// Creates a new repository
        /// </summary>
        /// <param name="dbContext">The database context</param>
        /// <param name="filterById">The filter by the entity if expression</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EFRepository(DbContext dbContext, Func<TEntity, TKey01, TKey02, TKey03, bool> filterById)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (filterById == null) throw new ArgumentNullException("filterById");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _repository =
                new EFRepository<TEntity>(
                    dbContext,
                    (e, o) => filterById(e, (TKey01) o[0], (TKey02) o[1], (TKey03) o[2]));
        }

        #region Query

        /// <summary>
        /// Gets an <see cref="System.Linq.IQueryable{T}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="System.Linq.IQueryable{T}"/> object</returns>
        public IQueryable<TEntity> Query()
        {
            return _repository.Query();
        }

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public IQueryable<TEntity> QueryById(TKey01 id01, TKey02 id02, TKey03 id03)
        {
            return _repository.QueryById(id01, id02, id03);
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
            return _repository.QueryFetching(propertiesToFetch);
        }

        #endregion

        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>The entity or null if not found</returns>
        public TEntity GetById(TKey01 id01, TKey02 id02, TKey03 id03)
        {
            return _repository.GetById(id01, id02, id03);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, TKey03 id03)
        {
            return _repository.GetByIdAsync(id01, id02, id03);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, TKey03 id03, CancellationToken ct)
        {
            return _repository.GetByIdAsync(ct, id01, id02, id03);
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
            return _repository.Add(entity);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity)
        {
            return _repository.AddAsync(entity);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.AddAsync(entity, ct);
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            return _repository.Add(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            return _repository.AddAsync(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.AddAsync(entities, ct);
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
            return _repository.Update(entity);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.UpdateAsync(entity, ct);
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            return _repository.Update(entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return _repository.UpdateAsync(entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.UpdateAsync(entities, ct);
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
            return _repository.Delete(entity);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return _repository.DeleteAsync(entity);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.DeleteAsync(entity, ct);
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            return _repository.Delete(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            return _repository.DeleteAsync(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.DeleteAsync(entities, ct);
        }

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        public long Total()
        {
            return _repository.Total();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public Task<long> TotalAsync()
        {
            return _repository.TotalAsync();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public Task<long> TotalAsync(CancellationToken ct)
        {
            return _repository.TotalAsync(ct);
        }

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <returns>True if entity exists</returns>
        public bool Exists(TKey01 id01, TKey02 id02, TKey03 id03)
        {
            return _repository.Exists(id01, id02, id03);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, TKey03 id03)
        {
            return _repository.ExistsAsync(id01, id02, id03);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, TKey03 id03, CancellationToken ct)
        {
            return _repository.ExistsAsync(ct, id01, id02, id03);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of an <see cref="IRepository{TEntity,TId}"/> for the Entity Framework,
    /// exposing both sync and async operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    /// <typeparam name="TKey03">The entity id third type</typeparam>
    /// <typeparam name="TKey04">The entity id fourth type</typeparam>
    public class EFRepository<TEntity, TKey01, TKey02, TKey03, TKey04> : IRepository<TEntity, TKey01, TKey02, TKey03, TKey04>
        where TEntity : class
        where TKey01 : IEquatable<TKey01>
        where TKey02 : IEquatable<TKey02>
        where TKey03 : IEquatable<TKey03>
        where TKey04 : IEquatable<TKey04>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;
        private readonly EFRepository<TEntity> _repository;

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        protected DbContext DbContext
        {
            get { return _dbContext; }
        }

        /// <summary>
        /// The <see cref="IDbSet{TEntity}"/> of this repository entity
        /// </summary>
        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        /// <summary>
        /// Creates a new repository
        /// </summary>
        /// <param name="dbContext">The database context</param>
        /// <param name="filterById">The filter by the entity if expression</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EFRepository(DbContext dbContext, Func<TEntity, TKey01, TKey02, TKey03, TKey04, bool> filterById)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (filterById == null) throw new ArgumentNullException("filterById");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _repository =
                new EFRepository<TEntity>(
                    dbContext,
                    (e, o) => filterById(e, (TKey01) o[0], (TKey02) o[1], (TKey03) o[2], (TKey04) o[3]));
        }

        #region Query

        /// <summary>
        /// Gets an <see cref="System.Linq.IQueryable{T}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="System.Linq.IQueryable{T}"/> object</returns>
        public IQueryable<TEntity> Query()
        {
            return _repository.Query();
        }

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <param name="id04">The entity fourth unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public IQueryable<TEntity> QueryById(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04)
        {
            return _repository.QueryById(id01, id02, id03, id04);
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
            return _repository.QueryFetching(propertiesToFetch);
        }

        #endregion

        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <returns>The entity or null if not found</returns>
        public TEntity GetById(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04)
        {
            return _repository.GetById(id01, id02, id03, id04);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04)
        {
            return _repository.GetByIdAsync(id01, id02, id03, id04);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04, CancellationToken ct)
        {
            return _repository.GetByIdAsync(ct, id01, id02, id03, id04);
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
            return _repository.Add(entity);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity)
        {
            return _repository.AddAsync(entity);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.AddAsync(entity, ct);
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            return _repository.Add(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            return _repository.AddAsync(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.AddAsync(entities, ct);
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
            return _repository.Update(entity);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.UpdateAsync(entity, ct);
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            return _repository.Update(entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return _repository.UpdateAsync(entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.UpdateAsync(entities, ct);
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
            return _repository.Delete(entity);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return _repository.DeleteAsync(entity);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            return _repository.DeleteAsync(entity, ct);
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            return _repository.Delete(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            return _repository.DeleteAsync(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _repository.DeleteAsync(entities, ct);
        }

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        public long Total()
        {
            return _repository.Total();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public Task<long> TotalAsync()
        {
            return _repository.TotalAsync();
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the total</returns>
        public Task<long> TotalAsync(CancellationToken ct)
        {
            return _repository.TotalAsync(ct);
        }

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <param name="id04">The entity fourth unique identifier value</param>
        /// <returns>True if entity exists</returns>
        public bool Exists(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04)
        {
            return _repository.Exists(id01, id02, id03, id04);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <param name="id04">The entity fourth unique identifier value</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04)
        {
            return _repository.ExistsAsync(id01, id02, id03, id04);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <param name="id04">The entity fourth unique identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04, CancellationToken ct)
        {
            return _repository.ExistsAsync(ct, id01, id02, id03, id04);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of an <see cref="IRepository{TEntity,TId}"/> for the Entity Framework,
    /// exposing both sync and async operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly Func<TEntity, object[], bool> _filterById;
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        protected DbContext DbContext
        {
            get { return _dbContext; }
        }

        /// <summary>
        /// The <see cref="IDbSet{TEntity}"/> of this repository entity
        /// </summary>
        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        /// <summary>
        /// Creates a new repository
        /// </summary>
        /// <param name="dbContext">The database context</param>
        /// <param name="filterById">The filter by the entity if expression</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EFRepository(DbContext dbContext, Func<TEntity, object[], bool> filterById)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (filterById == null) throw new ArgumentNullException("filterById");

            _filterById = filterById;
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
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
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="ids">The entity unique identifier</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public IQueryable<TEntity> QueryById(params object[] ids)
        {
            return Query().Where(e => _filterById(e, ids));
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
        /// <param name="ids">The entity unique identifier</param>
        /// <returns>The entity or null if not found</returns>
        public TEntity GetById(params object[] ids)
        {
            return DbSet.Find(ids);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="ids">The entity unique identifier</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public async Task<TEntity> GetByIdAsync(params object[] ids)
        {
            return await DbSet.FindAsync(ids);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="ids">The entity unique identifier</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> that will fetch the entity</returns>
        public async Task<TEntity> GetByIdAsync(CancellationToken ct, params object[] ids)
        {
            return await DbSet.FindAsync(ct, ids);
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
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await AddAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(Add(entity));
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
        public async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            return await AddAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(Add(entities));
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
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await UpdateAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(Update(entity));
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
        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return await UpdateAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(Update(entities));
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
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return await DeleteAsync(entity, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entity</returns>
        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(Delete(entity));
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
        public async Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities)
        {
            return await DeleteAsync(entities, CancellationToken.None);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task{TResult}"/> containing the entities</returns>
        public async Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return await Task.FromResult(Delete(entities));
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

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifier</param>
        /// <returns>True if entity exists</returns>
        public bool Exists(params object[] ids)
        {
            return QueryById(ids).Any();
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifier</param>
        /// <returns>True if entity exists</returns>
        public async Task<bool> ExistsAsync(params object[] ids)
        {
            return await QueryById(ids).AnyAsync();
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        public async Task<bool> ExistsAsync(CancellationToken ct, params object[] ids)
        {
            return await QueryById(ids).AnyAsync(ct);
        }

        #endregion
    }
}