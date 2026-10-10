# Current-source demo walkthrough

This is a documented starting point for the original sample, not a verified automated demo. The earlier demo implementation was reverted. No new scripts, provider changes, or SDK pinning file are supplied here.

## Prerequisites

- A .NET 5 SDK and ASP.NET Core 5 runtime. Use the same SDK version when implementing CI, for example `5.0.408`.
- SQL Server. The existing Development settings use Windows LocalDB and a database named `Dalvi`.
- A connection string configured for your local SQL Server instance.

MinIO is only needed to exercise the separate storage adapter; user CRUD does not use it.

## Build and run

From the repository root in PowerShell:

```powershell
dotnet restore Davli.Framework.sln
dotnet build Davli.Framework.sln --configuration Release
$env:ASPNETCORE_ENVIRONMENT = 'Development'
$env:ConnectionStrings__Davli = 'Server=(localdb)\MSSQLLocalDB;Database=Dalvi;Trusted_Connection=True;trustServerCertificate=true;'
dotnet run --project Davli.Framework.SampleApi --no-launch-profile --urls http://127.0.0.1:5080
```

Use the LocalDB connection only when that instance exists on your Windows machine; otherwise set the connection string for your own disposable SQL Server database. Keep real credentials outside tracked files.

Current startup invokes both `EnsureCreated` and `Migrate`, while no migrations are checked in. This must be resolved and tested before these commands qualify as a reproducible demo. Select one schema strategy: `EnsureCreated` for disposable demonstrations, or checked-in migrations for a durable schema. This document does not change startup.

## Walkthrough once startup succeeds

Open [Swagger](http://127.0.0.1:5080/swagger). The current Swagger JSON document is `/swagger/nameofApi/swagger.json`.

From another PowerShell terminal:

```powershell
$user = Invoke-RestMethod http://127.0.0.1:5080/api/user -Method Post -ContentType application/json -Body '{"name":"Portfolio User"}'
Invoke-RestMethod "http://127.0.0.1:5080/api/user/$($user.id)"
Invoke-RestMethod "http://127.0.0.1:5080/api/user/$($user.id)" -Method Put -ContentType application/json -Body '{"name":"Updated User"}'
Invoke-RestMethod http://127.0.0.1:5080/api/user
Invoke-RestMethod "http://127.0.0.1:5080/api/user/$($user.id)" -Method Delete
```

Then request the deleted ID and verify a 404 with `error.message` equal to `User not found.`. Record the actual status/body behavior during implementation rather than assuming it has been tested here.

Stop the server with Ctrl+C. Use a dedicated disposable database and remove only that database when resetting. The planned smoke script should create its own isolated database, stop the process it launched, and guarantee cleanup on success or failure.

The sample currently has no authentication middleware or endpoint authorization. The walkthrough uses synthetic local data. SQL Server setup, initialization, and a clean-machine smoke run remain prerequisites for calling the demo reproducible.
