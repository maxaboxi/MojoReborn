---
title: Wide Events / Canonical Log Lines
impact: CRITICAL
tags: logging, wide-events, canonical-log-lines, dotnet, wolverine
---

## Wide Events / Canonical Log Lines

**Impact: CRITICAL**

Wide events (also called canonical log lines) are the foundation of effective logging. For each handler execution, emit **a single context-rich event**. Instead of scattering 2-4 `LogInformation` calls throughout your handler, consolidate everything into one comprehensive event emitted at the end in a `finally` block.

### The Pattern

Build context throughout the handler lifecycle, then emit once at completion. Use `ILogger.BeginScope()` for correlation fields, and a single `LogInformation` in the `finally` block for the wide event.

**Incorrect:**

```csharp
public static class CreatePostHandler
{
    public static async Task<PostResponse> Handle(
        CreatePostCommand command,
        BlogDbContext db,
        ILogger logger)
    {
        logger.LogInformation("Creating post: {Title}", command.Title);
        // ... 20 lines of work ...
        logger.LogInformation("Post created: {PostId}", post.Id);
    }
}
// 2+ disconnected log lines with scattered context
// Cannot query: "show me all post creations for UserX that took > 2s"
```

**Correct:**

```csharp
public static class CreatePostHandler
{
    public static async Task<PostResponse> Handle(
        CreatePostCommand command,
        BlogDbContext db,
        ILogger logger)
    {
        // Correlation context attached to ALL log entries in this scope
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

            // === SINGLE wide event — Error level with exception on failure, Info on success ===
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

### Logging Boundaries in This Repo

| Boundary | Mechanism | Who Owns It |
|----------|-----------|-------------|
| **Wolverine message handlers** | `AuditLoggingBehavior` provides basic timing; handlers should add business context via `BeginScope` | Wolverine middleware + handler code |

The primary improvement opportunity is **command/event handlers** — consolidate logging into a single wide event per handler execution with rich business context.

### Use BeginScope for Correlation Context

`ILogger.BeginScope()` is the .NET mechanism for attaching structured properties to all log entries within a block. Use it for fields that should appear on every log entry in the handler:

```csharp
using var scope = logger.BeginScope(new Dictionary<string, object>
{
    ["UserId"] = command.UserId,
    ["PageId"] = command.PageId,
});
// Every LogInformation/LogWarning/LogError in this block automatically
// includes UserId and PageId in structured output
```

### Emit in Finally Block

Always emit wide events in a `finally` block. This ensures the event is emitted with complete context regardless of success or failure.

### Skipped Records — Aggregate, Don't Scatter

Do NOT emit per-item `LogWarning` calls for skipped records — that defeats the single-event principle. Instead, count skips and include the aggregate in the wide event:

```csharp
int skippedCount = 0;
var skippedIds = new List<Guid>();

foreach (var item in items)
{
    if (!item.IsValid)
    {
        skippedIds.Add(item.Id);
        skippedCount++;
        continue;
    }
    // ... process ...
}

// SINGLE wide event includes aggregate AND detail
logger.LogInformation(
    "Handler: {Handler} | Items: {ItemCount} | Skipped: {SkippedCount} | "
    + "SkippedIds: {SkippedIds} | Outcome: {Outcome} | Duration: {Duration:F2}ms",
    nameof(Handler), itemCount, skippedCount,
    skippedIds.Count > 0 ? string.Join(",", skippedIds) : "none",
    outcome, elapsed);
// One event. Queryable: WHERE SkippedCount > 0. Debuggable: SkippedIds has the IDs.
```