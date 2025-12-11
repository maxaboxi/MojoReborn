# MojoReborn

A modernization of the MojoPortal legacy system, built with .NET 10 (Vertical Slice Architecture) and React 19.

## Project Status: Active Development (Pre-Alpha)

This project is currently under active development and is subject to rapid iteration. While the core architectural foundations are being established, many features remain incomplete, and the codebase may undergo significant breaking changes without prior notice.

**Note:** This software is not yet ready for production use. Contributions and feedback are welcome as we work towards a stable release.

## Architecture

### Modular Monolith
The backend is structured as a Modular Monolith.
*   **Host:** `Mojo.Web` (ASP.NET Core Web API)
*   **Modules:** `Mojo.Modules.*` (e.g., Blog, Core, Forum)
*   **Shared Kernel:** `Mojo.Shared`

### Vertical Slice Architecture (VSA)
Each module organizes code by **Feature** rather than technical layer.
*   Example: `Features/Posts/CreatePost` contains the Endpoint, Command, Handler, and Validator for that specific feature.

### Legacy Compatibility
*   **Database:** The system runs on existing MojoPortal SQL Server databases.
*   **Mapping:** EF Core entities are mapped to legacy table names (e.g., `mp_Blog` -> `BlogPost`).
*   **Shared Data:** Some tables (like `mp_Comments`) are shared across modules.

## Getting Started

### Backend
1.  **Prerequisites:** .NET 10 SDK.
2.  **Database:** Update `appsettings.json` in `Mojo.Web` with your MojoPortal database connection string.
3.  **Run:** `dotnet run --project Mojo.Web`

### Frontend
1.  **Prerequisites:** Node.js 20+.
2.  **Install:** `cd Mojo.Frontend && npm install`
3.  **Run:** `npm run dev`

## Development Guidelines

*   **Async/Await:** Avoid `.Result` or `.Wait()`. Always use `await`.
*   **Pagination:** All list endpoints MUST implement pagination.
*   **Projections:** Use `Select` to project to DTOs. Avoid returning Entities directly.
