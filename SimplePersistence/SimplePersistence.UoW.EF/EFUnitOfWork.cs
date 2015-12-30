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
	using System.Data.Entity;
	using System.Linq;
    using System.Data.Entity.Infrastructure;
	using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Exceptions;

    /// <summary>
    /// An implementation compatible with Entity Framework for the Unit of Work pattern.
    /// Underline, it also uses work scopes (see: <see cref="ScopeEnabledUnitOfWork"/>).
    /// </summary>
    public abstract class EFUnitOfWork<TDbContext> : ScopeEnabledUnitOfWork, IEFUnitOfWork<TDbContext> 
        where TDbContext : DbContext
    {
        private readonly Task<int> _completedOnBeginAsyncTask;
        private readonly TDbContext _context;

        /// <summary>
        /// Creates a new object associated with the given database context
        /// </summary>
        /// <param name="context">The EF database context</param>
        /// <exception cref="ArgumentNullException">Thrown if the context is null</exception>
        protected EFUnitOfWork([NotNull] TDbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;

            var tcs = new TaskCompletionSource<int>();
            tcs.SetResult(0);
            _completedOnBeginAsyncTask = tcs.Task;
        }

        /// <summary>
        /// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~EFUnitOfWork()
        {
            Dispose(false);
        }

        #region IEFUnitOfWork

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        public TDbContext Context
	    {
		    get { return _context; }
	    }

	    #endregion

        #region ScopeEnabledUnitOfWork

        /// <summary>
        /// Since the underline ORM is EF, no action is executed
        /// </summary>
        protected override void OnBegin()
        {

        }

        /// <summary>
		/// Since the underline ORM is EF, no action is executed, returning immediately
        /// </summary>
        /// <param name="ct">The cancelation token</param>
        /// <returns>A completed task of this operation</returns>
        protected override Task OnBeginAsync(CancellationToken ct)
        {
            return _completedOnBeginAsyncTask;
        }

        /// <summary>
        /// Commits the current work by invoking <see cref="DbContext.SaveChanges"/>.
        /// </summary>
        /// <exception cref="ConcurrencyException">Thrown if a <see cref="DbUpdateConcurrencyException"/> is received</exception>
        protected override void OnCommit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException(e);
            }
        }

        /// <summary>
		/// Commits the current work by invoking <see cref="DbContext.SaveChangesAsync()"/>.
        /// </summary>
		/// <param name="ct">The cancelation token</param>
        /// <returns>The task for this operation</returns>
		/// <exception cref="ConcurrencyException">Thrown if a <see cref="DbUpdateConcurrencyException"/> is received</exception>
        protected override async Task OnCommitAsync(CancellationToken ct)
        {
            try
            {
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException(e);
            }
        }

        /// <summary>
        /// Prepares a given <see cref="System.Linq.IQueryable{T}"/> for asynchronous work,
        /// wrapping using an <see cref="EFAsyncQueryable{T}"/>
        /// </summary>
        /// <param name="queryable">The query to wrap</param>
        /// <typeparam name="T">The query item type</typeparam>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> instance, wrapping the given query</returns>
        public override IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable)
	    {
		    return new EFAsyncQueryable<T>(queryable);
	    }

	    #endregion

        #region IDisposable

        /// <summary>
        /// Disposes all resources managed by this instance
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the EF database context
        /// </summary>
        /// <param name="disposing">Disposes if true, else does nothing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// An implementation compatible with Entity Framework for the Unit of Work pattern.
    /// Underline, it also uses work scopes (see: <see cref="ScopeEnabledUnitOfWork"/>).
    /// </summary>
    public abstract class EFUnitOfWork : EFUnitOfWork<DbContext>, IEFUnitOfWork
    {
        /// <summary>
        /// Creates a new object associated with the given database context
        /// </summary>
        /// <param name="context">The EF database context</param>
        /// <exception cref="ArgumentNullException">Thrown if the context is null</exception>
        protected EFUnitOfWork([NotNull] DbContext context) : base(context)
        {
        }
    }
}