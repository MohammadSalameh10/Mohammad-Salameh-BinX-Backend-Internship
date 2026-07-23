# Day 1 — Program Orientation & .NET Development Environment Setup

Day 1 focused on understanding the BinX Backend Internship program, preparing the .NET development environment, and creating the first C# console application.

## Project Overview

The purpose of Day 1 was to prepare the development environment required for the internship program.

The .NET SDK was installed and verified, Visual Studio Code was configured for C# development, and a simple console application named `HelloBinX` was created.

The application displays my name and the training date in the console.

## Day 1 Objectives

- Understand the structure of the BinX Backend Internship program.
- Install and verify the .NET SDK.
- Configure Visual Studio Code for C# development.
- Install and configure the C# Dev Kit extension.
- Create a C# console application using the .NET CLI.
- Modify the application code.
- Build and run the application successfully.
- Upload the completed project to GitHub.

## Development Environment Setup

The installed .NET SDK was verified using:

```bash
dotnet --version
```

Additional information about the installed .NET environment was checked using:

```bash
dotnet --info
```

## Creating the Console Application

The `HelloBinX` console application was created using the .NET CLI:

```bash
dotnet new console -n HelloBinX
```

## Application Code

The console application displays my name and the training date:

```csharp
Console.WriteLine("Mohammad Salameh");
Console.WriteLine("19/07/2026");
```

## How to Run

From inside the `HelloBinX` project directory, build the application:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

## Expected Output

```text
Mohammad Salameh
19/07/2026
```

## Technologies and Tools

- C#
- .NET SDK
- dotnet CLI
- Visual Studio Code
- C# Dev Kit
- Terminal
- Git
- GitHub

## Project Files

- `Program.cs`
- `HelloBinX.csproj`

## Day 1 Folder

[View HelloBinX Project](./HelloBinX)
