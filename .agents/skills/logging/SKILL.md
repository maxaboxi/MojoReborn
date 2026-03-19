---
name: logging
description: Logging best practices focused on wide events (canonical log lines) for powerful debugging and analytics in .NET/Wolverine modular monolith
---

# Logging Skill

## Purpose

This skill provides guidelines for implementing effective logging in MojoReborn. It focuses on **wide events** (also called canonical log lines) — a pattern where you emit a single, context-rich event per request per handler, enabling powerful debugging and analytics.

## When to Apply

Apply these guidelines when:
- Writing or reviewing logging code in handlers, middleware, or services
- Adding `ILogger` calls to new or existing code
- Designing logging strategy for new modules or features
- Adding Wolverine message handlers or HTTP endpoints

## Architecture Context

This repo is a **.NET 10 modular monolith** with **Vertical Slice Architecture** and **Wolverine CQRS**.

| Component | Role |
|-----------|------|
| **Serilog** | Structured logging to Console; configured in `Mojo.Web/Program.cs` with enrichment from `LogContext` |
| **Microsoft.Extensions.Logging** | Logging abstraction — `ILogger<T>` via DI, structured message templates |
| **AuditLoggingBehavior** | Wolverine middleware that logs all handler executions with timing (start, finish, duration) |
| **FeatureSecurityMiddleware** | Wolverine middleware intercepting `IFeatureRequest` messages for permission checks |
| **ProblemDetails** | ASP.NET Core middleware mapping exceptions to HTTP status codes (403, 404, 400) |
| **Wolverine Handlers** | Static classes with `ILogger` method injection |

### Current Logging Patterns

- **Query handlers** (~20): Zero direct logging — rely on `AuditLoggingBehavior` middleware
- **Command/event handlers** (~30): Inconsistent logging — some handlers log errors (e.g., `LoginUserHandler` with 6 `LogError` calls), most log nothing
- **Services**: `ILogger<T>` for error-level logging (parse failures, missing data)
- **Log levels used**: Information, Warning, Error (no Debug/Trace/Critical)
- **All calls use structured message templates** — no string interpolation
- **Background services**: `DeleteNotificationsScheduler` logs scheduled task start/error

### Key Gaps

1. No `ILogger.BeginScope()` usage for contextual correlation
2. No wide event pattern — handlers either log nothing or scatter multiple log lines
3. `AuditLoggingBehavior` provides basic timing but no business context
4. No correlation IDs on events or in log scopes
5. No observability tooling beyond Serilog console output (no OpenTelemetry, no Application Insights)

## Core Principles

### 1. Wide Events (CRITICAL)

Emit **one context-rich log event per handler execution**. Instead of scattering `LogInformation` calls throughout your handler, build context and emit once at completion.

```csharp
public static class CreatePostHandler
{
    public static async Task<PostResponse> Handle(
        CreatePostCommand command,
        BlogDbContext db,
        IUserService userService,
        ILogger logger)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["UserId"] = command.UserId,
            ["PageId"] = command.PageId,
        });

        var stopwatch = Stopwatch.StartNew();
        Guid? postId = null;
        string outcome = "success";

        Exception? caught = null;

        try
        {
            var post = new BlogPost
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Body = command.Body,
                PageId = command.PageId,
                CreatedByUserId = command.UserId,
            };

            db.BlogPosts.Add(post);
            await db.SaveChangesAsync();
            postId = post.Id;

            return new PostResponse(post.Id, post.Title);
        }
        catch (Exception ex)
        {
            outcome = "error";
            caught = ex;
            throw;
        }
        finally
        {
            stopwatch.Stop();

            // === SINGLE LOG EVENT per handler execution ===
            if (caught is not null)
                logger.LogError(caught,
                    "Handler: {Handler} | PostId: {PostId} | UserId: {UserId} | "
                    + "Outcome: {Outcome} | Duration: {Duration:F2}ms",
                    nameof(CreatePostHandler), postId, command.UserId,
                    outcome, stopwatch.Elapsed.TotalMilliseconds);
            else
                logger.LogInformation(
                    "Handler: {Handler} | PostId: {PostId} | UserId: {UserId} | "
                    + "Outcome: {Outcome} | Duration: {Duration:F2}ms",
                    nameof(CreatePostHandler), postId, command.UserId,
                    outcome, stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}
```

### 2. High Cardinality & Dimensionality (CRITICAL)

