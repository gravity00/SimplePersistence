using NHibernate;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW.NH;

namespace SimplePersistence.Example.WebApi.UoW.NH.Repository
{
    public class LevelRepository : NHibernateRepository<Level, string>, ILevelRepository
    {
        public LevelRepository(ISession session)
            : base(session, (level, id) => level.Id == id)
        {
        }
    }
}