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

    /// <summary>
    /// Implementation of an <see cref="IAsyncRepository{TEntity,TId}"/> for the Entity Framework,
    /// exposing only sync operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The key type</typeparam>
    public class EFSyncRepository<TEntity, TKey> : ISyncRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        private readonly Func<TEntity, TKey, bool> _filterById;
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
        /// Creates a new sync repository
        /// </summary>
        /// <param name="dbContext">The database context</param>
        /// <param name="filterById">The filter by the entity if expression</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EFSyncRepository(DbContext dbContext, Func<TEntity, TKey, bool> filterById)
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
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        public IQueryable<TEntity> QueryById(TKey id)
        {
            return Query().Where(e => _filterById(e, id));
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