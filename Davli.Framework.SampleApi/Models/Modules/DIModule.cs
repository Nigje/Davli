using Autofac;
using Davli.Framework.DI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Davli.Framework.SampleApi.Models.Modules
{
    public class DIModule : Autofac.Module
    {
        /// <summary>
        /// Register objects.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            #region First
            //Just register current dll objects.
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<ITransientLifetime>()).AsSelf().AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<IScopedLifetime>()).AsSelf().AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<ISingletonLifetime>()).AsSelf().AsImplementedInterfaces()
                .SingleInstance();
            #endregion

            #region Second model (not recommended)
            // serch all dll and register objects
            //var assemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
            //   .Select(Assembly.LoadFrom);

            //builder.RegisterAssemblyTypes(assemblies.ToArray())
            //    .Where(x => x.IsAssignableTo<ITransientLifetime>()).AsSelf().AsImplementedInterfaces()
            //    .InstancePerDependency();

            //builder.RegisterAssemblyTypes(assemblies.ToArray())
            //    .Where(x => x.IsAssignableTo<IScopedLifetime>()).AsSelf().AsImplementedInterfaces()
            //    .InstancePerRequest();

            //builder.RegisterAssemblyTypes(assemblies.ToArray())
            //    .Where(x => x.IsAssignableTo<ISingletonLifetime>()).AsSelf().AsImplementedInterfaces()
            //    .SingleInstance();
            #endregion
        }
    }
}

