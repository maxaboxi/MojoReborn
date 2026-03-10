# Audit Fixes — Implementation Plan

**Date:** 2026-03-10
**Source:** `audit_suggestions.md` (2026-02-22 full-stack audit)
**Excluded:** BE-03 (local-only `ValidationKey` — intentional, will not be deployed)
**Status:** Complete — all 7 phases implemented on `audit/code-review-fixes`

---

## Execution Phases

Work is grouped into **7 phases** by dependency and risk level. Each phase is independently committable. Phases 1–3 are security/correctness fixes and should be done first. Phases 4–7 are quality/hardening improvements.

---

## Phase 1 — Security & Access Control (Critical)

### BE-01: Add global query filter for forum post moderation status

**Problem:** `ForumPost` has `ModStatus` and `Approved` properties but no `HasQueryFilter`. Unapproved/moderated posts leak to users.

**Approach A — Global query filter on `ForumPostConfiguration`:**
Add `.HasQueryFilter(p => p.Approved)` in the EF configuration. All queries automatically exclude unapproved posts. Admin queries use `.IgnoreQueryFilters()`.

**Approach B — Manual filter in each handler:**
Add `.Where(x => x.Approved)` to `GetThreadHandler` and `GetThreadsHandler`. Explicit but error-prone for future features.

**Choice: A — Global query filter.**
Rationale: Convention-based safety. New queries cannot accidentally leak unapproved posts. Admin handlers explicitly opt out. This is the standard EF Core pattern for soft deletes and moderation.

**Changes:**
- `Mojo.Modules.Forum/Domain/Configurations/ForumPostConfiguration.cs` — Add `builder.HasQueryFilter(p => p.Approved);`
- `Mojo.Modules.Forum/Features/Threads/GetThread/GetThreadHandler.cs` — No change needed (filter applies automatically). But verify the Include for posts still works.
- Review any admin moderation handlers that need `.IgnoreQueryFilters()` (none exist yet, so no changes needed now).

---

### BE-04: Add `IFeatureRequest` to `CreateBlogCommentCommand`

**Problem:** `CreateBlogCommentCommand` does not implement `IFeatureRequest`, so `FeatureSecurityMiddleware` never runs. Any authenticated user can create comments regardless of page permissions.

**Changes:**
- `Mojo.Modules.Blog/Features/Comments/CreateComment/CreateBlogCommentCommand.cs` — Add `: IFeatureRequest` to the record declaration. The `Name` and `PageId` properties already exist, which is all `IFeatureRequest` requires.

---

### BE-05: Remove client-settable `UserIpAddress` from commands

**Problem:** `UserIpAddress` is a `{ get; set; }` property on command records. Clients can spoof IPs. Both endpoints already extract the IP server-side from `HttpContext.Connection.RemoteIpAddress`, but the property is still deserializable from the request body.

**Approach A — Use `[JsonIgnore]` on the property:**
Prevents deserialization from request body while keeping the property settable by the endpoint.

**Approach B — Remove property from command, pass IP as separate parameter to handler:**
Cleaner separation but requires changing handler signatures.

**Choice: A — `[JsonIgnore]`.**
Rationale: Minimal change. The endpoint already sets the value server-side. `[JsonIgnore]` prevents client spoofing without changing handler contracts.

**Changes:**
- `Mojo.Modules.Blog/Features/Comments/CreateComment/CreateBlogCommentCommand.cs` — Add `[JsonIgnore]` to `UserIpAddress`
- `Mojo.Modules.Forum/Features/Posts/CreatePost/CreateForumPostCommand.cs` — Add `[JsonIgnore]` to `UserIpAddress`

---

### FE-01: Sanitize forum post HTML with DOMPurify

**Problem:** `ForumPostCard.tsx` uses `dangerouslySetInnerHTML={{ __html: post.content }}` without sanitization. XSS vulnerability. Blog module correctly uses DOMPurify.

**Changes:**
- `Mojo.Frontend/src/features/forum/components/ForumPostCard.tsx` — Import `DOMPurify` and wrap content: `dangerouslySetInnerHTML={{ __html: DOMPurify.sanitize(post.content) }}`

---

### BE-22: Stop leaking error details in login redirect URL

**Problem:** `LoginUserHandler` passes Identity framework error descriptions directly into the redirect URL query string, exposing internal details to users.

