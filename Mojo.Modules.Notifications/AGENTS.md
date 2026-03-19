# Mojo.Modules.Notifications — Agent Guide

## Purpose

User notifications with real-time delivery via SignalR, scheduled cleanup, and cross-module consumption.

## Entities → Tables

| Entity | Table | Notes |
|---|---|---|
| `UserNotification` | `UserNotifications` | New table; managed by EF migrations |

## DbContext

`NotificationsDbContext` — sets: `UserNotifications`

## Features

| Feature | Type | Route | Notes |
|---|---|---|---|
| SaveNotification | Handler | — | Consumes `SaveNotificationCommand` (Mojo.Shared); creates notification + pushes via SignalR |
| GetNotifications | Query | `GET /notifications` | Current user's notifications |
| NotificationRead | Command | `PUT /notifications/{id}/read` | Marks notification as read |
| DeleteNotifications | Scheduled | — | Scheduler + handler; cleans up old notifications |
| UserDeleted | Handler | — | Consumes `SubscriberDeletedEvent`; removes user's notifications |

## SignalR

- **NotificationsHub** at `/hubs/notifications`
- Requires authentication
- Groups by `user:{userId}` — each user receives their own notifications in real-time

## Cross-Module

- Consumes `SaveNotificationCommand` from any module (currently Blog)
- Consumes `SubscriberDeletedEvent` from Mojo.Shared
