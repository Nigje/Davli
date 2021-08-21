# Davli 

Davli is a framework based on ASP.Net Core for web projects with requirements such as high-performance, scalability, reliability, extensibility. Davli uses the update and famous technologies in ASP.Net Core. Davli is the summary of different frameworks. The following is a list of technologies that are used in Davli. I'm going to regularly update the document in the next commits.

### Technologies:

- .Net5
- EF Core
- Autofac
- Swagger
- MinIO
- SQL Server

## Integrate with a project

Davli is designed for ASP.Net Web API applications and there is a sample project that you can analyze Davli. To integrate it with your project you should do some changes in `Program.cs` and Sturtup.cs. At the first step, you should register `AutofacServiceProviderFactory` by using it as the following in the `Program.cs` file to integrate Autofac with project. 

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

At the second step, register Davli's services by `AddDavliServices` in `ConfigureServices` function at `Sturtup.cs` file.

```c#
services.AddDavliServices(Configuration);
```

Also, to use SQL SERVER database add the following code to `ConfigureServices`  function after registering Davli's `serviecs. SampleApiDbContext` is your `DbContext` and you will define you poco objects there. In the EF Core section we will explain more details.

```C#
services.AddDbContext<DavliDBContext, SampleApiDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Davli")),
                ServiceLifetime.Transient);
```

Your ConfigureServices will lookl like the following code.

```C#
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



## Migrations

To add new models use the following code in command line.

```powershell
dotnet ef migrations add InitialModels --project Davli.Framework.SampleApi
```

if you got a error like the following message, update your EF tools by `dotnet tool update --global dotnet-ef`

> The Entity Framework tools version 'x.x.x' is older than that of the runtime 'y.y.y'. Update the tools for the latest features and bug fixes.
> The name 'InitialModels' is used by an existing migration.

```powershell
dotnet tool update --global dotnet-ef
```



## Exceptions

When an exception occurs in the system the action result will be such as the following object and response HttpStatusCode will be 4xx or 5xx. HttopStatusCode in the range of 4xx shows the exception is because of user behavior (client-side) and 5xx shows it has occurred because of system defects (server-side). In the SampleApi project, there is an `ExceptionController` that you can use to throw an exception and see the result.

```json
{
  "__unauthorizedRequest": true,
  "__wrapped": true,
  "__traceId": "",
  "error": {
    "errorCode": "USER_NOT_FOUND",
    "message": "User not found.",
    "details": "",
    "source": ""
  }
}
```

 The above response had HttpStatusCode 404 that shows the not found exception has occured. Depend on the application is running on production or other environments, the `message`, `details`, and `source` can have more details about exceptions. 

- **__unauthorizedReques**t: shows the request is authorized or not. In the above example, the request is not authorized.

- **__wrapped**: shows the result is wrapped or not. Now, by default, all exceptions are wrapped.

- **__traceId**: TraceId will use to track a request over other systems and services. It is not implemented.

- **error.errorCode**: ErrorCode shows the exception reason. When a developer throws a `DavliException`, he/she will fill this field.

- **error.Message**: Message shows the exception reason.  

- **error.Details**: Details shows details of exceptions such as stack trace and inner exception details. It is always empty in production environment.

- **error.Source**: The source shows the service that has raised the exception. If you use microservice architecture, It would show the name of the service and its version. The following config is needed in `appsetting. json` (It is always empty in production).

  

```json
{
  "ServiceInfo": {
    "Name": "Service Name",
    "Description": "Service Description",
    "Version": "1.0"
  }
}
```



Davli has its Exception that is derived from Exception. Also, there are other exceptions that are derived from `DavliException`. We suggest to use `DavliException` and you can define new exceptions by driving `DavliException`. If you have defined a new exception that is derived from `DavliException`, check `GlobalExceptionFilter.cs` class for more details about exceptions.

```c#
public class DavliException : Exception 

{
    /// <summary>
    /// Error code that indicates a summary of error by using some words or numbers.
    /// </summary>
​	public string ErrorCode { get; protected set; }
    
    /// <summary>
    /// Technical-details are not allowed to be shown to the user.
    /// Just log them or use them internally by software-technicians.
    /// </summary>
    public string TechnicalMessage { get; protected set; }
}
```



- DavliException: This exception use generally and its HttpStatusCode will be 500.

- DavliExceptionNotFound: This exception use to show NotFoundException and HttpStatusCode will be 404.

- DavliExceptionInvalidParameter: This exception use to show InvalidParameter and HttpStatusCode will be 422.

- DavliBusinessException: This exception use to show BusinessException and HttpStatusCode will be 400 (recommended for exceptions that should be thrown because of a business defect).

- DavliExceptionBadRequest: This exception use to show BadRequestand HttpStatusCode will be 400.

- DavliExceptionExternalService: This exception shows during calling other API an exception has occurred and its HttpStatusCode can be set manually.

- Exception:  This exception use generally and its HttpStatusCode will be 500.

  

## Swagger

[Swagger](https://swagger.io/tools/swagger-ui/) is an Interface Description Language for describing RESTful APIs expressed using JSON. Swagger UI allows anyone — be it your development team or your end consumers — to visualize and interact with the API’s resources without having any of the implementation logic in place. It’s automatically generated from your OpenAPI (formerly known as Swagger) Specification, with the visual documentation making it easy for back end implementation and client side consumption. Swagger UI is embedded in Davli and just needs to add the following config to `appsetting.json`. To access Swagger UI use `{hostname}/swagger` address (you can change default `launchUrl` at your project `Properties/launchSettings.json` file).  

```json
{
  "SwaggerOption": {
    "JsonRoute": "swagger/{documentName}/swagger.json",
    "Description": "Sample Api",
    "Name": "nameofApi",
    "UIEndpoint": "v1/swagger.json",
    "Title": "Title of Api",
    "Version": "Version of Api"
  }
}
```