**Changes:**
- `Mojo.Modules.Identity/Features/LoginUser/LoginUserHandler.cs` — Remove `&details={errors}` from both redirect URLs (lines ~87 and ~155). The errors are already logged via `logger.LogError`. Keep only the generic error codes (`creation_failed`, `legacy_creation_failed`).

---

## Phase 2 — Logic Bugs & Data Correctness (Critical/High)

### BE-09: Fix `ModuleResolver` FK bug

**Problem:** Second query filters `Modules` by `x.Id` (module PK) instead of `x.ModuleDefinitionId` (FK to `ModuleDefinition`).

**Changes:**
- `Mojo.Modules.SiteStructure/Features/GetModules/ModuleResolver.cs` — Change `.Where(x => moduleDefinitionIds.Contains(x.Id))` to `.Where(x => moduleDefinitionIds.Contains(x.ModuleDefinitionId))`

---

### BE-10: Allow admins to edit other users' threads

**Problem:** `EditThreadHandler` filters by `StartedByUserId` in the query itself, so admins get a 404 instead of being allowed to edit.

**Approach A — Split query + auth check (match `EditForumPostHandler` pattern):**
Query without the `StartedByUserId` filter, then check ownership vs admin status after fetching.

**Approach B — Two separate queries with OR logic in the WHERE clause:**
Single query with `(x.StartedByUserId == legacyId || isAdmin)`.

**Choice: A — Split query + auth check.**
Rationale: Consistent with `EditForumPostHandler` and `DeleteBlogCommentHandler` patterns. Clear separation of "does it exist?" from "can this user edit it?".

**Changes:**
- `Mojo.Modules.Forum/Features/Threads/EditThread/EditThreadHandler.cs` — Remove `x.StartedByUserId == securityContext.User.LegacyId` from the query. Add post-fetch authorization check: `if (existingThread.StartedByUserId != securityContext.User.LegacyId && !securityContext.IsAdmin) throw new UnauthorizedAccessException();`

---

### BE-11: Fix moderator tag applied to admin's own post edits

**Problem:** `EditForumPostHandler` applies `[edited by Moderator]` tag based on `IsAdmin` flag, even when the admin is the post's own author.

**Changes:**
- `Mojo.Modules.Forum/Features/Posts/EditPost/EditForumPostHandler.cs` — Change condition from `!securityContext.IsAdmin` to `existingPost.Author.Id == securityContext.User.LegacyId` (i.e., if editor is the author, save content as-is; if editor is a different person who is admin, append moderator tag).

---

### BE-12 + BE-13: Fix transaction safety and double SaveChanges in `CreateForumPostHandler`

**Problem:** `UPDLOCK` hint without explicit transaction (Wolverine EF transaction wrapping should handle this, but the double `SaveChangesAsync` is redundant/risky).

**Approach:** Wolverine's `UseEntityFrameworkCoreTransactions()` is configured in `Program.cs`, which wraps handlers in a transaction. The `UPDLOCK` is therefore valid. The fix is simply to remove the first `SaveChangesAsync` — let the single save at the end handle everything atomically.

**Changes:**
- `Mojo.Modules.Forum/Features/Posts/CreatePost/CreateForumPostHandler.cs` — Remove the first `await db.SaveChangesAsync(ct);` (line ~51). Move the reply link logic before the single remaining `SaveChangesAsync`. The whole operation saves atomically within the Wolverine-managed transaction.

---

### BE-25: Fix race condition on `ForumSequence` in `CreateThreadHandler`

**Problem:** `MAX(ForumSequence) + 1` is not atomic. Two concurrent creates can get the same sequence number.

**Approach A — Use `FromSqlRaw` with `UPDLOCK` (match `CreateForumPostHandler`):**
Lock the relevant forum row while reading max sequence. Within the Wolverine transaction, this prevents concurrent reads.

**Approach B — Use a database DEFAULT or SEQUENCE object:**
Add a SQL Server SEQUENCE and always let the DB assign the value.

**Approach C — Use `UPDLOCK` on the Forum row to serialize thread creation per forum:**
Lock the parent `Forum` row, then read max safely.

**Choice: A — `UPDLOCK` on relevant rows.**
Rationale: Consistent with the existing `CreateForumPostHandler` pattern. Wolverine transaction wrapping makes it effective. No schema changes needed.

**Changes:**
- `Mojo.Modules.Forum/Features/Threads/CreateThread/CreateThreadHandler.cs` — Use `FromSqlRaw` with `UPDLOCK` on `mp_Forums` row by `ForumId` before reading max sequence. This serializes thread creation per-forum within the Wolverine transaction.

