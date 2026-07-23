# Day 4 — C# Fundamentals III: Collections & LINQ Basics

A product management console application created as part of Day 4 of the BinX Backend Internship program.

## Project Overview

This project demonstrates C# collections, LINQ operations, asynchronous programming, and exception handling through a product management console application.

The application:

- Creates a list containing eight product objects.
- Uses a dictionary to access products by ID.
- Uses a hash set to store unique processed order IDs.
- Filters available products with a price greater than `100`.
- Projects product objects into product names.
- Orders products by price.
- Calculates the product count, available product count, total price, and average price.
- Uses LINQ Method Syntax and Query Syntax.
- Simulates an asynchronous operation using `Task.Delay()`.
- Reads a product quantity and handles invalid input safely.

## Day 4 Objectives

- Choose the appropriate collection for a specific access pattern.
- Store ordered objects using `List<T>`.
- Access objects by key using `Dictionary<TKey, TValue>`.
- Store unique values using `HashSet<T>`.
- Filter data using `Where()`.
- Transform data using `Select()`.
- Order data using `OrderBy()`.
- Aggregate data using `Count()`, `Sum()`, and `Average()`.
- Use LINQ Method Syntax.
- Use LINQ Query Syntax.
- Create and await an asynchronous method.
- Simulate an I/O operation using `Task.Delay()`.
- Handle specific exceptions using `try`, `catch`, and `finally`.
- Build and run the application successfully.

## Concepts Applied

- `List<T>`
- `Dictionary<TKey, TValue>`
- `HashSet<T>`
- LINQ Method Syntax
- LINQ Query Syntax
- Lambda expressions
- `Where()`
- `Select()`
- `OrderBy()`
- `Count()`
- `Sum()`
- `Average()`
- `ToDictionary()`
- `TryGetValue()`
- `async`
- `await`
- `Task`
- `Task.Delay()`
- `try`
- `catch`
- `finally`
- `FormatException`
- `OverflowException`
- Nullable reference types
- Method overriding
- String interpolation

## Product Model

The `Product` class represents a product using the following properties:

- `Id`: The unique product identifier.
- `Name`: The product name.
- `Price`: The product price.
- `IsAvailable`: Indicates whether the product is currently available.

The class overrides `ToString()` to display the product information clearly:

```csharp
public override string ToString()
{
    return $"ID: {Id}, Name: {Name}, Price: {Price}, Available: {IsAvailable}";
}
```

## List

A `List<Product>` stores eight product objects:

```csharp
List<Product> products = new List<Product>
{
    new Product(1, "Laptop", 1200, true),
    new Product(2, "Mouse", 25, true),
    new Product(3, "Keyboard", 70, false),
    new Product(4, "Monitor", 350, true),
    new Product(5, "Headphones", 90, false),
    new Product(6, "Webcam", 60, true),
    new Product(7, "Printer", 220, false),
    new Product(8, "Tablet", 500, true)
};
```

The products are displayed using a `foreach` loop:

```csharp
foreach (Product product in products)
{
    Console.WriteLine(product);
}
```

## Dictionary

The product list is converted into a `Dictionary<int, Product>`:

```csharp
Dictionary<int, Product> productsById =
    products.ToDictionary(product => product.Id);
```

The product ID is used as the key, while the complete `Product` object is stored as the value.

`TryGetValue()` searches for the product whose ID is `4`:

```csharp
if (productsById.TryGetValue(4, out Product? foundProduct))
{
    Console.WriteLine($"Product found: {foundProduct}");
}
else
{
    Console.WriteLine("Product not found.");
}
```

## HashSet

A `HashSet<int>` stores unique processed order IDs:

```csharp
HashSet<int> processedOrderIds = new HashSet<int>
{
    101,
    102,
    103
};
```

The application attempts to add order ID `104` twice:

```csharp
bool firstAdd = processedOrderIds.Add(104);
bool secondAdd = processedOrderIds.Add(104);
```

The first operation returns `True`.

The second operation returns `False` because a `HashSet` does not store duplicate values.

## LINQ Filtering

`Where()` filters products that are available and have a price greater than `100`:

```csharp
List<Product> availableProducts = products
    .Where(product =>
        product.IsAvailable &&
        product.Price > 100
    )
    .ToList();
```

The filtered products are:

- Laptop
- Monitor
- Tablet

## LINQ Projection

`Select()` transforms the product collection into a list containing only product names:

```csharp
List<string> productNames = products
    .Select(product => product.Name)
    .ToList();
```

The original collection is a `List<Product>`, while the projected result is a `List<string>`.

## LINQ Ordering

`OrderBy()` orders products by price from lowest to highest:

```csharp
List<Product> productsOrderedByPrice = products
    .OrderBy(product => product.Price)
    .ToList();
```

The ordered prices are:

```text
25
60
70
90
220
350
500
1200
```

## LINQ Aggregation

The total number of products is calculated using `Count()`:

```csharp
int productCount = products.Count();
```

The number of available products is calculated using a condition:

```csharp
int availableProductCount = products
    .Count(product => product.IsAvailable);
```

