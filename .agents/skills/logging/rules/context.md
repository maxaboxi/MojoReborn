---
title: Context, Cardinality, and Dimensionality
impact: CRITICAL
tags: logging, context, cardinality, dimensionality, dotnet, wolverine
---

## Context, Cardinality, and Dimensionality

**Impact: CRITICAL**

Wide events must be context-rich with high cardinality and high dimensionality. This enables you to answer questions you haven't anticipated yet — the "unknown unknowns" that traditional logging misses.

### High Cardinality

High cardinality means a field can have many unique values. UserId, PageId, PostId, and ThreadId are high cardinality fields. Your logging must support querying against any specific value. Without high cardinality support, you cannot debug issues for specific users or pages.

### High Dimensionality

High dimensionality means your events have many properties (10-30+). More dimensions mean more questions you can answer without redeploying code.

```csharp
// Wide event from a Wolverine message handler
logger.LogInformation(
    "Handler complete: {Handler} | UserId: {UserId} | PageId: {PageId} | "
    + "PostId: {PostId} | Items: {ItemCount} | "
    + "Outcome: {Outcome} | Duration: {Duration:F2}ms",
    nameof(CreatePostHandler),
    command.UserId,                      // User ID (HIGH CARDINALITY)
    command.PageId,                      // Page ID (HIGH CARDINALITY)
    postId,                              // Created entity ID
    itemCount,                           // Business metric
    outcome,                             // success/error
    stopwatch.Elapsed.TotalMilliseconds  // Performance metric
);

// With BeginScope adding:
// UserId, PageId — automatically on every log entry
```

### Always Include Business Context

Include business-specific context, not just technical details. User IDs, page IDs, item counts, feature names — this context helps prioritize issues and understand business impact.

```csharp
// WITHOUT business context — useless for triage
logger.LogError(ex, "Handler failed");
// You know: something broke. You don't know: for whom, on which page, doing what.

// WITH business context — actionable
logger.LogError(ex,
    "Handler failed: {Handler} | UserId: {UserId} | "
    + "PageId: {PageId} | PostId: {PostId} | Duration: {Duration:F2}ms",
    nameof(CreatePostHandler), command.UserId,
    command.PageId, postId, elapsed);
// Now you KNOW: CreatePost for User ABC on Page XYZ failed
```

Business context transforms debugging from "something broke" to "this user's blog post creation failed on this page."

### Environment Context (Don't Duplicate Per-Call)

When observability tooling is configured (OpenTelemetry, Application Insights), environment and deployment information should be handled at the infrastructure level — not in individual log calls.

```csharp
// INCORRECT — redundant, should be handled at infrastructure level
logger.LogInformation(
    "Handler complete: {Handler} | Service: {Service} | Version: {Version} | Env: {Env}",
    handler, "MojoReborn", "1.0.0", "Production");

// CORRECT — environment handled at infra level, focus on business context
logger.LogInformation(
    "Handler complete: {Handler} | UserId: {UserId} | Items: {ItemCount}",
    handler, userId, itemCount);
```

Adding environment fields per-call wastes bytes and creates inconsistency risk.