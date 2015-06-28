using System;
using System.Configuration;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SimplePersistence.Example.WebApi.UoW;
using SimplePersistence.Example.WebApi.UoW.EF;
using SimplePersistence.Example.WebApi.UoW.EF.Mapping;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.WebApi.IoC.Installers
{
    public class UnitOfWorkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ApiDbContext>()
                    .UsingFactoryMethod(
                        k => new ApiDbContext(
                            ConfigurationManager.ConnectionStrings["ApiDbContext"].ConnectionString))
                    .LifestyleTransient(),

                Component.For<UnitOfWorkSelector>().LifestyleSingleton(),
                Component.For<IUnitOfWorkFactory>()
                    .AsFactory(x => x.SelectedWith<UnitOfWorkSelector>()).LifestyleSingleton(),

                Component.For<IApiUnitOfWork>()
                    .ImplementedBy<ApiUnitOfWork>()
                    .Named(typeof (IApiUnitOfWork).Name)
                    .LifestyleTransient());
        }
    }

    public class UnitOfWorkSelector : DefaultTypedFactoryComponentSelector
    {
        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            return arguments.Length == 0 ? method.ReturnType : (arguments[0] as Type ?? method.ReturnType);
        }

        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (arguments.Length == 0) return method.ReturnType.Name;

            var type = arguments[0] as Type;
            return type == null ? method.ReturnType.Name : type.Name;
        }
    }
}