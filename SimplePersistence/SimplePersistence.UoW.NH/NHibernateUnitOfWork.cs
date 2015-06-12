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
namespace SimplePersistence.UoW.NH
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using NHibernate;
    using Exceptions;

    public abstract class NHibernateUnitOfWork : ScopeEnabledUnitOfWork, INHibernateUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        protected NHibernateUnitOfWork(ISessionFactory sessionFactory)
            : this(sessionFactory.OpenSession()) { }

        protected NHibernateUnitOfWork(ISession session)
        {
            _session = session;
        }

        ~NHibernateUnitOfWork()
        {
            Dispose(false);
        }

        #region INHibernateUnitOfWork

        public ISession Session { get { return _session; } }

        #endregion

        #region ScopeEnabledUnitOfWork

        protected override void OnBegin()
        {
            _transaction = _session.BeginTransaction();
        }

        protected override Task OnBeginAsync(CancellationToken ct)
        {
            return Task.Factory.StartNew(OnBegin, ct);
        }

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

        protected override Task OnCommitAsync(CancellationToken ct)
        {
            return Task.Factory.StartNew(OnCommit, ct);
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _session.Dispose();
        }

        #endregion
    }
}