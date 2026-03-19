# Implementation Log: audit-fixes — Audit Code Review Fixes

**Plan:** `docs/Done/audit-fixes/audit-fixes-plan.md`
**Branch:** `audit/code-review-fixes`
**Started:** 2026-03-19
**Completed:** 2026-03-19
**Status:** complete

---

## Summary

Implemented all 7 phases of audit fixes from the 2026-02-22 full-stack code review. 28 issues fixed across backend (.NET/Wolverine) and frontend (React/TypeScript). BE-03 excluded (local-only config). FE-07, FE-08 deferred (not needed yet).

---

## Files Changed

| File | Change |
|------|--------|
| `Mojo.Modules.Forum/Domain/Configurations/ForumPostConfiguration.cs` | Added `HasQueryFilter(p => p.Approved)` |
| `Mojo.Modules.Blog/Features/Comments/CreateComment/CreateBlogCommentCommand.cs` | Added `IFeatureRequest`, `[JsonIgnore]` on UserIpAddress |
| `Mojo.Modules.Forum/Features/Posts/CreatePost/CreateForumPostCommand.cs` | Added `[JsonIgnore]` on UserIpAddress |
| `Mojo.Modules.Identity/Features/LoginUser/LoginUserHandler.cs` | Removed `&details=`, `Exception` → `InvalidOperationException` |
| `Mojo.Frontend/src/features/forum/components/ForumPostCard.tsx` | Added DOMPurify sanitization |
| `Mojo.Modules.SiteStructure/Features/SiteStructure/Modules/ModuleResolver.cs` | Fixed `x.Id` → `x.ModuleDefinitionId` |
| `Mojo.Modules.Forum/Features/Threads/EditThread/EditThreadHandler.cs` | Moved auth check post-fetch |
| `Mojo.Modules.Forum/Features/Posts/EditPost/EditForumPostHandler.cs` | Fixed inverted admin logic |
| `Mojo.Modules.Forum/Features/Posts/CreatePost/CreateForumPostHandler.cs` | Removed double SaveChanges |
| `Mojo.Modules.Forum/Features/Threads/CreateThread/CreateThreadHandler.cs` | Added UPDLOCK on ForumSequence |
| `Mojo.Web/Program.cs` | Removed duplicate DbContext registrations, fixed middleware order, CORS from config |
| 14 validator files | `.NotNull()` → `.GreaterThan(0)` on PageId/ForumId/ThreadId/CategoryId/PostId |
| 4 query validators | Added `Amount` upper bound `LessThanOrEqualTo(100)` |
| `Mojo.Modules.Identity/Features/Users/UserService.cs` | `Guid.TryParse` instead of `Guid.Parse` |
| `Mojo.Modules.Identity/Features/Users/GetCurrentUser/GetCurrentUserEndpoint.cs` | `Guid.TryParse` guard |
| `Mojo.Modules.Blog/Features/Posts/CreatePost/CreatePostHandler.cs` | Removed `SlugSuffixRegexCache` |
| `Mojo.Modules.SiteStructure/Features/SiteStructure/Sites/SiteResolver.cs` | `InvalidOperationException`, `IMemoryCache` |
| `Mojo.Modules.SiteStructure/Features/SiteStructure/Menu/GetMenuHandler.cs` | `InvalidOperationException` |
| `Mojo.Modules.Identity/Features/LegacyMigration/MigrateLegacyUserHandler.cs` | `InvalidOperationException` |
| `Mojo.Modules.Notifications/Features/GetNotifications/GetNotificationsHandler.cs` | Added `.Take(50)` |
| `Mojo.Modules.Blog/Features/Comments/DeleteComment/DeleteBlogCommentHandler.cs` | Added `ModerationStatus = 1` |
| `Mojo.Modules.Identity/Features/Permissions/PermissionService.cs` | Legacy compatibility docs |
| `Mojo.Modules.SiteStructure/Features/SiteStructure/FeatureContext/FeatureContextResolver.cs` | `IMemoryCache` |
| `Mojo.Frontend/src/shared/ui/ErrorBoundary.tsx` | New: global error boundary |
| `Mojo.Frontend/src/app/providers/AppProviders.tsx` | ErrorBoundary, auth-gated notifications |
| `Mojo.Frontend/src/shared/api/axiosClient.ts` | Removed no-op interceptor |
| `Mojo.Frontend/src/features/auth/hooks/useCurrentUserQuery.ts` | staleTime → 5 min |
| `Mojo.Frontend/src/shared/hooks/useMenuQuery.ts` | `as const` on query key |
| `Mojo.Frontend/package.json` | `@types/dompurify` → devDependencies |
| `Mojo.Frontend/src/features/forum/constants.ts` | Added `FORUM_FEATURE_NAME` |
| `Mojo.Frontend/src/features/navigation/components/DynamicFeaturePage.tsx` | Use `FORUM_FEATURE_NAME` constant |

