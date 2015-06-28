using System;
using System.Data.Entity;
using SimplePersistence.Example.WebApi.UoW.Area;
using SimplePersistence.Example.WebApi.UoW.EF.Repository;
using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.WebApi.UoW.EF.Area
{
    public class LoggingWorkArea : EFWorkArea, ILoggingWorkArea
    {
        private readonly Lazy<IApplicationRepository> _lazyApplicationRepository;
        private readonly Lazy<ILevelRepository> _lazyLevelRepository;
        private readonly Lazy<ILogRepository> _lazyLogRepository;

        public LoggingWorkArea(DbContext context) : base(context)
        {
            _lazyApplicationRepository = new Lazy<IApplicationRepository>(() => new ApplicationRepository(context));
            _lazyLevelRepository = new Lazy<ILevelRepository>(() => new LevelRepository(context));
            _lazyLogRepository = new Lazy<ILogRepository>(() => new LogRepository(context));
        }

        public IApplicationRepository Applications { get { return _lazyApplicationRepository.Value; } }
        public ILevelRepository Levels { get { return _lazyLevelRepository.Value; } }
        public ILogRepository Logs { get { return _lazyLogRepository.Value; } }
    }
}
