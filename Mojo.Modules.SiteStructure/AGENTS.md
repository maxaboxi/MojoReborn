# Mojo.Modules.SiteStructure — Agent Guide

## Purpose

CMS site, page, and module structure. Provides menu data, feature context resolution, and module/site lookups used by all other modules.

## Entities → Tables

| Entity | Table | Notes |
|---|---|---|
| `Site` | `mp_Sites` | Legacy; ExcludedFromMigrations |
| `Page` | `mp_Pages` | Legacy; ExcludedFromMigrations |
| `Module` | `mp_Modules` | Legacy; ExcludedFromMigrations |
| `PageModule` | `mp_PageModules` | Legacy join; ExcludedFromMigrations |
| `ModuleDefinition` | `mp_ModuleDefinitions` | Legacy; ExcludedFromMigrations |
| `SiteHost` | `mp_SiteHosts` | Legacy; ExcludedFromMigrations |

## DbContext

`SiteStructureDbContext` — sets: `Pages`, `Modules`, `PageModules`, `ModuleDefinitions`, `Sites`, `SiteHosts`

## Features

| Feature | Type | Route | Notes |
|---|---|---|---|
| GetMenu | Query | `GET /menu` | Returns `PageMenuItem[]` tree |
| GetFeatureContext | Service | — | `IFeatureContextResolver` — resolves page, module, permissions for a given pageId/featureName |
| GetModules | Service | — | `IModuleResolver` — looks up module definitions |
| GetSite | Service | — | `ISiteResolver` — resolves current site from host |

## Cross-Module Role

This module is foundational — `FeatureSecurityMiddleware` in Mojo.Web calls `IFeatureContextResolver`, `ISiteResolver`, and `IModuleResolver` to build `SecurityContext` before any `IFeatureRequest` handler executes. All modules that use `IFeatureRequest` depend on SiteStructure indirectly.
