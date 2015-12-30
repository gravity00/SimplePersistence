using System;
using NHibernate;
using SimplePersistence.Example.WebApi.UoW.Area;
using SimplePersistence.Example.WebApi.UoW.NH.Repository;
using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW.NH;

namespace SimplePersistence.Example.WebApi.UoW.NH.Area
{
    public class LoggingWorkArea : NHWorkArea, ILoggingWorkArea
    {
        private readonly Lazy<IApplicationRepository> _lazyApplicationRepository;
        private readonly Lazy<ILevelRepository> _lazyLevelRepository;
        private readonly Lazy<ILogRepository> _lazyLogRepository;

        public LoggingWorkArea(ISession session) : base(session)
        {
            _lazyApplicationRepository = new Lazy<IApplicationRepository>(() => new ApplicationRepository(session));
            _lazyLevelRepository = new Lazy<ILevelRepository>(() => new LevelRepository(session));
            _lazyLogRepository = new Lazy<ILogRepository>(() => new LogRepository(session));
        }

        public IApplicationRepository Applications { get { return _lazyApplicationRepository.Value; } }
        public ILevelRepository Levels { get { return _lazyLevelRepository.Value; } }
        public ILogRepository Logs { get { return _lazyLogRepository.Value; } }
    }
}
