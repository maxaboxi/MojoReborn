---
title: Structure and Format
impact: HIGH
tags: logging, structured-logging, message-templates, middleware, dotnet, wolverine
---

## Structure and Format

**Impact: HIGH**

Structured logging with consistent formats enables efficient querying and analysis. The right structure transforms logs from text output into queryable data.

### Use DI-Based ILogger (Idiomatic .NET)

.NET uses dependency-injected `ILogger<T>` for services and `ILogger` for Wolverine static handlers. This is the correct pattern — do not fight it.

```csharp
// Services: generic ILogger<T> via constructor injection
public class BlogService(
    BlogDbContext db,
    ILogger<BlogService> logger)
{
    public async Task CreatePost(BlogPost post)
    {
        // logger is scoped to this class for filtering
        logger.LogError("UserId is empty for post {PostId}",
            post.Id);
    }
}

// Wolverine handlers: non-generic ILogger via method injection
public static class CreatePostHandler
{
    public static async Task<PostResponse> Handle(
        CreatePostCommand command,
        BlogDbContext db,
        ILogger logger)  // Non-generic — Wolverine resolves from DI
    {
        logger.LogInformation("Handler complete: {Handler} | Items: {ItemCount}",
            nameof(CreatePostHandler), itemCount);
    }
}
```

**Do NOT:**
```csharp
// DON'T create loggers manually
var logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger("MyHandler");

// DON'T bypass the logging abstraction
Console.WriteLine("something happened");
System.Diagnostics.Debug.WriteLine("debug info");
```

### Use Wolverine Middleware for Unified Message Handler Logging

The existing `AuditLoggingBehavior` provides basic handler timing. Consider extending it or adding a `WideEventMiddleware` to handle wide event infrastructure (timing, outcome, correlation) so handlers only add business context.

```csharp
// Register in Wolverine configuration
opts.Policies.AddMiddleware<WideEventMiddleware>();

// Middleware implementation
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

            // Single event — Error with exception on failure, Info on success
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

**Handlers then only add scoped context (zero log calls — middleware emits the event):**

```csharp
public static class CreatePostHandler
{
    public static async Task<PostResponse> Handle(
        CreatePostCommand command,
        BlogDbContext db,
        ILogger logger)
    {
        // Add business context to scope — middleware's single log event
        // will automatically include these fields
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["UserId"] = command.UserId,
            ["PageId"] = command.PageId,
        });

        // ... processing logic — NO log calls here ...
        // Middleware emits the single wide event with all scoped context
    }
}
```

### Use Structured Message Templates

Always use structured message templates with named placeholders. This is critical for queryability.

```csharp
// CORRECT — each placeholder becomes a queryable property
logger.LogInformation(
    "Handler complete: {Handler} | Items: {ItemCount} | Duration: {Duration:F2}ms",
    nameof(CreatePostHandler), itemCount, elapsed);

// INCORRECT — string interpolation produces a flat string, not queryable properties
logger.LogInformation(
    $"Handler complete: {nameof(CreatePostHandler)} | Items: {itemCount}");
```

### Maintain Consistent Property Names

Use consistent property names across all modules. If one handler uses `UserId` and another uses `userId` or `user_id`, querying becomes painful.

**Standard property names for this repo:**

```csharp
// Handler identification
{Handler}           // nameof(HandlerClass)
{Outcome}           // "success", "error", "empty"
{Duration}          // Stopwatch elapsed in ms

// Business context
{UserId}            // Guid — authenticated user
{PageId}            // int — CMS page ID
{PostId}            // Guid — blog/forum post ID
{ThreadId}          // Guid — forum thread ID
{ItemCount}         // int — items processed
{FeatureName}       // string — feature identifier (e.g., "Blog", "Forum")
```

### Three Log Levels

This repo uses three levels meaningfully:

- **Information**: Handler completion wide events, successful operations
- **Warning**: Edge cases, data quality signals that aren't errors but need visibility
- **Error**: Unhandled exceptions, parse failures, missing required data, failed transactions

Do not use Debug, Trace, or Critical.

### Never Log Unstructured Strings

Every log must use structured message templates. Plain strings are useless for querying.

```csharp
// INCORRECT
logger.LogInformation("Processing complete");
logger.LogError("Something went wrong");

// CORRECT — add queryable properties
logger.LogInformation(
    "Handler complete: {Handler} | Items: {ItemCount}",
    nameof(Handler), itemCount);
```

If you're tempted to write `logger.LogInformation("something happened")`, ask: "What properties would make this queryable?" Then add those as message template parameters.