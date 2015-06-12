using System;
using SimplePersistence.Example.Console.UoW.Area;
using SimplePersistence.Example.Console.UoW.EF.Mapping;
using SimplePersistence.Example.Console.UoW.EF.Repository.Logging;
using SimplePersistence.Example.Console.UoW.Repository.Logging;

namespace SimplePersistence.Example.Console.UoW.EF.Area
{
    public class LoggingWorkArea : ILoggingWorkArea
    {
        private readonly Lazy<IApplicationRepository> _lazyApplicationRepository;
        private readonly Lazy<ILevelRepository> _lazyLevelRepository;
        private readonly Lazy<ILogRepository> _lazyLogRepository;

        // ReSharper disable once SuggestBaseTypeForParameter
        public LoggingWorkArea(ConsoleDbContext context)
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