---

## Phase Log

### Phase 1: Security & Access Control
- BE-01: Global query filter on ForumPost.Approved
- BE-04: IFeatureRequest on CreateBlogCommentCommand
- BE-05: [JsonIgnore] on UserIpAddress (both commands)
- BE-22: Error details removed from login redirects
- FE-01: DOMPurify sanitization on ForumPostCard
- Commit: bca67b8

### Phase 2: Logic Bugs & Data Correctness
- BE-09: ModuleResolver — fixed wrong ID property
- BE-10: EditThreadHandler — moved auth check post-fetch
- BE-11: EditForumPostHandler — fixed inverted admin logic
- BE-12/13: CreateForumPostHandler — removed double SaveChanges
- BE-25: CreateThreadHandler — UPDLOCK on ForumSequence
- Commit: f70b6fc

### Phase 3: Middleware & Pipeline
- BE-14: Removed 4 duplicate AddDbContext registrations
- BE-06: Moved UseExceptionHandler/UseStatusCodePages before auth
- BE-02: CORS origin from configuration with fallback
- Commit: 862a427

### Phase 4: Validation & Defensive Coding
- BE-07: PageId/ForumId/ThreadId/CategoryId/PostId → GreaterThan(0) (14 validators)
- BE-28: Amount upper bound LessThanOrEqualTo(100) (4 query validators)
- BE-21: Guid.TryParse in UserService + GetCurrentUserEndpoint
- BE-18: Removed SlugSuffixRegexCache (unbounded)
- BE-08: Exception → InvalidOperationException (9 occurrences, 4 files)
- BE-17: GetNotificationsHandler Take(50) pagination limit
- POST-GATE fix: 3 additional validators caught by reviewer
- Commit: 49135a7

### Phase 5: Forum Data Integrity
- BE-24: DeleteBlogCommentHandler — set ModerationStatus=1 on self-delete
- BE-30: PermissionService — legacy compatibility documentation
- Commit: f42376b

### Phase 6: Caching & Performance
- BE-15/16: SiteResolver — IMemoryCache with 5-min sliding expiration
- BE-19: FeatureContextResolver — IMemoryCache with composite key
- Commit: 40525db

### Phase 7: Frontend Improvements
- FE-02: Global ErrorBoundary component + provider tree wrap
- FE-03: Removed no-op axios response interceptor
- FE-04: useCurrentUserQuery staleTime → 5 minutes
- FE-05: Auth-gated NotificationsProvider
- FE-06: MENU_QUERY_KEY as const
- FE-09: @types/dompurify → devDependencies
- FE-10: FORUM_FEATURE_NAME constant + usage
- Commit: 82ff423

---

## Issues & Blockers

| # | Issue | Resolution | Phase |
|---|-------|------------|-------|
| 1 | Phase 4 POST-GATE found 3 missed validators | Fixed immediately, re-verified | 4 |

---

## Lessons Learned

- POST-GATE subagent review caught real issues (Phase 4 missed validators) — worth the extra step
- Terminal scrollback pollution from fastfetch requires /tmp file workaround for reliable output checking
- `tsc --noEmit` empty output = success on this project
