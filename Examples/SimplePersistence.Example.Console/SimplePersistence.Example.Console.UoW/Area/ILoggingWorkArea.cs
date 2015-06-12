using SimplePersistence.Example.Console.UoW.Repository.Logging;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.Console.UoW.Area
{
    public interface ILoggingWorkArea : IWorkArea
    {
        IApplicationRepository Applications { get; }
        ILevelRepository Levels { get; }
        ILogRepository Logs { get; }
    }
}
