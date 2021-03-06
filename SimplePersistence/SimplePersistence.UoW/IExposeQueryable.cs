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
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Can be exported as a <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IExposeQueryable<TEntity, in TId> 
        where TEntity : class
        where TId : IEquatable<TId>
    {
        #region Query

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId id);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);

        #endregion
    }

    /// <summary>
    /// Can be exported as a <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId01"></typeparam>
    /// <typeparam name="TId02"></typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
    {
        #region Query

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);

        #endregion
    }

    /// <summary>
    /// Can be exported as a <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId01"></typeparam>
    /// <typeparam name="TId02"></typeparam>
    /// <typeparam name="TId03"></typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02, in TId03>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
        where TId03 : IEquatable<TId03>
    {
        #region Query

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02, TId03 id03);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);

        #endregion
    }

    /// <summary>
    /// Can be exported as a <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId01"></typeparam>
    /// <typeparam name="TId02"></typeparam>
    /// <typeparam name="TId03"></typeparam>
    /// <typeparam name="TId04"></typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02, in TId03, in TId04>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
        where TId03 : IEquatable<TId03>
        where TId04 : IEquatable<TId04>
    {
        #region Query

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity third identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02, TId03 id03, TId04 id04);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);

        #endregion
    }

    /// <summary>
    /// Can be exported as a <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IExposeQueryable<TEntity>
        where TEntity : class
    {
        #region Query

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(params object[] ids);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> for this repository entities
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);

        #endregion
    }
}