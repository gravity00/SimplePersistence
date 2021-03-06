#region License
// The MIT License (MIT)
// Copyright (c) 2015 Jo�o Sim�es
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion
namespace SimplePersistence.UoW
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
    public interface IAsyncRepository<TEntity, in TId> : IExposeQueryable<TEntity, TId> 
        where TEntity : class 
        where TId : IEquatable<TId>
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId id);

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId id, CancellationToken ct);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync();

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct);

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId id);

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId id, CancellationToken ct);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    public interface IAsyncRepository<TEntity, in TId01, in TId02> : IExposeQueryable<TEntity, TId01, TId02>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId01 id01, TId02 id02);

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId01 id01, TId02 id02, CancellationToken ct);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync();

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct);

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId01 id01, TId02 id02);

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId01 id01, TId02 id02, CancellationToken ct);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    public interface IAsyncRepository<TEntity, in TId01, in TId02, in TId03> : IExposeQueryable<TEntity, TId01, TId02, TId03>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
        where TId03 : IEquatable<TId03>
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId01 id01, TId02 id02, TId03 id03);

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId01 id01, TId02 id02, TId03 id03, CancellationToken ct);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync();

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct);

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId01 id01, TId02 id02, TId03 id03);

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId01 id01, TId02 id02, TId03 id03, CancellationToken ct);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    /// <typeparam name="TId04">The entity id fourth type</typeparam>
    public interface IAsyncRepository<TEntity, in TId01, in TId02, in TId03, in TId04> : IExposeQueryable<TEntity, TId01, TId02, TId03, TId04>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
        where TId03 : IEquatable<TId03>
        where TId04 : IEquatable<TId04>
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId01 id01, TId02 id02, TId03 id03, TId04 id04);

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId01 id01, TId02 id02, TId03 id03, TId04 id04, CancellationToken ct);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync();

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct);

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId01 id01, TId02 id02, TId03 id03, TId04 id04);

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId01 id01, TId02 id02, TId03 id03, TId04 id04, CancellationToken ct);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IAsyncRepository<TEntity> : IExposeQueryable<TEntity>
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(params object[] ids);

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="ids">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(CancellationToken ct, params object[] ids);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync();

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct);

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(params object[] ids);

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(CancellationToken ct, params object[] ids);

        #endregion
    }
}