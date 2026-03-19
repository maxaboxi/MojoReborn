# Mojo.Modules.Blog — Agent Guide

## Purpose

Blog posts, categories, comments, and subscriptions. Supports CRUD, infinite-scroll paging, subscriber email notifications, and category management.

## Entities → Tables

| Entity | Table | Notes |
|---|---|---|
| `BlogPost` | `mp_Blogs` | Legacy columns; ExcludedFromMigrations |
| `BlogCategory` | `mp_BlogCategories` | Legacy columns; ExcludedFromMigrations |
| `BlogComment` | `mp_Comments` | Shared comments table; ExcludedFromMigrations |
| `BlogPostCategory` | `mp_BlogItemCategories` | Join table; ExcludedFromMigrations |
| `BlogSubscription` | `BlogSubscriptions` | New table; managed by EF migrations |

## DbContext

`BlogDbContext` — sets: `BlogPosts`, `Categories`, `BlogComments`, `BlogSubscriptions`

## Features

| Feature | Type | Route | Notes |
|---|---|---|---|
| Posts/CreatePost | Command | `POST /blog/posts` | IFeatureRequest; security-checked |
| Posts/UpdatePost | Command | `PUT /blog/posts` | IFeatureRequest |
| Posts/DeletePost | Command | `DELETE /{pageId}/blog/posts/{id}` | IFeatureRequest |
| Posts/GetPost | Query | `GET /blog/posts/{id}` | Includes paged comments |
| Posts/GetPosts | Query | `GET /blog/posts` | Cursor-based paging (lastPostDate/lastPostId) |
| Categories/* | CRUD | `/blog/category`, `/blog/categories` | IFeatureRequest on mutations |
| Comments/* | CRUD | `/blog/posts/comment` | IFeatureRequest on mutations |
| Blog/Subscribe | Command | `POST /blog/subscribe` | Creates BlogSubscription |
| Blog/Unsubscribe | Command | `POST /blog/unsubscribe` | Removes subscription |
| Blog/GetSubscriptions | Query | `GET /blog/subscriptions` | Current user's subscriptions |
| Blog/NotifySubscribers | Handler | — | Triggered by `PostCreatedEvent`; sends `SaveNotificationCommand` per subscriber |

## Cross-Module

- Publishes `SaveNotificationCommand` (Mojo.Shared) when a post is created → consumed by Notifications module
- Post creation raises `PostCreatedEvent` → `NotifySubscribers` handler
