using Autofac;

namespace Davli.Framework.DI
{
    public class DavliDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<ITransientLifetime>()).AsSelf().AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<IScopedLifetime>()).AsSelf().AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<ISingletonLifetime>()).AsSelf().AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
