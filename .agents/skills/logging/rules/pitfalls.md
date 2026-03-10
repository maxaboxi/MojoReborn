---
title: Common Pitfalls
impact: MEDIUM
tags: logging, anti-patterns, pitfalls, dotnet, wolverine
---

## Common Pitfalls

**Impact: MEDIUM**

Avoid these anti-patterns that undermine your logging effectiveness.

### Pitfall 1: Scattered Log Lines Per Handler

Emitting multiple `LogInformation` calls per handler creates noise without value. These scattered logs cannot be efficiently queried or correlated.

**Incorrect:**

```csharp
public static async Task Handle(
    CreatePostCommand command,
    BlogDbContext db,
    ILogger logger)
{
    logger.LogInformation("Creating blog post: {Title}", command.Title);  // Line 1

    var post = new BlogPost { Title = command.Title, Body = command.Body };
    db.BlogPosts.Add(post);
    await db.SaveChangesAsync();

    logger.LogInformation("Blog post created: {PostId}", post.Id);         // Line 2
}
// 2 log lines per handler = scattered context, no timing, no correlation
```

**Correct:**

```csharp
public static async Task Handle(
    CreatePostCommand command,
    BlogDbContext db,
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
        var post = new BlogPost { Title = command.Title, Body = command.Body };
        db.BlogPosts.Add(post);
        await db.SaveChangesAsync();
        postId = post.Id;
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

        // SINGLE event — LogError with exception on failure, LogInformation on success
        if (caught is not null)
            logger.LogError(caught,
                "Handler: {Handler} | PostId: {PostId} | "
                + "Outcome: {Outcome} | Duration: {Duration:F2}ms",
                nameof(CreatePostHandler), postId,
                outcome, stopwatch.Elapsed.TotalMilliseconds);
        else
            logger.LogInformation(
                "Handler: {Handler} | PostId: {PostId} | "
                + "Outcome: {Outcome} | Duration: {Duration:F2}ms",
                nameof(CreatePostHandler), postId,
                outcome, stopwatch.Elapsed.TotalMilliseconds);
    }
}
```

### Pitfall 2: String Interpolation in Log Calls

String interpolation (`$"..."`) in log calls defeats structured logging. The interpolated string becomes a flat message — properties are not extracted for querying.

**Incorrect:**

```csharp
// CA2254 warning — and properties lost for querying
logger.LogInformation($"Processing {itemCount} items for user {userId}");
logger.LogError($"Failed to create post: {ex.Message}");
```

**Correct:**

```csharp
// Named placeholders — each becomes a queryable property
logger.LogInformation("Processing {ItemCount} items for user {UserId}",
    itemCount, userId);
logger.LogError(ex, "Failed to create post for user {UserId}", userId);
```

### Pitfall 3: Not Designing for Unknown Unknowns

Traditional logging captures "known unknowns" — issues you anticipated. But production bugs are often "unknown unknowns." Wide events with rich context enable investigating issues you didn't predict.

**Incorrect:**

```csharp
// Logging only for anticipated issues
public static async Task Handle(CreatePostCommand command, BlogDbContext db, ILogger logger)
{
    logger.LogInformation("Creating post: {Title}", command.Title);
    // ... processing ...
    logger.LogInformation("Done");
}
// Bug: "Posts on Page XYZ are creating but the wrong user is attached"
// Your logs say: "Creating post: My Title" ✓  "Done" ✓
// But you have NO visibility into: which user, which page,
// how long it took, what the post ID was
```

**Correct:**

```csharp
// Wide event captures everything
logger.LogInformation(
    "Handler complete: {Handler} | UserId: {UserId} | PageId: {PageId} | "
    + "PostId: {PostId} | Outcome: {Outcome} | Duration: {Duration:F2}ms",
    handler, userId, pageId, postId, outcome, elapsed);
// Now you can query: WHERE UserId = 'abc' AND Outcome = 'success'
// and see exactly what happened — even for bugs you never anticipated
```

### Pitfall 4: Silent Command Handlers

Command and event handlers that rely entirely on `AuditLoggingBehavior` for logging miss business context. The middleware provides handler name and timing but no business-specific details like user ID, page ID, or affected entity counts.

For most query handlers this is fine. But for mutation handlers (create, update, delete) and event handlers, consider adding `BeginScope` with business context:

```csharp
// In a blog post query handler
public static async Task<IEnumerable<PostSummaryResponse>> Handle(
    GetPostsQuery query,
    BlogDbContext db,
    ILogger logger)
{
    using var scope = logger.BeginScope(new Dictionary<string, object>
    {
        ["PageId"] = query.PageId,
    });

    var posts = await db.BlogPosts
        .Where(p => p.PageId == query.PageId)
        .ToListAsync();

    // AuditLoggingBehavior logs timing
    // BeginScope adds PageId to the same log entry
    return posts.Select(p => new PostSummaryResponse(p.Id, p.Title));
}
```