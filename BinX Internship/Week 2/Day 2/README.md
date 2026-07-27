# Day 2 — Advanced LINQ & Deferred Execution

This project demonstrates advanced LINQ operations using related customer, order, and order-item data.

## Learning Objectives

- Understand deferred and immediate LINQ execution.
- Group related data using `GroupBy`.
- Combine collections using `Join`.
- Flatten nested collections using `SelectMany`.
- Avoid common LINQ performance issues.

## Project Overview

The application contains three related models:

- `Customer`
- `Order`
- `OrderItem`

The relationship between customers and orders is based on:

```text
Customer.Id == Order.CustomerId
```

Each order also contains a nested collection of order items.

## Implemented Features

### Grouping Orders

`GroupBy` was used to group orders by `CustomerId`.

For each customer, the application calculates:

- Number of orders
- Total order amount

```csharp
var orderTotalsByCustomer = orders
    .GroupBy(order => order.CustomerId)
    .Select(group => new
    {
        CustomerId = group.Key,
        OrderCount = group.Count(),
        TotalAmount = group.Sum(order => order.Amount)
    });
```

### Joining Customers and Orders

`Join` was used to combine customer information with related orders.

```csharp
var customerOrders = customers.Join(
    orders,
    customer => customer.Id,
    order => order.CustomerId,
    (customer, order) => new
    {
        CustomerName = customer.Name,
        OrderId = order.Id,
        OrderAmount = order.Amount
    }
);
```

The result displays the customer name, order ID, and order amount.

LINQ `Join` behaves like an inner join, so customers without matching orders are not included.

### Flattening Order Items

Each order contains a collection of `OrderItem` objects.

`SelectMany` was used to flatten all nested item collections into one sequence.

```csharp
var allOrderItems = orders.SelectMany(
    order => order.Items,
    (order, item) => new
    {
        OrderId = order.Id,
        CustomerId = order.CustomerId,
        ProductName = item.ProductName,
        Quantity = item.Quantity,
        UnitPrice = item.UnitPrice
    }
);
```

### Deferred Execution

A LINQ query was defined before modifying the source collection:

```csharp
IEnumerable<Order> highValueOrders = orders
    .Where(order => order.Amount >= 500m);
```

A new high-value order was then added before the query was enumerated.

The new order appeared in the result because `Where` uses deferred execution. The query was executed only when the program reached the `foreach` loop.

## LINQ Performance Notes

The project also demonstrates two important LINQ performance considerations:

- Avoid calling `ToList()` too early when additional filtering is still required.
- Avoid enumerating the same deferred query multiple times when its result is used repeatedly.

When a result must be reused, it can be materialized once:

```csharp
List<Order> highValueOrders = orders
    .Where(order => order.Amount >= 500m)
    .ToList();
```

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

Open the project in Visual Studio and run it using:

```text
Ctrl + F5
```

## Technologies and Tools

- C#
- .NET
- LINQ
- Visual Studio
- Git
- GitHub
