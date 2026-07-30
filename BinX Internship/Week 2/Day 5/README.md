# Week 2 — Day 5: Middleware Pipeline & Dependency Injection

## Overview

This project demonstrates how requests move through the ASP.NET Core middleware pipeline and how dependency injection is used to provide services to controllers.

The application includes custom request-logging middleware, middleware ordering experiments, a scoped product service, and constructor injection inside `ProductsController`.

## Learning Objectives

- Understand how requests move through the middleware pipeline.
- Explain how middleware registration order affects execution.
- Create and register custom middleware.
- Understand dependency injection and service lifetimes.
- Register a service using the appropriate lifetime.
- Inject a service into a controller through constructor injection.

## Custom Request Logging Middleware

A custom middleware component was created to log information about each incoming request:

- HTTP method
- Request path
- Selected endpoint

Example:

```text
----- Incoming Request -----
Method: GET
Path: /api/Products
Endpoint: MiddlewareDependencyInjectionDay5.Controllers.ProductsController.GetProducts
----------------------------
```

The middleware passes the request to the next pipeline component using:

```csharp
await _next(context);
```

## Middleware Ordering

The middleware was first registered before routing:

```csharp
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseRouting();
```

Because routing had not selected an endpoint yet, the output displayed:

```text
Endpoint: Not selected yet
```

The order was then corrected:

```csharp
app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();
```

After `UseRouting` selected the endpoint, the custom middleware was able to display the controller and action handling the request.

## Dependency Injection

The product functionality was moved from `ProductsController` into a separate service.

The service contract is defined by:

```text
IProductService
```

The implementation is provided by:

```text
ProductService
```

The service was registered inside `Program.cs`:

```csharp
builder.Services.AddScoped<IProductService, ProductService>();
```

The `Scoped` lifetime creates one service instance for each HTTP request.

## Constructor Injection

`IProductService` is injected into `ProductsController` through its constructor:

```csharp
private readonly IProductService _productService;

public ProductsController(IProductService productService)
{
    _productService = productService;
}
```

The controller depends on the interface instead of creating `ProductService` using `new`.

The dependency injection container automatically resolves and supplies the registered implementation.

## API Endpoints

### Get All Products

```http
GET /api/Products
```

Returns all available products.

### Get Product by ID

```http
GET /api/Products/{id}
```

Returns the product matching the provided ID.

When the product does not exist, the endpoint returns `404 Not Found`.

Example:

```text
Product with ID 20 was not found.
```

## Project Structure

```text
MiddlewareDependencyInjectionDay5/
├── Controllers/
│   └── ProductsController.cs
├── Middleware/
│   └── RequestLoggingMiddleware.cs
├── Models/
│   └── Product.cs
├── Services/
│   ├── IProductService.cs
│   └── ProductService.cs
└── Program.cs
```

## What I Learned

- Middleware executes in the order in which it is registered.
- `UseRouting` must execute before middleware that needs the selected endpoint.
- Custom middleware can inspect each incoming HTTP request.
- Dependency injection allows classes to receive their dependencies from the application container.
- `Scoped` services use one instance for each HTTP request.
- Constructor injection keeps controllers independent from concrete service implementations.
- Controllers should receive dependencies instead of constructing them internally.

## Technologies and Tools

- C#
- ASP.NET Core Web API
- Middleware
- Dependency Injection
- Swagger
- Visual Studio
- Git
- GitHub