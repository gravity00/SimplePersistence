using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SimplePersistence.Example.Console.Models;
using SimplePersistence.Example.Console.Models.Logging;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.Console.UoW.Repository.Logging
{
    public interface ILogRepository : IRepository<Log, long>
    {
        IEnumerable<Log> GetAllByLogger(string logger);

        Task<IEnumerable<Log>> GetAllByLoggerAsync(string logger, CancellationToken ct);
    }
}