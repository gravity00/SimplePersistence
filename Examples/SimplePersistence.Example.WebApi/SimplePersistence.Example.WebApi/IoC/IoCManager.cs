using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using SimplePersistence.Example.WebApi.IoC.Installers;

namespace SimplePersistence.Example.WebApi.IoC
{
    public static class IoCManager
    {
        public static T RegisterAllDependencies<T>(this T container)
            where T : IWindsorContainer
        {
            container.Kernel
                .AddFacility<TypedFactoryFacility>()
                .Resolver
                .AddSubResolver(
                    new ArrayResolver(container.Kernel, true));

            container.Install(
                new UnitOfWorkInstaller(),
                new ControllerInstaller());

            return container;
        }
    }
}