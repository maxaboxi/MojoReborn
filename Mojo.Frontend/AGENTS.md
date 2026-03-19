# Mojo.Frontend — Agent Guide

## Stack

React 19, TypeScript 5.9, Vite, MUI 7.3, React Router 7.9, TanStack Query 5.90, Axios, SignalR

## Path Aliases

`@app` → `src/app`, `@shared` → `src/shared`, `@features` → `src/features`, `@components` → `src/components`

## Structure

```
src/
├── app/
│   ├── providers/AppProviders.tsx   # ThemeProvider → QueryClient → AuthProvider → NotificationsProvider → AppRouter
│   └── router/
│       ├── AppRouter.tsx
│       └── routes.tsx               # RouteObject array
├── components/                      # AppLayout, Header, Sidebar, UserMenu, NavMenuItem
├── shared/
│   ├── api/axiosClient.ts           # Axios instance (withCredentials, JSON, baseURL from env)
│   ├── api/menuApi.ts               # GET /menu
│   ├── config/env.ts                # VITE_API_BASE_URL, buildApiUrl(), buildHubUrl()
│   ├── hooks/useMenuQuery.ts        # Menu data (5 min stale)
│   ├── theme/                       # ThemeProvider, useTheme (localStorage-backed)
│   ├── types/                       # auth.types, menu.types, subscription.types
│   ├── ui/                          # LoadingState, StatusMessage
│   └── utils/menuUtils.ts           # Path normalization, BFS menu search
└── features/
    ├── auth/                        # Login, legacy migration, RequireAuth, PostLoginRedirectListener
    ├── blog/                        # Posts CRUD, categories, comments, subscriptions
    ├── forum/                       # Forum threads/posts
    ├── home/                        # HomePage
    ├── navigation/                  # DynamicFeaturePage (CMS fallback routing)
    └── notifications/               # Real-time notifications via SignalR
```

## Routing

```
/ (AppLayout)
  ├── /           → HomePage
  ├── /blog/*     → blogRoutes (list, detail, create*, edit*, categories*)
  ├── /admin      → Coming Soon
  └── /*          → DynamicFeaturePage (matches CMS menu by URL)
/auth/login       → AuthLoginPage
/auth/migrate-legacy → LegacyMigrationPage

(* = RequireAuth-gated)
```

## Key Patterns

- **Cookie auth**: axios sends `withCredentials: true`; backend on `localhost:7001` issues cookies
- **useCurrentUserQuery**: swallows 401 (returns null); other errors re-thrown
- **Menu-driven routing**: `DynamicFeaturePage` resolves page by pathname from CMS menu, switches on `featureName` to render Blog/Forum/etc.
- **Blog page context**: `useBlogPageContext` discovers `pageId`/`pageUrl` from menu for all blog API calls
- **Infinite scroll**: cursor-based paging via React Query `pageParam` (e.g., `lastPostDate`/`lastPostId`)
- **PostLoginRedirectListener**: mounted in AppLayout; watches auth state, consumes sessionStorage redirect on login
- **SignalR**: `NotificationsProvider` connects to `/hubs/notifications` for real-time push

## Commands

```bash
npm run dev       # Vite dev server (:5173)
npm run build     # tsc -b + vite build
npm run lint      # ESLint
npm run preview   # Preview production build
```
