# BinX Backend Internship

This repository contains my daily work, exercises, documentation, and projects completed during the BinX Backend Internship program.

## Internship Progress

| Week   | Focus                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     | Documentation                               |
| ------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------- |
| Week&nbsp;1 | .NET setup, C# fundamentals, OOP, collections, LINQ basics, async/await, exception handling, and Git workflow                                                                                                                                                                                                                                                                                                                                                                                            | [View Week 1](./BinX%20Internship/Week%201) |
| Week&nbsp;2 | Generics, advanced collections, advanced LINQ, asynchronous programming, concurrency, ASP.NET Core Web APIs, routing, middleware, and dependency injection                                                                                                                                                                                                                                                                                                                                                | [View Week 2](./BinX%20Internship/Week%202) |
| Week&nbsp;3 | REST API design, SQL Server schema design, database normalization, Entity Framework Core Code-First development, migrations, asynchronous CRUD operations, request validation, service layers, Postman environments, automated API testing, and documentation                                                                                                                                                                                                                                               | [View Week 3](./BinX%20Internship/Week%203) |
| Week&nbsp;4 | ASP.NET Core Identity, user registration, password hashing and validation, JWT authentication, token issuance, protected routes, role-based access control, claims-based and policy-based authorization, Postman token reuse, FluentValidation, business validation rules, structured validation errors, rate limiting, CORS, HTTPS redirection, HSTS, and SQL injection prevention practices | [View Week 4](./BinX%20Internship/Week%204) |
| Week&nbsp;5 | xUnit unit testing, dedicated test projects, service-layer unit testing, mocking dependencies with Moq, repository abstraction, integration testing with `WebApplicationFactory`, Entity Framework Core InMemory test databases, authenticated endpoint testing with JWT, centralized error handling, global exception middleware, standardized `ProblemDetails` responses, structured logging with `ILogger`, risk-based testing, `[Fact]`, `[Theory]`, `[InlineData]`, Arrange-Act-Assert, `dotnet test`, and Visual Studio Test Explorer | [View Week 5](./BinX%20Internship/Week%205) |

## Repository Structure

```text
BinX Internship/
├── Week 1/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
├── Week 2/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
├── Week 3/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
├── Week 4/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
└── Week 5/
    ├── README.md
    ├── Day 1/
    ├── Day 2/
    ├── Day 3/
    ├── Day 4/
    └── Day 5/
```

Each week contains a summary README, and each completed day contains its own task documentation and project files when implementation is required.

## Current Learning Areas

### C# and .NET

* C# and .NET fundamentals
* Object-oriented programming
* Collections and LINQ
* Generic programming
* Generic constraints and collection interfaces
* Deferred and immediate LINQ execution
* Advanced LINQ operations with `GroupBy`, `Join`, and `SelectMany`
* Asynchronous programming with `async` and `await`
* Sequential and concurrent execution
* Running independent operations with `Task.WhenAll`
* Cancelling asynchronous operations with `CancellationToken`

### ASP.NET Core

* ASP.NET Core Web API development
* The minimal hosting model in `Program.cs`
* Controllers and Minimal APIs
* Routes, route parameters, and HTTP verbs
* Middleware pipeline and execution ordering
* Custom request-logging middleware
* Dependency Injection and service lifetimes
* Constructor injection using interfaces
* ASP.NET Core built-in Rate Limiting
* Fixed-window rate limiting policies
* Applying endpoint rate-limit policies with `EnableRateLimiting`
* CORS configuration using named policies
* HTTPS redirection
* HSTS configuration
* Security hardening in the middleware pipeline
* Centralized exception handling using custom middleware
* Global handling of unhandled exceptions
* Returning standardized API errors using `ProblemDetails`
* Preventing exception messages and stack traces from being exposed to clients
* Structured logging using `ILogger`
* Logging request context such as the request path

### REST APIs

* REST API design principles
* Resource-based API modeling
* RESTful resource naming conventions
* Nested resource relationships
* Correct HTTP status-code usage
* URL-based API versioning

### SQL Server and Database Design

* SQL Server database design
* Database normalization using `1NF`, `2NF`, and `3NF`
* Primary keys and foreign keys
* One-to-many database relationships
* SQL Server column-type selection
* Entity Relationship Diagrams
* Database design using `dbdiagram.io`
* Database implementation using SQL Server Management Studio

### Entity Framework Core

