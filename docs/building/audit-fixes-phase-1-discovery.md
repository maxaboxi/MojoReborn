# Discovery: Phase 1 - Security & Access Control

## Files Found
- Mojo.Modules.Forum/Domain/Configurations/ForumPostConfiguration.cs
- Mojo.Modules.Forum/Domain/Entities/ForumPost.cs
- Mojo.Modules.Forum/Features/Threads/GetThread/GetThreadHandler.cs
- Mojo.Modules.Forum/Features/Threads/GetThreads/GetThreadsHandler.cs
- Mojo.Modules.Blog/Features/Comments/CreateComment/CreateBlogCommentCommand.cs
- Mojo.Modules.Forum/Features/Posts/CreatePost/CreateForumPostCommand.cs
- Mojo.Modules.Identity/Features/LoginUser/LoginUserHandler.cs
- Mojo.Frontend/src/features/forum/components/ForumPostCard.tsx
- Mojo.Shared/Interfaces/Security/IFeatureRequest.cs

## Current State by Issue

### BE-01: ForumPost has `Approved` bool, no query filter exists. No admin handlers use `.IgnoreQueryFilters()` yet.

### BE-04: CreateBlogCommentCommand missing `: IFeatureRequest` + needs `RequiresEditPermission` and `UserCanEdit` properties. Other commands like CreateForumPostCommand already implement them.

### BE-05: Both commands have client-settable `UserIpAddress { get; set; }`. Project uses System.Text.Json. Need `[System.Text.Json.Serialization.JsonIgnore]`.

### BE-22: `&details={errors}` found in LoginUserHandler redirect URLs. Need to verify exact count of occurrences.

### FE-01: dompurify installed. Blog uses it correctly (BlogPostDetail.tsx). Forum's ForumPostCard.tsx uses raw `dangerouslySetInnerHTML` without sanitization.

## Gaps
- BE-04 needs TWO additional properties beyond interface: `RequiresEditPermission => true` and `UserCanEdit => false`
- No admin moderation handlers exist yet that need `.IgnoreQueryFilters()` for BE-01

## Recommendation
BUILD — All issues confirmed, ready for implementation.
