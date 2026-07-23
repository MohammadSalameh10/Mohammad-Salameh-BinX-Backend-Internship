# Day 3 — C# Fundamentals II: Object-Oriented Programming

An order management console application created as part of Day 3 of the BinX Backend Internship program.

## Project Overview

This project demonstrates object-oriented programming concepts in C# by modeling a small order system.

The application:

- Stores products and their available quantities.
- Creates a customer and an immutable order request.
- Verifies that the requested product exists.
- Verifies that the requested quantity is valid.
- Verifies that the requested quantity is available in stock.
- Creates an order only when all validation checks pass.
- Reduces the product stock after creating the order.
- Sends different notifications through a shared interface.

## Day 3 Objectives

- Model a small domain using related classes.
- Use private fields and public properties.
- Apply encapsulation to protect object data.
- Initialize objects using constructors.
- Use a record for immutable request data.
- Define and implement an interface.
- Demonstrate polymorphism through an interface parameter.
- Override the `ToString()` method.
- Verify product and stock availability before creating an order.
- Build and run the application successfully.

## Concepts Applied

- Classes
- Class fields
- Class properties
- Class constructors
- Class functions
- Class scope
- Encapsulation
- Access modifiers
- Records
- Interfaces
- Polymorphism
- Method overriding
- Dictionaries
- Conditional statements
- String interpolation

## Domain Model

The project contains the following domain types:

- `Customer`: Represents the customer who creates an order.
- `Product`: Represents a product and its available stock.
- `Order`: Represents a successful order associated with a customer and product.
- `CreateOrderRequest`: Represents immutable order-request data.
- `INotifiable`: Defines notification behavior implemented by different classes.

## Product Inventory

The application stores products in a dictionary:

```csharp
Dictionary<string, Product> products =
    new Dictionary<string, Product>();

products.Add("Laptop", new Product("Laptop", 5));
products.Add("Phone", new Product("Phone", 10));
products.Add("Keyboard", new Product("Keyboard", 3));
```

The product name is used as the dictionary key, while the `Product` object stores the product information and available quantity.

## Immutable Order Request

The order request is represented using a record:

```csharp
internal record CreateOrderRequest(string ProductName, int Quantity);
```

The application creates the following request:

```csharp
CreateOrderRequest request =
    new CreateOrderRequest("Laptop", 2);
```

## Product Validation

Before creating an order, the application checks whether the requested product exists:

```csharp
if (!products.ContainsKey(request.ProductName))
{
    Console.WriteLine("The requested product does not exist.");
}
```

The application also checks that the requested quantity is greater than zero:

```csharp
if (request.Quantity < 1)
{
    Console.WriteLine("The requested quantity must be at least 1.");
}
```

The `Product` class checks whether enough stock is available:

```csharp
public bool IsAvailable(int requestedQuantity)
{
    return requestedQuantity > 0 &&
           requestedQuantity <= stockQuantity;
}
```

## Stock Management

After successfully creating the order, the application reduces the available stock:

```csharp
public void ReduceStock(int requestedQuantity)
{
    if (IsAvailable(requestedQuantity))
    {
        stockQuantity -= requestedQuantity;
    }
}
```

The initial stock for `Laptop` is `5`, and the requested quantity is `2`.

The remaining stock is:

```text
3
```

## Encapsulation

The classes use private fields to protect their internal data:

```csharp
private string name;
private int stockQuantity;
```

The values are exposed through public read-only properties:

```csharp
public string Name
{
    get { return name; }
}

public int StockQuantity
{
    get { return stockQuantity; }
}
```

The values are initialized through constructors and cannot be changed directly from outside the classes.

## Interface

The project defines an interface for notification behavior:

```csharp
internal interface INotifiable
{
    void SendNotification();
}
```

The interface is implemented by two unrelated classes:

```csharp
internal class Customer : INotifiable
```

```csharp
internal class Order : INotifiable
```

Each class provides its own implementation of `SendNotification()`.

## Polymorphism

The application demonstrates polymorphism using a function that accepts the interface type:

```csharp
static void Notify(INotifiable target)
{
    target.SendNotification();
}
```

The same function works with both a `Customer` object and an `Order` object:

```csharp
Notify(customer);
Notify(order);
```

Each object executes its own notification behavior.

## ToString Override

The classes override the `ToString()` method to display their information clearly.

Example from the `Product` class:

```csharp
public override string ToString()
{
    return $"Product: {name} - Available Quantity: {stockQuantity}";
}
```

Example from the `Order` class:

```csharp
public override string ToString()
{
    return $"Product: {product.Name} - Quantity: {quantity} - Customer: {customer.Name}";
}
```

## Application Code

### Program.cs

