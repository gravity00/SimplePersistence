using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.WebApi.UoW.Repository
{
    public interface IApplicationRepository : IRepository<Application, string>
    {
        
    }
}