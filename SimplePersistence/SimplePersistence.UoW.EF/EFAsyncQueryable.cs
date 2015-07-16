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
	using System.Collections;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;

    /// <summary>
    /// Specialized <see cref="IQueryable{T}"/> for async executions.
    /// Used to wrap the Entity Framework persistence provider.
    /// </summary>
	/// <typeparam name="T"></typeparam>
	public class EFAsyncQueryable<T> : IAsyncQueryable<T>
	{
		private readonly IQueryable<T> _queryable;

		/// <summary>
		/// Creates a new instance that will wrapp the given <see cref="IQueryable{T}"/>
		/// </summary>
		/// <param name="queryable"></param>
		public EFAsyncQueryable(IQueryable<T> queryable)
		{
			_queryable = queryable;
		}

		#region IQueryable<T>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
		{
			return _queryable.GetEnumerator();
		}

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </returns>
        public Expression Expression { get { return _queryable.Expression; } }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType { get { return _queryable.ElementType; } }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider { get { return _queryable.Provider; } }

		#endregion

		#region IAsyncQueryable<T>

		#region LoadAsync

        /// <summary>
        /// Asynchronously enumerates the query result into memory by using 
        /// <see cref="QueryableExtensions.LoadAsync(System.Linq.IQueryable)"/>
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task LoadAsync()
		{
			return _queryable.LoadAsync();
		}

        /// <summary>
        /// Asynchronously enumerates the query result into memory by using 
        /// <see cref="QueryableExtensions.LoadAsync(System.Linq.IQueryable, System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task LoadAsync(CancellationToken ct)
		{
			return _queryable.LoadAsync(ct);
		}

		#endregion

		#region ForEachAsync

        /// <summary>
        /// Asynchronously enumerates the query results and performs the specified action on each element by using 
        /// <see cref="QueryableExtensions.ForEachAsync{T}(System.Linq.IQueryable{T},System.Action{T})"/>
        /// </summary>
        /// <param name="action">The action to perform on each element.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task ForEachAsync(Action<T> action)
		{
			return _queryable.ForEachAsync(action);
		}

        /// <summary>
        /// Asynchronously enumerates the query results and performs the specified action on each element by using 
        /// <see cref="QueryableExtensions.ForEachAsync{T}(System.Linq.IQueryable{T},System.Action{T}, System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="action">The action to perform on each element.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task ForEachAsync(Action<T> action, CancellationToken ct)
		{
			return _queryable.ForEachAsync(action, ct);
		}

		#endregion

		#region ToListAsync

        /// <summary>
        /// Creates a <see cref="System.Collections.Generic.List{T}"/> from an <see cref="System.Linq.IQueryable"/> by enumerating it asynchronously by using 
        /// <see cref="QueryableExtensions.ToListAsync{T}(System.Linq.IQueryable{T})"/>
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="System.Collections.Generic.List{T}"/> that contains elements from the input sequence.
        /// </returns>
        public Task<List<T>> ToListAsync()
		{
			return _queryable.ToListAsync();
		}

        /// <summary>
        /// Creates a <see cref="System.Collections.Generic.List{T}"/> from an <see cref="System.Linq.IQueryable"/> by enumerating it asynchronously by using 
        /// <see cref="QueryableExtensions.ToListAsync{T}(System.Linq.IQueryable{T}, System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="System.Collections.Generic.List{T}"/> that contains elements from the input sequence.
        /// </returns>
        public Task<List<T>> ToListAsync(CancellationToken ct)
		{
			return _queryable.ToListAsync(ct);
		}

		#endregion

		#region ToArrayAsync

        /// <summary>
        /// Creates an array from an <see cref="System.Linq.IQueryable{T}"/> by enumerating it asynchronously by using 
        /// <see cref="QueryableExtensions.ToArrayAsync{T}(System.Linq.IQueryable{T})"/>
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an array that contains elements from the input sequence.
        /// </returns>
        public Task<T[]> ToArrayAsync()
		{
			return _queryable.ToArrayAsync();
		}

        /// <summary>
        /// Creates an array from an <see cref="System.Linq.IQueryable{T}"/> by enumerating it asynchronously by using 
        /// <see cref="QueryableExtensions.ToArrayAsync{T}(System.Linq.IQueryable{T}, System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an array that contains elements from the input sequence.
        /// </returns>
        public Task<T[]> ToArrayAsync(CancellationToken ct)
		{
			return _queryable.ToArrayAsync(ct);
		}

		#endregion

		#region FirstOrDefaultAsync

        /// <summary>
        /// Asynchronously returns the first element of a sequence or a default value if no such element is found by using 
        /// <see cref="QueryableExtensions.FirstOrDefaultAsync{T}(System.Linq.IQueryable{T})"/>
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public Task<T> FirstOrDefaultAsync()
		{
			return _queryable.FirstOrDefaultAsync();
		}

        /// <summary>
        /// Asynchronously returns the first element of a sequence or a default value if no such element is found by using 
        /// <see cref="QueryableExtensions.FirstOrDefaultAsync{T}(System.Linq.IQueryable{T}, System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public Task<T> FirstOrDefaultAsync(CancellationToken ct)
		{
			return _queryable.FirstOrDefaultAsync(ct);
		}

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found by using 
        /// <see cref="QueryableExtensions.FirstOrDefaultAsync{T}(System.Linq.IQueryable{T}, Expression{Func{T, bool}})"/>
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			return _queryable.FirstOrDefaultAsync(predicate);
		}

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found by using 
        /// <see cref="QueryableExtensions.FirstOrDefaultAsync{T}(System.Linq.IQueryable{T}, Expression{Func{T, bool}}, CancellationToken)"/>
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
		{
			return _queryable.FirstOrDefaultAsync(predicate, ct);
		}

		#endregion

		#region FirstAsync

        /// <summary>
        /// Asynchronously returns the first element of a sequence by using 
        /// <see cref="QueryableExtensions.FirstAsync{T}(System.Linq.IQueryable{T})"/>
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public Task<T> FirstAsync()
		{
			return _queryable.FirstAsync();
		}

        /// <summary>
        /// Asynchronously returns the first element of a sequence by using 
        /// <see cref="QueryableExtensions.FirstAsync{T}(System.Linq.IQueryable{T}, System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public Task<T> FirstAsync(CancellationToken ct)
		{
			return _queryable.FirstAsync(ct);
		}

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition by using 
        /// <see cref="QueryableExtensions.FirstAsync{T}(System.Linq.IQueryable{T}, Expression{Func{T, bool}})"/>
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
		{
			return _queryable.FirstAsync(predicate);
		}

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition by using 
        /// <see cref="QueryableExtensions.FirstAsync{T}(System.Linq.IQueryable{T}, Expression{Func{T, bool}}, CancellationToken)"/>
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
		{
			return _queryable.FirstAsync(predicate, ct);
		}

		#endregion

        #region AnyAsync

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        public Task<bool> AnyAsync()
        {
            return _queryable.AnyAsync();
        }

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        public Task<bool> AnyAsync(CancellationToken ct)
        {
            return _queryable.AnyAsync(ct);
        }

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _queryable.AnyAsync();
        }

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return _queryable.AnyAsync(ct);
        }

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
        public Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
        {
            return _queryable.AllAsync(predicate);
        }

        /// <summary>
        ///     Asynchronously determines whether all the elements of a sequence satisfy a condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>true</c> if every element of the source sequence passes the test in the specified
        ///     predicate; otherwise, <c>false</c>.
        /// </returns>
        public Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return _queryable.AllAsync(predicate, ct);
        }

        #endregion

        #region CountAsync

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public Task<int> CountAsync()
        {
            return _queryable.CountAsync();
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public Task<int> CountAsync(CancellationToken ct)
        {
            return _queryable.CountAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return _queryable.CountAsync(predicate);
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return _queryable.CountAsync(predicate, ct);
        }

        #endregion

        #region LongCountAsync

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public Task<long> LongCountAsync()
        {
            return _queryable.LongCountAsync();
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public Task<long> LongCountAsync(CancellationToken ct)
        {
            return _queryable.LongCountAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public Task<long> LongCountAsync(Expression<Func<T, bool>> predicate)
        {
            return _queryable.LongCountAsync(predicate);
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return _queryable.LongCountAsync(predicate, ct);
        }

        #endregion

        #region LastAsync

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element.
        /// </returns>
        public Task<T> LastAsync()
        {
            return LastAsync(CancellationToken.None);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element.
        /// </returns>
        public Task<T> LastAsync(CancellationToken ct)
        {
            return Task.Run(() => _queryable.Last(), ct);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element that passes the test in
        ///     <paramref name="predicate" />.
        /// </returns>
        public Task<T> LastAsync(Expression<Func<T, bool>> predicate)
        {
            return LastAsync(predicate, CancellationToken.None);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element that passes the test in
        ///     <paramref name="predicate" />.
        /// </returns>
        public Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return Task.Run(() => _queryable.Last(predicate), ct);
        }

        #endregion

        #region LastOrDefaultAsync

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        public Task<T> LastOrDefaultAsync()
        {
            return LastOrDefaultAsync(CancellationToken.None);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        public Task<T> LastOrDefaultAsync(CancellationToken ct)
        {
            return Task.Run(() => _queryable.LastOrDefault(), ct);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        public Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return LastOrDefaultAsync(predicate, CancellationToken.None);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        public Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return Task.Run(() => _queryable.LastOrDefault(predicate), ct);
        }

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
        public Task<T> SingleAsync()
        {
            return _queryable.SingleAsync();
        }

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the sequence.
        /// </returns>
        public Task<T> SingleAsync(CancellationToken ct)
        {
            return _queryable.SingleAsync(ct);
        }

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
        public Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return _queryable.SingleAsync();
        }

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />.
        /// </returns>
        public Task<T> SingleAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return _queryable.SingleAsync(predicate, ct);
        }

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
        public Task<T> SingleOrDefaultAsync()
        {
            return _queryable.SingleOrDefaultAsync();
        }

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the sequence, or <c>default</c> ( <typeparamref name="T" /> ) if no such element is found.
        /// </returns>
        public Task<T> SingleOrDefaultAsync(CancellationToken ct)
        {
            return _queryable.SingleOrDefaultAsync(ct);
        }

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
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _queryable.SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />, or <c>default</c> ( <typeparamref name="T" /> ) if no such element is found.
        /// </returns>
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return _queryable.SingleOrDefaultAsync(predicate, ct);
        }

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
        public Task<T> MinAsync()
        {
            return _queryable.MinAsync();
        }

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        public Task<T> MinAsync(CancellationToken ct)
        {
            return _queryable.MinAsync(ct);
        }

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
        public Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return _queryable.MinAsync(selector);
        }

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        public Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct)
        {
            return _queryable.MinAsync(selector, ct);
        }

        #endregion

        #endregion
    }
}