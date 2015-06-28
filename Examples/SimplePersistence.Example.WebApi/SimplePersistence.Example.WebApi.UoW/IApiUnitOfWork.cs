using SimplePersistence.Example.WebApi.UoW.Area;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.WebApi.UoW
{
    public interface IApiUnitOfWork : IUnitOfWork
    {
        ILoggingWorkArea Logging { get; }
    }
}
