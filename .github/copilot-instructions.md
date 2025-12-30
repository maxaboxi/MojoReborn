# Mojo.Frontend Copilot Instructions

## Source & Conventions
- Anchor yourself with the LLM playbook in [context.md](context.md) plus stack notes in [Mojo.Frontend/README.md](Mojo.Frontend/README.md); keep guidance scoped to the React client.
- All AI work happens inside [Mojo.Frontend](Mojo.Frontend); backend folders describe the ASP.NET modules but are not edited from here.
- Path aliases (`@app`, `@shared`, `@features`, `@components`) and strict TS settings live in [Mojo.Frontend/vite.config.ts](Mojo.Frontend/vite.config.ts) and [Mojo.Frontend/tsconfig.app.json](Mojo.Frontend/tsconfig.app.json); use them to avoid relative import chains.

## App Shell & Routing
- Providers stack in [Mojo.Frontend/src/app/providers/AppProviders.tsx](Mojo.Frontend/src/app/providers/AppProviders.tsx) as `ThemeProvider → QueryClientProvider → AuthProvider → AppRouter`; mount new globals here.
- Routing is defined in [Mojo.Frontend/src/app/router/routes.tsx](Mojo.Frontend/src/app/router/routes.tsx) and wired via [Mojo.Frontend/src/app/router/AppRouter.tsx](Mojo.Frontend/src/app/router/AppRouter.tsx); prefer `RouteObject` arrays over nested `<Routes>`.
- The shell in [Mojo.Frontend/src/components/AppLayout/AppLayout.tsx](Mojo.Frontend/src/components/AppLayout/AppLayout.tsx) wraps every private page with `Header`, `Sidebar`, and `PostLoginRedirectListener`; keep long-running views inside the `<Outlet/>` region only.
- New feature surfaces should extend feature-specific route files (e.g., [Mojo.Frontend/src/features/blog/routes/index.tsx](Mojo.Frontend/src/features/blog/routes/index.tsx)) so they can be gated with `RequireAuth` as needed.

## Data & API Patterns
- Use the shared axios instance in [Mojo.Frontend/src/shared/api/axiosClient.ts](Mojo.Frontend/src/shared/api/axiosClient.ts); it already sets `withCredentials` and JSON headers so cookies flow to the .NET host.
- Compose URLs with [Mojo.Frontend/src/shared/config/env.ts](Mojo.Frontend/src/shared/config/env.ts) rather than string concatenation; `VITE_API_BASE_URL` defaults to `https://localhost:7001/api`.
- Each feature keeps its own API module plus React Query wrapper (see [Mojo.Frontend/src/features/blog/api/blogApi.ts](Mojo.Frontend/src/features/blog/api/blogApi.ts) + [Mojo.Frontend/src/features/blog/hooks/useBlogPostsQuery.ts](Mojo.Frontend/src/features/blog/hooks/useBlogPostsQuery.ts)); model new calls the same way and surface `queryKey` helpers for cache control.
- Infinite data honors legacy paging contracts (example: `BLOG_POSTS_PAGE_SIZE` in [Mojo.Frontend/src/features/blog/constants.ts](Mojo.Frontend/src/features/blog/constants.ts)); keep server-provided cursors in the React Query `pageParam`.

## Auth & Navigation
- Current-user state comes from [Mojo.Frontend/src/features/auth/providers/AuthProvider.tsx](Mojo.Frontend/src/features/auth/providers/AuthProvider.tsx), which normalizes role casing and exposes `useAuth()`; refresh via `refetchUser` instead of reloading.
- `useCurrentUserQuery` in [Mojo.Frontend/src/features/auth/hooks/useCurrentUserQuery.ts](Mojo.Frontend/src/features/auth/hooks/useCurrentUserQuery.ts) swallows 401s; mirror this guard whenever you add auth-sensitive queries.
- Protect privileged screens with [Mojo.Frontend/src/features/auth/components/RequireAuth.tsx](Mojo.Frontend/src/features/auth/components/RequireAuth.tsx); it records the intended path via [Mojo.Frontend/src/features/auth/utils/postLoginRedirect.ts](Mojo.Frontend/src/features/auth/utils/postLoginRedirect.ts) so users return post-sign-in.
- The listener in [Mojo.Frontend/src/features/auth/components/PostLoginRedirectListener.tsx](Mojo.Frontend/src/features/auth/components/PostLoginRedirectListener.tsx) must stay mounted near the layout header; do not wrap it in suspense or it may miss sessionStorage events.

## Menu-Driven Pages & Blog Context
- Navigation data is fetched once via [Mojo.Frontend/src/shared/hooks/useMenuQuery.ts](Mojo.Frontend/src/shared/hooks/useMenuQuery.ts) and cached for five minutes; rely on `menuItems` instead of hardcoding page IDs.
- URL resolution helpers in [Mojo.Frontend/src/shared/utils/menuUtils.ts](Mojo.Frontend/src/shared/utils/menuUtils.ts) normalize leading/trailing slashes; call them whenever comparing CMS paths.
- Fallback routing ([Mojo.Frontend/src/features/navigation/components/DynamicFeaturePage.tsx](Mojo.Frontend/src/features/navigation/components/DynamicFeaturePage.tsx)) inspects `featureName` to decide which React page to render; extend its switch when a legacy feature becomes available.
- Blog views must discover `pageId/pageUrl` through [Mojo.Frontend/src/features/blog/hooks/useBlogPageContext.ts](Mojo.Frontend/src/features/blog/hooks/useBlogPageContext.ts) and helpers in [Mojo.Frontend/src/features/blog/utils/findBlogPageId.ts](Mojo.Frontend/src/features/blog/utils/findBlogPageId.ts) so queries call the correct CMS page.

## UI & Styling
- Theme toggling (localStorage-backed) is handled in [Mojo.Frontend/src/shared/theme/ThemeProvider.tsx](Mojo.Frontend/src/shared/theme/ThemeProvider.tsx); obtain `mode`/`toggleTheme` via [Mojo.Frontend/src/shared/theme/useTheme.ts](Mojo.Frontend/src/shared/theme/useTheme.ts) instead of re-implementing state.
- Shared feedback components live in [Mojo.Frontend/src/shared/ui](Mojo.Frontend/src/shared/ui); use `LoadingState` and `StatusMessage` for async states instead of ad-hoc spinners.
- Follow the co-located CSS pattern from [Mojo.Frontend/src/components/AppLayout/AppLayout.tsx](Mojo.Frontend/src/components/AppLayout/AppLayout.tsx) (`.css` sibling files) and lean on MUI components (`@mui/material`) for structure.

## Local Workflow
- Use `npm run dev` for the Vite server, `npm run build` for `tsc -b` plus production bundling, `npm run lint` for ESLint, and `npm run preview` to test the built output (see [Mojo.Frontend/package.json](Mojo.Frontend/package.json)).
- The React app expects the ASP.NET backend on `https://localhost:7001`; set `VITE_API_BASE_URL` if your API host changes, otherwise cookie auth will fail.
- APIs rely on authenticated cookies issued by the .NET host, so run the backend (`dotnet run --project Mojo.Web`) alongside the frontend when exercising secured flows.
