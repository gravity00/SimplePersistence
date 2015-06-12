using SimplePersistence.Example.Console.UoW.Area;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.Console.UoW
{
    public interface IConsoleUnitOfWork : IUnitOfWork
    {
        ILoggingWorkArea Logging { get; }
    }
}
