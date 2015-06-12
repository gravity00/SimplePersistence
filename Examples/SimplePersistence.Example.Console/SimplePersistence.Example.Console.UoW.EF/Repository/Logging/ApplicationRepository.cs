using System.Data.Entity;
using SimplePersistence.Example.Console.Models;
using SimplePersistence.Example.Console.Models.Logging;
using SimplePersistence.Example.Console.UoW.Repository.Logging;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.Console.UoW.EF.Repository.Logging
{
    public class ApplicationRepository : EFRepository<Application, string>, IApplicationRepository
    {
        public ApplicationRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
