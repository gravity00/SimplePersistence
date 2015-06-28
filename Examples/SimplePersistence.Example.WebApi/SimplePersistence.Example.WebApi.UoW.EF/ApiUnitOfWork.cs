using System;
using SimplePersistence.Example.WebApi.UoW.Area;
using SimplePersistence.Example.WebApi.UoW.EF.Area;
using SimplePersistence.Example.WebApi.UoW.EF.Mapping;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.WebApi.UoW.EF
{
    public class ApiUnitOfWork : EFUnitOfWork<ApiDbContext>, IApiUnitOfWork
    {
        private readonly Lazy<ILoggingWorkArea> _lazyLoggingWorkArea;

        public ApiUnitOfWork(ApiDbContext context)
            : base(context)
        {
            _lazyLoggingWorkArea = new Lazy<ILoggingWorkArea>(() => new LoggingWorkArea(context));
        }

        public ILoggingWorkArea Logging { get { return _lazyLoggingWorkArea.Value; } }
    }
}
