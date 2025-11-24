# MojoPortal Reborn (.NET 10)
**Architecture Reference & Implementation Plan**

---

## 1. Executive Summary

This project is a "Greenfield on Brownfield" modernization of the legacy MojoPortal CMS. The goal is to rewrite the system using **.NET 10**, **Blazor Web App (Interactive Auto)**, and **Vertical Slice Architecture**, while migrating data from the legacy MSSQL database.

The system moves away from the traditional N-Tier layered architecture (UI → Business → Data) to a **Modular Monolith** where features are self-contained and the UI is decoupled from the logic via a **"Split Module"** strategy.

---

## 2. High-Level Architecture

The solution relies on the **Blazor Split-Module Pattern**. This allows the application to run seamlessly on both the Server (SSR) and the Browser (WebAssembly) without duplicating business logic.

### The Logical Flow

- **The Host (Mojo.Host)**: The entry point. It runs on the server, holds the Database connections, and executes business logic via Wolverine.
- **The Client (Mojo.Client)**: The WASM payload. It runs in the browser and talks to the Host via HTTP APIs.
- **The Modules**: Distinct business domains (Blog, Forum, Core) that contain their own UI and Logic.

### Architecture Diagram (Mermaid)

```mermaid
graph TD
    subgraph "User's Browser (Client Side)"
        WASM 
        BlogUI_C[Blog UI Component]
        WASM --> BlogUI_C
    end

    subgraph "Web Server (Server Side)"
        Host[Mojo.Host]
        BlogUI_S[Blog UI Component]
        BlogServer[Blog Server Logic]

        Host --> BlogUI_S
        Host --> BlogServer

        %% The Logic Connection
        BlogServer -- "Wolverine (In-Memory)" --> DB[(SQL Database)]
    end

    subgraph "Shared Contracts"
        Shared[Mojo.Shared]
    end

    %% Relationships
    BlogUI_C --> Shared
    BlogUI_S --> Shared
    BlogServer --> Shared

    %% The Magic Switch
    BlogUI_C -. "HTTP / Refit" .-> Host
    BlogUI_S -- "Direct Injection" --> BlogServer
```

---

## 3. Technical Stack & Key Decisions

| Component   | Technology               | Justification |
|-------------|--------------------------|---------------|
| Framework   | .NET 10 (Preview)        | Learning the bleeding edge. |
| UI Mode     | Blazor Interactive Auto  | Best of both worlds: Instant load (SSR) + Interactivity (WASM). |
| Messaging   | Wolverine                | Handles In-Process routing, API generation, and Transactional Outbox patterns. |
| Data Access | EF Core                  | Modern ORM. Replaces Stored Procedures with LINQ. |
| Database    | SQL Server               | Legacy compatibility. Single DB, but separated by Schemas (e.g., blog.Posts). |
| Validation  | FluentValidation         | Separates validation rules from UI logic. |
| HTTP Client | Refit                    | Auto-generates client-side HTTP code to reduce boilerplate. |

---

## 4. Solution Structure

```
/MojoReborn.sln
│
├── /src
│   ├── /Mojo.Host                  # ASP.NET Core Server Project
│   │   ├── Program.cs              # Registers Wolverine, Modules, DB
│   │   ├── App.razor               # Main Layout / Root
│   │   └── /Services               # Server-side implementations (Direct calls)
│   │
│   ├── /Mojo.Client                # Blazor WebAssembly Project
│   │   ├── Program.cs              # Registers HTTP Clients
│   │   └── /Services               # Client-side implementations (HTTP calls)
│   │
│   ├── /Mojo.Shared                # The "Glue" (Referenced by everyone)
│   │   ├── /Contracts              # Interfaces (e.g., IBlogService)
│   │   ├── /DTOs                   # Data Transfer Objects (Commands/Queries)
│   │   └── /Validation             # Shared Validation Rules
│   │
│   └── /Modules                    # The Business Domains
│       │
│       ├── /Mojo.Modules.Core      # Identity, Users, Site Settings
│       │
│       └── /Mojo.Modules.Blog      # Example Feature Module
│           │
│           ├── /Mojo.Modules.Blog.Client  # Razor Class Library (UI Only)
│           │   └── /Features
│           │       └── /CreatePost        # UI for creating a post
│           │           ├── CreatePostForm.razor
│           │           └── CreatePostForm.razor.css
│           │
│           └── /Mojo.Modules.Blog.Server  # Class Library (Logic Only)
│               ├── BlogDbContext.cs       # Module-specific DB Context
│               ├── BlogModuleExtensions.cs# DI Registration helper
│               └── /Features
│                   └── /CreatePost        # The Vertical Slice
│                       ├── CreatePostCommand.cs  # The Input
│                       ├── CreatePostHandler.cs  # The Logic (Wolverine)
│                       └── CreatePostEndpoint.cs # The API Route
```

