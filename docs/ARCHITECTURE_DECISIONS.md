# Architecture decisions and proposed improvements

This is a source-based explanation of the original .NET 5 architecture. The improvements below are proposals, not implemented changes. Application code and dependency versions are restored to the cloned version.

## Framework and sample separation

`Davli.Framework` groups reusable infrastructure; `Davli.Framework.SampleApi` demonstrates consuming it through a small CRUD API. Keeping those projects separate makes the integration boundary visible and prevents sample-specific models from becoming framework requirements.

The controller maps transport DTOs to application models, the service coordinates operations, and repositories access EF Core. This makes responsibilities easy to inspect, but adds mapping and abstraction overhead for a small API. Retain the boundary for the portfolio and explain its cost.

## Repository and unit of work

The wrappers provide a common vocabulary for entity access and saves. EF Core already provides comparable behavior, and returning `IQueryable` exposes persistence details to services. Treat these as explicit design tradeoffs rather than claiming full provider independence.

Proposed improvement: retain the abstraction initially, verify persistence with integration tests, and consider direct DbContext access only if a concrete simplification justifies it.

## Dependency injection conventions

Marker interfaces select transient, scoped, and singleton lifetimes through Autofac assembly scanning. This reduces repetitive registrations, but registration behavior is less explicit and currently duplicated between framework and sample modules.

Proposed improvement: consolidate scanning into one helper, use lifetime-scope registration appropriate to ASP.NET Core, and verify that related operations share a scoped DbContext. Current scoped registrations use `InstancePerRequest`, while the sample DbContext is transient. See [Autofac's ASP.NET Core guidance](https://docs.autofac.org/en/stable/integration/aspnetcore.html) when implementing this change.

## Auditing at persistence boundaries

The context applies creation/modification fields during saves. This centralizes cross-cutting behavior instead of repeating it in every service.

Proposed improvements: test every save overload, use a controlled UTC clock, and derive actor identity from validated claims. Currently `RequestContext.UserId` reads a request header and failed parsing becomes zero. Moving to claims changes behavior for callers using that header; document that compatibility change before implementation. The current timestamps use local time.

## Errors and API contracts

The exception filter maps categories to response statuses and logging severity. A shared error envelope makes client handling consistent, but custom conventions require documentation.

Proposed improvements: preserve intentional business messages, redact unexpected production messages, add a request trace ID, and test the response contract. The current filter suppresses production details but still returns raw exception messages; its trace ID is empty. Adopting Problem Details would be a separate public-contract decision.

## Configuration and startup

Reflection binds configuration into a static options dictionary. It is convenient for one application, but repeated startup and independent test hosts can conflict through global state.

Proposed improvement: prefer injectable validated options. A smaller snapshot-replacement fix can prevent duplicate keys but does not provide host isolation; document that limitation if choosing the smaller change.

Startup currently combines `EnsureCreated` and `Migrate`. Choose one schema strategy and make initialization explicit. A disposable demo and production deployment have different schema-management requirements.

## Storage adapter

MinIO sits behind a storage interface, keeping vendor calls away from consumers. The current download copies into a memory stream without rewinding it, and upload assumes stream length is available.

Proposed improvement: define stream position, ownership, supported stream types, size limits, and cancellation behavior, then verify byte round trips against a disposable MinIO instance. Keep this separate from the initial CRUD demo.

## Refactoring scope and proof

No public rename is needed merely for presentation. If implementation resumes, consolidate duplicated DI code and rename internal `ApplyAbpConcepts...` audit helpers to describe their Davli responsibilities. Preserve public routes and contracts unless a tested change requires otherwise.

Use [the test and CI plan](TESTS_AND_CI.md) to substantiate improvements. Do not claim production readiness, measured performance, completed fixes, or passing tests for this documentation-only state.
