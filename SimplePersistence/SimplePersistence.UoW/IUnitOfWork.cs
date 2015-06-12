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
	using Exceptions;
	using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface representing an unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Prepares the <see cref="IUnitOfWork"/> to work
        /// </summary>
        void Begin();

        /// <summary>
        /// Asynchronously prepares the <see cref="IUnitOfWork"/> to work
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task BeginAsync(CancellationToken ct);

        /// <summary>
        /// Commit the work made by this <see cref="IUnitOfWork"/>
        /// </summary>
        /// <exception cref="ConcurrencyException">
        ///     Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        /// <exception cref="CommitException"/>
        void Commit();

        /// <summary>
        /// Asynchronously commit the work made by this <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        /// <exception cref="ConcurrencyException">
        ///     Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        /// <exception cref="CommitException"/>
        Task CommitAsync(CancellationToken ct);

	    /// <summary>
		/// Prepares a given <see cref="IQueryable{T}"/> for asynchronous work.
	    /// </summary>
	    /// <param name="queryable">The query to wrap</param>
	    /// <typeparam name="T">The query item type</typeparam>
	    /// <returns>An <see cref="IAsyncQueryable{T}"/> instance, wrapping the given query</returns>
	    IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable);
    }
}
