# MojoReborn — Agent Guide

## Stack

- **.NET 10** modular monolith, Vertical Slice Architecture (VSA)
- **Wolverine** for CQRS, HTTP endpoints, and message durability (SQL Server transport)
- **Entity Framework Core** with SQL Server — legacy MojoPortal `mp_*` table mappings
- **ASP.NET Core Identity** with external OAuth (Google, Microsoft, Facebook)
- **Serilog** structured logging (console sink)
- **FluentValidation** for request validation
- **React 19 + TypeScript 5.9** frontend via Vite, MUI, React Query, SignalR

## Solution Layout

| Project | Role |
|---|---|
| `Mojo.Web` | ASP.NET Core host — DI, middleware, Wolverine config, DB migrations |
| `Mojo.Shared` | Shared kernel — contracts, DTOs, interfaces, events, domain types |
| `Mojo.Modules.Blog` | Blog posts, categories, comments, subscriptions |
| `Mojo.Modules.Forum` | Forums, threads, posts, subscriptions |
| `Mojo.Modules.Identity` | Auth, users, roles, legacy migration, permissions |
| `Mojo.Modules.SiteStructure` | Sites, pages, modules, menus, feature context resolution |
| `Mojo.Modules.Notifications` | User notifications, SignalR hub, scheduled cleanup |
| `Mojo.Frontend` | React SPA — blog, forum, auth, notifications UI |
| `MojoPortal.Legacy` | EF Core reference for legacy database access |

## Architecture Patterns

### Backend

- **Feature folders** per module: each feature has its own folder with endpoint, handler, request/response types
- **Wolverine endpoints** define HTTP routes; handlers are plain classes resolved by Wolverine
- **IFeatureRequest** marker interface on commands triggers `FeatureSecurityMiddleware` — resolves `SecurityContext` from claims + page/module before handler runs
- **AuditLoggingBehavior** wraps all handlers with timing and structured audit logs
- **Cross-module messaging** via Wolverine: modules publish commands/events (e.g., `SaveNotificationCommand`, `SubscriberDeletedEvent`) consumed by other modules
- **Legacy tables** mapped with `ExcludedFromMigrations` and explicit column names to coexist with MojoPortal DB

### Frontend

- **Feature folders** under `src/features/` with colocated api, hooks, components, pages, types, routes
- **Path aliases**: `@app`, `@shared`, `@features`, `@components`
- **Provider stack**: ThemeProvider → QueryClientProvider → AuthProvider → NotificationsProvider → AppRouter
- **Menu-driven routing**: `DynamicFeaturePage` resolves CMS page by URL path, renders matching feature component
- **Cookie auth**: axios client sends `withCredentials: true` to .NET host on `localhost:7001`

## Shared Kernel (Mojo.Shared)

- **Contracts**: `SaveNotificationCommand` — cross-module Wolverine command
- **Events**: `SubscriberDeletedEvent` — published when a subscriber is removed
- **Interfaces**: `IUserService`, `IPermissionService`, `IModuleResolver`, `ISiteResolver`, `IFeatureContextResolver`, `IFeatureRequest`
- **DTOs**: `ApplicationUserDto`, `UserSiteProfileDto`, `FeatureContextDto`, `PageDto`, `ModuleDto`, `SiteDto`, `SubscriptionDto`
- **Domain**: `SecurityContext` record, `FeatureNames` constants

## Host (Mojo.Web)

- `Program.cs` registers all 5 DbContexts, Identity, Wolverine (discovers all module assemblies), CORS, SignalR
- Middleware order: `FeatureSecurityMiddleware` → `AuditLoggingBehavior` (applied via Wolverine policies)
- `DatabaseExtensions.ApplyDatabaseMigrations()` runs EF migrations at startup in fixed order
- SignalR hub mapped at `/hubs/notifications`

## Commands

```bash
# Frontend
cd Mojo.Frontend && npm run dev      # Vite dev server (:5173)
cd Mojo.Frontend && npm run build    # tsc + vite build
cd Mojo.Frontend && npm run lint     # ESLint

# Backend
dotnet run --project Mojo.Web        # ASP.NET host (:7001)
dotnet build                         # Build solution
```
