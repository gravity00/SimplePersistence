using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimplePersistence.Example.Console.Models;
using SimplePersistence.Example.Console.Models.Logging;
using SimplePersistence.Example.Console.UoW.Repository.Logging;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.Console.UoW.EF.Repository.Logging
{
    public class LogRepository : EFRepository<Log, long>, ILogRepository
    {
        public LogRepository(DbContext dbContext)
            : base(dbContext, (e, id) => e.Id == id)
        {
        }

        public IEnumerable<Log> GetAllByLogger(string logger)
        {
            return GetAllByLoggerQuery(logger).ToArray();
        }

        public async Task<IEnumerable<Log>> GetAllByLoggerAsync(string logger, CancellationToken ct)
        {
            return await GetAllByLoggerQuery(logger).ToArrayAsync(ct);
        }

        private IQueryable<Log> GetAllByLoggerQuery(string logger)
        {
            return DbSet.Where(e => e.Logger == logger);
        }
    }
}