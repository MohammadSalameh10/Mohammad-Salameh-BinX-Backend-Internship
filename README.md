# BinX Backend Internship

This repository contains my daily work, exercises, and projects completed during the BinX Backend Internship program.

---

# Week 1

## Day 1 — Program Orientation & .NET Development Environment Setup

Day 1 focused on understanding the BinX Backend Internship program, preparing the .NET development environment, and creating the first C# console application.

### Project Overview

The purpose of Day 1 was to prepare the development environment required for the internship program.

The .NET SDK was installed and verified, Visual Studio Code was configured for C# development, and a simple console application named `HelloBinX` was created.

The application displays my name and the training date in the console.

### Day 1 Objectives

- Understand the structure of the BinX Backend Internship program.
- Install and verify the .NET SDK.
- Configure Visual Studio Code for C# development.
- Install and configure the C# Dev Kit extension.
- Create a C# console application using the .NET CLI.
- Modify the application code.
- Build and run the application successfully.
- Upload the completed project to GitHub.

### Development Environment Setup

The installed .NET SDK was verified using:

```bash
dotnet --version
```

Additional information about the installed .NET environment was checked using:

```bash
dotnet --info
```

### Creating the Console Application

The `HelloBinX` console application was created using the .NET CLI:

```bash
dotnet new console -n HelloBinX
```

### Application Code

The console application displays my name and the training date:

```csharp
Console.WriteLine("Mohammad Salameh");
Console.WriteLine("19/07/2026");
```

### How to Run

From inside the `HelloBinX` project directory, build the application:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

### Expected Output

```text
Mohammad Salameh
19/07/2026
```

### Technologies and Tools

- C#
- .NET SDK
- dotnet CLI
- Visual Studio Code
- C# Dev Kit
- Terminal
- Git
- GitHub

### Project Files

- `Program.cs`
- `HelloBinX.csproj`

### Day 1 Folder

[View Day 1 Work](./BinX%20Internship/Week%201/Day%201)

---

## Day 2 — C# Fundamentals I: Types, Variables & Control Flow

A C# console application created as part of Day 2 of the BinX Backend Internship program.

### Project Overview

This project demonstrates fundamental C# concepts related to data types, variables, functions, control flow, reference types, and safe console input handling.

The application:

- Creates three value-type variables.
- Creates three reference-type variables.
- Prints each variable's runtime type using `GetType()`.
- Demonstrates value-type copy behavior.
- Demonstrates reference-type copy behavior.
- Classifies a score using a switch expression.
- Reads user input and handles a possibly-null or empty value safely.

### Day 2 Objectives

- Distinguish value types from reference types.
- Create variables using clear and meaningful names.
- Print variable values and their runtime types.
- Demonstrate value-type copy behavior.
- Demonstrate reference-type copy behavior.
- Create void functions.
- Create return-type functions.
- Use function parameters.
- Classify scores using a switch expression.
- Read console input safely.
- Handle possibly-null and empty string values.
- Build and run the application successfully.

### Concepts Applied

- Numeric data types
- Text-based data types
- Boolean data type
- Variables
- Arrays
- Lists
- Console input and output
- String interpolation
- If statements
- Switch expression
- Void functions
- Return-type functions
- Function parameters
- Nullable reference types
- `GetType()`
- `string.IsNullOrEmpty()`

### Value Types

The application uses three value-type variables:

```csharp
int age = 24;
double salary = 1500.50;
bool isActive = true;
```

The runtime type of each variable is printed using `GetType()`:

```csharp
Console.WriteLine($"age: {age} - Type: {age.GetType()}");
Console.WriteLine($"salary: {salary} - Type: {salary.GetType()}");
Console.WriteLine($"isActive: {isActive} - Type: {isActive.GetType()}");
```

### Reference Types

The application uses three reference-type variables:

```csharp
string name = "Mohammad";
int[] numbers = { 10, 20, 30 };

List<string> skills = new List<string>
{
    "C#",
    ".NET",
    "Git"
};
```

The runtime type of each variable is also printed using `GetType()`.

### Copy Behavior

The application demonstrates value-type copy behavior using two integer variables:

```csharp
int originalNumber = 10;
int copiedNumber = originalNumber;

copiedNumber = 20;
```

Changing the copied value does not affect the original value:

```text
Original number: 10
Copied number: 20
```

The application demonstrates reference-type copy behavior using an array:

```csharp
int[] originalNumbers = { 10, 20, 30 };
int[] copiedNumbers = originalNumbers;

copiedNumbers[0] = 100;
```

Both variables reference the same array. Changing the first element through `copiedNumbers` also changes the value accessed through `originalNumbers`:

```text
Original first value: 100
Copied first value: 100
```

