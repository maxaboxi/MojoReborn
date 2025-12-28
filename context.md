## Mojo.Frontend context (LLM guide)

- **Stack**: Vite + React 19 + TS strict, MUI, TanStack Query, React Router v7. Aliases: `@app/*`, `@shared/*`, `@features/*`, `@components/*` (see `vite.config.ts`, `tsconfig.app.json`).
- **App shell**: `src/main.tsx` renders `AppProviders` → `ThemeProvider` (light/dark persisted), `QueryClientProvider`, `AuthProvider`, then `AppRouter`.
- **Routing**: `src/app/router/routes.tsx` uses `AppLayout` (Header, Sidebar, Outlet, post-login redirect listener). Feature routes: blog (`@features/blog/routes`), forum (`@features/forum/routes`), auth (`/auth/login`, `/auth/migrate-legacy`), fallback `DynamicFeaturePage` that maps menu featureName → feature component.
- **Data access**: `src/shared/api/axiosClient.ts` (baseURL from `VITE_API_BASE_URL`, withCredentials). Prefer feature-specific API modules + React Query hooks; set sensible `queryKey`s; no `.then` chains—use async/await.
- **Auth**: `AuthProvider` exposes `useAuth()` for `user`, `isAuthenticated`, `hasRole`, `refetchUser`. Gate with `RequireAuth` and use `postLoginRedirect` helpers for login redirects. Keep role checks case-insensitive.
- **Menus / page context**: `useMenuQuery` loads navigation and page metadata; blog pages derive `pageId/pageUrl` from menu (blog routes need this).
- **Styling**: Favor small, focused components per feature folder (`components/`, `pages/`, `hooks/`, `api/`, `types/`). Prefer duplication over “god” components. Put styles in a co-located `.css` file and import it; avoid inline styles except tiny one-offs or MUI `sx` when necessary. Keep shared UI in `src/shared/ui`.
- **Blog**: Uses infinite query for lists, comment CRUD with optimistic refetch, and pageId-aware routes. Respect `BLOG_COMMENTS_PAGE_SIZE` and `blogPageUrl` query when linking.
- **Forum**: `ForumFeaturePage` dispatches to threads vs thread detail based on URL identifiers.
- **Theme**: Use `useTheme()` from `@shared/theme` to toggle light/dark; avoid hardcoded colors—use theme palette/typography.
- **Env/config**: Build API URLs with `buildApiUrl` (`@shared/config/env`). Keep new constants/config under `src/shared/constants` or feature-local `constants.ts`.
- **Patterns**: Keep components small and testable; lift side effects into hooks; avoid inline fetches in components—wrap in hooks. Validate props and handle loading/error states with shared `LoadingState` and `StatusMessage`. Use TypeScript types from feature `types/` or `@shared/types`.
