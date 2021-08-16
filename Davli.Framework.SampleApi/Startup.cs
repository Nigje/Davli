using Davli.Framework.AspNet;
using Davli.Framework.Orm.EntityFrameworkCode;
using Davli.Framework.SampleApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Davli.Framework.DI;
using Davli.Framework.SampleApi.Models.Modules;

namespace Davli.Framework.SampleApi
{
    public class Startup
    {
        //*********************************************************************************************************
        //Variables:
        public IConfiguration Configuration { get; }
        //*********************************************************************************************************
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //*********************************************************************************************************
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDavliServices(Configuration);
            services.AddDbContext<DavliDBContext, SampleApiDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Davli")),
                ServiceLifetime.Transient); 
        }
        //*********************************************************************************************************
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDavliApplication();


            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Generate database to test api.
            //For production use migration and database update in the command line and comment the following scop.
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<DavliDBContext>())
                {
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
                    
            }
            
        }
        //*********************************************************************************************************
        /// <summary>
        /// ConfigureContainer is where you can register things directly
        /// with Autofac. This runs after ConfigureServices so the things
        /// here will override registrations made in ConfigureServices.
        /// Don't build the container; that gets done for you by the factory.
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DIModule());
            builder.RegisterModule(new DavliDIModule());
        }
        //*********************************************************************************************************
    }
}