### Grade Classifier

The application contains a return-type function that receives a score and returns its classification using a switch expression:

```csharp
static string DescribeGrade(int score)
{
    return score switch
    {
        >= 90 => "Excellent",
        >= 70 => "Proficient",
        >= 50 => "Developing",
        _ => "Below Standard"
    };
}
```

The score ranges are:

- `90` or higher: Excellent
- `70` to `89`: Proficient
- `50` to `69`: Developing
- Below `50`: Below Standard

The score used in the application is:

```csharp
int score = 90;
```

The result is:

```text
Score: 90
Grade: Excellent
```

### Nullable Input Handling

The application reads the user's name from the console:

```csharp
string? name = Console.ReadLine();
```

Because the value may be `null` or empty, it is checked before being used:

```csharp
if (string.IsNullOrEmpty(name))
{
    Console.WriteLine("No name was entered.");
}
else
{
    Console.WriteLine($"Hello, {name}");
}
```

### Application Code

```csharp
namespace CSharpFundamentalsDay2
{
    internal class Program
    {
        static void DemonstrateCopyBehavior()
        {
            Console.WriteLine("Copy Behavior:");

            int originalNumber = 10;
            int copiedNumber = originalNumber;

            Console.WriteLine("Value Type before change:");
            Console.WriteLine($"Original number: {originalNumber}");
            Console.WriteLine($"Copied number: {copiedNumber}");

            copiedNumber = 20;

            Console.WriteLine("Value Type after change:");
            Console.WriteLine($"Original number: {originalNumber}");
            Console.WriteLine($"Copied number: {copiedNumber}");

            Console.WriteLine();

            int[] originalNumbers = { 10, 20, 30 };
            int[] copiedNumbers = originalNumbers;

            Console.WriteLine("Reference Type before change:");
            Console.WriteLine($"Original first value: {originalNumbers[0]}");
            Console.WriteLine($"Copied first value: {copiedNumbers[0]}");

            copiedNumbers[0] = 100;

            Console.WriteLine("Reference Type after change:");
            Console.WriteLine($"Original first value: {originalNumbers[0]}");
            Console.WriteLine($"Copied first value: {copiedNumbers[0]}");
        }

        static string DescribeGrade(int score)
        {
            return score switch
            {
                >= 90 => "Excellent",
                >= 70 => "Proficient",
                >= 50 => "Developing",
                _ => "Below Standard"
            };
        }

        static void HandleNullableInput()
        {
            Console.WriteLine("Nullable Input Handling:");

            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("No name was entered.");
            }
            else
            {
                Console.WriteLine($"Hello, {name}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Value Types:");

            int age = 24;
            double salary = 1500.50;
            bool isActive = true;

            Console.WriteLine($"age: {age} - Type: {age.GetType()}");
            Console.WriteLine($"salary: {salary} - Type: {salary.GetType()}");
            Console.WriteLine($"isActive: {isActive} - Type: {isActive.GetType()}");

            Console.WriteLine();
            Console.WriteLine("Reference Types:");

            string name = "Mohammad";
            int[] numbers = { 10, 20, 30 };

            List<string> skills = new List<string>
            {
                "C#",
                ".NET",
                "Git"
            };

            Console.WriteLine($"name: {name} - Type: {name.GetType()}");
            Console.WriteLine($"numbers Type: {numbers.GetType()}");
            Console.WriteLine($"skills Type: {skills.GetType()}");

            Console.WriteLine();
            Console.WriteLine("==============================================================");

            Console.WriteLine();
            DemonstrateCopyBehavior();

            Console.WriteLine();
            Console.WriteLine("==============================================================");

            Console.WriteLine();
            Console.WriteLine("Grade Classifier:");

            int score = 90;
            string gradeDescription = DescribeGrade(score);

            Console.WriteLine($"Score: {score}");
            Console.WriteLine($"Grade: {gradeDescription}");

            Console.WriteLine();
            Console.WriteLine("==============================================================");

            Console.WriteLine();
            HandleNullableInput();
        }
    }
}
```

### How to Run

1. Open the `CSharpFundamentalsDay2` project in Visual Studio.
2. Build the solution using:

```text
Ctrl + Shift + B
```

3. Run the application without debugging using:

```text
Ctrl + F5
```

### Expected Output

```text
Value Types:
age: 24 - Type: System.Int32
salary: 1500.5 - Type: System.Double
isActive: True - Type: System.Boolean

Reference Types:
name: Mohammad - Type: System.String
numbers Type: System.Int32[]
skills Type: System.Collections.Generic.List`1[System.String]

==============================================================

Copy Behavior:
Value Type before change:
Original number: 10
Copied number: 10
Value Type after change:
Original number: 10
Copied number: 20

