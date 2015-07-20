using System.Data.Entity;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.WebApi.UoW.EF.Repository
{
    public class ApplicationRepository : EFRepository<Application, string>, IApplicationRepository
    {
        public ApplicationRepository(DbContext dbContext)
            : base(dbContext, (application, s) => application.Id == s)
        {

        }
    }
}