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

        #region AnyAsync

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> AnyAsync(CancellationToken ct);

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region AllAsync

        /// <summary>
        ///     Asynchronously determines whether all the elements of a sequence satisfy a condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>true</c> if every element of the source sequence passes the test in the specified
        ///     predicate; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Asynchronously determines whether all the elements of a sequence satisfy a condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>true</c> if every element of the source sequence passes the test in the specified
        ///     predicate; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region CountAsync

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        Task<int> CountAsync();

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        Task<int> CountAsync(CancellationToken ct);

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region LongCountAsync

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        Task<long> LongCountAsync();

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        Task<long> LongCountAsync(CancellationToken ct);

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        Task<long> LongCountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region LastAsync

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element.
        /// </returns>
        Task<T> LastAsync();

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element.
        /// </returns>
        Task<T> LastAsync(CancellationToken ct);

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element that passes the test in
        ///     <paramref name="predicate" />.
        /// </returns>
        Task<T> LastAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element that passes the test in
        ///     <paramref name="predicate" />.
        /// </returns>
        Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region LastOrDefaultAsync

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        Task<T> LastOrDefaultAsync();

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        Task<T> LastOrDefaultAsync(CancellationToken ct);

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region SingleAsync

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the sequence.
        /// </returns>
        Task<T> SingleAsync();

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the sequence.
        /// </returns>
        Task<T> SingleAsync(CancellationToken ct);

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />.
        /// </returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />.
        /// </returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region SingleOrDefaultAsync

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the sequence, or <c>default</c> ( <typeparamref name="T" /> ) if no such element is found.
        /// </returns>
        Task<T> SingleOrDefaultAsync();

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the sequence, or <c>default</c> ( <typeparamref name="T" /> ) if no such element is found.
        /// </returns>
        Task<T> SingleOrDefaultAsync(CancellationToken ct);

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />, or <c>default</c> ( <typeparamref name="T" /> ) if no such element is found.
        /// </returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />, or <c>default</c> ( <typeparamref name="T" /> ) if no such element is found.
        /// </returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);

        #endregion

        #region MinAsync

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        Task<T> MinAsync();

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        Task<T> MinAsync(CancellationToken ct);

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct);

        #endregion

        #region MaxAsync

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        Task<T> MaxAsync();

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        Task<T> MaxAsync(CancellationToken ct);

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the value returned by the function represented by <paramref name="selector" /> .
        /// </typeparam>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the value returned by the function represented by <paramref name="selector" /> .
        /// </typeparam>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct);

        #endregion

        #region SumAsync

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<int> SumAsync(Expression<Func<T, int>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<int> SumAsync(Expression<Func<T, int>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<int?> SumAsync(Expression<Func<T, int?>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<int?> SumAsync(Expression<Func<T, int?>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<long> SumAsync(Expression<Func<T, long>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<long> SumAsync(Expression<Func<T, long>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<long?> SumAsync(Expression<Func<T, long?>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<long?> SumAsync(Expression<Func<T, long?>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<double> SumAsync(Expression<Func<T, double>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<double> SumAsync(Expression<Func<T, double>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<double?> SumAsync(Expression<Func<T, double?>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<double?> SumAsync(Expression<Func<T, double?>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<float> SumAsync(Expression<Func<T, float>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<float> SumAsync(Expression<Func<T, float>> selector, CancellationToken ct);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<float?> SumAsync(Expression<Func<T, float?>> selector);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        Task<float?> SumAsync(Expression<Func<T, float?>> selector, CancellationToken ct);

        #endregion
    }
}