using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using SimplePersistence.Example.WebApi.IoC;
using SimplePersistence.Example.WebApi.IoC.Factories;

namespace SimplePersistence.Example.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        private volatile IWindsorContainer _container;

        protected void Application_Start()
        {
            _container = new WindsorContainer().RegisterAllDependencies();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Services.Replace(
                typeof (IHttpControllerActivator),
                new WindsorApiControllerFactory(_container));
        }

        protected void Application_End()
        {
            var container = _container;
            if (container == null) return;

            _container = null;
            container.Dispose();
        }
    }
}
