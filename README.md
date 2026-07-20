# BinX Backend Internship

This repository contains my daily work, exercises, and projects completed during the BinX Backend Internship program.

---

# Week 1

## Day 1 — HelloBinX

A simple C# console application created as part of Day 1 of the BinX Backend Internship program.

### Project Overview

This project was created to verify the .NET development environment and practice using the `dotnet` CLI.

The application displays my name and the current date in the console.

### Day 1 Objectives

- Understand the internship program structure.
- Install and verify the .NET SDK.
- Configure Visual Studio and Visual Studio Code.
- Create a console application using the `dotnet` CLI.
- Modify the application using C#.
- Build and run the project successfully.
- Upload the completed project to GitHub.

### Technologies and Tools

- C#
- .NET SDK
- dotnet CLI
- Visual Studio
- Visual Studio Code
- Git
- GitHub

### Application Code

```csharp
Console.WriteLine("Mohammad Salameh");
Console.WriteLine($"Today's date: {DateTime.Today:dd/MM/yyyy}");
```

### How to Run

Open the Day 1 project directory:

```bash
cd "BinX Internship/Week 1/Day 1"
```

Build the project:

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
Today's date: DD/MM/YYYY
```

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

The application demonstrates value-type copy behavior using two integer variables.

```csharp
int originalNumber = 10;
int copiedNumber = originalNumber;

copiedNumber = 20;
```

Changing the copied value does not affect the original value.

```text
Original number: 10
Copied number: 20
```

The application demonstrates reference-type copy behavior using an array.

```csharp
int[] originalNumbers = { 10, 20, 30 };
int[] copiedNumbers = originalNumbers;

copiedNumbers[0] = 100;
```

Both variables reference the same array. Therefore, changing the first element through `copiedNumbers` also changes the value accessed through `originalNumbers`.

```text
Original first value: 100
Copied first value: 100
```

### Grade Classifier

The application contains a return-type function that receives a score and returns its classification using a switch expression.

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

Open the Day 2 project directory:

```bash
cd "BinX Internship/Week 1/Day 2/CSharpFundamentalsDay2"
```

Build the project:

```bash
dotnet build
```

Run the application:

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
skills Type: System.Collections.Generic.List...

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

### Project Files

- `Program.cs`
- `CSharpFundamentalsDay2.csproj`

### Day 2 Folder

[View Day 2 Work](./BinX%20Internship/Week%201/Day%202/CSharpFundamentalsDay2)

---

## Day 3

The complete Day 3 documentation will be added after finishing the assigned work.

---

## Day 4

The complete Day 4 documentation will be added after finishing the assigned work.

---

## Day 5

The complete Day 5 documentation will be added after finishing the assigned work.

---

## Author

Mohammad Salameh
