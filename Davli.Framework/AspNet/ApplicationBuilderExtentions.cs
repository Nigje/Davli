using Microsoft.AspNetCore.Builder;
using Davli.Framework.Options;

namespace Davli.Framework.AspNet
{
    /// <summary>
    /// Application builder extentions.
    /// </summary>
    public static class ApplicationBuilderExtentions
    {
        //*********************************************************************************************************
        //Variables:
        //*********************************************************************************************************
        /// <summary>
        /// Init app.
        /// </summary>
        /// <param name="app"></param>
        public static void UseDavliApplication(this IApplicationBuilder app)
        {
            ConfigSwagger(app);
        }
        //*********************************************************************************************************
        /// <summary>
        /// congig swagger apps.
        /// </summary>
        /// <param name="app"></param>
        private static void ConfigSwagger(IApplicationBuilder app)
        {
            SwaggerOption swaggerOption = OptionService.GetOption<SwaggerOption>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(option =>
            {
                option.SerializeAsV2 = true;
                option.RouteTemplate = swaggerOption.JsonRoute;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerOption.Name}/swagger.json", swaggerOption.Name);
            });
        }
        //*********************************************************************************************************
    }
}
