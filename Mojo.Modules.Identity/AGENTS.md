# Mojo.Modules.Identity — Agent Guide

## Purpose

Authentication, user management, roles, permissions, and legacy MojoPortal user migration. Implements `IUserService` and `IPermissionService` from Mojo.Shared.

## Entities → Tables

| Entity | Table | Notes |
|---|---|---|
| `ApplicationUser` | ASP.NET Identity tables | Extends `IdentityUser<Guid>`; adds DisplayName, AvatarUrl, Bio, etc. |
| `ApplicationRole` | ASP.NET Identity tables | Extends `IdentityRole<Guid>` |
| `UserSiteProfile` | `UserSiteProfiles` | Per-site user profile |
| `UserSiteRoleAssignment` | `UserSiteRoleAssignments` | User ↔ SiteRole join |
| `SiteRole` | `SiteRoles` | Site-scoped roles |
| `LegacyUser` | `mp_Users` | Legacy; includes encrypted password fields; ExcludedFromMigrations |
| `LegacyRole` | `mp_Roles` | Legacy; ExcludedFromMigrations |
| `LegacyUserRole` | `mp_UserRoles` | Legacy join; ExcludedFromMigrations |

## DbContext

`IdentityDbContext` (extends `IdentityDbContext<ApplicationUser, ApplicationRole, Guid>`) — sets: `LegacyUsers`, `LegacyRoles`, `UserSiteProfiles`, `SiteRoles`, `UserSiteRoleAssignments`

## Features

| Feature | Type | Route | Notes |
|---|---|---|---|
| GetCurrentUser | Query | `GET /auth/current-user` | Returns null on 401 (no throw) |
| LoginUser | Command | `POST /auth/login` | Cookie-based sign-in |
| LogOutUser | Command | `POST /auth/logout` | Clears auth cookie |
| StartExternalLogin | Endpoint | `GET /auth/external-login` | OAuth flow (Google, Microsoft, Facebook) |
| GetLegacyUser | Query | — | Finds legacy user for migration |
| MigrateLegacyUser | Command | — | Migrates legacy account to Identity |
| DevLogin | Command | `POST /auth/dev-login` | Dev-only endpoint |

## Services

- **UserService** (`IUserService`) — user lookup, profile queries
- **PermissionService** (`IPermissionService`) — checks edit/view permissions for security context resolution
