# Week 2 — Advanced C#, LINQ, and ASP.NET Core

## Overview

Week 2 focused on advanced C# concepts, LINQ, asynchronous programming, ASP.NET Core Web APIs, middleware, and dependency injection.

The week progressed from reusable generic code and advanced data querying to asynchronous execution, API routing, middleware, and dependency injection.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Generics & Advanced Collections | [View Day 1](./Day%201) |
| Day 2 | Advanced LINQ & Deferred Execution | [View Day 2](./Day%202) |
| Day 3 | Async/Await Deep Dive & Concurrency Basics | [View Day 3](./Day%203) |
| Day 4 | ASP.NET Core Project Setup & Routing | [View Day 4](./Day%204) |
| Day 5 | Middleware Pipeline & Dependency Injection; Week 2 Synthesis | [View Day 5](./Day%205) |

## Week 2 Highlights

### Generics & Advanced Collections

- Practiced generic methods and generic classes.
- Used generic constraints to control which types can be used.
- Applied generics to improve type safety and code reusability.
- Worked with `IEnumerable<T>`, `IReadOnlyList<T>`, and `IList<T>`.
- Implemented a reusable generic repository with `Add`, `GetAll`, and `Find`.
- Used `IReadOnlyList<T>` to expose collection data without allowing direct modification through the returned collection.

### Advanced LINQ

- Compared deferred and immediate LINQ execution.
- Grouped data using `GroupBy`.
- Combined related collections using `Join`.
- Flattened nested collections using `SelectMany`.
- Materialized queries using `ToList`.
- Practiced avoiding unnecessary repeated query enumeration.
- Reviewed common LINQ performance pitfalls.

### Async/Await & Concurrency

- Worked with `Task` and `Task<T>`.
- Used `async` and `await` for asynchronous operations.
- Avoided blocking with `.Result` and `.Wait()`.
- Compared sequential and concurrent execution.
- Ran independent operations concurrently using `Task.WhenAll`.
- Measured execution time using `Stopwatch`.
- Implemented cancellation using `CancellationToken`.
- Handled cancellation using `OperationCanceledException`.

### ASP.NET Core Routing

- Created an ASP.NET Core Web API project.
- Reviewed the minimal hosting model in `Program.cs`.
- Configured services and the HTTP request pipeline.
- Implemented endpoints using Controllers and Minimal APIs.
- Used routes, route parameters, HTTP verbs, and REST conventions.
- Returned `200 OK` and `404 Not Found` responses.
- Configured Swagger and OpenAPI.
- Tested and saved API requests using Postman.

### Middleware & Dependency Injection

- Reviewed the ASP.NET Core middleware pipeline and execution order.
- Created custom middleware to log request methods and paths.
- Used `UseRouting` to understand endpoint selection.
- Practiced dependency injection and service registration.
- Compared Transient, Scoped, and Singleton service lifetimes.
- Created `IProductService` and `ProductService`.
- Registered the product service using a Scoped lifetime.
- Injected `IProductService` into `ProductsController`.
- Moved product data and search logic out of the controller and into the service layer.

## Tools Used

- C#
- .NET
- LINQ
- Async/Await
- `Task.WhenAll`
- `CancellationToken`
- ASP.NET Core Web API
- Controllers
- Minimal APIs
- Middleware
- Dependency Injection
- `IProductService`
- Service lifetimes
- Constructor injection
- Swagger
- OpenAPI
- Postman
- Visual Studio
- Git
- GitHub

## Summary

During Week 2, I expanded my C# knowledge by working with generics, advanced collections, LINQ, asynchronous programming, and cancellation.

I then moved into ASP.NET Core Web API development, where I practiced routing with Controllers and Minimal APIs, configured Swagger and Postman testing, and learned how requests move through the middleware pipeline.

I also applied dependency injection by creating a service interface and implementation, registering the service with a Scoped lifetime, and injecting it into a controller.
