# Day 2 — Advanced LINQ & Deferred Execution

## Learning Objectives

- Explain the difference between deferred and immediate LINQ execution.
- Use `GroupBy` to summarize related data.
- Use `Join` to combine two related collections.
- Use `SelectMany` to flatten nested collections.
- Identify common LINQ performance pitfalls.

## Key Topics

- Deferred vs. immediate execution
- Grouping data with `GroupBy`
- Joining related collections with `Join`
- Flattening nested collections with `SelectMany`
- LINQ materialization and repeated enumeration

## Hands-On Lab

The application uses three related models:

- `Customer`
- `Order`
- `OrderItem`

Two related collections were created:

- Six customers
- Six initial orders
- Multiple order items inside each order

The relationship between customers and orders is based on:

```text
Customer.Id == Order.CustomerId
```

### GroupBy

Orders were grouped by `CustomerId` to calculate:

- The number of orders for each customer
- The total order amount for each customer

### Join

The customers and orders collections were joined to display:

- Customer name
- Order ID
- Order amount

Only customers with matching orders appear because LINQ `Join` behaves like an inner join.

### SelectMany

`SelectMany` was used to flatten the nested `OrderItem` collections from all orders into one sequence.

Each result contains:

- Order ID
- Customer ID
- Product name
- Quantity
- Unit price

### Deferred Execution

A query for orders with an amount of at least `500` was defined before adding a new order.

The new order was added after defining the query but before enumerating it:

```text
Order ID: 107
Customer ID: 6
Amount: 900
```

The new order appeared in the result because the query was executed later during the `foreach` loop.

## Project Structure

```text
Day 2/
├── README.md
└── AdvancedLinqDay2/
    └── AdvancedLinqDay2/
        ├── Models/
        │   ├── Customer.cs
        │   ├── Order.cs
        │   └── OrderItem.cs
        ├── Program.cs
        └── AdvancedLinqDay2.csproj
```

## Run the Project

From the project directory, run:

```powershell
dotnet run
```

## Tools Used

- .NET SDK
- C#
- LINQ
- Visual Studio
- Git
- GitHub
