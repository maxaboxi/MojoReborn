# Mojo.Modules.Forum — Agent Guide

## Purpose

Discussion forums with threads and posts. Supports forum-level and thread-level subscriptions.

## Entities → Tables

| Entity | Table | Notes |
|---|---|---|
| `ForumEntity` | `mp_Forums` | Legacy; ExcludedFromMigrations |
| `ForumThread` | `mp_ForumThreads` | Legacy; ExcludedFromMigrations |
| `ForumPost` | `mp_ForumPosts` | Legacy; ExcludedFromMigrations |
| `ForumUser` | `mp_ForumUsers` | Legacy; ExcludedFromMigrations |
| `ForumSubscription` | `mp_ForumSubscriptions` | Legacy; ExcludedFromMigrations |
| `ForumThreadSubscription` | `mp_ForumThreadSubscriptions` | Legacy; ExcludedFromMigrations |
| `ForumPostReplyLink` | (join table) | Reply chain tracking |

## DbContext

`ForumDbContext` — sets: `Forums`, `ForumThreads`, `ForumPosts`, `ForumPostReplyLinks`, `ForumSubscriptions`, `ForumThreadSubscriptions`

## Features

| Feature | Type | Route | Notes |
|---|---|---|---|
| Threads/CreateThread | Command | `POST /{pageId}/forum/threads` | IFeatureRequest |
| Threads/UpdateThread | Command | `PUT /{pageId}/forum/threads` | IFeatureRequest |
| Threads/DeleteThread | Command | `DELETE /{pageId}/forum/threads/{id}` | IFeatureRequest |
| Threads/GetThread | Query | `GET /{pageId}/forum/threads/{id}` | Includes paged posts |
| Threads/GetThreads | Query | `GET /{pageId}/forum/threads` | Paged thread listing |
| Posts/CreatePost | Command | `POST /{pageId}/forum/posts` | IFeatureRequest |
| Posts/UpdatePost | Command | `PUT /{pageId}/forum/posts` | IFeatureRequest |
| Posts/DeletePost | Command | `DELETE /{pageId}/forum/posts/{id}` | IFeatureRequest |
| Forum/GetSubscriptions | Query | `GET /forum/subscriptions` | Current user's subscriptions |
