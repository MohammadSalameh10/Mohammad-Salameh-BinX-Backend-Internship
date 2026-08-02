# Week 3 — Day 1: REST API Design Principles & Resource Modeling

## Overview

This exercise focused on designing a RESTful API before implementing it in code.

A Task Tracker domain was selected, and its resources, endpoints, HTTP methods, status codes, nested relationships, and versioning convention were documented.

No backend implementation was required for this exercise. The API requests were organized in Postman as a resource map.

## Learning Objectives

- Explain what makes an API genuinely RESTful.
- Design an API around resources instead of actions.
- Apply consistent resource naming conventions.
- Use HTTP methods according to their intended purpose.
- Select appropriate HTTP status codes.
- Define an API versioning convention.

## Core Resources

The Task Tracker API contains the following resources:

```text
users
tasks
comments
```

All resources are named using plural nouns.

The primary resource used for the complete endpoint map is:

```text
tasks
```

## RESTful Design

RESTful URLs represent resources using nouns.

Avoid action-based routes such as:

```http
GET /api/getTasks
POST /api/createTask
POST /api/deleteTask
```

Use HTTP methods to represent the action:

```http
GET    /api/v1/tasks
POST   /api/v1/tasks
PUT    /api/v1/tasks/{id}
DELETE /api/v1/tasks/{id}
```

## Task Resource Map

| Method | Endpoint | Purpose |
|---|---|---|
| `GET` | `/api/v1/tasks` | Retrieve all tasks |
| `GET` | `/api/v1/tasks/{id}` | Retrieve one task |
| `POST` | `/api/v1/tasks` | Create a new task |
| `PUT` | `/api/v1/tasks/{id}` | Replace an existing task |
| `DELETE` | `/api/v1/tasks/{id}` | Delete a task |
| `GET` | `/api/v1/users/{userId}/tasks` | Retrieve tasks belonging to a user |

## HTTP Status Codes

| Endpoint | Success Response | Error Response |
|---|---|---|
| `GET /api/v1/tasks` | `200 OK` | `401 Unauthorized` if authentication is required |
| `GET /api/v1/tasks/{id}` | `200 OK` | `404 Not Found` |
| `POST /api/v1/tasks` | `201 Created` | `400 Bad Request` |
| `PUT /api/v1/tasks/{id}` | `200 OK` | `400 Bad Request` or `404 Not Found` |
| `DELETE /api/v1/tasks/{id}` | `204 No Content` | `404 Not Found` |
| `GET /api/v1/users/{userId}/tasks` | `200 OK` | `404 Not Found` |

A successful `POST` request should also return a `Location` header pointing to the newly created resource.

Example:

```text
Location: /api/v1/tasks/1
```

## Nested Resource

The following endpoint represents the ownership relationship between users and tasks:

```http
GET /api/v1/users/{userId}/tasks
```

Example:

```http
GET /api/v1/users/1/tasks
```

This endpoint retrieves all tasks belonging to the specified user.

## API Versioning

The API uses URL-based versioning.

All endpoints begin with:

```text
/api/v1
```

Examples:

```http
/api/v1/tasks
/api/v1/users
/api/v1/comments
```

A future breaking change could be introduced using:

```text
/api/v2
```

This allows existing consumers to continue using version 1 while newer consumers migrate to version 2.

## Postman Collection

A Postman collection was created with the name:

```text
Week 3 Day 1 - Task Tracker API Design
```

The collection contains six saved requests:

```text
Get All Tasks
Get Task By ID
Create Task
Update Task
Delete Task
Get Tasks By User
```

The requests were saved to document the REST resource map.

They were not executed because this exercise focused on API design and did not require a running backend.

## Tools Used

- Postman
- Notion

## Key Takeaways

- RESTful APIs are designed around resources rather than actions.
- Resource names should use plural nouns.
- HTTP methods should communicate the intended operation.
- HTTP status codes should accurately communicate request outcomes.
- Nested routes should represent real ownership relationships.
- Stateless requests contain everything required for processing.
- API versioning should be planned before clients begin using the API.