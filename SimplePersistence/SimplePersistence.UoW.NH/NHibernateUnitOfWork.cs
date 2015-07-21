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

using System.Linq;

namespace SimplePersistence.UoW.NH
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using NHibernate;
    using Exceptions;

    /// <summary>
    /// An implementation compatible with NHibernate for the Unit of Work pattern.
    /// Underline, it also uses work scopes (see: <see cref="ScopeEnabledUnitOfWork"/>).
    /// </summary>
    public abstract class NHibernateUnitOfWork : ScopeEnabledUnitOfWork, INHibernateUnitOfWork
    {
        private volatile ISession _session;
        private ITransaction _transaction;

        /// <summary>
        /// Creates a new unit of work that will extract the current <see cref="ISession"/> using <see cref="ISessionFactory.OpenSession()"/>
        /// </summary>
        /// <param name="sessionFactory">The session factory</param>
        protected NHibernateUnitOfWork(ISessionFactory sessionFactory)
            : this(sessionFactory.OpenSession()) { }

        /// <summary>
        /// Creates a new unit of work that will use the given <see cref="ISession"/>
        /// </summary>
        /// <param name="session">The database session</param>
        protected NHibernateUnitOfWork(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Allows an <see cref="T:System.Object"/> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object"/> is reclaimed by garbage collection.
        /// </summary>
        ~NHibernateUnitOfWork()
        {
            Dispose(false);
        }

        #region INHibernateUnitOfWork

        /// <summary>
        /// The database session
        /// </summary>
        public ISession Session { get { return _session; } }

        #endregion

        #region ScopeEnabledUnitOfWork

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        /// current instance for any subsequent work
        /// </summary>
        protected override void OnBegin()
        {
            _transaction = _session.BeginTransaction();
        }

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        /// current instance for any subsequent work
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        protected override Task OnBeginAsync(CancellationToken ct)
        {
            return Task.Factory.StartNew(OnBegin, ct);
        }

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
        /// made by this instance
        /// </summary>
        protected override void OnCommit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (StaleObjectStateException e)
            {
                throw new ConcurrencyException(e);
            }
        }

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
        /// made by this instance
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        protected override Task OnCommitAsync(CancellationToken ct)
        {
            return Task.Factory.StartNew(OnCommit, ct);
        }

        /// <summary>
        /// Prepares a given <see cref="IQueryable{T}"/> for asynchronous work.
        /// </summary>
        /// <param name="queryable">The query to wrap</param>
        /// <typeparam name="T">The query item type</typeparam>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> instance, wrapping the given query</returns>
        public override IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable)
        {
            return new NHAsyncQueryable<T>(queryable);
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
        /// Disposes the wrapped session
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _session.Dispose();
            _session = null;
        }

        #endregion
    }
}