The total product price is calculated using `Sum()`:

```csharp
double totalPrice = products
    .Sum(product => product.Price);
```

The average product price is calculated using `Average()`:

```csharp
double averagePrice = products
    .Average(product => product.Price);
```

The results are:

```text
Total Products: 8
Available Products Count: 5
Total Product Prices: 2515.00
Average Product Price: 314.38
```

## LINQ Query Syntax

LINQ Query Syntax selects available products, orders them alphabetically, and returns their names:

```csharp
List<string> availableProductNamesQuery =
    (
        from product in products
        where product.IsAvailable
        orderby product.Name
        select product.Name
    )
    .ToList();
```

The result is:

```text
Laptop
Monitor
Mouse
Tablet
Webcam
```

## Async and Await

The `LoadProductsAsync()` method simulates an asynchronous operation:

```csharp
static async Task<string> LoadProductsAsync()
{
    Console.WriteLine("Loading products...");

    await Task.Delay(2000);

    return "Products loaded successfully.";
}
```

`Task.Delay(2000)` simulates an operation that takes two seconds.

The method is awaited from `Main()`:

```csharp
string loadResult = await LoadProductsAsync();

Console.WriteLine(loadResult);
```

Because `await` is used inside `Main()`, the method is defined as:

```csharp
static async Task Main(string[] args)
```

## Exception Handling

The application asks the user to enter a product quantity:

```csharp
Console.Write("Enter product quantity: ");

string? quantityInput = Console.ReadLine();
```

The input is converted to an integer inside a `try` block:

```csharp
try
{
    int quantity = Convert.ToInt32(quantityInput);

    Console.WriteLine($"Quantity: {quantity}");
}
```

A `FormatException` is handled when the input is not a whole number:

```csharp
catch (FormatException)
{
    Console.WriteLine(
        "Invalid input. Please enter a whole number."
    );
}
```

An `OverflowException` is handled when the number exceeds the limits of `int`:

```csharp
catch (OverflowException)
{
    Console.WriteLine(
        "The number is too large or too small."
    );
}
```

The `finally` block runs after the operation finishes:

```csharp
finally
{
    Console.WriteLine(
        "Quantity input processing finished."
    );
}
```

## How to Run

1. Open the `CollectionsLinqDay4` project in Visual Studio.
2. Build the solution using:

```text
Ctrl + Shift + B
```

3. Run the application without debugging using:

```text
Ctrl + F5
```

## Expected Output

The following output is produced when the entered quantity is `10`:

```text
List:
All Products:
ID: 1, Name: Laptop, Price: 1200, Available: True
ID: 2, Name: Mouse, Price: 25, Available: True
ID: 3, Name: Keyboard, Price: 70, Available: False
ID: 4, Name: Monitor, Price: 350, Available: True
ID: 5, Name: Headphones, Price: 90, Available: False
ID: 6, Name: Webcam, Price: 60, Available: True
ID: 7, Name: Printer, Price: 220, Available: False
ID: 8, Name: Tablet, Price: 500, Available: True

==============================================================
Dictionary:

Product found: ID: 4, Name: Monitor, Price: 350, Available: True

==============================================================
HashSet:

First add of order 104: True
Second add of order 104: False

Processed Order IDs:
101
102
103
104

==============================================================
LINQ Filtering:

Available Products Over 100:
ID: 1, Name: Laptop, Price: 1200, Available: True
ID: 4, Name: Monitor, Price: 350, Available: True
ID: 8, Name: Tablet, Price: 500, Available: True

==============================================================
LINQ Projection:

Product Names:
Laptop
Mouse
Keyboard
Monitor
Headphones
Webcam
Printer
Tablet

==============================================================
LINQ Ordering:

Products Ordered by Price:
ID: 2, Name: Mouse, Price: 25, Available: True
ID: 6, Name: Webcam, Price: 60, Available: True
ID: 3, Name: Keyboard, Price: 70, Available: False
ID: 5, Name: Headphones, Price: 90, Available: False
ID: 7, Name: Printer, Price: 220, Available: False
ID: 4, Name: Monitor, Price: 350, Available: True
ID: 8, Name: Tablet, Price: 500, Available: True
ID: 1, Name: Laptop, Price: 1200, Available: True

==============================================================
LINQ Aggregation:

Total Products: 8
Available Products Count: 5
Total Product Prices: 2515.00
Average Product Price: 314.38

==============================================================
LINQ Query Syntax:

Available Product Names:
Laptop
Monitor
Mouse
Tablet
Webcam

==============================================================
Async and Await:

Loading products...
Products loaded successfully.

==============================================================
Exception Handling:

Enter product quantity: 10
Quantity: 10
Quantity input processing finished.
```

## Technologies and Tools

- C#
- .NET
- LINQ
- Visual Studio
- Console Application
- Git
- GitHub

## Project Files

- `Program.cs`
- `Product.cs`
- `CollectionsLinqDay4.csproj`

## Day 4 Folder

[View CollectionsLinqDay4 Project](./CollectionsLinqDay4)