---

## 5. Deep Dive: The Vertical Slice

In this architecture, a "Feature" is a self-contained folder. You do not jump between a "Controllers" folder and a "Services" folder.

### Example Slice: CreatePost

#### A. The Contract (Located in Mojo.Shared)

**File: CreatePostCommand.cs**  
**Purpose:** A simple class (Record) holding the data needed to create a post (Title, Body, AuthorId).

**File: IBlogService.cs**  
**Purpose:** Defines the method signature: `Task<int> CreatePostAsync(CreatePostCommand cmd);`

#### B. The UI (Located in Mojo.Modules.Blog.Client)

**File: CreatePostForm.razor**  
**Purpose:** The visual form.  
**Dependencies:** Injects `IBlogService`.  
**Behavior:** It doesn't know if it's on the server or client. It just calls `.CreatePostAsync()`.

#### C. The API Definition (Located in Mojo.Modules.Blog.Server)

**File: CreatePostEndpoint.cs**  
**Purpose:** Uses Wolverine attributes to expose an HTTP route (e.g., `POST /api/blog/posts`).  
**Behavior:** Receives the HTTP request and immediately passes the command to the Bus.

#### D. The Logic (Located in Mojo.Modules.Blog.Server)

**File: CreatePostHandler.cs**  
**Purpose:** The "Brain".  
**Dependencies:** Injects `BlogDbContext`.  
**Behavior:**
1. Receives `CreatePostCommand`.
2. Validates logic.
3. Adds Entity to `BlogDbContext`.
4. Saves changes.
5. (Optional) Returns a `PostCreated` event for the Outbox.

---

## 6. Implementation Strategy (Step-by-Step)

### Phase 1: The Foundation (Weekend 1)

- **Data Setup:** Run legacy MojoPortal locally to generate a populated MSSQL database.
- **Scaffold:** Create the Solution structure (Host, Client, Shared).
- **Reverse Engineer:** Create a temporary project to run `dotnet ef dbcontext scaffold` against the legacy DB.
- **Clean Up:** Move the generated Entities into the Mojo.Modules.*.Server projects (e.g., `mp_Blogs` → `Mojo.Modules.Blog.Server/Domain/BlogPost.cs`).

### Phase 2: The "Walking Skeleton" (Weekend 2)

- **Wiring:** Configure Wolverine and EF Core in `Mojo.Host/Program.cs`.
- **First Slice:** Implement "Get Blog Posts" (Read-Only).
  - Create the Query DTO.
  - Create the Handler (Server).
  - Create the Endpoint (Server).
  - Create the UI List (Client).
- **Verification:** Ensure the list loads instantly on refresh (SSR) and works via navigation (WASM).

### Phase 3: The Migration (Deep Work)

- **Identity:** Set up ASP.NET Core Identity tables. Write a SQL script to migrate users from `mp_Users` to `AspNetUsers`.
- **Password Compat:** Implement a custom `IPasswordHasher` that detects legacy MojoPortal hashes and upgrades them on successful login.

### Phase 4: Features

- Pick features one by one (Blog, Forums, HTML Content).
- Rewrite them using the Vertical Slice pattern.
- Do not port code directly. Read the old code to understand what it did, then write new code to do it the Wolverine way.

---

## 7. Reference: The "Magic Switch" Configuration

How the app decides between Server and Client execution:

### In Mojo.Host (Server Side)

```csharp
services.AddScoped<IBlogService, ServerBlogService>(); // ServerBlogService talks directly to Wolverine/Database
```

### In Mojo.Client (WASM Side)

```csharp
services.AddScoped<IBlogService, ClientBlogService>(); // ClientBlogService talks via HTTP/Refit to the API
```

- **ServerBlogService** → Direct Injection to server-side handlers and DB
- **ClientBlogService** → HTTP via Refit to the Host; same contracts via `Mojo.Shared`

---

## 8. Appendix & Notes

- **Schema Strategy:** Keep a single SQL Server instance but use schemas to separate module data (e.g., `blog.Posts`, `forum.Threads`).
- **Outbox Events:** Use Wolverine's transactional outbox pattern to reliably publish domain events after DB commits.
- **Testing:** Create integration tests that run against an in-memory or ephemeral SQL instance for each module.
- **Observability:** Add structured logging and distributed tracing early (Wolverine supports integrations).
- **Backward Compatibility:** Implement compatibility layers for legacy stored procedures and hashing where migrating behavior is non-trivial.

---

## End of Document