---

## Phase 3 — Middleware & Pipeline (High)

### BE-06: Fix middleware ordering — move `UseExceptionHandler` before auth

**Problem:** `UseExceptionHandler()` is placed after `UseAuthorization()`. Exceptions from auth middleware produce raw 500s instead of ProblemDetails.

**Changes:**
- `Mojo.Web/Program.cs` — Move `app.UseExceptionHandler();` and `app.UseStatusCodePages();` to immediately after `app.UseRouting();`, before `app.UseCors()`.

Correct order:
```
UseRouting
UseExceptionHandler     ← moved up
UseStatusCodePages      ← moved up
UseCors
UseHttpsRedirection
UseAuthentication
UseAuthorization
```

---

### BE-02: Read CORS origin from configuration

**Problem:** CORS origin is hardcoded to `http://localhost:5173`. `appsettings.json` already has `Frontend:Url`.

**Changes:**
- `Mojo.Web/Program.cs` — Replace hardcoded string with `builder.Configuration["Frontend:Url"]` (with null check / fallback to `http://localhost:5173` for dev).
- `Mojo.Web/appsettings.Development.json` — Ensure `Frontend:Url` is set to `http://localhost:5173`.

---

### BE-14: Remove duplicate DbContext registrations

**Problem:** Every `DbContext` is registered twice: once via `builder.Services.AddDbContext<T>()` and again via `opts.Services.AddDbContextWithWolverineIntegration<T>()`. The Wolverine registration calls `AddDbContext` internally.

**Changes:**
- `Mojo.Web/Program.cs` — Remove the 4 `builder.Services.AddDbContext<T>()` blocks (lines ~40-55). Keep only the Wolverine registrations inside `UseWolverine`.

---

## Phase 4 — Input Validation & Defensive Coding (High/Medium)

### BE-07: Add upper-bound validation on `Amount` parameters

**Problem:** `Amount` parameter in `GetPostsQuery` has no maximum. Clients can request `?amount=999999`.

**Changes — add `.LessThanOrEqualTo(100)` (or appropriate max) to validators:**
- `Mojo.Modules.Blog/Features/Posts/GetPosts/GetPostsQuery.cs` — Add `RuleFor(x => x.Amount).LessThanOrEqualTo(100).When(x => x.Amount.HasValue);`
- Review and apply same pattern to: `GetThreadsQuery`, `GetThreadQuery` validators.

### BE-28: Fix `NotNull` vs `NotEmpty` for value-type `PageId`

