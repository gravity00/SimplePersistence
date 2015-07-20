using System.Data.Entity;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.WebApi.UoW.EF.Repository
{
    public class LogRepository : EFRepository<Log, long>, ILogRepository
    {
        public LogRepository(DbContext dbContext)
            : base(dbContext, (log, l) => log.Id == l)
        {

        }
    }
}