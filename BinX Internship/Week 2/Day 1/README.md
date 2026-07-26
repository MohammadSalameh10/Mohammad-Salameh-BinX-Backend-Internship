# Day 1 — Generics & Advanced Collections

Day 1 focused on understanding generics in C#, applying generic constraints, choosing suitable collection interfaces, and building a reusable generic repository.

## Project Overview

The purpose of this project was to create a reusable `Repository<T>` class that can store and manage different domain-model types while maintaining type safety.

The application:

- Creates a generic repository with `Add`, `GetAll`, and `Find` operations.
- Applies the `where T : class` generic constraint.
- Stores repository items internally using `List<T>`.
- Returns repository items using `IReadOnlyList<T>`.
- Uses the same repository implementation with `Product` and `Customer`.
- Searches for products and customers using predicates and lambda expressions.
- Demonstrates that the collection returned by `GetAll()` cannot be modified directly.

## Day 1 Objectives

- Explain why generics are used.
- Understand the purpose of a type parameter.
- Write and use a generic class.
- Apply a generic constraint.
- Build a reusable generic repository.
- Add and retrieve items using generic methods.
- Search for items using `Predicate<T>`.
- Compare common collection interfaces.
- Return a read-only collection from a public method.
- Use one generic repository with different domain models.
- Build and run the application successfully.
- Upload the completed project to GitHub.

## Concepts Applied

- Generics
- Type parameters
- Generic classes
- Generic constraints
- `where T : class`
- `List<T>`
- `IEnumerable<T>`
- `IReadOnlyList<T>`
- `IList<T>`
- `Predicate<T>`
- Lambda expressions
- Nullable reference types
- Type safety
- Code reuse

## Domain Models

The application contains two domain-model classes:

- `Product`: Represents a product with a name and price.
- `Customer`: Represents a customer with a name and email address.

### Product Model

The `Product` class contains `Name` and `Price` properties:

```csharp
internal class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
```

### Customer Model

The `Customer` class contains `Name` and `Email` properties:

```csharp
internal class Customer
{
    public string Name { get; set; }
    public string Email { get; set; }

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }
}
```

## Generic Repository

The generic repository stores and manages items of type `T`:

```csharp
internal class Repository<T> where T : class
{
    // The repository stores domain objects, so T must be a reference type.
    private readonly List<T> _items = new List<T>();

    public void Add(T item)
    {
        _items.Add(item);
    }

    public IReadOnlyList<T> GetAll()
    {
        return _items.AsReadOnly();
    }

    public T? Find(Predicate<T> predicate)
    {
        return _items.Find(predicate);
    }
}
```

The same repository implementation can be used with different types:

```csharp
Repository<Product> productRepository =
    new Repository<Product>();

Repository<Customer> customerRepository =
    new Repository<Customer>();
```

For `Repository<Product>`, the type parameter `T` represents `Product`.

For `Repository<Customer>`, the type parameter `T` represents `Customer`.

## Generic Constraint

The repository uses the following generic constraint:

```csharp
where T : class
```

This restricts `T` to reference types.

The repository is designed to store domain objects such as:

```csharp
Product
Customer
```

The constraint prevents the repository from being used with value types such as:

```csharp
int
double
bool
```

## Add Operation

The `Add` method receives an item of type `T` and adds it to the internal list:

```csharp
public void Add(T item)
{
    _items.Add(item);
}
```

When using `Repository<Product>`, the method accepts `Product` objects:

```csharp
productRepository.Add(new Product("Laptop", 900));
productRepository.Add(new Product("Phone", 600));
productRepository.Add(new Product("Keyboard", 80));
```

When using `Repository<Customer>`, the method accepts `Customer` objects:

```csharp
customerRepository.Add(
    new Customer("Mohammad", "mohammad@gmail.com")
);

customerRepository.Add(
    new Customer("Ahmad", "ahmad@gmail.com")
);
```

## GetAll Operation

The `GetAll` method returns all stored items:

```csharp
public IReadOnlyList<T> GetAll()
{
    return _items.AsReadOnly();
}
```

The repository stores its items internally using:

```csharp
List<T>
```