```csharp
namespace OrderSystemDay3
{
    internal class Program
    {
        static void Notify(INotifiable target)
        {
            target.SendNotification();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Order System:");
            Console.WriteLine();

            Dictionary<string, Product> products =
                new Dictionary<string, Product>();

            products.Add("Laptop", new Product("Laptop", 5));
            products.Add("Phone", new Product("Phone", 10));
            products.Add("Keyboard", new Product("Keyboard", 3));

            CreateOrderRequest request =
                new CreateOrderRequest("Laptop", 2);

            Customer customer =
                new Customer("Mohammad Salameh", "mohammad@gmail.com");

            Console.WriteLine("Create Order Request:");
            Console.WriteLine($"Product: {request.ProductName}");
            Console.WriteLine($"Requested Quantity: {request.Quantity}");

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine();

            if (!products.ContainsKey(request.ProductName))
            {
                Console.WriteLine("The requested product does not exist.");
            }
            else
            {
                Product selectedProduct = products[request.ProductName];

                Console.WriteLine("Product Information:");
                Console.WriteLine(selectedProduct);

                Console.WriteLine();

                if (request.Quantity < 1)
                {
                    Console.WriteLine("The requested quantity must be at least 1.");
                }
                else if (!selectedProduct.IsAvailable(request.Quantity))
                {
                    Console.WriteLine("The requested quantity is not available in stock.");
                }
                else
                {
                    Order order =
                        new Order(selectedProduct, request.Quantity, customer);

                    selectedProduct.ReduceStock(request.Quantity);

                    Console.WriteLine("Customer Information:");
                    Console.WriteLine(customer);

                    Console.WriteLine();
                    Console.WriteLine("Order Information:");
                    Console.WriteLine(order);

                    Console.WriteLine();
                    Console.WriteLine(
                        $"Remaining Stock: {selectedProduct.StockQuantity}"
                    );

                    Console.WriteLine();
                    Console.WriteLine("==============================================================");
                    Console.WriteLine();

                    Console.WriteLine("Notifications:");

                    Notify(customer);
                    Notify(order);
                }
            }
        }
    }
}
```

### Product.cs

```csharp
namespace OrderSystemDay3
{
    internal class Product
    {
        private string name;
        private int stockQuantity;

        public string Name
        {
            get { return name; }
        }

        public int StockQuantity
        {
            get { return stockQuantity; }
        }

        public Product(string name, int stockQuantity)
        {
            this.name = name;

            if (stockQuantity < 0)
            {
                this.stockQuantity = 0;
            }
            else
            {
                this.stockQuantity = stockQuantity;
            }
        }

        public bool IsAvailable(int requestedQuantity)
        {
            return requestedQuantity > 0 &&
                   requestedQuantity <= stockQuantity;
        }

        public void ReduceStock(int requestedQuantity)
        {
            if (IsAvailable(requestedQuantity))
            {
                stockQuantity -= requestedQuantity;
            }
        }

        public override string ToString()
        {
            return $"Product: {name} - Available Quantity: {stockQuantity}";
        }
    }
}
```

### Customer.cs

```csharp
namespace OrderSystemDay3
{
    internal class Customer : INotifiable
    {
        private string name;
        private string email;

        public string Name
        {
            get { return name; }
        }

        public string Email
        {
            get { return email; }
        }

        public Customer(string name, string email)
        {
            if (string.IsNullOrEmpty(name))
            {
                this.name = "Unknown Customer";
            }
            else
            {
                this.name = name;
            }

            if (string.IsNullOrEmpty(email))
            {
                this.email = "No Email";
            }
            else
            {
                this.email = email;
            }
        }

        public void SendNotification()
        {
            Console.WriteLine(
                $"Notification sent to customer {name} at {email}."
            );
        }

        public override string ToString()
        {
            return $"Customer: {name} - Email: {email}";
        }
    }
}
```

### Order.cs

```csharp
namespace OrderSystemDay3
{
    internal class Order : INotifiable
    {
        private Product product;
        private int quantity;
        private Customer customer;

        public Product Product
        {
            get { return product; }
        }

        public int Quantity
        {
            get { return quantity; }
        }

        public Customer Customer
        {
            get { return customer; }
        }

        public Order(Product product, int quantity, Customer customer)
        {
            this.product = product;
            this.quantity = quantity;
            this.customer = customer;
        }

        public void SendNotification()
        {
            Console.WriteLine(
                $"Order notification sent for {product.Name} with quantity {quantity}."
            );
        }

        public override string ToString()
        {
            return $"Product: {product.Name} - Quantity: {quantity} - Customer: {customer.Name}";
        }
    }
}
```

### INotifiable.cs

```csharp
namespace OrderSystemDay3
{
    internal interface INotifiable
    {
        void SendNotification();
    }
}
```

### CreateOrderRequest.cs

```csharp
namespace OrderSystemDay3
{
    internal record CreateOrderRequest(string ProductName, int Quantity);
}
```

## How to Run

1. Open the `OrderSystemDay3` project in Visual Studio.
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
Order System:

Create Order Request:
Product: Laptop
Requested Quantity: 2

==============================================================

Product Information:
Product: Laptop - Available Quantity: 5

Customer Information:
Customer: Mohammad Salameh - Email: mohammad@gmail.com

Order Information:
Product: Laptop - Quantity: 2 - Customer: Mohammad Salameh

Remaining Stock: 3

==============================================================

Notifications:
Notification sent to customer Mohammad Salameh at mohammad@gmail.com.
Order notification sent for Laptop with quantity 2.
```

## Technologies and Tools

- C#
- .NET
- Visual Studio
- Console Application
- Git
- GitHub

## Project Files

- `Program.cs`
- `Product.cs`
- `Customer.cs`
- `Order.cs`
- `INotifiable.cs`
- `CreateOrderRequest.cs`
- `OrderSystemDay3.csproj`

## Day 3 Folder

[View OrderSystemDay3 Project](./OrderSystemDay3)
