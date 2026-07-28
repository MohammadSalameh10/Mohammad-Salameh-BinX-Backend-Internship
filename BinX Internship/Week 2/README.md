# Week 2 — Advanced C# and LINQ

Week 2 focuses on advanced C# concepts, including generics, collection interfaces, advanced LINQ operations, deferred execution, asynchronous programming, concurrency, and cancellation.

## Week Progress

| Day | Topic | Documentation |
|---|---|---|
| Day 1 | Generics & Advanced Collections | [View Day 1](./Day%201) |
| Day 2 | Advanced LINQ & Deferred Execution | [View Day 2](./Day%202) |
| Day 3 | Async/Await Deep Dive & Concurrency Basics | [View Day 3](./Day%203) |

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

## Technologies and Tools

- C#
- .NET
- LINQ
- Async/Await
- `Task.WhenAll`
- `CancellationToken`
- Visual Studio
- Git
- GitHub
