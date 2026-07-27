# SurveyBasket — Agent Context

Learning project following the "Survey Basket" ASP.NET Core Web API course (Mohammed Elhelaly). Built section by section; each course section = one git branch merged into `Main`.

## Structure (SurveyBasket.Api)

- `Program.cs` — thin; everything registered via `DependencyInjection.AddDependencies(configuration)`
- `GlobalUsing.cs` — all global usings live here
- `Entities/` — domain models (`Poll`, `ApplicationUser : IdentityUser`)
- `Contracts/<Feature>/` — request/response **records** + FluentValidation validators (Request/Response naming, never "Dto")
- `Services/<Feature>/` — interface + implementation, registered Scoped
- `Authentication/` — `IJwtProvider`/`JwtProvider` (JWT generation)
- `Persistence/` — `ApplicationDbContext : IdentityDbContext<ApplicationUser>`, `EntitiesConfigurations/` (Fluent API via `ApplyConfigurationsFromAssembly`), `Migrations/`
- `Mapping/MappingConfigurations.cs` — Mapster `IRegister`, scanned at startup

## Conventions

- Async everywhere with `CancellationToken` passed controller → service → EF
- Controllers stay thin: bind → call service → map with `.Adapt<T>()` → return `IActionResult`
- Auth: JWT Bearer is the default scheme; `[Authorize]` on protected controllers; pipeline order `UseAuthentication()` → `UseAuthorization()` → `MapControllers()`
- JWT key/issuer/audience are hardcoded until Section 08 (Options pattern) — do not "fix" prematurely
- Migrations: `dotnet ef migrations add <Name> -o Persistence/Migrations` from `SurveyBasket.Api/`

## Git

- Default branch: `Main` (capital M). Section checkpoints: `section/NN-topic` — never delete them.
- Build check: `dotnet build` from `SurveyBasket.Api/`; if the EXE is locked, stop the running `SurveyBasket.Api` process first.

## Related

- Obsidian notes vault: `D:\M.Abdeltawab\Survey Basket Course\`
- Course materials: `C:\Users\osman\Desktop\Eng Mohammed Elhelaly\`
