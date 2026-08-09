# Week 3 — REST APIs, SQL Server, Entity Framework Core & Postman

## Overview

Week 3 focused on designing and building a complete Task Tracker API workflow, starting with REST API design and database modeling, then moving to Entity Framework Core, asynchronous CRUD operations, and API testing with Postman.

The same Task Tracker domain was developed throughout the week to connect API design, database design, backend implementation, and testing.

## Daily Work

| Day   | Topic                                                        | Project / Documentation |
| ----- | ------------------------------------------------------------ | ----------------------- |
| Day 1 | REST API Design Principles & Resource Modeling               | [View Day 1](./Day%201) |
| Day 2 | SQL Server Schema Design & Normalization                     | [View Day 2](./Day%202) |
| Day 3 | Entity Framework Core Setup & Code-First Migrations          | [View Day 3](./Day%203) |
| Day 4 | Implementing CRUD Operations with EF Core                    | [View Day 4](./Day%204) |
| Day 5 | Testing & Documenting the API with Postman; Week 3 Synthesis | [View Day 5](./Day%205) |

## Topics Covered

### REST API Design

- Designed the Task Tracker API around resources.
- Used plural resource names.
- Selected appropriate HTTP methods.
- Defined success and error status codes.
- Designed nested resource routes.
- Selected URL-based API versioning.

The core resources were:

```text
users
tasks
comments
```

### Database Design & Normalization

- Designed the Task Tracker database schema.
- Applied First, Second, and Third Normal Form.
- Defined primary keys and foreign keys.
- Modeled one-to-many relationships.
- Selected appropriate SQL Server column types.
- Created an ERD using `dbdiagram.io`.
- Implemented the schema in SQL Server.

The main relationships were:

```text
Users.Id → Tasks.UserId
Tasks.Id → Comments.TaskId
Users.Id → Comments.UserId
```

### Entity Framework Core

- Created the `TaskTrackerApi` ASP.NET Core Web API project.
- Installed and configured Entity Framework Core with SQL Server.
- Created `User`, `TaskItem`, and `Comment` entity models.
- Created `AppDbContext`.
- Configured entities and relationships using Fluent API.
- Registered `AppDbContext` using Dependency Injection.
- Created and applied the initial Code-First migration.

Migration commands:

```powershell
Add-Migration InitialCreate
Update-Database
```

### CRUD Operations

Asynchronous CRUD operations were implemented for:

```text
Users
Tasks
Comments
```

The application uses:

- Request models
- Data Annotations
- Service interfaces
- Service implementations
- Dependency Injection
- Async EF Core operations
- Appropriate HTTP status codes

Each service provides:

```text
GetAllAsync
GetByIdAsync
CreateAsync
UpdateAsync
DeleteAsync
```

### Postman Testing

The Task Tracker API was organized and tested using Postman.

The tests covered:

- Successful CRUD requests
- Invalid request data
- Missing resources
- Automated status-code assertions
- Response validation
- Environment variables

A Postman environment was created using:

```text
baseUrl = https://localhost:7277
```

Requests then used:

```http
{{baseUrl}}/api/users
{{baseUrl}}/api/tasks
{{baseUrl}}/api/comments
```

The completed Postman collection was exported as:

```text
task-tracker-api.postman_collection.json
```

## Project

### Task Tracker API

The main project developed during Week 3 was an ASP.NET Core Web API for managing users, tasks, and comments.

The project includes:

- RESTful API endpoints
- SQL Server database
- Entity Framework Core
- Code-First migrations
- Entity relationships
- Request validation
- Service layer
- Dependency Injection
- Asynchronous CRUD operations
- Postman API tests

The main endpoints include:

```http
POST   /api/users
GET    /api/users
GET    /api/users/{id}
PUT    /api/users/{id}
DELETE /api/users/{id}

POST   /api/tasks
GET    /api/tasks
GET    /api/tasks/{id}
PUT    /api/tasks/{id}
DELETE /api/tasks/{id}

POST   /api/comments
GET    /api/comments
GET    /api/comments/{id}
PUT    /api/comments/{id}
DELETE /api/comments/{id}
```

The API was tested with the following HTTP status codes:

```text
200 OK
201 Created
204 No Content
400 Bad Request
404 Not Found
```

[View the Day 4 implementation](./Day%204)

[View the Day 5 Postman testing](./Day%205)

## Technologies and Tools

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- SQL Server Management Studio
- Fluent API
- Dependency Injection
- Swagger
- OpenAPI
- Postman
- Visual Studio
- Visual Studio Package Manager Console
- Visual Studio SQL Server Object Explorer
- dbdiagram.io
- Git
- GitHub

## Summary

During Week 3, I designed and implemented the main data and CRUD layers of the Task Tracker API.

I started by designing RESTful resources and a normalized SQL Server database schema. I then implemented the domain using ASP.NET Core Web API and Entity Framework Core, configured entity relationships, generated Code-First migrations, and built asynchronous CRUD operations using a service layer.

Finally, I tested successful and error paths using Postman, added automated test scripts and environment variables, and exported the completed Postman collection.
