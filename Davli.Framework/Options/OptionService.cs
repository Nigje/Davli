using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Davli.Framework.Options
{
    /// <summary>
    /// This class maintains read/write of option models.
    /// </summary>
    public class OptionService
    {
        //*******************************************************************************************
        //variables:

        /// <summary>
        /// As a cache to keep our option model values throughout the app.
        /// </summary>
        private static Dictionary<Type, IOptionModel> OptionServicceCache = new Dictionary<Type, IOptionModel>();
        //*******************************************************************************************
        /// <summary>
        /// Get option value.
        /// </summary>
        /// <typeparam name="Tout"></typeparam>
        /// <returns></returns>
        public static Tout GetOption<Tout>() where Tout : class
        {
            return (Tout)OptionServicceCache[typeof(Tout)];
        }
        //*******************************************************************************************
        /// <summary>
        /// Set option values. Always is executed just for one time at startup.
        /// </summary>
        /// <param name="configuration"></param>
        public static void SetOptions(IConfiguration configuration)
        {
            Assembly ass = Assembly.GetEntryAssembly();
            List<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IOptionModel))).ToList();
            foreach (var type in types)
            {
                var instance = (IOptionModel)Activator.CreateInstance(type);
                if (configuration.GetSection(type.Name).Exists())
                { 
                    configuration.GetSection(type.Name).Bind(instance); 
                }
                else
                {
                    configuration.GetSection(type.Name.Replace("Option", "")).Bind(instance);
                }

                OptionServicceCache.Add(type, instance);
            }
        }
        //*******************************************************************************************
    }
}