Include fields with high cardinality (userId, pageId, postId — many unique values) and high dimensionality (many properties per event). This enables querying by specific users or pages and answering questions you haven't anticipated yet.

### 3. Business Context (CRITICAL)

Always include business context: user ID, page ID, item counts, feature name. The goal is to know "CreatePost for User ABC on Page XYZ failed" not just "handler failed."

### 4. Environment Characteristics (CRITICAL)

When observability tooling is added (OpenTelemetry, Application Insights), environment context should be handled at the resource level — serviceName, serviceVersion, deployment.environment attached automatically to all telemetry. Do not duplicate these in individual log calls.

### 5. Scoped Context via BeginScope (HIGH)

Use `ILogger.BeginScope()` to attach correlation context that applies to all log entries within a handler execution. This is the .NET way to enrich a wide event with cross-cutting fields.

```csharp
using var scope = logger.BeginScope(new Dictionary<string, object>
{
    ["UserId"] = command.UserId,
    ["PageId"] = command.PageId,
});
// All LogInformation/LogError calls within this scope
// automatically include these fields in structured output
```

### 6. Structured Message Templates (HIGH)

Always use structured message templates with named placeholders. Never use string interpolation.

```csharp
// CORRECT — structured, queryable
logger.LogInformation(
    "Handler complete: {Handler} | Items: {ItemCount} | Duration: {Duration:F2}ms",
    nameof(CreatePostHandler), itemCount, elapsed);

// INCORRECT — string interpolation, not queryable
logger.LogInformation(
    $"Handler complete: {nameof(CreatePostHandler)} | Items: {itemCount}");
```

### 7. Wolverine Middleware for Handler Logging (HIGH)

The existing `AuditLoggingBehavior` provides basic handler timing. Consider extending it (or creating a `WideEventMiddleware`) to handle wide event infrastructure (timing, outcome, correlation) so handlers only add business context.

```csharp
// Wolverine middleware applied to message handlers
public class WideEventMiddleware
{
    public async Task InvokeAsync(
        MessageContext context,
        ILogger<WideEventMiddleware> logger,
        Func<Task> next)
    {
        var stopwatch = Stopwatch.StartNew();
        var messageType = context.Envelope?.Message?.GetType().Name ?? "Unknown";

        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["MessageType"] = messageType,
        });

        string outcome = "success";
        Exception? caught = null;

        try
        {
            await next();
        }
        catch (Exception ex)
        {
            outcome = "error";
            caught = ex;
            throw;
        }
        finally
        {
            stopwatch.Stop();

            // Single log event — Error level with exception on failure, Info on success
            if (caught is not null)
                logger.LogError(caught,
                    "Message handled: {MessageType} | Outcome: {Outcome} | Duration: {Duration:F2}ms",
                    messageType, outcome, stopwatch.Elapsed.TotalMilliseconds);
            else
                logger.LogInformation(
                    "Message handled: {MessageType} | Outcome: {Outcome} | Duration: {Duration:F2}ms",
                    messageType, outcome, stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}
```

## Anti-Patterns to Avoid

1. **Scattered log lines**: Multiple `LogInformation()` calls per handler (start, middle, end)
2. **String interpolation**: `LogInformation($"Failed: {error}")` instead of message templates
3. **Silent handlers**: Command/event handlers with zero logging beyond `AuditLoggingBehavior`
4. **Unscoped context**: Important IDs (userId, pageId) not in `BeginScope`
5. **Duplicate environment context**: Adding serviceName/version per log call (handle at infrastructure level)

## Guidelines

### Wide Events (`rules/wide-events.md`)
- Emit one wide event per handler execution
- Build context throughout the handler, emit in finally block
- Use `BeginScope()` for correlation context
- Emit at handler completion, not scattered throughout

### Context (`rules/context.md`)
- Support high cardinality fields (userId, pageId, postId)
- Include high dimensionality (many properties)
- Always include business context (item counts, feature name, user actions)
- Environment context via infrastructure level (don't duplicate per-call)

### Structure (`rules/structure.md`)
- Use DI-based `ILogger<T>` for services, `ILogger` for static handlers
- Use Wolverine middleware for unified message handler logging (extend `AuditLoggingBehavior` or add `WideEventMiddleware`)
- Use structured message templates, never string interpolation
- Maintain consistent property names across modules
- Three levels: Information, Warning, Error

### Common Pitfalls (`rules/pitfalls.md`)
- Avoid multiple log lines per handler
- Design for unknown unknowns with rich context
- Don't duplicate infrastructure-level context in application logs