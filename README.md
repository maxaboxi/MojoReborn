# MojoReborn

A modernization of the MojoPortal legacy system, built with .NET 10 (Vertical Slice Architecture) and React 19.

## Project Status: Active Development (Pre-Alpha)

This project is currently under active development and is subject to rapid iteration. While the core architectural foundations are being established, many features remain incomplete, and the codebase may undergo significant breaking changes without prior notice.

**Note:** This software is not yet ready for production use.

## Architecture

### Modular Monolith
The backend is structured as a Modular Monolith.
*   **Host:** `Mojo.Web` (ASP.NET Core Web API)
*   **Modules:** `Mojo.Modules.*` (e.g., Blog, Identity, Forum)
*   **Shared Kernel:** `Mojo.Shared`

### Vertical Slice Architecture (VSA)
Each module organizes code by **Feature** rather than technical layer.
*   Example: `Mojo.Modules.Blog/Features/Posts/CreatePost` contains the Endpoint, Command, Handler, and Validator for that specific feature.

### AI Assisted Development
The frontend is built entirely with AI-generated code. This serves multiple goals: accelerating delivery, running a learning exercise on how far we can push AI-led implementation, showcasing the backend as a headless CMS with the React app as just one possible consumer, and providing a fast feedback loop to validate that the end-to-end experience continues to work.

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
