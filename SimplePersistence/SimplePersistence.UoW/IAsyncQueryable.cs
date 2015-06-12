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
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Specialized <see cref="IQueryable{T}"/> for async executions.
    /// Used to wrap the persistence provider.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncQueryable<T> : IQueryable<T>
    {
        #region LoadAsync

        /// <summary>
        /// Asynchronously enumerates the query result into memory
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task LoadAsync();

        /// <summary>
        /// Asynchronously enumerates the query result into memory
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task LoadAsync(CancellationToken ct);

        #endregion

        #region ForEachAsync

        /// <summary>
        /// Asynchronously enumerates the query results and performs the specified action on each element.
        /// </summary>
        /// <param name="action">The action to perform on each element.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ForEachAsync(Action<T> action);

        /// <summary>
        /// Asynchronously enumerates the query results and performs the specified action on each element.
        /// </summary>
        /// <param name="action">The action to perform on each element.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ForEachAsync(Action<T> action, CancellationToken ct);

        #endregion

        #region ToListAsync

        /// <summary>
        /// Creates a <see cref="System.Collections.Generic.List{T}"/> from an <see cref="System.Linq.IQueryable"/> by enumerating it asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="System.Collections.Generic.List{T}"/> that contains elements from the input sequence.
        /// </returns>
        Task<List<T>> ToListAsync();

        /// <summary>
        /// Creates a <see cref="System.Collections.Generic.List{T}"/> from an <see cref="System.Linq.IQueryable"/> by enumerating it asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="System.Collections.Generic.List{T}"/> that contains elements from the input sequence.
        /// </returns>
        Task<List<T>> ToListAsync(CancellationToken ct);

        #endregion

        #region ToArrayAsync

        /// <summary>
        /// Creates an array from an <see cref="System.Linq.IQueryable{T}"/> by enumerating it asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an array that contains elements from the input sequence.
        /// </returns>
        Task<T[]> ToArrayAsync();

        /// <summary>
        /// Creates an array from an <see cref="System.Linq.IQueryable{T}"/> by enumerating it asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an array that contains elements from the input sequence.
        /// </returns>
        Task<T[]> ToArrayAsync(CancellationToken ct);

        #endregion

        #region FirstOrDefaultAsync

        /// <summary>
        /// Asynchronously returns the first element of a sequence or a default value if no such element is found.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        Task<T> FirstOrDefaultAsync();

        /// <summary>
        /// Asynchronously returns the first element of a sequence or a default value if no such element is found.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        Task<T> FirstOrDefaultAsync(CancellationToken ct);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region FirstAsync

        /// <summary>
        /// Asynchronously returns the first element of a sequence.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        Task<T> FirstAsync();

        /// <summary>
        /// Asynchronously returns the first element of a sequence.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        Task<T> FirstAsync(CancellationToken ct);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion
    }
}