* Entity Framework Core with SQL Server
* Entity models and navigation properties
* `DbContext` and `DbSet<T>`
* Entity configuration using Fluent API
* Connection strings and database configuration
* Code-First migrations
* Generating migrations with `Add-Migration`
* Applying migrations with `Update-Database`
* Verifying generated tables using SQL Server Object Explorer
* Asynchronous CRUD operations
* Creating records with `Add` and `SaveChangesAsync`
* Read-only queries using `AsNoTracking`
* Asynchronous queries using `ToListAsync` and `FirstOrDefaultAsync`
* Entity Framework Core change tracking
* Updating tracked entities
* Deleting entities with `Remove`
* EF Core query parameterization
* SQL injection prevention with LINQ and parameterized queries
* Reviewing raw SQL usage for security risks

### API Architecture and Validation

* Request models for create and update operations
* Request validation using Data Annotations
* Input validation using FluentValidation
* Dedicated validator classes using `AbstractValidator<T>`
* Defining validation rules using `RuleFor`
* Creating custom validation messages using `WithMessage`
* Business-oriented validation rules
* Conditional validation using `When`
* Separating validation logic from request models
* Automatic request validation before controller execution
* Structured `400 Bad Request` validation responses
* Service interfaces and service implementations
* Separating database operations from API controllers
* Registering application services using Dependency Injection
* Returning `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found`, and `429 Too Many Requests`
* Returning resource locations using `CreatedAtAction`
* Managing delete order for related database records

### ASP.NET Core Identity

* ASP.NET Core Identity integration
* `IdentityUser` and `IdentityRole`
* `IdentityDbContext<IdentityUser>`
* Identity integration with Entity Framework Core
* Identity database migrations
* Identity service registration
* `UserManager<IdentityUser>`
* `RoleManager<IdentityRole>`
* `SignInManager<IdentityUser>`
* User registration using `UserManager.CreateAsync`
* Credential verification using `CheckPasswordSignInAsync`
* Role creation using `RoleManager`
* Assigning users to roles using `UserManager.AddToRoleAsync`
* Retrieving user roles using `UserManager.GetRolesAsync`
* Password hashing using ASP.NET Core Identity
* PBKDF2 password hashing
* Unique password salts
* Built-in password validation
* Authentication and authorization middleware

### JWT Authentication

* JWT structure: Header, Payload, and Signature
* JWT claims
* User ID and email claims
* Role claims
* Custom permission claims
* Login using ASP.NET Core Identity
* JWT token generation
* JWT signing using HMAC SHA-256
* JWT issuer and audience configuration
* Short-lived access tokens
* 15-minute token expiration
* JWT Bearer Authentication
* JWT issuer validation
* JWT audience validation
* JWT lifetime validation
* JWT signing-key validation
* Protecting API endpoints using `[Authorize]`
* Sending JWTs using the Bearer authentication scheme
* Returning `401 Unauthorized` for invalid credentials
* Rejecting missing or expired JWTs
* Decoding JWTs and verifying claims

### Authorization

* Protecting controllers and endpoints using `[Authorize]`
* Understanding authentication vs authorization
* Role-based access control
* Restricting endpoints using `[Authorize(Roles = "Admin")]`
* Understanding `401 Unauthorized` vs `403 Forbidden`
* Claims-based authorization
* Custom permission claims
* Policy-based authorization
* Named authorization policies
* Requiring claims using `RequireClaim`
* Applying policies using `[Authorize(Policy = "...")]`
* Combining JWT authentication with authorization rules

### FluentValidation

* Comparing DataAnnotations and FluentValidation
* Creating dedicated validator classes
* `AbstractValidator<T>`
* `RuleFor`
* `NotEmpty`
* `MaximumLength`
* `GreaterThan`
* `When`
* `WithMessage`
* Validating `CreateTaskRequest`
* Validating `UpdateTaskRequest`
* Validating positive user IDs
* Validating future due dates
* Registering validators using assembly scanning
* Automatic FluentValidation integration
* Structured validation error responses
* Preventing invalid requests from reaching controller actions

### API Security Hardening

* Rate limiting to reduce excessive request patterns
* Stricter rate limiting for login endpoints
* Returning `429 Too Many Requests` when limits are exceeded
* Named CORS policies
* Restricting allowed frontend origins
* Testing allowed and disallowed origins
* Understanding browser-enforced CORS behavior
* HTTPS redirection
* Testing `307 Temporary Redirect`
* HSTS outside the Development environment
* Understanding Content-Security-Policy as a security concept
* SQL injection prevention
* EF Core automatic query parameterization
* Reviewing `FromSqlRaw`, `ExecuteSqlRaw`, `FromSqlInterpolated`, and manually written SQL usage

