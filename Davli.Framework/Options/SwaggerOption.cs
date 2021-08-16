
namespace Davli.Framework.Options
{
    /// <summary>
    /// Swagger config values in Configuration.
    /// </summary>
    public class SwaggerOption : IOptionModel
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// JsonRoute
        /// </summary>
        public string JsonRoute { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// UIEndpoint
        /// </summary>
        public string UIEndpoint { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

    }
}
