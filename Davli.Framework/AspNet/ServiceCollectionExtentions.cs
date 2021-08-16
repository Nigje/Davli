using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Davli.Framework.AspNet.Mvc.ExceptionHandling;
using Davli.Framework.AspNet.Mvc.Results;
using Davli.Framework.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.AspNet
{
    /// <summary>
    /// service collection extiontions.
    /// </summary>
    public static class ServiceCollectionExtentions
    {
        //*********************************************************************************************************
        //Variables:

        //*********************************************************************************************************
        /// <summary>
        /// Init services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDavliServices(this IServiceCollection services, IConfiguration configuration)
        {
            DependencyInjection(services);
            OptionService.SetOptions(configuration);
            ConfigSwagger(services);
            ConfigMvc(services);
            services.AddHttpContextAccessor();
            //ConfigNLog(services, configuration); todo: obsoleted...
        }

        //*********************************************************************************************************
        /// <summary>
        /// Config NLog.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void ConfigNLog(IServiceCollection services, IConfiguration configuration)
        {
            // services.AddLogging(loggingBuilder =>
            // {
            //     loggingBuilder.ClearProviders();
            //     loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
            //     loggingBuilder.AddNLog(configuration);
            // });
        }

        //*********************************************************************************************************
        /// <summary>
        /// congigMVC.
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigMvc(IServiceCollection services)
        {
            //We can use AddMvcCore but it needs to add additional config, so I have preferred to use AddMvc with too more config.
            services.AddMvc()
               .AddMvcOptions(option => option.Filters.AddService(typeof(GlobalExceptionFilter)))
               .AddMvcOptions(option => option.Filters.AddService(typeof(ResultFilter)));

            //So we can use the following configuration method to add Filters.
            //services.Configure<MvcOptions>(options => { optionsFilters.AddService(typeof(Some-ResultFilter)); });
        }

        //*********************************************************************************************************
        /// <summary>
        /// Init Dependency Injection
        /// </summary>
        /// <param name="services"></param>
        private static void DependencyInjection(IServiceCollection services)
        {
            services.AddHttpClient();
        }

        //*********************************************************************************************************
        /// <summary>
        /// Init config swagger.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void ConfigSwagger(IServiceCollection services)
        {
            SwaggerOption swaggerOption = OptionService.GetOption<SwaggerOption>();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

                options.SwaggerDoc(swaggerOption.Name, new OpenApiInfo
                {
                    Title = swaggerOption.Title,
                    Version = swaggerOption.Version,
                    Description = swaggerOption.Description
                });

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                if (File.Exists(commentsFile))
                {
                    options.IncludeXmlComments(commentsFile);
                }
                //options.DescribeAllEnumsAsStrings();
            });
        }
        //*********************************************************************************************************
    }
}
