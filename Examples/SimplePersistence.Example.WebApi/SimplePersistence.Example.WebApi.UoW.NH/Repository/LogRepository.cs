using NHibernate;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW.NH;

namespace SimplePersistence.Example.WebApi.UoW.NH.Repository
{
    public class LogRepository : NHibernateRepository<Log, long>, ILogRepository
    {
        public LogRepository(ISession session)
            : base(session, (log, id) => log.Id == id)
        {
        }
    }
}