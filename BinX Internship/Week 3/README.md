# Week 3 — Backend Internship

## Overview

Week 3 focuses on REST API design, resource modeling, SQL Server schema design, database normalization, Entity Framework Core Code-First development, and implementing asynchronous CRUD operations.

The first exercise focused on designing a RESTful API before implementing it, including resource naming, HTTP methods, status codes, nested resources, and API versioning.

The second exercise focused on designing and implementing a normalized SQL Server database schema for the same Task Tracker domain.

The third exercise focused on creating an ASP.NET Core Web API project, configuring Entity Framework Core with SQL Server, defining entity models and relationships, generating the first migration, and applying it to the database.

The fourth exercise focused on implementing complete asynchronous CRUD operations for Users, Tasks, and Comments using Entity Framework Core, request validation, a service layer, Dependency Injection, and Postman testing.

This README will continue to be updated as the remaining Week 3 exercises are completed.

## Completed Days

### Day 1 — REST API Design Principles & Resource Modeling

Designed a REST resource map for a Task Tracker API.

The exercise included:

- Identifying the core API resources.
- Naming resources using plural nouns.
- Designing CRUD endpoints for the `tasks` resource.
- Using the appropriate HTTP methods.
- Assigning success and error status codes.
- Creating a nested resource endpoint.
- Selecting URL-based API versioning.
- Organizing the resource map in Postman.
- Documenting the API design in Notion and GitHub.

Core resources:

```text
users
tasks
comments
```

Designed endpoints:

```http
GET    /api/v1/tasks
GET    /api/v1/tasks/{id}
POST   /api/v1/tasks
PUT    /api/v1/tasks/{id}
DELETE /api/v1/tasks/{id}
GET    /api/v1/users/{userId}/tasks
```

[View Day 1 Documentation](./Day%201/README.md)

---

### Day 2 — SQL Server Schema Design & Normalization

Designed and implemented a normalized database schema for the Task Tracker API.

The exercise included:

- Identifying the required database entities and attributes.
- Applying First, Second, and Third Normal Form.
- Defining primary keys and foreign keys.
- Modeling one-to-many relationships.
- Selecting appropriate SQL Server column types.
- Designing an ERD using `dbdiagram.io`.
- Creating the database and tables in SQL Server Management Studio.
- Adding primary-key, unique, not-null, and foreign-key constraints.
- Creating a database diagram in SQL Server Management Studio.
- Documenting the database design in Notion and GitHub.

Database entities:

```text
Users
Tasks
Comments
```

Relationships:

```text
Users.Id → Tasks.UserId
Tasks.Id → Comments.TaskId
Users.Id → Comments.UserId
```

Implemented database:

```text
TaskTrackerDb
├── dbo.Users
├── dbo.Tasks
└── dbo.Comments
```

The final schema satisfies:

```text
1NF
2NF
3NF
```

[View Day 2 Documentation](./Day%202/README.md)

---

### Day 3 — Entity Framework Core Setup & Code-First Migrations

Created a new ASP.NET Core Web API project for the Task Tracker domain and configured Entity Framework Core with SQL Server.

The exercise included:

- Creating the `TaskTrackerApi` project.
- Installing the EF Core SQL Server and Tools packages.
- Defining the `User`, `TaskItem`, and `Comment` entity models.
- Adding foreign-key and navigation properties.
- Creating the `AppDbContext`.
- Exposing a `DbSet<T>` for each entity.
- Configuring tables, columns, indexes, and relationships using Fluent API.
- Adding the SQL Server connection string.
- Registering `AppDbContext` using Dependency Injection.
- Generating the `InitialCreate` migration.
- Inspecting the generated migration file.
- Applying the migration using `Update-Database`.
- Verifying the generated tables using Visual Studio SQL Server Object Explorer.
- Documenting the implementation in Notion and GitHub.

Entity models:

```text
User
TaskItem
Comment
```

DbContext tables:

```text
DbSet<User>      → Users
DbSet<TaskItem>  → Tasks
DbSet<Comment>   → Comments
```

Migration commands:

```powershell
Add-Migration InitialCreate
Update-Database
```

Generated database:

```text
TaskTrackerEfCoreDb
├── dbo.Users
├── dbo.Tasks
├── dbo.Comments
└── dbo.__EFMigrationsHistory
```

[View Day 3 Documentation](./Day%203/README.md)

---

### Day 4 — Implementing CRUD Operations with EF Core

Implemented complete asynchronous CRUD operations for the Task Tracker API using Entity Framework Core.

CRUD operations were implemented for:

```text
Users
Tasks
Comments
```

The exercise included:

- Creating separate request models for create and update operations.
- Validating incoming requests using Data Annotations.
- Creating service interfaces and service implementations.
- Registering services using Dependency Injection.
- Keeping database logic outside the controllers.
- Implementing asynchronous create, read, update, and delete operations.
- Using `AsNoTracking` for read-only queries.
- Using EF Core change tracking for update operations.
- Calling `SaveChangesAsync` after database changes.
- Returning appropriate HTTP status codes.
- Returning `201 Created` with a `Location` header.
- Returning `400 Bad Request` for invalid request data.
- Returning `404 Not Found` for missing resources.
- Testing successful and invalid requests using Postman.
- Organizing Postman requests into Users, Tasks, and Comments folders.
- Documenting the implementation in Notion and GitHub.

Service interfaces:

```text
IUserService
ITaskService
ICommentService
```

Service implementations:

```text
UserService
TaskService
CommentService
```

Each service implements:

```text
GetAllAsync
GetByIdAsync
CreateAsync
UpdateAsync
DeleteAsync
```

Implemented endpoints:

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

Tested HTTP status codes:

```text
200 OK
201 Created
204 No Content
400 Bad Request
404 Not Found
```

Because the resources are connected through foreign-key relationships, records were deleted in this order:

```text
Comment → Task → User
```

[View Day 4 Documentation](./Day%204/README.md)

## Tools Used

- Postman
- Swagger
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- SQL Server Management Studio
- Visual Studio
- Visual Studio Package Manager Console
- Visual Studio SQL Server Object Explorer
- dbdiagram.io
- Notion
- Git
- GitHub

## Repository Structure

```text
Week 3/
├── README.md
├── Day 1/
│   └── README.md
├── Day 2/
│   ├── README.md
│   ├── task-tracker-erd.png
│   └── task-tracker-ssms-diagram.png
├── Day 3/
│   ├── README.md
│   ├── initial-create-migration.png
│   ├── task-tracker-efcore-tables.png
│   └── TaskTrackerApi/
└── Day 4/
    ├── README.md
    ├── task-tracker-crud-endpoints.png
    ├── task-tracker-postman-tests.png
    └── TaskTrackerApi/
```

## Progress

| Day | Topic | Status |
|---|---|---|
| Day 1 | REST API Design Principles & Resource Modeling | Completed |
| Day 2 | SQL Server Schema Design & Normalization | Completed |
| Day 3 | Entity Framework Core Setup & Code-First Migrations | Completed |
| Day 4 | Implementing CRUD Operations with EF Core | Completed |
| Day 5 | To be added | Not started |
