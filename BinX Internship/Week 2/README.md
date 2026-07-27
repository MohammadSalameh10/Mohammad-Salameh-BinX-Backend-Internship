# Week 2 — Advanced C# and LINQ

Week 2 focuses on advanced C# concepts, including generics, collection interfaces, advanced LINQ operations, and deferred execution.

## Week Progress

| Day | Topic | Documentation |
|---|---|---|
| Day 1 | Generics & Advanced Collections | [View Day 1](./Day%201) |
| Day 2 | Advanced LINQ & Deferred Execution | [View Day 2](./Day%202) |

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

## Technologies and Tools

- C#
- .NET
- LINQ
- Visual Studio
- Git
- GitHub