Reference Type before change:
Original first value: 10
Copied first value: 10
Reference Type after change:
Original first value: 100
Copied first value: 100

==============================================================

Grade Classifier:
Score: 90
Grade: Excellent

==============================================================

Nullable Input Handling:
Enter your name:
```

When a valid name is entered:

```text
Hello, Mohammad
```

When no name is entered:

```text
No name was entered.
```

### Technologies and Tools

- C#
- .NET
- Visual Studio
- Console Application
- Git
- GitHub

### Project Files

- `Program.cs`
- `CSharpFundamentalsDay2.csproj`

### Day 2 Folder

[View Day 2 Work](./BinX%20Internship/Week%201/Day%202/CSharpFundamentalsDay2)

---

## Day 3 — C# Fundamentals II: Object-Oriented Programming

An order management console application created as part of Day 3 of the BinX Backend Internship program.

### Project Overview

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

### Day 3 Objectives

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

### Concepts Applied

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

### Domain Model

The project contains the following domain types:

- `Customer`: Represents the customer who creates an order.
- `Product`: Represents a product and its available stock.
- `Order`: Represents a successful order associated with a customer and product.
- `CreateOrderRequest`: Represents immutable order-request data.
- `INotifiable`: Defines notification behavior implemented by different classes.

### Product Inventory

The application stores products in a dictionary:

```csharp
Dictionary<string, Product> products =
    new Dictionary<string, Product>();

products.Add("Laptop", new Product("Laptop", 5));
products.Add("Phone", new Product("Phone", 10));
products.Add("Keyboard", new Product("Keyboard", 3));
```

The product name is used as the dictionary key, while the `Product` object stores the product information and available quantity.

### Immutable Order Request

The order request is represented using a record:

```csharp
internal record CreateOrderRequest(string ProductName, int Quantity);
```

The application creates the following request:

```csharp
CreateOrderRequest request =
    new CreateOrderRequest("Laptop", 2);
```

### Product Validation

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

### Stock Management

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

### Encapsulation

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

### Interface

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

### Polymorphism

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

### ToString Override

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

### Application Code

#### Program.cs

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

#### Product.cs

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

#### Customer.cs

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

#### Order.cs

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

#### INotifiable.cs

```csharp
namespace OrderSystemDay3
{
    internal interface INotifiable
    {
        void SendNotification();
    }
}
```

#### CreateOrderRequest.cs

```csharp
namespace OrderSystemDay3
{
    internal record CreateOrderRequest(string ProductName, int Quantity);
}
```

### How to Run

1. Open the `OrderSystemDay3` project in Visual Studio.
2. Build the solution using:

```text
Ctrl + Shift + B
```

3. Run the application without debugging using:

```text
Ctrl + F5
```

### Expected Output

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

### Technologies and Tools

- C#
- .NET
- Visual Studio
- Console Application
- Git
- GitHub

### Project Files

- `Program.cs`
- `Product.cs`
- `Customer.cs`
- `Order.cs`
- `INotifiable.cs`
- `CreateOrderRequest.cs`
- `OrderSystemDay3.csproj`

### Day 3 Folder

[View Day 3 Work](./BinX%20Internship/Week%201/Day%203/OrderSystemDay3)

---

## Day 4 — C# Fundamentals III: Collections & LINQ Basics

A product management console application created as part of Day 4 of the BinX Backend Internship program.

### Project Overview

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

### Day 4 Objectives

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

### Concepts Applied

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

### Product Model

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

### List

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

### Dictionary

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

### HashSet

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

### LINQ Filtering

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

### LINQ Projection

`Select()` transforms the product collection into a list containing only product names:

```csharp
List<string> productNames = products
    .Select(product => product.Name)
    .ToList();
```

The original collection is a `List<Product>`, while the projected result is a `List<string>`.

### LINQ Ordering

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

### LINQ Aggregation

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

### LINQ Query Syntax

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

### Async and Await

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

### Exception Handling

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

### How to Run

1. Open the `CollectionsLinqDay4` project in Visual Studio.
2. Build the solution using:

```text
Ctrl + Shift + B
```

3. Run the application without debugging using:

```text
Ctrl + F5
```

### Expected Output

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

### Technologies and Tools

- C#
- .NET
- LINQ
- Visual Studio
- Console Application
- Git
- GitHub

### Project Files

- `Program.cs`
- `Product.cs`
- `CollectionsLinqDay4.csproj`

### Day 4 Folder

[View Day 4 Work](./BinX%20Internship/Week%201/Day%204/CollectionsLinqDay4)

---

## Day 5

The complete Day 5 documentation will be added after finishing the assigned work.

---

## Author

Mohammad Salameh
