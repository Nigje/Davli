using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Options
{
    /// <summary>
    /// Service info that is filled from appsetting.
    /// </summary>
    public class ServiceInfoOption: IOptionModel
    {
        /// <summary>
        /// Service name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Service description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Service version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Name nad version of service.
        /// </summary>
        public string FullName => $"{Name} - {Version}";

        /// <summary>
        /// Service Now date.
        /// </summary>
        public DateTime ServerDateTime => DateTime.Now;
    }
}
