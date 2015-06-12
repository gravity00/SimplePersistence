using SimplePersistence.Example.Console.Models;
using SimplePersistence.Example.Console.Models.Logging;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.Console.UoW.Repository.Logging
{
    public interface IApplicationRepository : IRepository<Application, string>
    {
    }
}
