using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace SimplePersistence.Example.WebApi.IoC.Factories
{
    public class WindsorApiControllerFactory : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        /// Creates a new factory that will use the given container that will resolve
        /// the api controllers
        /// </summary>
        /// <param name="container">The container to resolve the api controllers</param>
        /// <exception cref="ArgumentNullException">If the container is null</exception>
        public WindsorApiControllerFactory(IWindsorContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            _container = container;
        }

        /// <summary>
        /// Creates an <see cref="T:System.Web.Http.Controllers.IHttpController"/> object.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Web.Http.Controllers.IHttpController"/> object.
        /// </returns>
        /// <param name="request">The message request.</param><param name="controllerDescriptor">The HTTP controller descriptor.</param><param name="controllerType">The type of the controller.</param>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var scope = _container.RequireScope();
            var controller = (IHttpController)_container.Resolve(controllerType);

            request.RegisterForDispose(
                new Release(
                    () =>
                    {
                        _container.Release(controller);
                        scope.Dispose();
                    }));

            return controller;
        }


        private class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release();
            }
        }
    }
}