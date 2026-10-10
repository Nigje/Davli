# Tests, CI, and reproducible demo plan

This document proposes future work against the original .NET 5 implementation. No test project, CI workflow, demo script, SQLite provider, or application refactor is included in this documentation update. The earlier implementation changes have been reverted. Do not use their test results as evidence for the restored code.

## Meaningful tests

Use a .NET 5-compatible xUnit test project. Exercise the real sample API with `WebApplicationFactory<Program>` and a disposable relational database. SQL Server coverage is preferred for the existing provider; SQLite would require a deliberate sample/provider change and would not prove SQL Server compatibility. Avoid using EF Core InMemory as evidence of relational behavior.

| Scenario | Observable assertion | Reason |
| --- | --- | --- |
| CRUD round trip | Create a user, retrieve it in a new request, update it, list it, delete it, then receive 404. Confirm database state. | Proves wiring, mapping, persistence, and deletion behavior together. |
| Missing user | GET, PUT, and DELETE for absent IDs return 404 and the expected error envelope. | Protects the API contract. |
| Invalid name | Missing, blank, and over-100-character names return a deliberate client error without inserting data. | Desired behavior; the current DTO lacks these validation rules, so tests may expose necessary changes. |
| Creation/modification audit | Save and reload entities; creation metadata stays constant on update, modification metadata changes appropriately. Exercise synchronous, asynchronous, and boolean save overloads. | Finds gaps in auditing rather than merely checking helper calls. |
| Audit identity | Anonymous identity is distinct from a real user; a request header cannot impersonate another actor. | Desired trust boundary; the current implementation reads `UserId` from headers. |
| Dependency lifetimes | Scoped components are shared within a scope and isolated between scopes; transient/singleton behavior matches its declared contract. | Checks real resolution and disposal behavior. |
| Error redaction | Unexpected production failures hide internal messages and expose a request trace ID. | Desired behavior; the current filter returns exception messages and an empty trace ID. |
| Repeated startup | Independent hosts start without duplicate option registrations or leaking configuration. | Exposes the current static options-cache limitation. |
| Optional MinIO adapter | Upload/download bytes round-trip, returned streams are readable from the documented position, and missing objects fail predictably. | Tests storage behavior with a disposable MinIO instance rather than mocking SDK calls. |

Run tests against isolated databases with synthetic data and guaranteed cleanup. Initially distinguish characterization tests for existing behavior from regression tests for proposed fixes. Do not silently change assertions to make known bugs pass.

## CI design

A proposed GitHub Actions workflow should run on pushes and pull requests, with manual dispatch available:

1. Check out the repository with read-only repository permissions.
2. Install a pinned .NET 5 SDK, for example `5.0.408`, without changing target frameworks.
3. Restore dependencies and build the solution in Release.
4. Provision the chosen disposable test database and wait for readiness.
5. Run the test suite and upload TRX reports even when tests fail.
6. Start the sample API, execute the HTTP smoke walkthrough, and stop all demo processes in cleanup.

Windows is a useful initial runner for consistency with the original LocalDB-oriented sample. SQL Server provisioning must be explicit; do not assume a runner has the same LocalDB setup as a developer laptop. MinIO coverage can be a separate optional integration job.

CI is complete only after a real remote run passes from a clean checkout. Add badges and passing-test claims at that point. There is no active workflow in this documentation update.

## Reproducible demo acceptance criteria

The eventual demo should provide one documented setup path, a pinned SDK, explicit database configuration and initialization, synthetic data, a known loopback URL, a smoke script, and cleanup/reset instructions. A fresh reviewer machine should be able to follow the guide without undocumented services or credentials.

Use the [current-source walkthrough](DEMO.md) as a starting point. Resolve startup/schema issues explicitly before calling it verified. If adding SQLite later, document it as a separate demo provider while preserving SQL Server as the default.

## Implementation order

1. Establish a clean build and document the original SQL Server setup.
2. Add CRUD and missing-record characterization tests.
3. Add focused regression tests for validation, auditing, lifetimes, configuration, and errors; fix the discovered failures in separate reviewable changes.
4. Automate the local smoke walkthrough and cleanup.
5. Add CI, verify its first remote run, and record evidence in the README.

.NET 5 remains unchanged throughout this plan.
