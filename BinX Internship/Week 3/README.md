# Week 3 — REST APIs, SQL Server, Entity Framework Core & Postman

## Overview

Week 3 focused on designing and building a complete Task Tracker API workflow, starting with REST API design and database modeling, then moving to Entity Framework Core, asynchronous CRUD operations, and API testing with Postman.

The same Task Tracker domain was developed throughout the week to connect API design, database design, backend implementation, and testing.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | REST API Design Principles & Resource Modeling | [View Day 1](./Day%201) |
| Day 2 | SQL Server Schema Design & Normalization | [View Day 2](./Day%202) |
| Day 3 | Entity Framework Core Setup & Code-First Migrations | [View Day 3](./Day%203) |
| Day 4 | Implementing CRUD Operations with EF Core | [View Day 4](./Day%204) |
| Day 5 | Testing & Documenting the API with Postman; Week 3 Synthesis | [View Day 5](./Day%205) |

## Week 3 Highlights

### REST API Design

- Designed the Task Tracker API around `users`, `tasks`, and `comments` resources.
- Used plural resource names and appropriate HTTP methods.
- Defined success and error status codes.
- Designed nested resource routes.
- Selected URL-based API versioning.

### Database Design & Normalization

- Designed the Task Tracker SQL Server database schema.
- Applied First, Second, and Third Normal Form.
- Defined primary keys, foreign keys, and one-to-many relationships.
- Selected appropriate SQL Server column types.
- Created an ERD using `dbdiagram.io`.
- Implemented the schema in SQL Server.

### Entity Framework Core

- Created the `TaskTrackerApi` ASP.NET Core Web API project.
- Configured Entity Framework Core with SQL Server.
- Created `User`, `TaskItem`, and `Comment` entity models.
- Created and registered `AppDbContext`.
- Configured entity relationships using Fluent API.
- Created and applied the initial Code-First migration.

### Asynchronous CRUD Operations

- Implemented CRUD operations for Users, Tasks, and Comments.
- Used request models and Data Annotations.
- Separated business logic using service interfaces and service implementations.
- Registered services using Dependency Injection.
- Used asynchronous Entity Framework Core operations.
- Returned appropriate HTTP status codes.
- Implemented `GetAllAsync`, `GetByIdAsync`, `CreateAsync`, `UpdateAsync`, and `DeleteAsync` in the service layer.

### Postman Testing

- Organized and tested the Task Tracker API using Postman.
- Tested successful CRUD requests, invalid data, and missing resources.
- Added automated status-code assertions and response validation.
- Created a Postman environment using a reusable `baseUrl` variable.
- Used environment variables across Users, Tasks, and Comments requests.
- Exported the completed Postman collection as `task-tracker-api.postman_collection.json`.

## Tools Used

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
