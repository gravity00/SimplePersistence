using System.Data.Entity;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Example.WebApi.UoW.Repository;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.WebApi.UoW.EF.Repository
{
    public class LevelRepository : EFRepository<Level, string>, ILevelRepository
    {
        public LevelRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