### Postman and API Testing

* API design and testing with Postman
* Testing successful and invalid API requests
* Organizing Postman collections by API resource
* Testing success paths and realistic error paths
* Writing automated Postman tests using `pm.test`
* Asserting HTTP status codes and response properties
* Creating Postman environments
* Using the `baseUrl` environment variable
* Exporting and sharing Postman collections
* Testing JWT login and token issuance
* Sending Bearer tokens to protected endpoints
* Testing expired JWT rejection
* Storing JWTs in environment variables
* Capturing login tokens using Post-response scripts
* Reusing `{{token}}` automatically in protected requests
* Testing role-based authorization
* Testing policy-based authorization
* Testing FluentValidation rules individually
* Verifying field-specific validation messages
* Verifying structured `400 Bad Request` responses
* Testing rate-limit rejection with `429 Too Many Requests`
* Testing allowed and disallowed CORS origins
* Inspecting `Access-Control-Allow-Origin`
* Disabling automatic redirect following to test HTTPS redirection
* Verifying `307 Temporary Redirect` and the HTTPS `Location` header

### Unit Testing with xUnit

* Unit testing small units of application logic independently
* Applying unit testing concepts to the Cardiac Patient Monitoring System API
* Creating a dedicated xUnit test project
* Referencing the main ASP.NET Core API project from the test project
* Testing service-layer business logic using `VitalSignService`
* Adding and testing `GetHeartRateStatus(int heartRate)` in `IVitalSignService` and `VitalSignService`
* Writing tests using `[Fact]`
* Writing parameterized tests using `[Theory]`
* Providing multiple test cases using `[InlineData]`
* Organizing tests using the Arrange-Act-Assert pattern
* Testing service methods without external dependencies
* Using descriptive test naming based on method, scenario, and expected result
* Running tests using Visual Studio Test Explorer
* Running individual tests and all tests
* Reviewing passed, failed, and skipped test results
* Mocking service dependencies using Moq
* Creating mocked implementations of repository interfaces
* Configuring mocked return values using `Setup` and `ReturnsAsync`
* Simulating dependency failures using `ThrowsAsync`
* Verifying mocked method calls using `Verify` and `Times.Once`
* Testing service-layer logic without accessing the real database
* Integration testing ASP.NET Core endpoints using `WebApplicationFactory<Program>`
* Hosting the API in-memory during integration tests
* Sending real HTTP requests using `HttpClient`
* Replacing the development SQL Server database with Entity Framework Core InMemory during testing
* Preparing controlled and repeatable integration test data
* Testing protected endpoints using valid JWT Bearer tokens
* Testing role-based authorization with the `Admin` role
* Verifying successful `200 OK` responses and complete response bodies
* Verifying `404 Not Found` error paths
* Verifying `401 Unauthorized` responses for missing authentication
* Running unit and integration tests together using Visual Studio Test Explorer
* Applying risk-based testing to prioritize high-risk service operations.
* Testing `CreateAsync`, `UpdateAsync`, and `DeleteAsync` in `VitalSignService`.
* Testing both successful and failure paths for data modification operations.
* Verifying repository interactions for create, update, and delete operations.
* Verifying that unnecessary database operations are not performed during failure paths.
* Running the complete test suite using `dotnet test`.
* Interpreting complete test suite results and confirming that all tests pass successfully.

### Centralized Error Handling and Logging

* Understanding the problems caused by repeated `try/catch` blocks across controllers
* Implementing custom global exception-handling middleware
* Registering exception middleware early in the ASP.NET Core request pipeline
* Allowing unexpected exceptions to propagate to a centralized handler
* Returning `500 Internal Server Error` for unhandled exceptions
* Returning standardized error responses using `ProblemDetails`
* Using `title`, `status`, and `instance` in error responses
* Preventing internal exception messages from being exposed to API clients
* Preventing stack traces from being exposed to API clients
* Logging complete exception details on the server
* Using `ILogger` for structured logging
* Logging the request path as structured context
* Testing global exception handling using a deliberately failing endpoint
* Verifying safe error responses using Postman
* Reviewing controllers for redundant general-purpose `try/catch` blocks

### Development Tools and Workflow

* Swagger and OpenAPI documentation
* Visual Studio
* Visual Studio Test Explorer
* SQL Server Management Studio
* Visual Studio Package Manager Console
* Postman
* jwt.io
* xUnit
* Git and GitHub workflows
* ASP.NET Core `ILogger`
* Structured logging

## Author

Mohammad Salameh
