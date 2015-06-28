using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.WebApi.UoW.Area
{
    public interface ILoggingWorkArea : IWorkArea
    {
        IApplicationRepository Applications { get; }
        ILevelRepository Levels { get; }
        ILogRepository Logs { get; }
    }
}
