# BinX Backend Internship

This repository contains my daily work, exercises, and projects completed during the BinX Backend Internship program.

---

# Week 1

## Day 1 — Program Orientation & .NET Development Environment Setup

Day 1 focused on understanding the BinX Backend Internship program, preparing the .NET development environment, and creating the first C# console application.

### Project Overview

The purpose of Day 1 was to prepare the development environment required for the internship program.

The .NET SDK was installed and verified, Visual Studio and Visual Studio Code were configured, and a simple console application named `HelloBinX` was created.

The application displays my name and the training date in the console.

### Day 1 Objectives

- Understand the structure of the BinX Backend Internship program.
- Install and verify the .NET SDK.
- Configure Visual Studio for C# and .NET development.
- Configure Visual Studio Code with the required C# extension.
- Create a C# console application.
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

### IDE Configuration

The following development environments were prepared:

- Visual Studio with .NET desktop development tools.
- Visual Studio with ASP.NET and web development tools.
- Visual Studio Code with the C# Dev Kit extension.

Visual Studio is the primary development environment used for the internship tasks.

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
- Visual Studio
- Visual Studio Code
- C# Dev Kit
- Terminal
- Git
- GitHub
- Notion

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

Open the `CSharpFundamentalsDay2` project in Visual Studio.

Run the application without debugging:

```text
Ctrl + F5
```

The application can also be built and run from inside the project directory:

```bash
dotnet build
```

```bash
dotnet run
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
- .NET SDK
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
- Verifies that the requested quantity is valid and available.
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

It also checks that the requested quantity is greater than zero:

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

The values can be initialized through constructors but cannot be changed directly from outside the class.

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

Open the `OrderSystemDay3` project in Visual Studio.

Run the application without debugging:

```text
Ctrl + F5
```

The application can also be built and run from inside the project directory:

```bash
dotnet build
```

```bash
dotnet run
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
- .NET SDK
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

---

## Day 4

The complete Day 4 documentation will be added after finishing the assigned work.

---

## Day 5

The complete Day 5 documentation will be added after finishing the assigned work.

---

## Author

Mohammad Salameh
