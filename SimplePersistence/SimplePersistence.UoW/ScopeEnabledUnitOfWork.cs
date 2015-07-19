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
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;

    /// <summary>
    /// Represents a scope enabled <see cref="IUnitOfWork"/> that guarantees any given
    /// <see cref="Begin"/>/<see cref="BeginAsync"/> and <see cref="Commit"/>/<see cref="CommitAsync"/> logic
    /// is only invoqued once for any given scope. Every scope information is thread safe.
	/// <example>
	/// <code>
	/// using(var uow = new ImplScopeEnabledUnitOfWork()){
	///     uow.Begin() // scope is incremented and OnBegin() is invoked
	///     // uow is used
	///     uow.Begin() // only scope is incremented 
	///     // uow is used
	///     uow.Commit(); // only scope is decremented
	///     // uow is used
	///     uow.Commit(); // scope is decremented and OnCommit() is invoked
	/// } // uow is disposed
	/// </code>
    /// </example>
    /// </summary>
    public abstract class ScopeEnabledUnitOfWork : IUnitOfWork
    {
        private static readonly Task<bool> CachedCompletedTask;

        static ScopeEnabledUnitOfWork()
        {
            var tcs = new TaskCompletionSource<bool>();
            tcs.TrySetResult(true);
            CachedCompletedTask = tcs.Task;
        }

        private int _currentScope;
        private readonly Guid _privateId = Guid.NewGuid();

		#region IUnitOfWork

        /// <summary>
        /// Prepares the <see cref="IUnitOfWork"/> to work by invoking <see cref="OnBegin"/>
        /// on first invocation. This method is thread safe and every invocation increments the scope accordingly.
        /// </summary>
        public void Begin()
        {
            var s = IncrementScope();
            if (s == 1)
                OnBegin();
        }

        /// <summary>
        /// Asynchronously prepares the <see cref="IUnitOfWork"/> to work by invoking <see cref="OnBeginAsync"/>
        /// on first invocation. This method is thread safe and every invocation increments the scope accordingly.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        public Task BeginAsync(CancellationToken ct)
        {
            var s = IncrementScope();
	        return s == 1 ? OnBeginAsync(ct) : CachedCompletedTask;
        }

        /// <summary>
        /// Commit the work made by this <see cref="IUnitOfWork"/> by invoking <see cref="OnCommit"/> when
        /// the scope ends. This method is thread safe and every invocation decrements the scope accordingly.
        /// </summary>
        /// <exception cref="ConcurrencyException">
        ///     Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        /// <exception cref="CommitException"/>
        public void Commit()
        {
            var s = DecrementScope();
            if (s < 0)
                throw new UndefinedScopeException();
            if (s != 0) return;

            try
            {
                OnCommit();
            }
            catch (UnitOfWorkException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CommitException(e);
            }
        }

        /// <summary>
        /// Asynchronously commit the work made by this <see cref="IUnitOfWork"/> by invoking <see cref="OnCommitAsync"/> 
        /// when the scope ends. This method is thread safe and every invocation decrements the scope accordingly.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        /// <exception cref="ConcurrencyException">
        ///     Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        /// <exception cref="CommitException"/>
        public Task CommitAsync(CancellationToken ct)
        {
            var s = DecrementScope();
            if (s < 0)
                throw new UndefinedScopeException();
            if (s != 0)
                return CachedCompletedTask;

            return
                OnCommitAsync(ct)
                    .ContinueWith(
                        t =>
                        {
                            if (!t.IsFaulted) return;

                            if (t.Exception == null)
                                throw new CommitException();

                            var ex = t.Exception.InnerException;
                            if (ex == null)
                                throw new CommitException();

                            if (ex.GetType() != typeof(UnitOfWorkException))
                                throw t.Exception;
                            throw new CommitException(ex);
                        }, ct);
        }

        /// <summary>
        /// Prepares a given <see cref="IQueryable{T}"/> for asynchronous work.
        /// </summary>
        /// <param name="queryable">The query to wrap</param>
        /// <typeparam name="T">The query item type</typeparam>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> instance, wrapping the given query</returns>
        public abstract IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable);

	    #endregion

        #region Object

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Concat("{ UoW : '", GetType().FullName, "', Id : '", _privateId, "' }");
        }

        #endregion

        /// <summary>
		/// Invoked once for any given scope, it should prepare the
		/// current instance for any subsequent work
		/// </summary>
        protected abstract void OnBegin();

		/// <summary>
		/// Invoked once for any given scope, it should prepare the
		/// current instance for any subsequent work
		/// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        protected abstract Task OnBeginAsync(CancellationToken ct);

		/// <summary>
		/// Invoked once for any given scope, it should commit any work
		/// made by this instance
		/// </summary>
        protected abstract void OnCommit();

		/// <summary>
		/// Invoked once for any given scope, it should commit any work
		/// made by this instance
		/// </summary>
		/// <param name="ct">The cancellation token</param>
		/// <returns>The task for this operation</returns>
        protected abstract Task OnCommitAsync(CancellationToken ct);

        /// <summary>
        /// Disposes all resources managed by this instance
        /// </summary>
        public abstract void Dispose();

        private int DecrementScope()
        {
            return Interlocked.Decrement(ref _currentScope);
        }

        private int IncrementScope()
        {
            return Interlocked.Increment(ref _currentScope);
        }
    }
}