# Week 2 — Day 4: ASP.NET Core Project Setup & Routing

## Overview

This exercise introduces ASP.NET Core Web API project structure, routing, Controllers, Minimal APIs, route parameters, and HTTP verbs.

A Web API project was created using Visual Studio with Controllers enabled and top-level statements disabled.

## Learning Objectives

- Understand the ASP.NET Core Web API project structure.
- Understand the minimal hosting model in `Program.cs`.
- Define endpoints using Controllers.
- Define endpoints using Minimal APIs.
- Use route parameters.
- Test API endpoints using Swagger and Postman.

## Project Setup

The project was created using the following Visual Studio template:

```text
ASP.NET Core Web API
```

The selected options included:

```text
Authentication Type: None
Configure for HTTPS: Enabled
Enable OpenAPI Support: Enabled
Use Controllers: Enabled
Do Not Use Top-Level Statements: Enabled
```

## Minimal Hosting Model

The application is configured inside `Program.cs`.

The main setup includes:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

The `builder` registers the services required by the application.

The `app` configures the HTTP request pipeline, maps the endpoints, and starts the application.

## Swagger Setup

Swagger was added to document and test the API endpoints.

### Install the Package

Open the Visual Studio Package Manager Console:

```text
Tools → NuGet Package Manager → Package Manager Console
```

Run:

```powershell
Install-Package Swashbuckle.AspNetCore -Version 10.2.3
```

### Register Swagger Services

The following services were added after `builder.Services.AddOpenApi()`:

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

### Enable Swagger Middleware

The following lines were added inside the development environment condition:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

### Open Swagger Automatically

Inside `Properties/launchSettings.json`, the following setting was added after `"launchBrowser": true`:

```json
"launchUrl": "swagger"
```

Swagger now opens automatically when the project starts.

## Domain Model

A `Product` model was created with the following properties:

```csharp
public int Id { get; set; }

public string Name { get; set; } = string.Empty;

public decimal Price { get; set; }
```

The application uses a hardcoded list containing products such as:

```text
Laptop
Keyboard
Mouse
```

## Controller Endpoints

A `ProductsController` was created to group the product endpoints.

### Get All Products

```text
GET /api/Products
```

Returns the complete list of products.

### Get Product by ID

```text
GET /api/Products/{id}
```

Returns a single product whose ID matches the route parameter.

If the product does not exist, the endpoint returns:

```text
404 Not Found
```

## Minimal API Endpoints

The same two endpoints were added directly inside `Program.cs`.

### Get All Products

```text
GET /minimal/products
```

### Get Product by ID

```text
GET /minimal/products/{id}
```

These endpoints use `Results.Ok` and `Results.NotFound` to return HTTP responses.

## Available Endpoints

| Approach | HTTP Verb | Route | Description |
|---|---|---|---|
| Controller | GET | `/api/Products` | Returns all products |
| Controller | GET | `/api/Products/{id}` | Returns one product by ID |
| Minimal API | GET | `/minimal/products` | Returns all products |
| Minimal API | GET | `/minimal/products/{id}` | Returns one product by ID |

## Controllers vs. Minimal APIs

Controllers organize related endpoints inside separate classes.

They are suitable for larger APIs because they provide clearer organization and separation.

Minimal APIs define endpoints directly inside `Program.cs`.

They require less code and are suitable for small or simple APIs, but `Program.cs` can become difficult to maintain when the number of endpoints increases.

## Postman Testing

All four endpoints were tested using Postman.

The requests were saved inside a collection named:

```text
Week 2 Day 4 - Routing API
```

The collection contains:

```text
Controller - Get All Products
Controller - Get Product By ID
Minimal API - Get All Products
Minimal API - Get Product By ID
```

## What I Learned

- ASP.NET Core services are registered using `builder.Services`.
- The HTTP request pipeline is configured after `builder.Build()`.
- Controllers group related endpoints inside classes.
- Minimal APIs define endpoints directly inside `Program.cs`.
- Route parameters capture values from the URL.
- `GET` is used to read data without modifying it.
- `Ok` represents a successful `200 OK` response.
- `NotFound` represents a `404 Not Found` response.
- Swagger documents and tests API endpoints in the browser.
- Postman collections organize and save related API requests.