It returns them publicly using:

```csharp
IReadOnlyList<T>
```

The caller can:

- Iterate over the items using `foreach`.
- Access items using an index.
- Read the number of items using `Count`.

The caller cannot add or remove items directly from the returned collection.

## Find Operation

The `Find` method receives a `Predicate<T>` that defines the search condition:

```csharp
public T? Find(Predicate<T> predicate)
{
    return _items.Find(predicate);
}
```

It returns the first matching item.

If no matching item is found, it returns `null`.

A product is found by its name:

```csharp
Product? foundProduct = productRepository.Find(
    product => product.Name == "Phone"
);
```

A customer is found by the email address:

```csharp
Customer? foundCustomer = customerRepository.Find(
    customer => customer.Email == "ahmad@gmail.com"
);
```

## Product Repository

A repository was created for `Product` objects:

```csharp
Repository<Product> productRepository =
    new Repository<Product>();
```

Three products were added:

```csharp
productRepository.Add(new Product("Laptop", 900));
productRepository.Add(new Product("Phone", 600));
productRepository.Add(new Product("Keyboard", 80));
```

All products were retrieved using:

```csharp
IReadOnlyList<Product> products =
    productRepository.GetAll();
```

The products were displayed using a `foreach` loop:

```csharp
foreach (Product product in products)
{
    Console.WriteLine($"{product.Name} - {product.Price}");
}
```

## Customer Repository

The same generic repository was used with `Customer` objects:

```csharp
Repository<Customer> customerRepository =
    new Repository<Customer>();
```

Two customers were added:

```csharp
customerRepository.Add(
    new Customer("Mohammad", "mohammad@gmail.com")
);

customerRepository.Add(
    new Customer("Ahmad", "ahmad@gmail.com")
);
```

All customers were retrieved using:

```csharp
IReadOnlyList<Customer> customers =
    customerRepository.GetAll();
```

The customers were displayed using:

```csharp
foreach (Customer customer in customers)
{
    Console.WriteLine($"{customer.Name} - {customer.Email}");
}
```

## Read-Only Collection

The products returned by `GetAll()` are stored in an `IReadOnlyList<Product>`:

```csharp
IReadOnlyList<Product> products =
    productRepository.GetAll();
```

The following line causes a compile-time error because `IReadOnlyList<Product>` does not provide an `Add` method:

```csharp
// IReadOnlyList<Product> does not allow adding or removing items.
// Uncommenting this line causes a compile-time error.
// products.Add(new Product("Mouse", 40));
```

This prevents the caller from modifying the returned collection directly.

## Collection Interfaces

### IEnumerable&lt;T&gt;

`IEnumerable<T>` provides forward iteration and is suitable when the caller only needs to iterate over a sequence.

### IReadOnlyList&lt;T&gt;

`IReadOnlyList<T>` provides:

- Iteration using `foreach`.
- Indexed access.
- A known item count.
- No direct add or remove operations.

### IList&lt;T&gt;

`IList<T>` allows the caller to:

- Add items.
- Remove items.
- Replace items.
- Access items by index.

A public method should return the least permissive interface that satisfies the caller's needs.

## How to Run

1. Open the `GenericsAdvancedCollectionsDay1` solution in Visual Studio.
2. Build the solution using:

```text
Ctrl + Shift + B
```

3. Run the application without debugging using:

```text
Ctrl + F5
```

## Expected Output

```text
All Products:
Laptop - 900
Phone - 600
Keyboard - 80

Phone - 600

All Customers:
Mohammad - mohammad@gmail.com
Ahmad - ahmad@gmail.com

Ahmad - ahmad@gmail.com
```

## Technologies and Tools

- C#
- .NET
- Generics
- Collections
- Visual Studio
- Console Application
- Git
- GitHub

## Project Files

- `Program.cs`
- `Product.cs`
- `Customer.cs`
- `Repository.cs`
- `GenericsAdvancedCollectionsDay1.csproj`
- `GenericsAdvancedCollectionsDay1.slnx`

## Day 1 Folder

[View GenericsAdvancedCollectionsDay1 Project](./GenericsAdvancedCollectionsDay1)