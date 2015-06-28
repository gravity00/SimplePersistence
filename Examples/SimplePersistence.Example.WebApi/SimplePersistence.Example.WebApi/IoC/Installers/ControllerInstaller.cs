using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.OData;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SimplePersistence.Example.WebApi.IoC.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IHttpController>()
                    .WithService.Self()
                    .LifestyleTransient(),
                Component.For<MetadataController>().LifestyleTransient());
        }
    }
}