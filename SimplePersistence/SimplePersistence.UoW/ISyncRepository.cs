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
namespace SimplePersistence.UoW
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
    public interface ISyncRepository<TEntity, in TId> : IExposeQueryable<TEntity, TId>
        where TEntity : class
        where TId : IEquatable<TId>
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(TId id);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>True if entity exists</returns>
        bool Exists(TId id);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    public interface ISyncRepository<TEntity, in TId01, in TId02> : IExposeQueryable<TEntity, TId01, TId02>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(TId01 id01, TId02 id02);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>True if entity exists</returns>
        bool Exists(TId01 id01, TId02 id02);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    public interface ISyncRepository<TEntity, in TId01, in TId02, in TId03> : IExposeQueryable<TEntity, TId01, TId02, TId03>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
        where TId03 : IEquatable<TId03>
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(TId01 id01, TId02 id02, TId03 id03);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>True if entity exists</returns>
        bool Exists(TId01 id01, TId02 id02, TId03 id03);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface ISyncRepository<TEntity> : IExposeQueryable<TEntity> 
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(params object[] ids);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>True if entity exists</returns>
        bool Exists(params object[] ids);

        #endregion
    }
}