**Problem:** `.NotNull()` on `int PageId` is always true (value types can't be null). `PageId=0` slips through.

**Changes — replace `.NotNull()` with `.GreaterThan(0)` across all validators:**
- `Mojo.Modules.Blog/Features/Posts/GetPosts/GetPostsQuery.cs`
- `Mojo.Modules.Blog/Features/Comments/CreateComment/CreateBlogCommentCommand.cs`
- `Mojo.Modules.Forum/Features/Threads/GetThread/GetThreadQuery.cs`
- `Mojo.Modules.Forum/Features/Threads/GetThreads/GetThreadsQuery.cs`
- Any other validators with `.NotNull()` on `int PageId` — search with `grep -rn "NotNull.*PageId"` across Blog and Forum modules.

---

### BE-21: Use `Guid.TryParse` instead of `Guid.Parse` for user ID claims

**Problem:** `Guid.Parse(userId)` throws `FormatException` if claim value is malformed.

**Changes:**
- `Mojo.Modules.Identity/Features/Services/UserService.cs` — Replace `Guid.Parse(userId)` with `Guid.TryParse` + early return null.
- `Mojo.Modules.Identity/Features/GetCurrentUser/GetCurrentUserEndpoint.cs` — Same pattern.

---

### BE-08: Replace unbounded regex cache with inline regex

**Problem:** `SlugSuffixRegexCache` is a `ConcurrentDictionary<string, Regex>` that never evicts entries. Memory leak over years of posts.

**Approach A — Remove caching, inline the regex:**
`new Regex(...)` per call. The regex is simple and the operation is infrequent (only on post creation).

**Approach B — Use a bounded cache (e.g., `MemoryCacheEntryOptions` with size limit):**
Add complexity for marginal gain.

**Choice: A — Remove caching, inline.**
Rationale: Post creation is a low-frequency operation. The compiled regex pattern is trivial. Unbounded caching is worse than no caching here.

**Changes:**
- `Mojo.Modules.Blog/Features/Posts/CreatePost/CreatePostHandler.cs` — Remove `SlugSuffixRegexCache` dictionary. Change `GetSuffixRegex` to return `new Regex(...)` directly (without `RegexOptions.Compiled` since it's one-shot).

---

### BE-17: Replace bare `throw new Exception(...)` with specific types

**Problem:** 9 occurrences of `throw new Exception(...)` for configuration issues. These produce untyped 500 errors.

**Changes — replace with `InvalidOperationException`:**
- `Mojo.Modules.Identity/Features/LoginUser/LoginUserHandler.cs` — Both `throw new Exception(...)` → `throw new InvalidOperationException(...)`
- Search for other occurrences in: `MigrateLegacyUserHandler.cs`, `SiteResolver.cs`, `GetMenuHandler.cs` — apply same change.

---

### BE-18: Add pagination limit to `GetNotificationsHandler`

**Problem:** Unbounded query returns ALL notifications for a user.

**Changes:**
- `Mojo.Modules.Notifications/Features/GetNotifications/GetNotificationsHandler.cs` — Add `.Take(50)` (or a configurable constant) before `.ToListAsync()`.

---

## Phase 5 — Forum Data Integrity (Medium)

### BE-24: Set `ModerationStatus` on self-deleted blog comments

**Problem:** When a user deletes their own comment, content is replaced with `[Deleted by User]` but `ModerationStatus` isn't updated.

**Changes:**
- `Mojo.Modules.Blog/Features/Comments/DeleteComment/DeleteBlogCommentHandler.cs` — In the `else` branch (self-deletion), add `comment.ModerationStatus = 1;` to match the admin-deletion behavior.

---

## Phase 6 — Caching & Performance (Medium)

### BE-15 + BE-16 + BE-19: Implement `IMemoryCache` for SiteResolver and FeatureContextResolver

**Problem:** `IMemoryCache` is registered but never used. `SiteResolver` and `FeatureContextResolver` query the DB on every request for data that rarely changes.

**Approach A — Cache in `SiteResolver` and `FeatureContextResolver` using `IMemoryCache`:**
Inject `IMemoryCache`, cache with 5-minute sliding expiration. Simple, effective for single-server.

**Approach B — Cache in middleware layer above both resolvers:**
Add a caching decorator. More complex, separates concerns.

**Choice: A — Direct `IMemoryCache` injection.**
Rationale: These are module-internal services. Adding cache decorators is over-engineering for two services. `IMemoryCache` is already registered.

**Changes:**
- `Mojo.Modules.SiteStructure/Features/GetSite/SiteResolver.cs` — Inject `IMemoryCache`, wrap DB query in `GetOrCreateAsync` with 5-min expiration. Remove the instance-field cache.
- `Mojo.Modules.SiteStructure/Features/GetFeatureContext/FeatureContextResolver.cs` — Inject `IMemoryCache`, cache by `(pageId, featureName)` composite key with 5-min expiration.

---

### BE-20: Note on `GetMenuHandler` performance

**Problem:** `EF.Functions.Like` generates N `LIKE` clauses per role. Could be slow with many roles/pages.

**Decision: Defer.** This can be addressed by caching menu results (Phase 6 caching work will help since the menu data changes rarely). A dedicated refactor of the menu query is lower priority than the caching work above. Document for future optimization.

---

## Phase 7 — Frontend Improvements (Medium/Low)

### FE-02: Add global error boundary

**Changes:**
- Create `Mojo.Frontend/src/shared/ui/ErrorBoundary.tsx` — React class component error boundary with fallback UI.
- `Mojo.Frontend/src/app/providers/AppProviders.tsx` — Wrap the provider tree in `<ErrorBoundary>`.

---

### FE-03: Remove no-op axios interceptor

**Changes:**
- `Mojo.Frontend/src/shared/api/axiosClient.ts` — Remove the response interceptor (lines 11-13). It's the default behavior.

---

### FE-04: Set reasonable `staleTime` on `useCurrentUserQuery`

**Problem:** `staleTime: 0` causes refetch on every mount/navigation.

**Changes:**
- `Mojo.Frontend/src/features/auth/hooks/useCurrentUserQuery.ts` — Change `staleTime: 0` to `staleTime: 5 * 60 * 1000` (5 minutes). The auth provider already calls `refetchUser` on login/logout, so manual cache invalidation is handled.

---

### FE-05: Gate `NotificationsProvider` on authentication state

**Problem:** `useNotificationsQuery` fires a 401 request for unauthenticated users.

**Changes:**
- `Mojo.Frontend/src/app/providers/AppProviders.tsx` — Conditionally render `<NotificationsProvider>` only when `isAuthenticated` is true from `useAuth()`. This requires moving it inside a component that has access to `AuthProvider` context, or using a wrapper component.

---

### FE-06: Add `as const` to `MENU_QUERY_KEY`

**Changes:**
- `Mojo.Frontend/src/shared/hooks/useMenuQuery.ts` — Change `['menu']` to `['menu'] as const`.

---

### FE-07: Map missing blog comment fields

**Problem:** `mapCommentDto` drops `modifiedAt`, `moderatedBy`, and `moderationReason`.

**Decision: Defer.** These fields aren't used in the current UI. When comment moderation UI is built, these fields should be mapped. Low priority.

---

### FE-09: Move `@types/dompurify` to devDependencies

**Changes:**
- `Mojo.Frontend/package.json` — Move `@types/dompurify` from `dependencies` to `devDependencies`.

---

### FE-10: Extract forum feature name to a constant

**Changes:**
- Create or add `FORUM_FEATURE_NAME` constant in `Mojo.Frontend/src/features/forum/constants.ts` (or create the file if it doesn't exist).
- `Mojo.Frontend/src/features/navigation/components/DynamicFeaturePage.tsx` — Import and use `FORUM_FEATURE_NAME` instead of hardcoded `'ForumsFeatureName'`.

---

## Informational / No-Action Items

### BE-26 + BE-27: Empty event handlers (`UserDeletedHandler`, `BlogDeletedHandler`)

**Decision: Leave as-is.** These are intentional stubs for future cleanup logic. They serve as documented extension points. Removing them would lose the "this event should be handled here" marker.

---

### BE-29: `CreatePostEndpoint` discards `CreationResponse.Url`

**Decision: Defer.** This is an API design inconsistency, not a bug. The frontend doesn't use 201 + Location header. Can be addressed when API conventions are standardized. Low priority.

---

### BE-30: `PermissionService.CanEdit` returns `true` for empty `EditRoles`

**Decision: Document, don't change.** This is intentional legacy compatibility behavior. The condition also checks for `"All Users"` explicitly. The existing behavior matches the legacy MojoPortal system where empty edit roles = open to all authenticated users.

**Changes:**
- Add a comment in `PermissionService.cs` documenting this behavior: `// Legacy compatibility: empty EditRoles means all authenticated users can edit.`

---

### BE-23: No rate limiting

**Decision: Defer.** Rate limiting is a cross-cutting infrastructure concern that requires design decisions about limits, scopes (per-user, per-IP, per-endpoint), and response behavior. Should be a separate design document. Not included in this audit fix plan.

---

### FE-08: No test infrastructure

**Decision: Defer.** Setting up Vitest, test utilities, and establishing patterns is a separate initiative. Tracked separately from audit fixes.

---

## Phase Summary

| Phase | Issues | Risk | Scope |
|-------|--------|------|-------|
| 1 — Security & Access Control | BE-01, BE-04, BE-05, BE-22, FE-01 | Critical | 6 files |
| 2 — Logic Bugs & Data Correctness | BE-09, BE-10, BE-11, BE-12/13, BE-25 | Critical/High | 5 files |
| 3 — Middleware & Pipeline | BE-02, BE-06, BE-14 | High | 1 file (Program.cs) |
| 4 — Input Validation & Defensive Coding | BE-07, BE-08, BE-17, BE-18, BE-21, BE-28 | High/Medium | ~10 files |
| 5 — Forum Data Integrity | BE-24 | Medium | 1 file |
| 6 — Caching & Performance | BE-15, BE-16, BE-19 | Medium | 2 files |
| 7 — Frontend Improvements | FE-02, FE-03, FE-04, FE-05, FE-06, FE-09, FE-10 | Medium/Low | ~8 files |

**Deferred:** BE-20 (menu query perf), BE-23 (rate limiting), BE-29 (201 status), FE-07 (comment fields), FE-08 (test infra)
**No action:** BE-03 (excluded), BE-26/27 (intentional stubs), BE-30 (documented legacy behavior)
