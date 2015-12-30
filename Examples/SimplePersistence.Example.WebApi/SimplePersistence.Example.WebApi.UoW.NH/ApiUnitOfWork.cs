using System;
using NHibernate;
using SimplePersistence.Example.WebApi.UoW.Area;
using SimplePersistence.Example.WebApi.UoW.NH.Area;
using SimplePersistence.UoW.NH;

namespace SimplePersistence.Example.WebApi.UoW.NH
{
    public class ApiUnitOfWork : NHibernateUnitOfWork, IApiUnitOfWork
    {
        private readonly Lazy<ILoggingWorkArea> _lazyLoggingWorkArea;

        public ApiUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            _lazyLoggingWorkArea = new Lazy<ILoggingWorkArea>(() => new LoggingWorkArea(Session));
        }

        public ILoggingWorkArea Logging { get { return _lazyLoggingWorkArea.Value; } }
    }
}
