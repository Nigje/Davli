# Davli 

Davli is a framework based on ASP.Net Core for web projects with requirements such as high-performance, scalability, reliability, extensibility. Davli uses the update and famous technologies in ASP.Net Core. The following is a list of technologies that are used in Davli.

### Technologies:

- [ ] .Net5
- [ ] EF Core
- [ ] Autofac
- [ ] Swagger
- [ ] MinIO
- [ ] SQL Server

## Integrate with a project

Davli is designed for ASP.Net Web API applications and there is a sample project that you can analyze Davli. To integrate it with your project you should do some changes in Program.cs and Sturtup.cs. At the first step, you should register AutofacServiceProviderFactory by using it as the following in the Program.cs file to integrate Autofac with project. 

```c#
public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
```

After that, add the following function to Sturtup.cs to define dependency injection roles. In Autofac section we will explain details.

```C#
public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DIModule());
            builder.RegisterModule(new DavliDIModule());
        }
```

At the second step, register Davli's services by AddDavliServices in ConfigureServices function at Sturtup.cs file.

```c#
services.AddDavliServices(Configuration);
```

Also, to use SQL SERVER database add the following code to ConfigureServices  function after registering Davli's serviecs. SampleApiDbContext is your DbContext and you will define you poco objects there. In the EF Core section we will explain more details.

```C#
services.AddDbContext<DavliDBContext, SampleApiDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Davli")),
                ServiceLifetime.Transient);
```

Your ConfigureServices will lookl like the following code.

```
public void ConfigureServices(IServiceCollection services)
        {
            services.AddDavliServices(Configuration);
            services.AddDbContext<DavliDBContext, SampleApiDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Davli")),
                ServiceLifetime.Transient); 
        }
```

Finally, add UseDavliApplication to your Configure function in Startup.cs file. your Configure function will look like the following code:

```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDavliApplication();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
```



#### Migrations

```powershell
dotnet ef migrations add InitialModels --project Davli.Framework.SampleApi
```

if you got a error like the following message, update your EF tools by `dotnet tool update --global dotnet-ef`

> The Entity Framework tools version 'x.x.x' is older than that of the runtime 'y.y.y'. Update the tools for the latest features and bug fixes.
> The name 'InitialModels' is used by an existing migration.

dotnet tool update --global dotnet-ef