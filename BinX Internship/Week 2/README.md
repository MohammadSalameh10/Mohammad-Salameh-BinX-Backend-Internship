# Week 2 — Advanced C#, LINQ, and ASP.NET Core

Week 2 focuses on advanced C# concepts, LINQ, asynchronous programming, and the fundamentals of building and testing ASP.NET Core Web APIs.

## Week Progress

| Day | Topic | Documentation |
|---|---|---|
| Day 1 | Generics & Advanced Collections | [View Day 1](./Day%201) |
| Day 2 | Advanced LINQ & Deferred Execution | [View Day 2](./Day%202) |
| Day 3 | Async/Await Deep Dive & Concurrency Basics | [View Day 3](./Day%203) |
| Day 4 | ASP.NET Core Project Setup & Routing | [View Day 4](./Day%204) |

## Topics Covered

### Day 1 — Generics & Advanced Collections

- Generic methods and classes
- Generic constraints
- Type safety and code reusability
- `IEnumerable<T>`
- `IReadOnlyList<T>`
- `IList<T>`
- Generic repository implementation

### Day 2 — Advanced LINQ & Deferred Execution

- Deferred and immediate LINQ execution
- Grouping data with `GroupBy`
- Combining related collections with `Join`
- Flattening nested collections with `SelectMany`
- LINQ materialization using `ToList`
- Avoiding repeated query enumeration
- Common LINQ performance pitfalls

### Day 3 — Async/Await Deep Dive & Concurrency Basics

- Task-based asynchronous programming
- `Task` and `Task<T>`
- Asynchronous methods using `async` and `await`
- Avoiding blocking with `.Result` and `.Wait()`
- Sequential and concurrent execution
- Running independent operations with `Task.WhenAll`
- Measuring execution time using `Stopwatch`
- Cancelling asynchronous operations with `CancellationToken`
- Handling `OperationCanceledException`

### Day 4 — ASP.NET Core Project Setup & Routing

- ASP.NET Core Web API project structure
- The minimal hosting model in `Program.cs`
- Service registration and the HTTP request pipeline
- Controllers and Minimal APIs
- Routes and route parameters
- HTTP verbs and REST conventions
- Swagger and OpenAPI configuration
- Testing endpoints using Postman
- Saving API requests inside a Postman collection

## Projects

### Generic Repository

A reusable generic repository was implemented with:

- `Add`
- `GetAll`
- `Find`

The repository uses a generic constraint and returns an `IReadOnlyList<T>` to prevent direct modification through the returned collection.

[View the Day 1 project](./Day%201)

### Advanced LINQ Application

A console application was created using related customer, order, and order-item data.

The application demonstrates:

- Grouping orders by customer
- Calculating total order amounts
- Joining customers with their orders
- Flattening nested order items
- Demonstrating deferred execution

[View the Day 2 project](./Day%202)

### Concurrent Async Operations

A console application was created to compare sequential and concurrent asynchronous execution.

The application demonstrates:

- Simulating multiple data sources using `Task.Delay`
- Executing operations sequentially with individual `await` statements
- Running independent operations concurrently with `Task.WhenAll`
- Comparing elapsed execution times
- Cancelling an operation during execution with `CancellationToken`
- Handling cancellation using `OperationCanceledException`

[View the Day 3 project](./Day%203)

### ASP.NET Core Routing API

An ASP.NET Core Web API application was created to demonstrate routing using Controllers and Minimal APIs.

The application demonstrates:

- Creating an ASP.NET Core Web API project
- Configuring services and middleware in `Program.cs`
- Creating a `Product` domain model
- Returning a hardcoded list of products
- Retrieving a single product using a route parameter
- Building endpoints with a Controller
- Building the same endpoints using Minimal APIs
- Returning `200 OK` and `404 Not Found` responses
- Documenting endpoints using Swagger
- Testing and saving requests using Postman

The application contains the following endpoints:

| Approach | HTTP Verb | Route |
|---|---|---|
| Controller | GET | `/api/Products` |
| Controller | GET | `/api/Products/{id}` |
| Minimal API | GET | `/minimal/products` |
| Minimal API | GET | `/minimal/products/{id}` |

[View the Day 4 project](./Day%204)

## Technologies and Tools

- C#
- .NET
- LINQ
- Async/Await
- `Task.WhenAll`
- `CancellationToken`
- ASP.NET Core Web API
- Controllers
- Minimal APIs
- Swagger
- OpenAPI
- Postman
- Visual Studio
- Git
- GitHub
