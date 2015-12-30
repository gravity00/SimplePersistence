using NHibernate;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW.NH;

namespace SimplePersistence.Example.WebApi.UoW.NH.Repository
{
    public class ApplicationRepository : NHibernateRepository<Application, string>, IApplicationRepository
    {
        public ApplicationRepository(ISession session)
            : base(session, (application, id) => application.Id == id)
        {
        }
    }
}
