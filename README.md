# MojoReborn

## Overview
MojoReborn is a modern full-stack web application project designed as a "Greenfield on Brownfield" modernization of the legacy MojoPortal CMS. The primary objective is to re-architect the system using a Modular Monolith approach with Vertical Slice Architecture, leveraging the latest technologies in the .NET and React ecosystems.

**Status: Work In Progress (WIP)**
This project is currently in active development. Features and architecture are subject to change as the modernization effort progresses.

## Architecture
The solution is built on a Headless API architecture, separating the backend logic from the frontend presentation.

*   **Modular Monolith**: The backend is structured as a single deployment unit logically separated into distinct business modules (Core, Blog, Forum).
*   **Vertical Slice Architecture**: Features are self-contained within their respective modules, encapsulating the user interface, business logic, and data access requirements for that specific feature.
*   **CQRS & Messaging**: Wolverine is utilized for command/query handling and internal messaging, promoting a decoupled and testable design.

## Technology Stack

### Backend
*   **Framework**: .NET 10 / ASP.NET Core
*   **Messaging & Mediation**: Wolverine
*   **Data Access**: Entity Framework Core (SQL Server)
*   **Validation**: FluentValidation

### Frontend
*   **Library**: React 19
*   **Build Tool**: Vite
*   **Language**: TypeScript
*   **UI Framework**: Material UI (MUI)
*   **Networking**: Axios

## Project Structure

*   **Mojo.Web**: The host ASP.NET Core application. Responsible for bootstrapping the application, configuring the DI container, and hosting the API endpoints.
*   **Mojo.Modules.Core**: Contains the core domain logic, including system configuration and shared module definitions.
*   **Mojo.Modules.Blog**: Implementation of the Blog feature using vertical slices.
*   **Mojo.Modules.Forum**: Implementation of the Forum feature.
*   **Mojo.Shared**: Shared contracts, DTOs, and utilities utilized across different modules.
*   **Mojo.Frontend**: The React-based Single Page Application (SPA). Frontend is 99% generated with LLMs (Claude Sonnet 4.5 / Gemini 3 Pro / GPT-5.1-Codex) and is meant mostly for testing and as an example how to work with the backend.

## Getting Started

### Prerequisites
*   .NET 10 SDK
*   Node.js (Latest LTS recommended)
*   SQL Server instance

### Backend Setup
1.  Navigate to the `Mojo.Web` directory.
2.  Update `appsettings.json` with your SQL Server connection string.
3.  Run the application:
    ```bash
    dotnet run
    ```

### Frontend Setup
1.  Navigate to the `Mojo.Frontend` directory.
2.  Install dependencies:
    ```bash
    npm install
    ```
3.  Start the development server:
    ```bash
    npm run dev
    ```

## License
This project is intended for educational and demonstration purposes.
