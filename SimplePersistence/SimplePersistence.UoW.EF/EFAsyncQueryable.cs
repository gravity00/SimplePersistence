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
    using JetBrains.Annotations;

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
        /// <param name="queryable">The <see cref="IQueryable{T}"/> to be wrapped</param>
        /// <exception cref="ArgumentNullException"/>
		public EFAsyncQueryable([NotNull] IQueryable<T> queryable)
		{
		    if (queryable == null) throw new ArgumentNullException("queryable");
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
        /// <exception cref="ArgumentNullException"/>
        public Task ForEachAsync(Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            return _queryable.ForEachAsync(action);
        }

        /// <summary>
        /// Asynchronously enumerates the query results and performs the specified action on each element by using 
        /// <see cref="QueryableExtensions.ForEachAsync{T}(System.Linq.IQueryable{T},System.Action{T}, System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="action">The action to perform on each element.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"/>
        public Task ForEachAsync(Action<T> action, CancellationToken ct)
        {
            if (action == null) throw new ArgumentNullException("action");
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

        #region ToDictionaryAsync

        /// <summary>
        ///     Creates a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> from an <see cref="System.Linq.IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> that contains selected keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(Func<T, TKey> keySelector)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            return _queryable.ToDictionaryAsync(keySelector);
        }

        /// <summary>
        ///     Creates a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> from an <see cref="System.Linq.IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> that contains selected keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(Func<T, TKey> keySelector, CancellationToken ct)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            return _queryable.ToDictionaryAsync(keySelector, ct);
        }

        /// <summary>
        ///     Creates a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> from an <see cref="System.Linq.IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function and a comparer.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="comparer">
        ///     An <see cref="System.Collections.Generic.IEqualityComparer{T}" /> to compare keys.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> that contains selected keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (comparer == null) throw new ArgumentNullException("comparer");
            return _queryable.ToDictionaryAsync(keySelector, comparer);
        }

        /// <summary>
        ///     Creates a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> from an <see cref="System.Linq.IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function and a comparer.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="comparer">
        ///     An <see cref="System.Collections.Generic.IEqualityComparer{T}" /> to compare keys.
        /// </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> that contains selected keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken ct)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (comparer == null) throw new ArgumentNullException("comparer");
            return _queryable.ToDictionaryAsync(keySelector, comparer, ct);
        }

        /// <summary>
        ///     Creates a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> from an <see cref="System.Linq.IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector and an element selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <typeparam name="TElement">
        ///     The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="elementSelector"> A transform function to produce a result element value from each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> that contains values of type
        ///     <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(Func<T, TKey> keySelector, Func<T, TElement> elementSelector)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");
            return _queryable.ToDictionaryAsync(keySelector, elementSelector);
        }

        /// <summary>
        ///     Creates a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> from an <see cref="System.Linq.IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector and an element selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <typeparam name="TElement">
        ///     The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="elementSelector"> A transform function to produce a result element value from each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> that contains values of type
        ///     <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(Func<T, TKey> keySelector, Func<T, TElement> elementSelector, CancellationToken ct)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");
            return _queryable.ToDictionaryAsync(keySelector, elementSelector, ct);
        }

        /// <summary>
        ///     Creates a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> from an <see cref="System.Linq.IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function, a comparer, and an element selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <typeparam name="TElement">
        ///     The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="elementSelector"> A transform function to produce a result element value from each element. </param>
        /// <param name="comparer">
        ///     An <see cref="System.Collections.Generic.IEqualityComparer{T}" /> to compare keys.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> that contains values of type
        ///     <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(Func<T, TKey> keySelector, Func<T, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");
            if (comparer == null) throw new ArgumentNullException("comparer");
            return _queryable.ToDictionaryAsync(keySelector, elementSelector, comparer);
        }

        /// <summary>
        ///     Creates a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> from an <see cref="System.Linq.IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function, a comparer, and an element selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <typeparam name="TElement">
        ///     The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="elementSelector"> A transform function to produce a result element value from each element. </param>
        /// <param name="comparer">
        ///     An <see cref="System.Collections.Generic.IEqualityComparer{T}" /> to compare keys.
        /// </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="System.Collections.Generic.Dictionary{TKey,TValue}" /> that contains values of type
        ///     <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            Func<T, TKey> keySelector, Func<T, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken ct)
        {
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");
            if (comparer == null) throw new ArgumentNullException("comparer");
            return _queryable.ToDictionaryAsync(keySelector, elementSelector, comparer, ct);
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
            if (predicate == null) throw new ArgumentNullException("predicate");
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
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            return _queryable.AnyAsync(predicate);
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
        /// <exception cref="ArgumentNullException"/>
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            return _queryable.AnyAsync(predicate, ct);
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
        /// <exception cref="ArgumentNullException"/>
        public Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<long> LongCountAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> LastAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            return _queryable.SingleAsync(predicate);
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> SingleAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
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
        /// <exception cref="ArgumentNullException"/>
        public Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.MinAsync(selector, ct);
        }

        #endregion

        #region MaxAsync

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        public Task<T> MaxAsync()
        {
            return _queryable.MaxAsync();
        }

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        public Task<T> MaxAsync(CancellationToken ct)
        {
            return _queryable.MaxAsync(ct);
        }

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
        /// <exception cref="ArgumentNullException"/>
        public Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.MaxAsync(selector);
        }

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the value returned by the function represented by <paramref name="selector" /> .
        /// </typeparam>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.MaxAsync(selector, ct);
        }

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
        /// <exception cref="ArgumentNullException"/>
        public Task<decimal> SumAsync(Expression<Func<T, decimal>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<int> SumAsync(Expression<Func<T, int>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<int> SumAsync(Expression<Func<T, int>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<int?> SumAsync(Expression<Func<T, int?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<int?> SumAsync(Expression<Func<T, int?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<long> SumAsync(Expression<Func<T, long>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<long> SumAsync(Expression<Func<T, long>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<long?> SumAsync(Expression<Func<T, long?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<long?> SumAsync(Expression<Func<T, long?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double> SumAsync(Expression<Func<T, double>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double> SumAsync(Expression<Func<T, double>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double?> SumAsync(Expression<Func<T, double?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double?> SumAsync(Expression<Func<T, double?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<float> SumAsync(Expression<Func<T, float>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<float> SumAsync(Expression<Func<T, float>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<float?> SumAsync(Expression<Func<T, float?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<float?> SumAsync(Expression<Func<T, float?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.SumAsync(selector, ct);
        }

        #endregion

        #region AverageAsync

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<decimal?> AverageAsync(Expression<Func<T, decimal?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<decimal?> AverageAsync(Expression<Func<T, decimal?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double> AverageAsync(Expression<Func<T, int>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double> AverageAsync(Expression<Func<T, int>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double?> AverageAsync(Expression<Func<T, int?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double?> AverageAsync(Expression<Func<T, int?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double> AverageAsync(Expression<Func<T, long>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double> AverageAsync(Expression<Func<T, long>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double?> AverageAsync(Expression<Func<T, long?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double?> AverageAsync(Expression<Func<T, long?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double> AverageAsync(Expression<Func<T, double>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double> AverageAsync(Expression<Func<T, double>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double?> AverageAsync(Expression<Func<T, double?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<double?> AverageAsync(Expression<Func<T, double?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<float> AverageAsync(Expression<Func<T, float>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<float> AverageAsync(Expression<Func<T, float>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<float?> AverageAsync(Expression<Func<T, float?>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector);
        }

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<float?> AverageAsync(Expression<Func<T, float?>> selector, CancellationToken ct)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return _queryable.AverageAsync(selector, ct);
        }

        #endregion

        #region ContainsAsync

        /// <summary>
        ///     Asynchronously determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        /// <param name="item"> The object to locate in the sequence. </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>true</c> if the input sequence contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<bool> ContainsAsync(T item)
        {
            if (item == null) throw new ArgumentNullException("item");
            return _queryable.ContainsAsync(item);
        }

        /// <summary>
        ///     Asynchronously determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        /// <param name="item"> The object to locate in the sequence. </param>
        /// <param name="ct">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>true</c> if the input sequence contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public Task<bool> ContainsAsync(T item, CancellationToken ct)
        {
            if (item == null) throw new ArgumentNullException("item");
            return _queryable.ContainsAsync(item, ct);
        }

        #endregion

        #endregion
    }
}