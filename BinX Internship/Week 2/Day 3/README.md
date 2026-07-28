# Day 3: Async/Await Deep Dive & Concurrency Basics

## Overview

This exercise demonstrates asynchronous programming in C# using `Task`, `async`, and `await`.

The project compares sequential and concurrent execution, measures the elapsed time for both approaches, and demonstrates how to cancel an asynchronous operation using `CancellationToken`.

## Learning Objectives

- Understand the Task-based Asynchronous Pattern.
- Use `Task` and `Task<T>` with `async` and `await`.
- Avoid blocking asynchronous operations with `.Result` and `.Wait()`.
- Compare sequential and concurrent execution.
- Run independent asynchronous operations using `Task.WhenAll`.
- Cancel an asynchronous operation using `CancellationToken`.

## Hands-On Lab

The application implements three asynchronous methods that simulate different data sources:

- `GetCustomerDataAsync` simulates loading customer data.
- `GetOrderDataAsync` simulates loading order data.
- `GetNotificationDataAsync` simulates loading notification data.

Each method uses `Task.Delay` to simulate an operation that requires time to complete.

## Sequential Execution

The three operations are called one after another using individual `await` statements.

```csharp
string customerData = await GetCustomerDataAsync();
string orderData = await GetOrderDataAsync();
string notificationData = await GetNotificationDataAsync();
```

Each operation starts only after the previous operation has completed.

The approximate total execution time is:

```text
Customer:      2000 ms
Order:         3000 ms
Notification:  1000 ms
Total:         6000 ms
```

## Concurrent Execution

The three independent operations are started first and then awaited together using `Task.WhenAll`.

```csharp
Task<string> customerTask = GetCustomerDataAsync();
Task<string> orderTask = GetOrderDataAsync();
Task<string> notificationTask = GetNotificationDataAsync();

string[] results = await Task.WhenAll(
    customerTask,
    orderTask,
    notificationTask
);
```

Because the operations run during the same time period, the total execution time is approximately equal to the longest operation.

```text
Approximate concurrent time: 3000 ms
```

## Time Comparison

The application uses `Stopwatch` to measure the elapsed time for both execution approaches.

```text
Sequential execution: approximately 6000 ms
Concurrent execution: approximately 3000 ms
Time saved: approximately 3000 ms
```

This demonstrates that `Task.WhenAll` can reduce the total execution time when the operations are independent.

## Cancellation Demo

The `GetOrderDataAsync` method accepts a `CancellationToken`.

```csharp
static async Task<string> GetOrderDataAsync(
    CancellationToken cancellationToken = default
)
```

The token is passed to `Task.Delay`:

```csharp
await Task.Delay(3000, cancellationToken);
```

The application starts the operation and requests cancellation before it finishes:

```csharp
Task<string> orderTask = GetOrderDataAsync(
    cancellationTokenSource.Token
);

await Task.Delay(1500);

cancellationTokenSource.Cancel();
```

The cancellation is handled using:

```csharp
catch (OperationCanceledException)
{
    Console.WriteLine("Order loading was canceled.");
}
```

## Application Output

```text
Sequential execution started.

Loading customer data...
Customer data loaded.
Loading order data...
Order data loaded.
Loading notification data...
Notification data loaded.

Sequential Results:
Customer: Mohammad
Order: 101
Notification: Order confirmed

Sequential elapsed time: approximately 6000 ms

========================================

Concurrent execution started.

Loading customer data...
Loading order data...
Loading notification data...
Notification data loaded.
Customer data loaded.
Order data loaded.

Concurrent Results:
Customer: Mohammad
Order: 101
Notification: Order confirmed

Concurrent elapsed time: approximately 3000 ms

========================================

Time Comparison:
Sequential Time: approximately 6000 ms
Concurrent Time: approximately 3000 ms
Time Saved: approximately 3000 ms

========================================

Cancellation demo started.

Loading order data...
Cancellation requested.
Order loading was canceled.
```

## What I Learned

- A `Task` represents an asynchronous operation.
- `Task<T>` represents an asynchronous operation that returns a value.
- `await` waits for a task without using `.Result` or `.Wait()`.
- Individual `await` statements execute operations sequentially.
- `Task.WhenAll` allows independent operations to run concurrently.
- Concurrent execution time is approximately equal to the longest operation.
- `CancellationTokenSource` sends a cancellation request.
- `CancellationToken` allows an asynchronous method to respond to cancellation.
- `OperationCanceledException` can be handled when an operation is canceled.
